using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceDownload
{
    internal class InvoiceDownloadPage : PopUpPage
    {
        internal InvoiceDownloadPage(IWebDriver driver) : base(driver, Pages.InvoiceDownload)
        {
        }

        internal void SelectExport(string exportName)
        {
            var tr = driver.FindElements(GetElement(FieldNames.AvailableExportTemplates));
            Click(tr.First(x => x.Text.Contains(exportName)));
            WaitForLoadingGrid();
        }

        internal By GetButton()
        {
            return GetElement(ButtonsAndMessages.Execute);
        }

        new internal List<string> VerifyLocationMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName)
        {
            return VerifyLocationMultipleColumnsDropdown(dropdownCaption, locationType, dropdownUserType, userName, headerName: TableHeaders.Name, hasClearButton: true, canLocationExist: true);
        }
    }
}