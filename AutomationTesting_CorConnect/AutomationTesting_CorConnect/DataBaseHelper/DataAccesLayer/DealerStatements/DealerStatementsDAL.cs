using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerStatements
{
    internal class DealerStatementsDAL : BaseDataAccessLayer
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
                    query = @"SELECT TOP 1 CONVERT (Date , statementEndDate) AS FromDate, Convert (Date, statementEndDate) AS ToDate FROM [statement_tb] ST WITH (NOLOCK) INNER JOIN [entityDetails_tb] E WITH (NOLOCK) on ST.entityDetailId = E.entityDetailId AND E.entityTypeId =2 WHERE ST.statementStatus = 1 And  ST.statementType = 'AP' AND ST.isActive = 1 ORDER BY statementEndDate DESC;";

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
                    query = @"DECLARE @@Userid as int DECLARE @@DealerAccessLocations table(entityDetailId    INT  primary key)  DECLARE @@MasterInvoiceTypeId INT SELECT @@MasterInvoiceTypeId = lookupid FROM [lookup_tb] WHERE parentlookupcode=119 and lookupcode=3; SELECT @@Userid=userid FROM [user_tb] WHERE username= 'ArgentinaQADealer';  WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE  entityDetailId iN (SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK)  INNER JOIN [user_tb]  WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId)insert into @@DealerAccessLocations SELECT   entityDetailId  FROM  RootNumber  where   parentEntityDetailId <> entityDetailId   and parentEntityDetailId <>0  UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb]  WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; SELECT  top 1 Convert(Date, ST.statementEndDate) as FromDate, Convert(Date, ST.statementEndDate) as ToDate FROM [statement_tb] ST WITH (NOLOCK) INNER JOIN [entityDetails_tb] E WITH (NOLOCK) on ST.entityDetailId = E.entityDetailId AND E.entityTypeId =2 INNER JOIN [statementDetail_tb] SD WITH ( NOLOCK ) ON ST.statementId = SD.statementId INNER JOIN [invoice_tb] I WITH ( NOLOCK ) ON I.invoiceId = SD.invoiceId INNER JOIN  @@DealerAccessLocations AE ON AE.entityDetailId = I.senderEntityDetailId WHERE ST.statementStatus = 1 AND ST.statementTypeId != @@MasterInvoiceTypeId AND  ST.statementType = 'AP' AND ST.isActive = 1 ORDER BY ST.statementEndDate DESC;";
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
