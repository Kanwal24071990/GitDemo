using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPurchaseReport;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartPurchasesReport;
using AutomationTesting_CorConnect.DataBaseHelper.PODiscrepancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerPurchaseReport
{
    internal class DealerPurchaseReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new DealerPurchaseReportDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new DealerPurchaseReportDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
