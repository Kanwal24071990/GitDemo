using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PriceExceptionReport;

namespace AutomationTesting_CorConnect.Utils.PriceExceptionReport
{
    internal class PriceExceptionReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new PriceExceptionReportDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
