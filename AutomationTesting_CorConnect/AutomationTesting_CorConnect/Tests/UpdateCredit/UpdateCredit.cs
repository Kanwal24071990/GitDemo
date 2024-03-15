using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.UpdateCredit;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.UpdateCredit
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Update Credit")]
    internal class UpdateCredit : DriverBuilderClass
    {
        UpdateCreditPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.UpdateCredit,false,true);
            Page = new UpdateCreditPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22235" })]
        public void TC_22235(string UserType)
       {
            Page.SwitchIframe();
            Page.OpenDropDown(FieldNames.BillingLocation);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.BillingLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BillingLocation));
            string billingLocation = Page.GetValueDropDown(FieldNames.BillingLocation).Trim();
            Page.ClickPageTitle();
            Page.SelectValueFirstRow(FieldNames.BillingLocation, billingLocation);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.BillingLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BillingLocation));
        }
    }
}