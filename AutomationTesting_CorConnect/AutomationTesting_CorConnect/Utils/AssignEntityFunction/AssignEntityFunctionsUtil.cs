using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AssignedEntityFunctions;

namespace AutomationTesting_CorConnect.Utils.AssignEntityFunction
{
    internal class AssignEntityFunctionsUtil
    {
        internal static bool GetCheckboxData(bool isChecked = false)
        {
            return new AssignedEntityFunctionsDAL().GetCheckboxData(isChecked);
        }
    }
}
