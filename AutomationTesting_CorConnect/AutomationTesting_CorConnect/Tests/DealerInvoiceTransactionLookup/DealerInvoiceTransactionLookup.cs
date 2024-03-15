using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.PageObjects.ManageUsers;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Tests.Disputes;
using AutomationTesting_CorConnect.Tests.PODiscrepancy;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.Utils.FleetInvoices;
using AutomationTesting_CorConnect.Utils.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.DealerInvoiceTransactionLookup
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Invoice Transaction Lookup")]
    internal class DealerInvoiceTransactionLookup : DriverBuilderClass
    {
        DealerInvoiceTransactionLookupPage Page;
        InvoiceOptionsPage popupPage;
        CreateNewInvoicePage popupPageIE;
        internal ApplicationContext appContext;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
            Page = new DealerInvoiceTransactionLookupPage(driver);
            popupPage = new InvoiceOptionsPage(driver);
            popupPageIE = new CreateNewInvoicePage(driver); // Invoice Entry Popup page
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20669" })]
        public void TC_20669(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.IsFalse(popupPage.IsButtonEnabled(ButtonsAndMessages.CreateRebill), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.CreateRebill));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20673" })]
        public void TC_20673(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.InvoiceHistory), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.InvoiceHistory));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.AddLineItem));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.DeleteSection));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.AddTax));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.UploadAttachments));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Save));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SaveInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SaveInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SubmitInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SubmitInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Movetohistory), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Movetohistory));
            });


        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20675" })]
        public void TC_20675(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.InvoiceHistory), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.InvoiceHistory));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddLineItem), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.AddLineItem));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteSection), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.DeleteSection));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.AddTax), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.AddTax));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.UploadAttachments), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.UploadAttachments));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Save));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SaveInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SaveInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SubmitInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SubmitInvoice));
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20683" })]
        public void TC_20683(string DealerInvoiceNumber)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20670" })]
        public void TC_20670(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            Assert.IsTrue(popupPage.IsButtonEnabled(ButtonsAndMessages.CreateRebill), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.CreateRebill));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20671" })]
        public void TC_20671(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPage.SwitchIframe();
            Assert.IsTrue(popupPage.IsButtonEnabled(ButtonsAndMessages.CreateRebill), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.CreateRebill));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20676" })]
        public void TC_20676(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                popupPageIE.CheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotPutHoldForDealerCopy));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotPutHoldForDealerCopy));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SaveInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SaveInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SubmitInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SubmitInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.DeleteInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.DeleteInvoice));
            });


        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20677" })]
        public void TC_20677(string DealerInvoiceNumber)
        {
            var invoiceNumSplit = DealerInvoiceNumber.Split(",");
            string fixableDiscrepany = invoiceNumSplit[0];
            string fatalDiscrepancy = invoiceNumSplit[1];
            string reviewableDiscrepancy = invoiceNumSplit[2];

            Page.LoadDataOnGrid(fixableDiscrepany);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                popupPageIE.CheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotPutHoldForDealerCopy));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotPutHoldForDealerCopy));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Movetohistory), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Movetohistory));
                popupPageIE.CheckCheckBox(FieldNames.DoNotMoveToHistory);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotMoveToHistory), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotMoveToHistory));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotMoveToHistory);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotMoveToHistory), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotMoveToHistory));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SaveInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SaveInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SubmitInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SubmitInvoice));

            });

            popupPageIE.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(fatalDiscrepancy);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                popupPageIE.CheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotPutHoldForDealerCopy));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotPutHoldForDealerCopy));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Movetohistory), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Movetohistory));
                popupPageIE.CheckCheckBox(FieldNames.DoNotMoveToHistory);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotMoveToHistory), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotMoveToHistory));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotMoveToHistory);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotMoveToHistory), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotMoveToHistory));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SaveInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SaveInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SubmitInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SubmitInvoice));

            });

            popupPageIE.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(reviewableDiscrepancy);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.Multiple(() =>
            {
                popupPageIE.CheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotPutHoldForDealerCopy));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotPutHoldForDealerCopy), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotPutHoldForDealerCopy));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.Movetohistory), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Movetohistory));
                popupPageIE.CheckCheckBox(FieldNames.DoNotMoveToHistory);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotMoveToHistory), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.DoNotMoveToHistory));
                popupPageIE.UncheckCheckBox(FieldNames.DoNotMoveToHistory);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.DoNotMoveToHistory), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DoNotMoveToHistory));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SaveInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SaveInvoice));
                Assert.IsTrue(popupPageIE.IsButtonEnabled(ButtonsAndMessages.SubmitInvoice), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SubmitInvoice));

            });



        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20686" })]
        public void TC_20686(string DealerInvoiceNumber)
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

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20662" })]
        public void TC_20662(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20661" })]
        public void TC_20661(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20681" })]
        public void TC_20681(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            CommonUtils.ActivateTokenPPV();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(popupPageIE.IsTextVisible("Admin Error: Pending Price Validation from Daimler ", true));
                Assert.IsTrue(popupPageIE.IsCheckBoxDisplayed(FieldNames.AdminOverride));
                popupPageIE.CheckCheckBox(FieldNames.AdminOverride);
                Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.AdminOverride), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.AdminOverride));
                popupPageIE.UncheckCheckBox(FieldNames.AdminOverride);
                Assert.IsFalse(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.AdminOverride), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.AdminOverride));

            });
            CommonUtils.DeactivateTokenPPV();
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20674" })]
        public void TC_20674(string DealerInvoiceNumber)
        {
            string DealerInvoiceNumber1, DealerInvoiceNumber2;
            DealerInvoiceNumber1 = DealerInvoiceNumber.Split(',')[0];
            DealerInvoiceNumber2 = DealerInvoiceNumber.Split(',')[1];
            Page.LoadDataOnGrid(DealerInvoiceNumber1);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.IsFalse(popupPageIE.IsCheckBoxDisplayed(FieldNames.AdminOverride));

            popupPageIE.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClearText(FieldNames.DealerInvoiceNumber);
            Page.LoadDataOnGrid(DealerInvoiceNumber2);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            Assert.IsFalse(popupPageIE.IsCheckBoxDisplayed(FieldNames.AdminOverride));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18295" })]
        public void TC_18295(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18296" })]
        public void TC_18296(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18310" })]
        public void TC_18310(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18300" })]
        public void TC_18300(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18301" })]
        public void TC_18301(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18314" })]
        public void TC_18314(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18315" })]
        public void TC_18315(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "18322" })]
        [NonParallelizable]
        public void TC_18322(string DealerInvoiceNumber)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20682" })]
        public void TC_20682(string DealerInvoiceNumber, string dealerCode, string fleetCode)
        {
            CommonUtils.ActivateTokenPPV();
            string invoice1 = CommonUtils.RandomString(8);
            Assert.IsTrue(new DMSServices().SubmitInvoiceWithPPV(invoice1, dealerCode, fleetCode), string.Format(ErrorMessages.FailedToSubmitInvoice, invoice1));
            Page.LoadDataOnGrid(invoice1);
            string transactionStatus = Page.GetFirstRowData(TableHeaders.TransactionStatus);
            Assert.AreEqual("Fixable Discrepancy", transactionStatus);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            popupPageIE.CheckCheckBox(FieldNames.AdminOverride);
            Assert.IsTrue(popupPageIE.IsCheckBoxCheckedWithLabel(FieldNames.AdminOverride), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.AdminOverride));
            popupPageIE.ButtonClick(ButtonsAndMessages.SubmitInvoice);
            popupPageIE.WaitForLoadingGrid();
            popupPageIE.Click(ButtonsAndMessages.Continue);
            popupPageIE.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully);
            Page.SwitchToMainWindow();
            Page.LoadDataOnGrid(invoice1);
            transactionStatus = Page.GetFirstRowData(TableHeaders.TransactionStatus);
            Assert.AreEqual("Awaiting Reseller Release", transactionStatus);
            CommonUtils.DeactivateTokenPPV();
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21402" })]
        public void TC_21402(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGridWithFilter(DealerInvoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(DealerInvoiceNumber);
            Assert.IsEmpty(invoiceObj.APPaidDate);
            Assert.AreEqual(" ", Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, DealerInvoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, DealerInvoiceNumber));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21399" })]
        public void TC_21399(string DealerInvoiceNumber, string User)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21398" })]
        public void TC_21398(string DealerInvoiceNumber, string User)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21390" })]
        public void TC_21390(string DealerInvoiceNumber, string User)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();

            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual("", Page.GetFirstRowData(TableHeaders.DatePaid).Trim(), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
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
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21383" })]
        public void TC_21383(string DealerInvoiceNumber)
        {
            Page.LoadDataOnGrid(DealerInvoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(DealerInvoiceNumber);
            Assert.IsEmpty(invoiceObj.APPaidDate);
            Assert.AreEqual(" ", Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, DealerInvoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, DealerInvoiceNumber));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21382" })]
        public void TC_21382(string DealerInvoiceNumber)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();

            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual("", Page.GetFirstRowData(TableHeaders.DatePaid).Trim(), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGrid(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNumber);
            Assert.AreEqual(invoiceObj.APPaidDate, Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19314" })]
        public void TC_19314(string DealerInvoiceNumber)
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21389" })]
        public void TC_21389(string DealerInvoiceNumber, string User)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();
            menu.OpenPage(Pages.ManageUsers);
            ManageUsersPage manageUsers = new ManageUsersPage(driver);
            manageUsers.ImpersonateUser(User);
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
            Page = new DealerInvoiceTransactionLookupPage(driver);

            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual("Current", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));
            Page.ClearFilter();

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21391" })]
        public void TC_21391(string DealerInvoiceNumber, string User)
        {
            List<string> dealerInvoceNumbers = DealerInvoiceNumber.Split(',').ToList();
            menu.OpenPage(Pages.ManageUsers);
            ManageUsersPage manageUsers = new ManageUsersPage(driver);
            manageUsers.ImpersonateUser(User);
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
            Page = new DealerInvoiceTransactionLookupPage(driver);

            string invoiceNumber = dealerInvoceNumbers[0];
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual("Current", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));
            Page.ClearFilter();

            invoiceNumber = dealerInvoceNumbers[1];
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreNotEqual("Paid", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21406" })]
        public void TC_21406(string DealerInvoiceNumber, string User)
        {
            menu.OpenPage(Pages.ManageUsers);
            ManageUsersPage manageUsers = new ManageUsersPage(driver);
            manageUsers.ImpersonateUser(User);
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
            Page = new DealerInvoiceTransactionLookupPage(driver);

            string invoiceNumber = DealerInvoiceNumber;
            Page.LoadDataOnGridWithFilter(invoiceNumber);
            Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual("Current", Page.GetFirstRowData(TableHeaders.TransactionStatus), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.TransactionStatus, invoiceNumber));
            Assert.AreEqual(" ", Page.GetFirstRowData(TableHeaders.DatePaid), GetErrorMessage(ErrorMessages.IncorrectDataForInvoice, TableHeaders.DatePaid, invoiceNumber));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23308" })]
        public void TC_23308(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.DealerInvoiceTransactionLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.DealerInvoiceTransactionLookup).ForEach(x => { Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());
               
                DealerInvoiceTransactionLookupUtil.GetData(out string from, out string to);
                Page.PopulateGrid(from, to);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                    

                    string dealerCode = Page.GetFirstRowData(TableHeaders.DealerCode);
                    Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerCode, dealerCode), ErrorMessages.NoRowAfterFilter);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerCode, dealerCode), ErrorMessages.NoRowAfterFilter);
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16067" })]
        public void TC_16067(string UserType)
        {
            CommonUtils.GetInvoicebyTransactionStatus(2, out string transactioNumber2);
            Page.LoadDataOnGrid(transactioNumber2);
            Page.FilterTable(TableHeaders.TransactionStatus, "Fixable Discrepancy");
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.IsTrue(popupPageIE.IsElementVisible(FieldNames.OverrideInvoiceValidity));
            popupPageIE.ClosePopupWindow();
            popupPageIE.SwitchToMainWindow();

            CommonUtils.GetInvoicebyTransactionStatus(3, out string transactioNumber3);
            Page.LoadDataOnGrid(transactioNumber3);
            Page.FilterTable(TableHeaders.TransactionStatus, "Fatal Discrepancy");
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.IsTrue(popupPageIE.IsElementVisible(FieldNames.OverrideInvoiceValidity));
            popupPageIE.ClosePopupWindow();
            popupPageIE.SwitchToMainWindow();

            CommonUtils.GetInvoicebyTransactionStatus(4, out string transactioNumber4);
            Page.LoadDataOnGrid(transactioNumber4);
            Page.FilterTable(TableHeaders.TransactionStatus, "Reviewable Discrepancy");
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.IsTrue(popupPageIE.IsElementVisible(FieldNames.OverrideInvoiceValidity));
            popupPageIE.ClosePopupWindow();
            popupPageIE.SwitchToMainWindow();

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16068" })]
        public void TC_16068(string UserType)
        {
            CommonUtils.GetInvoicebyTransactionStatus(2, out string transactioNumber2);
            Page.LoadDataOnGrid(transactioNumber2);
            Page.FilterTable(TableHeaders.TransactionStatus, "Fixable Discrepancy");
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.IsFalse(popupPageIE.IsElementVisible(FieldNames.OverrideInvoiceValidity));
            popupPageIE.ClosePopupWindow();
            popupPageIE.SwitchToMainWindow();

            CommonUtils.GetInvoicebyTransactionStatus(3, out string transactioNumber3);
            Page.LoadDataOnGrid(transactioNumber3);
            Page.FilterTable(TableHeaders.TransactionStatus, "Fatal Discrepancy");
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.IsFalse(popupPageIE.IsElementVisible(FieldNames.OverrideInvoiceValidity));
            popupPageIE.ClosePopupWindow();
            popupPageIE.SwitchToMainWindow();

            CommonUtils.GetInvoicebyTransactionStatus(4, out string transactioNumber4);
            Page.LoadDataOnGrid(transactioNumber4);
            Page.FilterTable(TableHeaders.TransactionStatus, "Reviewable Discrepancy");
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.IsFalse(popupPageIE.IsElementVisible(FieldNames.OverrideInvoiceValidity));
            popupPageIE.ClosePopupWindow();
            popupPageIE.SwitchToMainWindow();

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25019" })]
        public void TC_25019(string UserType)
        {
            appContext = ApplicationContext.GetInstance();
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            var user = client.Users.First(u => u.Type == "admin");
            var currentUser = user.User;
            string errorMsg = "Count mismatch for combination T Status[{0}] DateType[{1}] DateRange[{2}]";

            int transactionStatusAuthorizedCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Authorized");
            int transactionStatusAuthorizationExpiredCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Authorization Expired");
            int transactionStatusAuthorizationVoidedCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Authorization Voided");
            int transactionStatusAwaitingFleetReleaseCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Awaiting Fleet Release");
            int transactionStatusAwaitingResellerReleaseCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Awaiting Reseller Release");
            int transactionStatusFleetReleaseRejectedCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Fleet Release Rejected");
            int transactionStatusResellerReleaseRejectedCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Reseller Release Rejected");
            int transactionStatusPaidCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Paid");
            int transactionStatusCurrentDisputedCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current-Disputed");
            int transactionStatusAwaitingDealerReleaseCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Awaiting Dealer Release");
            int transactionStatusDraftInvoiceCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Draft Invoice");
            int transactionStatusFatalDiscrepancyCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Fatal Discrepancy");
            int transactionStatusFixableDiscrepancyCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Fixable Discrepancy");
            int transactionStatusReviewableDiscrepancyCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Reviewable Discrepancy");
            int transactionStatusAdminErrorCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Admin Error");
            int transactionStatusCurrentCount = DealerInvoiceTransactionLookupUtil.GetRowCountTransactionStatus("Current");

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
                Page.SelectTransactionStatusAndDateType("Authorization Expired", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAuthorizationExpiredCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Authorization Expired", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Authorization Expired");
                Page.SelectTransactionStatusAndDateType("Authorization Voided", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAuthorizationVoidedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Authorization Voided", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Authorization Voided");
                Page.SelectTransactionStatusAndDateType("Awaiting Fleet Release", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAwaitingFleetReleaseCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Awaiting Fleet Release", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Awaiting Fleet Release");
                Page.SelectTransactionStatusAndDateType("Awaiting Reseller Release", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAwaitingResellerReleaseCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Awaiting Reseller Release", "System received date Dealer", "Last 7 days"));
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
                Page.SelectTransactionStatusAndDateType("Paid", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusPaidCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Paid", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Paid");
                Page.SelectTransactionStatusAndDateType("Current-Disputed", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentDisputedCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current-Disputed", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current-Disputed");
                Page.SelectTransactionStatusAndDateType("Awaiting Dealer Release", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAwaitingDealerReleaseCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Awaiting Dealer Release", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Awaiting Dealer Release");
                Page.SelectTransactionStatusAndDateType("Draft Invoice", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusDraftInvoiceCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Draft Invoice", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Draft Invoice");
                Page.SelectTransactionStatusAndDateType("Fatal Discrepancy", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusFatalDiscrepancyCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Fatal Discrepancy", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Fatal Discrepancy");
                Page.SelectTransactionStatusAndDateType("Fixable Discrepancy", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusFixableDiscrepancyCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Fixable Discrepancy", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Fixable Discrepancy");
                Page.SelectTransactionStatusAndDateType("Reviewable Discrepancy", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusReviewableDiscrepancyCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Reviewable Discrepancy", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Reviewable Discrepancy");
                Page.SelectTransactionStatusAndDateType("Admin Error", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusAdminErrorCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Admin Error", "System received date Dealer", "Customized date"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Admin Error");
                Page.SelectTransactionStatusAndDateType("Current", "System received date Dealer", "Last 7 days", 0);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(transactionStatusCurrentCount, int.Parse(gridCount), GetErrorMessage(errorMsg, "Current", "System received date Dealer", "Last 7 days"));
                }

                Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current");
                InvoiceObject invoiceDetails = DealerInvoiceTransactionLookupUtil.GetInvoiceInfoFromTransactionStatus();
                string dealerInvoiceNumber = invoiceDetails.TransactionNumber;
                string originatingDocumentNumber = invoiceDetails.OriginatingDocumentNumber;
                string poNumber = invoiceDetails.PurchaseOrderNumber;
                string invoiceNumber = invoiceDetails.InvoiceNumber;

                Page.EnterTextAfterClear(FieldNames.DealerInvoiceNumber, dealerInvoiceNumber);
                Page.EnterTextAfterClear(FieldNames.OriginatingDocumentNumber, originatingDocumentNumber);
                Page.EnterTextAfterClear(FieldNames.PONumber, poNumber);
                Page.EnterTextAfterClear(FieldNames.ProgramInvoiceNumber, invoiceNumber);
                Page.SelectTransactionStatusAndDateType("Current", "System received date Dealer", "Customized date", -30);

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
                Page.SelectTransactionStatusAndDateType("Current-Disputed", "System received date Dealer", "Customized date", -30);
                if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
                {
                    string gridCount = Page.GetPageCounterTotal();
                    Assert.AreEqual(groupCount, int.Parse(gridCount));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25776" })]
        public void TC_25776(string UserType, string dealerUser)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25664" })]
        public void TC_25664(string UserType,string dealerCode, string fleetCode, string partNumber)
        {
            List<string> dealerCodes = dealerCode.Split(',').ToList();
            List<string> fleetCodes = fleetCode.Split(',').ToList();
            string dealerName = dealerCodes[0];
            string fleetName = fleetCodes[0];
            string rrValue = CommonUtils.RandomString(5);
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = partNumber,
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvMultipleSectionsWithReferenceType(invNum, dealerName, fleetName, 100, part, rrValue));
            Console.WriteLine($"Successfully Created 1st Invoice: [{invNum}]");

            dealerName = dealerCodes[1];
            fleetName = fleetCodes[1];

            invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Assert.IsTrue(new DMSServices().SubmitInvMultipleSectionsWithReferenceType(invNum, dealerName, fleetName, 100, part, rrValue));
            Console.WriteLine($"Successfully Created 2nd Invoice: [{invNum}]");
            Page.SwitchToAdvanceSearch();

            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Awaiting Reseller Release");
            Page.EnterTextAfterClear(FieldNames.DealerInvoiceNumber, invNum);
            Page.LoadDataOnGrid();

            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Page.SwitchToPopUp();

            Task t = Task.Run(() => popupPageIE.WaitForStalenessOfElement(FieldNames.UnitNumber));
            popupPageIE.Click(popupPageIE.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!popupPageIE.IsCheckBoxChecked(popupPageIE.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => popupPageIE.WaitForStalenessOfElement(FieldNames.UnitNumber));
                popupPageIE.Click(popupPageIE.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }

            Assert.AreEqual(100, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            popupPageIE.ClickElementByIndex(ButtonsAndMessages.DeleteSection, 100);
            popupPageIE.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, invoiceMsg);
            popupPageIE.WaitForLoadingIcon();

            popupPageIE.ClickElementByIndex(ButtonsAndMessages.EditLineItem, 99);
            popupPageIE.WaitForLoadingIcon();
            popupPageIE.SelectValueByScroll(FieldNames.Type, "Variable");
            popupPageIE.WaitForLoadingIcon();
            popupPageIE.EnterText(FieldNames.Item, "Variable Auto", ButtonsAndMessages.Edit);
            popupPageIE.SetValue(FieldNames.UnitPrice, "4.0000");
            popupPageIE.SetValue(FieldNames.CorePrice, "4.0000");
            popupPageIE.EnterTextAfterClear(FieldNames.ItemDescription, "Variable Desc");
            popupPageIE.Click(ButtonsAndMessages.SaveLineItem);
            popupPageIE.WaitForLoadingIcon();

            popupPageIE.ClickElementByIndex(ButtonsAndMessages.AddLineItem, 99);
            popupPageIE.WaitForLoadingIcon();
            popupPageIE.SelectValueByScroll(FieldNames.Type, "Rental");
            popupPageIE.WaitForLoadingIcon();
            popupPageIE.EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
            popupPageIE.EnterText(FieldNames.ItemDescription, "Rental Auto description");
            popupPageIE.SetValue(FieldNames.UnitPrice, "100.0000");
            popupPageIE.Click(ButtonsAndMessages.SaveLineItem);
            popupPageIE.WaitForLoadingIcon();

            popupPageIE.Click(ButtonsAndMessages.AddSection);
            popupPageIE.WaitForLoadingIcon();
            popupPageIE.SelectValueByScroll(FieldNames.Type, "Tax");
            popupPageIE.WaitForLoadingIcon();
            popupPageIE.EnterText(FieldNames.Item, "Tax Auto", TableHeaders.New);
            popupPageIE.EnterText(FieldNames.ItemDescription, "Tax Auto description");
            popupPageIE.SetValue(FieldNames.UnitPrice, "50.0000");
            popupPageIE.Click(ButtonsAndMessages.SaveLineItem);
            popupPageIE.WaitForLoadingIcon();

            popupPageIE.Click(ButtonsAndMessages.SubmitInvoice);
            popupPageIE.WaitForLoadingIcon();
            popupPageIE.Click(ButtonsAndMessages.Continue);
            popupPageIE.AcceptAlert(out string invoiceMsg1);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);
            popupPageIE.SwitchToMainWindow();
            Page.LoadDataOnGrid();
            Assert.AreEqual("Awaiting Reseller Release", Page.GetFirstRowData(TableHeaders.TransactionStatus));

            JobHelper.ExecuteJob("SettleResellerTransactions", "c1qsrs01-uspa01");
            Page.SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Awaiting Reseller Release");
            Page.LoadDataOnGrid(); 
            Assert.AreEqual("Current",Page.GetFirstRowData(TableHeaders.TransactionStatus));
            Assert.AreEqual(100, InvoiceEntryUtils.GetInvoiceSectionCount(invNum));
        }

    }
}
