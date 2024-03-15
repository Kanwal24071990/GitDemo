using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DunningStatus;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;

namespace AutomationTesting_CorConnect.Utils.DunningStatus
{
    internal class DunningStatusUtil
    {
        internal static void GetData(out string corcentricCode, out string from, out string to)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            new DunningStatusDAL().GetData(out corcentricCode, out from, out to);
        }
    }
}
