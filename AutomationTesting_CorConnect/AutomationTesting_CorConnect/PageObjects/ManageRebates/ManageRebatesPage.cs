using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManageRebates
{
    internal class ManageRebatesPage : Commons
    {
        internal ManageRebatesPage(IWebDriver webDriver) : base(webDriver, Pages.ManageRebates)
        {
        }

        internal void SelectLoadBookmarkFirstRow()
        {
            SelectValueFirstRow(FieldNames.LoadBookmark);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void ResetFields()
        {
            ButtonClick(ButtonsAndMessages.Clear);
            AcceptAlert();
            WaitForLoadingMessage();
        }

    }
}
