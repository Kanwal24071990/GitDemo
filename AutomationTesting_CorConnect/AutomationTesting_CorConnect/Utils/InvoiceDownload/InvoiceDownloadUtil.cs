using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceDownload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.InvoiceDownload
{
    internal class InvoiceDownloadUtil
    {

        internal static string GetExportConfigName() { return new InvoiceDownloadDAL().GetExportConfigName(); }
    }
}
