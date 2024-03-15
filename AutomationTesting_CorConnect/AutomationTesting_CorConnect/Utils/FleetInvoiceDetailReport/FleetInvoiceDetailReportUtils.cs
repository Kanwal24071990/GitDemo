using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoiceDetailReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetInvoiceDetailReport
{
    internal class FleetInvoiceDetailReportUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new FleetInvoiceDetailReportDAL().GetData(out FromDate, out ToDate);
        }
    }
}
