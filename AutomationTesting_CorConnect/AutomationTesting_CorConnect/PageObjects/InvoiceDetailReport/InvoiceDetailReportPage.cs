using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceDetailReport
{
    internal class InvoiceDetailReportPage : Commons
    {
        internal InvoiceDetailReportPage(IWebDriver webDriver) : base(webDriver, Pages.InvoiceDetailReport)
        {
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterDateInFromDate(fromDate);
            EnterDateInToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
