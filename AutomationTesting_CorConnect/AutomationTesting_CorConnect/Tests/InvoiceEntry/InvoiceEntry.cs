using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.CreateAuthorization;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.InvoiceEntry
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Invoice Entry")]
    internal class InvoiceEntry : DriverBuilderClass
    {
        InvoiceEntryPage page;
        CreateNewInvoicePage CreateNewInvoicePage;
        CreateAuthorizationPage CreateAuthorizationPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.InvoiceEntry);
            page = new InvoiceEntryPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17504" })]
        public void TC_17504(string UserType)
        {
            InvoiceEntryUtils.GetInvoice(out DateTime tranactionDate, out string transactionNumber);
            Assert.That(tranactionDate, Is.Not.Null, "Transaction Number returned empty from DB");
            var invoice = page.OpenExistingInvoice(tranactionDate, transactionNumber, TableHeaders.DealerInv_);
            var invoiceHistory = invoice.ClickInvoiceHistory();

            var errorMsgs = invoiceHistory.ValidateTableDetails(false, true);
            errorMsgs.AddRange(invoiceHistory.ValidateTableHeaders(TableHeaders.Date, TableHeaders.InvoiceState, TableHeaders.Description));

            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }
            string dateTime = CommonUtils.RemoveTimeZone(invoiceHistory.GetElementByIndex(By.XPath("//table[contains(@id,'DXMainTable')]"), TableHeaders.Date));
            DateTime dt = DateTime.ParseExact(dateTime, CommonUtils.GetClientDateFormat() + " HH:mm:ss", CultureInfo.InvariantCulture);
            Assert.AreEqual(CommonUtils.ConvertDate(dt, "M/d/yyyy"), tranactionDate.ToString("M/d/yyyy"), GetErrorMessage(ErrorMessages.ValueMisMatch, TableHeaders.Date));
            Assert.AreEqual(invoiceHistory.GetElementByIndex(By.XPath("//table[contains(@id,'DXMainTable')]"), TableHeaders.InvoiceState), "Saved", GetErrorMessage(ErrorMessages.ValueMisMatch, TableHeaders.InvoiceState));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20747" })]
        public void TC_20747(string UserType)
        {
            page.OpenDropDown(FieldNames.CompanyName);
            page.ClickPageTitle();
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            page.OpenDropDown(FieldNames.CompanyName);
            page.ScrollDiv();
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            page.OpenDatePicker(FieldNames.From);
            page.ClickPageTitle();
            Assert.IsTrue(page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            page.OpenDatePicker(FieldNames.From);
            page.ScrollDiv();
            Assert.IsTrue(page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            page.SelectDate(FieldNames.From);
            Assert.IsTrue(page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            page.OpenDatePicker(FieldNames.To);
            page.ClickPageTitle();
            Assert.IsTrue(page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            page.OpenDatePicker(FieldNames.To);
            page.ScrollDiv();
            Assert.IsTrue(page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            page.SelectDate(FieldNames.To);
            Assert.IsTrue(page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24212" })]
        public void TC_24212(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.InvoiceEntry), page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.CreateNewInvoice), GetErrorMessage(ErrorMessages.CreateNewInvoiceNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Assert.IsTrue(page.IsElementDisplayed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.FieldDisplayed, FieldNames.CompanyName));
            Assert.IsTrue(page.IsElementDisplayed(FieldNames.From), GetErrorMessage(ErrorMessages.FieldDisplayed, FieldNames.From));
            Assert.IsTrue(page.IsElementDisplayed(FieldNames.To), GetErrorMessage(ErrorMessages.FieldDisplayed, FieldNames.To));

            var buttons = page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateNewInvoice);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(page.ValidateTableHeadersFromFile());

            InvoiceEntryUtils.GetData(out string from, out string to);
            page.LoadDataOnGrid(from, to);

            if (page.IsAnyDataOnGrid())
            {

                string InvNo = page.GetFirstRowData(TableHeaders.DealerInv_);
                page.FilterTable(TableHeaders.DealerInv_, InvNo);
                Assert.IsTrue(page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerInv_, InvNo), ErrorMessages.NoRowAfterFilter);
                page.FilterTable(TableHeaders.DealerInv_, CommonUtils.RandomString(10));
                Assert.IsTrue(page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                page.ClearFilter();
                Assert.IsTrue(page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                page.FilterTable(TableHeaders.DealerInv_, CommonUtils.RandomString(10));
                Assert.IsTrue(page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                page.ResetFilter();
                Assert.IsTrue(page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                errorMsgs.AddRange(page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateNewInvoice));
                errorMsgs.AddRange(page.ValidateTableDetails(true, true));               
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25438" })]
        public void TC_25438(string UserType, string Fleet, string Dealer, string Part, int NumberOfSections)
        {
            CreateNewInvoicePage CreateNewInvoicePage = page.OpenCreateNewInvoice();
            string dealerInvNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            string InvoiceType = "Fixed";
            var errorMsgs = CreateNewInvoicePage.CreateNewInvoice(Fleet, Dealer, dealerInvNum, InvoiceType);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, Part);
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections ; i++)
            {
                CreateNewInvoicePage.Click(ButtonsAndMessages.AddSection);
                CreateNewInvoicePage.WaitForLoadingIcon();
                CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, Part);
                CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
                CreateNewInvoicePage.WaitForLoadingIcon();
            }

            CreateNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.Continue);
            CreateNewInvoicePage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            if (!CreateNewInvoicePage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Creating Invoice invoice [{dealerInvNum}]");
            }

            Assert.AreEqual(NumberOfSections+1, InvoiceEntryUtils.GetInvoiceSectionCount(dealerInvNum), "Invoice Section count from DB is not equal to expected count.");

            Console.WriteLine($"Successfully Created Invoice: [{dealerInvNum}]");

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25421" })]
        public void TC_25421(string UserType, string Fleet, string Dealer, string Part, int NumberOfSections)
        {
            CreateNewInvoicePage CreateNewInvoicePage = page.OpenCreateNewInvoice();
            string dealerInvNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            var errorMsgs = CreateNewInvoicePage.CreateNewInvoice(Fleet, Dealer, dealerInvNum);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, Part);
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections; i++)
            {
                CreateNewInvoicePage.Click(ButtonsAndMessages.AddSection);
                CreateNewInvoicePage.WaitForLoadingIcon();
                CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, Part);
                CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
                CreateNewInvoicePage.WaitForLoadingIcon();
            }

            CreateNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.Continue);
            CreateNewInvoicePage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            if (!CreateNewInvoicePage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Creating Invoice invoice [{dealerInvNum}]");
            }
            Assert.AreEqual(NumberOfSections + 1, InvoiceEntryUtils.GetInvoiceSectionCount(dealerInvNum), "Invoice Section count from DB is not equal to expected count.");
            Console.WriteLine($"Successfully Created Invoice: [{dealerInvNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25424" })]
        public void TC_25424(string UserType, string Fleet, string Dealer, string Part, int NumberOfSections)
        {
            CreateNewInvoicePage CreateNewInvoicePage = page.OpenCreateNewInvoice();
            string dealerInvNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            var errorMsgs = CreateNewInvoicePage.CreateNewInvoice(Fleet, Dealer, dealerInvNum);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections; i++)
            {
                CreateNewInvoicePage.Click(ButtonsAndMessages.AddSection);
                CreateNewInvoicePage.WaitForLoadingIcon();
                CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, Part);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
                CreateNewInvoicePage.WaitForLoadingIcon();
            }

            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.ClosePopupWindow();
            page.LoadDataOnGrid(Dealer);
            page.FilterTable(TableHeaders.DealerInv_, dealerInvNum);
            page.ClickHyperLinkOnGrid(TableHeaders.DealerInv_);
            page.SwitchToPopUp();

            CreateNewInvoicePage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, NumberOfSections+1);
            CreateNewInvoicePage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, invoiceMsg);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.ClickElementByIndex(ButtonsAndMessages.EditLineItem,NumberOfSections);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            CreateNewInvoicePage.SetValue(FieldNames.CorePrice, "4.0000");
            CreateNewInvoicePage.WaitForElementToHaveValue(FieldNames.CorePrice,"4.0000");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.ClickElementByIndex(ButtonsAndMessages.AddLineItem,NumberOfSections);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.AddSection);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();

            Console.WriteLine($"Successfully Saved Invoice: [{dealerInvNum}]");
        }
        
        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25697" })]
        public void TC_25697(string Dealer, string Fleet, string Part)
        {
            page.CreateAuthorization(Dealer, Fleet, out string authCode, out string dealerInvNum);
            CreateNewInvoicePage CreateNewInvoicePage = page.OpenCreateNewInvoice();
            var errorMsgs = CreateNewInvoicePage.CreateNewAuthInvoice(Fleet,Dealer, authCode, dealerInvNum);
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
            CreateNewInvoicePage.AddLineItem(Part);
            CreateNewInvoicePage.AddTax();
            CreateNewInvoicePage.SubmitInvoice(dealerInvNum);

            Console.WriteLine($"Successfully Created Auth Invoice: [{dealerInvNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25891" })]
        public void TC_25891(string UserType, string Dealer)
        {
            var errorMsgs = page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(Dealer, Constants.UserType.Dealer);
            menu.OpenPage(Pages.InvoiceEntry);
            errorMsgs.AddRange(page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, Dealer));
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
