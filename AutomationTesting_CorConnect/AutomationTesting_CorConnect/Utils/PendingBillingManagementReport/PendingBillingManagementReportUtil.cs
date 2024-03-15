using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PendingBillingManagementReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.PendingBillingManagementReport
{
    internal class PendingBillingManagementReportUtil
    {
        internal static void GetData(out string companyName)
        {

            new PendingBillingManagementReportDAL().GetData(out companyName);
        }
    }
}
