using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartSalesByShopReport;


namespace AutomationTesting_CorConnect.Utils.PartSalesByShopReport
{
    internal class PartSalesByShopReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new PartSalesByShopReportDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
