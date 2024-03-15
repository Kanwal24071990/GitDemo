using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPartSummaryBillTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetPartSummaryBillTo
{
    internal class FleetPartSummaryBillToUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new FleetPartSummaryBillToDAL().GetData(out FromDate, out ToDate);
        }
    }
}
