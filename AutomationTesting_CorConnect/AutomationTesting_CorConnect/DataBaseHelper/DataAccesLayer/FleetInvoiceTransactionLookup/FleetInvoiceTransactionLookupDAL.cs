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

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoiceTransactionLookup
{
    internal class FleetInvoiceTransactionLookupDAL : BaseDataAccessLayer
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
                    query = @"select top 1 Convert (Date , createdate) as FromDate ,Convert (Date , createdate) as ToDate from transaction_tb T where isActive =1 AND
                            (T.requestTypeCode IN('T', 'A') OR (T.requestTypeCode = 'S' AND T.validationStatus = 2 AND(T.transactionId NOT IN(SELECT ee.transactionid
                            from transactionerror_tb ee WITH(NOLOCK) INNER JOIN errorCodeCorrectionOptRel_tb EC(NOLOCK) ON EC.errorCodeId = ee.errorCodeId AND ec.DiscrepancyType = 1
                            WHERE ee.errorcodeid NOT IN(23, 12) AND ee.isactive = 1))) OR (T.requestTypeCode = 'S' AND T.validationStatus IN(1, 13, 14))) order by createDate desc;";

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
                    query = @"DECLARE @@Userid AS INT; DECLARE @@FleetAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM user_tb
                            WHERE username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK)
                            WHERE entityDetailId IN (SELECT DISTINCT UR.entityId FROM userRelationships_tb UR WITH(NOLOCK) INNER JOIN user_tb U WITH(NOLOCK) ON UR.userId = U.userId
                            WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID
                            FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations
                            SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId
                            FROM userRelationships_tb UR2 WITH(NOLOCK) WHERE UR2.userId = @@UserID AND IsActive=1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 Convert(Date, t.createdate)
                            AS FromDate, Convert(Date, t.createdate) AS ToDate FROM transaction_tb t INNER JOIN @@FleetAccessLocations SE ON SE.entityDetailId = T.receiverEntityDetailId
                            LEFT JOIN transaction_tb TI WITH (NOLOCK) ON T.transactionId = TI.transactionId AND (TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus
                            IN(3, 4, 7) LEFT JOIN invoice_tb I WITH (NOLOCK) ON I.transactionId = T.transactionId WHERE t.isActive =1 AND t.validationStatus NOT IN(0, 9) AND T.requestTypeCode
                            NOT IN ('I', 'O', 'Q', 'R') AND TI.transactionId IS NULL AND(I.invoiceId IS NULL OR I.systemType = 0) AND(T.requestTypeCode IN('T', 'A') OR
                            ((T.requestTypeCode = 'S' AND T.validationStatus = 2 AND(T.transactionId NOT IN(SELECT ee.transactionid FROM transactionerror_tb EE WITH (NOLOCK)
                            INNER JOIN errorCodeCorrectionOptRel_tb EC (NOLOCK) ON EC.errorCodeId = EE.errorCodeId AND EC.DiscrepancyType =1 WHERE EE.errorcodeid NOT IN(23, 12)
                            AND EE.isactive = 1))) OR(T.requestTypeCode = 'S' AND T.validationStatus IN(1, 13, 14)))) ORDER BY T.createDate DESC;";
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
        internal int GetRowCountTransactionStatus(string transactionStatus)
        {

            string query = null;
            List<string> rowCount = new List<string>();
            string fromDateTime = null;
            List<SqlParameter> sp = new();

            if (transactionStatus == "Authorized")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select t.transactionNumber,* from transaction_tb t 
                        where T.requestTypeCode in ('T','A') and T.validationStatus in (1,2)
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Authorization Expired")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select t.transactionNumber,* from transaction_tb t 
                        where T.requestTypeCode in ('T','A') and (T.validationStatus = 6)
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Authorization Voided")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select t.transactionNumber,* from transaction_tb T
                        where T.requestTypeCode in ('T','A') and (T.validationStatus = 5)
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Awaiting Fleet Release")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select t.transactionNumber,* from transaction_tb t 
                        where T.requestTypeCode = 'S' AND T.validationStatus = 13
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Awaiting Reseller Release")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select t.transactionNumber,* from transaction_tb t 
                        where T.requestTypeCode = 'S' AND T.validationStatus = 13
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Fleet Release Rejected")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select t.transactionNumber, * from transaction_tb t 
                        where T.requestTypeCode = 'S' AND T.validationStatus = 14
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Reseller Release Rejected")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select  * from transaction_tb t 
                        where t.requestTypeCode = 'S' AND t.validationStatus = 18
                        and t.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Disputed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 and td.disputeStatus = 1
                        and T.createDate between @fromDateTime and getdate()
                        and appaidinfull is null and arpaidinfull is null
                        and apinvoiceduedate>getdate() and arinvoiceduedate>getdate()
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Resolved")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 and td.disputeStatus = 2
                        and T.createDate between @fromDateTime and getdate()
                        and appaidinfull is null and arpaidinfull is null
                        and apinvoiceduedate>getdate() and arinvoiceduedate>getdate()
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Closed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 and td.disputeStatus = 3
                        and T.createDate between @fromDateTime and getdate()
                        and appaidinfull is null and arpaidinfull is null
                        and apinvoiceduedate>getdate() and arinvoiceduedate>getdate()
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Paid")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime and getdate()
                        and arpaidinfull=1
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Paid-Closed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime and getdate()
                        and arpaidinfull=1 and td.disputeStatus = 3
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }

            else if (transactionStatus == "Paid-Disputed")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime and getdate()
                        and arpaidinfull=1 and td.disputeStatus = 1
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            }
            else if (transactionStatus == "Paid-Resolved")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime and getdate()
                        and arpaidinfull=1 and td.disputeStatus = 2
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Hold")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime and getdate()
                        and iv.arInvoiceDueDate < GETDATE() and th.holdStatus=1
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            }
            else if (transactionStatus == "Current")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between  @fromDateTime AND GETDATE() 
                        and td.transactionDisputeId is  null and iv.arPaidInFull is null and iv.apPaidInFull is null and iv.arInvoiceDueDate > GETDATE() and t.validationStatus=1
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime AND GETDATE()
                        and (td.transactionDisputeId is  null or td.transactionDisputeId = 0) and (iv.arPaidInFull is null or iv.arPaidInFull = 0 ) and (iv.apPaidInFull is null or iv.apPaidInFull = 0)
                        and iv.arInvoiceDueDate < CAST(GETDATE() AS DATE)  and t.validationStatus=1
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Closed")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime AND GETDATE()
                        and iv.arInvoiceDueDate < CAST(GETDATE() AS DATE) AND td.disputeStatus = 3 
                        order by t.createdate desc";
               
                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Hold")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime AND GETDATE()
                        and th.holdStatus = 1
                        and iv.arInvoiceDueDate > CAST(GETDATE() AS DATE)
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Hold Released")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime AND GETDATE()
                        and  th.holdStatus = 2 and (th.releaseDate > td.dateSent or td.transactionDisputeId is null)
                        and iv.arInvoiceDueDate > CAST(GETDATE() AS DATE) 
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Disputed")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
	                    left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
	                    left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
	                    INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
	                    where IV.systemType=0 
	                    and T.createDate between @fromDateTime AND GETDATE()
	                    and iv.arInvoiceDueDate < CAST(GETDATE() AS DATE) AND td.disputeStatus = 1
	                    order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            }
            else if (transactionStatus == "Past due-Resolved")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime AND GETDATE()
                        and td.disputeStatus = 2
                        and iv.arInvoiceDueDate < GETDATE() 
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Hold Released")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime AND GETDATE()
                        and iv.arInvoiceDueDate < GETDATE()
                        and th.holdStatus = 2 and (th.releaseDate > td.dateSent or td.transactionDisputeId is null)
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Pay-In Progress")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        LEFT JOIN payInvoiceOnline_tb pi WITH (NOLOCK) ON Iv.transactionId = pi.transactionId and pi.isActive = 1
                        inner join lookup_tb l WITH (NOLOCK) ON l.lookupid = pi.paymentStatus
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime AND GETDATE()
                        and l.parentlookupcode=250 and l.lookupcode=1
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Pay-Pending")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select iv.invoiceNumber from invoice_tb iv
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        LEFT JOIN payInvoiceOnline_tb pi WITH (NOLOCK) ON Iv.transactionId = pi.transactionId and pi.isActive = 1
                        inner join lookup_tb l WITH (NOLOCK) ON l.lookupid = pi.paymentStatus
                        where IV.systemType=0 
                        and T.createDate between @fromDateTime AND GETDATE()
                        and l.parentlookupcode=250 and l.lookupcode=2
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Fixable Discrepancy")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

                query = @"select t.transactionNumber from transaction_tb t 
                        where T.requestTypeCode = 'S' AND T.validationStatus = 2
                        and requestId not in ('InvForwarding')
                        and T.createDate between @fromDateTime AND GETDATE()
                        order by t.createDate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            }
            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    rowCount.Add(reader.GetString(0));

                }
            }

            int count = rowCount.Count();
            return count;
        }
        internal int GetCountByDateRange(string dateRange, int days = 0)
        {

            string query = null;
            string fromDateTime = null;

            if (dateRange == "Last 7 days")
            {
                fromDateTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                query = @"select Count(*)  from transaction_tb T LEFT JOIN transaction_tb TI WITH (NOLOCK) ON T.transactionId = TI.transactionId AND (TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus in (3,4,7)
                LEFT JOIN invoice_tb I WITH (NOLOCK) on I.transactionId = T.transactionId
                WHERE TI.transactionId IS NULL AND T.validationStatus <> 0 AND T.requestTypeCode NOT IN ('I','O','Q','R')
                AND (I.invoiceId IS NULL OR I.systemType in (0,1,2,3)) AND CAST (T.createDate as DATE) BETWEEN @fromDateTime AND GETDATE() AND (
                T.requestTypeCode IN ('T','A') OR ((T.requestTypeCode ='S' AND T.validationStatus = 2 AND (T.transactionId NOT IN 
                (SELECT ee.transactionid from transactionerror_tb ee WITH (NOLOCK)
                INNER JOIN [errorCodeCorrectionOptRel_tb] EC WITH (NOLOCK) ON EC.errorCodeId = ee.errorCodeId AND ec.DiscrepancyType =1  
                WHERE ee.errorcodeid NOT IN (23,12) AND ee.isactive = 1))) 
                OR (T.requestTypeCode ='S' AND T.validationStatus IN (1,13,14,17,18))))";

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
                query = @"select Count(*)  from transaction_tb T LEFT JOIN transaction_tb TI WITH (NOLOCK) ON T.transactionId = TI.transactionId AND (TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus in (3,4,7)
                LEFT JOIN invoice_tb I WITH (NOLOCK) on I.transactionId = T.transactionId
                WHERE TI.transactionId IS NULL AND T.validationStatus <> 0 AND T.requestTypeCode NOT IN ('I','O','Q','R')
                AND (I.invoiceId IS NULL OR I.systemType in (0,1,2,3)) AND CAST (T.createDate as DATE) BETWEEN @fromDateTime AND GETDATE() AND (
                T.requestTypeCode IN ('T','A') OR ((T.requestTypeCode ='S' AND T.validationStatus = 2 AND (T.transactionId NOT IN 
                (SELECT ee.transactionid from transactionerror_tb ee WITH (NOLOCK)
                INNER JOIN [errorCodeCorrectionOptRel_tb] EC WITH (NOLOCK) ON EC.errorCodeId = ee.errorCodeId AND ec.DiscrepancyType =1  
                WHERE ee.errorcodeid NOT IN (23,12) AND ee.isactive = 1))) 
                OR (T.requestTypeCode ='S' AND T.validationStatus IN (1,13,14,17,18))))";

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
                query = @"select Count(*)  from transaction_tb T LEFT JOIN transaction_tb TI WITH (NOLOCK) ON T.transactionId = TI.transactionId AND (TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus in (3,4,7)
                LEFT JOIN invoice_tb I WITH (NOLOCK) on I.transactionId = T.transactionId
                WHERE TI.transactionId IS NULL AND T.validationStatus <> 0 AND T.requestTypeCode NOT IN ('I','O','Q','R')
                AND (I.invoiceId IS NULL OR I.systemType in (0,1,2,3)) AND CAST (T.createDate as DATE) BETWEEN @fromDateTime AND GETDATE() AND (
                T.requestTypeCode IN ('T','A') OR ((T.requestTypeCode ='S' AND T.validationStatus = 2 AND (T.transactionId NOT IN 
                (SELECT ee.transactionid from transactionerror_tb ee WITH (NOLOCK)
                INNER JOIN [errorCodeCorrectionOptRel_tb] EC WITH (NOLOCK) ON EC.errorCodeId = ee.errorCodeId AND ec.DiscrepancyType =1  
                WHERE ee.errorcodeid NOT IN (23,12) AND ee.isactive = 1))) 
                OR (T.requestTypeCode ='S' AND T.validationStatus IN (1,13,14,17,18))))";

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

                query = @"select Count(*)  from transaction_tb T LEFT JOIN transaction_tb TI WITH (NOLOCK) ON T.transactionId = TI.transactionId AND (TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus in (3,4,7)
                LEFT JOIN invoice_tb I WITH (NOLOCK) on I.transactionId = T.transactionId
                WHERE TI.transactionId IS NULL AND T.validationStatus <> 0 AND T.requestTypeCode NOT IN ('I','O','Q','R')
                AND (I.invoiceId IS NULL OR I.systemType in (0,1,2,3)) AND CAST (T.createDate as DATE) BETWEEN @fromDateTime AND GETDATE() AND (
                T.requestTypeCode IN ('T','A') OR ((T.requestTypeCode ='S' AND T.validationStatus = 2 AND (T.transactionId NOT IN 
                (SELECT ee.transactionid from transactionerror_tb ee WITH (NOLOCK)
                INNER JOIN [errorCodeCorrectionOptRel_tb] EC WITH (NOLOCK) ON EC.errorCodeId = ee.errorCodeId AND ec.DiscrepancyType =1  
                WHERE ee.errorcodeid NOT IN (23,12) AND ee.isactive = 1))) 
                OR (T.requestTypeCode ='S' AND T.validationStatus IN (1,13,14,17,18))))";

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

                query = @"select Count(*)  from transaction_tb T LEFT JOIN transaction_tb TI WITH (NOLOCK) ON T.transactionId = TI.transactionId AND (TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus in (3,4,7)
                LEFT JOIN invoice_tb I WITH (NOLOCK) on I.transactionId = T.transactionId
                WHERE TI.transactionId IS NULL AND T.validationStatus <> 0 AND T.requestTypeCode NOT IN ('I','O','Q','R')
                AND (I.invoiceId IS NULL OR I.systemType in (0,1,2,3)) AND CAST (T.createDate as DATE) BETWEEN @fromDateTime AND @toDateTime AND (
                T.requestTypeCode IN ('T','A') OR ((T.requestTypeCode ='S' AND T.validationStatus = 2 AND (T.transactionId NOT IN 
                (SELECT ee.transactionid from transactionerror_tb ee WITH (NOLOCK)
                INNER JOIN [errorCodeCorrectionOptRel_tb] EC WITH (NOLOCK) ON EC.errorCodeId = ee.errorCodeId AND ec.DiscrepancyType =1  
                WHERE ee.errorcodeid NOT IN (23,12) AND ee.isactive = 1))) 
                OR (T.requestTypeCode ='S' AND T.validationStatus IN (1,13,14,17,18))))";

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
                query = @"select Count(*)  from transaction_tb T LEFT JOIN transaction_tb TI WITH (NOLOCK) ON T.transactionId = TI.transactionId AND (TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus in (3,4,7)
                LEFT JOIN invoice_tb I WITH (NOLOCK) on I.transactionId = T.transactionId
                WHERE TI.transactionId IS NULL AND T.validationStatus <> 0 AND T.requestTypeCode NOT IN ('I','O','Q','R')
                AND (I.invoiceId IS NULL OR I.systemType in (0,1,2,3)) AND CAST (T.createDate as DATE) BETWEEN @fromDateTime AND GETDATE() AND (
                T.requestTypeCode IN ('T','A') OR ((T.requestTypeCode ='S' AND T.validationStatus = 2 AND (T.transactionId NOT IN 
                (SELECT ee.transactionid from transactionerror_tb ee WITH (NOLOCK)
                INNER JOIN [errorCodeCorrectionOptRel_tb] EC WITH (NOLOCK) ON EC.errorCodeId = ee.errorCodeId AND ec.DiscrepancyType =1  
                WHERE ee.errorcodeid NOT IN (23,12) AND ee.isactive = 1))) 
                OR (T.requestTypeCode ='S' AND T.validationStatus IN (1,13,14,17,18))))";

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
