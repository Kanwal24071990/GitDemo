using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.Disputes;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.Disputes;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.Disputes
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Disputes")]
    internal class Disputes : DriverBuilderClass
    {
        DisputesPage Page;
        InvoiceOptionsAspx aspxPage;
        InvoiceOptionsPage popupPage;
        DealerInvoiceTransactionLookupPage DITLPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.Disputes);
            Page = new DisputesPage(driver);
            popupPage = new InvoiceOptionsPage(driver);
            aspxPage = new InvoiceOptionsAspx(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20721" })]
        public void TC_20721(string UserType)
        {
            Page.OpenDropDown(FieldNames.Status);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            Page.OpenDropDown(FieldNames.Status);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            Page.SelectValueFirstRow(FieldNames.Status);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));

            Page.OpenDropDown(FieldNames.DateType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));
            Page.OpenDropDown(FieldNames.DateType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));
            Page.SelectValueFirstRow(FieldNames.DateType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateType));

            Page.OpenDropDown(FieldNames.DateRange);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.OpenDropDown(FieldNames.DateRange);
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

            Page.OpenDropDown(FieldNames.Currency);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            Page.OpenDropDown(FieldNames.Currency);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            Page.SelectValueFirstRow(FieldNames.Currency);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));

            Page.OpenDropDown(FieldNames.ResolutionInformation);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ResolutionInformation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ResolutionInformation));
            Page.OpenDropDown(FieldNames.ResolutionInformation);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ResolutionInformation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ResolutionInformation));
            Page.SelectValueFirstRow(FieldNames.ResolutionInformation);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ResolutionInformation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ResolutionInformation));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18383" })]
        [NonParallelizable]
        public void TC_18383(string UserType)
        {
            CommonUtils.DeactivateTokenPPV();
            CommonUtils.ActivateFutureDateInMinsToken();
            CommonUtils.SetFutureDateInMinsValue(0);
            string invoice1 = CommonUtils.RandomString(8);
            string invoice2 = CommonUtils.RandomString(8);
            string invoice3 = CommonUtils.RandomString(8);
            string invoice4 = CommonUtils.RandomString(8);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice1), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice1));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice2), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice2));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice3), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice3));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice4), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice4));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice1), string.Format(ErrorMessages.InvoiceValidationFailed, invoice1));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice2), string.Format(ErrorMessages.InvoiceValidationFailed, invoice2));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice3), string.Format(ErrorMessages.InvoiceValidationFailed, invoice3));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice4), string.Format(ErrorMessages.InvoiceValidationFailed, invoice4));

            menu.OpenNextPage(Pages.Disputes, Pages.DealerInvoiceTransactionLookup);
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);

            DITLPage.LoadDataOnGrid(invoice1);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            DITLPage.LoadDataOnGrid(invoice2);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            DITLPage.LoadDataOnGrid(invoice3);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            DITLPage.LoadDataOnGrid(invoice4);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            CommonUtils.UpdateDisputeDateForInvoice(invoice1, 0);
            CommonUtils.UpdateDisputeDateForInvoice(invoice2, 1);
            CommonUtils.UpdateDisputeDateForInvoice(invoice3, -6);
            CommonUtils.UpdateDisputeDateForInvoice(invoice4, -5);

            menu.OpenNextPage(Pages.DealerInvoiceTransactionLookup, Pages.Disputes);
            Page = new DisputesPage(driver);
            Page.SelectDateRange("Customized date");
            Page.SelectDateRange("Last 7 days");
            var from = DateTime.Parse(Page.GetValue(FieldNames.From));
            Assert.AreEqual(DateTime.Now.AddDays(-6).ChangeDateFormat("yyyy-MM-dd"), from.ChangeDateFormat("yyyy-MM-dd"));
            var to = DateTime.Parse(Page.GetValue(FieldNames.To));
            Assert.AreEqual(DateTime.Now.AddDays(0).ChangeDateFormat("yyyy-MM-dd"), to.ChangeDateFormat("yyyy-MM-dd"));
            Page.SelectValueTableDropDown(FieldNames.DateType, "Dispute Date");
            Page.SelectDateRange("Last 7 days");
            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)));
            Page.EnterToDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)));
            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.NoResultForInvoice, invoice2));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18398" })]
        public void TC_18398(string UserType)
        {
            CommonUtils.DeactivateTokenPPV();
            CommonUtils.ActivateFutureDateInMinsToken();
            CommonUtils.SetFutureDateInMinsValue(1440);
            string invoice1 = CommonUtils.RandomString(8);
            string invoice2 = CommonUtils.RandomString(8);
            string invoice3 = CommonUtils.RandomString(8);
            string invoice4 = CommonUtils.RandomString(8);
            string invoice5 = CommonUtils.RandomString(8);
            string invoice6 = CommonUtils.RandomString(8);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice1), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice1));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice2), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice2));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice3), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice3));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice4), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice4));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice5), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice5));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice6), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice6));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice1), string.Format(ErrorMessages.InvoiceValidationFailed, invoice1));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice2), string.Format(ErrorMessages.InvoiceValidationFailed, invoice2));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice3), string.Format(ErrorMessages.InvoiceValidationFailed, invoice3));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice4), string.Format(ErrorMessages.InvoiceValidationFailed, invoice4));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice5), string.Format(ErrorMessages.InvoiceValidationFailed, invoice5));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice6), string.Format(ErrorMessages.InvoiceValidationFailed, invoice6));

            menu.OpenNextPage(Pages.Disputes, Pages.DealerInvoiceTransactionLookup);
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);

            DITLPage.LoadDataOnGrid(invoice1);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            DITLPage.LoadDataOnGrid(invoice2);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            DITLPage.LoadDataOnGrid(invoice3);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            DITLPage.LoadDataOnGrid(invoice4);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            DITLPage.LoadDataOnGrid(invoice5);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            DITLPage.LoadDataOnGrid(invoice6);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            aspxPage.EnterText(FieldNames.Notes, "Dispute");
            aspxPage.Click(ButtonsAndMessages.Submit);
            aspxPage.WaitForLoadingGrid();
            aspxPage.ClosePopupWindow();
            DITLPage.SwitchToMainWindow();

            CommonUtils.UpdateDisputeDateForInvoice(invoice1, 0);
            CommonUtils.UpdateDisputeDateForInvoice(invoice2, 1);
            CommonUtils.UpdateDisputeDateForInvoice(invoice3, 2);
            CommonUtils.UpdateDisputeDateForInvoice(invoice4, -6);
            CommonUtils.UpdateDisputeDateForInvoice(invoice5, -5);
            CommonUtils.UpdateDisputeDateForInvoice(invoice6, -4);

            menu.OpenNextPage(Pages.DealerInvoiceTransactionLookup, Pages.Disputes);
            Page = new DisputesPage(driver);
            Page.SelectDateRange("Last 14 days");
            Page.SelectDateRange("Last 7 days");
            var from = DateTime.Parse(Page.GetValue(FieldNames.From));
            Assert.AreEqual(DateTime.Now.AddDays(-5).ChangeDateFormat("yyyy-MM-dd"), from.ChangeDateFormat("yyyy-MM-dd"));
            var to = DateTime.Parse(Page.GetValue(FieldNames.To));
            Assert.AreEqual(DateTime.Now.AddDays(1).ChangeDateFormat("yyyy-MM-dd"), to.ChangeDateFormat("yyyy-MM-dd"));
            Page.SelectValueTableDropDown(FieldNames.DateType, "Dispute Date");
            Page.SelectDateRange("Last 7 days");
            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.NoResultForInvoice, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice5);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice5), string.Format(ErrorMessages.NoResultForInvoice, invoice5));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice6);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice6), string.Format(ErrorMessages.NoResultForInvoice, invoice6));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice4));
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(2)));
            Page.EnterToDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(2)));
            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6)));
            Page.EnterToDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6)));
            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22933" })]
        public void TC_22933(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.Disputes, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.Disputes).ForEach(x => { Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                string[] dropDowns = { Page.RenameMenuField("Dealer"), Page.RenameMenuField("Fleet") };
                string currentDropDown = null;
                foreach (string dropDown in dropDowns)
                {
                    List<string> headerNames = null;
                    if (dropDown.Equals(Page.RenameMenuField("Dealer")))
                    {
                        currentDropDown = Page.RenameMenuField(FieldNames.Dealer);
                        headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.Dealer);
                    }

                    else if (dropDown.Equals("Fleet"))
                    {
                        currentDropDown = Page.RenameMenuField(FieldNames.Fleet);
                        headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.Fleet);
                    }

                    Assert.IsTrue(headerNames.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, currentDropDown));

                }

                Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.Status, "All", "Disputed", "Resolved", "Closed"), $"{FieldNames.Status}" + ErrorMessages.ListElementsMissing);
                Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateType, "Program Invoice Date", "Settlement Date", "Dispute Date", "Follow Up By", "Last Updated"), $"{FieldNames.DateType}" + ErrorMessages.ListElementsMissing);
                Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateRange, "Customized date", "Last 7 days", "Last 14 days", "Current month", "Last month"), $"{FieldNames.DateRange} " + ErrorMessages.ListElementsMissing);
                Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.ResolutionInformation, "All Notes", "Last Note"), $"{FieldNames.ResolutionInformation} " + ErrorMessages.ListElementsMissing);
                Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.Currency, CommonUtils.GetActiveCurrencies().ToArray()), $"{FieldNames.Currency} " + ErrorMessages.ListElementsMissing);
                
                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());
            
                DisputesUtil.GetData(out string from, out string to);
                Page.PopulateGrid(from, to);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                                      

                    string fleetCode = Page.GetFirstRowData(TableHeaders.FleetCode);
                    Page.FilterTable(TableHeaders.FleetCode, fleetCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.FleetCode, fleetCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                    DisputesUtil.GetGridRowCount("All", from, to, out int dbRows);
                    Assert.AreEqual(dbRows, Page.GetGridTotalRowCount());

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

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16258" })]
        public void TC_16258(string UserType)
        {

            Page.PopulateGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Program Invoice Date");
            DisputesUtil.GetDataRowCount("Program Invoice Date", out int rowCountPID);
            Assert.AreEqual(rowCountPID, Page.GetRowCount());

            Page.PopulateGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Settlement Date");
            DisputesUtil.GetDataRowCount("Settlement Date", out int rowCountSD);
            Assert.AreEqual(rowCountSD, Page.GetRowCount());

            Page.PopulateGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCount("Dispute Date", out int rowCountDD);
            Assert.AreEqual(rowCountDD, Page.GetRowCount());

            Page.PopulateGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Follow Up By");
            DisputesUtil.GetDataRowCount("Follow Up By", out int rowCountFUB);
            Assert.AreEqual(rowCountFUB, Page.GetRowCount());

            Page.PopulateGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Last Updated");
            DisputesUtil.GetDataRowCount("Last Updated", out int rowCountLU);
            Assert.AreEqual(rowCountLU, Page.GetRowCount());

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24741" })]
        public void TC_24741(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.GetPageLabel(), Pages.Disputes, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            Page.PopulateGrid("All", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCountForStatus("All", out int dbRowsAll);
            Assert.AreEqual(dbRowsAll, Page.GetGridTotalRowCount());

            Page.PopulateGrid("Disputed", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCountForStatus("Disputed", out int dbRowsDisputed);
            Assert.AreEqual(dbRowsDisputed, Page.GetGridTotalRowCount());

            Page.PopulateGrid("Resolved", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCountForStatus("Resolved", out int dbRowsResolved);
            Assert.AreEqual(dbRowsResolved, Page.GetGridTotalRowCount());

            Page.PopulateGrid("Closed", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCountForStatus("Closed", out int dbRowsClosed);
            Assert.AreEqual(dbRowsClosed, Page.GetGridTotalRowCount());
            string dealerCode = String.Empty;
            string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
            if (userType == "ADMIN")
            {
                dealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            }
            else {
                dealerCode = CommonUtils.GetDealerCode();
            }
            var dealerEntityDetailId = CommonUtils.GetEntityDetails(dealerCode).EntityDetailId;

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerCode);
            Page.PopulateGrid("Disputed", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCountForEntity("Disputed", new string[] { dealerEntityDetailId.ToString() }, true, false, false, out int dbRowsCount);
            Assert.AreEqual(dbRowsCount, Page.GetGridTotalRowCount());

            Page.ClearSelectionMultiSelectDropDown(FieldNames.Dealer);

            string fleetCode = CommonUtils.GetFleetCode();
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, fleetCode);
            var fleetEntityDetailId = CommonUtils.GetEntityDetails(fleetCode).EntityDetailId;

            Page.PopulateGrid("Resolved", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCountForEntity("Resolved", new string[] { fleetEntityDetailId.ToString() }, false, true, false, out int dbRowsCount2);
            Assert.AreEqual(dbRowsCount2, Page.GetGridTotalRowCount());

            Page.PopulateGrid("Closed", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCountForEntity("Closed", new string[] { fleetEntityDetailId.ToString() }, false, true, false, out int dbRowsCount3);
            Assert.AreEqual(dbRowsCount3, Page.GetGridTotalRowCount());

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerCode);
            Page.PopulateGrid("All", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate(), "Dispute Date");
            DisputesUtil.GetDataRowCountForEntity("All", new string[] { dealerEntityDetailId.ToString(), fleetEntityDetailId.ToString() }, false, false, true, out int dbRows);
            Assert.AreEqual(dbRows, Page.GetGridTotalRowCount());

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24754" })]
        public void TC_24754(string UserType)
        {
            string dealerInvoiceNumber = string.Empty;
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.GetPageLabel(), Pages.Disputes, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            Page.PopulateGrid("Resolved", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate(), "Settlement Date");

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Resolved", "Credit/Rebill Issued", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("Credit/Rebill Issued", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

            Page.ResetFilter();

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Resolved", "Dealer No-action", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("Dealer No-action", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

            Page.ResetFilter();

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Resolved", "Documentation Received", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("Documentation Received", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

            Page.ResetFilter();

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Resolved", "PO Modified/Provided", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("PO Modified/Provided", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

            Page.PopulateGrid("Closed", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate(), "Settlement Date");

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Closed", "60-day Dispute Policy", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("60-day Dispute Policy", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

            Page.ResetFilter();

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Closed", "Chargeback", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("Chargeback", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

            Page.ResetFilter();

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Closed", "Collection", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("Collection", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

            Page.ResetFilter();

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Closed", "Non-Disputable Request", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("Non-Disputable Request", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

            Page.ResetFilter();

            dealerInvoiceNumber = DisputesUtil.GetDisputeDealerInvoiceNumber("Closed", "Internal Adjustment", CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-30)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvoiceNumber);
            Assert.AreEqual("Internal Adjustment", Page.GetFirstRowData(TableHeaders.ResolutionDetail));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22992" })]
        public void TC_22992(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.GetPageLabel(), Pages.Disputes, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            string fromDate = CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-180));
            string toDate = CommonUtils.GetCurrentDate();
            string dateType = "Dispute Date";
            List<string> activeCurrencies = CommonUtils.GetActiveCurrencies();
            Assert.IsTrue(activeCurrencies.Count > 0, $"{FieldNames.Currency} DropDown : " + ErrorMessages.NoRecordInDB);
            foreach (string currency in activeCurrencies)
            {
                Page.LoadDataOnGrid(dateType, currency, fromDate, toDate);
                Assert.AreEqual(Page.GetGridTotalRowCount(), DisputesUtil.GetDBRowCountForCurrencySearch(currency, fromDate, toDate), $"{FieldNames.Currency} : " + currency + " : " + ErrorMessages.ValueMisMatch);
            }
            string newCurrency = "USD";
            string dealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerEntityDetailId = CommonUtils.GetEntityDetails(dealerCode).EntityDetailId.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(dealerCode), $"{FieldNames.Dealer} DropDown : " + "Dealer code : " + ErrorMessages.NoRecordInDB);
            Assert.IsFalse(string.IsNullOrEmpty(dealerEntityDetailId), $"{FieldNames.Dealer} DropDown: " + "Dealer Entity Detail ID : " + ErrorMessages.NoRecordInDB);
            string fleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetEntityDetailId = CommonUtils.GetEntityDetails(fleetCode).EntityDetailId.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(fleetCode), $"{FieldNames.Fleet} DropDown: " + "Fleet code : " + ErrorMessages.NoRecordInDB);
            Assert.IsFalse(string.IsNullOrEmpty(fleetEntityDetailId), $"{FieldNames.Fleet} DropDown: " + "Fleet Entity Detail ID :  " + ErrorMessages.NoRecordInDB);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerCode);
            Page.LoadDataOnGrid(dateType, newCurrency, fromDate, toDate);
            Assert.AreEqual(Page.GetGridTotalRowCount(), DisputesUtil.GetDBRowCountCurrencySearchForEntity(newCurrency, new string[] { dealerEntityDetailId }, fromDate, toDate, true, false, false), $"{FieldNames.Currency} : " + ErrorMessages.ValueMisMatch);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, fleetCode);
            Page.LoadDataOnGrid(dateType, newCurrency, fromDate, toDate);
            Assert.AreEqual(Page.GetGridTotalRowCount(), DisputesUtil.GetDBRowCountCurrencySearchForEntity(newCurrency, new string[] { fleetEntityDetailId }, fromDate, toDate, false, false, true), $"{FieldNames.Currency} : " + ErrorMessages.ValueMisMatch);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.Dealer);
            Page.LoadDataOnGrid(dateType, newCurrency, fromDate, toDate);
            Assert.AreEqual(Page.GetGridTotalRowCount(), DisputesUtil.GetDBRowCountCurrencySearchForEntity(newCurrency, new string[] { dealerEntityDetailId, fleetEntityDetailId }, fromDate, toDate, false, true, false), $"{FieldNames.Currency} : " + ErrorMessages.ValueMisMatch); 
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25778" })]
        public void TC_25778(string UserType, string dealerUser, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.Disputes);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.Disputes);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, null));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
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
