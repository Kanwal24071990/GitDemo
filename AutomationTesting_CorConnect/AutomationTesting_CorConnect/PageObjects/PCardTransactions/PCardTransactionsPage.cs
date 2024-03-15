using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.PCardTransactions
{
    internal class PCardTransactionsPage : Commons
    {
        internal PCardTransactionsPage(IWebDriver webDriver) : base(webDriver, Pages.P_CardTransactions)
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
