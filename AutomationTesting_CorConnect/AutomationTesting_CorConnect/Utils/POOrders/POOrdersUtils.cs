using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.POOrders;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.POTransactionLookup;

namespace AutomationTesting_CorConnect.Utils.POOrders
{
    internal class POOrdersUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new POOrdersDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new POOrdersDAL().GetCountByDateRange(dateRange, days);
        }
    }
}
