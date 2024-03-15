using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPartCategorySalesSummaryBillTo;

namespace AutomationTesting_CorConnect.Utils.FleetPartCategorySalesSummaryBillTo
{
    internal class FleetPartCategorySalesSummaryBillToUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new FleetPartCategorySalesSummaryBillToDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
