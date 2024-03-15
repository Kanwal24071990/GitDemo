using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.DealerPartSummaryFleetLocation
{
    internal class DealerPartSummaryFleetLocationPage : Commons
    {
        internal DealerPartSummaryFleetLocationPage(IWebDriver webDriver) : base(webDriver, Pages.DealerPartSummaryFleetLocation)
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
