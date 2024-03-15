using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.POQEntry;

namespace AutomationTesting_CorConnect.Utils.POQEntry
{
    internal class POQEntryUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new POQEntryDAL().GetDateData(out fromDate, out toDate);
        }
        internal static int GetPOQSectionCount(string transactionNumber)
        { 
            return new POQEntryDAL().GetPOQSectionCount(transactionNumber);
        }

    }
}
