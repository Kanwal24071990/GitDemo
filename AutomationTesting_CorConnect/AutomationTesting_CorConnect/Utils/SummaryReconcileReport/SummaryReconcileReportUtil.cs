using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.SummaryReconcileReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.SummaryReconcileReport
{
    internal class SummaryReconcileReportUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new SummaryReconcileReportDAL().GetData(out FromDate, out ToDate);
        }
    }
}
