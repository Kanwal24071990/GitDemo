using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPOPOQTransactionLookup;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.POTransactionLookup;
using AutomationTesting_CorConnect.DataBaseHelper.PODiscrepancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.POTransactionLookup
{
    internal class POTransactionLookupUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new POTransactionLookupDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new POTransactionLookupDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
