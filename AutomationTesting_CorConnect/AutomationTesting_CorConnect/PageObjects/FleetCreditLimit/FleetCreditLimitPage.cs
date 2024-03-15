using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.FleetCreditLimit
{
    internal class FleetCreditLimitPage : Commons
    {
        internal FleetCreditLimitPage(IWebDriver webDriver) : base(webDriver, Pages.FleetCreditLimit)
        {
        }

        internal void PopulateGrid(string location)
        {
            SearchAndSelectValue(FieldNames.Location, location);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForFleetCreditGridLoad();

        }
    }
}
