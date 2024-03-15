using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.BatchRequestStatus
{
    internal class BatchRequestStatusDAL : BaseDataAccessLayer
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
                    query = @"SELECT TOP 1 CONVERT (Date , RequestedDate) AS FromDate, Convert (Date, RequestedDate) AS ToDate FROM [batchReports_tb] BR WITH (NOLOCK) 
                        INNER JOIN [lookUp_tb] L WITH (NOLOCK) ON (BR.[Status] = L.[lookUpCode]) WHERE L.[parentLookUpCode] = 203 AND userId = 
                        (select userid from [user_tb] where username= @UserName) order by RequestedDate DESC";
                }
                else if (userType == "FLEET")
                {
                    query = @"declare @@Userid as int select @@Userid=userid FROM [user_tb] where username= @UserName; Select top 10000 Convert 
                        (Date , RequestedDate) as FromDate ,Convert (Date , RequestedDate) as ToDate FROM [batchReports_tb] INNER JOIN [lookUp_tb] ON batchReports_tb.Status = 
                        lookUp_tb.lookUpCode WHERE lookUp_tb.parentLookUpCode = 203 AND userId = @@Userid AND FunctionName LIKE '% Print' order by RequestedDate DESC ";
                }
                else if (userType == "DEALER")
                {
                    query = @"declare @@Userid as int select @@Userid=userid from [user_tb] where username= @UserName; 
                        Select top 10000 Convert(Date, RequestedDate) as FromDate, Convert(Date, RequestedDate) as ToDate FROM [batchReports_tb] INNER JOIN [lookUp_tb] 
                        ON batchReports_tb.Status = lookUp_tb.lookUpCode WHERE lookUp_tb.parentLookUpCode = 203 AND userId = @@Userid AND FunctionName LIKE '% Print' ORDER BY RequestedDate DESC ";
                }

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
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                FromDate = null;
                ToDate = null;
            }
        }
    }
}
