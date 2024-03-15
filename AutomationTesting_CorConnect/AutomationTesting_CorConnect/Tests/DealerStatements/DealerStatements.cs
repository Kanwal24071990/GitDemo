using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerStatements;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerStatements;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.DealerStatements
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Statements")]
    internal class DealerStatements : DriverBuilderClass
    {
        DealerStatementsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerStatements);
            Page = new DealerStatementsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20720" })]
        public void TC_20720(string UserType)
        {
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDatePicker(FieldNames.From);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.OpenDatePicker(FieldNames.From);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.SelectDate(FieldNames.From);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.OpenDatePicker(FieldNames.To);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            Page.OpenDatePicker(FieldNames.To);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            Page.SelectDate(FieldNames.To);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23586" })]
        public void TC_23586(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.DealerStatements), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Page.AreFieldsAvailable(Pages.DealerStatements).ForEach(x => { Assert.Fail(x); });

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);
            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            DealerStatementsUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));               

                string statementNo = Page.GetFirstRowData(TableHeaders.Statement_);
                Page.FilterTable(TableHeaders.Statement_, statementNo);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Statement_, statementNo), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Statement_, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.Statement_, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));

                errorMsgs.AddRange(Page.ValidateNestedTableHeaders(TableHeaders.DealerCode, TableHeaders.Dealer, TableHeaders.DealerInv__spc, TableHeaders.OriginatingDocumentNumber
                                                                   , TableHeaders.DealerInvTotal, TableHeaders.AdjTotal, TableHeaders.SettledTotal, TableHeaders.Fee, TableHeaders.PaymentAmount
                                                                   , TableHeaders.Currency, TableHeaders.PaymentTermsDescription, TableHeaders.Status, TableHeaders.DlrInvoiceDate, TableHeaders.SettlementDate
                                                                   , TableHeaders.DueDate, TableHeaders.DiscountDueDate, TableHeaders.DiscountAmount, TableHeaders.FleetCode, TableHeaders.Fleet, TableHeaders.po, TableHeaders.ProgramInvoiceNumber));
                errorMsgs.AddRange(Page.ValidateNestedTableDetails(true, true));
                Assert.IsTrue(Page.IsNestedGridClosed(), ErrorMessages.NestedGridNotClosing);
            }
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25807" })]
        public void TC_25807(string UserType, string dealerUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.DealerStatements);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));
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
