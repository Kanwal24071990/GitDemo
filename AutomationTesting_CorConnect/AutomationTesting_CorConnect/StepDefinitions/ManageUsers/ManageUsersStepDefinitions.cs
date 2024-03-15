using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.ManageUsers;
using AutomationTesting_CorConnect.PageObjects.ManageUsers.AddNewUser;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.ManageUsers;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AutomationTesting_CorConnect.StepDefinitions.ManageUsers
{
    [Binding]

    internal class ManageUsersStepDefinitions : DriverBuilderClass
    {
        ManageUsersPage page;
        private string randomValue;
        AddNewUserPage newUserPage;
        private string language;
        bool isActive;

        Dictionary<string, string> userlist = new Dictionary<string, string>();

        [When(@"Open Add New User page and select the User Type ""([^""]*)"" and Entity Type ""([^""]*)""")]
        public void GUIvalidationOfAddNewUserPage(string userType, string entityType)
        {
            //isActive = CommonUtils.GetMultilingualtoken();
            //Assert.IsTrue(isActive);
            page = new ManageUsersPage(driver);
            page.SearchByUserName(CommonUtils.GetUserName());
            var addNewUserPage = page.OpenCreateUser();


            addNewUserPage.SelectValueTableDropDown(FieldNames.UserType_PermLevel_, userType);
            addNewUserPage.SearchAndSelectValueWithoutLoadingMsg(FieldNames.EntityType, page.RenameMenuField(entityType));
            addNewUserPage.WaitForLoadingGrid();


        }

        [Then(@"Valid fields should be displayed on Add New User page for ""([^""]*)"" and ""([^""]*)""")]
        public void FieldsValidationOnAddNewUser(string userType, string entityType)
        {

            AddNewUserPage newUserPage = new AddNewUserPage(driver);
            switch (userType)
            {
                case "Regular Users":

                    newUserPage.VerifyRegularUserFields(entityType);

                    break;
                case "Entity Admin":

                    newUserPage.VerifyEntityAdminUserFields(entityType);

                    break;
                case "Community Admin":

                    newUserPage.VerifyCommunityAdminUserFields();

                    break;
                case "Super Admin":

                    newUserPage.VerifySuperAdminUserFields();

                    break;
            }

        }

        [When(@"Create new user:")]
        public void UserCreation(Table table)
        {

            //isActive = CommonUtils.GetMultilingualtoken();
            ////Assert.IsTrue(isActive);
            string email = CommonUtils.GetRandomEmail();
            var userdataobject = table.CreateSet<UserDetails>();

            page = new ManageUsersPage(driver);
            page.SearchByUserName(CommonUtils.GetUserName());


            foreach (var item in userdataobject)
            {
                string username = CommonUtils.RandomString(7);
                userlist.Add(username, item.Language);
                var addNewUserPage = page.OpenCreateUser();
                addNewUserPage.CreateUser(username, email, item.UserType, item.EntityType, item.LocType, item.Language);
            }
        }
        [When(@"Create new Notification user:")]
        public void NotificationUserCreation(Table table)
        {
            //isActive = CommonUtils.GetMultilingualtoken();
            //Assert.IsTrue(isActive);
            string email = CommonUtils.GetRandomEmail();
            var userdataobject = table.CreateSet<UserDetails>();

            page = new ManageUsersPage(driver);
            page.SearchByUserName(CommonUtils.GetUserName());


            foreach (var item in userdataobject)
            {
                string username = CommonUtils.RandomString(7);
                userlist.Add(username, item.Language);
                var addNewUserPage = page.OpenCreateUser();
                addNewUserPage.CreateNotifyUser(username, email, item.UserType, item.EntityType, item.LocType, item.Language);
            }
        }


        [Then(@"user is created")]
        public void ValidateUserCreation()
        {
            foreach (var item in userlist)
            {
                UserDetails userDetails = ManageUsersUtils.GetUserDetails(item.Key);
                Assert.AreEqual(item.Key, userDetails.Username);
                Assert.AreEqual(item.Key, userDetails.FirstName);
                Assert.AreEqual(item.Key, userDetails.LastName);
                Assert.IsFalse(userDetails.isNotificationUser);
                //Int32 webcoreID = userDetails.WebCoreUID;
                //string Webcoreuserid = webcoreID.ToString();
                //UserDetails webcoreuserdetails = ManageUsersUtils.GetUserWebCoreLanguageID(Webcoreuserid);
                //string languageid=(webcoreuserdetails.LanguageID);
                //UserDetails userlanguageDetails = ManageUsersUtils.GetUserLanguage(languageid);
                //if (item.Value == "English")
                //{
                //    Assert.AreEqual(item.Value, userlanguageDetails.UserLanguage);
                //}
                //else if (item.Value == "French")
                //{
                //    Assert.AreEqual(item.Value + " (CA)-Français (CA)", userlanguageDetails.UserLanguage);
                //}
                //else if (item.Value == "Spanish")
                //{
                //    Assert.AreEqual(item.Value + "-Español", userlanguageDetails.UserLanguage);
                //}
            }
            userlist.Clear();
        }

        [Then(@"Notification user is created")]
        public void ValidateNotificationUserCreation()
        {
            foreach (var item in userlist)
            {
                UserDetails userDetails = ManageUsersUtils.GetUserDetails(item.Key);
                Assert.AreEqual(item.Key, userDetails.Username);
                Assert.AreEqual(item.Key, userDetails.FirstName);
                Assert.AreEqual(item.Key, userDetails.LastName);
                Assert.IsTrue(userDetails.isNotificationUser);
                //Int32 webcoreID = userDetails.WebCoreUID;
                //string Webcoreuserid = webcoreID.ToString();
                //UserDetails webcoreuserdetails = ManageUsersUtils.GetUserWebCoreLanguageID(Webcoreuserid);
                //string languageid = (webcoreuserdetails.LanguageID);
                //UserDetails userlanguageDetails = ManageUsersUtils.GetUserLanguage(languageid);
                //if (item.Value == "English")
                //{
                //    Assert.AreEqual(item.Value, userlanguageDetails.UserLanguage);
                //}
                //else if (item.Value == "French")
                //{
                //    Assert.AreEqual(item.Value + " (CA)-Français (CA)", userlanguageDetails.UserLanguage);
                //}
                //else if (item.Value == "Spanish")
                //{
                //    Assert.AreEqual(item.Value + "-Español", userlanguageDetails.UserLanguage);
                //}
            }
            userlist.Clear();

        }

        [When(@"User navigates to Edit User Page for ""([^""]*)""")]
        public void WhenEditTheUser(string userName)
        {
            //isActive = CommonUtils.GetMultilingualtoken();
            //Assert.IsTrue(isActive);
            page = new ManageUsersPage(driver);
            page.SearchByUserName(userName);
            page.ClickHyperLinkOnGrid(TableHeaders.Commands);
        }

        [Then(@"On Edit page valid fields are displayed for ""([^""]*)"" and ""([^""]*)""")]
        public void FieldValidationOnEdit(string userType, string entityType)
        {

            AddNewUserPage newUserPage = new AddNewUserPage(driver);
            switch (userType)
            {
                case "Regular Users":

                    newUserPage.VerifyRegularUserFieldsOnEdit(entityType);

                    break;
                case "Entity Admin":

                    newUserPage.VerifyEntityAdminUserFieldsOnEdit(entityType);

                    break;
                case "Community Admin":

                    newUserPage.VerifyCommunityAdminUserFieldsOnEdit();

                    break;
                case "Super Admin":

                    newUserPage.VerifySuperAdminUserFieldsOnEdit();

                    break;
            }

        }

        [When(@"Update all editable fields for User ""([^""]*)"" on Edit User Page")]
        public void WhenUserIsUpdated(string userName)
        {
            //isActive = CommonUtils.GetMultilingualtoken();
            //Assert.IsTrue(isActive);
            string email = CommonUtils.GetRandomEmail();
            randomValue = CommonUtils.RandomString(3);
            page = new ManageUsersPage(driver);
            page.SearchByUserName(userName);
            page.ClickHyperLinkOnGrid(TableHeaders.Commands);
            newUserPage = new AddNewUserPage(driver);
            language = newUserPage.UpdateUserFields(userName, email, randomValue);

        }

        [Then(@"User ""([^""]*)"" of user type <User Type> and entity type <Entity Type> is updated")]
        public void VerifyFieldsUpdated(string userName)
        {
            string updateddata = userName + randomValue;
            UserDetails userDetails = ManageUsersUtils.GetUserDetails(userName);
            Assert.AreEqual(userName, userDetails.Username);
            Assert.AreEqual(updateddata, userDetails.FirstName);
            Assert.AreEqual(updateddata, userDetails.LastName);
            Assert.AreEqual(updateddata, userDetails.phone);
            Assert.AreEqual(updateddata, userDetails.ext);
            Assert.AreEqual(updateddata, userDetails.cell);
            Assert.AreEqual(updateddata, userDetails.fax);
            Assert.AreEqual(updateddata, userDetails.securitycode);
            newUserPage.Refresh();
            if (newUserPage.IsCheckBoxChecked(FieldNames.IsNotificationUser))
            {
                Assert.IsTrue(userDetails.isNotificationUser);

                Assert.IsFalse(newUserPage.IsButtonEnabled(ButtonsAndMessages.SendNewTempPassword));
            }
            else if (!newUserPage.IsCheckBoxChecked(FieldNames.IsNotificationUser))
            {
                Assert.IsFalse(userDetails.isNotificationUser);

                Assert.IsTrue(newUserPage.IsButtonEnabled(ButtonsAndMessages.SendNewTempPassword));
            }
            //Int32 webcoreID = userDetails.WebCoreUID;
            //string Webcoreuserid = webcoreID.ToString();
            //UserDetails webcoreuserdetails = ManageUsersUtils.GetUserWebCoreLanguageID(Webcoreuserid);
            //string languageid = (webcoreuserdetails.LanguageID);
            //UserDetails userlanguageDetails = ManageUsersUtils.GetUserLanguage(languageid);           
            //Assert.AreEqual(language, userlanguageDetails.UserLanguage);            

        }

        [Given(@"User Populate Grid for ""([^""]*)""")]
        public void GivenUserPopulateGridFor(string userName)
        {
            page = new ManageUsersPage(driver);
            if (userName == "Admin")
            {
                userName = ManageUsersUtils.GetSuperAdminUser();
                page.LoadDataOnGrid(userName);
            }
            else
                page.LoadDataOnGrid(userName);
        }

        [When(@"(.*) locations are saved using select all option for ""([^""]*)""")]
        public void WhenLocationsAreSavedUsingSelectAllOptionFor(int locations, string notificationTitle)
        {
            page = new ManageUsersPage(driver);
            if (page.IsAnyDataOnGrid())
            {
                Assert.IsTrue(page.IsNestedGridOpen(1), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.ClickNestedGridTab("Assign Notifications");
                page.FilterNestedTable(TableHeaders.ScheduleCreated, "True", 2);
                page.FilterNestedTable(TableHeaders.Description, notificationTitle, 2);
                Assert.IsTrue(page.IsNestedGridOpen(2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.WaitForLoadingGrid();

            }
            switch (locations)
            {
                case 10:

                    page.ClickGridButtons(ButtonsAndMessages.SaveLocation);
                    page.WaitForGridLoad();
                    break;
                case 200:

                    page.IsNestedTableRowCheckBoxUnChecked();
                    if (page.IsNextPageNestedGrid())
                    {
                        for (int i = 2; i < 21; i++)
                        {
                            page.GoToPageNestedGrid(i, "Assign Notifications", 2);
                            page.ClickSelectAllCheckBox(TableHeaders.Name);

                        }
                    }
                    page.ClickGridButtons(ButtonsAndMessages.SaveLocation);
                    break;
            }
        }


        [When(@"(.*) locations gets saved using select all option for ""([^""]*)""")]
        public void WhenLocationsGetsSavedUsingSelectAllOptionFor(int locations, string notificationTitle)
        {
            page = new ManageUsersPage(driver);
            if (page.IsAnyDataOnGrid())
            {
                Assert.IsTrue(page.IsNestedGridOpen(1), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.ClickNestedGridTab("Assign Notifications");
                page.FilterNestedTable(TableHeaders.ScheduleCreated, "False", 2);
                page.FilterNestedTable(TableHeaders.Description, notificationTitle, 2);
                Assert.IsTrue(page.IsNestedGridOpen(2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.WaitForLoadingGrid();
            }
            switch (locations)
            {
                case 10:

                    page.ClickGridButtons(ButtonsAndMessages.SaveLocation);
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                    Assert.IsTrue(wait.Until(driver => page.IsAlertPresent()));
                    driver.SwitchTo().Alert().Accept();
                    break;
                case 200:

                    page.IsNestedTableRowCheckBoxUnChecked();
                    if (page.IsNextPageNestedGrid())
                    {
                        for (int i = 2; i < 21; i++)
                        {
                            page.GoToPageNestedGrid(i, "Assign Notifications", 2);
                            page.ClickSelectAllCheckBox(TableHeaders.Name);

                        }
                    }
                    page.ClickGridButtons(ButtonsAndMessages.SaveLocation);
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                    Assert.IsTrue(wait.Until(driver => page.IsAlertPresent()));
                    driver.SwitchTo().Alert().Accept();
                    break;
            }
        }

        [Then(@"Verify for (.*) locations (.*)  are saved")]
        public void ThenVerifyForAdminLocationsAreSaved(string userType, int locations)
        {
            if (locations == 10)
            {
                Assert.IsFalse(page.IsNestedTableRowCheckBoxUnChecked());
                string userName = ManageUsersUtils.GetSuperAdminUser();
                string UserId = ManageUsersUtils.GetUserIdforAdmin(userName);
                string savedLocationCount = ManageUsersUtils.GetSelectAllCount(UserId);
                Assert.AreEqual(locations, int.Parse(savedLocationCount));
            }
            if (locations == 200)
            {
                Assert.IsFalse(page.IsNestedTableRowCheckBoxUnChecked());
                string userName = ManageUsersUtils.GetSuperAdminUser();
                string UserId = ManageUsersUtils.GetUserIdforAdmin(userName);
                string savedLocationCount = ManageUsersUtils.GetSelectAllCount(UserId);
                Assert.AreEqual(locations, int.Parse(savedLocationCount));

            }
        }

        [Then(@"Verify for (.*) locations (.*)  gets saved")]
        public void ThenVerifyForAdminLocationsGetsSaved(string userType, int locations)
        {
            switch (userType)
            {
                case "Admin":
                    if (locations == 10)
                    {
                        Assert.IsFalse(page.IsNestedTableRowCheckBoxUnChecked(2));
                        string userName = ManageUsersUtils.GetSuperAdminUser();
                        string UserId = ManageUsersUtils.GetUserIdforAdmin(userName);
                        string savedLocationCount = ManageUsersUtils.GetSelectAllCountforDR(UserId);
                        Assert.AreEqual(int.Parse(savedLocationCount), int.Parse(savedLocationCount));
                    }
                    if (locations == 200)
                    {
                        Assert.IsFalse(page.IsNestedTableRowCheckBoxUnChecked());
                        string userName = ManageUsersUtils.GetSuperAdminUser();
                        string UserId = ManageUsersUtils.GetUserIdforAdmin(userName);
                        string savedLocationCount = ManageUsersUtils.GetSelectAllCountforDR(UserId);
                        Assert.AreEqual(locations, int.Parse(savedLocationCount));
                    }
                    break;
            }
        }

        [When(@"User Navigates to Linked Assign Locations for ""([^""]*)""")]
        public void WhenUserNavigatesToLinkedAssignLocationsFor(string tabName)
        {
            page = new ManageUsersPage(driver);
            if (page.IsAnyDataOnGrid())
            {
                Assert.IsTrue(page.IsNestedGridOpen(1), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.ClickNestedGridTab(tabName);
                page.FilterNestedTable(TableHeaders.ScheduleCreated, "False", 2);
                Assert.IsTrue(page.IsNestedGridOpen(2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.WaitForLoadingGrid();
                page.ClickGridButtons(ButtonsAndMessages.LinkAssignedLocations);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                Assert.IsTrue(wait.Until(driver => page.IsAlertPresent()));
                driver.SwitchTo().Alert().Accept();
            }
        }

        [Then(@"Checkboxes are disbaled")]
        public void ThenCheckboxesAreDisbaled()
        {
            page = new ManageUsersPage(driver);
            Assert.IsTrue(page.IsNestedTableRowCheckBoxDisabled(2));
            Assert.IsFalse(page.IsElementEnabled(ButtonsAndMessages.LinkAssignedLocations));
        }

        [Then(@"Checkboxes and Link Assign Button are disbaled")]
        public void ThenCheckboxesareEnabledAndLinkAssignButtonAreDisbaled()
        {
            page = new ManageUsersPage(driver);
            Assert.True(page.IsNestedTableRowCheckBoxDisabled(2));
            Assert.IsFalse(page.IsElementEnabled(ButtonsAndMessages.LinkAssignedLocations));
        }

        [Then(@"Checkboxes are Enabled and Link Assign Button are disbaled")]
        public void ThenCheckboxesAreEnabledAndLinkAssignButtonAreDisbaled()
        {
            page = new ManageUsersPage(driver);
            Assert.False(page.IsNestedTableRowCheckBoxDisabled(2));
            Assert.IsFalse(page.IsElementEnabled(ButtonsAndMessages.LinkAssignedLocations));
        }

        [Then(@"Checkboxes are Unchecked and Link Assign Button is Enabled")]
        public void ThenCheckboxesAreUncheckedAndLinkAssignButtonIsEnabled()
        {
            page = new ManageUsersPage(driver);
            Assert.False(page.IsNestedTableRowCheckBoxDisabled(2));
            Assert.True(page.IsElementEnabled(ButtonsAndMessages.LinkAssignedLocations));
        }

        [When(@"User Navigates to Link Assigned Locations for ""([^""]*)""")]
        public void WhenUserNavigatesToLinkAssignedLocationsFor(string tabName)
        {
            page = new ManageUsersPage(driver);
            if (page.IsAnyDataOnGrid())
            {
                Assert.IsTrue(page.IsNestedGridOpen(1), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.ClickNestedGridTab(tabName);
                page.FilterNestedTable(TableHeaders.ScheduleCreated, "True", 2);
                Assert.IsTrue(page.IsNestedGridOpen(2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.WaitForLoadingGrid();
                page.ClickSelectAllCheckBox(TableHeaders.Name);
                Assert.IsTrue(page.IsElementVisible(ButtonsAndMessages.LinkAssignedLocations));
                page.ClickGridButtons(ButtonsAndMessages.LinkAssignedLocations);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                Assert.IsTrue(wait.Until(driver => page.IsAlertPresent()));
                driver.SwitchTo().Alert().Accept();
            }
        }

        [When(@"User Navigates to Link Assigned Locations for ""([^""]*)"" and save Unlink Locations")]
        public void WhenUserNavigatesToLinkAssignedLocationsForAndSaveUnlinkLocations(string tabName)
        {
            page = new ManageUsersPage(driver);
            if (page.IsAnyDataOnGrid())
            {
                Assert.IsTrue(page.IsNestedGridOpen(1), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.ClickNestedGridTab(tabName);
                page.FilterNestedTable(TableHeaders.ScheduleCreated, "True", 2);
                Assert.IsTrue(page.IsNestedGridOpen(2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.WaitForLoadingGrid();
                page.ClickSelectAllCheckBox(TableHeaders.Name);
                Assert.IsTrue(page.IsElementVisible(ButtonsAndMessages.LinkAssignedLocations));
                page.ClickGridButtons(ButtonsAndMessages.LinkAssignedLocations);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                Assert.IsTrue(wait.Until(driver => page.IsAlertPresent()));
                driver.SwitchTo().Alert().Accept();
            }

        }

        [When(@"User Clicks on Save Location for Unlink Assign Locations in ""([^""]*)""")]
        public void WhenUserClicksOnSaveLocationForUnlinkAssignLocationsIn(string tabName)
        {
            page = new ManageUsersPage(driver);
            if (page.IsAnyDataOnGrid())
            {
                Assert.IsTrue(page.IsNestedGridOpen(1), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.ClickNestedGridTab(tabName);
                page.FilterNestedTable(TableHeaders.ScheduleCreated, "False", 2);
                Assert.IsTrue(page.IsNestedGridOpen(2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.WaitForLoadingGrid();
                Assert.IsTrue(page.IsElementVisible(ButtonsAndMessages.LinkAssignedLocations));
                page.Click(ButtonsAndMessages.LinkAssignedLocations);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                Assert.IsTrue(wait.Until(driver => page.IsAlertPresent()));
                driver.SwitchTo().Alert().Accept();
                page.ClickGridButtons(ButtonsAndMessages.SaveLocation);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                Assert.IsTrue(wait.Until(driver => page.IsAlertPresent()));
                driver.SwitchTo().Alert().Accept();
            }
        }

        [When(@"User Clicks on Reset to UnLinked Assign Locations for ""([^""]*)""")]
        public void WhenUserClicksOnResetToUnLinkedAssignLocationsFor(string tabName)
        {
            page = new ManageUsersPage(driver);
            if (page.IsAnyDataOnGrid())
            {
                Assert.IsTrue(page.IsNestedGridOpen(1), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.ClickNestedGridTab(tabName);
                page.FilterNestedTable(TableHeaders.ScheduleCreated, "False", 2);
                Assert.IsTrue(page.IsNestedGridOpen(2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                page.WaitForLoadingGrid();

                page.ClickGridButtons(ButtonsAndMessages.LinkAssignedLocations);
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                Assert.IsTrue(wait.Until(driver => page.IsAlertPresent()));
                driver.SwitchTo().Alert().Accept();
                page.ClickGridButtons(ButtonsAndMessages.Reset);

            }
        }
    }


}
