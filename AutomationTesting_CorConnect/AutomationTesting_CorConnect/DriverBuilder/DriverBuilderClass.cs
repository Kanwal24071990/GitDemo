using Allure.Commons;
using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.LoginPage;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AutomationTesting_CorConnect.DataObjects;
using TechTalk.SpecFlow;
using NUnit.Framework.Interfaces;
using AutomationTesting_CorConnect.Constants;

namespace AutomationTesting_CorConnect.DriverBuilder
{
    [TestFixture]
    internal class DriverBuilderClass
    {
        internal static IWebDriver driver;
        protected LoginPage loginPage;
        protected static ApplicationContext applicationContext;
        protected Menu menu;

        public void Initialize()
        {
            applicationContext = ApplicationContext.GetInstance();
            Environment.SetEnvironmentVariable(
                AllureConstants.ALLURE_CONFIG_ENV_VARIABLE,
                Path.Combine(Environment.CurrentDirectory, AllureConstants.CONFIG_FILENAME));
            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        }

        public void BeforeEach()
        {
            var testProperties = TestContext.CurrentContext.Test.Properties;
            var userType = testProperties["Type"]?.First().ToString();
            if (testProperties["execute"]?.First().ToString().ToUpper() == "FAIL")
            {
                Assert.Fail("TestCase Failed due to no data provided");
            }
            else if (testProperties["execute"].First().ToString() == "Fail Due To invalid araguments")
            {
                Assert.Fail("TestCase Failed due to invalid data");
            }
            else if (testProperties["execute"]?.First().ToString().ToUpper() == "NO")
            {
                Assert.Ignore();
            }
            else if (testProperties["execute"]?.First().ToString() == "Screen Not exist")
            {
                Assert.Ignore("Screen Not exist");
            }
            else if (string.IsNullOrEmpty(testProperties["UserName"]?.First().ToString()))
            {
                Assert.Ignore($"User of type [{userType}] not exists in ClientConfiguration file");
            }
            else if ((userType == Constants.UserType.Admin.NameUpperCase && !applicationContext.AdminUser)
                || (userType == Constants.UserType.Dealer.NameUpperCase && !applicationContext.DealerUser)
                || (userType == Constants.UserType.Fleet.NameUpperCase && !applicationContext.FleetUser))
            {
                Assert.Ignore($"Ignoring test case because user of type [{userType}] is set to be ignored in environment properties.");
            }
            else
            {
                LoggingHelper.LogMessage(LoggerMesages.StartingTest);
                CreateDriver();
                menu = new Menu(driver);
                loginPage = new LoginPage(driver);
                loginPage.PerformLogin(testProperties["UserName"]?.First().ToString());
            }
        }

        public void PerformLogin(string userType)
        {
            if ((userType == Constants.UserType.Admin.NameUpperCase && !applicationContext.AdminUser)
                || (userType == Constants.UserType.Dealer.NameUpperCase && !applicationContext.DealerUser)
                || (userType == Constants.UserType.Fleet.NameUpperCase && !applicationContext.FleetUser))
            {
                Assert.Ignore($"Ignoring test case because user of type [{userType}] is set to be ignored in environment properties.");
            }
            else
            {
                var client = applicationContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
                var user = client.Users.First(x => x.Type == userType.ToLower());
                TestCaseData testCaseData = new TestCaseData();
                testCaseData.SetProperty("UserName", user.User);
                testCaseData.SetProperty("Type", userType);
                ScenarioContext.Current.Add("ActionResult", userType);
                ScenarioContext.Current.Add("UserName", user.User);
                LoggingHelper.LogMessage(LoggerMesages.StartingTest);
                menu = new Menu(driver);
                loginPage = new LoginPage(driver);
                loginPage.PerformLogin(user, client.URL);
            }
        }

