using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoices
{
    internal class DealerInvoicesDAL : BaseDataAccessLayer
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
                    query = @"SELECT TOP 1 CONVERT (Date , receivedate) AS FromDate, Convert (Date, receivedate) AS ToDate FROM [Invoice_tb] ORDER BY receivedate DESC";

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
                    query = @"DECLARE @@Userid as int DECLARE @@DealerAccessLocations table (entityDetailId INT primary key) SELECT @@Userid = userid from [user_tb] where username = 'ArgentinaQADealer'; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON userRelationships_tb.userId = U.userId WHERE U.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID and IsActive = 1 AND userRelationships_tb.entityId IS NOT NULL; select top 1 Convert(Date, receivedate) as FromDate ,Convert(Date, receivedate) as ToDate from [Invoice_tb] iv with (nolock) inner JOIN @@DealerAccessLocations ae ON ae.entityDetailId = iv.senderEntityDetailId inner join [transaction_tb] t with(nolock) on t.transactionId = IV.transactionId left join [transactionDisputes_tb] td WITH(NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 INNER join [lookup_tb] l2 on l2.parentLookUpCode = 21 AND l2.description = IV.transactionTypeCode INNER JOIN [entityDetails_tb] R WITH (NOLOCK) ON R.entityDetailId = IV.receiverEntityDetailId INNER JOIN [entityDetails_tb] S WITH (NOLOCK) ON S.entityDetailId = IV.senderEntityDetailId LEFT JOIN [user_tb] u WITH (NOLOCK) ON u.userid = t.updatedby where iv.isActive = 1 order by receivedate desc";
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

        internal void ActivateUpdateInvoiceOption(string transactionNumber)
        {
            string query = "update invoice_tb set arPostedToAccounting = 1 where transactionNumber=@transactionNumber";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@transactionNumber", transactionNumber),

            };
            ExecuteNonQuery(query,sp, false);  
        }
        internal int GetCountByDateRange(string dateRange, int days = 0)
        {

            string query = null;
            string fromDateTime = null;

            if (dateRange == "Last 7 days")
            {
                fromDateTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                query = @"SELECT COUNT (*)   
                    from Invoice_tb Where receiveDate BETWEEN @fromDateTime AND GETDATE()";             

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
                query = @"SELECT COUNT (*)   
                    from Invoice_tb Where receiveDate BETWEEN @fromDateTime AND GETDATE()";

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
                Console.WriteLine("From Date: " + fromDateTime);
                query = @"SELECT COUNT (*)   
                    from Invoice_tb Where receiveDate BETWEEN @fromDateTime AND GETDATE()";

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

                query = @"SELECT COUNT (*)   
                    from Invoice_tb Where receiveDate BETWEEN @fromDateTime AND GETDATE()";

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

                query = @"SELECT COUNT (*)   
                    from Invoice_tb Where receiveDate BETWEEN @fromDateTime AND @toDateTime";

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
                query = @"SELECT COUNT(*)   
                    from Invoice_tb Where receiveDate BETWEEN @fromDateTime AND GETDATE()";

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
