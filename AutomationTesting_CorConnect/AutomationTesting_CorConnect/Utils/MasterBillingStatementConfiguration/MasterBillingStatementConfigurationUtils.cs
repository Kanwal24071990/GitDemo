using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.MasterBillingStatementConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.MasterBillingStatementConfiguration
{
    internal class MasterBillingStatementConfigurationUtils
    {
        internal static bool DeleteStatementConfiguration(string communityStatementName)
        {
            return new MasterBillingStatementConfigurationDAL().DeleteStatementConfiguration(communityStatementName);
        }
    }
}
