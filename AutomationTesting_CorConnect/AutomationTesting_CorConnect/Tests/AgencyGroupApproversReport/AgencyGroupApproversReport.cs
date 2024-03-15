using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.AgencyGroupApproversReport;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.AgencyGroupApproversReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Agency Group Approvers Report")]
    internal class AgencyGroupApproversReport : DriverBuilderClass
    {
        AgencyGroupApproversReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.AgencyGroupApproversReport);
            Page = new AgencyGroupApproversReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20749" })]
        public void TC_20749(string UserType)
        {
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.ProductGroups);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ProductGroups), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ProductGroups));
            Page.OpenDropDown(FieldNames.ProductGroups);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ProductGroups), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ProductGroups));
            Page.SelectValueFirstRow(FieldNames.ProductGroups);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ProductGroups), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ProductGroups));
        }
    }
}
