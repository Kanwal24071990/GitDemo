using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.SettlementFile;


namespace AutomationTesting_CorConnect.Utils.SettlementFile
{
    internal class SettlementFileUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new SettlementFileDAL().GetData(out FromDate, out ToDate);
        }
    }
}
