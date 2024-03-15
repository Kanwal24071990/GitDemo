using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetLookup;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Utils.FleetLookup
{
    internal class FleetLookupUtils
    {
        internal static string GetCorcenticCode()
        {
            return new FleetLookupDAL().GetCorCentricCode();
        }

        internal static string GetSubCommunityAccountCodeforDealer()
        {
            return new FleetLookupDAL().GetSubCommunityAccountCodeforDealer();
        }

        internal static string GetSubCommunityAccountCodeforFleet()
        {
            return new FleetLookupDAL().GetSubCommunityAccountCodeforFleet();
        }

        internal static string GetEntityLevelAccountCodeforDealer()
        {
            return new FleetLookupDAL().GetEntityLevelAccountCodeforDealer();
        }

        internal static string GetEntityLevelAccountCodeforFleet()
        {
            return new FleetLookupDAL().GetEntityLevelAccountCodeforFleet();
        }

        internal static string GetEntityLevelPCValueforFleetEnroll()
        {
            return new FleetLookupDAL().GetEntityLevelPCValueforFleetEnroll();
        }

        internal static List<string> GetEntityLevelPCValueforFleet()
        {
            return new FleetLookupDAL().GetEntityLevelPCValueforFleet();
        }

        internal static List<string> GetEntityLevelPCValueforDealer()
        {
            return new FleetLookupDAL().GetEntityLevelPCValueforDealer();
        }

        internal static string GetEnrollmentPCValue()
        {
            return new FleetLookupDAL().GetEnrollmentPCValue();
        }

        internal static string GetEnrollmentPCValueforDealer()
        {
            return new FleetLookupDAL().GetEnrollmentPCValueforDealer();
        }
    }
}
