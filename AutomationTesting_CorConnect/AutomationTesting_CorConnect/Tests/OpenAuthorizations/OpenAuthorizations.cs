using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations;
using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations.CreateNewAuthorization;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.OpenAuthorization;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.OpenAuthorizations
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Open Authorizations")]
    internal class OpenAuthorizations : DriverBuilderClass
    {
        OpenAuthorizationsPage Page;
        CreateNewAuthorizationPopUpPage aspxPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.OpenAuthorizations);
            Page = new OpenAuthorizationsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20744" })]
        public void TC_20744(string UserType)
        {
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23554" })]
        public void TC_23554(string dealerCode, string fleetCode)
        {
            string poNumber = CommonUtils.RandomString(6);
            string invNum = CommonUtils.RandomString(6);
            Page.GridLoad();
            CreateNewAuthorizationPopUpPage aspxPage = Page.CreateNewAuthorization();
            aspxPage.SelectValueByScroll(FieldNames.TransactionType, "Service");
            aspxPage.EnterDateInInvoiceDate();
            aspxPage.EnterDealerCode(dealerCode);
            aspxPage.EnterFleetCode(fleetCode);
            aspxPage.ClickContinue();
            aspxPage.WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            aspxPage.WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            aspxPage.EnterInvoiceAmount("50.00");
            aspxPage.EnterTextAfterClear(FieldNames.PurchaseOrderNumber, poNumber);
            aspxPage.EnterTextAfterClear(FieldNames.InvoiceNumber, invNum);
            aspxPage.ClickCreateAuthorization();
            Assert.IsTrue(aspxPage.CheckForText(ButtonsAndMessages.SuccessfulTransaction));
            var authcode = aspxPage.GetText("Authorization Code Label");
            aspxPage.ClosePopupWindow();
            aspxPage.SwitchToMainWindow();
            Page.PopulateGrid(fleetCode);
            Page.FilterTable(TableHeaders.Auth_Code, authcode);
            StringAssert.Contains("*", Page.GetFirstRowData(TableHeaders.Currency));
            Page.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            Assert.IsTrue(aspxPage.CheckForText(authcode));
            Assert.IsFalse(aspxPage.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23555" })]
        public void TC_23555(string dealerCode, string fleetCode)
        {
            string poNumber = CommonUtils.RandomString(6);
            string invNum = CommonUtils.RandomString(6);
            Page.GridLoad();
            CreateNewAuthorizationPopUpPage aspxPage = Page.CreateNewAuthorization();
            aspxPage.SelectValueByScroll(FieldNames.TransactionType, "Service");
            aspxPage.EnterDateInInvoiceDate();
            aspxPage.EnterDealerCode(dealerCode);
            aspxPage.EnterFleetCode(fleetCode);
            aspxPage.ClickContinue();
            aspxPage.WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            aspxPage.WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            aspxPage.EnterInvoiceAmount("50.00");
            aspxPage.EnterTextAfterClear(FieldNames.PurchaseOrderNumber, poNumber);
            aspxPage.EnterTextAfterClear(FieldNames.InvoiceNumber, invNum);
            aspxPage.ClickCreateAuthorization();
            Assert.IsTrue(aspxPage.CheckForText(ButtonsAndMessages.SuccessfulTransaction));
            var authcode = aspxPage.GetText("Authorization Code Label");
            aspxPage.ClosePopupWindow();
            aspxPage.SwitchToMainWindow();
            Page.GridLoad();
            Page.FilterTable(TableHeaders.Auth_Code, authcode);
            StringAssert.Contains("*", Page.GetFirstRowData(TableHeaders.Currency));
            Page.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            Assert.IsTrue(aspxPage.CheckForText(authcode));
            Assert.IsFalse(aspxPage.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23556" })]
        public void TC_23556(string dealerCode, string fleetCode)
        {
            Page.GridLoad();
            Page.FilterTable(TableHeaders.DealerCode, dealerCode);
            Page.FilterTable(TableHeaders.FleetCode, fleetCode);
            StringAssert.Contains("*", Page.GetFirstRowData(TableHeaders.Currency));
            Page.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            aspxPage = new CreateNewAuthorizationPopUpPage(driver);
            Assert.AreEqual(aspxPage.VerifyDropDownIsDisabled(FieldNames.TransactionType), "true");
            Assert.IsFalse(aspxPage.IsElementEnabled(FieldNames.InvoiceDate));
            Assert.AreEqual(aspxPage.VerifyDropDownIsDisabled(aspxPage.RenameMenuField(FieldNames.DealerCode)), "true");
            Assert.AreEqual(aspxPage.VerifyDropDownIsDisabled(aspxPage.RenameMenuField(FieldNames.FleetCode)), "true");
            Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.InvoiceAmount));
            Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.PurchaseOrderNumber));
            Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.InvoiceNumber));
            aspxPage.EnterInvoiceAmount("100.00");
            aspxPage.ClickResubmitAuthorization();
            aspxPage.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), "If customer has credit available for $100.00, this action will void your existing authorization and obtain a new authorization.");
            Assert.IsFalse(aspxPage.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
            Assert.IsTrue(aspxPage.CheckForText(ButtonsAndMessages.SuccessfulTransaction, true));
            var authcode = aspxPage.GetText("Authorization Code Label");
            aspxPage.ClosePopupWindow();
            aspxPage.SwitchToMainWindow();
            Page.GridLoad();
            Page.FilterTable(TableHeaders.Auth_Code, authcode);
            StringAssert.Contains("*", Page.GetFirstRowData(TableHeaders.Currency));
            Page.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            Assert.IsFalse(aspxPage.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23557" })]
        public void TC_23557(string dealerCode, string fleetCode)
        {
            Page.GridLoad();
            Page.FilterTable(TableHeaders.DealerCode, dealerCode);
            Page.FilterTable(TableHeaders.FleetCode, fleetCode);
            StringAssert.Contains("*", Page.GetFirstRowData(TableHeaders.Currency));
            Page.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            aspxPage = new CreateNewAuthorizationPopUpPage(driver);
            Assert.AreEqual(aspxPage.VerifyDropDownIsDisabled(FieldNames.TransactionType), "true");
            Assert.IsFalse(aspxPage.IsElementEnabled(FieldNames.InvoiceDate));
            Assert.AreEqual(aspxPage.VerifyDropDownIsDisabled(aspxPage.RenameMenuField(FieldNames.DealerCode)), "true");
            Assert.AreEqual(aspxPage.VerifyDropDownIsDisabled(aspxPage.RenameMenuField(FieldNames.FleetCode)), "true");
            Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.InvoiceAmount));
            Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.PurchaseOrderNumber));
            Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.InvoiceNumber));
            aspxPage.EnterInvoiceAmount("100.00");
            aspxPage.ClickResubmitAuthorization();
            aspxPage.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), "If customer has credit available for $100.00, this action will void your existing authorization and obtain a new authorization.");
            Assert.IsFalse(aspxPage.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
            Assert.IsTrue(aspxPage.CheckForText(ButtonsAndMessages.SuccessfulTransaction, true));
            var authcode = aspxPage.GetText("Authorization Code Label");
            aspxPage.ClosePopupWindow();
            aspxPage.SwitchToMainWindow();
            Page.GridLoad();
            Page.FilterTable(TableHeaders.Auth_Code, authcode);
            StringAssert.Contains("*", Page.GetFirstRowData(TableHeaders.Currency));
            Page.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            Assert.IsFalse(aspxPage.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23497" })]
        public void TC_23497(string dealerCode, string fleetCode)
        {
            string invNum = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().CreateAuthorization(invNum, dealerCode, fleetCode, out string authCode));
            Page.GridLoad();
            Page.FilterTable(TableHeaders.Auth_Code, authCode);
            StringAssert.Contains("*", Page.GetFirstRowData(TableHeaders.Currency));
            Page.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            aspxPage = new CreateNewAuthorizationPopUpPage(driver);
            Assert.IsTrue(aspxPage.CheckForText(authCode));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23499" })]
        public void TC_23499(string dealerCode, string fleetCode)
        {
            string invNum = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().CreateAuthorization(invNum, dealerCode, fleetCode, out string authCode));
            Page.GridLoad();
            Page.FilterTable(TableHeaders.Auth_Code, authCode);
            StringAssert.Contains("*", Page.GetFirstRowData(TableHeaders.Currency));
            Page.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            aspxPage = new CreateNewAuthorizationPopUpPage(driver);
            Assert.IsTrue(aspxPage.CheckForText(authCode));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23313" })]
        public void TC_23313(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.OpenAuthorization), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Page.AreFieldsAvailable(Pages.OpenAuthorizations).ForEach(x=>{ Assert.Fail(x); });

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            var errorMsgs= Page.ValidateTableHeadersFromFile();

            OpenAuthorizationUtils.GetData(out string from, out string to);

            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                

                string dealerInv = Page.GetFirstRowData(TableHeaders.DealerInv__spc);
                Page.FilterTable(TableHeaders.DealerInv__spc, dealerInv);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerInv__spc, dealerInv), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerInv__spc, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerInv__spc, CommonUtils.RandomString(10));
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25817" })]
        public void TC_25817(string UserType, string dealerUser, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.OpenAuthorizations);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.OpenAuthorizations);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
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
