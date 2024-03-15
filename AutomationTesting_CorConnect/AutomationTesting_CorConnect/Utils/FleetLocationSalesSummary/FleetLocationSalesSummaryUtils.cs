using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetLocationSalesSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetLocationSalesSummary
{
    internal class FleetLocationSalesSummaryUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new FleetLocationSalesSummaryDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
