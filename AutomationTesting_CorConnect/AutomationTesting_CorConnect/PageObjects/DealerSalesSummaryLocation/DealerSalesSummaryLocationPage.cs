using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerSalesSummaryLocation
{
    internal class DealerSalesSummaryLocationPage : Commons
    {
        internal DealerSalesSummaryLocationPage(IWebDriver webDriver) : base(webDriver, Pages.DealerSalesSummaryLocation)
        {
        }

        internal void PopulateGrid(string from, string to)
        {
            EnterDateInFromDate(from);
            EnterDateInToDate(to);
            ClickElement(FieldNames.EnableGroupBy);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
