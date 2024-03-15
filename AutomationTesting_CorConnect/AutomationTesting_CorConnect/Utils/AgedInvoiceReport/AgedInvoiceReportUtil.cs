using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AgedInvoiceReport;


namespace AutomationTesting_CorConnect.Utils.AgedInvoiceReport
{
    internal class AgedInvoiceReportUtil
    {
        internal static void GetData(out string location)
        {

            new AgedInvoiceReportDAL().GetData(out location);
        }
    }
}
