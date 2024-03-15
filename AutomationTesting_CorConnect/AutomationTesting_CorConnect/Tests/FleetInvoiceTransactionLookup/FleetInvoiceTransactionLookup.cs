using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.Disputes;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.FleetReleaseInvoices;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.PageObjects.ManageUsers;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Tests.PODiscrepancy;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.FleetInvoices;
using AutomationTesting_CorConnect.Utils.FleetInvoiceTransactionLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.FleetInvoiceTransactionLookup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Invoice Transaction Lookup")]
    internal class FleetInvoiceTransactionLookup : DriverBuilderClass
    {
        FleetInvoiceTransactionLookupPage Page;
        DealerInvoiceTransactionLookupPage DITLPage;
        InvoiceOptionsPage popupPage;
        InvoiceOptionsAspx invoiceOptionsAspxPage;
        CreateNewInvoicePage popupPageIE;
        FleetReleaseInvoicesPage FRIPage;
        DisputesPage disputePage;
        internal ApplicationContext appContext;


        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            Page = new FleetInvoiceTransactionLookupPage(driver);
            popupPage = new InvoiceOptionsPage(driver);
            popupPageIE = new CreateNewInvoicePage(driver); // Invoice Entry Popup page
            disputePage = new DisputesPage(driver);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20689" })]
        public void TC_20689(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddLineItem));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.DeleteSection));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddTax));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.UploadAttachments));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Reject));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Release));

            });

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20687" })]
        public void TC_20687(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddLineItem));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.DeleteSection));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddTax));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.UploadAttachments));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Reject));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Release));

            });

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20685" })]
        public void TC_20685(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.ButtonClick(ButtonsAndMessages.ChangeInvoice);
            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddLineItem));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.DeleteSection));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddTax));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.UploadAttachments));

            });

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20684" })]
        public void TC_20684(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            popupPage.ButtonClick(ButtonsAndMessages.ChangeInvoice);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.PurchaseOrderNumber), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PurchaseOrderNumber));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.Note1), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.Note1));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.Note2), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.Note2));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.UnitNumber), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.UnitNumber));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.VINNumber), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.VINNumber));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.VehicleID), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.VehicleID));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.VehicleMake), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.VehicleMake));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.VehicleYear), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.VehicleYear));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.VehicleMileage), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.VehicleMileage));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.VehicleLicense), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.VehicleLicense));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.Ref_), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.Ref_));
                Assert.IsTrue(popupPageIE.IsTextBoxEnabled(FieldNames.DeliveryReceipt), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.DeliveryReceipt));
            });

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20680" })]
        public void TC_20680(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddLineItem));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.DeleteSection));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddTax));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.UploadAttachments));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Reject));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Release));

            });

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20723" })]
        public void TC_20723(string UserType)
        {
            Page.SwitchToAdvanceSearch();
            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.SelectValueMultiSelectFirstRow(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));

            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.SelectValueMultiSelectFirstRow(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));

            Page.OpenMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));
            Page.OpenMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));
            Page.SelectValueMultiSelectFirstRow(FieldNames.TransactionStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));

            Page.OpenMultiSelectDropDown(FieldNames.Currency);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            Page.OpenMultiSelectDropDown(FieldNames.Currency);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            Page.SelectValueMultiSelectFirstRow(FieldNames.Currency);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20722" })]
        public void TC_20722(string UserType)
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
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.SelectLoadBookmarkFirstRow();
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

            Page.OpenDropDown(FieldNames.DocumentType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DocumentType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DocumentType));
            Page.OpenDropDown(FieldNames.DocumentType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DocumentType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DocumentType));
            Page.SelectValueFirstRow(FieldNames.DocumentType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DocumentType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DocumentType));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20678" })]
        public void TC_20678(string DealerInvoiceNumber)
        {
            string DealerInvoiceNumber1, DealerInvoiceNumber2;
            DealerInvoiceNumber1 = DealerInvoiceNumber.Split(',')[0];
            DealerInvoiceNumber2 = DealerInvoiceNumber.Split(',')[1];
            Page.LoadDataOnGrid(DealerInvoiceNumber1);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddLineItem));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.DeleteSection));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddTax));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.UploadAttachments));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Reject));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Release));

            });

            popupPageIE.ClosePopupWindow();
            menu.OpenNextPage(Pages.FleetInvoiceTransactionLookup, Pages.DealerInvoiceTransactionLookup);
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
            DITLPage.LoadDataOnGrid(DealerInvoiceNumber2);
            DITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SaveInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SaveInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Release));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Movetohistory), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Movetohistory));
                popupPageIE.CheckCheckBox(FieldNames.DoNotMoveToHistory);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotMoveToHistory), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotMoveToHistory));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotMoveToHistory);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotMoveToHistory), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotMoveToHistory));
                popupPageIE.CheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotPutHoldForDealerCopy));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotPutHoldForDealerCopy));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18293" })]
        public void TC_18293(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18298" })]
        public void TC_18298(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18312" })]
        public void TC_18312(string UserType)
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
            CommonUtils.UpdateCreateDateForInvoice(invoice1, -179);
            CommonUtils.UpdateCreateDateForInvoice(invoice2, -178);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18304" })]
        public void TC_18304(string UserType)
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
            CommonUtils.UpdateCreateDateForInvoice(invoice1, 0);
            CommonUtils.UpdateCreateDateForInvoice(invoice2, 1);
            CommonUtils.UpdateCreateDateForInvoice(invoice3, 2);
            CommonUtils.UpdateCreateDateForInvoice(invoice4, -6);
            CommonUtils.UpdateCreateDateForInvoice(invoice5, -5);
            CommonUtils.UpdateCreateDateForInvoice(invoice6, -4);
            Page.SwitchToAdvanceSearch();
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18305" })]
        public void TC_18305(string UserType)
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
            CommonUtils.UpdateCreateDateForInvoice(invoice1, 0);
            CommonUtils.UpdateCreateDateForInvoice(invoice2, 1);
            CommonUtils.UpdateCreateDateForInvoice(invoice3, -6);
            CommonUtils.UpdateCreateDateForInvoice(invoice4, -5);
            Page.SwitchToAdvanceSearch();
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18316" })]
        public void TC_18316(string UserType)
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

            CommonUtils.UpdateCreateDateForInvoice(invoice1, 0);
            CommonUtils.UpdateCreateDateForInvoice(invoice2, 1);
            CommonUtils.UpdateCreateDateForInvoice(invoice3, 2);
            CommonUtils.UpdateCreateDateForInvoice(invoice4, -6);
            CommonUtils.UpdateCreateDateForInvoice(invoice5, -5);
            CommonUtils.UpdateCreateDateForInvoice(invoice6, -4);
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

            CommonUtils.UpdateTransactionDateForInvoice(invoice1, 0);
            CommonUtils.UpdateTransactionDateForInvoice(invoice2, 1);
            CommonUtils.UpdateTransactionDateForInvoice(invoice3, 2);
            CommonUtils.UpdateTransactionDateForInvoice(invoice4, -6);
            CommonUtils.UpdateTransactionDateForInvoice(invoice5, -5);
            CommonUtils.UpdateTransactionDateForInvoice(invoice6, -4);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18317" })]
        public void TC_18317(string UserType)
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

            CommonUtils.UpdateCreateDateForInvoice(invoice1, 0);
            CommonUtils.UpdateCreateDateForInvoice(invoice2, 1);
            CommonUtils.UpdateCreateDateForInvoice(invoice3, -6);
            CommonUtils.UpdateCreateDateForInvoice(invoice4, -5);
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

            CommonUtils.UpdateTransactionDateForInvoice(invoice1, 0);
            CommonUtils.UpdateTransactionDateForInvoice(invoice2, 1);
            CommonUtils.UpdateTransactionDateForInvoice(invoice3, -6);
            CommonUtils.UpdateTransactionDateForInvoice(invoice4, -5);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18323" })]
        [NonParallelizable]
        public void TC_18323(string DealerInvoiceNumber)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20672" })]
        public void TC_20672(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddLineItem));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.DeleteSection));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddTax));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.UploadAttachments));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Reject));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Release));

            });

            popupPageIE.ClosePopupWindow();
            menu.OpenNextPage(Pages.FleetInvoiceTransactionLookup, Pages.FleetReleaseInvoices);
            FRIPage = new FleetReleaseInvoicesPage(driver);
            FRIPage.ButtonClick(ButtonsAndMessages.Search);
            FRIPage.WaitForMsg(ButtonsAndMessages.PleaseWait);
            FRIPage.FilterTable(TableHeaders.DealerInvoiceNumber, DealerInvoiceNumber);
            FRIPage.ClickHyperLinkOnGrid(TableHeaders.DealerInvoiceNumber);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddLineItem));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.DeleteSection));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.AddTax));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.UploadAttachments));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Reject));
                Assert.IsFalse(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Release));

            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21407" })]
        public void TC_21407(string DealerInvoiceNumber, string User)
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
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            Page = new FleetInvoiceTransactionLookupPage(driver);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23309" })]
        public void TC_23309(string UserType)
        {
            Assert.Multiple(() =>
            {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.FleetInvoiceTransactionLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.FleetInvoiceTransactionLookup).ForEach(x => { Assert.Fail(x); });
            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            FleetInvoiceTransactionLookupUtil.GetData(out string from, out string to);
            Page.PopulateGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                   
                    string fleetCode = Page.GetFirstRowData(TableHeaders.FleetCode);
                    Page.FilterTable(TableHeaders.FleetCode, fleetCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.FleetCode, fleetCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ResetNotWorking);
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25017" })]
        public void TC_25017(string UserType)
        {

            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var user = client.Users.First(u => u.Type == "admin");
            var currentUser = user.User;
            string errorMsg = "Count mismatch for combination T Status[{0}] DateType[{1}] DateRange[{2}]";


            int transactionStatusAuthorizedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Authorized");
            int transactionStatusAuthorizationVoidedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Authorization Voided");
            int transactionStatusAuthorizationExpiredCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Authorization Expired");
            int transactionStatusAwaitingFleetReleaseCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Awaiting Fleet Release");
            int transactionStatusAwaitingResellerReleaseCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Awaiting Reseller Release");
            int transactionStatusFleetReleaseRejectedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Fleet Release Rejected");
            int transactionStatusResellerReleaseRejectedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Reseller Release Rejected");
            int transactionStatusCurrentDisputeCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current-Disputed");
            int transactionStatusCurrentResolvedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current-Resolved");
            int transactionStatusCurrentClosedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current-Closed");
            int transactionStatusPaidCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Paid");
            int transactionStatusPaidClosedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Paid-Closed");
            int transactionStatusPaidDisputedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Paid-Disputed");
            int transactionStatusPaidResolvedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Paid-Resolved");
            int transactionStatusPastDueHoldCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Past due-Hold");
            int transactionStatusCurrentCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current");
            int transactionStatusPastdueCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Past due");
            int transactionStatusPastdueClosedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Past due-Closed");
            int transactionStatusCurrentHoldCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current-Hold");
            int transactionStatusCurrentHoldReleasedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current-Hold Released");
            int transactionStatusPastDueDisputedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Past due-Disputed");
            int transactionStatusPastDueResolvedCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Past due-Resolved");
            int transactionStatusPastDueHoldReleaseCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Past due-Hold Released");
            int transactionStatusPayInProgressCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Pay-In Progress");
            int transactionStatusPayPendingCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Pay-Pending");
            int transactionStatusFixableDiscrepancyCount = FleetInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Fixable Discrepancy");

            Page.SwitchToAdvanceSearch();

            Assert.Multiple(() =>
            {
                Page.SelectTransactionStatusAndDateType("Authorized", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAuthorizedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Authorized", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Authorized");
                Page.SelectTransactionStatusAndDateType("Authorization Voided", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAuthorizationVoidedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Authorization Voided", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Authorization Voided");
                Page.SelectTransactionStatusAndDateType("Authorization Expired", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAuthorizationExpiredCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Authorization Expired", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Authorization Expired");
                Page.SelectTransactionStatusAndDateType("Awaiting Fleet Release", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAwaitingFleetReleaseCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Awaiting Fleet Release", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Awaiting Fleet Release");
                Page.SelectTransactionStatusAndDateType("Awaiting Reseller Release", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAwaitingResellerReleaseCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Awaiting Reseller Release", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Awaiting Reseller Release");
                Page.SelectTransactionStatusAndDateType("Fleet Release Rejected", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusFleetReleaseRejectedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Fleet Release Rejected", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Fleet Release Rejected");
                Page.SelectTransactionStatusAndDateType("Reseller Release Rejected", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusResellerReleaseRejectedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Reseller Release Rejected", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Reseller Release Rejected");
                Page.SelectTransactionStatusAndDateType("Current-Disputed", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentDisputeCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current-Disputed", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Disputed");
                Page.SelectTransactionStatusAndDateType("Current-Resolved", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentResolvedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current-Resolved", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Resolved");
                Page.SelectTransactionStatusAndDateType("Current-Closed", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentClosedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current-Closed", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Closed");
                Page.SelectTransactionStatusAndDateType("Paid", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Paid", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid");
                Page.SelectTransactionStatusAndDateType("Paid-Closed", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidClosedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Paid-Closed", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid-Closed");
                Page.SelectTransactionStatusAndDateType("Paid-Disputed", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidDisputedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Paid-Disputed", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid-Disputed");
                Page.SelectTransactionStatusAndDateType("Paid-Resolved", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidResolvedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Paid-Resolved", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid-Resolved");
                Page.SelectTransactionStatusAndDateType("Past due-Hold", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastDueHoldCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Past due-Hold", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Hold");
                Page.SelectTransactionStatusAndDateType("Current", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current");
                Page.SelectTransactionStatusAndDateType("Past due", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastdueCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Past due", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due");
                Page.SelectTransactionStatusAndDateType("Past due-Closed", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastdueClosedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Past due-Closed", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Closed");
                Page.SelectTransactionStatusAndDateType("Current-Hold", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentHoldCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current-Hold", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Hold");
                Page.SelectTransactionStatusAndDateType("Current-Hold Released", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentHoldReleasedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current-Hold Released", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Hold Released");
                Page.SelectTransactionStatusAndDateType("Past due-Disputed", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastDueDisputedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Past due-Disputed", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Disputed");
                Page.SelectTransactionStatusAndDateType("Past due-Resolved", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastDueResolvedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Past due-Resolved", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Resolved");
                Page.SelectTransactionStatusAndDateType("Past due-Hold Released", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPastDueResolvedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Past due-Hold Released", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Past due-Hold Released");
                Page.SelectTransactionStatusAndDateType("Pay-In Progress", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPayInProgressCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Pay-In Progress", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Pay-In Progress");
                Page.SelectTransactionStatusAndDateType("Pay-Pending", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPayPendingCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Pay-Pending", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Pay-Pending");
                Page.SelectTransactionStatusAndDateType("Fixable Discrepancy", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusFixableDiscrepancyCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Fixable Discrepancy", "System received date Dealer", "Last 7 days"));
                }
                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Fixable Discrepancy");
                InvoiceObject invoiceDetails = FleetInvoicesUtils.GetInvoiceInfoFromTransactionStatus();
                string dealerInvoiceNumber = invoiceDetails.TransactionNumber;
                string originatingDocumentNumber = invoiceDetails.OriginatingDocumentNumber;
                string poNumber = invoiceDetails.PurchaseOrderNumber;
                string invoiceNumber = invoiceDetails.InvoiceNumber;

                Page.EnterTextAfterClear(FieldNames.DealerInvoiceNumber, dealerInvoiceNumber);
                Page.EnterTextAfterClear(FieldNames.OriginatingDocumentNumber, originatingDocumentNumber);
                Page.EnterTextAfterClear(FieldNames.PONumber, poNumber);
                Page.EnterTextAfterClear(FieldNames.ProgramInvoiceNumber, invoiceNumber);
                Page.SelectTransactionStatusAndDateType("Current-Closed", "Program invoice date", "Customized date", -29);

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

                EntityDetails entityDetails1 = FleetInvoicesUtils.GetEntityGroup(currentUser);
                var fleetGroup = entityDetails1.FleetGroup;
                var dealerGroup = entityDetails1.DealerGroup;
                int groupCount = FleetInvoicesUtils.GetInvoicesCountByGroup(currentUser, dealerGroup, fleetGroup);
                Page.EnterTextDropDown(FieldNames.DealerGroup, dealerGroup);
                Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
                Page.EnterTextDropDown(FieldNames.FleetGroup, fleetGroup);
                Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
                Page.SelectTransactionStatusAndDateType("Current-Resolved", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(groupCount, int.Parse(gridCount));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25701" })]
        public void TC_25701(string UserType, string DealerName, string FleetName)
        {
            string invoiceNumber = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoiceNumber, DealerName, FleetName), ErrorMessages.DMSInvoiceCreationFailed);
            Page.LoadDataOnGrid(invoiceNumber);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ReverseInvoice(invoiceNumber));
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25702" })]
        public void TC_25702(string UserType, string DealerName, string FleetName)
        {
            string invoiceNumber = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoiceNumber, DealerName, FleetName), "Failed to submit invoice via DMS Services.");
            Page.LoadDataOnGrid(invoiceNumber);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.CloneInvoice(TableHeaders.DealerInv__spc));
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25705" })]
        public void TC_25705(string UserType, string DealerName, string FleetName)
        {
            string invoiceNumber = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoiceNumber, DealerName, FleetName), ErrorMessages.DMSInvoiceCreationFailed);
            Page.LoadDataOnGrid(invoiceNumber);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.CreateDispute(invoiceNumber, TableHeaders.DealerInv__spc));
            Page.LoadDataOnGrid(invoiceNumber);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
            errorMsgs.AddRange(Page.ResolveDispute(invoiceNumber, TableHeaders.DealerInv__spc));
            Page.LoadDataOnGrid(invoiceNumber);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
            errorMsgs.AddRange(Page.ReDispute(invoiceNumber, TableHeaders.DealerInv__spc));
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25777"})]
        public void TC_25777(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
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
