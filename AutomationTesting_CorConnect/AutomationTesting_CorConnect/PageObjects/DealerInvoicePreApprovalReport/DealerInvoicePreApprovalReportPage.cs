using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerInvoicePreApprovalReport
{
    internal class DealerInvoicePreApprovalReportPage : Commons
    {
        internal DealerInvoicePreApprovalReportPage(IWebDriver webDriver) : base(webDriver, Pages.DealerInvoicePreApprovalReport)
        {
        }
        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg("Please wait... we are processing your request.");
            WaitForGridLoad();
        }
    }
}
