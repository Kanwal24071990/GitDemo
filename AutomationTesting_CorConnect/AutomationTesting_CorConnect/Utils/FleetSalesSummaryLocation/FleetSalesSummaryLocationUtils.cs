using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetSalesSummaryLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetSalesSummaryLocation
{
    internal class FleetSalesSummaryLocationUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new FleetSalesSummaryLocationDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
