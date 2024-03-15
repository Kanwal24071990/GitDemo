using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetPOPOQTransactionLookup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetPOPOQTransactionLookup;
using AutomationTesting_CorConnect.Utils.PODiscrepancy;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Xml.Linq;

namespace AutomationTesting_CorConnect.Tests.FleetPOPOQTransactionLookup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet PO/POQ Transaction Lookup")]
    internal class FleetPOPOQTransactionLookup : DriverBuilderClass
    {
        FleetPOPOQTransactionLookupPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetPOPOQTransactionLookup);
            Page = new FleetPOPOQTransactionLookupPage(driver);
        }

        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20099" })]
        public void TC_20099(string DocumentType, string DateType, string FromDate, string ToDate, string TransactionStatus)
        {
            Page.PopulateGrid(DocumentType, DateType, FromDate, ToDate, TransactionStatus);

            var data = FleetPOPOQTransactionLookupUtil.GetData(FromDate, ToDate);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.GetElementsList(TableHeaders.DocumentType).SearchInList(data.Select(z => z.DocumentType).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.DocumentType));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.TransactionStatus).SearchInList(data.Select(z => z.TransactionStatus).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.TransactionStatus));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.TransactionType).SearchInList(data.Select(z => z.TransactionType).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.TransactionType));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.Update).SearchInList(data.Select(z => z.Update).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.Update));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.Historical).SearchInList(data.Select(z => z.Historical).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.Historical));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.ReceivedDate).SearchInList(data.Select(z => z.ReceivedDate).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.ReceivedDate));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.DocumentNumber).SearchInList(data.Select(z => z.DocumentNumber).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.DocumentNumber));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.DocumentDate).SearchInList(data.Select(z => z.DocumentDate).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.DocumentDate));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.RefPOQ_).SearchInList(data.Select(z => z.RefPOQ).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.RefPOQ_));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.POQDate).SearchInList(data.Select(z => z.POQDate).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.POQDate));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.DealerCode).SearchInList(data.Select(z => z.DealerCode).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.DealerCode));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.Dealer).SearchInList(data.Select(z => z.Dealer).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.Dealer));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.FleetCode).SearchInList(data.Select(z => z.FleetCode).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.FleetCode));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.Fleet).SearchInList(data.Select(z => z.Fleet).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.Fleet));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.Total).SearchInList(data.Select(z => z.TotalString).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.Total));
                Assert.IsTrue(Page.GetElementsList(TableHeaders.Currency).SearchInList(data.Select(z => z.Currency).ToList()), GetErrorMessage(ErrorMessages.DataMisMatch, TableHeaders.Currency));
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20739" })]
        public void TC_20739(string UserType)
        {
            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.SelectLoadBookmarkFirstRow();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));

            Page.OpenDropDown(FieldNames.DocumentType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DocumentType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DocumentType));
            Page.OpenDropDown(FieldNames.DocumentType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DocumentType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DocumentType));
            Page.SelectValueFirstRow(FieldNames.DocumentType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DocumentType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DocumentType));

            Page.OpenDropDown(FieldNames.DealerGroup);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerGroup));
            Page.OpenDropDown(FieldNames.DealerGroup);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerGroup));
            Page.SelectDealerGroupFirstRow();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerGroup));

            Page.OpenDropDown(FieldNames.FleetGroup);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetGroup));
            Page.OpenDropDown(FieldNames.FleetGroup);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetGroup));
            Page.SelectFleetGroupFirstRow();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetGroup));

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
            Page.SelectDateRangeFirstRow();
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24230" })]
        public void TC_24230(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.FleetPOPOQTransactionLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.SaveAsBookmark), GetErrorMessage(ErrorMessages.SaveAsBookmarkNotFound));
            
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.FleetPOPOQTransactionLookup).ForEach(x => { Assert.Fail(x); });
            Assert.AreEqual("All", Page.GetValueDropDown(FieldNames.DocumentType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DocumentType));
            Assert.AreEqual("Received Date", Page.GetValueDropDown(FieldNames.DateType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DateType));
            Assert.AreEqual("Customized date", Page.GetValueDropDown(FieldNames.DateRange), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DateRange));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            FleetPOPOQTransactionLookupUtil.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {               
                string documentNumber = Page.GetFirstRowData(TableHeaders.DocumentNumber);
                Page.FilterTable(TableHeaders.DocumentNumber, documentNumber);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DocumentNumber, documentNumber), ErrorMessages.NoRowAfterFilter);

                Page.FilterTable(TableHeaders.DocumentNumber, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);

                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                Page.FilterTable(TableHeaders.DocumentNumber, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);

                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.Columns, ButtonsAndMessages.SaveLayout));
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25883" })]
        public void TC_25883(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetPOPOQTransactionLookup);
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
