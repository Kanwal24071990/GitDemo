using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceWatchList
{
    internal class InvoiceWatchListPage : Commons
    {
        internal InvoiceWatchListPage(IWebDriver webDriver) : base(webDriver, Pages.InvoiceWatchList)
        {
        }
        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            EnterStartFromDate(fromDate);
            EnterStartDateTo(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void PopulateGrid()
        {
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void OpenCreateInvoiceWatchListPage()
        {
            ButtonClick(ButtonsAndMessages.AddNewRecord);
            SwitchToPopUp();
        }
    }
}
