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

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.IntercommunityInvoiceReport
{
    internal class IntercommunityInvoiceReportDAL : BaseDataAccessLayer
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
                query = @"SELECT TOP 1 CONVERT (Date , invoicedate) AS FromDate, Convert (Date, invoicedate) AS ToDate FROM [invoice_tb] IV WITH (NOLOCK) LEFT JOIN [transactionDisputes_tb] TD WITH (NOLOCK) ON (IV.transactionId = TD.transactionId AND TD.isActive = 1) INNER JOIN [lookup_tb] L ON (ISNULL(IV.systemType, 0) = L.lookupcode AND L.parentlookupcode = 115) INNER JOIN [lookup_tb] L2 ON (L2.parentLookUpCode=21 AND L2.description=IV.transactionTypeCode) INNER JOIN [entityDetails_tb] R WITH (NOLOCK) ON (R.entityDetailId = IV.receiverEntityDetailId) INNER JOIN [entityDetails_tb] S WITH (NOLOCK) ON (S.entityDetailId = IV.senderEntityDetailId) LEFT JOIN [transactionMap_tb] TM WITH (NOLOCK) ON (IV.transactionId = TM.srcTransactionId) LEFT JOIN [lookUp_tb] L3 WITH (NOLOCK) ON (TM.validationStatusId = L3.lookUpId) WHERE ISNULL(TM.id, 0) <> 0 AND IV.isActive = 1 ORDER BY invoicedate DESC;";

                    using (var reader = ExecuteReader(query, false))
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
                query = @"Select count(*) FROM invoice_tb iv with (nolock) left join transactionDisputes_tb td  WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 INNER join lookup_tb L on isNull(IV.systemType, 0) = L.lookupcode and L.parentlookupcode = 115 INNER join lookup_tb l2 on l2.parentLookUpCode=21 AND l2.description=iv.transactionTypeCode    
                INNER JOIN entityDetails_tb R WITH (NOLOCK) ON R.entityDetailId = IV.receiverEntityDetailId
                INNER JOIN  entityDetails_tb S WITH (NOLOCK) ON S.entityDetailId = IV.senderEntityDetailId                                      
                LEFT JOIN transactionMap_tb tranMap WITH (NOLOCK) on IV.transactionId = tranMap.srcTransactionId
                Left Join lookUp_tb l3 WITH (NOLOCK) on tranMap.validationStatusId = l3.lookUpId
                Where isnull(tranMap.id, 0) <> 0 and iv.isActive = 1 and iv.invoicedate between @fromDateTime AND GETDATE()";

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
                query = @"Select count(*) FROM invoice_tb iv with (nolock) left join transactionDisputes_tb td  WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 INNER join lookup_tb L on isNull(IV.systemType, 0) = L.lookupcode and L.parentlookupcode = 115 INNER join lookup_tb l2 on l2.parentLookUpCode=21 AND l2.description=iv.transactionTypeCode    
                INNER JOIN entityDetails_tb R WITH (NOLOCK) ON R.entityDetailId = IV.receiverEntityDetailId
                INNER JOIN  entityDetails_tb S WITH (NOLOCK) ON S.entityDetailId = IV.senderEntityDetailId                                      
                LEFT JOIN transactionMap_tb tranMap WITH (NOLOCK) on IV.transactionId = tranMap.srcTransactionId
                Left Join lookUp_tb l3 WITH (NOLOCK) on tranMap.validationStatusId = l3.lookUpId
                Where isnull(tranMap.id, 0) <> 0 and iv.isActive = 1 and iv.invoicedate between @fromDateTime AND GETDATE()";


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
                query = @"Select count(*) FROM invoice_tb iv with (nolock) left join transactionDisputes_tb td  WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 INNER join lookup_tb L on isNull(IV.systemType, 0) = L.lookupcode and L.parentlookupcode = 115 INNER join lookup_tb l2 on l2.parentLookUpCode=21 AND l2.description=iv.transactionTypeCode    
                INNER JOIN entityDetails_tb R WITH (NOLOCK) ON R.entityDetailId = IV.receiverEntityDetailId
                INNER JOIN  entityDetails_tb S WITH (NOLOCK) ON S.entityDetailId = IV.senderEntityDetailId                                      
                LEFT JOIN transactionMap_tb tranMap WITH (NOLOCK) on IV.transactionId = tranMap.srcTransactionId
                Left Join lookUp_tb l3 WITH (NOLOCK) on tranMap.validationStatusId = l3.lookUpId
                Where isnull(tranMap.id, 0) <> 0 and iv.isActive = 1 and iv.invoicedate between @fromDateTime AND GETDATE()";

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

                query = @"Select count(*) FROM invoice_tb iv with (nolock) left join transactionDisputes_tb td  WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 INNER join lookup_tb L on isNull(IV.systemType, 0) = L.lookupcode and L.parentlookupcode = 115 INNER join lookup_tb l2 on l2.parentLookUpCode=21 AND l2.description=iv.transactionTypeCode    
                INNER JOIN entityDetails_tb R WITH (NOLOCK) ON R.entityDetailId = IV.receiverEntityDetailId
                INNER JOIN  entityDetails_tb S WITH (NOLOCK) ON S.entityDetailId = IV.senderEntityDetailId                                      
                LEFT JOIN transactionMap_tb tranMap WITH (NOLOCK) on IV.transactionId = tranMap.srcTransactionId
                Left Join lookUp_tb l3 WITH (NOLOCK) on tranMap.validationStatusId = l3.lookUpId
                Where isnull(tranMap.id, 0) <> 0 and iv.isActive = 1 and iv.invoicedate between @fromDateTime AND GETDATE()";

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

                query = @"Select count(*) FROM invoice_tb iv with (nolock) left join transactionDisputes_tb td  WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 INNER join lookup_tb L on isNull(IV.systemType, 0) = L.lookupcode and L.parentlookupcode = 115 INNER join lookup_tb l2 on l2.parentLookUpCode=21 AND l2.description=iv.transactionTypeCode    
                INNER JOIN entityDetails_tb R WITH (NOLOCK) ON R.entityDetailId = IV.receiverEntityDetailId
                INNER JOIN  entityDetails_tb S WITH (NOLOCK) ON S.entityDetailId = IV.senderEntityDetailId                                      
                LEFT JOIN transactionMap_tb tranMap WITH (NOLOCK) on IV.transactionId = tranMap.srcTransactionId
                Left Join lookUp_tb l3 WITH (NOLOCK) on tranMap.validationStatusId = l3.lookUpId
                Where isnull(tranMap.id, 0) <> 0 and iv.isActive = 1 and iv.invoicedate between @fromDateTime AND @toDateTime";


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
                query = @"Select count(*) FROM invoice_tb iv with (nolock) left join transactionDisputes_tb td  WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 INNER join lookup_tb L on isNull(IV.systemType, 0) = L.lookupcode and L.parentlookupcode = 115 INNER join lookup_tb l2 on l2.parentLookUpCode=21 AND l2.description=iv.transactionTypeCode    
                INNER JOIN entityDetails_tb R WITH (NOLOCK) ON R.entityDetailId = IV.receiverEntityDetailId
                INNER JOIN  entityDetails_tb S WITH (NOLOCK) ON S.entityDetailId = IV.senderEntityDetailId                                      
                LEFT JOIN transactionMap_tb tranMap WITH (NOLOCK) on IV.transactionId = tranMap.srcTransactionId
                Left Join lookUp_tb l3 WITH (NOLOCK) on tranMap.validationStatusId = l3.lookUpId
                Where isnull(tranMap.id, 0) <> 0 and iv.isActive = 1 and iv.invoicedate between @fromDateTime AND GETDATE()";


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
