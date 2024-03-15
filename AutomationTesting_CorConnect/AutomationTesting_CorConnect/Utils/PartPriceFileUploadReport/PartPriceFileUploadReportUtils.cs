using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartPriceFileUploadReport;

namespace AutomationTesting_CorConnect.Utils.PartPriceFileUploadReport
{
    internal class PartPriceFileUploadReportUtils
    {
        internal static void GetDateData(out string FromDate, out string ToDate)
        {
            new PartPriceFileUploadReportDAL().GetDateData(out FromDate, out ToDate);
        }
    }
}
