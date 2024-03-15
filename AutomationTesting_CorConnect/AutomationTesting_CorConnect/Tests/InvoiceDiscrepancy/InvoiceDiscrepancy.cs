using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerInvoices;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancy;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.PageObjects.UpdateCredit;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using AutomationTesting_CorConnect.Utils.POEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.InvoiceDiscrepancy
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Invoice Discrepancy")]
    internal class InvoiceDiscrepancy : DriverBuilderClass
    {
        InvoiceDiscrepancyPage Page;
        CreateNewInvoicePage InvoiceEntryPage;
        DealerInvoiceTransactionLookupPage DITLPage;
        DealerInvoicesPage DIPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.InvoiceDiscrepancy);
            Page = new InvoiceDiscrepancyPage(driver);
            InvoiceEntryPage = new CreateNewInvoicePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20666" })]
        public void TC_20666(string UserType)
        {
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23312" })]
        public void TC_23312(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.InvoiceDiscrepancy), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));


            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.InvoiceDiscrepancy).ForEach(x => { Assert.Fail(x); });

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid();

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

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23706" })]
        public void TC_23706(string UserType)
        {
            Page.GridLoad();
            Page.FilterTable(TableHeaders.Errors, "Unit Number for Processing");
            Page.FilterTable(TableHeaders.Status, "Fixable");
            if (Page.GetRowCountCurrentPage() <= 0)
            {
                Assert.Fail($"No Data Created to Run this Test Case");
            }
            string poNum = Page.GetFirstRowData(Page.RenameMenuField(TableHeaders.DealerInv__spc));
            Page.ClickHyperLinkOnGrid(Page.RenameMenuField(TableHeaders.DealerInv__spc));
            InvoiceEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, "auto-001");
            Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }
            Assert.IsTrue(InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)));
            InvoiceEntryPage.Click(ButtonsAndMessages.AddLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SelectValueByScroll(FieldNames.Type, "Shop Supplies");
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.EnterText(FieldNames.Item, "Shop Supplies Auto", TableHeaders.New);
            InvoiceEntryPage.EnterText(FieldNames.ItemDescription, "Shop Supplies Auto description");
            InvoiceEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.AddTax);
            InvoiceEntryPage.SelectValueByScroll(FieldNames.TaxType, "GST");
            InvoiceEntryPage.SetValue(FieldNames.Amount, "20.00");
            InvoiceEntryPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveTax);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            InvoiceEntryPage.SwitchToMainWindow();
            menu.OpenNextPage(Pages.InvoiceDiscrepancy, Pages.DealerInvoiceTransactionLookup);
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
            DITLPage.LoadDataOnGrid(poNum);
            Assert.AreEqual("Current", DITLPage.GetFirstRowData(TableHeaders.TransactionStatus));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23701" })]
        public void TC_23701(string UserType)
        {
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            string invNum = CommonUtils.RandomString(6);
            CommonUtils.UpdateFleetCreditLimits(0, 0, fleetCode);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invNum, dealerCode, fleetCode));
            Page.GridLoad();
            Page.FilterTable(Page.RenameMenuField(TableHeaders.DealerInv__spc), invNum);
            if (Page.GetRowCountCurrentPage() <= 0)
            {
                Assert.Fail($"No Data Created to Run this Test Case");
            }
            Assert.AreEqual("Reviewable", Page.GetFirstRowData(TableHeaders.Status));
            StringAssert.Contains("Credit not available", Page.GetFirstRowData(TableHeaders.Errors));
            CommonUtils.UpdateFleetCreditLimits(555555, 555555, fleetCode);
            Page.ClickHyperLinkOnGrid(Page.RenameMenuField(TableHeaders.DealerInv__spc));
            if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
                Assert.IsTrue(InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)));
            }
            InvoiceEntryPage.Click(ButtonsAndMessages.AddLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            InvoiceEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            InvoiceEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.AddTax);
            InvoiceEntryPage.SelectValueByScroll(FieldNames.TaxType, "GST");
            InvoiceEntryPage.SetValue(FieldNames.Amount, "20.00");
            InvoiceEntryPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveTax);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            InvoiceEntryPage.SwitchToMainWindow();
            menu.OpenNextPage(Pages.InvoiceDiscrepancy, Pages.DealerInvoiceTransactionLookup);
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
            DITLPage.LoadDataOnGrid(invNum);
            Assert.AreEqual("Current", DITLPage.GetFirstRowData(TableHeaders.TransactionStatus));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23703" })]
        public void TC_23703(string UserType, string DealerName, string FleetName)
        {
            string invNum = CommonUtils.RandomString(6);
            Assert.IsFalse(new DMSServices().SubmitInvoice(invNum, invNum, FleetName));
            Page.GridLoad();
            Page.FilterTable(Page.RenameMenuField(TableHeaders.DealerInv__spc), invNum);
            if (Page.GetRowCountCurrentPage() <= 0)
            {
                Assert.Fail($"No Data Created to Run this Test Case");
            }
            Assert.AreEqual("Fatal", Page.GetFirstRowData(TableHeaders.Status));
            StringAssert.Contains("Code Invalid", Page.GetFirstRowData(TableHeaders.Errors));
            Page.ClickHyperLinkOnGrid(Page.RenameMenuField(TableHeaders.DealerInv__spc));
            InvoiceEntryPage.SearchAndSelectValue(FieldNames.DealerCode, DealerName);
            if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
                Assert.IsTrue(InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)));
            }
            InvoiceEntryPage.Click(ButtonsAndMessages.AddLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            InvoiceEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            InvoiceEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.AddTax);
            InvoiceEntryPage.SelectValueByScroll(FieldNames.TaxType, "GST");
            InvoiceEntryPage.SetValue(FieldNames.Amount, "20.00");
            InvoiceEntryPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveTax);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            InvoiceEntryPage.SwitchToMainWindow();
            menu.OpenNextPage(Pages.InvoiceDiscrepancy, Pages.DealerInvoiceTransactionLookup);
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
            DITLPage.LoadDataOnGrid(invNum);
            Assert.AreEqual("Current", DITLPage.GetFirstRowData(TableHeaders.TransactionStatus));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25428" })]
        public void TC_25428(string UserType, string DealerName, string FleetName)
        {
            string invalidDealer = CommonUtils.RandomString(3) + CommonUtils.RandomString(3);
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17Part",
                CorePrice = 5.00,
                UnitPrice = 5.00

            };
            Assert.IsFalse(new DMSServices().SubmitInvoiceMultipleSections(invNum, invalidDealer, FleetName, 100, part));

            Page.LoadDataOnGrid(FleetName);
            Page.FilterTable(TableHeaders.DealerInv__spc, invNum);

            string transactionStatus = Page.GetFirstRowData(TableHeaders.Status);
            Assert.AreEqual("Fatal", transactionStatus);

            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Page.SwitchToPopUp();
            InvoiceEntryPage.SearchAndSelectValueAfterOpen(FieldNames.DealerCode, DealerName);
            var entityDetails = CommonUtils.GetEntityDetails(DealerName);
            InvoiceEntryPage.SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());

            Assert.IsTrue(InvoiceEntryPage.IsCheckBoxChecked(FieldNames.SameAsDealerAddress));

            Assert.AreEqual(100, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 100);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, invoiceMsg);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 99);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            InvoiceEntryPage.SetValue(FieldNames.CorePrice, "4.0000");
            InvoiceEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 99);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            InvoiceEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            InvoiceEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.Click(ButtonsAndMessages.AddSection);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SearchAndSelectValue(FieldNames.Item, "17Part");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg1);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);

            if (!InvoiceEntryPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while submitting fatal discrepant Invoice [{invNum}]");
            }

            Assert.AreEqual(100, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            Console.WriteLine($"Successfully submitted fatal discrepant Invoice: [{invNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25427" })]
        public void TC_25427(string UserType, string DealerName, string FleetName)
        {
            string unitNumber = "12345";
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17FPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, 100, part));

            Page.LoadDataOnGrid(FleetName);
            Page.FilterTable(TableHeaders.DealerInv__spc, invNum);

            string transactionStatus = Page.GetFirstRowData(TableHeaders.Status);
            Assert.AreEqual("Fixable", transactionStatus);

            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Page.SwitchToPopUp();
            InvoiceEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, unitNumber);
            Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }

            Assert.AreEqual(100, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 100);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, invoiceMsg);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 99);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            InvoiceEntryPage.SetValue(FieldNames.CorePrice, "4.0000");
            InvoiceEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 99);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            InvoiceEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            InvoiceEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.Click(ButtonsAndMessages.AddSection);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg1);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);

            if (!InvoiceEntryPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Submitting Fixable Discrepant Invoice [{invNum}]");
            }

            Assert.AreEqual(100, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            Console.WriteLine($"Successfully Submitted Fixable Discrepant Invoice: [{invNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25429" })]
        public void TC_25429(string UserType, string DealerName, string FleetName)
        {
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17RPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, 100, part));

            Page.LoadDataOnGrid(FleetName);
            Page.FilterTable(TableHeaders.DealerInv__spc, invNum);

            string transactionStatus = Page.GetFirstRowData(TableHeaders.Status);
            Assert.AreEqual("Reviewable", transactionStatus);

            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Page.SwitchToPopUp();
            Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }

            Assert.AreEqual(100, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 100);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, invoiceMsg);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 99);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            InvoiceEntryPage.SetValue(FieldNames.CorePrice, "4.0000");
            InvoiceEntryPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 99);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SelectValueByScroll(FieldNames.Type, "Rental");
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            InvoiceEntryPage.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            InvoiceEntryPage.SetValue(FieldNames.UnitPrice, "100.0000");
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.Click(ButtonsAndMessages.AddSection);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            InvoiceEntryPage.Click(ButtonsAndMessages.SaveLineItem);
            InvoiceEntryPage.WaitForLoadingIcon();

            InvoiceEntryPage.Click(FieldNames.DoNotPutHoldForDealerCopy);
            if (!InvoiceEntryPage.IsCheckBoxChecked(FieldNames.DoNotPutHoldForDealerCopy))
            {
                InvoiceEntryPage.Click(FieldNames.DoNotPutHoldForDealerCopy);
            }
            InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
            InvoiceEntryPage.AcceptAlert(out string invoiceMsg1);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);

            if (!InvoiceEntryPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Submitting Reviewable Invoice [{invNum}]");
            }

            Assert.AreEqual(100, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            Console.WriteLine($"Successfully Submitted Reviewable Discrepant Invoice: [{invNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25811" })]
        public void TC_25811(string UserType, string dealerUser, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.InvoiceDiscrepancy);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.InvoiceDiscrepancy);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25663" })]
        public void TC_25663(string UserType, string DealerName, string FleetName)
        {
            string updateCreditLimit = "9999999" + CommonUtils.GenerateRandom(0, 9);
            string unitNumber = "12345";
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            string pageName = Pages.InvoiceDiscrepancy;
            Part part = new Part()
            {
                PartNumber = "17FPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, 100, part));

            Page.LoadDataOnGrid(FleetName);
            Page.FilterTable(TableHeaders.DealerInv__spc, invNum);
            string transactionStatus = Page.GetFirstRowData(TableHeaders.Status);
            Assert.AreEqual("Fixable", transactionStatus);

            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Page.SwitchToPopUp();
            InvoiceEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, unitNumber);
            Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }

            InvoiceEntryPage.Click(ButtonsAndMessages.SaveInvoice);
            InvoiceEntryPage.WaitForLoadingIcon();
            InvoiceEntryPage.ClosePopupWindow();

            menu.OpenNextPage(Pages.InvoiceDiscrepancy, Pages.UpdateCredit, true);
            pageName = "Update Credit Limit";
            UpdateCreditPage updateCreditPage;
            updateCreditPage = new UpdateCreditPage(driver);

            updateCreditPage.SwitchIframe();

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(pageName, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));

            updateCreditPage.SearchAndSelectValue(FieldNames.BillingLocation, FleetName);
            Assert.IsTrue(updateCreditPage.IsTextBoxEnabled(FieldNames.NewCreditLimit));
            updateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, updateCreditLimit);
            updateCreditPage.Click(FieldNames.Update);
            if (updateCreditPage.CheckForText("Please enter credit limit"))
            {
                updateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, updateCreditLimit);
                updateCreditPage.Click(FieldNames.Update);
            }
            Assert.IsTrue(updateCreditPage.CheckForText(ButtonsAndMessages.UpdateCreditSuccessfully, true), "Credit Not Updated Sucessfully");

            CommonUtils.RunResubmitDiscrepanciesJob();

            menu.OpenNextPage(Pages.UpdateCredit, Pages.DealerInvoiceTransactionLookup);
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
           
            pageName = Pages.DealerInvoiceTransactionLookup;
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(pageName, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
           
            DITLPage.LoadDataOnGrid(invNum);
            Assert.AreEqual("Current", DITLPage.GetFirstRowData(TableHeaders.TransactionStatus));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25762" })]
        [NonParallelizable]
        public void TC_25762(string UserType, string DealerName, string FleetName,string partNumber)
        {
            try
            {
                string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
                Part part = new Part()
                {
                    PartNumber = partNumber,
                    CorePrice = 5.00,
                    UnitPrice = 5.00
                };

                CommonUtils.ActivateTokenPPV();
                Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, 100, part));

                Page.LoadDataOnGrid(FleetName);
                Page.FilterTable(TableHeaders.DealerInv__spc, invNum);
                string transactionStatus = Page.GetFirstRowData(TableHeaders.Status);
                Assert.AreEqual("Fixable", transactionStatus);

                Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                Page.SwitchToPopUp();
                Assert.IsTrue(InvoiceEntryPage.IsTextVisible("Admin Error: Pending Price Validation from Daimler ", true));
                Assert.IsTrue(InvoiceEntryPage.IsCheckBoxDisplayed(FieldNames.AdminOverride));

                Assert.AreEqual(100, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));
                Assert.IsTrue(InvoiceEntryPage.IsEachElementDisabled(ButtonsAndMessages.AddLineItem, ButtonsAndMessages.DeleteSection, ButtonsAndMessages.EditLineItem, ButtonsAndMessages.DeleteLineItem), "Some Elements Of Sections Are Enable");
                Assert.IsFalse(InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.AddSection), "Add Section Button Visible");

                InvoiceEntryPage.Click(FieldNames.AdminOverride);
                Assert.IsTrue(InvoiceEntryPage.IsCheckBoxChecked(FieldNames.AdminOverride), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.AdminOverride));

                InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                InvoiceEntryPage.WaitForLoadingIcon();
                InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                InvoiceEntryPage.AcceptAlert(out string invoiceMsg1);
                Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);
                if (!InvoiceEntryPage.WaitForPopupWindowToClose())
                {
                    Assert.Fail($"Some error occurred while Submitting Fixable PPV Discrepant Invoice [{invNum}]");
                }
                menu.OpenNextPage(Pages.InvoiceDiscrepancy, Pages.DealerInvoices);
                DIPage = new DealerInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.DealerInvoices, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, Pages.DealerInvoices));
                DIPage.LoadDataOnGrid(invNum);
                Assert.AreEqual("Current", DIPage.GetFirstRowData(TableHeaders.TransactionStatus));
                Assert.AreEqual(100, InvoiceEntryUtils.GetInvoiceSectionCount(invNum));
                Console.WriteLine($"Successfully Submitted Fixable PPV Discrepant Invoice: [{invNum}]");
            }
            finally
            {
                CommonUtils.DeactivateTokenPPV();
            }
        }
    }
}
