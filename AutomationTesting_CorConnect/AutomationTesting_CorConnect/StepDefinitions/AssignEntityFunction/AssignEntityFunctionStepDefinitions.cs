using System.Collections.Generic;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.AssignEntityFunction;
using AutomationTesting_CorConnect.PageObjects.UserGroupSetup;
using AutomationTesting_CorConnect.Resources;
using NUnit.Framework;
using StackExchange.Redis;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.AssignEntityFunction
{
    [Binding]
    internal class AssignEntityFunctionStepDefinitions : DriverBuilderClass
    {
        AssignEntityFunctionPage Page;
        UserGroupSetupPage UserGroupPage;
        List<string> errorMsgs;

        [Given(@"Usergroup (.*) have (.*) function assgined")]
        public void GivenUserGroupHaveFunctionAssgined(string accessGroup, string functionName)
        {
            Page = new AssignEntityFunctionPage(driver);
            errorMsgs = new List<string>();
            Page.SearchAccessGroupByName(accessGroup);
            Page.IsNestedGridOpen(1);
            Page.FilterNestedTable(TableHeaders.FunctionName, functionName);
            Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.FilterNotWorking);

            if (Page.IsNestedTableRowCheckBoxUnChecked())
            {
                Page.CheckNestedTableRowCheckBoxByIndex(1);
                Page.ClickInputButton("Save Entity Function");
                Page.WaitForMsg(ButtonsAndMessages.Loading);
            }
            errorMsgs.Add(Page.ClickCheckboxAndVerifyStatus(1, true));
        }

        [Then(@"(.*) function should be available for user group (.*) on (.*) page")]
        public void ThenFunctionShouldBeAvailableForUserGroupOnPage(string functionName, string accessGroup, string pageName)
        {
            UserGroupPage.WaitForLoadingMessage();
            UserGroupPage.IsNestedGridOpen();
            UserGroupPage.WaitForLoadingMessage();

            Page.FilterNestedTable(TableHeaders.FunctionName, functionName);
            UserGroupPage.WaitForLoadingMessage();

            Assert.IsTrue(Page.GetRowCountNestedPage() > 0, ErrorMessages.FilterNotWorking);
        }

        [When(@"user (.*) Search (.*) on ""([^""]*)"" page")]
        public void WhenUserSearchOnPage(string username, string accessGroup, string page)
        {
            UserGroupPage = new UserGroupSetupPage(driver);
            menu = new Menu(driver);
            menu.OpenPage(page, false);
            UserGroupPage.WaitForLoadingMessage();
            UserGroupPage.FilterTable(TableHeaders.EntityType, accessGroup);
        }

        [When(@"user ""([^""]*)"" Search for an entity functions in access group")]
        public void WhenUserSearchEntityFunctionsInAccessGroup(string admin)
        {
            Page = new AssignEntityFunctionPage(driver);
            errorMsgs = new List<string>();

            errorMsgs.AddRange(Page.SearchAllFunctions());
        }

        [Then(@"Verify that entity functions is visible in the relevant accesss group")]
        public void ThenValidateAllEntityFunctionsShouldBeVisibleInTheirRelevantAccesssGroup()
        {
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                }
            });
        }
    }
}



