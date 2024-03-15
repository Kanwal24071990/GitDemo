using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ManageDisputes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.ManageDisputes
{
    internal class ManageDisputesUtil
    {
        internal static void GetDisputeOwnerDetails(out string FirstName)
        {

            new ManageDisputesDAL().GetDisputeOwnerDetails(out FirstName);
        }
    }
}
