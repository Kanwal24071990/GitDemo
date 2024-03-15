using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetStatements;


namespace AutomationTesting_CorConnect.Utils.FleetStatements
{
    internal class FleetStatementsUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new FleetStatementsDAL().GetData(out FromDate, out ToDate);
        }
    }
}
