using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPreAuthorization;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;

namespace AutomationTesting_CorConnect.Utils.DealerPreAuthorization
{
    internal class DealerPreAuthorizationUtils
    {
        internal static void GetData(out string dealerCode, out string fleetCode)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            new DealerPreAuthorizationDAL().GetData(out dealerCode, out fleetCode);
        }
    }
}
