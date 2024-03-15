using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPartsCrossReference;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetPartsCrossReference
{
    internal class FleetPartsCrossReferenceUtil
    {
        internal static void GetData(out string fleetCode)
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            new FleetPartsCrossReferenceDAL().GetData(out fleetCode);
        }

        internal static void GetCommunityPartNumber(string fleetDisplayName, out string communityPartNumber)
        {
            new FleetPartsCrossReferenceDAL().GetCommunityPartNumber(fleetDisplayName, out communityPartNumber);
        }
    }
}
