using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DraftStatementReport;
using AutomationTesting_CorConnect.DataObjects;

namespace AutomationTesting_CorConnect.Utils.DraftStatementReport
{
    internal class DraftStatementReportUtil
    {
        internal static void GetData(out string FromDate, out string ToDate)
        {

            new DraftStatementReportDAL().GetData(out FromDate, out ToDate);
        }

        internal static string GetSubCummunityLevelPCEntity()
        {
            return new DraftStatementReportDAL().GetSubCummunityLevelPCEntity();
        }

        internal static LocationDetails GetLocationDetails()
        {
            return new DraftStatementReportDAL().GetLocationDetails();
        }

        internal static LocationDetails GetLocationDetailsforSubcom()
        {
            return new DraftStatementReportDAL().GetLocationDetailsforSubcom();
        }
    }
}
