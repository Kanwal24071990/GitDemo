using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.POEntry;

namespace AutomationTesting_CorConnect.Utils.POEntry
{
    internal class POEntryUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new POEntryDAL().GetDateData(out fromDate, out toDate);
        }
        internal static int GetPOSectionCount(string transactionNumber)
        {
            return new POEntryDAL().GetPOSectionCount(transactionNumber);
        }
        internal static int GetDisputedPOPOQSectionCount(string transactionNumber,string requestType)
        {
            return new POEntryDAL().GetDisputedPOPOQSectionCount(transactionNumber, requestType);
        }

    }
}
