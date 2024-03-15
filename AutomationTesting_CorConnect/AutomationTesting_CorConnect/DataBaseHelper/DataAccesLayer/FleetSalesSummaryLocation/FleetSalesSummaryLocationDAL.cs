using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetSalesSummaryLocation
{
    internal class FleetSalesSummaryLocationDAL : BaseDataAccessLayer
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
                    query = @" SELECT  top 1 CONVERT (Date , invoicedate) as FromDate ,Convert (Date , invoicedate) as ToDate  FROM invoice_tb iv WITH (nolock) 
                        INNER JOIN invoiceReport_tb ir ON iv.invoiceId = ir.invoiceId WHERE iv.isActive = 1 ORDER BY invoicedate DESC ";
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
                    query = @"DECLARE @@Userid as int DECLARE @@DealerAccessLocations table (entityDetailId INT  primary key) SELECT @@Userid = userid FROM user_tb 
                        where username = @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb  WITH(NOLOCK) 
                        WHERE  entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM userRelationships_tb WITH(NOLOCK) INNER JOIN user_tb WITH(NOLOCK) 
                        ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID  AND userRelationships_tb.IsActive = 1  AND 
                        userRelationships_tb.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P 
                        INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@DealerAccessLocations SELECT entityDetailId 
                        FROM RootNumber where   parentEntityDetailId<> entityDetailId   and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId 
                        FROM userRelationships_tb  WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; 
                        Select top 1 Convert(Date, invoicedate) as FromDate ,Convert(Date, invoicedate) as ToDate  FROM invoice_tb iv with (nolock) INNER JOIN  @@DealerAccessLocations AE 
                        ON AE.entityDetailId = iv.senderEntityDetailId inner join invoiceReport_tb ir on iv.invoiceId = ir.invoiceId where iv.isActive = 1 Order by invoicedate desc ";
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
    }
}
