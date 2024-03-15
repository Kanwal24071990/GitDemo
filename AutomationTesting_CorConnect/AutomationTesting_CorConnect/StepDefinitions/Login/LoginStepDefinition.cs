using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using AutomationTesting_CorConnect.applicationContext;
using OpenQA.Selenium;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.PageObjects.LoginPage;
using OpenQA.Selenium.Support.UI;
using AutomationTesting_CorConnect.PageObjects.ManageUsers;
using AutomationTesting_CorConnect.Utils.ManageUsers;
using NUnit.Framework;
using AutomationTesting_CorConnect.DriverBuilder;

namespace AutomationTesting_CorConnect.StepDefinitions.Login
{
    [Binding]

    internal class LoginStepDefinition : DriverBuilderClass
    {

        LoginPage loginPage;
        ManageUsersPage manageuserpage;
        private string username;

        [When(@"Login with username ""([^""]*)"" and password ""([^""]*)""")]
        public void LoginToApplication(string userName, string password)
        {
            username = userName;
            UserDetails userDetails = ManageUsersUtils.GetUserDetails(userName);

            if (userName == "ActiveNonNotifyUser")
            {
                Assert.IsFalse(userDetails.isNotificationUser);
                Assert.IsTrue(userDetails.isActive);
            }
            else if (userName == "ActiveNotifyUser")
            {
                Assert.IsTrue(userDetails.isNotificationUser);
                Assert.IsTrue(userDetails.isActive);
            }
            else if (userName == "InActNonNotifyUser")
            {
                Assert.IsFalse(userDetails.isNotificationUser);
                Assert.IsFalse(userDetails.isActive);
            }
            else if (userName == "InActNonNotifyUser ")
            {
                Assert.IsTrue(userDetails.isNotificationUser);
                Assert.IsFalse(userDetails.isActive);
            }

            loginPage = new LoginPage(driver);

            loginPage.EnterText(FieldNames.UserID, userName);
            loginPage.EnterText(FieldNames.Password, password);
            loginPage.Click(ButtonsAndMessages.SUBMITALLCAPS);
        }
        [When(@"Impersonate to the user ""([^""]*)""")]
        public void ImpersonateUser(string userName)
        {
            UserDetails userDetails = ManageUsersUtils.GetUserDetails(userName);
            if (userName == "ActiveNonNotifyUser")
            {
                Assert.IsFalse(userDetails.isNotificationUser);
                Assert.IsTrue(userDetails.isActive);
            }
            else if (userName == "ActiveNotifyUser")
            {
                Assert.IsTrue(userDetails.isNotificationUser);
                Assert.IsTrue(userDetails.isActive);
            }
            manageuserpage = new ManageUsersPage(driver);
            manageuserpage.ImpersonateUser(userName);

        }

        [Then(@"Login failed with error ""([^""]*)""")]
        public void LoginUnsuccessful(string error)
        {
            if (error == "Invalid User ID or Password")
            {
                loginPage.CheckForText("Invalid User ID or Password.");
            }

            else if (error == "User setup as notification user and cannot login to the system")
            {
                loginPage.CheckForText("User setup as notification user and cannot login to the system");
            }

            else if (error == "Your account has been suspended")
            {
                loginPage.CheckForText("Your account : " + username + "has been suspended.\r\nPlease use forgot password to reset your password.");
            }

        }







    }
}
