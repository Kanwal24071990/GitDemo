using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerSalesSummaryLocation;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerSalesSummaryLocation
{
    internal class DealerSalesSummaryLocationUtil
    {
        internal static void GetData(out string from, out string to)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            new DealerSalesSummaryLocationDAL().GetData(out from, out to);
        }
    }
}
