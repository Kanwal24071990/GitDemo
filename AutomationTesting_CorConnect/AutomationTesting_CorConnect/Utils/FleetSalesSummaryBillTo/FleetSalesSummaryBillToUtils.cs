using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetSalesSummaryBillTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetSalesSummaryBillTo
{
    internal class FleetSalesSummaryBillToUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new FleetSalesSummaryBillToDAL().GetData(out FromDate, out ToDate);
        }
    }
}
