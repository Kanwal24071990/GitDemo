using System;
using System.Collections.Generic;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.BulkActions;
using AutomationTesting_CorConnect.PageObjects.UserGroupSetup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.UserGroupSetup;
using NUnit.Framework;
using OpenQA.Selenium;
using StackExchange.Redis;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions
{
    [Binding]
    internal class UserGroupSetupStepDefinitions : DriverBuilderClass
    {

        UserGroupSetupPage Page;
        BulkActionsPage BAPage;
        private List<string> errorMsg;


        [Given(@"User group - (.*) do not have (.*) function access")]
        public void GivenUserGroupOfUserDoNotHaveFunctionAccess(string usergroup, string functionName)
        {
            errorMsg = new List<string>();
        }

        [When(@"user ""([^""]*)"" navigates to ""([^""]*)"" page under Support Menu")]
        public void WhenUserNavigatesToPageUnderMenu(string user, string pageName)
        {
            try
            {
                errorMsg = new List<string>();
                menu = new Menu(driver);
                menu.WaitForLoadingMessage();
                menu.OpenPopUpPage(pageName);
            }
            catch (WebDriverException ex)
            {
                errorMsg.Add(ErrorMessages.PageCaptionMisMatch);
            }
        }

        [Then(@"(.*) menu item should not visible to ""([^""]*)"" user under ""([^""]*)"" menu")]
        public void ThenMenuItemShouldNotVisibleToUserUnderMenu(string pageName, string admin, string support)
        {
            if (!errorMsg.Contains(ErrorMessages.PageCaptionMisMatch))
            {
                Assert.Fail("Page is still visible.");
            }
        }

        [Given(@"User group - (.*) have (.*) function full access")]
        public void GivenUserGroupOfUserHaveFunctionFullAccess(string usergroup, string functionName)
        {

            var errorMsgs = new List<string>();
            UserGroupSetupUtils.CheckFunctionAccessInUserGroup(usergroup, functionName);

        }

        [Then(@"(.*) pop up page should be loaded for the ""([^""]*)"" user and submission is allowed")]
        public void ThenOnPopUpPageSubmissionIsAllowed(string page, string user)
        {
            BAPage = new BulkActionsPage(driver);

            Assert.Multiple(() =>
            {
                Assert.IsEmpty(BAPage.GetValueOfDropDown(FieldNames.Action), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.IsFalse(BAPage.IsDropDownDisabled(FieldNames.Action), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.Action));
                foreach (var msg in errorMsg)
                {
                    Assert.Fail(GetErrorMessage(msg));
                }
            });
        }

        [Then(@"(.*) pop up page should be loaded for the ""([^""]*)"" user and submission is allowed for (.*)")]
        public void ThenPopUpPageShouldBeLoadedForTheUserAndSubmissionIsAllowed(string page, string user, string functionName)
        {
            BAPage = new BulkActionsPage(driver);
            string dropdownValue = functionName.Replace("Bulk Actions - ", "");         // trim value to check in dropdown

            Assert.IsEmpty(BAPage.GetValueOfDropDown(FieldNames.Action), GetErrorMessage(ErrorMessages.FieldNotEmpty));
            BAPage.SelectAction(dropdownValue);

            if (dropdownValue.Contains("Resend"))
            {
                BAPage.SelectDistributionMethod("Email");
            }

            Assert.Multiple(() =>
            {
                Assert.IsTrue(BAPage.IsButtonEnabled(ButtonsAndMessages.Submit),
                                    string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Submit));
                if (BAPage.IsButtonVisible(ButtonsAndMessages.Upload))
                {
                    Assert.IsTrue(BAPage.IsButtonEnabled(ButtonsAndMessages.Upload),
                                    string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Upload));
                }
            });
        }
        [Given(@"User group - (.*) have (.*) function readonly access")]
        public void GivenUserGroupOfUserHaveFunctionReadonlyAccess(string user, string functionName)
        {
            var errorMsgs = new List<string>();
        }

        [Then(@"(.*) pop up page should be loaded for ""([^""]*)"" user with submission disabled for (.*)")]
        public void ThenPopUpPageShouldBeLoadedForUserWithSubmissionDisabledFor(string page, string user, string functionName)
        {
            BAPage = new BulkActionsPage(driver);
            string dropdownValue = functionName.Replace("Bulk Actions - ", "");

            Assert.IsEmpty(BAPage.GetValueOfDropDown(FieldNames.Action), GetErrorMessage(ErrorMessages.FieldNotEmpty));
            BAPage.SelectAction(dropdownValue);

            if (dropdownValue.Contains("Resend"))
            {
                Assert.IsTrue(BAPage.IsDropDownDisabled(FieldNames.DistributionMethod), string.Format(ErrorMessages.DropDownEnabled, FieldNames.DistributionMethod));
            }

            Assert.Multiple(() =>
            {
                Assert.IsFalse(BAPage.IsButtonEnabled(ButtonsAndMessages.Submit),
                                    string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Submit));
                if (BAPage.IsButtonVisible(ButtonsAndMessages.Upload))
                {
                    Assert.IsFalse(BAPage.IsButtonEnabled(ButtonsAndMessages.Upload),
                                    string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Upload));
                }
            });
        }


        [Then(@"(.*) pop up page should be loaded for ""([^""]*)"" user with submission disabled")]
        public void ThenPopUpPageShouldBeLoadedForUserWithSubmissionDisabled(string functionName, string user)
        {
            BAPage = new BulkActionsPage(driver);
            Assert.IsTrue(BAPage.IsDropDownDisabled(FieldNames.Action)
                , string.Format(ErrorMessages.DropDownEnabled, FieldNames.Action));
        }

        [Then(@"(.*) pop up page should be loaded for ""([^""]*)"" user without (.*) in the dropdown")]
        public void ThenPopUpPageShouldBeLoadedForUserWithoutBulkActionsInTheDropdown(string page, string user, string functionName)
        {
            BAPage = new BulkActionsPage(driver);
            string dropdownValue = functionName.Replace("Bulk Actions - ", "");

            Assert.IsFalse(BAPage.VerifyValue(FieldNames.Action, dropdownValue), GetErrorMessage(ErrorMessages.IncorrectValue, dropdownValue));

        }


    }
}
