using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetStatements
{
    internal class FleetStatementsPage : Commons
    {
        internal FleetStatementsPage(IWebDriver webDriver) : base(webDriver, Pages.FleetStatements)
        {
        }
        internal void PopulateGrid(string from, string to)
        {
            ClickElement("Enable Group By");
            EnterFromDate(from);
            EnterToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
           
        }

    }
}
