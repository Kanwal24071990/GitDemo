using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetLocationSalesSummary
{
    internal class FleetLocationSalesSummaryPage : Commons
    {
        internal FleetLocationSalesSummaryPage(IWebDriver webDriver) : base(webDriver, Pages.FleetLocationSalesSummary)
        {

        }

        internal void PopulateGrid(string from, string to)
        {
            EnterFromDate(from);
            EnterToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
