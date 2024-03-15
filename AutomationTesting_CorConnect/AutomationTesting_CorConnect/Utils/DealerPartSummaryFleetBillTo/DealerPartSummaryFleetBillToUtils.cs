using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPartSummaryFleetBillTo;

namespace AutomationTesting_CorConnect.Utils.DealerPartSummaryFleetBillTo
{
    internal class DealerPartSummaryFleetBillToUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new DealerPartSummaryFleetBillToDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
