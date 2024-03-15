using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.FleetMasterInvoicesStatements
{
    internal class FleetMasterInvoicesStatementsPage : Commons
    {
        internal FleetMasterInvoicesStatementsPage(IWebDriver webDriver) : base(webDriver, Pages.FleetMasterInvoicesStatements)
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
