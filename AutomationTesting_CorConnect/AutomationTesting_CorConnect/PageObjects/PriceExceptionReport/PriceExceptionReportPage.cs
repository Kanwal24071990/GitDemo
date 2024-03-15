using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.PriceExceptionReport
{
    internal class PriceExceptionReportPage : Commons
    {
        internal PriceExceptionReportPage(IWebDriver webDriver) : base(webDriver, Pages.PriceExceptionReport)
        {
        }
        internal void PopulateGrid(string FromDate, string ToDate)
        {
            EnterDateInFromDate(FromDate);
            EnterDateInToDate(ToDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
