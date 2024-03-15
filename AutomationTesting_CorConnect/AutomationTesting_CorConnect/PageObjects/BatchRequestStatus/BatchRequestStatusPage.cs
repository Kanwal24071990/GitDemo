using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.BatchRequestStatus
{
    internal class BatchRequestStatusPage : Commons
    {
        internal BatchRequestStatusPage(IWebDriver webDriver) : base(webDriver, Pages.BatchRequestStatus)
        {
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
