using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.UserGroupSetup
{
    internal class UserGroupSetupPage : Commons
    {
        internal UserGroupSetupPage(IWebDriver webDriver) : base(webDriver, Pages.UserGroupSetup)
        {
        }

        internal string CreateNewUser(out string userGroup, out string userGroupDescription)
        {
            ClickNew();
            userGroup = CommonUtils.RandomString(5);
            Scroll(FieldNames.UserGroup, ButtonsAndMessages.New);
            EnterText(FieldNames.UserGroup, userGroup, ButtonsAndMessages.New);
            userGroupDescription = CommonUtils.RandomString(5);
            EnterText(FieldNames.UserGroupDescription, userGroupDescription, ButtonsAndMessages.New);
            InsertEditGrid();
            var msg = gridHelper.GetEditMsg();
            CloseEditGrid();
            return msg;
        }


        internal string EditUser()
        {
            ClickEdit();
            UpdateEditGrid();
            var msg = gridHelper.GetEditMsg();
            CloseEditGrid();
            return msg;
        }
    }
}
