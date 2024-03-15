using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerLocations;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.DealerLocations
{
    internal class DealerLocationsUtil
    {
        internal static void GetData(out string dealerCoder)
        {

            new DealerLocationsDAL().GetData(out dealerCoder);
        }

        internal static List<string> GetActiveUsersListforDealerEntity(int entityID, string subCommunity)
        {
            return new DealerLocationsDAL().GetActiveUsersListforDealerEntity(entityID, subCommunity);
        }

        internal static List<string> GetInActiveUsersListforDealerEntity()
        {
            return new DealerLocationsDAL().GetInActiveUsersListforDealerEntity();
        }

        internal static string GetInActiveCorCentricCodeforDealer()
        {
            return new DealerLocationsDAL().GetInActiveCorCentricCodeforDealer();
        }
    }
}
