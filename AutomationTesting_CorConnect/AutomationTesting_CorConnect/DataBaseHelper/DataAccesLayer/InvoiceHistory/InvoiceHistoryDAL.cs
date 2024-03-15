using AutomationTesting_CorConnect.Helper;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceHistory
{
    internal class InvoiceHistoryDAL : BaseDataAccessLayer
    {
        internal bool UpdateInvoicePostedToAccounting(string transactionNumber)
        {
            string query = @"update Invoice_tb set arPostedToAccounting =1, apPostedToAccounting = 1
                            from Invoice_tb where transactionNumber =@transactionNumber";
            List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@transactionNumber", transactionNumber),
                    };
            try
            {
                ExecuteNonQuery(query, sp, false);
                return true;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return false;
            }
        }

        internal void GetBulkCommentsChangeInvoice(out string invoiceNumber, out string userID)
        {
            invoiceNumber = string.Empty;
            userID = string.Empty;
            string query = @"select top 1 inv.invoiceNumber, UserID 
                            from invoice_tb inv with (NOLOCK)
                            inner join bulkActionsDetail_tb bad with (NOLOCK) on inv.transactionId = bad.bulkActionValue and bad.isProcessed = 1
                            left join invoiceReference_tb ir with (NOLOCK) on inv.invoiceId = ir.invoiceId
                            inner join AuditTrail_tb at with (NOLOCK) on  ir.invoiceReferenceId = at.AuditPKId and at.AuditTable = 'invoiceComment_tb'
                            where 
                            AuditAction = 'CI'
                            order by invoiceDate desc";
            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        invoiceNumber = reader.GetValue(0).ToString();
                        userID = reader.GetValue(1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }
        }

        internal void GetBulkUnitNumberChangeInvoice(out string invoiceNumber, out string userID)
        {
            invoiceNumber = string.Empty;
            userID = string.Empty;
            string query = @"select top 1 inv.invoiceNumber, UserID
                            from invoice_tb inv with (NOLOCK)
                            inner join bulkActionsDetail_tb bad with (NOLOCK) on inv.transactionId = bad.bulkActionValue and bad.isProcessed = 1
                            left join invoiceReference_tb ir with (NOLOCK) on inv.invoiceId = ir.invoiceId
                            inner join AuditTrail_tb at with (NOLOCK) on  ir.invoiceReferenceId = at.AuditPKId and at.AuditTable = 'invoiceReference_tb'
                            where 
                            AuditAction = 'CI'
                            order by invoiceDate desc";
            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        invoiceNumber = reader.GetValue(0).ToString();
                        userID = reader.GetValue(1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }
        }

        internal void GetBulkPurchaseOrderNumberChangeInvoice(out string invoiceNumber, out string userID)
        {
            invoiceNumber = string.Empty;
            userID = string.Empty;
            string query = @"select top 1 inv.invoiceNumber, UserID
                            from invoice_tb inv with (NOLOCK)
                            inner join bulkActionsDetail_tb bad with (NOLOCK) on inv.transactionId = bad.bulkActionValue and bad.isProcessed = 1
                            inner join AuditTrail_tb at with (NOLOCK) on  inv.invoiceId = at.AuditPKId and at.AuditTable = 'invoice_tb'
                            where 
                            AuditAction = 'CI' and AuditColumn = 'purchaseOrderNumber'
                            order by invoiceDate desc";
            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        invoiceNumber = reader.GetValue(0).ToString();
                        userID = reader.GetValue(1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }
        }

        internal void GetBulkDocumentNumberChangeInvoice(out string invoiceNumber, out string userID)
        {
            invoiceNumber = string.Empty;
            userID = string.Empty;
            string query = @"select top 1 inv.invoiceNumber, UserID
                            from invoice_tb inv with (NOLOCK)
                            inner join bulkActionsDetail_tb bad with (NOLOCK) on inv.transactionId = bad.bulkActionValue and bad.isProcessed = 1
                            inner join AuditTrail_tb at with (NOLOCK) on  inv.invoiceId = at.AuditPKId and at.AuditTable = 'invoice_tb'
                            where 
                            AuditAction = 'CI' and AuditColumn = 'originatingDocumentNumber'
                            order by invoiceDate desc";
            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        invoiceNumber = reader.GetValue(0).ToString();
                        userID = reader.GetValue(1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }
        }
        internal void GetBulkReversalInvoice(out string invoiceNumber, out string userName)
        {
            invoiceNumber = string.Empty;
            userName = string.Empty;
            string query = @"select top 1 inv.InvoiceNumber, u.userName from invoice_tb inv with (NOLOCK)
                            inner join transaction_tb trx with (NOLOCK) on inv.transactionId = trx.transactionId
                            inner join userInvoiceStatus_tb uis with (NOLOCK) on uis.transactionId = inv.transactionId 
	                        and uis.statusId in (select lookupId from lookup_tb where parentlookupcode = 202 and lookupcode = 45)
                            inner join user_tb u with (NOLOCK) on trx.userId = u.userId
                            where isnull(referenceType,0) = 1 and referencedTransactionid is NOT NULL
                            and inv.transactionId in (
	                        select NewTransactionId from staging_BulkReversalInvoices_bk  br where statusId = 1)
                            order by inv.createDate desc";
            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        invoiceNumber = reader.GetString(0);
                        userName = reader.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }
        }
        
    }
}
