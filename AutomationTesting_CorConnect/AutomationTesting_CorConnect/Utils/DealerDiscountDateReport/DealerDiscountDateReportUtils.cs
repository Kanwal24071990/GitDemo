using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerDiscountDateReport;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoiceTransactionLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerDiscountDateReport
{
    internal class DealerDiscountDateReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new DealerDiscountDateReportDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new DealerDiscountDateReportDAL().GetCountByDateRange(dateRange, days);
        }

    }
}


