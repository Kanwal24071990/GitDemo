using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartPriceLookup;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.PartPriceLookup
{
    internal class PartPriceLookupUtil
    {
        internal static string GetDealerCode()
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            return new PartPriceLookupDAL().GetDealerCode();
        }

        internal static string GetFleetCode()
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            return new PartPriceLookupDAL().GetFleetCode();
        }

        internal static string GetPartNumber()
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            return new PartPriceLookupDAL().GetPartNumber();
        }
    }
}
