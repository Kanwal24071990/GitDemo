using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.LineItemReport;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartPurchasesReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.PartPurchasesReport
{
    internal class PartPurchasesReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new PartPurchasesReportDAL().GetData(out FromDate, out ToDate);
        }

        internal static string GetDealerCompanyName(string entityType)
        {
            return new PartPurchasesReportDAL().GetDealerCompanyName(entityType);
        }

        internal static string GetFleetCompanyName(string entityType)
        {
            return new PartPurchasesReportDAL().GetFleetCompanyName(entityType);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new PartPurchasesReportDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
