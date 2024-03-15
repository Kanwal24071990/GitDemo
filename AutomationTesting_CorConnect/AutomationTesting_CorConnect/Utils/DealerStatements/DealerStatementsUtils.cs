using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerStatements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerStatements
{
    internal class DealerStatementsUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new DealerStatementsDAL().GetData(out FromDate, out ToDate);
        }
    }
}
