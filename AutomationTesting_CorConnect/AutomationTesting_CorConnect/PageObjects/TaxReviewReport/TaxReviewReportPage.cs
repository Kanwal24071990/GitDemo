using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.TaxReviewReport
{
    internal class TaxReviewReportPage : Commons
    {
        internal TaxReviewReportPage(IWebDriver webDriver) : base(webDriver, Pages.TaxReviewReport)
        {
        }

        internal void PopulateGrid(string FromDate, string ToDate)
        {
            EnterFromDate(FromDate);
            EnterToDate(ToDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
