using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerLocations
{
    internal class DealerLocationsPage : Commons
    {
        internal DealerLocationsPage(IWebDriver webDriver) : base(webDriver, Pages.DealerLocations)
        {
        }

        public void LoadDataOnGrid(string dealerCoder)
        {
            SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealerCoder);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        public void LoadDataOnGrid()
        {
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
