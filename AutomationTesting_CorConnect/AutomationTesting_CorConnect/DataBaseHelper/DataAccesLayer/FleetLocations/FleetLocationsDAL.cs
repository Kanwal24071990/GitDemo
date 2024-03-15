using AutomationTesting_CorConnect.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetLocations
{
    internal class FleetLocationsDAL : BaseDataAccessLayer
    {
        internal void GetData(out string fleetCode)
        {
            fleetCode = string.Empty;

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
                    query = @"SELECT TOP 1 E.corcentriccode FROM [entityDetails_tb] E WITH (NOLOCK) INNER JOIN [entityAddressRel_tb] EA WITH (NOLOCK) ON (E.entityDetailId = EA.entityDetailId) INNER JOIN [address_tb] A WITH (NOLOCK) ON (EA.addressId = A.addressId ) WHERE E.entityTypeId = 3 AND E.enrollmentStatusId = 13 AND E.isActive = 1 ORDER BY E.entitydetailid DESC;";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            fleetCode = reader.GetString(0);

                        }
                    }
                }
                else if (userType == "FLEET")
                {
                    query = @"DECLARE @@Userid as int DECLARE @@FleetAccessLocations table(entityDetailId INT  primary key) select @@Userid = userid from [user_tb] where
                            username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb]  WITH(NOLOCK) WHERE  entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK)  INNER JOIN [user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; Select top  1  e.corcentriccode FROM [entityDetails_tb] E WITH (NOLOCK)inner join @@FleetAccessLocations AE on AE.Entitydetailid = E.entityDetailId INNER JOIN [entityAddressRel_tb] EA WITH (NOLOCK) ON E.entityDetailId = EA.entityDetailId INNER JOIN [address_tb] A WITH (NOLOCK) ON EA.addressId = A.addressId WHERE E.entityTypeId =3 And E.enrollmentStatusId = 13 AND E.isActive=1 order by e.entitydetailid desc;";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            fleetCode = reader.GetString(0);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                fleetCode = null;

            }
        }
        
        internal string GetInActiveCorCentricCodeforFleet()
        {
            var query = @"select Top 1 e.corcentriccode from user_tb u inner join userRelationships_tb ur on u.userid = ur.userid inner join entitydetails_tb e on e.entitydetailid = ur.entityid where u.isactive = 0 and e.isactive = 1 and e.entityTypeId = 3";

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

        internal List<string> GetInActiveUsersListforFleetEntity()
        {
            List<string> userList = new List<string>();
            string query = @"select u.userName from user_tb u inner join userRelationships_tb ur on u.userid = ur.userid inner join entitydetails_tb e on e.entitydetailid = ur.entityid where u.isactive = 0 and e.isactive = 1 and e.entityTypeId = 3";

            using (var reader = ExecuteReader(query, false))
            {
                while (reader.Read())
                {
                    userList.Add(reader.GetString(0));
                }
            }

            return userList;
        }

        internal List<string> GetActiveUsersListforFleetEntity(int entityID, string subCommunity)
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
    }
}
