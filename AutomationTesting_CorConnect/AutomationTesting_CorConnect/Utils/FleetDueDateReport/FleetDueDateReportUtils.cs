using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetDueDateReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetDueDateReport
{
    internal class FleetDueDateReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new FleetDueDateReportDAL().GetDateData(out fromDate, out toDate);
        }
        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new FleetDueDateReportDAL().GetCountByDateRange(dateRange, days);
        }


    }
}
