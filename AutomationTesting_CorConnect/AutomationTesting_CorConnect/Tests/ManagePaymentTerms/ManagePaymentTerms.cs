using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ManagePaymentTerms;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.ManagePaymentTerms
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Payment Terms")]
    internal class ManagePaymentTerms : DriverBuilderClass
    {
        ManagePaymentTermsPage page;
        private string paymentTerm = CommonUtils.RandomString(4);

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManagePaymentTerms);
            page = new ManagePaymentTermsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21055" })]
        public void TC_21055(string UserType)
        {
            page.LoadDataOnGrid();

            var createNewPaymentTermPage = page.OpenCreateNewPaymentTerm();

            bool isMsgDisplayed = createNewPaymentTermPage.CreateNewPaymentTerm(paymentTerm, "4");

            page.SearchByPaymentTermName(paymentTerm);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(isMsgDisplayed);
                Assert.AreEqual(1, page.GetRowCount());
                Assert.AreEqual(paymentTerm, page.GetFirstRowData(TableHeaders.PaymentTerm));
                Assert.AreEqual("4", page.GetFirstRowData(TableHeaders.NetDueDays));
                Assert.AreEqual("Calendar Days", page.GetFirstRowData(TableHeaders.CalculationBasedOn));
                Assert.AreEqual("Both AP and AR", page.GetFirstRowData(TableHeaders.AvailableTo));
                Assert.AreEqual("Active", page.GetFirstRowData(TableHeaders.Status));
                Assert.AreEqual(DateTime.Now.ChangeDateFormat(CommonUtils.GetClientGridDateFormat()), page.GetFirstRowData(TableHeaders.CreationDate));
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21056" })]
        public void TC_21056(string UserType)
        {
            page.SearchByPaymentTermName(paymentTerm);
            page.ClickHyperLinkOnGrid(TableHeaders.Commands, false);
            Assert.AreEqual(page.GetAlertMessage(), ButtonsAndMessages.DeletePaymentTermsAlert);
            page.AcceptAlert();
            page.LoadDataOnGrid();
            Assert.AreEqual("InActive", page.GetFirstRowData(TableHeaders.Status));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24225" })]
        public void TC_24225(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ManagePaymentTerms), page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            page.AreFieldsAvailable(Pages.ManagePaymentTerms).ForEach(x => { Assert.Fail(x); });

            var buttons = page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset,ButtonsAndMessages.CreateNewPaymentTerm);
            Assert.IsTrue(buttons.Count > 0);

            Assert.IsTrue(page.VerifyValueDropDown(FieldNames.TermStatus, "All", "Active", "InActive"), $"{FieldNames.TermStatus} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(page.VerifyValueDropDown(FieldNames.TermAvailableto, "Both AP and AR", "AP Only", "AR Only"), $"{FieldNames.TermAvailableto} DD: " + ErrorMessages.ListElementsMissing);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(page.ValidateTableHeadersFromFile());

            page.LoadDataOnGrid();

            if (page.IsAnyDataOnGrid())
            { 
                errorMsgs.AddRange(page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateNewPaymentTerm));
                errorMsgs.AddRange(page.ValidateTableDetails(true, true));
                         
                string paymentTerm = page.GetFirstRowData(TableHeaders.PaymentTerm);
                page.FilterTable(TableHeaders.PaymentTerm, paymentTerm);
                Assert.IsTrue(page.VerifyFilterDataOnGridByHeader(TableHeaders.PaymentTerm, paymentTerm), ErrorMessages.NoRowAfterFilter);
                page.FilterTable(TableHeaders.PaymentTerm, CommonUtils.RandomString(10));
                Assert.IsTrue(page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                page.ClearFilter();
                Assert.IsTrue(page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                page.FilterTable(TableHeaders.PaymentTerm, CommonUtils.RandomString(10));
                Assert.IsTrue(page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                page.ResetFilter();
                Assert.IsTrue(page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22228" })]
        public void TC_22228(string UserType)
        {

            page.OpenDropDown(FieldNames.TermStatus);
            page.ClickPageTitle();
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.TermStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TermStatus));

            page.OpenDropDown(FieldNames.TermStatus);
            page.ScrollDiv();
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.TermStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TermStatus));

            page.SelectValueFirstRow(FieldNames.TermStatus);
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.TermStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TermStatus));

            string termStatus = page.GetValueDropDown(FieldNames.TermStatus).Trim().Split(' ')[0];
            page.SearchAndSelectValueAfterOpen(FieldNames.TermStatus, termStatus);
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.TermStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TermStatus));
            
            page.OpenDropDown(FieldNames.TermAvailableto);
            page.ClickPageTitle();
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.TermAvailableto), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TermAvailableto));

            page.OpenDropDown(FieldNames.TermAvailableto);
            page.ScrollDiv();
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.TermAvailableto), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TermAvailableto));

            page.SelectValueFirstRow(FieldNames.TermAvailableto);
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.TermAvailableto), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TermAvailableto));

            string termAvailableto = page.GetValueDropDown(FieldNames.TermAvailableto).Trim().Split(' ')[0];
            page.SearchAndSelectValueAfterOpen(FieldNames.TermAvailableto, termAvailableto);
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.TermAvailableto), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TermAvailableto));
        }
    }
}