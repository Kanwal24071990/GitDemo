using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.GPDraftStatements;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.GPDraftStatements;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.GPDraftStatements
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("GP Draft Statements")]
    internal class GPDraftStatements : DriverBuilderClass
    {
        GPDraftStatementsPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.GPDraftStatements);
            Page = new GPDraftStatementsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20730" })]
        public void TC_20730(string UserType)
        {
            Page.OpenMultiSelectDropDown(FieldNames.StatementType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));
            Page.OpenMultiSelectDropDown(FieldNames.StatementType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));
            Page.SelectValueMultiSelectFirstRow(FieldNames.StatementType);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20729" })]
        public void TC_20729(string UserType)
        {
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDatePicker(FieldNames.FromDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.OpenDatePicker(FieldNames.FromDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.SelectDate(FieldNames.FromDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.OpenDatePicker(FieldNames.ToDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
            Page.OpenDatePicker(FieldNames.ToDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
            Page.SelectDate(FieldNames.ToDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23317" })]
        public void TC_23317(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.GPDraftStatements, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.GPDraftStatements).ForEach(x=>{ Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());


                GPDraftStatementsUtil.GetData(out string from, out string to);
                Page.PopulateGrid(from, to);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, false));                                       

                    string statementId = Page.GetFirstRowData(TableHeaders.StatementID);
                    Page.FilterTable(TableHeaders.StatementID, statementId);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.StatementID, statementId), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.FleetCode, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.StatementID, statementId);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.StatementID, statementId), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.FleetCode, CommonUtils.RandomString(10));
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
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25813" })]
        public void TC_25813(string UserType)
        {
            Page.WaitForAnyElementLocatedBy(FieldNames.CompanyName);
            var errorMsgs = Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
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
