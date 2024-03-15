using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FranchiseCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FranchiseCodeManagement
{
    internal class FranchiseCodeManagementUtil
    {
        internal static List<string> GetFranchiseCodes()
        {
            return new FranchiseCodeManagementDAL().GetFranchiseCodes();
        }

        internal static List<string> GetNames()
        {
            return new FranchiseCodeManagementDAL().GetNames();
        }

        internal static List<string> GetVisibleCodes()
        {
            return new FranchiseCodeManagementDAL().GetVisibleCodes();
        }
    }
}
