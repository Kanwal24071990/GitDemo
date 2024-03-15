using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetBillToSalesSummary;

namespace AutomationTesting_CorConnect.Utils.FleetBillToSalesSummary
{
    internal class FleetBillToSalesSummaryUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new FleetBillToSalesSummaryDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
