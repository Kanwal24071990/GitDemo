using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoices;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.IntercommunityInvoiceReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.IntercommunityInvoiceReport
{
    internal class IntercommunityInvoiceReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new IntercommunityInvoiceReportDAL().GetData(out FromDate, out ToDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new IntercommunityInvoiceReportDAL().GetCountByDateRange(dateRange, days);
        }

    }
}
