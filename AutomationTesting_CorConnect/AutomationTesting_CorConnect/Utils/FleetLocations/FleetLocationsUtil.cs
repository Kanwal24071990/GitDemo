using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerLocations;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetLocations
{
    internal class FleetLocationsUtil
    {
        internal static void GetData(out string fleetCode)
        {

            new FleetLocationsDAL().GetData(out fleetCode);
        }

        internal static string GetInActiveCorCentricCodeforFleet()
        {
          return  new FleetLocationsDAL().GetInActiveCorCentricCodeforFleet();
        }
        internal static List<string> GetInActiveUsersListforFleetEntity()
        {
            return new FleetLocationsDAL().GetInActiveUsersListforFleetEntity();
        }

        internal static List<string> GetActiveUsersListforFleetEntity(int entityID,string subCommunity)
        {
            return new FleetLocationsDAL().GetActiveUsersListforFleetEntity(entityID, subCommunity);
        }
    }
}
