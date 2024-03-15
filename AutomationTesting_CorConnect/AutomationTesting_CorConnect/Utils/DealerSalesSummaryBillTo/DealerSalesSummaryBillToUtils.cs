using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerSalesSummaryBillTo;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;

namespace AutomationTesting_CorConnect.Utils.DealerSalesSummaryBillTo
{
    internal class DealerSalesSummaryBillToUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            new DealerSalesSummaryBillToDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
