using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetSalesSummaryBillTo
{
    internal class FleetSalesSummaryBillToPage : Commons
    {
        internal FleetSalesSummaryBillToPage(IWebDriver webDriver) : base(webDriver, Pages.FleetSalesSummaryBillTo)
        {
        }

        internal void PopulateGrid(string FromDate, string ToDate)
        {
            EnterFromDate(FromDate);
            EnterToDate(ToDate);
            ClickSearch();
            WaitForMsg("Please wait... we are processing your request.");
            WaitForGridLoad();
        }
    }
}
