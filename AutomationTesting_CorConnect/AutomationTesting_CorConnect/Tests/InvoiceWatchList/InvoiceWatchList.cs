using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.InvoiceWatchList;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.InvoiceWatchList;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.InvoiceWatchList
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Invoice Watch List")]
    internal class InvoiceWatchList : DriverBuilderClass
    {
        InvoiceWatchListPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.InvoiceWatchList);
            Page = new InvoiceWatchListPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22212" })]
        public void TC_22212(string UserType)
        {
            Page.OpenDatePicker(FieldNames.StartDateFrom);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.StartDateFrom), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.StartDateFrom));
            Page.OpenDatePicker(FieldNames.StartDateFrom);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.StartDateFrom), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.StartDateFrom));
            Page.SelectDate(FieldNames.StartDateFrom);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.StartDateFrom), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.StartDateFrom));

            Page.OpenDatePicker(FieldNames.StartDateTo);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.StartDateTo), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.StartDateTo));
            Page.OpenDatePicker(FieldNames.StartDateTo);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.StartDateTo), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.StartDateTo));
            Page.SelectDate(FieldNames.StartDateTo);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.StartDateTo), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.StartDateTo));

            Page.OpenDropDown(FieldNames.Fleet);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Fleet), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Fleet));
            Page.SelectValueFirstRow(FieldNames.Fleet);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Fleet), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Fleet));
            Page.OpenDropDown(FieldNames.Fleet);
            string fleetValue = Page.SelectValueTableDropDown(FieldNames.Fleet);
            Page.SearchAndSelectValueAfterOpen(FieldNames.Fleet, fleetValue);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Fleet), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Fleet));

            Page.OpenDropDown(FieldNames.Dealer);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Dealer), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Dealer));
            Page.SelectValueFirstRow(FieldNames.Dealer);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Dealer), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Dealer));
            Page.OpenDropDown(FieldNames.Dealer);
            string dealerValue = Page.SelectValueTableDropDown(FieldNames.Dealer);
            Page.SearchAndSelectValueAfterOpen(FieldNames.Dealer, dealerValue);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Dealer), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Dealer));
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24611" })]
        public void TC_24611(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.InvoiceWatchList), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            InvoiceWatchListUtils.GetData(out string StartDateFrom , out string StartDateTo);
            Page.LoadDataOnGrid(StartDateFrom, StartDateTo);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string dealer = Page.GetFirstRowData(TableHeaders.Dealer);

                Page.SetFilterCreiteria(TableHeaders.Dealer, FilterCriteria.Equals);

                Page.FilterTable(TableHeaders.Dealer, dealer);

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
                Page.ClickInputButton(ButtonsAndMessages.AddNewRecord);

                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                Page.ClickEdit();
                Page.ClosePopupWindow();
            }
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }
    }
}