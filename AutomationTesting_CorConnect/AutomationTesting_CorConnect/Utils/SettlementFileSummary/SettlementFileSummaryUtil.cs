using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.SettlementFileSummary;


namespace AutomationTesting_CorConnect.Utils.SettlementFileSummary
{
    internal class SettlementFileSummaryUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new SettlementFileSummaryDAL().GetData(out FromDate, out ToDate);
        }
    }
}
