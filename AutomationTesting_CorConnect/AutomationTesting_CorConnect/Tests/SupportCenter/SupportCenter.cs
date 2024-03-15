using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.SupportCenter;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.SupportCenter
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Support Center")]
    internal class SupportCenter : DriverBuilderClass
    {
        SupportCenterPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.SupportCenter);
            Page = new SupportCenterPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20253" })]
        public void TC_20253(string UserType)
        {
            Assert.AreEqual(driver.Url, "https://www.zendesk.com/help-center-closed/?utm_content=corcentrictestzendesk.zendesk.com&utm_source=helpcenter-closed&utm_medium=poweredbyzendesk");
        }
    }
}
