using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetMasterInvoicesStatements;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetMasterInvoicesStatements;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.FleetMasterInvoicesStatements
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Master Invoices/Statements")]
    internal class FleetMasterInvoicesStatements : DriverBuilderClass
    {
        FleetMasterInvoicesStatementsPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetMasterInvoicesStatements);
            Page = new FleetMasterInvoicesStatementsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20727" })]
        public void TC_20727(string UserType)
        {
            Page.OpenMultiSelectDropDown(FieldNames.CompanyName);
            Page.ClickFieldLabel(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenMultiSelectDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueMultiSelectFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenMultiSelectDropDown(FieldNames.StatementType);
            Page.ClickFieldLabel(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));
            Page.OpenMultiSelectDropDown(FieldNames.StatementType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));
            Page.SelectValueMultiSelectFirstRow(FieldNames.StatementType);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20726" })]
        public void TC_20726(string UserType)
        {
            Page.OpenDatePicker(FieldNames.From);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.OpenDatePicker(FieldNames.From);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.SelectDate(FieldNames.From);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.OpenDatePicker(FieldNames.To);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            Page.OpenDatePicker(FieldNames.To);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            Page.SelectDate(FieldNames.To);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23316" })]
        public void TC_23316(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.FleetMasterInvoicesStatements), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.FleetMasterInvoicesStatements).ForEach(x=>{ Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                FleetMasterInvoicesStatementsUtil.GetData(out string from, out string to);

                Page.PopulateGrid(from, to);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                                       

                    string billCode = Page.GetFirstRowData(TableHeaders.FleetBillCode);
                    Page.FilterTable(TableHeaders.FleetBillCode, billCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetBillCode, billCode), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.FleetBillName, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.FleetBillCode, billCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetBillCode, billCode), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.FleetBillName, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ResetNotWorking);
                }
                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25810" })]
        public void TC_25810(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null);
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetMasterInvoicesStatements);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
        }
    }
}
