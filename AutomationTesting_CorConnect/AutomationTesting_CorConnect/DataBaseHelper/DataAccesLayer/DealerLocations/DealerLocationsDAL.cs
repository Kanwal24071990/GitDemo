using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerLocations
{
    internal class DealerLocationsDAL : BaseDataAccessLayer
    {
        internal void GetData(out string dealerCoder)
        {
            dealerCoder = string.Empty;

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
                    query = @"SELECT TOP 1 corcentriccode FROM [entitydetails_tb] AS T WITH(NOLOCK) INNER JOIN [entityaddressrel_tb] E WITH(NOLOCK) ON T.entitydetailid = E.entitydetailid INNER JOIN [address_tb] A WITH(NOLOCK) ON A.addressid = E.addressid WHERE T.entitytypeid = 2 AND T.enrollmentstatusid = 13 AND T.isActive = 1;";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            dealerCoder = reader.GetString(0);

                        }
                    }
                }
                else if (userType == "DEALER")
                {
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE
                            username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM [userRelationships_tb] UR2 WITH(NOLOCK) WHERE UR2.userId = @@UserID AND IsActive =1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 T.corcentriccode FROM [entitydetails_tb] AS T WITH(NOLOCK) INNER JOIN [entityaddressrel_tb] E WITH (NOLOCK) ON T.entitydetailid = E.entitydetailid INNER JOIN [address_tb] A WITH(NOLOCK) ON A.addressid = E.addressid INNER JOIN @@DealerAccessLocations AE ON T.entitydetailid = AE.entitydetailid WHERE T.entitytypeid = 2 AND T.enrollmentstatusid = 13 AND T.isActive = 1;";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            dealerCoder = reader.GetString(0);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                dealerCoder = null;

            }
        }

        internal List<string> GetActiveUsersListforDealerEntity(int entityID, string subCommunity)
        {
            List<string> userList = new List<string>();
            string query = @"select U.userName,* from userRelationships_tb UR INNER JOIN user_tb U ON UR.userId=U.userID left join subcommunity_tb s on ur.SubCommunityId=s.subCommunityId WHERE ( UR.entityId= '" + entityID + "' or s.subcommunityname='"+ subCommunity + "') and U.IsActive = 1 AND UR.IsActive = 1";

            using (var reader = ExecuteReader(query, false))
            {
                while (reader.Read())
                {
                    userList.Add(reader.GetString(0));
                }
            }

            return userList;
        }

        internal List<string> GetInActiveUsersListforDealerEntity()
        {
            List<string> userList = new List<string>();
            string query = @"select u.userName from user_tb u inner join userRelationships_tb ur on u.userid = ur.userid inner join entitydetails_tb e on e.entitydetailid = ur.entityid where u.isactive = 0 and e.isactive = 1 and e.entityTypeId = 2";

            using (var reader = ExecuteReader(query, false))
            {
                while (reader.Read())
                {
                    userList.Add(reader.GetString(0));
                }
            }

            return userList;
        }
        internal string GetInActiveCorCentricCodeforDealer()
        {
            var query = @"select Top 1 e.corcentriccode from user_tb u inner join userRelationships_tb ur on u.userid = ur.userid inner join entitydetails_tb e on e.entitydetailid = ur.entityid where u.isactive = 0 and e.isactive = 1 and e.entityTypeId = 2";

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

            return string.Empty;
        }

    }
}
