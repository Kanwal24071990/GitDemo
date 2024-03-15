﻿using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPartSummaryBillTo
{
    internal class FleetPartSummaryBillToDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string userType;

                if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
                {
                    userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                }
                else
                {
                    userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                }

                if (userType == "ADMIN")
                {
                    query = @"SELECT TOP 1 CONVERT (Date , invoicedate) AS FromDate, Convert (Date, invoicedate) AS ToDate FROM part_tb PT 
                        INNER JOIN partCategoryCode_tb ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId 
                        RIGHT OUTER JOIN Invoice_tb IV INNER JOIN  invoiceSection_tb ON IV.invoiceId = invoiceSection_tb.invoiceId 
                        INNER JOIN invoiceLineDetail_tb ID ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId ON PT.partId = ID.itemId 
                        WHERE  ID.lineDetailType = 'P' AND(ISNULL(ID.accountingDocumentTypeId, 0) = 0 OR ID.accountingDocumentTypeId = (SELECT lookUpId FROM lookup_tb WITH (NOLOCK) 
                        WHERE parentLookUpCode = 218 AND lookupcode = 1)) AND ID.rebateType IN (0,2) AND iv.isActive = 1 ORDER BY invoicedate DESC;";
                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
                else if (userType == "FLEET")
                {
                    query = @"DECLARE @@Userid as int DECLARE @@FleetAccessLocations table (entityDetailId INT  primary key) 
                        select @@Userid = userid FROM user_tb where username = @UserName; 
                        WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb  WITH(NOLOCK) 
                        WHERE  entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM userRelationships_tb WITH(NOLOCK)INNER JOIN user_tb WITH(NOLOCK) 
                        ON userRelationships_tb.userId = user_tb.userId  WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1) 
                        UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) 
                        ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT   entityDetailId FROM    RootNumber 
                        where  parentEntityDetailId<> entityDetailId   and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId 
                        FROM userRelationships_tb  WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; 
                        Select top 1 Convert(Date, invoicedate) as FromDate ,Convert(Date, invoicedate) as ToDate  FROM part_tb as PT INNER JOIN partCategoryCode_tb 
                        ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId RIGHT OUTER JOIN Invoice_tb as IV INNER JOIN invoiceSection_tb ON
                        IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN invoiceLineDetail_tb as ID ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId ON PT.partId = ID.itemId
                        WHERE  ID.lineDetailType = 'P' and  (ISNULL(ID.accountingDocumentTypeId,0) = 0 OR ID.accountingDocumentTypeId = (Select lookUpId FROM lookup_tb with (nolock) 
                        where parentLookUpCode = 218 and lookupcode = 1)) AND ID.rebateType IN (0,2) and iv.isActive = 1 and iv.receiverEntityDetailId in (Select entityDetailId 
                        FROM @@FleetAccessLocations) order by invoiceDate desc";
                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                ToDate = null;
            }
        }
    }
}