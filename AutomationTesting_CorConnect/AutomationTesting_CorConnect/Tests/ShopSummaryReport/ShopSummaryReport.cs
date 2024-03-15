using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ShopSummaryReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.ShopSummaryReport
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Shop Summary Report")]
    internal class ShopSummaryReport : DriverBuilderClass
    {
        ShopSummaryReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ShopSummaryReport);
            Page = new ShopSummaryReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20753" })]
        public void TC_20753(string UserType)
        {
            Page.OpenDropDown(FieldNames.Location);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Location), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Location));
            Page.OpenDropDown(FieldNames.Location);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Location), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Location));
            Page.SelectValueFirstRow(FieldNames.Location);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Location), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Location));
            Page.OpenDatePicker(FieldNames.FleetInvoiceDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FleetInvoiceDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FleetInvoiceDate));
            Page.OpenDatePicker(FieldNames.FleetInvoiceDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FleetInvoiceDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FleetInvoiceDate));
            Page.SelectDate(FieldNames.FleetInvoiceDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FleetInvoiceDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FleetInvoiceDate));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23289" })]
        public void TC_23289(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.ShopSummaryReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.ShopSummaryReport).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual("", Page.GetValueDropDown(FieldNames.Location), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Location));
            
            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            List<string> gridValidatingErrors = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(gridValidatingErrors.Count == 3, ErrorMessages.RightPanelNotEmpty);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.PopulateGrid();

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string dealerCode = Page.GetFirstRowData(TableHeaders.DealerCode);
                string dealerName = Page.GetFirstRowData(TableHeaders.DealerName);
                Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerName, dealerName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerName, dealerName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
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
    }
}
