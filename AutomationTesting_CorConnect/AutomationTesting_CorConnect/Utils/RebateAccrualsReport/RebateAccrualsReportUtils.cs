using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.RebateAccrualsReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.RebateAccrualsReport
{
    internal class RebateAccrualsReportUtils
    {
        internal static void GetData(out string contractName)
        {
            new RebateAccrualsReportDAL().GetData(out contractName);
        }
    }
}
