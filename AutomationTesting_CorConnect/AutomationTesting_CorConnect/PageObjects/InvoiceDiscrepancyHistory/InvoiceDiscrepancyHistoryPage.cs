using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancyHistory
{
    internal class InvoiceDiscrepancyHistoryPage : Commons
    {
        internal InvoiceDiscrepancyHistoryPage(IWebDriver webDriver) : base(webDriver, Pages.InvoiceDiscrepancyHistory)
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

        internal void LoadDataOnGrid(string location ,string fromDate, string toDate)
        {
            SearchAndSelectValue(FieldNames.CompanyName, location);
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

    }
}
