using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.Price
{
    internal class PriceDAL : BaseDataAccessLayer
    {
        internal string GetPartNumber()
        {
            string partNumber = string.Empty;
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
                    query = @"SELECT  TOP 1 PTB.[PartNumber] FROM [priceList_tb] PL (NOLOCK) INNER JOIN [price_tb] P (NOLOCK) ON (PL.priceListId = P.priceListId) 
                        INNER JOIN [part_tb] PTB (NOLOCK) ON (P.partId = PTB.partId AND PTB.isActive = 1) ORDER BY priceid DESC;";
                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            partNumber = reader.GetStringValue(0);
                        }
                    }
                }
                else if (userType == "DEALER")
                {
                    query = @"declare @Userid as int declare @DealerAccessLocations table ( entityDetailId INT primary key ) select @Userid=userid from [user_tb] where 
                        username=@UserName; WITH RootNumber AS ( SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE 
                        entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK) INNER JOIN [user_tb] WITH(NOLOCK) ON 
                        userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) 
                        UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = 
                        C.parentEntityDetailId ) insert into @DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId and 
                        parentEntityDetailId <>0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = 
                        @UserID and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; Select top 1 p.PartNumber FROM [priceList_tb] (NOLOCK) INNER JOIN [price_tb] (NOLOCK) ON 
                        priceList_tb.priceListId = price_tb.priceListId INNER JOIN [part_tb] p (NOLOCK) ON price_tb.partId = p.partId AND p.isActive= 1 inner join @DealerAccessLocations d 
                        on d.entityDetailId = p.supplierEntityId union Select top 1 p.PartNumber FROM [priceList_tb] (NOLOCK) INNER JOIN [price_tb] (NOLOCK) ON priceList_tb.priceListId = 
                        price_tb.priceListId INNER JOIN [part_tb] p (NOLOCK) ON price_tb.partId = p.partId AND p.isActive= 1 where (p.supplierEntityId = 0 or p.supplierEntityId is null)";
                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            partNumber = reader.GetStringValue(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
            }
            return partNumber;
        }

        internal bool DeletePartAndPrice(string partNumber)
        {
            string query = @"select count(*) from Part_tb where partnumber=@partNumber";
            int partPriceCount = -1;

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@partNumber", partNumber),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    partPriceCount = reader.GetInt32(0);
                }
            }

            if (partPriceCount == 1)
            {
                query = @"declare @@partnumber as varchar(100)
                    set @@partnumber=@partNumber
                    delete  from pricedetail_tb  from pricedetail_tb d inner join price_tb r on d.priceid =r.priceid inner join part_tb p on r.partid=p.partid where p.partnumber=@partnumber;
                    delete from price_tb from price_tb r inner join part_tb p on r.partid=p.partid where p.partnumber=@@partnumber;
                    delete from Part_tb where partnumber=@@partnumber;";

                sp = new()
                {
                    new SqlParameter("@partNumber", partNumber),
                };

                if (ExecuteNonQuery(query, sp, false) < 1)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
