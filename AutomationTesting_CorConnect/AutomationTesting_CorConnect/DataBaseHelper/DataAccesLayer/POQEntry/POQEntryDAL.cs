using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.POQEntry
{
    internal class POQEntryDAL : BaseDataAccessLayer
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
                    query = @"SELECT    top 1 Convert (Date , transactiondate) as FromDate ,Convert (Date , transactiondate) as ToDate FROM [transaction_tb] tr with (NOLOCK) 
                        where  isActive = 1 and validationStatus = 8 and requestTypeCode = 'Q' ORDER BY  transactiondate DESC";
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
                    query = @"DECLARE @@Userid as int DECLARE @@DealerAccessLocations table ( entityDetailId INT  primary key ) SELECT @@Userid = userid FROM [user_tb] where username= 
                        @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb]  WITH(NOLOCK) WHERE 
                        entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK)  INNER JOIN [user_tb] WITH(NOLOCK) ON 
                        userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1  AND userRelationships_tb.hasHierarchyAccess = 1) 
                        UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = 
                        C.parentEntityDetailId ) insert into @@DealerAccessLocations SELECT entityDetailId  FROM RootNumber  where parentEntityDetailId <> entityDetailId   and 
                        parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId  FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = 
                        @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; SELECT top 1 Convert (Date , transactiondate) as FromDate ,Convert (Date , transactiondate) 
                        as ToDate FROM [transaction_tb] tr with (NOLOCK) INNER JOIN  @@DealerAccessLocations AE ON AE.entityDetailId = tr.senderEntityDetailId where  isActive = 1 and 
                        validationStatus = 8 and requestTypeCode = 'Q' ORDER BY transactiondate DESC ";
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

        internal int GetPOQSectionCount(string transactionNumber)
        {
            var query = "Select Count(*) from transaction_tb IV INNER JOIN transactionSection_tb IVS ON IV.transactionId=IVS.transactionId where requestTypeCode  IN ('Q') and validationStatus=1 AND IV.transactionNumber=@transactionNumber";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@transactionNumber", transactionNumber),

            };

            try
            {
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return -1;
        }
    }
}
