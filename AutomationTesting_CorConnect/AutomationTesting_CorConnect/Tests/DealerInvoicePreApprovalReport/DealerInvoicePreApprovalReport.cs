using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.Utils.FleetSalesSummaryBillTo;
using AutomationTesting_CorConnect.Utils.PartPriceLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AutomationTesting_CorConnect.Tests.DealerInvoicePreApprovalReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Invoice Pre-Approval Report")]
    internal class DealerInvoicePreApprovalReport : DriverBuilderClass
    {
        DealerInvoicePreApprovalReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerInvoicePreApprovalReport);
            Page = new DealerInvoicePreApprovalReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22159" })]
        public void TC_22159(string UserType)
        {
            string dealerCode = PartPriceLookupUtil.GetDealerCode();
            string fleetCode = PartPriceLookupUtil.GetFleetCode();

            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ClickFieldLabel(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerName, TableHeaders.AccountCode, dealerCode);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerName);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerName);
            Page.SelectAllRowsMultiSelectDropDown(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerName);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));

            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ClickFieldLabel(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetName, TableHeaders.AccountCode, fleetCode);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetName);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetName);
            Page.SelectAllRowsMultiSelectDropDown(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetName);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));


        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22158" })]
        public void TC_22158(string UserType)
        {

            Page.OpenDropDown(FieldNames.DateType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));
            Page.OpenDropDown(FieldNames.DateType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));
            Page.SelectValueFirstRow(FieldNames.DateType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.DateType, "Rejection Date");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));

            Page.OpenDropDown(FieldNames.DateRange);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.OpenDropDown(FieldNames.DateRange);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.SelectValueFirstRow(FieldNames.DateRange);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.DateRange, "Last month");
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));

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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24767" })]
        public void TC_24767(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.DealerInvoicePreApprovalReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.FieldsNotFoundException));

            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ClickFieldLabel(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));

            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));
            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ClickFieldLabel(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));

            Assert.AreEqual(Page.GetValueDropDown(FieldNames.DateType), "Received Date");
            Page.OpenDropDown(FieldNames.DateType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));

            Assert.AreEqual(Page.GetValueDropDown(FieldNames.DateRange), "Customized date");
            Page.OpenDropDown(FieldNames.DateRange);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));

            Page.OpenDatePicker(FieldNames.From);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));


            Page.OpenDatePicker(FieldNames.To);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.DealerInvoicePreApprovalReport).ForEach(x => { Assert.Fail(x); });

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            DealerInvoicePreApprovalReportUtils.GetData(out string FromDate, out string ToDate);
            Page.LoadDataOnGrid(FromDate, ToDate);

            if (Page.IsAnyDataOnGrid())
            {
               
                Page.SetFilterCreiteria(TableHeaders.Dealer, FilterCriteria.Equals);
                string dealerName = Page.GetFirstRowData(TableHeaders.Dealer);
                Page.FilterTable(TableHeaders.Dealer, dealerName);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Dealer, dealerName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Dealer, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.Dealer, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                Page.FilterTable(TableHeaders.Dealer, dealerName);
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                

                if (Page.IsNextPage())
                {
                    Page.GoToPage(2);
                }

            }

            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }
    }
}
