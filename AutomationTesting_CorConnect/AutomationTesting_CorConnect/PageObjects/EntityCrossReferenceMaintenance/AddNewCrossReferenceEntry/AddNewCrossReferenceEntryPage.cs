using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.EntityCrossReferenceMaintenance.AddNewCrossReferenceEntry
{
    internal class AddNewCrossReferenceEntryPage : PopUpPage
    {
        internal AddNewCrossReferenceEntryPage(IWebDriver driver) : base(driver, Pages.AddNewCrossReferenceEntry)
        {
        }

        internal void AddNewCrossRefrence(string corcentricCode, string type, out string crossRefCode, out bool isCorrectDropdown)
        {
            SelectValueTableDropDown(FieldNames.EntityType, RenameMenuField(type));
            WaitForLoadingGrid();
            SelectValueTableDropDown(FieldNames.CrossReferenceType);
            WaitForLoadingGrid();
            InsertTimeStamp(FieldNames.CrossReferenceCode, out crossRefCode);

            if (type != RenameMenuField("Fleet"))
            {
                InsertTimeStamp(FieldNames.SourceBillTo);
            }

            isCorrectDropdown = ValidateDropDown(FieldNames.CorcentricCode);
            SearchAndSelectValue(FieldNames.CorcentricCode, corcentricCode);
            ClickAdd();
            WaitForLoadingGrid();
        }

        internal void ClickAdd()
        {
            Click(ButtonsAndMessages.ADD);
        }

        internal void ClickClose()
        {
            Click(ButtonsAndMessages.Close);
            SwitchToMainWindow();
        }
    }
}
