using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPreAuthorization;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;

namespace AutomationTesting_CorConnect.Utils.FleetPreAuthorization
{
    internal class FleetPreAuthorizationUtil
    {
        internal static void GetData(out string dealerCode, out string fleetCode)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            new FleetPreAuthorizationDAL().GetData(out dealerCode, out fleetCode);
        }
    }
}
