using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.GroupRelationshipsMaintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.GroupRelationshipsMaintenance
{
    internal class GroupRelationshipsMaintenanceUtils
    {
        internal static string DeactivateCurrencyCodeReltionship(string groupName)
        {
            return new GroupRelationshipsMaintenanceDAL().DeactivateCurrencyCodeReltionship(groupName);
        }
    }
}