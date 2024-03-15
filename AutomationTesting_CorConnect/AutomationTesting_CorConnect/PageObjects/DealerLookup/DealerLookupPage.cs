using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerLookup
{
    internal class DealerLookupPage : Commons
    {
        internal DealerLookupPage(IWebDriver webDriver) : base(webDriver, Pages.DealerLookup)
        {
        }

        public void LoadDataOnGrid(string dealerCode)
        {
            SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealerCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

    }
}
