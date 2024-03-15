using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPOPOQTransactionLookup;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPOPOQTransactionLookup;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetPOPOQTransactionLookup
{
    internal class FleetPOPOQTransactionLookupUtil
    {
        internal static List<DealerPOPOQTransactionLookupObject> GetData(string FromDate, string ToDate)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            return new FleetPOPOQTransactionLookupDAL().GetGridData(FromDate, ToDate);
        }

        internal static void GetData(out string FromDate, out string ToDate)
        {
            new FleetPOPOQTransactionLookupDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new FleetPOPOQTransactionLookupDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
