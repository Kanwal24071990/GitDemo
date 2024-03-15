using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.AgedInvoiceReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AgedInvoiceReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.AgedInvoiceReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Aged Invoice Report")]
    internal class AgedInvoiceReport : DriverBuilderClass
    {
        AgedInvoiceReportPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.AgedInvoiceReport);
            Page = new AgedInvoiceReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23407" })]
        public void TC_23407(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.AgedInvoiceReport, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
                Assert.AreEqual(string.Empty, Page.GetValueDropDown(FieldNames.InvoiceStatus));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.AgedInvoiceReport).ForEach(x=>{ Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                AgedInvoiceReportUtil.GetData(out string location);
                Page.PopulateGrid(location);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                                    
                    string balDue = Page.GetFirstRowData(TableHeaders.BalanceDue);
                    Page.FilterTable(TableHeaders.BalanceDue, balDue);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.BalanceDue, balDue), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.AgingBucket, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.BalanceDue, balDue);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.BalanceDue, balDue), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.AgingBucket, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
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

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22197" })]
        public void TC_22197(string UserType)
        {
            Page.OpenMultiSelectDropDown(FieldNames.AgedBucket);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.AgedBucket), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.AgedBucket));

            Page.OpenMultiSelectDropDown(FieldNames.AgedBucket);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.AgedBucket), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.AgedBucket));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.AgedBucket);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.AgedBucket);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.AgedBucket), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.AgedBucket));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.AgedBucket, TableHeaders.AgedBucket, "Current");
            Page.ClearSelectionMultiSelectDropDown(FieldNames.AgedBucket);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.AgedBucket), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AgedBucket));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.AgedBucket);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.AgedBucket);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.AgedBucket), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AgedBucket));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.AgedBucket);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.AgedBucket), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AgedBucket));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25842" })]
        public void TC_25842(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Fleet, null);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Dealer, null));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.AgedInvoiceReport);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Dealer, null));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25908" })]
        public void TC_25908(string UserType, string dealerUser)
        {
            var errorMsgs = Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.AgedInvoiceReport);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));

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
