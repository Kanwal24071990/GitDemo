using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetInvoices;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.PageObjects.ManageUsers;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.Utils.FleetInvoices;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace AutomationTesting_CorConnect.Tests.FleetInvoices
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Invoices")]
    internal class FleetInvoices : DriverBuilderClass
    {
        FleetInvoicesPage Page;
        InvoiceOptionsPage popupPage;
        InvoiceOptionsAspx aspxPage;
        internal ApplicationContext appContext;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetInvoices);
            Page = new FleetInvoicesPage(driver);
            popupPage = new InvoiceOptionsPage(driver);
            aspxPage = new InvoiceOptionsAspx(driver);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18776" })]
        public void TC_18776(string paymentMethod)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.RenameMenuField(Pages.FleetInvoices), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Page.PopulateGrid(CommonUtils.ConvertDate(DateTime.Now.AddDays(-179)),
                    CommonUtils.GetCurrentDate(), paymentMethod);

                List<FleetInvoiceObject> fleetOnlineInvoicesList = FleetInvoicesUtils.GetFleetOnlineInvoices();
                List<FleetInvoiceObject> fleetInvoicesList = FleetInvoicesUtils.GetFleetInvoices();
                fleetInvoicesList = fleetInvoicesList.Where(x => !fleetOnlineInvoicesList.Any(y => y.InvoiceNumber == x.InvoiceNumber)).ToList();
                fleetInvoicesList.AddRange(fleetOnlineInvoicesList);
                var errorMsgs = Page.VerifyOnlinePaymentMethod(fleetInvoicesList);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20725" })]
        public void TC_20725(string UserType)
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

            Page.OpenMultiSelectDropDown(FieldNames.ViewStatus);
            Page.ClickFieldLabel(FieldNames.ViewStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.ViewStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ViewStatus));
            Page.OpenMultiSelectDropDown(FieldNames.ViewStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.ViewStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ViewStatus));
            Page.SelectValueMultiSelectFirstRow(FieldNames.ViewStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.ViewStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ViewStatus));

            Page.OpenMultiSelectDropDown(FieldNames.ApprovalStatus);
            Page.ClickFieldLabel(FieldNames.ApprovalStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.ApprovalStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ApprovalStatus));
            Page.OpenMultiSelectDropDown(FieldNames.ApprovalStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.ApprovalStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ApprovalStatus));
            Page.SelectValueMultiSelectFirstRow(FieldNames.ApprovalStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.ApprovalStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ApprovalStatus));

            Page.OpenMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ClickFieldLabel(FieldNames.TransactionStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));
            Page.OpenMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));
            Page.SelectValueMultiSelectFirstRow(FieldNames.TransactionStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));

            Page.OpenMultiSelectDropDown(FieldNames.DeliveryStatus);
            Page.ClickFieldLabel(FieldNames.DeliveryStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DeliveryStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DeliveryStatus));
            Page.OpenMultiSelectDropDown(FieldNames.DeliveryStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DeliveryStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DeliveryStatus));
            Page.SelectValueMultiSelectFirstRow(FieldNames.DeliveryStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DeliveryStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DeliveryStatus));

            Page.OpenMultiSelectDropDown(FieldNames.Currency);
            Page.ClickFieldLabel(FieldNames.Currency);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            Page.OpenMultiSelectDropDown(FieldNames.Currency);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            Page.SelectValueMultiSelectFirstRow(FieldNames.Currency);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20724" })]
        public void TC_20724(string UserType)
        {
            Page.OpenDropDown(FieldNames.SearchType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SearchType));
            Page.OpenDropDown(FieldNames.SearchType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SearchType));
            Page.SelectValueFirstRow(FieldNames.SearchType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SearchType));
            Page.SwitchToAdvanceSearch();


            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.SelectLoadBookmarkFirstRow();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));

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

            Page.OpenDropDown(FieldNames.PaymentMethod);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PaymentMethod), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PaymentMethod));
            Page.OpenDropDown(FieldNames.PaymentMethod);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PaymentMethod), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PaymentMethod));
            Page.SelectValueFirstRow(FieldNames.PaymentMethod);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PaymentMethod), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PaymentMethod));

            Page.OpenDropDown(FieldNames.ViewHistory);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ViewHistory), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ViewHistory));
            Page.OpenDropDown(FieldNames.ViewHistory);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ViewHistory), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ViewHistory));
            Page.SelectValueFirstRow(FieldNames.ViewHistory);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ViewHistory), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ViewHistory));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18294" })]
        public void TC_18294(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18299" })]
        public void TC_18299(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18313" })]
        public void TC_18313(string UserType)
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
            Page.SelectValueTableDropDown(FieldNames.DateType, "System received date Dealer");
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18320" })]
        public void TC_18320(string UserType)
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
            Page.SwitchToLastSevenDayDateRange();
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

            CommonUtils.UpdateARInvoiceDateForInvoice(invoice1, 0);
            CommonUtils.UpdateARInvoiceDateForInvoice(invoice2, 1);
            CommonUtils.UpdateARInvoiceDateForInvoice(invoice3, 2);
            CommonUtils.UpdateARInvoiceDateForInvoice(invoice4, -6);
            CommonUtils.UpdateARInvoiceDateForInvoice(invoice5, -5);
            CommonUtils.UpdateARInvoiceDateForInvoice(invoice6, -4);
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

            CommonUtils.UpdateInvoiceDateForInvoice(invoice1, 0);
            CommonUtils.UpdateInvoiceDateForInvoice(invoice2, 1);
            CommonUtils.UpdateInvoiceDateForInvoice(invoice3, 2);
            CommonUtils.UpdateInvoiceDateForInvoice(invoice4, -6);
            CommonUtils.UpdateInvoiceDateForInvoice(invoice5, -5);
            CommonUtils.UpdateInvoiceDateForInvoice(invoice6, -4);
            Page.SelectValueTableDropDown(FieldNames.DateType, "Settlement Date");
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
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18321" })]
        public void TC_18321(string UserType)
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

            CommonUtils.UpdateARInvoiceDateForInvoice(invoice1, 0);
            CommonUtils.UpdateARInvoiceDateForInvoice(invoice2, 1);
            CommonUtils.UpdateARInvoiceDateForInvoice(invoice3, -6);
            CommonUtils.UpdateARInvoiceDateForInvoice(invoice4, -5);
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

            CommonUtils.UpdateInvoiceDateForInvoice(invoice1, 0);
            CommonUtils.UpdateInvoiceDateForInvoice(invoice2, 1);
            CommonUtils.UpdateInvoiceDateForInvoice(invoice3, -6);
            CommonUtils.UpdateInvoiceDateForInvoice(invoice4, -5);
            Page.SelectValueTableDropDown(FieldNames.DateType, "Settlement Date");
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));
        }
        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18306" })]
        public void TC_18306(string UserType)
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
            Page.SelectDateRange("Last 7 days");
            var from = DateTime.Parse(Page.GetValue(FieldNames.From));
            Assert.AreEqual(DateTime.Now.AddDays(-5).ChangeDateFormat("yyyy-MM-dd"), from.ChangeDateFormat("yyyy-MM-dd"));
            var to = DateTime.Parse(Page.GetValue(FieldNames.To));
            Assert.AreEqual(DateTime.Now.AddDays(1).ChangeDateFormat("yyyy-MM-dd"), to.ChangeDateFormat("yyyy-MM-dd"));
            Page.LoadDataOnGrid(invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.LoadDataOnGrid(invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.NoResultForInvoice, invoice2));
            Page.LoadDataOnGrid(invoice3);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice3));
            Page.LoadDataOnGrid(invoice4);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice4));
            Page.LoadDataOnGrid(invoice5);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice5), string.Format(ErrorMessages.NoResultForInvoice, invoice5));
            Page.LoadDataOnGrid(invoice6);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice6), string.Format(ErrorMessages.NoResultForInvoice, invoice6));
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(2)));
            Page.EnterToDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(2)));
            Page.LoadDataOnGrid("");
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6)));
            Page.EnterToDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6)));
            Page.LoadDataOnGrid("");
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18307" })]
        public void TC_18307(string UserType)
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
            Page.SelectDateRange("Last 7 days");
            var from = DateTime.Parse(Page.GetValue(FieldNames.From));
            Assert.AreEqual(DateTime.Now.AddDays(-6).ChangeDateFormat("yyyy-MM-dd"), from.ChangeDateFormat("yyyy-MM-dd"));
            var to = DateTime.Parse(Page.GetValue(FieldNames.To));
            Assert.AreEqual(DateTime.Now.AddDays(0).ChangeDateFormat("yyyy-MM-dd"), to.ChangeDateFormat("yyyy-MM-dd"));
            Page.LoadDataOnGrid(invoice1);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice1), string.Format(ErrorMessages.NoResultForInvoice, invoice1));
            Page.LoadDataOnGrid(invoice2);
            Assert.IsFalse(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.ResultsForInvoiceFound, invoice2));
            Page.LoadDataOnGrid(invoice3);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice3), string.Format(ErrorMessages.NoResultForInvoice, invoice3));
            Page.LoadDataOnGrid(invoice4);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice4), string.Format(ErrorMessages.NoResultForInvoice, invoice4));
            Page.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)));
            Page.EnterToDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)));
            Page.LoadDataOnGrid("");
            Page.FilterTable(TableHeaders.DealerInv__spc, invoice2);
            Assert.IsTrue(Page.VerifyDataOnGrid(invoice2), string.Format(ErrorMessages.NoResultForInvoice, invoice2));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18325" })]
        [NonParallelizable]
        public void TC_18325(string DealerInvoiceNumber)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21397" })]
        public void TC_21397(string DealerInvoiceNumber, string User)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();
            string user = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString().ToUpper();

            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.ARPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber) + $" and User [{user}]");
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber) + $" and User [{user}]");

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.ARPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber) + $" and User [{user}]");
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber) + $" and User [{user}]");

            invoiceNumber = dealerInvoceNumbers[2];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.ARPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber) + $" and User [{user}]");
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber) + $" and User [{user}]");

            invoiceNumber = dealerInvoceNumbers[3];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual("", Page.GetFirstRowData(TableHeaders.DatePaid).Trim(), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber) + $" and User [{user}]");
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber) + $" and User [{user}]");

            menu.OpenPage(Pages.ManageUsers);
            ManageUsersPage manageUsers = new ManageUsersPage(driver);
            manageUsers.ImpersonateUser(User);
            menu.OpenPage(Pages.FleetInvoices);
            Page = new FleetInvoicesPage(driver);
            user = User;

            invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.ARPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber) + $" and User [{user}]");
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber) + $" and User [{user}]");

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.ARPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber) + $" and User [{user}]");
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber) + $" and User [{user}]");

            invoiceNumber = dealerInvoceNumbers[2];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.ARPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber) + $" and User [{user}]");
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber) + $" and User [{user}]");

            invoiceNumber = dealerInvoceNumbers[3];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual("", Page.GetFirstRowData(TableHeaders.DatePaid).Trim(), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber) + $" and User [{user}]");
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber) + $" and User [{user}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23310" })]
        public void TC_23310(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.FleetInvoices), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));


                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.FleetInvoices).ForEach(x=>{ Assert.Fail(x); });
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.SaveAsBookmark), GetErrorMessage(ErrorMessages.SaveAsBookmarkNotFound));
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);
                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());
                FleetInvoicesUtils.GetData(out string from, out string to);
                Page.LoadDataOnGrid(from, to);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));    
                                     
                    Assert.IsTrue(Page.IsNestedGridOpen());
                    Assert.IsTrue(Page.IsNestedGridClosed());

                    string fleetCode = Page.GetFirstRowData(TableHeaders.FleetCode);
                    Page.FilterTable(TableHeaders.FleetCode, fleetCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.FleetCode, fleetCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
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
            });

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24665" })]
        public void TC_24665(string UserType)
        {
            string label = " Label";
            string requiredLabel = " Required Label";
            string dealerInvoiceNumber = FleetInvoicesUtils.GetDisputedInvoice();

            Page.LoadDataOnGrid(dealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.ProgramInvoiceNumber);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.Status + label), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Status + label));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.DisputedBy + label), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisputedBy + label));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.On + label), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.On + label));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.Reason + label), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Reason + label));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.ReasonDetail + label), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ReasonDetail + label));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.DisputeNote + label), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisputeNote + label));

            aspxPage.ValidateDisputeExportButtons(false);
            Assert.AreEqual("Export to Excel in MS 97 & 2003 versions (.xls)", aspxPage.GetTextOnHover("exportXlsButton"));
            Assert.AreEqual("Export to latest Excel versions (.xlsx)", aspxPage.GetTextOnHover("exportXlsxButton"));

            List<string> errorMsgs = new List<string>();

            List<string> headers = new List<String>()
                    {
                        TableHeaders.NotesDate,
                        TableHeaders.Note,
                        TableHeaders.NotesBy,
                        TableHeaders.Phone,
                        TableHeaders.Email_,
                        TableHeaders.Attachment,
                        TableHeaders.Viewable

                    };

            errorMsgs.AddRange(aspxPage.ValidateTableHeaders("Dispute Grid", headers.ToArray()));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.PendingActionBy), string.Format(ErrorMessages.DropDownNotFound, FieldNames.PendingActionBy));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.OwnedBy), string.Format(ErrorMessages.DropDownNotFound, FieldNames.OwnedBy));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.FollowUpBy), string.Format(ErrorMessages.DropDownNotFound, FieldNames.FollowUpBy));
            Assert.IsTrue(aspxPage.IsCheckBoxDisplayed(FieldNames.NotifyAndVisibleToDealer), string.Format(ErrorMessages.CheckBoxMissing, FieldNames.NotifyAndVisibleToDealer));
            Assert.IsTrue(aspxPage.IsCheckBoxDisplayed(FieldNames.NotifyAndVisibleToFleet), string.Format(ErrorMessages.CheckBoxMissing, FieldNames.NotifyAndVisibleToFleet));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.Action), string.Format(ErrorMessages.DropDownNotFound, FieldNames.Action));
            Assert.IsTrue(aspxPage.IsElementVisible(ButtonsAndMessages.Uploadfile), string.Format(ErrorMessages.DropDownNotFound, ButtonsAndMessages.Uploadfile));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.Notes+requiredLabel), string.Format(ErrorMessages.ElementNotPresent, FieldNames.Notes+requiredLabel));
            Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.Notes), string.Format(ErrorMessages.TextBoxMissing, FieldNames.Notes));
            Assert.IsTrue(aspxPage.CheckForText("*- indicates required Field "));
            Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
            Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Cancel));

            List<string> ownedByHeaders = aspxPage.GetHeaderNamesTableDropDown(FieldNames.OwnedBy);
            Assert.IsTrue(ownedByHeaders.Contains(TableHeaders.FirstName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.FirstName, FieldNames.OwnedBy));
            Assert.IsTrue(ownedByHeaders.Contains(TableHeaders.LastName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LastName, FieldNames.OwnedBy));
            Assert.IsTrue(ownedByHeaders.Contains("Login ID"), GetErrorMessage(ErrorMessages.ColumnMissing, "Login ID", FieldNames.OwnedBy));

            List<string> actionTypes = FleetInvoicesUtils.GetActionTypes();
            Assert.IsTrue(aspxPage.VerifyValueSimpleSelect("Action", actionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");
            
            Task t = Task.Run(() => aspxPage.WaitForStalenessOfElement("Resolution Note"));
            aspxPage.SimpleSelectOptionByText("Action", "Resolve Dispute");
            t.Wait();
            t.Dispose();

            var resolutionDetails = FleetInvoicesUtils.GetResolutionDetails("Resolve Dispute");

            Assert.IsTrue(aspxPage.VerifyValueSimpleSelect("Resolution Detail", resolutionDetails.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");

            t = Task.Run(() => aspxPage.WaitForStalenessOfElement("Resolution Note"));
            aspxPage.SimpleSelectOptionByText("Action", "Close Dispute");
            t.Wait();
            t.Dispose();

            var resolutionDetailsClose = FleetInvoicesUtils.GetResolutionDetails("Close Dispute");

            Assert.IsTrue(aspxPage.VerifyValueSimpleSelect("Resolution Detail", resolutionDetailsClose.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");

            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24666" })]
        public void TC_24666(string UserType)
        {
            string dealerInvoiceNumber = FleetInvoicesUtils.GetDisputedInvoice();

            Page.LoadDataOnGrid(dealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.ProgramInvoiceNumber);
            popupPage.SwitchIframe();
            popupPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);

            aspxPage.ValidateDisputeExportButtons(false);
            Assert.AreEqual("Export to Excel in MS 97 & 2003 versions (.xls)", aspxPage.GetTextOnHover("exportXlsButton"));
            Assert.AreEqual("Export to latest Excel versions (.xlsx)", aspxPage.GetTextOnHover("exportXlsxButton"));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24703" })]
        public void TC_24703(string UserType)
        {
            string programInvoiceNumber = FleetInvoicesUtils.GetDisputedInvoice();

            Page.LoadDataOnGridWithProgramInvoiceNumber(programInvoiceNumber);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.ProgramInvoiceNumber);
                popupPage.SwitchIframe();
                popupPage.Click(ButtonsAndMessages.InvoiceOptions);

                driver.SwitchTo().Frame(1);
                driver.SwitchTo().Frame(1);

                aspxPage.ValidateDisputeExportButtons(false);
                
                Assert.AreEqual("Export to Excel in MS 97 & 2003 versions (.xls)", aspxPage.GetTextOnHover("exportXlsButton"));
                Assert.AreEqual("Export to latest Excel versions (.xlsx)", aspxPage.GetTextOnHover("exportXlsxButton"));

            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25016" })]
        public void TC_25016(string UserType)
        {

            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var user = client.Users.First(u => u.Type == "admin");
            var currentUser = user.User;
            string errorMsg = "Count mismatch for combination T Status[{0}] DateType[{1}] DateRange[{2}]";

            int transactionStatusCurrentCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Current");
            int transactionStatusCurrentClosedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Current-Closed");
            int transactionStatusCurrentDisputedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Current-Disputed");
            int transactionStatusCurrentResolvedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Current-Resolved");
            int transactionStatusCurrentHoldCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Current-Hold");
            int transactionStatusCurrentHoldReleasedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Current-Hold Released");
            int transactionStatusPaidCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Paid");
            int transactionStatusPaidClosedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Paid-Closed");
            int transactionStatusPaidDisputedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Paid-Disputed");
            int transactionStatusPaidResolvedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Paid-Resolved");
            int transactionStatusPastdueCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Past due");
            int transactionStatusPastdueClosedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Past due-Closed");
            int transactionStatusPastdueDisputedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Past due-Disputed");
            int transactionStatusPastdueHoldCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Past due-Hold");
            int transactionStatusPastdueHoldReleasedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Past due-Hold Released");
            int transactionStatusPastdueResolvedCount = FleetInvoicesUtils.GetRowCountTransactionStatus("Past due-Resolved");


            Page.SwitchToAdvanceSearch();
            
            Assert.Multiple(() =>
            {
                Page.SelectTransactionStatusAndDateType("Current", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Current", "Settlement Date", "Last 7 days"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current");
            Page.SelectTransactionStatusAndDateType("Current-Closed", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentClosedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Current-Closed", "Settlement Date", "Last 7 days"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Closed");
            Page.SelectTransactionStatusAndDateType("Current-Disputed", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentDisputedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Current-Disputed", "Settlement Date", "Last 7 days"));
            }
            
            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Disputed");
            Page.SelectTransactionStatusAndDateType("Current-Resolved", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
               
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentResolvedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Current-Resolved", "Settlement Date", "Last 7 days"));
             
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Resolved");
            Page.SelectTransactionStatusAndDateType("Current-Hold", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentHoldCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Current-Hold", "Settlement Date", "Last 7 days"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Hold");
            Page.SelectTransactionStatusAndDateType("Current-Hold Released", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
               
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentHoldReleasedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Current-Hold Released", "Settlement Date", "Last 7 days"));    
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Hold Released");
            Page.SelectTransactionStatusAndDateType("Paid", "Program invoice date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Paid", "Program invoice date", "Last 7 days"));
               
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid");
            Page.SelectTransactionStatusAndDateType("Paid-Closed", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidClosedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Paid-Closed", "Settlement Date", "Last 7 days"));
                
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid-Closed");
            Page.SelectTransactionStatusAndDateType("Paid-Disputed", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidDisputedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Paid-Disputed", "Settlement Date", "Last 7 days"));
                
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid-Disputed");
            Page.SelectTransactionStatusAndDateType("Paid-Resolved", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidResolvedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Paid-Resolved", "Settlement Date", "Last 7 days"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid-Resolved");
            Page.SelectTransactionStatusAndDateType("Past due", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastdueCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Past due", "Settlement Date", "Last 7 days"));
               
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due");
            Page.SelectTransactionStatusAndDateType("Past due-Closed", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastdueClosedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Past due-Closed", "Settlement Date", "Last 7 days"));
                
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Closed");
            Page.SelectTransactionStatusAndDateType("Past due-Disputed", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastdueDisputedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Past due-Disputed", "Settlement Date", "Last 7 days"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Disputed");
            Page.SelectTransactionStatusAndDateType("Past due-Hold", "Settlement Date", "Customized date", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastdueHoldCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Past due-Hold", "Settlement Date", "Customized date"));
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Hold");
            Page.SelectTransactionStatusAndDateType("Past due-Hold Released", "Program invoice date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastdueHoldReleasedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Past due-Hold Released", "Program invoice date", "Last 7 days"));
              
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Hold Released");
            Page.SelectTransactionStatusAndDateType("Past due-Resolved", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastdueResolvedCount, int.Parse(gridCount),GetErrorMessage(errorMsg, "Past due-Resolved", "Settlement Date", "Last 7 days"));
                
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Resolved");
            InvoiceObject invoiceDetails = FleetInvoicesUtils.GetInvoiceInfoFromTransactionStatus();

            string dealerInvoiceNumber = invoiceDetails.TransactionNumber;
            string originatingDocumentNumber = invoiceDetails.OriginatingDocumentNumber;
            string poNumber = invoiceDetails.PurchaseOrderNumber;
            string invoiceNumber = invoiceDetails.InvoiceNumber;

            Page.EnterTextAfterClear(FieldNames.DealerInvoiceNumber, dealerInvoiceNumber);
            Page.EnterTextAfterClear(FieldNames.OriginatingDocumentNumber, originatingDocumentNumber);
            Page.EnterTextAfterClear(FieldNames.PONumber, poNumber);
            Page.EnterTextAfterClear(FieldNames.ProgramInvoiceNumber, invoiceNumber);
            Page.SelectTransactionStatusAndDateType("Current-Closed", "Program invoice date", "Customized date", -30);

            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                
                    Assert.AreEqual(invoiceDetails.TransactionNumber, Page.GetFirstRowData(TableHeaders.DealerInv__spc));
                    Assert.AreEqual(invoiceDetails.OriginatingDocumentNumber, Page.GetFirstRowData(TableHeaders.OriginatingDocumentNumber));
                    Assert.AreEqual(invoiceDetails.PurchaseOrderNumber, Page.GetFirstRowData(TableHeaders.PO_).Trim());
                    Assert.AreEqual(invoiceDetails.InvoiceNumber, Page.GetFirstRowData(TableHeaders.ProgramInvoiceNumber));
                    Assert.AreEqual("Current-Closed", Page.GetFirstRowData(TableHeaders.TransactionStatus));
                
            }
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.ClearText(FieldNames.OriginatingDocumentNumber);
            Page.ClearText(FieldNames.PONumber);
            Page.ClearText(FieldNames.ProgramInvoiceNumber);
            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Closed");

            EntityDetails entityDetails = FleetInvoicesUtils.GetInvoicesSubCommunities();
            var dealerSubCommunityName = entityDetails.DealerSubCommunity.Split("-")[1];
            var fleetSubCommunityName = entityDetails.FleetSubCommunity.Split("-")[1];
            int subCommunityCount = FleetInvoicesUtils.GetInvoicesCountBySubCommunities(dealerSubCommunityName, fleetSubCommunityName);
            string dealersubcommunity = entityDetails.DealerSubCommunity;
            string fleetsubcommunity = entityDetails.FleetSubCommunity;
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerSubcommunity, TableHeaders.SubCommunity, dealersubcommunity);
            Page.ClickFieldLabel(FieldNames.DealerSubcommunity);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetSubcommunity, TableHeaders.SubCommunity, fleetsubcommunity);
            Page.SelectTransactionStatusAndDateType("Current-Resolved", "Settlement Date", "Customized date", -30);

            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(subCommunityCount, int.Parse(gridCount));
                
            }
            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Resolved");
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerSubcommunity, TableHeaders.SubCommunity, dealersubcommunity);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetSubcommunity, TableHeaders.SubCommunity, fleetsubcommunity);
           
            Page.SelectValueMultiSelectDropDown(FieldNames.DeliveryStatus, TableHeaders.DeliveryStatus, "EDI-ACCEPTED");
            Page.SelectTransactionStatusAndDateType("Current", "Settlement Date", "Customized date", -30);
            int deliveryStatusCount = FleetInvoicesUtils.GetInvoicesCountByDeliveryStatus();
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                   string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(deliveryStatusCount, int.Parse(gridCount));
               
            }

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current");
            Page.SelectValueMultiSelectDropDown(FieldNames.DeliveryStatus, TableHeaders.DeliveryStatus, "EDI-ACCEPTED");
            EntityDetails entityDetails1 = FleetInvoicesUtils.GetEntityGroup(currentUser);
            var fleetGroup = entityDetails1.FleetGroup;
            var dealerGroup = entityDetails1.DealerGroup;
            int groupCount = FleetInvoicesUtils.GetInvoicesCountByGroup(currentUser, dealerGroup,fleetGroup);
            Page.EnterTextDropDown(FieldNames.DealerGroup, dealerGroup);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.EnterTextDropDown(FieldNames.FleetGroup, fleetGroup);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Page.SelectTransactionStatusAndDateType("Current-Resolved", "Settlement Date", "Last 7 days", 0);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string gridCount = Page.GetPageCounterTotal();
                Assert.AreEqual(groupCount, int.Parse(gridCount));

            }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24667" })]
        public void TC_24667(string UserType)
        {
            string invoiceNumber = FleetInvoicesUtils.GetDisputedInvoiceWithMaxNotes();

            Page.LoadDataOnGrid(invoiceNumber);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.ProgramInvoiceNumber);
                popupPage.SwitchIframe();
                popupPage.Click(ButtonsAndMessages.InvoiceOptions);

                driver.SwitchTo().Frame(1);
                driver.SwitchTo().Frame(0);

                aspxPage.ValidateDisputeExportButtons(false);

            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24722" })]
        public void TC_24722(string UserType)
        {

            string invoice1 = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoice1));

            Page.LoadDataOnGrid(invoice1);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.ProgramInvoiceNumber);
                popupPage.SwitchIframe();
                popupPage.Click(ButtonsAndMessages.InvoiceOptions);

                driver.SwitchTo().Frame(1);
                driver.SwitchTo().Frame(0);
                aspxPage.EnterText(FieldNames.Notes, CommonUtils.RandomString(1000));
                aspxPage.Click(ButtonsAndMessages.Submit);
                aspxPage.WaitForLoadingGrid();
                aspxPage.ClosePopupWindow();
                aspxPage.SwitchToMainWindow();
                Page.ClickHyperLinkOnGrid(TableHeaders.ProgramInvoiceNumber);
                popupPage.SwitchIframe();
                popupPage.Click(ButtonsAndMessages.InvoiceOptions);
                driver.SwitchTo().Frame(1);
                driver.SwitchTo().Frame(0);
                aspxPage.ValidateDisputeExportButtons(false);

            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25783" })]
        public void TC_25783(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetInvoices);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, FleetName));
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
