using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoices;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoicePreApprovalReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerInvoices
{
    internal class DealerInvoicesUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new DealerInvoicesDAL().GetData(out FromDate, out ToDate);
        }
        internal static void ActivateUpdateInvoiceOption(string transactionNumber)
        {
            new DealerInvoicesDAL().ActivateUpdateInvoiceOption(transactionNumber);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new DealerInvoicesDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
