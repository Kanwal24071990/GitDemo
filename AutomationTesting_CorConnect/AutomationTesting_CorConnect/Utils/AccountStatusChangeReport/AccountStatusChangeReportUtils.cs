using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AccountStatusChangeReport;

namespace AutomationTesting_CorConnect.Utils.AccountStatusChangeReport
{
    internal class AccountStatusChangeReportUtils
    {
        internal static void GetDateData(out string fromDate, out string toDate)
        {
            new AccountStatusChangeReportDAL().GetDateData(out fromDate, out toDate);
        }
    }
}
