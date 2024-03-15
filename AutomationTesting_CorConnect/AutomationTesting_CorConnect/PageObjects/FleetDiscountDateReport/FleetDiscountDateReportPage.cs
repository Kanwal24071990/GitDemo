using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetDiscountDateReport
{
    internal class FleetDiscountDateReportPage : Commons
    {
        internal FleetDiscountDateReportPage(IWebDriver webDriver) : base(webDriver, Pages.FleetDiscountDateReport)
        { }

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
