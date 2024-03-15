using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancyHistory;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceDiscrepancyHistory;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.InvoiceDiscrepancyHistory
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Invoice Discrepancy History")]
    internal class InvoiceDiscrepancyHistory : DriverBuilderClass
    {
        InvoiceDiscrepancyHistoryPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.InvoiceDiscrepancyHistory);
            Page = new InvoiceDiscrepancyHistoryPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20736" })]
        public void TC_20736(string UserType)
        {
            Assert.Multiple(() =>
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
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23311" })]
        public void TC_23311(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.InvoiceDiscrepancyHistory), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.InvoiceDiscrepancyHistory).ForEach(x => { Assert.Fail(x); });

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            InvoiceDiscrepancyHistoryUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                string dealerInv = Page.GetFirstRowData(TableHeaders.DealerInv__spc);
                Page.FilterTable(TableHeaders.DealerInv__spc, dealerInv);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerInv__spc, dealerInv), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerInv__spc, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerInv__spc, CommonUtils.RandomString(10));
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25812" })]
        public void TC_25812(string UserType, string dealerUser, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.InvoiceDiscrepancyHistory);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.InvoiceDiscrepancyHistory);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
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
