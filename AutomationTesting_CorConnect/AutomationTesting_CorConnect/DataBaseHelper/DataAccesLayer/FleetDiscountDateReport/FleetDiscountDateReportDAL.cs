using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetDiscountDateReport
{
    internal class FleetDiscountDateReportDAL : BaseDataAccessLayer
    {
        internal void GetDateData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                query = @"SELECT TOP 1 ISNULL(ivpt.ARDiscountDueDate, arInvoiceDueDate) AS FromDate, ISNULL(ivpt.ARDiscountDueDate, arInvoiceDueDate) AS ToDate 
                    FROM invoice_tb IV WITH (NOLOCK) LEFT JOIN invoiceVariablePaymentTerms_tb ivpt WITH (NOLOCK) ON (iv.invoiceId = ivpt.invoiceId) ORDER BY fromdate DESC";
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
                LoggingHelper.LogException(ex);
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
                query = "SELECT COUNT(*) FROM invoice_tb iv with (nolock) LEFT JOIN invoiceVariablePaymentTerms_tb ivpt with (nolock) on iv.invoiceId = ivpt.invoiceId where (iv.ARInvoiceDueDate BETWEEN @fromDateTime AND GETDATE())";
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
                query = "SELECT COUNT(*) FROM invoice_tb iv with (nolock) LEFT JOIN invoiceVariablePaymentTerms_tb ivpt with (nolock) on iv.invoiceId = ivpt.invoiceId where (iv.ARInvoiceDueDate BETWEEN @fromDateTime AND GETDATE())";

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
                query = "SELECT COUNT(*) FROM invoice_tb iv with (nolock) LEFT JOIN invoiceVariablePaymentTerms_tb ivpt with (nolock) on iv.invoiceId = ivpt.invoiceId where (iv.ARInvoiceDueDate BETWEEN @fromDateTime AND GETDATE())";

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

                query = "SELECT COUNT(*) FROM invoice_tb iv with (nolock) LEFT JOIN invoiceVariablePaymentTerms_tb ivpt with (nolock) on iv.invoiceId = ivpt.invoiceId where (iv.ARInvoiceDueDate BETWEEN @fromDateTime AND GETDATE())";

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

                query = @"SELECT COUNT(*) FROM invoice_tb iv with (nolock) LEFT JOIN invoiceVariablePaymentTerms_tb ivpt with (nolock) on iv.invoiceId = ivpt.invoiceId where (iv.ARInvoiceDueDate BETWEEN @fromDateTime AND @toDateTime)";

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
                query = "SELECT COUNT(*) FROM invoice_tb iv with (nolock) LEFT JOIN invoiceVariablePaymentTerms_tb ivpt with (nolock) on iv.invoiceId = ivpt.invoiceId where (iv.ARInvoiceDueDate BETWEEN @fromDateTime AND GETDATE())";

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
