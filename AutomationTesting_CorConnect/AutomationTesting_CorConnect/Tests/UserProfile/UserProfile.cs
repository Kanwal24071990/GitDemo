using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.UserProfile;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.User;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.UserProfile
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("User Profile")]
    internal class UserProfile : DriverBuilderClass
    {
        UserProfilePage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.UserProfile, false, true);
            Page = new UserProfilePage(driver);
        }


        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20251" })]
        public void TC_20251(string UserType)
        {
            UserUtils.GetUserDetails(UserUtils.GetUserName(), out string firstName, out string lastName, out string email, out string cell);
            CommonUtils.DeactivateStrongPassowordToken();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(Page.GetText(FieldNames.Title), ButtonsAndMessages.EditUser, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.FirstName), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.LastName), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.WorkNo), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.Ext), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.CellNo), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.FaxNo), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.Email), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.CurrentPassword), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.CurrentPassword));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.NewPassword), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.NewPassword));
                Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.ConfirmNewPassword), GetErrorMessage(ErrorMessages.FieldNotDisplayed, FieldNames.ConfirmNewPassword));
                Assert.IsTrue(Page.IsElementDisplayed(ButtonsAndMessages.SaveUser), GetErrorMessage(ErrorMessages.ButtonMissing, ButtonsAndMessages.SaveUser));
                Assert.AreEqual(Page.GetValue(FieldNames.FirstName), firstName, GetErrorMessage(ErrorMessages.IncorrectValue, FieldNames.FirstName));
                Assert.AreEqual(Page.GetValue(FieldNames.LastName), lastName, GetErrorMessage(ErrorMessages.IncorrectValue, FieldNames.LastName));
                Assert.AreEqual(Page.GetValue(FieldNames.Email), email, GetErrorMessage(ErrorMessages.IncorrectValue, FieldNames.Email));
                Assert.AreEqual(Page.GetValue(FieldNames.CellNo), cell, GetErrorMessage(ErrorMessages.IncorrectValue, FieldNames.CellNo));
                Assert.IsTrue(Page.IsElementDisplayed(ButtonsAndMessages.Submit), GetErrorMessage(ErrorMessages.ButtonMissing, ButtonsAndMessages.Submit));

                Page.ChangePassword();
                Assert.IsTrue(Page.IsTextVisible(ButtonsAndMessages.PasswordChangeSuccessfully), GetErrorMessage(ErrorMessages.TextMissing, ButtonsAndMessages.PasswordChangeSuccessfully));

                Page.ClickSaveUser();
                Assert.IsTrue(Page.IsTextVisible(ButtonsAndMessages.UserSavedSuccessfully, true), GetErrorMessage(ErrorMessages.TextMissing, ButtonsAndMessages.UserSavedSuccessfully));

               // CommonUtils.ActivateStrongPassowordToken();
            });
        }
    }
}
