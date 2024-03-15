using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerDiscountDateReport;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerDueDateReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerDueDateReport
{
    internal class DealerDueDateReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new DealerDueDateReportDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new DealerDueDateReportDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
