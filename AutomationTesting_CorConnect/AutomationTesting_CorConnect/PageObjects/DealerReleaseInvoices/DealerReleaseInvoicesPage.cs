using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerReleaseInvoices
{
    internal class DealerReleaseInvoicesPage : Commons
    {
        internal DealerReleaseInvoicesPage(IWebDriver webDriver) : base(webDriver, Pages.DealerReleaseInvoices)
        {
        }

        internal void PopulateGrid(string companyName, string fromDate, string toDate)
        {
            SearchAndSelectValue(FieldNames.CompanyName, companyName);
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
