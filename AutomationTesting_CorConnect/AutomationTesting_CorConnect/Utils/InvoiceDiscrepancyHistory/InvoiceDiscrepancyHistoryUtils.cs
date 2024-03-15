using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceDiscrepancyHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.InvoiceDiscrepancyHistory
{
    internal class InvoiceDiscrepancyHistoryUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new InvoiceDiscrepancyHistoryDAL().GetData(out FromDate, out ToDate);
        }

        internal static void GetDateFromLocation(string location ,out string FromDate, out string ToDate)
        {
            new InvoiceDiscrepancyHistoryDAL().GetDateFromLocation(location ,out FromDate, out ToDate);
        }

        internal static bool ValidateInvoiceMovedFromHistory(string dealerInv)
        {
            return new InvoiceDiscrepancyHistoryDAL().ValidateInvoiceMovedFromHistory(dealerInv);
        }

        internal static bool ValidateInvoiceInHistory(string dealerInv)
        {
            return new InvoiceDiscrepancyHistoryDAL().ValidateInvoiceInHistory(dealerInv);
        }
        internal static void UpdateInvoiceToExpire(string dealerInv)
        {
             new InvoiceDiscrepancyHistoryDAL().UpdateInvoiceToExpire(dealerInv);
        }
        internal static string GetInvoiceNotInBalance(string dealerName)
        {
            return new InvoiceDiscrepancyHistoryDAL().GetInvoiceNotInBalance(dealerName);
        }
    }
}
