using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.PODiscrepancy;
using AutomationTesting_CorConnect.PageObjects.PODiscrepancyHistory;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.DealerReleaseInvoices;
using AutomationTesting_CorConnect.Utils.PODiscrepancy;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.PODiscrepancy
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("PO Discrepancy")]
    internal class PODiscrepancy : DriverBuilderClass
    {
        PODiscrepancyPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.PODiscrepancy);
            Page = new PODiscrepancyPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22248" })]
        public void TC_22248(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24214" })]
        public void TC_24214(string UserType)
        {

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.PODiscrepancy), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.PODiscrepancy).ForEach(x => { Assert.Fail(x); });

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            PODiscrepancyUtils.GetData(out string from, out string to);
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

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.MoveToHistory_));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                Page.CheckTableRowCheckBoxByIndex(1);
                Page.MoveToHistory(out string Historymsg1, out string Historymsg2);
                Assert.AreEqual(Historymsg1, ButtonsAndMessages.MoveToHistoryPODiscrepancyAlert);
                Assert.AreEqual(Historymsg2, ButtonsAndMessages.MoveToHistorySuccess);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22247" })]
        public void TC_22247(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.PODiscrepancy), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25898" })]
        public void TC_25898(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.PODiscrepancy);
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
