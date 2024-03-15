using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.InvoiceHistory
{
    internal class InvoiceHistoryUtil
    {
        internal static bool UpdateInvoicePostedToAccounting(string transactionNumber)
        {
            return new InvoiceHistoryDAL().UpdateInvoicePostedToAccounting(transactionNumber);
        }
        internal static void GetBulkCommentsChangeInvoice(out string invoiceNumber, out string userID)
        {
            new InvoiceHistoryDAL().GetBulkCommentsChangeInvoice(out invoiceNumber, out userID);
        }
        internal static void GetBulkUnitNumberChangeInvoice(out string invoiceNumber, out string userID)
        {
            new InvoiceHistoryDAL().GetBulkUnitNumberChangeInvoice(out invoiceNumber, out userID);
        }
        internal static void GetBulkPurchaseOrderNumberChangeInvoice(out string invoiceNumber, out string userID)
        {
            new InvoiceHistoryDAL().GetBulkPurchaseOrderNumberChangeInvoice(out invoiceNumber, out userID);
        }
        internal static void GetBulkDocumentNumberChangeInvoice(out string invoiceNumber, out string userID)
        {
            new InvoiceHistoryDAL().GetBulkDocumentNumberChangeInvoice(out invoiceNumber, out userID);
        }
        internal static void GetBulkReversalInvoice(out string invoiceNumber, out string userName)
        {
            new InvoiceHistoryDAL().GetBulkReversalInvoice(out invoiceNumber, out userName);
        }
        

    }
}
