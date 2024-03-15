using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.TaxReviewReport
{
    internal class TaxReviewReportDAL : BaseDataAccessLayer
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
                    query = @"SELECT TOP 1 CONVERT (Date , invoicedate) AS FromDate, Convert (Date, invoicedate) AS ToDate FROM invoice_tb I 
                        INNER JOIN transaction_tb T ON (I.transactionId = T.transactionId) WHERE I.isActive = 1 
                        AND T.calculatedTotal <> I.transactionAmount ORDER BY invoicedate DESC";
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
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM user_tb where 
                        username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) 
                        WHERE entityDetailId IN (SELECT DISTINCT UR.entityId FROM userRelationships_tb UR WITH(NOLOCK) INNER JOIN user_tb U WITH(NOLOCK) ON UR.userId=U.userId 
                        WHERE U.userId=@@UserID AND UR.IsActive=1 AND UR.hasHierarchyAccess=1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P 
                        INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId=C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId 
                        FROM RootNumber WHERE parentEntityDetailId<>entityDetailId AND parentEntityDetailId<>0 UNION SELECT UR2.entityId AS entityDetailId FROM userRelationships_tb 
                        UR2 WITH(NOLOCK) WHERE UR2.userId=@@UserID AND IsActive=1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 Convert (Date,invoicedate) AS FromDate, Convert (Date,invoicedate) 
                        AS ToDate FROM invoice_tb I INNER JOIN transaction_tb T on I.transactionId=T.transactionId INNER JOIN @@DealerAccessLocations AE ON AE.entityDetailId=I.senderEntityDetailId 
                        WHERE I.isActive=1 AND  T.calculatedTotal<>I.transactionAmount;";
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
