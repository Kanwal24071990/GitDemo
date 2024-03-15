using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceDownloadSetup;

namespace AutomationTesting_CorConnect.Utils.InvoiceDownloadSetup
{
    internal class InvoiceDownloadSetupUtil
    {
        internal static bool GetInvoice(string exportName)
        {
            return new InvoiceDownloadSetupDAL().GetInvoice(exportName);
        }
    }
}
