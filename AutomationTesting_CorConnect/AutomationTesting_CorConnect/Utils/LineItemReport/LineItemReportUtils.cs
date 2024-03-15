using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.IntercommunityInvoiceReport;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.LineItemReport;
using AutomationTesting_CorConnect.DataBaseHelper.PODiscrepancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.LineItemReport
{
    internal class LineItemReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new LineItemReportDAL().GetData(out FromDate, out ToDate);
        }

        internal static string GetDealerCompanyName(string entityType)
        {
            return new LineItemReportDAL().GetDealerCompanyName(entityType);
        }

        internal static string GetFleetCompanyName(string entityType)
        {
            return new LineItemReportDAL().GetFleetCompanyName(entityType);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new LineItemReportDAL().GetCountByDateRange(dateRange, days);
        }
    }
}
