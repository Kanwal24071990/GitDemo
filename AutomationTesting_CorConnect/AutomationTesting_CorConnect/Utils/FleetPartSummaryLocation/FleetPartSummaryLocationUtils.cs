using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPartSummaryLocation;

namespace AutomationTesting_CorConnect.Utils.FleetPartSummaryLocation
{
    internal class FleetPartSummaryLocationUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new FleetPartSummaryLocationDAL().GetData(out FromDate, out ToDate);
        }
    }
}
