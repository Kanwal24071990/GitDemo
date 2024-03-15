using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.POEntry;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using AutomationTesting_CorConnect.Utils.POEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.POEntry
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("PO Entry")]
    internal class POEntry : DriverBuilderClass
    {
        POEntryPage Page;
        POEntryAspx aspxPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.POEntry);
            Page = new POEntryPage(driver);
            aspxPage = new POEntryAspx(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20741" })]
        public void TC_20741(string UserType)
        {
            Page.OpenDropDown(FieldNames.FleetCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));
            Page.OpenDropDown(FieldNames.FleetCompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));
            Page.SelectValueFirstRow(FieldNames.FleetCompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));
            Page.OpenDropDown(FieldNames.DealerCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));
            Page.OpenDropDown(FieldNames.DealerCompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));
            Page.SelectValueFirstRow(FieldNames.DealerCompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));
            Page.OpenDatePicker(FieldNames.FromDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.OpenDatePicker(FieldNames.FromDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.SelectDate(FieldNames.FromDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.OpenDatePicker(FieldNames.ToDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
            Page.OpenDatePicker(FieldNames.ToDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
            Page.SelectDate(FieldNames.ToDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23412" })]
        public void TC_23412(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.POEntry), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.POEntry).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.FromDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.ToDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            List<string> gridValidatingErrors = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(gridValidatingErrors.Count == 3, ErrorMessages.RightPanelNotEmpty);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            POEntryUtils.GetDateData(out string from, out string to);
            Page.PopulateGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateNewPO));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string poq = Page.GetFirstRowData(TableHeaders.PO_);
                string dealerCode = Page.GetFirstRowData(TableHeaders.Dealer);
                Page.FilterTable(TableHeaders.PO_, poq);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Dealer, dealerCode), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Dealer, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.PO_, poq);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Dealer, dealerCode), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Dealer, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                Page.ButtonClick(ButtonsAndMessages.CreateNewPO);
                Page.SwitchToPopUp();

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.MandatoryField), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.MandatoryField));
                    Assert.IsTrue(aspxPage.IsDropDownClosed(FieldNames.POType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.POType));
                    Assert.IsTrue(aspxPage.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.DealerCode));
                    Assert.IsTrue(aspxPage.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.FleetCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.FleetPONumber), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.FleetPONumber));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PODate), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.PODate));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.SaveAndContinue), GetErrorMessage(ErrorMessages.ButtonMissing, ButtonsAndMessages.SaveAndContinue));
                });

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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25439" })]
        public void TC_25439(string UserType, string Fleet , string Dealer , string Part , int NumberOfSections)
        {
            Page.GridLoad();
            POEntryAspx POEntryAspxPage = Page.OpenCreateNewPO();
            string poNumber = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            string poType = "Rental";

            var errorMsgs = POEntryAspxPage.CreateNewPO(Fleet, Dealer, poNumber,poType);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections; i++)
            {
                POEntryAspxPage.Click(ButtonsAndMessages.AddSection);
                POEntryAspxPage.WaitForLoadingIcon();
                POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
                POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
                POEntryAspxPage.WaitForLoadingIcon();
            }

            POEntryAspxPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.Click(ButtonsAndMessages.Continue);
            POEntryAspxPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, invoiceMsg);
            if (!POEntryAspxPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Submitting PO:  [{poNumber}]");
            }

            Assert.AreEqual(NumberOfSections + 1, POEntryUtils.GetPOSectionCount(poNumber), "PO Section count from DB is not equal to expected count.");
            Console.WriteLine($"Successfully Submitting PO: [{poNumber}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25422" })]
        public void TC_25422(string UserType, string Fleet, string Dealer, string Part, int NumberOfSections)
        {
            Page.GridLoad();
            POEntryAspx POEntryAspxPage = Page.OpenCreateNewPO();
            string poNumber = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();

            var errorMsgs = POEntryAspxPage.CreateNewPO(Fleet, Dealer, poNumber);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections; i++)
            {
                POEntryAspxPage.Click(ButtonsAndMessages.AddSection);
                POEntryAspxPage.WaitForLoadingIcon();
                POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
                POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
                POEntryAspxPage.WaitForLoadingIcon();
            }

            POEntryAspxPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.Click(ButtonsAndMessages.Continue);
            POEntryAspxPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, invoiceMsg);
            if (!POEntryAspxPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Submitting PO:  [{poNumber}]");
            }

            Assert.AreEqual(NumberOfSections + 1, POEntryUtils.GetPOSectionCount(poNumber), "PO Section count from DB is not equal to expected count.");
            Console.WriteLine($"Successfully Submitting PO: [{poNumber}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25425" })]
        public void TC_25425(string UserType, string Fleet, string Dealer, string Part, int NumberOfSections)
        {
            Page.GridLoad();
            POEntryAspx POEntryAspxPage = Page.OpenCreateNewPO();
            string poNumber = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();

            var errorMsgs = POEntryAspxPage.CreateNewPO(Fleet, Dealer, poNumber);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections; i++)
            {
                POEntryAspxPage.Click(ButtonsAndMessages.AddSection);
                POEntryAspxPage.WaitForLoadingIcon();
                POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
                POEntryAspxPage.WaitForLoadingIcon();
            }

            POEntryAspxPage.Click(ButtonsAndMessages.SavePO);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.ClosePopupWindow();

            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.PO_, poNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.PO_);
            Page.SwitchToPopUp();

            POEntryAspxPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, NumberOfSections+1);
            POEntryAspxPage.AcceptAlert(out string poMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, poMsg);
            POEntryAspxPage.WaitForLoadingIcon();

            POEntryAspxPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, NumberOfSections);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            POEntryAspxPage.SetValue(FieldNames.CorePrice, "4.0000");
            POEntryAspxPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, NumberOfSections);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();

            POEntryAspxPage.Click(ButtonsAndMessages.AddSection);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.Click(ButtonsAndMessages.SavePO);
            POEntryAspxPage.WaitForLoadingIcon();
            Console.WriteLine($"Successfully Saved PO: [{poNumber}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25884" })]
        public void TC_25884(string UserType, string Fleet)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(Fleet, Constants.UserType.Fleet);
            menu.OpenPage(Pages.POEntry);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, Fleet));
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
