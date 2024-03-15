using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.LoginPage;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.Login
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Login")]
    internal class Login : DriverBuilderClass
    {
        internal ApplicationContext appContext;
        
        LoginPage Page;

        [SetUp]
        public void Setup ()
        {
            Page = new LoginPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24810" })]
        public void TC_24810(string UserType)
        {
            var testProperties = TestContext.CurrentContext.Test.Properties;
            
            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());
            
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            driver.Navigate().GoToUrl(client.URL);
            Page = new LoginPage(driver);
            Page.PerformLogin(testProperties["UserName"]?.First().ToString());
            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());

            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            appContext = ApplicationContext.GetInstance();
            ClientConfiguration edgeClient = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            driver.Navigate().GoToUrl(edgeClient.URL);
            Page = new LoginPage(driver);
            Page.PerformLogin(testProperties["UserName"]?.First().ToString());
            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());
            driver.Quit();

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24811" })]
        public void TC_24811(string UserType)
        {
            var testProperties = TestContext.CurrentContext.Test.Properties;

            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());
            
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--incognito");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            driver.Navigate().GoToUrl(client.URL);
            
            Page = new LoginPage(driver);
            Page.PerformLogin(testProperties["UserName"]?.First().ToString());
            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24812" })]
        public void TC_24812(string UserType)
        {
            driver.Quit();

            var testProperties = TestContext.CurrentContext.Test.Properties;
          
            driver = new EdgeDriver(EdgeDriverService.CreateDefaultService(@".\", @"msedgedriver.exe"));
            driver.Manage().Window.Maximize();
            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            driver.Navigate().GoToUrl(client.URL);
            Page = new LoginPage(driver);
            Page.PerformLogin(testProperties["UserName"]?.First().ToString());
            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());

            string[] AllClients = { "pronto", "premierinc", "alliance", "daimleraus", "aurora", "bridgestone", "doublecoin", "hino", "img", "ameriquestcorp" };
            for(int i = 0; i < AllClients.Length; i++)
            {
                driver.SwitchTo().NewWindow(OpenQA.Selenium.WindowType.Tab);

                testProperties = TestContext.CurrentContext.Test.Properties;
                appContext = ApplicationContext.GetInstance();

                ClientConfiguration clientName = appContext.ClientConfigurations.First(x => x.Client.ToLower() == AllClients[i]);
                driver.Navigate().GoToUrl(clientName.URL);

                Page.Login(AllClients[i]);
            }
           
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24815" })]
        public void TC_24815(string UserType)
        {
            var testProperties = TestContext.CurrentContext.Test.Properties;

            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());

            driver.SwitchTo().NewWindow(OpenQA.Selenium.WindowType.Tab);

            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            driver.Navigate().GoToUrl(client.URL);
            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());

        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24813" })]
        public void TC_24813(string UserType)
        {
            var testProperties = TestContext.CurrentContext.Test.Properties;

            Page.WaitForLoginUserLabelToHaveText(testProperties["UserName"]?.First().ToString().ToUpper());

            driver.Manage().Cookies.DeleteAllCookies();

            driver.SwitchTo().NewWindow(OpenQA.Selenium.WindowType.Tab);

            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            driver.Navigate().GoToUrl(client.URL);
            
            Assert.IsTrue(Page.IsElementVisible(FieldNames.UserID));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.Password));
            Assert.IsTrue(Page.IsInputFieldEmpty(FieldNames.UserID));
            Assert.IsTrue(Page.IsInputFieldEmpty(FieldNames.Password));

        }
    }
}
