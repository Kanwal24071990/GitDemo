using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.QlikViewDashboard;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.QlikViewDashboard
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("QlikView Dashboard")]
    internal class QlikViewDashboard : DriverBuilderClass
    {
        QlikViewDashboardPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.QlikViewDashboard, false);
            Page = new QlikViewDashboardPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20252" })]
        public void TC_20252(string UserType)
        {
            Assert.Multiple(() =>
            {
                Page.SwitchToPopUp();
                var errorMsgs = Page.VerifyAlertMessage(ButtonsAndMessages.QlikViewDashboardAlertMsg);
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }
    }
}
