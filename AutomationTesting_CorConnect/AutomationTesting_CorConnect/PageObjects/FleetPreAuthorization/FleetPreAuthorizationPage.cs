using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetPreAuthorization
{
    internal class FleetPreAuthorizationPage : Commons
    {
        public FleetPreAuthorizationPage(IWebDriver webDriver) : base(webDriver, Pages.FleetPreAuthorization)
        {
        }

        internal void PopulateGrid( string dealerCode,  string fleetCode)
        {
            SearchAndSelectValueAfterOpen(Constants.EntityType.Dealer, dealerCode);
            SearchAndSelectValueAfterOpen(Constants.EntityType.Fleet, fleetCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
