using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.User;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.UserProfile
{
    internal class UserProfilePage : StaticPage
    {
        internal UserProfilePage(IWebDriver driver) : base(driver, Pages.UserProfile)
        {
            SwitchIframe();
        }

        internal void ChangePassword()
        {
            EnterText(FieldNames.CurrentPassword, UserUtils.GetUserPassword());
            EnterText(FieldNames.NewPassword, UserUtils.GetUserPassword());
            EnterText(FieldNames.ConfirmNewPassword, UserUtils.GetUserPassword());
            ClickSubmit();
        }

        internal void ClickSubmit()
        {
            Click(ButtonsAndMessages.Submit);
        }

        internal void ClickSaveUser()
        {
            driver.FindElement(GetElement(ButtonsAndMessages.SaveUser)).Click();
        }
    }
}
