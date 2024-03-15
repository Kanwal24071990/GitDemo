
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PODiscrepancyHistory;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.POOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.PODiscrepancyHistory
{
    internal class PODiscrepancyHistoryUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new PODiscrepancyHistoryDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new PODiscrepancyHistoryDAL().GetCountByDateRange(dateRange, days);
        }
    }
}
