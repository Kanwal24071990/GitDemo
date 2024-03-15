using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.SettlementFileSummary
{
    internal class SettlementFileSummaryPage : Commons
    {
        internal SettlementFileSummaryPage(IWebDriver webDriver) : base(webDriver, Pages.SettlementFileSummary)
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
