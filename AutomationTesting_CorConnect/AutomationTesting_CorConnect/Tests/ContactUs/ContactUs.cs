using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.ContactUs;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.ContactUs;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.ContactUs
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Contact Us")]
    internal class ContactUs : DriverBuilderClass
    {
        ContactUsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ContactUs, false);
            Page = new ContactUsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16376" })]
        public void TC_16376(string UserType)
        {
            var pageDetails = ContactUsUtil.GetPageDetails();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(pageDetails[0].Replace("<br>", "\r\n"), Page.GetText("Call Us"), ErrorMessages.ValueMisMatch);
                Assert.AreEqual("You can reach the Program Support team via email\r\nby using the following email address:", Page.GetText("Email Us"), ErrorMessages.ValueMisMatch);
                Assert.AreEqual(pageDetails[1], Page.GetText("Email Link"), ErrorMessages.EmailFormatNotValid);
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21236" })]
        public void TC_21236(string UserType)
        {
            Page.OpenOnlineSupportCenter();

            Assert.AreEqual(2, Page.GetWindowsCount());
            Assert.AreEqual(driver.Url, "https://www.zendesk.com/help-center-closed/?utm_content=corcentrictestzendesk.zendesk.com&utm_source=helpcenter-closed&utm_medium=poweredbyzendesk");
        }
    }
}
