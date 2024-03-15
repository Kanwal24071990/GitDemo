using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPartSummaryFleetLocation;

namespace AutomationTesting_CorConnect.Utils.DealerPartSummaryFleetLocation
{
    internal class DealerPartSummaryFleetLocationUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new DealerPartSummaryFleetLocationDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
