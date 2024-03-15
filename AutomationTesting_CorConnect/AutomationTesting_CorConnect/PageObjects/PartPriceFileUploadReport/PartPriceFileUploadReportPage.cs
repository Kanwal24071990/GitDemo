using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.PartPriceFileUploadReport
{
    internal class PartPriceFileUploadReportPage : Commons
    {
        internal PartPriceFileUploadReportPage(IWebDriver webDriver) : base(webDriver, Pages.PartPriceFileUploadReport)
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
