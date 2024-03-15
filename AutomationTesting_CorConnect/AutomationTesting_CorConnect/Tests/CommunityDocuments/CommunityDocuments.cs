using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.CommunityDocuments;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.CommunityDocuments
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Community Documents")]
    internal class CommunityDocuments : DriverBuilderClass
    {
        CommunityDocumentsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.CommunityDocuments, false);
            Page = new CommunityDocumentsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21235" })]
        public void TC_21235(string UserType)
        {
            Page.OpenOnlineSupportCenter();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(2, Page.GetWindowsCount());
                Assert.AreEqual("https://www.zendesk.com/help-center-closed/?utm_content=corcentrictestzendesk.zendesk.com&utm_source=helpcenter-closed&utm_medium=poweredbyzendesk", driver.Url);
            });
        }
    }
}
