using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetDiscountDateReport;
using AutomationTesting_CorConnect.DataBaseHelper.PODiscrepancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.PODiscrepancy
{
    internal class PODiscrepancyUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new PODiscrepancyDAL().GetData(out FromDate, out ToDate);
        }

        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new PODiscrepancyDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
