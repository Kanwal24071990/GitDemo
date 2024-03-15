using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartsCrossReference;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.PartsCrossReference
{
    internal class PartsCrossReferenceUtil
    {
        internal static string GetCompanyNameCode()
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            return new PartsCrossReferenceDAL().GetCompanyNameCode();
        }
    }
}
