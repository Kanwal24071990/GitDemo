using AutomationTesting_CorConnect.PageObjects.FleetPartsCrossReference.CreatePartsCrossReference;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetPartsCrossReference
{
    internal class FleetPartsCrossReferencePage : Commons
    {
        public FleetPartsCrossReferencePage(IWebDriver webDriver) : base(webDriver, Pages.FleetPartsCrossReference)
        {
        }

        internal void PopolateGrid(string fleetCode)
        {
            SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleetCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void SearchByFleetPartNumber(string fleetPartNumber)
        {
            //SearchAndSelectValueAfterOpen(FieldNames.FleetPartNumber, fleetPartNumber);
            SearchAndSelectValue(FieldNames.FleetPartNumber, fleetPartNumber);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal CreatePartsCrossReferencePage OpenCreatePartsCrossReference()
        {
            ButtonClick(RenameMenuField(ButtonsAndMessages.CreatePartsCrossReference));
            SwitchToPopUp();
            return new CreatePartsCrossReferencePage(driver);
        }

    }
}
