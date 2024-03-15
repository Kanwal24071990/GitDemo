using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerLookup
{
    internal class DealerLookupUtil
    {
        internal static string GetCorcenticCode()
        {
            return new DealerLookupDAL().GetCorCentricCode();
        }

        internal static string GetDealerCode()
        {
            return new DealerLookupDAL().GetDealerCode();
        }
        internal static void GetData(out string dealerCode)
        {

            new DealerLookupDAL().GetData(out dealerCode);
        }

    }
}
