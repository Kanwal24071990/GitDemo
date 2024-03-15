using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerInvoices;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Tests.Parts;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerInvoices;
using AutomationTesting_CorConnect.Utils.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.Utils.FleetInvoices;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.DealerInvoices
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Invoices")]
    internal class DealerInvoices : DriverBuilderClass
    {
        DealerInvoicesPage Page;
        InvoiceOptionsPage popupPage;
        CreateNewInvoicePage CreateNewInvoicePage;
        InvoiceOptionsAspx aspxPage;
        internal ApplicationContext appContext;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerInvoices);
            Page = new DealerInvoicesPage(driver);
            popupPage = new InvoiceOptionsPage(driver);
            aspxPage = new InvoiceOptionsAspx(driver);
            CreateNewInvoicePage = new CreateNewInvoicePage(driver);

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20719" })]
        public void TC_20719(string UserType)
        {
            Page.SwitchToAdvanceSearch();
            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ClickFieldLabel(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.SelectValueMultiSelectFirstRow(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));

            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ClickFieldLabel(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.SelectValueMultiSelectFirstRow(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));

            Page.OpenMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ClickFieldLabel(FieldNames.TransactionStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));
            Page.OpenMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));
            Page.SelectValueMultiSelectFirstRow(FieldNames.TransactionStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18292" })]
        public void TC_18292(string UserType)
        {
            CommonUtils.DeactivateTokenPPV();
            CommonUtils.ActivateFutureDateInMinsToken();
            CommonUtils.SetFutureDateInMinsValue(1440);
            string invoice1 = CommonUtils.RandomString(6);
            string invoice2 = CommonUtils.RandomString(6);
            string invoice3 = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice1));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice2));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice3));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice1));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice2));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice3));
            CommonUtils.UpdateTransactionDateForInvoice(invoice1, 0);
            CommonUtils.UpdateTransactionDateForInvoice(invoice2, 1);
            CommonUtils.UpdateTransactionDateForInvoice(invoice3, 2);
            Page.LoadDataOnGrid(invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1));
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2));
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(invoice3);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice3));
            CommonUtils.SetFutureDateInMinsValue(0);
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1));
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2));
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(invoice3);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice3));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18297" })]
        public void TC_18297(string UserType)
        {
            CommonUtils.DeactivateTokenPPV();
            CommonUtils.ActivateFutureDateInMinsToken();
            CommonUtils.SetFutureDateInMinsValue(1440);
            string invoice1 = CommonUtils.RandomString(6);
            string invoice2 = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice1));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice2));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice1));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice2));
            CommonUtils.UpdateTransactionDateForInvoice(invoice1, -730);
            CommonUtils.UpdateTransactionDateForInvoice(invoice2, -729);
            Page.LoadDataOnGrid(invoice1);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice1));
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2));
            CommonUtils.SetFutureDateInMinsValue(0);
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1));
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18311" })]
        public void TC_18311(string UserType)
        {
            CommonUtils.DeactivateTokenPPV();
            CommonUtils.ActivateFutureDateInMinsToken();
            CommonUtils.SetFutureDateInMinsValue(1440);
            string invoice1 = CommonUtils.RandomString(8);
            string invoice2 = CommonUtils.RandomString(8);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice1), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice1));
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice2), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice2));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice1), string.Format(ErrorMessages.InvoiceValidationFailed, invoice1));
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(invoice2), string.Format(ErrorMessages.InvoiceValidationFailed, invoice2));
            CommonUtils.UpdateReceiveDateForInvoice(invoice1, -179);
            CommonUtils.UpdateReceiveDateForInvoice(invoice2, -178);
            Page.SwitchToAdvanceSearch();
            string toDate = Page.GetValue(FieldNames.To);
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-180), toDate));
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsErrorIconVisibleWithTitle(ButtonsAndMessages.DateRangeError), "No error displayed for date range exceeding 180 days.");
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-179), toDate));
            Page.ClickPageTitle();
            Assert.IsFalse(Page.IsErrorIconVisibleWithTitle(ButtonsAndMessages.DateRangeError), "Error displaying for date range exceeding under 180 days.");
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-178)));
            Page.EnterToDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)));
            Page.LoadDataOnGrid(invoice1);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice1));
            Page.LoadDataOnGrid(invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.NoResultForInvoice, invoice2));
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-179)));
            Page.EnterToDate(CommonUtils.GetCurrentDate());
            Page.ClickPageTitle();
            Assert.IsFalse(Page.IsErrorIconVisibleWithTitle(ButtonsAndMessages.DateRangeError), "Error displaying for date range exceeding under 180 days.");
            Page.LoadDataOnGrid(invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice1));
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-178)));
            Page.EnterToDate(CommonUtils.GetCurrentDate());
            Page.ClickPageTitle();
            Page.LoadDataOnGrid(invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.NoResultForInvoice, invoice2));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18318" })]
        public void TC_18318(string UserType)
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

            CommonUtils.UpdateReceiveDateForInvoice(invoice1, 0);
            CommonUtils.UpdateReceiveDateForInvoice(invoice2, 1);
            CommonUtils.UpdateReceiveDateForInvoice(invoice3, 2);
            CommonUtils.UpdateReceiveDateForInvoice(invoice4, -6);
            CommonUtils.UpdateReceiveDateForInvoice(invoice5, -5);
            CommonUtils.UpdateReceiveDateForInvoice(invoice6, -4);
            Page.SwitchToAdvanceSearch();
            Page.SelectValueTableDropDown(FieldNames.DateType, "System received date Dealer");
            Page.SelectValueTableDropDown(FieldNames.DateRange, "Last 7 days");
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            string toDate = Page.GetValue(FieldNames.To);
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)), toDate, string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6), toDate), Page.GetValue(FieldNames.From), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Page.LoadDataOnGrid();
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

            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice1, 0);
            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice2, 1);
            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice3, 2);
            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice4, -6);
            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice5, -5);
            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice6, -4);
            Page.SelectValueTableDropDown(FieldNames.DateType, "Program invoice date");
            Page.LoadDataOnGrid();
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

            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice1, 0);
            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice2, 1);
            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice3, 2);
            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice4, -6);
            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice5, -5);
            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice6, -4);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Dealer invoice date"));
            Page.LoadDataOnGrid();
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

            CommonUtils.UpdateARStatementStartDateForInvoice(invoice1, 0);
            CommonUtils.UpdateARStatementStartDateForInvoice(invoice2, 1);
            CommonUtils.UpdateARStatementStartDateForInvoice(invoice3, 2);
            CommonUtils.UpdateARStatementStartDateForInvoice(invoice4, -6);
            CommonUtils.UpdateARStatementStartDateForInvoice(invoice5, -5);
            CommonUtils.UpdateARStatementStartDateForInvoice(invoice6, -4);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Fleet statement period start date"));
            Page.LoadDataOnGrid();
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

            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice1, 0);
            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice2, 1);
            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice3, 2);
            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice4, -6);
            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice5, -5);
            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice6, -4);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Dealer statement period start date"));
            Page.LoadDataOnGrid();
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

            CommonUtils.UpdateARStatementDateForInvoice(invoice1, 0);
            CommonUtils.UpdateARStatementDateForInvoice(invoice2, 1);
            CommonUtils.UpdateARStatementDateForInvoice(invoice3, 2);
            CommonUtils.UpdateARStatementDateForInvoice(invoice4, -6);
            CommonUtils.UpdateARStatementDateForInvoice(invoice5, -5);
            CommonUtils.UpdateARStatementDateForInvoice(invoice6, -4);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Fleet statement period end date"));
            Page.LoadDataOnGrid();
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

            CommonUtils.UpdateAPStatementDateForInvoice(invoice1, 0);
            CommonUtils.UpdateAPStatementDateForInvoice(invoice2, 1);
            CommonUtils.UpdateAPStatementDateForInvoice(invoice3, 2);
            CommonUtils.UpdateAPStatementDateForInvoice(invoice4, -6);
            CommonUtils.UpdateAPStatementDateForInvoice(invoice5, -5);
            CommonUtils.UpdateAPStatementDateForInvoice(invoice6, -4);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Dealer statement period end date"));
            Page.LoadDataOnGrid();
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

            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice1, 0);
            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice2, 1);
            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice3, 2);
            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice4, -6);
            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice5, -5);
            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice6, -4);
            Page.SelectValueTableDropDown(FieldNames.DateType, "System received date Fleet");
            Page.LoadDataOnGrid();
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

            //CommonUtils.UpdateInvoiceDateForInvoice(invoice1, 0);
            //CommonUtils.UpdateInvoiceDateForInvoice(invoice2, 1);
            //CommonUtils.UpdateInvoiceDateForInvoice(invoice3, 2);
            //CommonUtils.UpdateInvoiceDateForInvoice(invoice4, -6);
            //CommonUtils.UpdateInvoiceDateForInvoice(invoice5, -5);
            //CommonUtils.UpdateInvoiceDateForInvoice(invoice6, -4);
            //Page.SelectValueTableDropDown(FieldNames.DateType, "Settlement Date");
            //Page.LoadDataOnGrid();
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            //Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            //Assert.IsTrue(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.NoResultForInvoice, invoice2));
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice5);
            //Assert.IsTrue(Page.VerifyDataOnGrid(invoice5), string.Format(ErrorMessages.NoResultForInvoice, invoice5));
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice6);
            //Assert.IsTrue(Page.VerifyDataOnGrid(invoice6), string.Format(ErrorMessages.NoResultForInvoice, invoice6));
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            //Assert.IsFalse(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice3));
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            //Assert.IsFalse(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice4));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18319" })]
        [NonParallelizable]
        public void TC_18319(string UserType)
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

            CommonUtils.UpdateReceiveDateForInvoice(invoice1, 0);
            CommonUtils.UpdateReceiveDateForInvoice(invoice2, 1);
            CommonUtils.UpdateReceiveDateForInvoice(invoice3, -6);
            CommonUtils.UpdateReceiveDateForInvoice(invoice4, -5);
            Page.SwitchToAdvanceSearch();
            Page.SelectValueTableDropDown(FieldNames.DateType, "System received date Dealer");
            Page.SelectValueTableDropDown(FieldNames.DateRange, "Last 7 days");
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            string toDate = Page.GetValue(FieldNames.To);
            Assert.AreEqual(CommonUtils.GetCurrentDate(), toDate, string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6), toDate), Page.GetValue(FieldNames.From), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice1, 0);
            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice2, 1);
            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice3, -6);
            CommonUtils.UpdateAPInvoiceDateForInvoice(invoice4, -5);
            Page.SelectValueTableDropDown(FieldNames.DateType, "Program invoice date");
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice1, 0);
            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice2, 1);
            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice3, -6);
            CommonUtils.UpdateTransactionDateInvoiceTbForInvoice(invoice4, -5);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Dealer invoice date"));
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

            CommonUtils.UpdateARStatementStartDateForInvoice(invoice1, 0);
            CommonUtils.UpdateARStatementStartDateForInvoice(invoice2, 1);
            CommonUtils.UpdateARStatementStartDateForInvoice(invoice3, -6);
            CommonUtils.UpdateARStatementStartDateForInvoice(invoice4, -5);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Fleet statement period start date"));
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice1, 0);
            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice2, 1);
            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice3, -6);
            CommonUtils.UpdateAPStatementStartDateForInvoice(invoice4, -5);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Dealer statement period start date"));
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

            CommonUtils.UpdateARStatementDateForInvoice(invoice1, 0);
            CommonUtils.UpdateARStatementDateForInvoice(invoice2, 1);
            CommonUtils.UpdateARStatementDateForInvoice(invoice3, -6);
            CommonUtils.UpdateARStatementDateForInvoice(invoice4, -5);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Fleet statement period end date"));
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

            CommonUtils.UpdateAPStatementDateForInvoice(invoice1, 0);
            CommonUtils.UpdateAPStatementDateForInvoice(invoice2, 1);
            CommonUtils.UpdateAPStatementDateForInvoice(invoice3, -6);
            CommonUtils.UpdateAPStatementDateForInvoice(invoice4, -5);
            Page.SelectValueTableDropDown(FieldNames.DateType, menu.RenameMenuField("Dealer statement period end date"));
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice1, 0);
            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice2, 1);
            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice3, -6);
            CommonUtils.UpdateCreateDateInvoiceTbForInvoice(invoice4, -5);
            Page.SelectValueTableDropDown(FieldNames.DateType, "System received date Fleet");
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));

            //CommonUtils.UpdateInvoiceDateForInvoice(invoice1, 0);
            //CommonUtils.UpdateInvoiceDateForInvoice(invoice2, 1);
            //CommonUtils.UpdateInvoiceDateForInvoice(invoice3, -6);
            //CommonUtils.UpdateInvoiceDateForInvoice(invoice4, -5);
            //Page.SelectValueTableDropDown(FieldNames.DateType, "Settlement Date");
            //Page.LoadDataOnGrid();
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            //Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            //Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            //Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            //Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            //Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18324" })]
        [NonParallelizable]
        public void TC_18324(string DealerInvoiceNumber)
        {
            CommonUtils.DeactivateTokenPPV();
            CommonUtils.ActivateFutureDateInMinsToken();
            CommonUtils.SetFutureDateInMinsValue(1440);
            Page.SwitchToAdvanceSearch();
            Page.SwitchToLastSevenDayDateRange();
            string toDate = Page.GetValue(FieldNames.To);
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)), toDate, string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6), toDate), Page.GetValue(FieldNames.From), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Page.SwitchToQuickSearch();
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.SwitchToAdvanceSearch();
            toDate = Page.GetValue(FieldNames.To);
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)), toDate, string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6), toDate), Page.GetValue(FieldNames.From), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            CommonUtils.SetFutureDateInMinsValue(0);
            Page.SwitchToLastSevenDayDateRange();
            toDate = Page.GetValue(FieldNames.To);
            Assert.AreEqual(CommonUtils.GetCurrentDate(), toDate, string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6), toDate), Page.GetValue(FieldNames.From), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Page.SwitchToQuickSearch();
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.SwitchToAdvanceSearch();
            toDate = Page.GetValue(FieldNames.To);
            Assert.AreEqual(CommonUtils.GetCurrentDate(), toDate, string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6), toDate), Page.GetValue(FieldNames.From), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21405" })]
        public void TC_21405(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGridWithFilter(DealerInvoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(DealerInvoiceNumber);
            Assert.IsEmpty(invoiceObj.APPaidDate);
            Assert.AreEqual(" ", Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, DealerInvoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, DealerInvoiceNumber));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21396" })]
        public void TC_21396(string DealerInvoiceNumber)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();

            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual("", Page.GetFirstRowData(TableHeaders.DatePaid).Trim(), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21395" })]
        public void TC_21395(string DealerInvoiceNumber)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();

            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual(" ", Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[2];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[3];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid).Trim(), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21384" })]
        public void TC_21384(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGridWithFilter(DealerInvoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(DealerInvoiceNumber);
            Assert.IsEmpty(invoiceObj.APPaidDate);
            Assert.AreEqual(" ", Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, DealerInvoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, DealerInvoiceNumber));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21388" })]
        public void TC_21388(string DealerInvoiceNumber)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();
            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual(" ", Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21381" })]
        public void TC_21381(string DealerInvoiceNumber)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();
            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual(" ", Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19315" })]
        public void TC_19315(string DealerInvoiceNumber)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();
            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23585" })]
        public void TC_23585(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.DealerInvoices), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.DealerInvoices).ForEach(x => { Assert.Fail(x); });
            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());
            
            DealerInvoicesUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);
            if (Page.IsAnyDataOnGrid())
            {
                string dealerCode = Page.GetFirstRowData(TableHeaders.DealerCode);
                Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerCode, dealerCode), ErrorMessages.NoRowAfterFilter);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerCode, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                if (Page.IsNextPage())
                {
                    Page.GoToPage(2);
                }
                              
                Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24704" })]
        [NonParallelizable]
        public void TC_24704(string UserType)
        {

            string programInvoiceNumber = FleetInvoicesUtils.GetDisputedInvoice();

            Page.LoadDataOnGridWithProgramInvoiceNumber(programInvoiceNumber);

            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);

            aspxPage.ValidateDisputeExportButtons(false);
            Assert.AreEqual("Export to Excel in MS 97 & 2003 versions (.xls)", aspxPage.GetTextOnHover("exportXlsButton"));
            Assert.AreEqual("Export to latest Excel versions (.xlsx)", aspxPage.GetTextOnHover("exportXlsxButton"));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25018" })]
        [NonParallelizable]
        public void TC_25018(string UserType)
        {
            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var user = client.Users.First(u => u.Type == "admin");
            var currentUser = user.User;
            string errorMsg = "Count mismatch for combination T Status[{0}] DateType[{1}] DateRange[{2}]";

            int transactionStatusCurrentCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current");
            int transactionStatusPaidCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Paid");
            int transactionStatusCurrentDisputedCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current-Disputed");

            Page.SwitchToAdvanceSearch();

            Page.SelectTransactionStatusAndDateType("Current", "System received date Dealer", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string gridCount = Page.GetPageCounterTotal();
                Assert.AreEqual(transactionStatusCurrentCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current", "System received date Dealer", "Last 7 days"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current");
            Page.SelectTransactionStatusAndDateType("Current-Disputed", "System received date Dealer", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string gridCount = Page.GetPageCounterTotal();
                Assert.AreEqual(transactionStatusCurrentDisputedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current-Disputed", "System received date Dealer", "Last 7 days"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Disputed");
            Page.SelectTransactionStatusAndDateType("Paid", "System received date Dealer", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string gridCount = Page.GetPageCounterTotal();
                Assert.AreEqual(transactionStatusCurrentDisputedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Paid", "System received date Dealer", "Last 7 days"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid");
            InvoiceObject invoiceDetails = DealerInvoiceTransactionLookupUtil.GetInvoiceInfoFromTransactionStatus();
            string dealerInvoiceNumber = invoiceDetails.TransactionNumber;
            string originatingDocumentNumber = invoiceDetails.OriginatingDocumentNumber;
            string poNumber = invoiceDetails.PurchaseOrderNumber;
            string invoiceNumber = invoiceDetails.InvoiceNumber;

            Page.EnterTextAfterClear(FieldNames.DealerInvoiceNumber, dealerInvoiceNumber);
            Page.EnterTextAfterClear(FieldNames.OriginatingDocumentNumber, originatingDocumentNumber);
            Page.EnterTextAfterClear(FieldNames.PONumber, poNumber);
            Page.EnterTextAfterClear(FieldNames.ProgramInvoiceNumber, invoiceNumber);
            Page.SelectTransactionStatusAndDateType("Current", "System received date Dealer", "Customized date", -29);

            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {

                Assert.AreEqual(invoiceDetails.TransactionNumber, Page.GetFirstRowData(TableHeaders.DealerInv__spc));
                Assert.AreEqual(invoiceDetails.OriginatingDocumentNumber, Page.GetFirstRowData(TableHeaders.OriginatingDocumentNumber));
                Assert.AreEqual(invoiceDetails.PurchaseOrderNumber, Page.GetFirstRowData(TableHeaders.PO_).Trim());
                Assert.AreEqual(invoiceDetails.InvoiceNumber, Page.GetFirstRowData(TableHeaders.ProgramInvoiceNumber));
                Assert.AreEqual("Current", Page.GetFirstRowData(TableHeaders.TransactionStatus));

            }
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.ClearText(FieldNames.OriginatingDocumentNumber);
            Page.ClearText(FieldNames.PONumber);
            Page.ClearText(FieldNames.ProgramInvoiceNumber);
            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current");


            EntityDetails entityDetails = FleetInvoicesUtils.GetInvoicesSubCommunities();
            var dealerSubCommunityName = entityDetails.DealerSubCommunity.Split("-")[1];
            var fleetSubCommunityName = entityDetails.FleetSubCommunity.Split("-")[1];
            int subCommunityCount = DealerInvoiceTransactionLookupUtil.GetInvoicesCountBySubCommunities(dealerSubCommunityName, fleetSubCommunityName);
            string dealersubcommunity = entityDetails.DealerSubCommunity;
            string fleetsubcommunity = entityDetails.FleetSubCommunity;
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerSubcommunity, TableHeaders.SubCommunity, dealersubcommunity);
            Page.ClickFieldLabel(FieldNames.DealerSubcommunity);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetSubcommunity, TableHeaders.SubCommunity, fleetsubcommunity);
            Page.SelectTransactionStatusAndDateType("Current", "System received date Dealer", "Last 7 days", 0);

            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string gridCount = Page.GetPageCounterTotal();
                Assert.AreEqual(subCommunityCount, int.Parse(gridCount));
            }
            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current");
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerSubcommunity, TableHeaders.SubCommunity, dealersubcommunity);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetSubcommunity, TableHeaders.SubCommunity, fleetsubcommunity);

            EntityDetails entityDetails1 = DealerInvoiceTransactionLookupUtil.GetEntityGroup(currentUser);
            var fleetGroup = entityDetails1.FleetGroup;
            var dealerGroup = entityDetails1.DealerGroup;
            int groupCount = DealerInvoiceTransactionLookupUtil.GetInvoicesCountByGroup(currentUser, dealerGroup, fleetGroup);
            Page.EnterTextDropDown(FieldNames.DealerGroup, dealerGroup);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.EnterTextDropDown(FieldNames.FleetGroup, fleetGroup);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.SelectTransactionStatusAndDateType("Current-Disputed", "System received date Dealer", "Customized date", -29);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string gridCount = Page.GetPageCounterTotal();
                Assert.AreEqual(groupCount, int.Parse(gridCount));
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25662" })]
        public void TC_25662(string UserType, string DealerName, string FleetName)
        {
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17KPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, 30, part));

            Page.LoadDataOnGrid(invNum);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            OffsetTransactionPage OffsetTransactionPopUpPage = popupPage.CreateRebill();

            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.CreateAReversal);
            Assert.IsTrue(OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.CreateAReversal));
            OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.ReversalReason);
            Task t = Task.Run(() => OffsetTransactionPopUpPage.WaitForStalenessOfElement("Dealer For Reversal"));
            OffsetTransactionPopUpPage.SelectValueFirstRow(FieldNames.ReversalReason);
            t.Wait();
            t.Dispose();
            OffsetTransactionPopUpPage.EnterText(FieldNames.FleetOrDealerApprover, "Dealer Approval");
            OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.Comments);
            OffsetTransactionPopUpPage.EnterText(FieldNames.Comments, "Comments");
            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Reverse);
            if (!OffsetTransactionPopUpPage.IsTextVisible(ButtonsAndMessages.ReversalTransactionCompletedSuccessfully, true))
            {
                Assert.Fail($"Some error occurred while Reserval of Invoice: [{invNum}]");
            }

            OffsetTransactionPopUpPage.SwitchToMainWindow();
            Page.LoadDataOnGrid(invNum + 'R');

            Assert.AreEqual(1, Page.GetRowCountCurrentPage());
            Assert.AreEqual(30, InvoiceEntryUtils.GetInvoiceSectionCount(invNum + 'R'));
            Console.WriteLine($"Successfully Reserval of Invoice: [{invNum+'R'}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25436" })]
        public void TC_25436(string UserType, string DealerName, string FleetName)
        {
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17KPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, 30, part));

            Page.LoadDataOnGrid(invNum);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            OffsetTransactionPage OffsetTransactionPopUpPage = popupPage.CreateRebill();

            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.RebillTheInvoice);
            Assert.IsTrue(OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.RebillTheInvoice));
            OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.CommentsRebill);
            OffsetTransactionPopUpPage.EnterText(FieldNames.CommentsRebill, "Comments");

            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Rebill);
            OffsetTransactionPopUpPage.SwitchToMainWindow();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            Page.SwitchToPopUp();
            CreateNewInvoicePage.WaitForElementToBeVisible(ButtonsAndMessages.EditLineItem);
            Task t = Task.Run(() => CreateNewInvoicePage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            CreateNewInvoicePage.Click(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!CreateNewInvoicePage.IsCheckBoxChecked(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => CreateNewInvoicePage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                CreateNewInvoicePage.Click(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }
           
            CreateNewInvoicePage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 30);
            CreateNewInvoicePage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, invoiceMsg);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 29);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            CreateNewInvoicePage.SetValue(FieldNames.CorePrice, "4.0000");
            CreateNewInvoicePage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 29);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Rental");
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            CreateNewInvoicePage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            CreateNewInvoicePage.SetValue(FieldNames.UnitPrice, "100.0000");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.AddSection);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.Continue);
            CreateNewInvoicePage.AcceptAlert(out string invoiceMsg1);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);
            if (!CreateNewInvoicePage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Cloning invoice [{invNum}]");
            }

            CreateNewInvoicePage.SwitchToMainWindow();
            Page.LoadDataOnGrid(invNum + 'C');

            Assert.AreEqual(1, Page.GetRowCountCurrentPage());
            Assert.AreEqual(30, InvoiceEntryUtils.GetInvoiceSectionCount(invNum + 'C'));
            Console.WriteLine($"Successfully Clone of Invoice: [{invNum+'C'}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25437" })]
        public void TC_25437(string UserType, string DealerName, string FleetName , int numberOfSections)
        {
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17KPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, numberOfSections, part));
            DealerInvoicesUtils.ActivateUpdateInvoiceOption(invNum);

            Page.LoadDataOnGrid(invNum);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            CreateNewInvoicePage CreateNewInvoicePopUpPage = popupPage.ChangeInvoice();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            Assert.AreEqual(numberOfSections, InvoiceEntryUtils.GetInvoiceSectionCount(invNum));
            Assert.IsTrue(CreateNewInvoicePage.IsEachElementDisabled(ButtonsAndMessages.AddLineItem, ButtonsAndMessages.DeleteSection, ButtonsAndMessages.EditLineItem, ButtonsAndMessages.DeleteLineItem), "Some Elements Of Sections Are Enable");
            Assert.IsFalse(CreateNewInvoicePage.IsElementVisible(ButtonsAndMessages.AddSection), "Add Section Button Visible");
            CreateNewInvoicePopUpPage.EnterText(FieldNames.UnitNumber, "Updated");
            CreateNewInvoicePopUpPage.Click(ButtonsAndMessages.Update);
            CreateNewInvoicePopUpPage.AcceptAlert(out string invoiceMsg1);
            Assert.AreEqual(ButtonsAndMessages.InvoiceResubmissionSucessfully, invoiceMsg1);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25781" })]
        public void TC_25781(string UserType, string dealerUser)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.DealerInvoices);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Category(TestCategory.EOPSmoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24395" })]
        public void TC_24395(string UserType)
        {
            List<string> errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.DealerInvoices), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            errorMsgs.AddRange(Page.ValidateLeftGridSearchFields(Pages.DealerInvoices));

            List<string> headerNames = null;

            headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.CompanyName);
            Assert.AreEqual(headerNames.Count, 4);
            Assert.IsTrue(headerNames.Contains(TableHeaders.PartnerName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.PartnerName));
            Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City));
            Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State));
            Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType));

            headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.VendorName);
            Assert.AreEqual(headerNames.Count, 4);
            Assert.IsTrue(headerNames.Contains(TableHeaders.PartnerName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.PartnerName));
            Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City));
            Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State));
            Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType));

            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid();
            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridFilters(TableHeaders.LocationCode));

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                errorMsgs.AddRange(Page.ValidateNavigationToNextPage(true));

                Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
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
