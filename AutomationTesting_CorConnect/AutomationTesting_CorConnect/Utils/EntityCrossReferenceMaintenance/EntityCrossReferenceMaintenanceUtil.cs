using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.EntityCrossReferenceMaintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.EntityCrossReferenceMaintenance
{
    internal class EntityCrossReferenceMaintenanceUtil
    {
        internal static void GetCorCentricCode(out string CorCentricCode, out string type)
        {
            new EntityCrossReferenceMaintenanceDAL().GetCorCentricCode(out  CorCentricCode, out  type);
        }
    }
}
