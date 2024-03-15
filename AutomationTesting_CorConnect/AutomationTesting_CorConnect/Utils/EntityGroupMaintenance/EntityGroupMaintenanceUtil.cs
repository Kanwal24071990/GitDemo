using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.EntityGroupMaintenance;

namespace AutomationTesting_CorConnect.Utils.EntityGroupMaintenance
{
    internal class EntityGroupMaintenanceUtil
    {
        internal static string GetGroupName()
        {
            return new EntityGroupMaintenanceDAL().GetGroupName();
        }
    }
}
