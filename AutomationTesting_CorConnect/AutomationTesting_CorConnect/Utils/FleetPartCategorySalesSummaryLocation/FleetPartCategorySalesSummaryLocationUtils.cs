using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPartCategorySalesSummaryLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetPartCategorySalesSummaryLocation
{
    internal class FleetPartCategorySalesSummaryLocationUtils
    {
        internal static void GetDateData(out string FromDate, out string ToDate)
        {
            new FleetPartCategorySalesSummaryLocationDAL().GetDateData(out FromDate, out ToDate);
        }
    }
}
