

using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoiceTransactionLookup;

namespace AutomationTesting_CorConnect.Utils.FleetInvoiceTransactionLookup
{
    internal class FleetInvoiceTransactionLookupUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new FleetInvoiceTransactionLookupDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetRowCountTransactionStatus(string transactionStatus)
        {
            return new FleetInvoiceTransactionLookupDAL().GetRowCountTransactionStatus(transactionStatus);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new FleetInvoiceTransactionLookupDAL().GetCountByDateRange(dateRange, days);
        }


    }
}
