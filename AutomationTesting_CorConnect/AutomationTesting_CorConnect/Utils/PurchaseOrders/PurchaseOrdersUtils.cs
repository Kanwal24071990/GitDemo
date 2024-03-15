using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.PurchaseOrders
{
    internal class PurchaseOrdersUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new PurchaseOrdersDAL().GetData(out FromDate, out ToDate);
        }
    }
}
