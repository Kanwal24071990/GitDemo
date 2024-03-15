using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.BatchRequestStatus;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;

namespace AutomationTesting_CorConnect.Utils.BatchRequestStatus
{
    internal class BatchRequestStatusUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            new BatchRequestStatusDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
