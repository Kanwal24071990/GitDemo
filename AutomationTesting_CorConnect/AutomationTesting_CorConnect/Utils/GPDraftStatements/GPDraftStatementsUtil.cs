using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DraftStatementReport;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetLookup;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.GPDraftStatements;
using AutomationTesting_CorConnect.DataObjects;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Utils.GPDraftStatements
{
    internal class GPDraftStatementsUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new GPDraftStatementsDAL().GetData(out FromDate, out ToDate);
        }

        internal static string GetSubCummunityLevelPCEntity()
        {
            return new GPDraftStatementsDAL().GetSubCummunityLevelPCEntity();
        }

        internal static LocationDetails GetLocationDetails()
        {
            return new GPDraftStatementsDAL().GetLocationDetails();
        }
        internal static LocationDetails GetLocationDetailsforSubCom()
        {
            return new GPDraftStatementsDAL().GetLocationDetailsforSubCom();
        }
    }
}
