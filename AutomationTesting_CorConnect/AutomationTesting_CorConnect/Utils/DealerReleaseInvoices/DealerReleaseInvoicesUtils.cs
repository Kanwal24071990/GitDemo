using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerReleaseInvoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerReleaseInvoices
{
    internal class DealerReleaseInvoicesUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new DealerReleaseInvoicesDAL().GetData(out FromDate, out ToDate);
        }

        internal static void GetData(string dealerName, string fleetName, out string TransactionNumber, out string FromDate, out string ToDate)
        {
            new DealerReleaseInvoicesDAL().GetData(dealerName,fleetName,out TransactionNumber, out FromDate, out ToDate);
        }
    }
}
