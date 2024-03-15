using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.BookmarksMaintenance
{
    internal class BookmarksMaintenanceDAL : BaseDataAccessLayer
    {
        internal string GetData()
        {
            string query = @"SELECT top 1 b.name AS [Name] FROM BOOKMARK b
                            INNER JOIN SP_LOOKUP l ON l.Sp_ID = b.sp_Id
                            INNER JOIN SP_LOOKUP_NAMES ln ON ln.SpName_ID = l.SpName_ID
                            WHERE b.u_Id= (select SourceUID from WEBCORE_UID where userid =@userName )";

            string userName;

                if (TestContext.CurrentContext.Test.Properties["UserName"].Count() > 0)
                {
                    userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
                }
                else
                {
                string userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                var client = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
                var user = client.Users.First(x => x.Type == userType.ToLower());
                userName = user.User;
                }


            List<SqlParameter> sp = new()
            {
                new SqlParameter("@userName", userName),
            };

            try
            {
                using (var reader = ExecuteReader(query, sp, true))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;
            }

            return string.Empty;
        }
    }
}
