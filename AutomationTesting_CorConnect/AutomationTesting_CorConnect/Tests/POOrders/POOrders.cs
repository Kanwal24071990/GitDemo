using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.POOrders;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.POOrders;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;


namespace AutomationTesting_CorConnect.Tests.POOrders
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("PO Orders")]

    internal class POOrders : DriverBuilderClass
    {

        POOrdersPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.POOrders);
            Page = new POOrdersPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22252" })]
        public void TC_22252(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24216" })]
        public void TC_24216(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.POOrders), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.POOrders).ForEach(x => { Assert.Fail(x); });
            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            POOrdersUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                string dealername = Page.GetFirstRowData(TableHeaders.DealerName);
                Page.FilterTable(TableHeaders.DealerName, dealername);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerName, dealername), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22251" })]
        public void TC_22251(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.POOrders), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25885" })]
        public void TC_25885(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.POOrders);
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

