using AutomationTesting_CorConnect.Helper;
using System;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.EntityGroupMaintenance
{
    internal class EntityGroupMaintenanceDAL : BaseDataAccessLayer
    {
        internal string GetGroupName()
        {
            var query = @"Select top  1 g.name as GroupName from group_tb g join lookUp_tb l on g.typeId = l.lookUpId join lookUp_tb k on g.applicableToId = k.lookUpId join user_tb ut on ut.userId = g.userId order by groupid desc";

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;
            }

            return string.Empty;
        }
    }
}
