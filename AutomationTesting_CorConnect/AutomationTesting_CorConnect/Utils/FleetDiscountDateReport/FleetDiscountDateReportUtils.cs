using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetDiscountDateReport;

namespace AutomationTesting_CorConnect.Utils.FleetDiscountDateReport
{
    internal class FleetDiscountDateReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new FleetDiscountDateReportDAL().GetDateData(out fromDate, out toDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new FleetDiscountDateReportDAL().GetCountByDateRange(dateRange, days);
        }

       
    }
}
