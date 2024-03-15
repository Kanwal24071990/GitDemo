using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.RemittanceReport;

namespace AutomationTesting_CorConnect.Utils.RemittanceReport
{
    internal class RemittanceReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new RemittanceReportDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
