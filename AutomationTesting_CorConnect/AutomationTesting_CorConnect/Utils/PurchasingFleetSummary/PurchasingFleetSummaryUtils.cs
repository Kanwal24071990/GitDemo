using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PurchasingFleetSummary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.PurchasingFleetSummary
{
    internal class PurchasingFleetSummaryUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new PurchasingFleetSummaryDAL().GetData(out FromDate, out ToDate);
        }

        internal static void GetTotalSales(out string totalSales, string caseType, string dealerCode, string fleetCode, string fromDate, string toDate)
        {
            new PurchasingFleetSummaryDAL().GetTotalSales(out totalSales, caseType, dealerCode, fleetCode, fromDate, toDate);
        }

        internal static void GetTotalInvoiceCount(out string totalInvCount, string dealerCode, string fromDate, string toDate)
        {
            new PurchasingFleetSummaryDAL().GetTotalInvoiceCount(out totalInvCount, dealerCode, fromDate, toDate);
        }
    }
}
