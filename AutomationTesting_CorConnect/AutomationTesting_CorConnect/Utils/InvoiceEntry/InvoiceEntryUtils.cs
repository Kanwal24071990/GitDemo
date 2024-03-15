using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceEntry;
using System;

namespace AutomationTesting_CorConnect.Utils.InvoiceEntry
{
    internal class InvoiceEntryUtils
    {
        internal static void GetInvoice(out DateTime dateTime, out string transactionNumber)
        {
            new InvoiceEntryDAL().GetInvoice(out dateTime, out transactionNumber);
        }

        internal static void GetData(out string FromDate, out string ToDate)
        {
            new InvoiceEntryDAL().GetData(out FromDate, out ToDate);
        }

        internal static int GetInvoiceSectionCount(string transactionNumber)
        { 
           return new InvoiceEntryDAL().GetInvoiceSectionCount(transactionNumber);
        }
        internal static int GetDiscrepantInvoiceSectionCount(string transactionNumber)
        {
            return new InvoiceEntryDAL().GetDiscrepantInvoiceSectionCount(transactionNumber);
        }

        internal static bool IsInvoiceEligiblForPaymentPortal(string transactionNumber)
        {
            return new InvoiceEntryDAL().IsInvoiceEligiblForPaymentPortal(transactionNumber);
        }
    }
}
