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

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PODiscrepancyHistory
{
    internal class PODiscrepancyHistoryDAL : BaseDataAccessLayer
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
                    query = @"SELECT top 1 Convert (Date , t.createdate) as FromDate ,Convert (Date , t.createdate) as ToDate FROM [transaction_tb] t (NOLOCK) JOIN [transactionPO_tb] PO (NOLOCK) ON t.transactionId=PO.transactionId LEFT JOIN [entityDetails_tb] er (NOLOCK) on t.receiverEntityDetailId = er.entityDetailId and er.isActive=1 LEFT JOIN [entityDetails_tb] es (NOLOCK) on t.senderEntityDetailId = es.entityDetailId and es.isActive=1 WHERE t.isActive = 1 AND t.requestTypeCode = 'O' AND t.validationStatus in (2, 3, 5, 6, 7) AND t.isHistorical = 1;";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }

                else if (userType == "FLEET")
                {
                    query = @"DECLARE @@Userid as int DECLARE @@FleetAccessLocations table( entityDetailId INT primary key ) select @@Userid = userid from [user_tb] where username = @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN(SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK) INNER JOIN [user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID and IsActive = 1 AND userRelationships_tb.entityId IS NOT NULL; Select top 1 Convert(Date, t.createdate) as FromDate ,Convert(Date, t.createdate) as ToDate FROM [transaction_tb] t(NOLOCK) JOIN [transactionPO_tb] PO(NOLOCK) ON t.transactionId = PO.transactionId LEFT JOIN [entityDetails_tb] er(NOLOCK) on t.receiverEntityDetailId = er.entityDetailId and er.isActive = 1 LEFT JOIN [entityDetails_tb] es(NOLOCK) on t.senderEntityDetailId = es.entityDetailId and es.isActive = 1 inner join @@FleetAccessLocations d on d.entitydetailid = t.receiverEntityDetailId WHERE t.isActive = 1 AND t.requestTypeCode = 'O' AND t.validationStatus in (2,3,5,6,7) AND t.isHistorical = 1;";
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
        internal int GetCountByDateRange(string dateRange, int days = 0)
        {

            string query = null;
            string fromDateTime = null;

            if (dateRange == "Last 7 days")
            {
                fromDateTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                query = @"Select count(*) FROM transaction_tb t (NOLOCK)
               JOIN transactionPO_tb PO (NOLOCK) ON t.transactionId=PO.transactionId
               LEFT JOIN entityDetails_tb er (NOLOCK) on t.receiverEntityDetailId = er.entityDetailId and er.isActive=1
               LEFT JOIN entityDetails_tb es (NOLOCK) on t.senderEntityDetailId = es.entityDetailId and es.isActive=1
               WHERE t.isActive=1 AND t.requestTypeCode='O' AND t.validationStatus in (2,3,5,6,7) AND t.isHistorical=1 
		        AND CAST (t.createdate as DATE) BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }


            }

            else if (dateRange == "Last 14 days")
            {
                fromDateTime = DateTime.Now.AddDays(-13).ToString("yyyy-MM-dd");
                query = @"Select count(*) FROM transaction_tb t (NOLOCK)
               JOIN transactionPO_tb PO (NOLOCK) ON t.transactionId=PO.transactionId
               LEFT JOIN entityDetails_tb er (NOLOCK) on t.receiverEntityDetailId = er.entityDetailId and er.isActive=1
               LEFT JOIN entityDetails_tb es (NOLOCK) on t.senderEntityDetailId = es.entityDetailId and es.isActive=1
               WHERE t.isActive=1 AND t.requestTypeCode='O' AND t.validationStatus in (2,3,5,6,7) AND t.isHistorical=1 
		        AND CAST (t.createdate as DATE) BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            else if (dateRange == "Last 185 days")
            {
                fromDateTime = DateTime.Now.AddDays(-184).ToString("yyyy-MM-dd");
                query = @"Select count(*) FROM transaction_tb t (NOLOCK)
               JOIN transactionPO_tb PO (NOLOCK) ON t.transactionId=PO.transactionId
               LEFT JOIN entityDetails_tb er (NOLOCK) on t.receiverEntityDetailId = er.entityDetailId and er.isActive=1
               LEFT JOIN entityDetails_tb es (NOLOCK) on t.senderEntityDetailId = es.entityDetailId and es.isActive=1
               WHERE t.isActive=1 AND t.requestTypeCode='O' AND t.validationStatus in (2,3,5,6,7) AND t.isHistorical=1 
		        AND CAST (t.createdate as DATE) BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            else if (dateRange == "Current month")
            {
                var today = DateTime.Now;
                fromDateTime = new DateTime(today.Year, today.Month, 1).ToString("yyyy-MM-dd");

                query = @"Select count(*) FROM transaction_tb t (NOLOCK)
               JOIN transactionPO_tb PO (NOLOCK) ON t.transactionId=PO.transactionId
               LEFT JOIN entityDetails_tb er (NOLOCK) on t.receiverEntityDetailId = er.entityDetailId and er.isActive=1
               LEFT JOIN entityDetails_tb es (NOLOCK) on t.senderEntityDetailId = es.entityDetailId and es.isActive=1
               WHERE t.isActive=1 AND t.requestTypeCode='O' AND t.validationStatus in (2,3,5,6,7) AND t.isHistorical=1 
		        AND CAST (t.createdate as DATE) BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }

            else if (dateRange == "Last month")
            {
                var lastmonth = DateTime.Now.AddMonths(-1);
                fromDateTime = new DateTime(lastmonth.Year, lastmonth.Month, 1).ToString("yyyy-MM-dd");
                var toDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1).ToString("yyyy-MM-dd");

                query = @"Select count(*) FROM transaction_tb t (NOLOCK)
               JOIN transactionPO_tb PO (NOLOCK) ON t.transactionId=PO.transactionId
               LEFT JOIN entityDetails_tb er (NOLOCK) on t.receiverEntityDetailId = er.entityDetailId and er.isActive=1
               LEFT JOIN entityDetails_tb es (NOLOCK) on t.senderEntityDetailId = es.entityDetailId and es.isActive=1
               WHERE t.isActive=1 AND t.requestTypeCode='O' AND t.validationStatus in (2,3,5,6,7) AND t.isHistorical=1 
		        AND CAST (t.createdate as DATE) BETWEEN @fromDateTime AND @toDateTime";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                    new SqlParameter("@toDateTime", toDateTime),

                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }

            else if (dateRange == "Customized date")
            {
                fromDateTime = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
                query = @"Select count(*) FROM transaction_tb t (NOLOCK)
               JOIN transactionPO_tb PO (NOLOCK) ON t.transactionId=PO.transactionId
               LEFT JOIN entityDetails_tb er (NOLOCK) on t.receiverEntityDetailId = er.entityDetailId and er.isActive=1
               LEFT JOIN entityDetails_tb es (NOLOCK) on t.senderEntityDetailId = es.entityDetailId and es.isActive=1
               WHERE t.isActive=1 AND t.requestTypeCode='O' AND t.validationStatus in (2,3,5,6,7) AND t.isHistorical=1 
		       AND CAST (t.createdate as DATE) BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };

                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }

            return -1;
        }

    }
}
