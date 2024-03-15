using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceDiscrepancyHistory
{
    internal class InvoiceDiscrepancyHistoryDAL : BaseDataAccessLayer
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
                    query = @"SELECT TOP 1 CONVERT (Date , createDate) AS FromDate, Convert (Date, createDate) AS ToDate FROM [transaction_tb] WITH (NOLOCK) WHERE [isActive] = 1 AND [validationStatus] IN (2,3,4,9,13,14,16) AND[requestTypeCode] = 'S' AND[isHistorical] = 1 ORDER BY createdate DESC;";

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
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE username='ArgentinaQADealer'; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM [userRelationships_tb] UR2 WITH(NOLOCK)WHERE UR2.userId = @@UserID AND IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, createDate) AS FromDate, CONVERT(DATE, createDate) AS ToDate FROM [transaction_tb] WITH(NOLOCK) INNER JOIN @@DealerAccessLocations AE ON AE.entityDetailId = senderEntityDetailId WHERE isActive = 1 AND validationStatus IN(2, 3, 4, 9, 13, 14) AND requestTypeCode = 'S' AND isHistorical = 1 ORDER BY createdate DESC;";
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
                else if (userType == "FLEET")
                {
                    query = @"DECLARE @@Userid AS INT; DECLARE @@FleetAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE username='ArgentinaQAFleet'; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId = @@UserID  AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM [userRelationships_tb] UR2 WITH(NOLOCK) WHERE UR2.userId = @@UserID AND IsActive =1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, IV.createDate) AS FromDate, CONVERT(DATE, IV.createDate) AS ToDate FROM [transaction_tb] IV WITH(NOLOCK) INNER JOIN @@FleetAccessLocations AE ON AE.entityDetailId = IV.receiverEntityDetailId WHERE IV.isActive = 1 AND IV.validationStatus IN(2, 3, 4, 9, 13, 14) AND IV.requestTypeCode = 'S' AND IV.isHistorical = 1 ORDER BY IV.createdate DESC;";
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

        internal void GetDateFromLocation(string location, out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string userType;
                string userName;

                if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
                {
                    userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                    userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();

                }
                else
                {
                    userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                    userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
                }
                if (userType == "ADMIN")
                {
                    query = @"SELECT TOP 1 CONVERT (Date , createDate) AS FromDate, Convert (Date, createDate) AS ToDate FROM [transaction_tb] WITH (NOLOCK) WHERE [isActive] = 1 AND [validationStatus] IN (2,3,4,9,13,14,16) AND[requestTypeCode] = 'S' AND[isHistorical] = 1 ORDER BY createdate DESC;";

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
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE username=@UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM [userRelationships_tb] UR2 WITH(NOLOCK)WHERE UR2.userId = @@UserID AND IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, createDate) AS FromDate, CONVERT(DATE, createDate) AS ToDate FROM [transaction_tb] WITH(NOLOCK) INNER JOIN @@DealerAccessLocations AE ON AE.entityDetailId = senderEntityDetailId WHERE isActive = 1 AND validationStatus IN(2, 3, 4, 9, 13, 14) AND requestTypeCode = 'S' AND isHistorical = 1 and senderCorcentricCode = @location ORDER BY createdate DESC;";
                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", userName),
                        new SqlParameter("@location", location),
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
                else if (userType == "FLEET")
                {
                    query = @"DECLARE @@Userid AS INT; DECLARE @@FleetAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE username=@UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId = @@UserID  AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM [userRelationships_tb] UR2 WITH(NOLOCK) WHERE UR2.userId = @@UserID AND IsActive =1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, IV.createDate) AS FromDate, CONVERT(DATE, IV.createDate) AS ToDate FROM [transaction_tb] IV WITH(NOLOCK) INNER JOIN @@FleetAccessLocations AE ON AE.entityDetailId = IV.receiverEntityDetailId WHERE IV.isActive = 1 AND IV.validationStatus IN(2, 3, 4, 9, 13, 14) AND IV.requestTypeCode = 'S' AND IV.isHistorical = 1 and receiverCorcentricCode = @location ORDER BY IV.createdate DESC;";
                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", userName),
                        new SqlParameter("@location", location),
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

        internal bool ValidateInvoiceMovedFromHistory(string dealerInv)
        {
            string query = "select hasMovedFromHistory from transaction_tb where transactionnumber = @dealerInv";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerInv", dealerInv)
            };

            using (var reader = ExecuteReader(query,sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetBoolean(0);
                }
            }

            return false;
        }

        internal bool ValidateInvoiceInHistory(string dealerInv)
        {
            string query = "select isHistorical from transaction_tb where transactionnumber = @dealerInv";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerInv", dealerInv)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetBoolean(0);
                }
            }

            return false;
        }

        internal void UpdateInvoiceToExpire(string dealerInv)
        {

            string query = "update transaction_tb set expirationdate = GETDATE()-1 where transactionnumber = @dealerInv";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerInv", dealerInv)
            };
            ExecuteNonQuery(query,sp,false);
        }

        internal string GetInvoiceNotInBalance(string dealerName)
        {
            string query = "select top 1 transactionNumber from transaction_tb t INNER JOIN transactionError_tb te on t.transactionId = te.transactionId where expirationDate > GETDATE() and validationStatus = 3 and senderCorcentricCode = @dealerName and te.description = 'Not in Balance' and createDate between GETDATE()-59 and GETDATE()";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerName", dealerName)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return null;
        }        
    }
}
