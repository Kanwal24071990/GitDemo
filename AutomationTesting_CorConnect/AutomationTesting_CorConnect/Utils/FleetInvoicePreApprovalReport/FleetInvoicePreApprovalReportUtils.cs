using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoicePreApprovalReport;
using AutomationTesting_CorConnect.DataBaseHelper.PODiscrepancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetInvoicePreApprovalReport
{
    internal class FleetInvoicePreApprovalReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new FleetInvoicePreApprovalReportDAL().GetData(out FromDate, out ToDate);
        }

        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new FleetInvoicePreApprovalReportDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
