using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.LineItemReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.FleetInvoicePreApprovalReport;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;
using AutomationTesting_CorConnect.Utils.LineItemReport;

namespace AutomationTesting_CorConnect.Tests.LineItemReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Line Item Report")]
    internal class LineItemReport : DriverBuilderClass
    {
        LineItemReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.LineItemReport);
            Page = new LineItemReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20668" })]
        public void TC_20668(string UserType)
        {

            Page.OpenMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            Page.ClickFieldLabel(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCriteriaCompanyName));
            Page.OpenMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCriteriaCompanyName));
            Page.SelectValueMultiSelectFirstRow(FieldNames.DealerCriteriaCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCriteriaCompanyName));

            Page.OpenMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCriteriaCompanyName));
            Page.OpenMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCriteriaCompanyName));
            Page.SelectValueMultiSelectFirstRow(FieldNames.FleetCriteriaCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCriteriaCompanyName));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20667" })]
        public void TC_20667(string UserType)
        {
            Page.OpenDropDown(FieldNames.DateType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));
            Page.OpenDropDown(FieldNames.DateType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));
            Page.SelectValueFirstRow(FieldNames.DateType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));

            Page.OpenDropDownByInputField(FieldNames.DateRange);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.OpenDropDownByInputField(FieldNames.DateRange);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.SelectValueTableDropDown(FieldNames.DateRange, "Current Quarter");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);

            Page.OpenDatePicker(FieldNames.From);
            Page.ClickFieldLabel(FieldNames.From);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.OpenDatePicker(FieldNames.From);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.SelectDate(FieldNames.From);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));

            Page.OpenDatePicker(FieldNames.To);
            Page.ClickFieldLabel(FieldNames.To);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            Page.OpenDatePicker(FieldNames.To);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            Page.SelectDate(FieldNames.To);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));

            Page.ButtonClick(ButtonsAndMessages.Clear);
            Page.AcceptAlert();
            Page.WaitForLoadingMessage();

            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.SelectLoadBookmarkFirstRow();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ClickFieldLabel(FieldNames.LoadBookmark);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24616" })]
        public void TC_24616(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.LineItemReport, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.SaveAsBookmark), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.LineItemReport).ForEach(x => { Assert.Fail(x); });

            if (Page.IsDataExistsInDropdown(FieldNames.LoadBookmark))
            {
                List<string> loadBookmarkHeaders = Page.GetHeaderNamesTableDropDown(FieldNames.LoadBookmark);
                Assert.IsTrue(loadBookmarkHeaders.Contains(TableHeaders.Name), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Name, FieldNames.ContainsLocation));
                Assert.IsTrue(loadBookmarkHeaders.Contains(TableHeaders.Description), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Description, FieldNames.ContainsLocation));
            }

            string[] dropDowns = { "Dealer Criteria Company Name", "Fleet Criteria Company Name" };
            string currentDropDown = null;
            foreach (string dropDown in dropDowns)
            {
                List<string> headerNames = null;
                if (dropDown.Equals("Dealer Criteria Company Name"))
                {
                    currentDropDown = FieldNames.DealerCriteriaCompanyName;
                    headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
                }

                else if (dropDown.Equals("Fleet Criteria Company Name"))
                {
                    currentDropDown = FieldNames.DealerCriteriaCompanyName;
                    headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
                }

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(headerNames.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, currentDropDown));
                });
            }

            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateType, "Program invoice date", "Dealer invoice date", "Fleet statement period start date", "Fleet statement period end date", "Fleet approval date", "System received date Fleet", "Settlement Date"), $"{FieldNames.DateType} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateRange, "Customized date", "Last 7 days", "Current month", "Current Quarter", "YTD", "Last 12 months"), $"{FieldNames.DateRange} DD: " + ErrorMessages.ListElementsMissing);

            Page.SelectValueFirstRow(FieldNames.DateRange);
            Assert.AreEqual(Pages.LineItemReport, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            Page.ButtonClick(ButtonsAndMessages.Clear);
            Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
            Page.AcceptAlert();
            Page.WaitForMsg(ButtonsAndMessages.Loading);

            string dealerCompanyName = LineItemReportUtils.GetDealerCompanyName(UserType);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName, TableHeaders.DisplayName, dealerCompanyName);

            string fleetCompanyName = LineItemReportUtils.GetFleetCompanyName(UserType);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName, TableHeaders.DisplayName, fleetCompanyName);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            LineItemReportUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                string quantity = Page.GetFirstRowData(TableHeaders.Quantity);
                Page.FilterTable(TableHeaders.Quantity, quantity);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Quantity, quantity), ErrorMessages.NoRowAfterFilter);

                Page.FilterTable(TableHeaders.Quantity, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);

                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                Page.FilterTable(TableHeaders.Quantity, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);

                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.Columns, ButtonsAndMessages.SaveLayout));
                Page.ButtonClick(ButtonsAndMessages.SaveLayout);
                Page.WaitForMsg(ButtonsAndMessages.Loading);

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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25892" })]
        public void TC_25892(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.LineItemReport);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, FleetName));
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
