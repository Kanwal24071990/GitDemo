using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ASN;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetSummary
{
    internal class FleetSummaryUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new FleetSummaryDAL().GetData(out FromDate, out ToDate);
        }
    }
}
