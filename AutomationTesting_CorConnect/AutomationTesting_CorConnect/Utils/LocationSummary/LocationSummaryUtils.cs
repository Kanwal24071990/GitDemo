using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ASN;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.LocationSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.LocationSummary
{
    internal class LocationSummaryUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new LocationSummaryDAL().GetData(out FromDate, out ToDate);
        }
    }
}
