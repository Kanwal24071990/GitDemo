using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetPartCategorySalesSummaryBillTo
{
    internal class FleetPartCategorySalesSummaryBillToPage : Commons
    {
        internal FleetPartCategorySalesSummaryBillToPage(IWebDriver webDriver) : base(webDriver, Pages.FleetPartCategorySalesSummaryBillTo)
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
