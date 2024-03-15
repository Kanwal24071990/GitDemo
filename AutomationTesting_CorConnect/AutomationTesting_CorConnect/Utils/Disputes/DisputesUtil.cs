using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.Disputes;

namespace AutomationTesting_CorConnect.Utils.Disputes
{
    internal class DisputesUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new DisputesDAL().GetData(out FromDate, out ToDate);
        }

        internal static void GetDataRowCount(string dateType, out int rowCount)
        {
            new DisputesDAL().GetDataRowCount(dateType, out rowCount);
        }

        internal static void GetDataRowCountForStatus(string status, out int rowCount)
        {
            new DisputesDAL().GetDataRowCountForStatus(status, out rowCount);
        }

        internal static void GetDataRowCountForEntity(string status, string[] billToEntityDetailId, bool isDealer, bool isFleet, bool isDealerAndFleet, out int rowCount)
        {
            new DisputesDAL().GetDataRowCountForEntity(status, billToEntityDetailId, isDealer, isFleet, isDealerAndFleet, out rowCount);
        }
        internal static void GetBillToEntityDetailId(string entityType, string invoiceNumber, out string entityDetailId)
        {
            new DisputesDAL().GetBillToEntityDetailId(entityType, invoiceNumber, out entityDetailId);
        }

        internal static void GetGridRowCount(string status, string fromDate, string toDate, out int rowCount)
        {
            new DisputesDAL().GetGridRowCount(status, fromDate, toDate, out rowCount);
        }

        internal static string GetDisputeDealerInvoiceNumber(string status, string resolutionDetail, string from, string to)
        {
            return new DisputesDAL().GetDisputeDealerInvoiceNumber(status, resolutionDetail, from, to);
        }

        internal static int GetDBRowCountForCurrencySearch(string currency, string fromDate, string toDate)
        {
            return new DisputesDAL().GetDBRowCountForCurrencySearch(currency, fromDate, toDate);
        }

        internal static int GetDBRowCountCurrencySearchForEntity(string currency, string[] billToEntityDetailId, string fromDate, string toDate, bool isDealerDropDown, bool isFleetDropDown, bool isDealerAndFleetDropDown)
        {
            return new DisputesDAL().GetDBRowCountCurrencySearchForEntity(currency, billToEntityDetailId, fromDate, toDate, isDealerDropDown, isFleetDropDown, isDealerAndFleetDropDown);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new DisputesDAL().GetCountByDateRange(dateRange, days);
        }

        internal static string GetDisputedInvoiceNumber()
        {
            return new DisputesDAL().GetDisputedInvoiceNumber();
        }


        internal static string GetDisputeResolvedInvoiceNumber()
        {
            return new DisputesDAL().GetDisputeResolvedInvoiceNumber();
        }

        internal static void GetDataByProgramInvNumber(string programInvNumber, out string FromDate, out string ToDate)
        {

            new DisputesDAL().GetDataByProgramInvNumber(programInvNumber, out FromDate, out ToDate);
        }
        


    }

}
