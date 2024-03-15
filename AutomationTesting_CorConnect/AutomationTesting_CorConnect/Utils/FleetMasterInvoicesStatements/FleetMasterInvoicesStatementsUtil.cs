using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetMasterInvoicesStatements;


namespace AutomationTesting_CorConnect.Utils.FleetMasterInvoicesStatements
{
    internal class FleetMasterInvoicesStatementsUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new FleetMasterInvoicesStatementsDAL().GetData(out FromDate, out ToDate);
        }
    }
}
