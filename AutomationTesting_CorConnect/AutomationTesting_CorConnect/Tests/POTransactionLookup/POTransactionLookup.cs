using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.POTransactionLookup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.ManageDisputes;
using AutomationTesting_CorConnect.Utils.PODiscrepancy;
using AutomationTesting_CorConnect.Utils.POTransactionLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.POTransactionLookup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("PO Transaction Lookup")]

    internal class POTransactionLookup : DriverBuilderClass
    {
        POTransactionLookupPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.POTransactionLookup);
            Page = new POTransactionLookupPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22254" })]
        public void TC_22254(string UserType)
        {
            string fleet = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string dealer = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);

            Page.OpenMultiSelectDropDown(FieldNames.DealerCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));

            Page.OpenMultiSelectDropDown(FieldNames.DealerCompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.DealerCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCompanyName, TableHeaders.AccountCode, dealer);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.DealerCompanyName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));


            Page.OpenMultiSelectDropDown(FieldNames.FleetCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));

            Page.OpenMultiSelectDropDown(FieldNames.FleetCompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.FleetCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCompanyName, TableHeaders.AccountCode, fleet);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.FleetCompanyName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24217" })]
        public void TC_24217(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.POTransactionLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.POTransactionLookup).ForEach(x => { Assert.Fail(x); });

            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            POTransactionLookupUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                string dealerName = Page.GetFirstRowData(TableHeaders.DealerName);
                Page.FilterTable(TableHeaders.DealerName, dealerName);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerName, dealerName), ErrorMessages.NoRowAfterFilter);

                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);

                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);

                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22253" })]
        public void TC_22253(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.POTransactionLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

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

            Page.OpenDropDown(FieldNames.DirectBillCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DirectBillCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DirectBillCode));

            Page.OpenDropDown(FieldNames.DirectBillCode);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DirectBillCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DirectBillCode));

            Page.SelectValueFirstRow(FieldNames.DirectBillCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DirectBillCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DirectBillCode));

            string directBillCode = Page.GetValueDropDown(FieldNames.DirectBillCode).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.DirectBillCode, directBillCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DirectBillCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DirectBillCode));

            Page.OpenDropDown(FieldNames.DateType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));

            Page.OpenDropDown(FieldNames.DateType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));

            Page.SelectValueFirstRow(FieldNames.DateType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));

            string dateType = Page.GetValueDropDown(FieldNames.DateType).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.DateType, dateType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));

            Page.OpenDropDown(FieldNames.DateRange);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));

            Page.OpenDropDown(FieldNames.DateRange);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));

            Page.SelectValueFirstRow(FieldNames.DateRange);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));

            string dateRange = Page.GetValueDropDown(FieldNames.DateRange).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.DateRange, dateRange);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));

        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24402" })]
        public void TC_24402(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.POTransactionLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DirectBillCode), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DateType), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.From), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.To), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.TransactionID), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.OrderNumber), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.FieldNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.FieldNotDisplayed));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-365)), CommonUtils.GetCurrentDate());
            if (Page.IsAnyDataOnGrid())
            {
               
                string purchaseOrder = Page.GetFirstRowData(TableHeaders.PurchaseOrder);
                Page.FilterTable(TableHeaders.PurchaseOrder, purchaseOrder);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.PurchaseOrder, purchaseOrder), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.PurchaseOrder, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                Page.FilterTable(TableHeaders.PurchaseOrder, purchaseOrder);
                errorMsgs = Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter);
                errorMsgs.AddRange(Page.ValidateTableDetails(true, false));

            }
            if (Page.IsNextPage())
            {
                Page.GoToPage(2);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25888" })]
        public void TC_25888(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.POTransactionLookup);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, FleetName));
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
