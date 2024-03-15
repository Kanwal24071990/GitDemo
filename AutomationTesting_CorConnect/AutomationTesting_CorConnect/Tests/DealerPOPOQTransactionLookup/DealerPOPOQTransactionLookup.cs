using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerPOPOQTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.POEntry;
using AutomationTesting_CorConnect.PageObjects.POQEntry;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Tests.Parts;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerPOPOQTransactionLookup;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using AutomationTesting_CorConnect.Utils.POEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.DealerPOPOQTransactionLookup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer PO/POQ Transaction Lookup")]
    internal class DealerPOPOQTransactionLookup : DriverBuilderClass
    {
        DealerPOPOQTransactionLookupPage Page;
        POQEntryAspx POQEntryPage;
        POEntryAspx POEntryPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerPOPOQTransactionLookup);
            Page = new DealerPOPOQTransactionLookupPage(driver);
            POQEntryPage = new POQEntryAspx(driver);
            POEntryPage = new POEntryAspx(driver);
        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20096" })]
        public void TC_20096(string DocumentType, string DateType, string FromDate, string ToDate, string TransactionStatus)
        {
            Page.PopulateGrid(DocumentType, DateType, FromDate, ToDate, TransactionStatus);

            var data = DealerPOPOQTransactionLookupUtil.GetData(FromDate, ToDate);

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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20737" })]
        public void TC_20737(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23411" })]
        public void TC_23411(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.DealerPOPOQTransactionLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.DealerPOPOQTransactionLookup).ForEach(x => { Assert.Fail(x); });
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.From), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.From));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.To), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.To));

            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DocumentType, "All", "PO", "POQ"), $"{FieldNames.DocumentType} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateType, "Received Date", "Document Date", "POQ Date"), $"{FieldNames.DateType} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateRange, "Customized date", "Last 7 days", "Last 14 days", "Current month", "Last month"), $"{FieldNames.DateRange} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyDataMultiSelectDropDown(FieldNames.Currency, CommonUtils.GetActiveCurrencies().ToArray()), $"{FieldNames.Currency} DD: " + ErrorMessages.CurrencyCodeMissing);
            Assert.IsTrue(Page.VerifyDataMultiSelectDropDown(FieldNames.TransactionStatus, CommonUtils.GetActiveTransactionStatus().ToArray()), $"{FieldNames.TransactionStatus} DD: " + ErrorMessages.ListElementsMissing);

            Assert.AreEqual("All", Page.GetValueDropDown(FieldNames.DocumentType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DocumentType));
            Assert.AreEqual("Received Date", Page.GetValueDropDown(FieldNames.DateType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DateType));
            Assert.AreEqual("Customized date", Page.GetValueDropDown(FieldNames.DateRange), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DateRange));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            List<string> gridValidatingErrors = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(gridValidatingErrors.Count == 3, ErrorMessages.RightPanelNotEmpty);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            DealerPOPOQTransactionLookupUtil.GetDateData(out string from, out string to);
            Page.PopulateGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                
                string documentNumber = Page.GetFirstRowData(TableHeaders.DocumentNumber);
                string refPoq = Page.GetFirstRowData(TableHeaders.RefPOQ_);
                Page.FilterTable(TableHeaders.DocumentNumber, documentNumber);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.RefPOQ_, refPoq), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.RefPOQ_, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DocumentNumber, documentNumber);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.RefPOQ_, refPoq), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.RefPOQ_, CommonUtils.RandomString(10));
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

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23708" })]
        public void TC_23708(string UserType)
        {
            Page.GridLoad();

            Page.FilterTable(TableHeaders.DocumentType, "POQ");
            Page.FilterTable(TableHeaders.Errors, "Unit Number for Processing");
            Page.FilterTable(TableHeaders.TransactionStatus, "Fixable Discrepancy");
            if (Page.GetRowCountCurrentPage() <= 0) {
                Assert.Fail($"No Data Created to Run this Test Case");
            }
            string poqNum = Page.GetFirstRowData(TableHeaders.DocumentNumber);
            Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DocumentType, "POQ"), ErrorMessages.NoRowAfterFilter);
            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            POQEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, "auto-001");
            Task t = Task.Run(() => POQEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            POQEntryPage.Click(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!POQEntryPage.IsCheckBoxChecked(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => POQEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                POQEntryPage.Click(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }
            Assert.IsTrue(POQEntryPage.IsCheckBoxChecked(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)));
            POQEntryPage.Click(ButtonsAndMessages.AddLineItem);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.SelectValueByScroll(FieldNames.Type, "Shop Supplies");
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.EnterText(FieldNames.Item, "Shop Supplies Auto", TableHeaders.New);
            POQEntryPage.EnterText(FieldNames.ItemDescription, "Shop Supplies Auto description");
            POQEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POQEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.Click(ButtonsAndMessages.AddTax);
            POQEntryPage.SelectValueByScroll(FieldNames.TaxType, "GST");
            POQEntryPage.SetValue(FieldNames.Amount, "20.00");
            POQEntryPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POQEntryPage.Click(ButtonsAndMessages.SaveTax);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.Click(ButtonsAndMessages.SubmitPOQ);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.Click(ButtonsAndMessages.Continue);
            POQEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POQSubmissionCompletedSuccessfully, invoiceMsg);
            POQEntryPage.SwitchToMainWindow();
            Page.LoadDataOnDrid(poqNum);
            Assert.AreEqual("Completed",Page.GetFirstRowData(TableHeaders.TransactionStatus));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23707" })]
        public void TC_23707(string UserType)
        {
            Page.GridLoad();

            Page.FilterTable(TableHeaders.DocumentType, "PO");
            Page.FilterTable(TableHeaders.Errors, "Unit Number for Processing");
            Page.FilterTable(TableHeaders.TransactionStatus, "Fixable Discrepancy");
            if (Page.GetRowCountCurrentPage() <= 0)
            {
                Assert.Fail($"No Data Created to Run this Test Case");
            }
            string poNum = Page.GetFirstRowData(TableHeaders.DocumentNumber);
            Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DocumentType, "PO"), ErrorMessages.NoRowAfterFilter);
            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            POEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, "auto-001");
            Task t = Task.Run(() => POEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            POEntryPage.Click(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!POEntryPage.IsCheckBoxChecked(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress))) {
                t = Task.Run(() => POEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                POEntryPage.Click(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }
            Assert.IsTrue(POEntryPage.IsCheckBoxChecked(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)));
            POEntryPage.Click(ButtonsAndMessages.AddLineItem);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SelectValueByScroll(FieldNames.Type, "Shop Supplies");
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.EnterText(FieldNames.Item, "Shop Supplies Auto", TableHeaders.New);
            POEntryPage.EnterText(FieldNames.ItemDescription, "Shop Supplies Auto description");
            POEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.AddTax);
            POEntryPage.SelectValueByScroll(FieldNames.TaxType, "GST");
            POEntryPage.SetValue(FieldNames.Amount, "20.00");
            POEntryPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POEntryPage.Click(ButtonsAndMessages.SaveTax);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.Continue);
            POEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, invoiceMsg);
            POEntryPage.SwitchToMainWindow();
            Page.LoadDataOnDrid(poNum);
            Assert.AreEqual("Completed", Page.GetFirstRowData(TableHeaders.TransactionStatus));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23702" })]
        public void TC_23702(string UserType)
        {
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            string poNum = CommonUtils.RandomString(6);
            CommonUtils.UpdateFleetCreditLimits(0,0,fleetCode);
            Assert.IsTrue(new DMSServices().SubmitPO(poNum, dealerCode, fleetCode));
            Page.LoadDataOnDrid(poNum);
            if (Page.GetRowCountCurrentPage() <= 0)
            {
                Assert.Fail($"No Data Created to Run this Test Case");
            }

            Assert.AreEqual("Reviewable Discrepancy", Page.GetFirstRowData(TableHeaders.TransactionStatus));
            StringAssert.Contains("Credit not available", Page.GetFirstRowData(TableHeaders.Errors));
            CommonUtils.UpdateFleetCreditLimits(555555, 555555, fleetCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            if (!POEntryPage.IsCheckBoxChecked(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress))) {
                Task t = Task.Run(() => POEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                POEntryPage.Click(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
                Assert.IsTrue(POEntryPage.IsCheckBoxChecked(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)));
            }
            POEntryPage.Click(ButtonsAndMessages.AddLineItem);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            POEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            POEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.AddTax);
            POEntryPage.SelectValueByScroll(FieldNames.TaxType, "GST");
            POEntryPage.SetValue(FieldNames.Amount, "20.00");
            POEntryPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POEntryPage.Click(ButtonsAndMessages.SaveTax);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.Continue);
            POEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, invoiceMsg);
            POEntryPage.SwitchToMainWindow();
            Page.LoadDataOnDrid(poNum);
            Assert.AreEqual("Completed", Page.GetFirstRowData(TableHeaders.TransactionStatus));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23705" })]
        public void TC_23705(string UserType, string DealerName, string FleetName)
        {
            string poqNum = CommonUtils.RandomString(6);
            Assert.IsFalse(new DMSServices().SubmitPOQ(poqNum, poqNum, FleetName));
            Page.GridLoad();
            Page.FilterTable(TableHeaders.DocumentType, "POQ");
            Page.FilterTable(TableHeaders.Errors, "Code Invalid");
            Page.FilterTable(TableHeaders.TransactionStatus, "Fatal Discrepancy");
            if (Page.GetRowCountCurrentPage() <= 0)
            {
                Assert.Fail($"No Data Created to Run this Test Case");
            }
            string docNum = Page.GetFirstRowData(TableHeaders.DocumentNumber);
            Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DocumentType, "POQ"), ErrorMessages.NoRowAfterFilter);
            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            POQEntryPage.SearchAndSelectValue(FieldNames.DealerCode, DealerName);
            if (!POQEntryPage.IsCheckBoxChecked(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                Task t = Task.Run(() => POQEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                POQEntryPage.Click(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
                Assert.IsTrue(POQEntryPage.IsCheckBoxChecked(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)));
            }
            POQEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, "auto-001");
            POQEntryPage.Click(ButtonsAndMessages.AddLineItem);
            POQEntryPage.WaitForLoadingIcon();
            POEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            POEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            POEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POQEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.Click(ButtonsAndMessages.AddTax);
            POQEntryPage.SelectValueByScroll(FieldNames.TaxType, "GST");
            POQEntryPage.SetValue(FieldNames.Amount, "20.00");
            POQEntryPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POQEntryPage.Click(ButtonsAndMessages.SaveTax);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.Click(ButtonsAndMessages.SubmitPOQ);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.Click(ButtonsAndMessages.Continue);
            POQEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POQSubmissionCompletedSuccessfully, invoiceMsg);
            POQEntryPage.SwitchToMainWindow();
            Page.LoadDataOnDrid(poqNum);
            Assert.AreEqual("Completed", Page.GetFirstRowData(TableHeaders.TransactionStatus));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23704" })]
        public void TC_23704(string UserType, string DealerName, string FleetName)
        {
            string poNum = CommonUtils.RandomString(6);
            Assert.IsFalse(new DMSServices().SubmitPO(poNum, poNum, FleetName));
            Page.GridLoad();
            Page.FilterTable(TableHeaders.DocumentType, "PO");
            Page.FilterTable(TableHeaders.Errors, "Code Invalid");
            Page.FilterTable(TableHeaders.TransactionStatus, "Fatal Discrepancy");
            if (Page.GetRowCountCurrentPage() <= 0)
            {
                Assert.Fail($"No Data Created to Run this Test Case");
            }
            string docNum = Page.GetFirstRowData(TableHeaders.DocumentNumber);
            Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DocumentType, "PO"), ErrorMessages.NoRowAfterFilter);
            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            POEntryPage.SearchAndSelectValue(FieldNames.DealerCode, DealerName);
            if (!POEntryPage.IsCheckBoxChecked(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                Task t = Task.Run(() => POEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                POEntryPage.Click(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
                Assert.IsTrue(POEntryPage.IsCheckBoxChecked(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)));
            }
            POEntryPage.Click(ButtonsAndMessages.AddLineItem);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            POEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            POEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.AddTax);
            POEntryPage.SelectValueByScroll(FieldNames.TaxType, "GST");
            POEntryPage.SetValue(FieldNames.Amount, "20.00");
            POEntryPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POEntryPage.Click(ButtonsAndMessages.SaveTax);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.Continue);
            POEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, invoiceMsg);
            POEntryPage.SwitchToMainWindow();
            Page.LoadDataOnDrid(poNum);
            Assert.AreEqual("Completed", Page.GetFirstRowData(TableHeaders.TransactionStatus));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25431" })]
        public void TC_25431(string UserType, string DealerName, string FleetName)
        {
            string invalidDealer = CommonUtils.RandomString(3) + CommonUtils.RandomString(3);
            string poNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17Part",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsFalse(new DMSServices().SubmitPOPOQMultipleSections(poNum, invalidDealer, FleetName, 29,"R",part));
          
            Page.LoadDataOnDrid(poNum);
            Page.FilterTable(TableHeaders.DocumentNumber, poNum);
            
            string transactionStatus= Page.GetFirstRowData(TableHeaders.TransactionStatus);
            Assert.AreEqual("Fatal Discrepancy", transactionStatus);
            
            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            Page.SwitchToPopUp();

            POEntryPage.SearchAndSelectValueAfterOpen(FieldNames.DealerCode, DealerName);
            var entityDetails = CommonUtils.GetEntityDetails(DealerName);
            POEntryPage.SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());

            Assert.IsTrue(POEntryPage.IsCheckBoxChecked(FieldNames.SameAsDealerAddress));
            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poNum,"R"));

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 29);
            POEntryPage.AcceptAlert(out string poMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, poMsg);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 28);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            POEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            POEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 28);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            POEntryPage.SetValue(FieldNames.CorePrice, "4.0000");
            POEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();   

            POEntryPage.Click(ButtonsAndMessages.AddSection);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.Continue);
            POEntryPage.AcceptAlert(out string poMsg1);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, poMsg1);

            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poNum, "R"));

            Console.WriteLine($"Successfully Submit PO: [{poNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25434" })]
        public void TC_25434(string UserType, string DealerName, string FleetName)
        {
            string invalidDealer = CommonUtils.RandomString(3) + CommonUtils.RandomString(3);
            string poqNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17Part",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsFalse(new DMSServices().SubmitPOPOQMultipleSections(poqNum, invalidDealer, FleetName, 29, "Q",part));

            Page.LoadDataOnDrid(poqNum);
            Page.FilterTable(TableHeaders.DocumentNumber, poqNum);

            string transactionStatus = Page.GetFirstRowData(TableHeaders.TransactionStatus);
            Assert.AreEqual("Fatal Discrepancy", transactionStatus);

            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            Page.SwitchToPopUp();

            POQEntryPage.SearchAndSelectValueAfterOpen(FieldNames.DealerCode, DealerName);
            var entityDetails = CommonUtils.GetEntityDetails(DealerName);
            POQEntryPage.SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());

            Assert.IsTrue(POQEntryPage.IsCheckBoxChecked(FieldNames.SameAsDealerAddress));
            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poqNum,"Q"));

            POQEntryPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 29);
            POQEntryPage.AcceptAlert(out string poMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, poMsg);
            POQEntryPage.WaitForLoadingIcon();

            POQEntryPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 28);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            POQEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            POQEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POQEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryPage.WaitForLoadingIcon();

            POQEntryPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 28);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            POQEntryPage.SetValue(FieldNames.CorePrice, "4.0000");
            POQEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            POQEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryPage.WaitForLoadingIcon();

            POQEntryPage.Click(ButtonsAndMessages.AddSection);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POQEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryPage.WaitForLoadingIcon();

            POQEntryPage.Click(ButtonsAndMessages.SubmitPOQ);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.Click(ButtonsAndMessages.Continue);
            POQEntryPage.AcceptAlert(out string poqMsg1);
            Assert.AreEqual(ButtonsAndMessages.POQSubmissionCompletedSuccessfully, poqMsg1);

            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poqNum, "Q"));

            Console.WriteLine($"Successfully Submit POQ: [{poqNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25430" })]
        public void TC_25430(string UserType, string DealerName, string FleetName)
        {
            string unitNumber = "12345";
            string poNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17FPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitPOPOQMultipleSections(poNum, DealerName, FleetName, 29, "R", part));

            Page.LoadDataOnDrid(poNum);
            Page.FilterTable(TableHeaders.DocumentNumber, poNum);

            string transactionStatus = Page.GetFirstRowData(TableHeaders.TransactionStatus);
            Assert.AreEqual("Fixable Discrepancy", transactionStatus);

            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            Page.SwitchToPopUp();
            POEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, unitNumber);

            Task t = Task.Run(() => POEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            POEntryPage.Click(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!POEntryPage.IsCheckBoxChecked(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => POEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                POEntryPage.Click(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }

            Assert.IsTrue(POEntryPage.IsCheckBoxChecked(FieldNames.SameAsDealerAddress));
            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poNum, "R"));

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 29);
            POEntryPage.AcceptAlert(out string poMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, poMsg);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 28);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            POEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            POEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 28);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            POEntryPage.SetValue(FieldNames.CorePrice, "4.0000");
            POEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.Click(ButtonsAndMessages.AddSection);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.Continue);
            POEntryPage.AcceptAlert(out string poMsg1);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, poMsg1);

            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poNum, "R"));

            Console.WriteLine($"Successfully Submit PO: [{poNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25433" })]
        public void TC_25433(string UserType, string DealerName, string FleetName)
        {
            string unitNumber = "12345";
            string poqNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17FPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitPOPOQMultipleSections(poqNum, DealerName, FleetName, 29, "Q", part));

            Page.LoadDataOnDrid(poqNum);
            Page.FilterTable(TableHeaders.DocumentNumber, poqNum);

            string transactionStatus = Page.GetFirstRowData(TableHeaders.TransactionStatus);
            Assert.AreEqual("Fixable Discrepancy", transactionStatus);

            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            Page.SwitchToPopUp();
            POQEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, unitNumber);

            Task t = Task.Run(() => POQEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            POQEntryPage.Click(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!POQEntryPage.IsCheckBoxChecked(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => POQEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                POQEntryPage.Click(POQEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }

            Assert.IsTrue(POQEntryPage.IsCheckBoxChecked(FieldNames.SameAsDealerAddress));
            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poqNum, "Q"));

            POQEntryPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 29);
            POQEntryPage.AcceptAlert(out string poqMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, poqMsg);
            POQEntryPage.WaitForLoadingIcon();

            POQEntryPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 28);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            POQEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            POQEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POQEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryPage.WaitForLoadingIcon();

            POQEntryPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 28);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            POQEntryPage.SetValue(FieldNames.CorePrice, "4.0000");
            POQEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            POQEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryPage.WaitForLoadingIcon();

            POQEntryPage.Click(ButtonsAndMessages.AddSection);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POQEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryPage.WaitForLoadingIcon();

            POQEntryPage.Click(ButtonsAndMessages.SubmitPOQ);
            POQEntryPage.WaitForLoadingIcon();
            POQEntryPage.Click(ButtonsAndMessages.Continue);
            POQEntryPage.AcceptAlert(out string poqMsg1);
            Assert.AreEqual(ButtonsAndMessages.POQSubmissionCompletedSuccessfully, poqMsg1);

            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poqNum, "Q"));

            Console.WriteLine($"Successfully Submit POQ: [{poqNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25432" })]
        public void TC_25432(string UserType, string DealerName, string FleetName)
        {
            CommonUtils.UpdateFleetCreditLimits(0, 0, FleetName);
            string poNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17RPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitPOPOQMultipleSections(poNum, DealerName, FleetName, 29, "R", part));

            CommonUtils.UpdateFleetCreditLimits(99999999, 99999999, FleetName);

            Page.LoadDataOnDrid(poNum);
            Page.FilterTable(TableHeaders.DocumentNumber, poNum);

            string transactionStatus = Page.GetFirstRowData(TableHeaders.TransactionStatus);
            Assert.AreEqual("Reviewable Discrepancy", transactionStatus);

            Page.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);
            Page.SwitchToPopUp();

            Task t = Task.Run(() => POEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            POEntryPage.Click(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!POEntryPage.IsCheckBoxChecked(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => POEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                POEntryPage.Click(POEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }

            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poNum, "R"));

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 29);
            POEntryPage.AcceptAlert(out string poqMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, poqMsg);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 28);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            POEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            POEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 28);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            POEntryPage.SetValue(FieldNames.CorePrice, "4.0000");
            POEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.Click(ButtonsAndMessages.AddSection);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryPage.WaitForLoadingIcon();

            POEntryPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryPage.WaitForLoadingIcon();
            POEntryPage.Click(ButtonsAndMessages.Continue);
            POEntryPage.AcceptAlert(out string poqMsg1);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, poqMsg1);

            Assert.AreEqual(29, POEntryUtils.GetDisputedPOPOQSectionCount(poNum, "R"));

            Console.WriteLine($"Successfully Submit PO: [{poNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25779" })]
        public void TC_25779(string UserType, string DealerName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(DealerName, Constants.UserType.Dealer);
            menu.OpenPage(Pages.DealerPOPOQTransactionLookup);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, DealerName));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCriteriaCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
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
