using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.OpenBalanceReport;
using AutomationTesting_CorConnect.PageObjects.PODiscrepancy;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.OpenBalanceReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Open Balance Report")]
    internal class OpenBalanceReport : DriverBuilderClass
    {
        OpenBalanceReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.OpenBalanceReport);
            Page = new OpenBalanceReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24620" })]
        public void TC_24620(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.OpenBalanceReport, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.OpenBalanceReport).ForEach(x => { Assert.Fail(x); });
            Page.ClickPageTitle();
            List<string> locationHeaders = Page.GetHeaderNamesTableDropDown(FieldNames.Location);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(locationHeaders.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, FieldNames.Location));
                Assert.IsTrue(locationHeaders.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.Location));
                Assert.IsTrue(locationHeaders.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, FieldNames.Location));
                Assert.IsTrue(locationHeaders.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.Location));
                Assert.IsTrue(locationHeaders.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.Location));
                Assert.IsTrue(locationHeaders.Contains(TableHeaders.EntityType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, FieldNames.Location));
                Assert.IsTrue(locationHeaders.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, FieldNames.Location));
            });

            Page.ClickPageTitle();
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.AgingBucket, "All", "Current", "Past due 1-30", "Past Due 31-60", "Past Due 61 -90", "Past Due 91+"), $"{FieldNames.AgingBucket} : " + ErrorMessages.ListElementsMissing);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.Currency, CommonUtils.GetActiveCurrencies().ToArray()), $"{FieldNames.Currency} : " + ErrorMessages.ListElementsMissing);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.Status, "All", "Exclude Disputes", "Only Disputes"), $"{FieldNames.Status} : " + ErrorMessages.ListElementsMissing);
            Page.ButtonClick(ButtonsAndMessages.Clear);
            Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
            Page.AcceptAlert();
            Page.WaitForMsg(ButtonsAndMessages.Loading);
            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());
            Page.LoadDataOnGrid();
            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string billingLocation = Page.GetFirstRowData(TableHeaders.BillingLocation);
                Page.FilterTable(TableHeaders.BillingLocation, billingLocation);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.BillingLocation, billingLocation), ErrorMessages.NoRowAfterFilter);
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.Columns, ButtonsAndMessages.SaveLayout));
                Page.FilterTable(TableHeaders.BillingLocation, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.BillingLocation, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);                
                Task t = Task.Run(() => Page.WaitForStalenessTable());
                Page.ButtonClick(ButtonsAndMessages.SaveLayout);
                Page.WaitForMsg(ButtonsAndMessages.Loading);
                t.Wait();
                t.Dispose();

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

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24727" })]
        public void TC_24727(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.OpenBalanceReport, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.OpenBalanceReport).ForEach(x => { Assert.Fail(x); });

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid();

            if (Page.IsAnyDataOnGrid())
            {
                string billingLocation = Page.GetFirstRowData(TableHeaders.BillingLocation);
                Page.FilterTable(TableHeaders.BillingLocation, billingLocation);

                Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                    List<string> headers = new List<String>()
                    {
                        TableHeaders.ProgramInvoice_,
                        TableHeaders.DealerInvoice_,
                        TableHeaders.OriginatingDocumentNumber,
                        TableHeaders.PO_,
                        TableHeaders.InvoiceDueDate,
                        TableHeaders.OriginalInvoiceAmount,
                        TableHeaders.CurrentDue,
                        TableHeaders.PastDue30Days,
                        TableHeaders.PastDue60Days,
                        TableHeaders.PastDue90Days,
                        TableHeaders.PastDue91Days,
                        TableHeaders.BalanceDue,
                        TableHeaders.InvoiceStatus,
                        TableHeaders.FleetShop,
                        TableHeaders.DealerShop,
                        TableHeaders.ProgramInvoiceDate,
                        TableHeaders.DealerInvoiceDate,
                        TableHeaders.PaymentTerms,
                        TableHeaders.Currency,
                        TableHeaders.BillingStatement_,
                        TableHeaders.DiscountAmount

                    };
                    errorMsgs.AddRange(Page.ValidateNestedTableHeaders(headers.ToArray()));
                    errorMsgs.AddRange(Page.ValidateNestedGridButtons(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));

                    string programInvoiceNumber = Page.GetFirstRowDataNestedTable(TableHeaders.ProgramInvoice_);
                    Page.FilterNestedTable(TableHeaders.ProgramInvoice_, programInvoiceNumber);

                    Page.FilterNestedTable(TableHeaders.ProgramInvoice_, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountNestedPage() <= 0, ErrorMessages.FilterNotWorking);

                    Page.ClearNestedFilter(1);
                    Assert.IsTrue(Page.GetRowCountNestedPage() > 0, ErrorMessages.ClearFilterNotWorking);

                    Page.FilterNestedTable(TableHeaders.ProgramInvoice_, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountNestedPage() <= 0, ErrorMessages.FilterNotWorking);

                    Page.ResetNestedFilter(1);
                    Assert.IsTrue(Page.GetRowCountNestedPage() > 0, ErrorMessages.ResetNotWorking);

                    Assert.IsTrue(Page.ValidateHyperlinkNestedGrid(TableHeaders.ProgramInvoice_));

                }
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25835" })]
        public void TC_25835(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Fleet, null);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Dealer, null));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.OpenBalanceReport);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Dealer, null));
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
