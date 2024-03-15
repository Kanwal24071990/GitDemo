using AutomationTesting_CorConnect.PageObjects.ManageUsers.AddNewUser;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManageUsers
{
    internal class ManageUsersPage : Commons
    {
        internal ManageUsersPage(IWebDriver webDriver) : base(webDriver, Pages.ManageUsers)
        {
        }

        internal void SearchByUserName(string userName)
        {
            EnterTextAfterClear(FieldNames.UserName, userName);
            GridLoad();
        }

        internal AddNewUserPage OpenCreateUser()
        {
            WaitForButtonToBePresent(ButtonsAndMessages.CreateUser);
            ButtonClick(ButtonsAndMessages.CreateUser);
            SwitchToPopUp();
            return new AddNewUserPage(driver);
        }

        internal void ImpersonateUser(string userName)
        {
            WaitForElementToBePresent(FieldNames.UserName);
            SearchByUserName(userName);
            WaitForLoadingGrid();
            ClickRadioButton();
            ButtonClick(ButtonsAndMessages.Impersonate);
            WaitForLoginUserLabelToHaveText(userName.ToUpper());
        }
        internal void LoadDataOnGrid()
        {
            
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void ClearGrid()
        {
            ButtonClick(ButtonsAndMessages.Clear);
            AcceptAlert();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }

        internal void LoadDataOnGrid(string userName)
        {
            EnterTextAfterClear(FieldNames.UserName, userName);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

    }
}
