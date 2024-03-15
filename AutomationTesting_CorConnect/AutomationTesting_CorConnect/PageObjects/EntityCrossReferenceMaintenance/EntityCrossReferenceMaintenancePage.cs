using AutomationTesting_CorConnect.PageObjects.EntityCrossReferenceMaintenance.AddNewCrossReferenceEntry;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.PageObjects.EntityCrossReferenceMaintenance
{
    internal class EntityCrossReferenceMaintenancePage : Commons
    {
        internal EntityCrossReferenceMaintenancePage(IWebDriver webDriver) : base(webDriver, Pages.EntityCrossReferenceMaintenance)
        {
        }

        internal void PopulateGrid(string corcentricCode)
        {
            EnterText(FieldNames.AccountCode, corcentricCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal bool ClickAddNewCrossReferenceEntry()
        {
            ButtonClick(ButtonsAndMessages.NewEntry);
            return GetWindowsCount() == 2;
        }

        internal AddNewCrossReferenceEntryPage OpenAddNewCrossReferenceEntry()
        {
            ButtonClick(ButtonsAndMessages.NewEntry);
            SwitchToPopUp();
            return new AddNewCrossReferenceEntryPage(driver);
        }

        internal List<string> Delete(string header, string value)
        {
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            var errosMsgs = new List<string>();
            FilterTable(header, value);
            ClickAnchorbutton(TableHeaders.Commands, ButtonsAndMessages.Delete);

            AcceptAlert(out string alertMsg);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);

            if (alertMsg != ButtonsAndMessages.DeleteAlertMessage)
            {
                errosMsgs.Add(ErrorMessages.AlertMessageMisMatch);
            }

            if (GetFirstRowData("Is Active") != "False")
            {
                errosMsgs.Add(ErrorMessages.DeleteValueNotFalse);
            }

            return errosMsgs;
        }
    }
}
