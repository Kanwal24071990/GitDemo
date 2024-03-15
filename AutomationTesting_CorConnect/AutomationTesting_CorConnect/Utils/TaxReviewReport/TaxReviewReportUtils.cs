using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.TaxReviewReport;

namespace AutomationTesting_CorConnect.Utils.TaxReviewReport
{
    internal class TaxReviewReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new TaxReviewReportDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
