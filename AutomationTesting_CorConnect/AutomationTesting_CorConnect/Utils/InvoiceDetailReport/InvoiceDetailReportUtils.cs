using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceDetailReport;

namespace AutomationTesting_CorConnect.Utils.InvoiceDetailReport
{
    internal class InvoiceDetailReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new InvoiceDetailReportDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
