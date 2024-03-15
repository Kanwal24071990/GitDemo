using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartSalesByFleetReport;

namespace AutomationTesting_CorConnect.Utils.PartSalesByFleetReport
{
    internal class PartSalesByFleetReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new PartSalesByFleetReportDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
