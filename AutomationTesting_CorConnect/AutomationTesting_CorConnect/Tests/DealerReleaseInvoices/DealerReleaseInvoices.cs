using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerReleaseInvoices;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerReleaseInvoices;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.DealerReleaseInvoices
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Release Invoices")]
    internal class DealerReleaseInvoices : DriverBuilderClass
    {
        DealerReleaseInvoicesPage Page;
        CreateNewInvoicePage popupPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerReleaseInvoices);
            Page = new DealerReleaseInvoicesPage(driver);
            popupPage = new CreateNewInvoicePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20745" })]
        public void TC_20745(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20690" })]
        public void TC_20690(string CompanyName, string DealerInvoiceNumber)
        {
            Page.PopulateGrid(CompanyName, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-90)), CommonUtils.GetCurrentDate());
            Page.FilterTable(TableHeaders.DealerInv__spc, DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPage.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Cancel));
                Assert.IsFalse(popupPage.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Reject));
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24213" })]
        public void TC_24213(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.DealerReleaseInvoices), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.FieldDisplayed, FieldNames.CompanyName));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.From), GetErrorMessage(ErrorMessages.FieldDisplayed, FieldNames.From));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.To), GetErrorMessage(ErrorMessages.FieldDisplayed, FieldNames.To));

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            DealerReleaseInvoicesUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {

                string dealerCode = Page.GetFirstRowData(TableHeaders.DealerCode);
                Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerCode, dealerCode), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerCode, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerCode, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);


                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.ClearSelection, ButtonsAndMessages.ReleaseInvoices, ButtonsAndMessages.MoveToHistory_));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));


                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();

                if (Page.GetRowCountCurrentPage() > 0)
                {
                    int rowCount = Page.GetRowCountCurrentPage();
                    int i = 0;
                    while (i < rowCount)
                    {
                        Page.CheckTableRowCheckBoxByIndex(++i);

                    }
                }
                else
                {
                    errorMsgs.Add("No rows found");

                }
                Page.ClearSelection();

                Page.FilterTable(TableHeaders.Status, "Awaiting");

                int filterRowCount = Page.GetRowCountCurrentPage();
                if (filterRowCount > 0)
                {
                    Page.CheckTableRowCheckBoxByIndex(1);
                    Page.ReleaseInvoices(out string msg1, out string msg2);

                    Assert.AreEqual(msg1, ButtonsAndMessages.ReleaseInvoiceAlert);
                    Assert.AreEqual(msg2, ButtonsAndMessages.ReleaseInvoiceSuccess);
                }

                    Page.WaitForGridLoad();

                    Page.ButtonClick(ButtonsAndMessages.Clear);
                    Page.AcceptAlert();
                    Page.WaitForLoadingMessage();
                    
                    DealerReleaseInvoicesUtils.GetData(out string from1, out string to1);
                    Page.LoadDataOnGrid(from1, to1);

                if (Page.IsAnyDataOnGrid())
                {
                    Page.MoveToHistory(out string Historymsg1, out string Historymsg2);

                    Assert.AreEqual(Historymsg1, ButtonsAndMessages.MoveToHistoryInvoiceAlert);
                    Assert.AreEqual(Historymsg2, ButtonsAndMessages.SelectInvoiceHistoryAlert);

                    Page.CheckTableRowCheckBoxByIndex(1);

                    Page.MoveToHistory(out string Historymsg3, out string Historymsg4);

                    Assert.AreEqual(Historymsg3, ButtonsAndMessages.MoveToHistoryInvoiceAlert);
                    Assert.AreEqual(Historymsg4, ButtonsAndMessages.MoveToHistorySuccess);
                }
                
                if (Page.IsAnyDataOnGrid())
                { 
                if (Page.IsNextPage())
                {   
                    Page.GoToPage(2);
                }
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25441" })]
        public void TC_25441(string UserType, string DealerName , string FleetName)
        {
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17DPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, 29, part));

            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInv__spc, invNum);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Page.SwitchToPopUp();

            Task t = Task.Run(() => popupPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            popupPage.Click(popupPage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!popupPage.IsCheckBoxChecked(popupPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => popupPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                popupPage.Click(popupPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }

            Assert.AreEqual(29, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            popupPage.Click(ButtonsAndMessages.AddSection);
            popupPage.WaitForLoadingIcon();
            popupPage.SearchAndSelectValue(FieldNames.Item, part.PartNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            popupPage.Click(ButtonsAndMessages.SaveLineItem);
            popupPage.WaitForLoadingIcon();

            popupPage.Click(ButtonsAndMessages.Release);
            popupPage.WaitForLoadingIcon();
            popupPage.Click(ButtonsAndMessages.Continue);
            popupPage.AcceptAlert(out string invoiceMsg1);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);

            if (!popupPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Releasing Awaiting Dealer Invoice [{invNum}]");
            }

            Assert.AreEqual(30, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            Console.WriteLine($"Successfully Released Awaiting Dealer Invoice: [{invNum}]");

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25894" })]
        public void TC_25894(string UserType, string DealerName)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(DealerName, Constants.UserType.Dealer);
            menu.OpenPage(Pages.DealerReleaseInvoices);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, DealerName));
            
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
