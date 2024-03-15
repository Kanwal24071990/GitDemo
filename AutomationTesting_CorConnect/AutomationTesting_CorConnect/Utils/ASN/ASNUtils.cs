using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ASN;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoicePreApprovalReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.ASN
{
    internal class ASNUtils
    {
            internal static void GetData(out string FromDate, out string ToDate)
            {
                new ASNDAL().GetData(out FromDate, out ToDate);
            }
        
    }
}
