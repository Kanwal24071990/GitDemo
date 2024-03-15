using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPurchaseReport
{
    internal class DealerPurchaseReportDAL:BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                if (userType == "ADMIN")
                {
                    query = @"SELECT TOP 1 CONVERT (Date, IV.receiveDate) AS FromDate, Convert (Date, IV.receiveDate) AS ToDate FROM (SELECT invoice_tb.invoiceId, invoice_tb.ApprovalStatus, s.corcentricCode as dealerCode, f.corcentricCode as FleetCode, receiveDate FROM [invoice_tb] with (nolock) inner join
[entitydetails_tb] f on f.entitydetailid=invoice_tb.receiverEntityDetailId inner join [entitydetails_tb] s on 
s.entitydetailid=invoice_tb.senderbilltoEntityDetailId where invoice_tb.isActive = 1) AS IV LEFT JOIN [actionComments_tb] 
AC WITH (NOLOCK) ON AC.invoiceId = IV.invoiceId AND IV.ApprovalStatus = AC.actionTypeId AND AC.isActive = 1 ORDER BY IV.receiveDate DESC;";

                    using (var reader = ExecuteReader(query, false))
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
                query = @"SELECT  Count(*) FROM ( SELECT invoice_tb.invoiceId , invoice_tb.ApprovalStatus,s.corcentricCode 
                as dealerCode, f.corcentricCode as FleetCode ,receiveDate FROM  invoice_tb  with (nolock) inner join entitydetails_tb f 
                on f.entitydetailid=invoice_tb.receiverEntityDetailId inner join entitydetails_tb s on s.entitydetailid=invoice_tb.senderbilltoEntityDetailId 
                where invoice_tb.isActive = 1 AND invoice_tb.senderEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoDlr') 
                AND invoice_tb.receiverEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoFlt') 
                AND invoice_tb.ARinvoiceDate BETWEEN @fromDateTime AND GETDATE() AND currencyCode='USD' ) AS IV LEFT JOIN actionComments_tb AC WITH (NOLOCK) 
                ON AC.invoiceId = IV.invoiceId AND IV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1";

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

            else if (dateRange == "Current Quarter")
            {
                DateTime currentDate = DateTime.Now;
                int currentQuarter = (currentDate.Month - 1) / 3 + 1;
                fromDateTime = new DateTime(currentDate.Year, 3 * currentQuarter - 2, 1).ToString("yyyy-MM-dd");
                var toDateTime = Convert.ToDateTime(fromDateTime).AddMonths(3).AddDays(-1).ToString("yyyy-MM-dd");
                Console.WriteLine("From Date: " + fromDateTime);
                Console.WriteLine("To Date: " + toDateTime);

                query = @"SELECT Count(*) FROM(SELECT invoice_tb.invoiceId, invoice_tb.ApprovalStatus, s.corcentricCode
                as dealerCode, f.corcentricCode as FleetCode, receiveDate FROM  invoice_tb  with(nolock) inner join entitydetails_tb f
                on f.entitydetailid = invoice_tb.receiverEntityDetailId inner join entitydetails_tb s on s.entitydetailid = invoice_tb.senderbilltoEntityDetailId
                where invoice_tb.isActive = 1 AND invoice_tb.senderEntityDetailId = (Select entitydetailID from entitydetails_tb where corcentricCode = '18AutoDlr')
                AND invoice_tb.receiverEntityDetailId = (Select entitydetailID from entitydetails_tb where corcentricCode = '18AutoFlt') 
                AND invoice_tb.ARinvoiceDate BETWEEN @fromDateTime AND @toDateTime AND currencyCode = 'USD' ) AS IV LEFT JOIN actionComments_tb AC WITH(NOLOCK)
                ON AC.invoiceId = IV.invoiceId AND IV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1";

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
            else if (dateRange == "Last 185 days")
            {
                fromDateTime = DateTime.Now.AddDays(-184).ToString("yyyy-MM-dd");
                query = @"SELECT Count(*) FROM(SELECT invoice_tb.invoiceId, invoice_tb.ApprovalStatus, s.corcentricCode
                as dealerCode, f.corcentricCode as FleetCode, receiveDate FROM  invoice_tb  with(nolock) inner join entitydetails_tb f
                on f.entitydetailid = invoice_tb.receiverEntityDetailId inner join entitydetails_tb s on s.entitydetailid = invoice_tb.senderbilltoEntityDetailId
                where invoice_tb.isActive = 1 AND invoice_tb.senderEntityDetailId = (Select entitydetailID from entitydetails_tb where corcentricCode = '18AutoDlr')
                AND invoice_tb.receiverEntityDetailId = (Select entitydetailID from entitydetails_tb where corcentricCode = '18AutoFlt') 
                AND invoice_tb.ARinvoiceDate BETWEEN @fromDateTime AND GETDATE() AND currencyCode = 'USD' ) AS IV LEFT JOIN actionComments_tb AC WITH(NOLOCK)
                ON AC.invoiceId = IV.invoiceId AND IV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1";

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

                query = @"SELECT  Count(*) FROM ( SELECT invoice_tb.invoiceId , invoice_tb.ApprovalStatus,s.corcentricCode 
                as dealerCode, f.corcentricCode as FleetCode ,receiveDate FROM  invoice_tb  with (nolock) inner join entitydetails_tb f 
                on f.entitydetailid=invoice_tb.receiverEntityDetailId inner join entitydetails_tb s on s.entitydetailid=invoice_tb.senderbilltoEntityDetailId 
                where invoice_tb.isActive = 1 AND invoice_tb.senderEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoDlr') 
                AND invoice_tb.receiverEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoFlt') 
                AND invoice_tb.ARinvoiceDate BETWEEN @fromDateTime AND GETDATE() AND currencyCode='USD' ) AS IV LEFT JOIN actionComments_tb AC WITH (NOLOCK) 
                ON AC.invoiceId = IV.invoiceId AND IV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1";

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

            else if (dateRange == "Last 12 months")
            {
                fromDateTime = DateTime.Now.AddMonths(-12).AddDays(-1).ToString("yyyy-MM-dd");
                Console.WriteLine("From Date: " + fromDateTime);
                
                query = @"SELECT  Count(*) FROM ( SELECT invoice_tb.invoiceId , invoice_tb.ApprovalStatus,s.corcentricCode 
                as dealerCode, f.corcentricCode as FleetCode ,receiveDate FROM  invoice_tb  with (nolock) inner join entitydetails_tb f 
                on f.entitydetailid=invoice_tb.receiverEntityDetailId inner join entitydetails_tb s on s.entitydetailid=invoice_tb.senderbilltoEntityDetailId 
                where invoice_tb.isActive = 1 AND invoice_tb.senderEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoDlr') 
                AND invoice_tb.receiverEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoFlt') 
                AND invoice_tb.ARinvoiceDate BETWEEN @fromDateTime AND GETDATE() AND currencyCode='USD' ) AS IV LEFT JOIN actionComments_tb AC WITH (NOLOCK) 
                ON AC.invoiceId = IV.invoiceId AND IV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1";

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
            else if (dateRange == "YTD")
            {
                fromDateTime = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
                Console.WriteLine("From Date: " + fromDateTime);

                query = @"SELECT  Count(*) FROM ( SELECT invoice_tb.invoiceId , invoice_tb.ApprovalStatus,s.corcentricCode 
                as dealerCode, f.corcentricCode as FleetCode ,receiveDate FROM  invoice_tb  with (nolock) inner join entitydetails_tb f 
                on f.entitydetailid=invoice_tb.receiverEntityDetailId inner join entitydetails_tb s on s.entitydetailid=invoice_tb.senderbilltoEntityDetailId 
                where invoice_tb.isActive = 1 AND invoice_tb.senderEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoDlr') 
                AND invoice_tb.receiverEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoFlt') 
                AND invoice_tb.ARinvoiceDate BETWEEN @fromDateTime AND GETDATE() AND currencyCode='USD' ) AS IV LEFT JOIN actionComments_tb AC WITH (NOLOCK) 
                ON AC.invoiceId = IV.invoiceId AND IV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1";

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



            else if (dateRange == "Customized date")
            {
                fromDateTime = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
                Console.WriteLine("From Date: " + fromDateTime);

                query = @"SELECT  Count(*) FROM ( SELECT invoice_tb.invoiceId , invoice_tb.ApprovalStatus,s.corcentricCode 
                as dealerCode, f.corcentricCode as FleetCode ,receiveDate FROM  invoice_tb  with (nolock) inner join entitydetails_tb f 
                on f.entitydetailid=invoice_tb.receiverEntityDetailId inner join entitydetails_tb s on s.entitydetailid=invoice_tb.senderbilltoEntityDetailId 
                where invoice_tb.isActive = 1 AND invoice_tb.senderEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoDlr') 
                AND invoice_tb.receiverEntityDetailId=(Select entitydetailID from entitydetails_tb where corcentricCode='18AutoFlt') 
                AND invoice_tb.ARinvoiceDate BETWEEN @fromDateTime AND GETDATE() AND currencyCode='USD' ) AS IV LEFT JOIN actionComments_tb AC WITH (NOLOCK) 
                ON AC.invoiceId = IV.invoiceId AND IV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1";

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