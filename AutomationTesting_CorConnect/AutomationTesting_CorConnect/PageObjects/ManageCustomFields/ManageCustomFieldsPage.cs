using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManageCustomFields
{
    internal class ManageCustomFieldsPage : Commons
    {
        internal ManageCustomFieldsPage(IWebDriver webDriver) : base(webDriver, Pages.ManageCustomFields)
        {
        }

        internal void PopulateGrid(string label)
        {
            EnterTextAfterClear(FieldNames.Label, label);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
