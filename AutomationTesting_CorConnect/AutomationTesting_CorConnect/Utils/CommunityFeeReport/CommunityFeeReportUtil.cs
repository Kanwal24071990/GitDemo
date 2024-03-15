using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CommunityFeeReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.CommunityFeeReport
{
    internal class CommunityFeeReportUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new CommunityFeeReportDAL().GetData(out FromDate, out ToDate);
        }

        internal static void GetCurrencyCode(out string currencyCode)
        {

            new CommunityFeeReportDAL().GetCurrencyCode(out currencyCode);
        }
    }
}
