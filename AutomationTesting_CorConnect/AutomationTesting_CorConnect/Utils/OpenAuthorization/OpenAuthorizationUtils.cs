using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.OpenAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.OpenAuthorization
{
    internal class OpenAuthorizationUtils
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {
            new OpenAuthorizationDAL().GetData(out FromDate, out ToDate);
        }
    }
}
