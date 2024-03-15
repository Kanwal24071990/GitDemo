using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetDueDateReport
{
    internal class FleetDueDateReportPage : Commons
    {
        internal FleetDueDateReportPage(IWebDriver webDriver) : base(webDriver, Pages.FleetDueDateReport)
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
