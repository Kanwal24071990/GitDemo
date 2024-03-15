using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PCardTransactions;

namespace AutomationTesting_CorConnect.Utils.PCardTransactions
{
    internal class PCardTransactionsUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new PCardTransactionsDAL().GetData(out FromDate, out ToDate);
        }
    }
}
