using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using NUnit.Framework;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartsCrossReference
{
    internal class PartsCrossReferenceDAL : BaseDataAccessLayer
    {
        internal string GetCompanyNameCode()
        {
            string companyNameCode = string.Empty;
            string query = string.Empty;

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
                query = "DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString'); DECLARE @@sql NVARCHAR(4000) = 'SELECT top 1 AE.corcentricCode FROM ' + @@databaseName + '.[dbo].[partxref_tb] P INNER JOIN ' + @@databaseName + '.[dbo].[entityDetails_tb] AE WITH(NOLOCK) ON AE.entityDetailId = P.supplierEntityId WHERE P.isActive = 1 AND AE.isactive=1 AND ISNULL(AE.locationTypeId,0) <> 0 ORDER By AE.entityDetailId ASC ' DECLARE  @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT;";
            }
            else
            {
                query = "DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString'); DECLARE @@sql NVARCHAR(4000) = 'declare @@Userid as int declare @@DealerAccessLocations table ( entityDetailId INT primary key ) select @@Userid=userid from '+ @@databaseName +'.[dbo].[user_tb] where username=''" + GetUserId() + "''; WITH RootNumber AS ( SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM '+ @@databaseName +'.[dbo].[entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM '+ @@databaseName +'.[dbo].[userRelationships_tb] WITH(NOLOCK) INNER JOIN '+ @@databaseName +'.[dbo].[user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN '+ @@databaseName +'.[dbo].[entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId and parentEntityDetailId <>0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM '+ @@databaseName +'.[dbo].[userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; select top 1 AE.corcentricCode from '+ @@databaseName +'.[dbo].[partxref_tb] P INNER JOIN '+ @@databaseName +'.[dbo].[entityDetails_tb] AE WITH(NOLOCK) ON AE.entityDetailId = P.supplierEntityId inner join @@DealerAccessLocations d on d.entityDetailId = p.supplierEntityId where P.isActive = 1 and AE.isactive=1 and ISNULL(AE.locationTypeId,0) <> 0 ORDER By AE.entityDetailId asc '; DECLARE @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT;";
            }

            using (var reader = ExecuteReader(query, true))
            {
                if (reader.Read())
                {
                    companyNameCode = reader.GetString(0);
                }
            }

            return companyNameCode;
        }
    }
}
