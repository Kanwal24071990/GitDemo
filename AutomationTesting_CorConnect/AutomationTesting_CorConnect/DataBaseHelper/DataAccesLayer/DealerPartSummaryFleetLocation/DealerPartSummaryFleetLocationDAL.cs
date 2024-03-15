using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPartSummaryFleetLocation
{
    internal class DealerPartSummaryFleetLocationDAL : BaseDataAccessLayer
    {
        internal void GetDateData(out string FromDate, out string ToDate)
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
                        INNER JOIN partCategoryCode_tb ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId RIGHT OUTER JOIN Invoice_tb IV 
                        INNER JOIN invoiceSection_tb ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN invoiceLineDetail_tb ID ON invoiceSection_tb.invoiceSectionId 
                        = ID.invoiceSectionId ON PT.partId = ID.itemId WHERE  ID.lineDetailType = 'P'  AND(ISNULL(ID.accountingDocumentTypeId, 0) = 0 OR ID.accountingDocumentTypeId = 
                        (SELECT lookUpId FROM lookup_tb WITH (NOLOCK) WHERE parentLookUpCode = 218 AND lookupcode = 1)) AND ID.rebateType IN (0,1) ORDER BY invoicedate DESC";
                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
                else if (userType == "DEALER")
                {
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM user_tb 
                        where username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) 
                        WHERE entityDetailId IN (SELECT DISTINCT UR.entityId FROM userRelationships_tb UR WITH(NOLOCK) INNER JOIN user_tb U WITH(NOLOCK) ON UR.userId = U.userId 
                        WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM 
                        RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations 
                        SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId 
                        FROM userRelationships_tb UR2 WITH(NOLOCK)WHERE UR2.userId = @@UserID AND IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, invoicedate) AS 
                        FromDate, CONVERT (DATE, invoicedate) AS ToDate FROM part_tb PT INNER JOIN partCategoryCode_tb PCC ON PT.categoryCode1Id = PCC.partCategoryCodeId RIGHT OUTER JOIN 
                        Invoice_tb IV INNER JOIN invoiceSection_tb IST ON IV.invoiceId = IST.invoiceId INNER JOIN invoiceLineDetail_tb ID ON IST.invoiceSectionId = ID.invoiceSectionId 
                        ON PT.partId = ID.itemId INNER JOIN @@DealerAccessLocations AE ON AE.entityDetailId = iv.senderEntityDetailId WHERE ID.lineDetailType = 'P' 
                        AND(ISNULL(ID.accountingDocumentTypeId, 0) = 0 OR ID.accountingDocumentTypeId = (SELECT lookUpId FROM lookup_tb WITH(NOLOCK) WHERE parentLookUpCode = 218 AND 
                        lookupcode = 1)) AND ID.rebateType IN (0, 1) ORDER BY invoiceDate DESC;";
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
