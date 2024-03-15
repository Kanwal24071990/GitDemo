using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.AccountStatusChangeReport
{
    internal class AccountStatusChangeReportPage : Commons
    {
        internal AccountStatusChangeReportPage(IWebDriver webDriver) : base(webDriver, Pages.AccountStatusChangeReport)
        {
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterDateInFromDate(fromDate);
            EnterDateInToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
