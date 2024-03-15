using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.PartPriceLookup
{
    internal class PartPriceLookupPage : StaticPage
    {
        internal PartPriceLookupPage(IWebDriver driver) : base(driver, Pages.PartPriceLookup)
        { }

        internal void PopulateGrid(string dealerCode, string fleetCode, string partNumber, string date)
        {
            if (!string.IsNullOrEmpty(dealerCode) && !string.IsNullOrEmpty(fleetCode) && !string.IsNullOrEmpty(partNumber))
            {
                EnterTextAfterClear(FieldNames.Date, date);
                SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealerCode);
                WaitForElementToBeClickable(GetElement(FieldNames.FleetCode));
                SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleetCode);
                WaitForElementToBeClickable(GetElement(FieldNames.PartNumber));
                SearchAndSelectValue(FieldNames.PartNumber, partNumber);
                WaitForElementToBeClickable(GetElement(FieldNames.PartNumber));
                ClickSubmit();
                WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
                WaitForPDFButtonToLoad();
            }
        }
    }
}
