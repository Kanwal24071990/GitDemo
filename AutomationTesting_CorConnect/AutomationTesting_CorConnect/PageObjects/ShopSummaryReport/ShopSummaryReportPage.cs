using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ShopSummaryReport
{
    internal class ShopSummaryReportPage : Commons
    {
        internal ShopSummaryReportPage(IWebDriver webDriver) : base(webDriver, Pages.ShopSummaryReport)
        {
        }

        internal void PopulateGrid()
        {
            ClickElement(FieldNames.EnableGroupBy);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
