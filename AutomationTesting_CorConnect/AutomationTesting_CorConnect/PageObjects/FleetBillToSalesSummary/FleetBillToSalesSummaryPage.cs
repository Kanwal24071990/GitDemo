using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetBillToSalesSummary
{
    internal class FleetBillToSalesSummaryPage : Commons
    {
        internal FleetBillToSalesSummaryPage(IWebDriver webDriver) : base(webDriver, Pages.FleetBillToSalesSummary)
        {
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
