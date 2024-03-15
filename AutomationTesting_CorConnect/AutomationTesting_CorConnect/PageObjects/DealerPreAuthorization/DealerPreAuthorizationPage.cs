using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerPreAuthorization
{
    internal class DealerPreAuthorizationPage : Commons
    {
        internal DealerPreAuthorizationPage(IWebDriver webDriver) : base(webDriver, Pages.DealerPreAuthorization)
        {
        }

        internal void PopulateGrid(string dealerCode, string fleetCode)
        {
            SearchAndSelectValueAfterOpen(FieldNames.Dealer, dealerCode);
            SearchAndSelectValueAfterOpen(FieldNames.Fleet, fleetCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
