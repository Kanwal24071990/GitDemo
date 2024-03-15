using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.BillingScheduleManagement;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.BillingScheduleManagement;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;


namespace AutomationTesting_CorConnect.Tests.BillingScheduleManagement
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Billing Schedule Management")]
    internal class BillingScheduleManagement : DriverBuilderClass
    {

        BillingScheduleManagementPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.BillingScheduleManagement);
            Page = new BillingScheduleManagementPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23415" })]
        public void TC_23415(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.BillingScheduleManagement), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.BillingScheduleManagement).ForEach(x=>{ Assert.Fail(x); });

            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            List<string> gridValidatingErrors = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(gridValidatingErrors.Count > 0, ErrorMessages.RightPanelNotEmpty);

            BillingScheduleManagementUtils.GetFilterData(out string companyName, out string effectiveDate);
            if (string.IsNullOrEmpty(companyName))
                Assert.Fail("Company Name returned empty from DB");
            if (string.IsNullOrEmpty(effectiveDate))
                Assert.Fail("Effective Date returned empty from DB");

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.PopulateGrid(companyName, effectiveDate);

            if (Page.IsAnyDataOnGrid())
            {

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateBillingSchedule));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                                               
                string dealer = Page.GetFirstRowData(TableHeaders.Dealer);
                string fleet = Page.GetFirstRowData(TableHeaders.Fleet);
                Page.FilterTable(TableHeaders.Dealer, dealer);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Fleet, fleet), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Fleet, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.Dealer, dealer);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Fleet, fleet), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Fleet, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
            }
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22239" })]
        public void TC_22239(string UserType)
        {
            Page.OpenMultiSelectDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.CompanyName));
            Page.OpenMultiSelectDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueMultiSelectFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.CompanyName);
            BillingScheduleManagementUtils.GetFilterAccountCode(out string accountCode);

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.CompanyName, TableHeaders.AccountCode, accountCode);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.CompanyName);

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.CompanyName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.CompanyName);

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.CompanyName);
            Page.OpenMultiSelectDropDown(FieldNames.CompanyName);

        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22238" })]
        public void TC_22238(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.BillingScheduleManagement), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            Page.OpenDatePicker(FieldNames.EffectiveDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));

            Page.OpenDatePicker(FieldNames.EffectiveDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));

            Page.SelectDate(FieldNames.EffectiveDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));

            Page.OpenDropDown(FieldNames.SearchType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SearchType));

            Page.OpenDropDown(FieldNames.SearchType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SearchType));

            Page.SelectValueFirstRow(FieldNames.SearchType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SearchType));

            string searchType = Page.GetValueDropDown(FieldNames.SearchType).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.SearchType, searchType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SearchType));

        }
    }
}
