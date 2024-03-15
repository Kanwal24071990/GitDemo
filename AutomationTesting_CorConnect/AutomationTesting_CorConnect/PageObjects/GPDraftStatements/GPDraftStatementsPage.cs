using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.GPDraftStatements
{
    internal class GPDraftStatementsPage : Commons
    {
        internal GPDraftStatementsPage(IWebDriver webDriver) : base(webDriver, Pages.GPDraftStatements)
        {
        }

        internal void PopulateGrid(string from, string to)
        {

            EnterDateInFromDate(from);
            EnterDateInToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }

        internal void LoaddataonGrid(string from, string to, string entity)
        {
            
            EnterTextAfterClear(FieldNames.FromDate,from);
            SearchAndSelectValueAfterOpen(FieldNames.CompanyName, entity);
            EnterDateInToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }
    }
}
