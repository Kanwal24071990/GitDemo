using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.AccountMaintenance
{
    internal class AccountMaintenancePage : Commons
    {
        internal AccountMaintenancePage(IWebDriver webDriver) : base(webDriver, Pages.AccountMaintenance)
        {

        }

        internal void LoadDataOnGrid(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            EnterTextAfterClear("Name", Name);
            EnterText("Legal Name", LegalName);
            EnterText("Parent", Parent);
            EnterTextAfterClear("Account Code", CorcentricCode);
            SelectValueTableDropDown("Location Type", LocationType);
            SelectValueTableDropDown("Entity Type", EntityType);

            try
            {
                ButtonClick("Search");
            }
            catch(StaleElementReferenceException)
            {
                ButtonClick("Search");
            }
            WaitForMsg("Please wait... we are processing your request.");
        }

        internal void LoadDataOnGrid(string accountCode, string entityType = "All")
        {
            EnterTextAfterClear(FieldNames.AccountCode, accountCode);
            SelectValueTableDropDown("Entity Type", RenameMenuField(entityType));
            try
            {
                ButtonClick("Search");
            }
            catch (StaleElementReferenceException)
            {
                ButtonClick("Search");
            }
            WaitForMsg("Please wait... we are processing your request.");
        }
    }
}
