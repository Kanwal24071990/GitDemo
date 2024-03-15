using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPOPOQTransactionLookup;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetDiscountDateReport;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Utils.DealerPOPOQTransactionLookup
{
    internal class DealerPOPOQTransactionLookupUtil
    {
        internal static List<DealerPOPOQTransactionLookupObject> GetData(string FromDate, string ToDate)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            return new DealerPOPOQTransactionLookupDAL().GetData(FromDate, ToDate);
        }

        internal static void GetDateData(out string FromDate, out string ToDate)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            new DealerPOPOQTransactionLookupDAL().GetDateData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new DealerPOPOQTransactionLookupDAL().GetCountByDateRange(dateRange, days);
        }


    }
}
