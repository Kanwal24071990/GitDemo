using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetLookup
{
    internal class FleetLookupPage : Commons
    {
        internal FleetLookupPage(IWebDriver webDriver) : base(webDriver, Pages.FleetLookup)
        {
        }

        internal void SelectFleetCode(string corcentriCode)
        {
            SelectValueMultipleColumns(FieldNames.FleetCode, corcentriCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal void PopulateGrid(string fleetCode, string locationType = "All")
        {
            SelectValueMultipleColumns(FieldNames.FleetCode, fleetCode);
            SelectValueTableDropDown(FieldNames.LocationType, locationType);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
