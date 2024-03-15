using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.POQEntry;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.POEntry;
using AutomationTesting_CorConnect.Utils.POQEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.POQEntry
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("POQ Entry")]
    internal class POQEntry : DriverBuilderClass
    {
        POQEntryPage Page;
        POQEntryAspx aspxPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.POQEntry);
            Page = new POQEntryPage(driver);
            aspxPage = new POQEntryAspx(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20742" })]
        public void TC_20742(string UserType)
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23413" })]
        public void TC_23413(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.POQEntry), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.POQEntry).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.FromDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.ToDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            List<string> gridValidatingErrors = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(gridValidatingErrors.Count == 3, ErrorMessages.RightPanelNotEmpty);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            POQEntryUtils.GetDateData(out string from, out string to);
            Page.PopulateGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateNewPOQ));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string poq = Page.GetFirstRowData(TableHeaders.POQ_);
                string fleet = Page.GetFirstRowData(TableHeaders.Fleet);
                Page.FilterTable(TableHeaders.POQ_, poq);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Fleet, fleet), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Fleet, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.POQ_, poq);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Fleet, fleet), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Fleet, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                Page.ButtonClick(ButtonsAndMessages.CreateNewPOQ);
                Page.SwitchToPopUp();

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.MandatoryField), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.MandatoryField));
                    Assert.IsTrue(aspxPage.IsDropDownClosed(FieldNames.QuoteType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.QuoteType));
                    Assert.IsTrue(aspxPage.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.DealerCode));
                    Assert.IsTrue(aspxPage.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.FleetCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DealerPOQNumber), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.DealerPOQNumber));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.POQDate), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.POQDate));
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25440" })]
        public void TC_25440(string UserType, string Fleet, string Dealer, string Part, int NumberOfSections)
        {
            Page.GridLoad();
            string poqNumber = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            POQEntryAspx POQEntryAspxPage = Page.OpenCreateNewPOQ();
            string quoteType = "Variable";

            var errorMsgs = POQEntryAspxPage.CreateNewPOQ(Fleet, Dealer, poqNumber, quoteType);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POQEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POQEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections; i++)
            {
                POQEntryAspxPage.Click(ButtonsAndMessages.AddSection);
                POQEntryAspxPage.WaitForLoadingIcon();
                POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
                POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
                POQEntryAspxPage.WaitForLoadingIcon();
            }
           
            POQEntryAspxPage.Click(ButtonsAndMessages.SubmitPOQ);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.Click(ButtonsAndMessages.Continue);
            POQEntryAspxPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POQSubmissionCompletedSuccessfully, invoiceMsg);
            if (!POQEntryAspxPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Submitting POQ:  [{poqNumber}]");
            }
            Assert.AreEqual(NumberOfSections + 1, POQEntryUtils.GetPOQSectionCount(poqNumber), "POQ Section count from DB is not equal to expected count.");
            Console.WriteLine($"Successfully Submitted POQ: [{poqNumber}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25423" })]
        public void TC_25423(string UserType, string Fleet, string Dealer, string Part, int NumberOfSections)
        {
            Page.GridLoad();
            string poqNumber = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            POQEntryAspx POQEntryAspxPage = Page.OpenCreateNewPOQ();

            var errorMsgs = POQEntryAspxPage.CreateNewPOQ(Fleet, Dealer, poqNumber);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POQEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POQEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections; i++)
            {
                POQEntryAspxPage.Click(ButtonsAndMessages.AddSection);
                POQEntryAspxPage.WaitForLoadingIcon();
                POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
                POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
                POQEntryAspxPage.WaitForLoadingIcon();
            }

            POQEntryAspxPage.Click(ButtonsAndMessages.SubmitPOQ);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.Click(ButtonsAndMessages.Continue);
            POQEntryAspxPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POQSubmissionCompletedSuccessfully, invoiceMsg);
            if (!POQEntryAspxPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Submitting POQ:  [{poqNumber}]");
            }
            Assert.AreEqual(NumberOfSections + 1, POQEntryUtils.GetPOQSectionCount(poqNumber), "POQ Section count from DB is not equal to expected count.");
            Console.WriteLine($"Successfully Submitted POQ: [{poqNumber}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25426" })]
        public void TC_25426(string UserType, string Fleet, string Dealer, string Part, int NumberOfSections)
        {
            Page.GridLoad();
            string poqNumber = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            POQEntryAspx POQEntryAspxPage = Page.OpenCreateNewPOQ();

            var errorMsgs = POQEntryAspxPage.CreateNewPOQ(Fleet, Dealer, poqNumber);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POQEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POQEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();

            for (int i = 1; i <= NumberOfSections; i++)
            {
                POQEntryAspxPage.Click(ButtonsAndMessages.AddSection);
                POQEntryAspxPage.WaitForLoadingIcon();
                POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
                POQEntryAspxPage.WaitForLoadingIcon();
            }
            POQEntryAspxPage.Click(ButtonsAndMessages.SavePOQ);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.ClosePopupWindow();

            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.POQ_, poqNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.POQ_);
            Page.SwitchToPopUp();
    
            POQEntryAspxPage.ClickElementByIndex(ButtonsAndMessages.DeleteSection, NumberOfSections + 1);
            POQEntryAspxPage.AcceptAlert(out string poqMsg);
            Assert.AreEqual(ButtonsAndMessages.DeleteSectionAlertMsg, poqMsg);
            POQEntryAspxPage.WaitForLoadingIcon();

            POQEntryAspxPage.ClickElementByIndex(ButtonsAndMessages.EditLineItem, NumberOfSections);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.WaitForElementToHaveValue(FieldNames.CorePrice, "5.0000");
            POQEntryAspxPage.SetValue(FieldNames.CorePrice, "4.0000");
            POQEntryAspxPage.WaitForElementToHaveValue(FieldNames.CorePrice, "4.0000");
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.ClickElementByIndex(ButtonsAndMessages.AddLineItem, NumberOfSections);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();

            POQEntryAspxPage.Click(ButtonsAndMessages.AddSection);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, Part);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.Click(ButtonsAndMessages.SavePOQ);
            POQEntryAspxPage.WaitForLoadingIcon();
            Console.WriteLine($"Successfully Saved POQ: [{poqNumber}]");
        }
    }
}
