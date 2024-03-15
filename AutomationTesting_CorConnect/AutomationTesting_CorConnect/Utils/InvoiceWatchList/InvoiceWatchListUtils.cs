using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceWatchList;


namespace AutomationTesting_CorConnect.Utils.InvoiceWatchList
{
    internal class InvoiceWatchListUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new InvoiceWatchListDAL().GetData(out FromDate, out ToDate);
        }
    }
}
