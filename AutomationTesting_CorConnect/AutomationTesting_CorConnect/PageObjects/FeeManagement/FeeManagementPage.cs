using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FeeManagement
{
    internal class FeeManagementPage : Commons
    {
        internal FeeManagementPage(IWebDriver webDriver) : base(webDriver, Pages.FeeManagement)
        {
        }

        internal void PopulateGrid(string feeName)
        {
            EnterTextAfterClear(FieldNames.DisplayName, feeName);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
