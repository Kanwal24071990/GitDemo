using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.GrossMarginCreditReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.GrossMarginCreditReport
{
    internal class GrossMarginCreditReportUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new GrossMarginCreditReportDAL().GetData(out FromDate, out ToDate);
        }
    }
}
