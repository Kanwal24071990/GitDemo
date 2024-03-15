using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.BillingScheduleManagement;

namespace AutomationTesting_CorConnect.Utils.BillingScheduleManagement
{
    internal class BillingScheduleManagementUtils
    {
        internal static void GetFilterData(out string companyName, out string effectiveDate)
        {
            new BillingScheduleManagementDAL().GetFilterData(out companyName, out effectiveDate);
        }

        internal static void GetFilterAccountCode(out string accountCode)
        {
            new BillingScheduleManagementDAL().GetFilterAccountCode(out accountCode);
        }
    }
}
