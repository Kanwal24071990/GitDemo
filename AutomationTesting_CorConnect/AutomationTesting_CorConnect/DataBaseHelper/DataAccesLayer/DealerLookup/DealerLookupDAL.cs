using AutomationTesting_CorConnect.DriverBuilder;
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

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerLookup
{
    internal class DealerLookupDAL:BaseDataAccessLayer
    {
        internal string GetCorCentricCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                        INNER JOIN entityaddressrel_tb entAdd WITH(NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                        INNER JOIN address_tb adr WITH(NOLOCK) ON adr.addressid = entAdd.addressid
                        where t.entitytypeid = 2 and t.enrollmentstatusid = 13 AND t.isActive = 1 and t.isterminated = 0";

            try
            {
                using (var reader = ExecuteReader(query, false))
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

            return null;
        }
        
        
        internal string GetDealerCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                INNER JOIN entityaddressrel_tb entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                INNER JOIN address_tb adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid
                where t.entitytypeid = 2 and t.enrollmentstatusid = 13  AND t.isActive=1 
                order by t.corcentricCode desc";
            try
            {
                using (var reader = ExecuteReader(query, false))
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
            return null;
        }

        internal void GetData(out string dealerCode)
        {
            dealerCode = string.Empty;
            
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
                    query = @"SELECT top 1 t.displayName FROM [entitydetails_tb] AS t WITH (NOLOCK)INNER JOIN [entityaddressrel_tb] entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid INNER JOIN [address_tb] adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid WHERE t.entitytypeid = 2 and t.enrollmentstatusid = 13  AND t.isActive = 1 ORDER BY t.corcentricCode DESC;";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            dealerCode = reader.GetStringValue(0).Trim();
                            
                        }
                    }
                }
                else if (userType == "FLEET")
                {
                    query = @"SELECT top 1 t.corcentricCode FROM [entitydetails_tb] AS t WITH(NOLOCK) INNER JOIN [entityaddressrel_tb] entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid INNER JOIN [address_tb] adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid where t.entitytypeid = 2 and t.enrollmentstatusid = 13  AND t.isActive=1 ORDER BY t.corcentricCode DESC;";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            dealerCode = reader.GetStringValue(0).Trim();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                dealerCode = null;
                
            }
        }
    }
}
