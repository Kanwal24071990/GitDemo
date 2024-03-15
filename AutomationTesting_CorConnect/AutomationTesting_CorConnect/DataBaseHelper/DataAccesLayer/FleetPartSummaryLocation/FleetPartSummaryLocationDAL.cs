using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPartSummaryLocation
{
    internal class FleetPartSummaryLocationDAL : BaseDataAccessLayer
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
                    query = @"SELECT  top 1 CONVERT (Date , invoicedate) as FromDate ,Convert (Date , invoicedate) as 
                        ToDate  FROM part_tb AS PT 
                        INNER JOIN partCategoryCode_tb ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId 
                        RIGHT OUTER JOIN Invoice_tb AS IV INNER JOIN invoiceSection_tb ON IV.invoiceId = invoiceSection_tb.invoiceId 
                        INNER JOIN invoiceLineDetail_tb AS ID ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId 
                        ON PT.partId = ID.itemId WHERE  ID.lineDetailType = 'P' AND(ISNULL(ID.accountingDocumentTypeId, 0) = 0 OR 
                        ID.accountingDocumentTypeId = (SELECT lookUpId FROM lookup_tb WITH (nolock) WHERE parentLookUpCode = 218 
                        AND lookupcode = 1)) AND ID.rebateType IN(0, 2) AND iv.isActive = 1 ORDER BY invoiceDate DESC";
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
                    query = @"DECLARE @@Userid AS INT; 
                        DECLARE @@FleetAccessLocations TABLE (entityDetailId INT PRIMARY KEY); 
                        SELECT @@Userid=userid FROM user_tb WHERE username=@UserName; 
                        WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId 
                        IN (SELECT DISTINCT UR.entityId FROM userRelationships_tb UR WITH(NOLOCK) INNER JOIN user_tb U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId=@@UserID 
                        AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P 
                        INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations SELECT entityDetailId 
                        FROM RootNumbeR WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId 
                        FROM userRelationships_tb UR2 WITH(NOLOCK) WHERE UR2.userId=@@UserID AND IsActive=1 AND UR2.entityId IS NOT NULL;SELECT TOP 1 Convert(Date, invoicedate) AS FromDate,
                        Convert(Date, invoicedate) AS ToDate FROM part_tb AS PT INNER JOIN partCategoryCode_tb PTC ON PT.categoryCode1Id = PTC.partCategoryCodeId RIGHT OUTER JOIN 
                        Invoice_tb AS IV INNER JOIN invoiceSection_tb ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN invoiceLineDetail_tb AS ID ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId 
                        ON PT.partId = ID.itemId WHERE ID.lineDetailType = 'P' AND(ISNULL(ID.accountingDocumentTypeId, 0) = 0 OR ID.accountingDocumentTypeId = 
                        (SELECT lookUpId FROM lookup_tb WITH(NOLOCK) WHERE parentLookUpCode = 218 AND lookupcode = 1)) AND ID.rebateType IN (0,2) AND iv.isActive = 1 AND 
                        iv.receiverEntityDetailId IN (SELECT entityDetailId FROM @@FleetAccessLocations) ORDER BY invoiceDate DESC; ";
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
                LoggingHelper.LogException(ex);
                FromDate = null;
                ToDate = null;
            }
        }
    }
}