        public void PerformLoginWithUserName(string userName)
        {
            var client = applicationContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var user = client.Users.First(x => x.User == userName);
            if ((user.Type == Constants.UserType.Admin.NameUpperCase && !applicationContext.AdminUser)
                || (user.Type == Constants.UserType.Dealer.NameUpperCase && !applicationContext.DealerUser)
                || (user.Type == Constants.UserType.Fleet.NameUpperCase && !applicationContext.FleetUser))
            {
                Assert.Ignore($"Ignoring test case because user of type [{user.Type}] is set to be ignored in environment properties.");
            }
            else
            {

                TestCaseData testCaseData = new TestCaseData();
                testCaseData.SetProperty("UserName", user.User);
                testCaseData.SetProperty("Type", user.Type);
                ScenarioContext.Current.Add("ActionResult", user.Type);
                ScenarioContext.Current.Add("UserName", user.User);
                LoggingHelper.LogMessage(LoggerMesages.StartingTest);
                menu = new Menu(driver);
                loginPage = new LoginPage(driver);
                loginPage.PerformLogin(user, client.URL);
            }
        }

        public void OpenURL()
        {
            var client = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            CreateDriver();
            menu = new Menu(driver);
            loginPage = new LoginPage(driver);
            loginPage.OpenLoginPage(client.URL);           
            applicationContext.DriverList.Add(new DataProvider.TestDriverProvider() { TestScenario = ScenarioContext.Current.ScenarioInfo.Title, Driver = driver });

        }


        protected void CreateDriver()
        {
            string browser = ApplicationContext.Browser.ToLower();

            try
            {
                driver = browser switch
                {
                    "chrome" => new ChromeDriver(ChromeOptionsSafeBrowsing()),
                    "firefox" => FireFoxDriver(),
                    "edge" => new EdgeDriver(EdgeDriverService.CreateDefaultService(@".\", @"msedgedriver.exe")),
                    "ie" => new InternetExplorerDriver(IEOptions()),
                    "chromeheadless" => new ChromeDriver(ChromeOptions()),
                    _ => FireFoxDriver(),
                };

                driver.Manage().Window.Maximize();
                applicationContext.DriverList.Add(new DataProvider.TestDriverProvider() { TestScenario = ScenarioContext.Current.ScenarioInfo.Title, Driver = driver });
            }
            catch (Exception e)
            {
            }
        }


        public void Close()
        {
            if (driver != null)
            {
                try
                {
                    if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                    {
                        byte[] content = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
                        AllureLifecycle.Instance.AddAttachment("Screenshot", "image/png", content);
                    }
                    driver.Close();
                }
                catch { }
                finally
                {
                    driver.Quit();
                }
            }
        }

        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //    Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

        //    foreach (var chromeDriverProcess in chromeDriverProcesses)
        //    {
        //        chromeDriverProcess.Kill();
        //    }
        //}

        private InternetExplorerOptions IEOptions()
        {
            return new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                RequireWindowFocus = true,
                EnableNativeEvents = false,
                EnablePersistentHover = true
            };
        }

        private ChromeOptions ChromeOptions()
        {
            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("--window-size=1920,1080");
            chromeOptions.AddArguments("--start-maximized");
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);

            return chromeOptions;
        }
        private ChromeOptions ChromeOptionsSafeBrowsing()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);

            return chromeOptions;
        }

        private FirefoxDriver FireFoxDriver()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            service.Host = "::1";
            return new FirefoxDriver(service);
        }

        protected string GetErrorMessage(string Message, params string[] arguments)
        {
            Message = TestContext.CurrentContext.Test.Name + ": " + Message;
            return CommonUtils.FormatString(Message, arguments);
        }

        private bool IsClientUserTypeExempted(string userType, string screenName)
        {
            var exemptedClients = ApplicationContext.GetInstance().skippedScreens.FirstOrDefault(x => x.ScreenName == screenName)?.Clients;
            if (userType == Constants.UserType.Admin.NameUpperCase)
            {
                return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipAdminUser);
            }
            Client client = exemptedClients.FirstOrDefault(x => x.ClientName.ToLower() == CommonUtils.GetClientLower());
            if (client != null && !client.SkipAdminUser)
            {
                if (userType == Constants.UserType.Fleet.NameUpperCase)
                {
                    return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipFleetUser);
                }
                if (userType == Constants.UserType.Dealer.NameUpperCase)
                {
                    return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipDealerUser);
                }
            }
            return false;
        }
    }
}
