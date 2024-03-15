using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerStatements
{
    internal class DealerStatementsPage : Commons
    {
        internal DealerStatementsPage(IWebDriver webDriver) : base(webDriver, Pages.DealerStatements)
        {
        }

        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            WaitForElementToBeClickable(GetElement(FieldNames.From));
            ClickElement(FieldNames.EnableGroupBy);
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
