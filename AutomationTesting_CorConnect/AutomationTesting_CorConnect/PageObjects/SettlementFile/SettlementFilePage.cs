using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.SettlementFile
{
    internal class SettlementFilePage : Commons
    {
        internal SettlementFilePage(IWebDriver webDriver) : base(webDriver, Pages.SettlementFile)
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
