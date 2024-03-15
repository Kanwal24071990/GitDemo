using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations.CreateNewAuthorization;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.OpenAuthorizations
{
    internal class OpenAuthorizationsPage : Commons
    {
        internal OpenAuthorizationsPage(IWebDriver webDriver) : base(webDriver, Pages.OpenAuthorizations)
        {
        }

        internal CreateNewAuthorizationPopUpPage CreateNewAuthorization()
        {
            ButtonClick(ButtonsAndMessages.CreateAuthorization);
            SwitchToPopUp();
            return new CreateNewAuthorizationPopUpPage(driver);
        }

        internal void PopulateGrid(string fleetCode)
        {
            SearchAndSelectValue(FieldNames.CompanyName, fleetCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            WaitForElementToBeClickable(GetElement(FieldNames.From));
            ClickElement(FieldNames.EnableGroupBy);
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }


    }
}
