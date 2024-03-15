using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.SummaryRemittanceReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.SummaryRemittanceReport
{
    internal class SummaryRemittanceReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new SummaryRemittanceReportDAL().GetData(out FromDate, out ToDate);
        }
    }
}
