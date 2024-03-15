using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetCreditLimitWatchListReport;


namespace AutomationTesting_CorConnect.Utils.FleetCreditLimitWatchListReport
{
    internal class FleetCreditLimitWatchListReportUtil
    {
        internal static void GetData(out string location)
        {

            new FleetCreditLimitWatchListReportDAL().GetData(out location);
        }
    }
}
