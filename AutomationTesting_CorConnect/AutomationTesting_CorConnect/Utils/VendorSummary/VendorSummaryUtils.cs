using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ASN;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.VendorSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.VendorSummary
{
    internal class VendorSummaryUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new VendorSummaryDAL().GetData(out FromDate, out ToDate);
        }
    }
}
