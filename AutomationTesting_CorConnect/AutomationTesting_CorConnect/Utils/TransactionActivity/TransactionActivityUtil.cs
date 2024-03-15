using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.TransactionActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.TransactionActivity
{
    internal class TransactionActivityUtil
    {
        internal static void GetSellerAndFromDate(out string FromDate, out string SellerName)
        {
            new TransactionActivityDAL().GetSellerAndFromDate(out FromDate, out SellerName);
        }
    }
}
