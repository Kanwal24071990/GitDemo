using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetCreditLimit;


namespace AutomationTesting_CorConnect.Utils.FleetCreditLimit
{
    internal class FleetCreditLimitUtil
    {
        internal static void GetData(out string location)
        {

            new FleetCreditLimitDAL().GetData(out location);
        }
    }
}
