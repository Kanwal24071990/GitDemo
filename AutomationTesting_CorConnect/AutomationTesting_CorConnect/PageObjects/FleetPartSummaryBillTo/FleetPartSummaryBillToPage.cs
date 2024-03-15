using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetPartSummaryBillTo
{
    internal class FleetPartSummaryBillToPage : Commons
    {
        internal FleetPartSummaryBillToPage(IWebDriver webDriver) : base(webDriver, Pages.FleetPartSummaryBillTo) { }

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
