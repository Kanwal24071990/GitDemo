using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoices;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PODiscrepancyHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerInvoicePreApprovalReport
{
    internal class DealerInvoicePreApprovalReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new DealerInvoicePreApprovalReportDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new DealerInvoicePreApprovalReportDAL().GetCountByDateRange(dateRange, days);
        }

       
    }
}
