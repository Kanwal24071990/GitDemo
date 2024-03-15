using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.GrossMarginCreditReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.GrossMarginCreditReport;
using AutomationTesting_CorConnect.Utils.PartPriceLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.GrossMarginCreditReport
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Gross Margin Credit Report")]
    internal class GrossMarginCreditReport : DriverBuilderClass
    {
        GrossMarginCreditReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.GrossMarginCreditReport);
            Page = new GrossMarginCreditReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22166" })]
        public void TC_22166(string UserType)
        {
            string dealerCode = PartPriceLookupUtil.GetDealerCode();
            string fleetCode = PartPriceLookupUtil.GetFleetCode();

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

            Page.OpenDropDown(FieldNames.DealerName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.OpenDropDown(FieldNames.DealerName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.SelectValueFirstRow(FieldNames.DealerName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
            Page.SearchAndSelectValueAfterOpen(FieldNames.DealerName, dealerCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));

            Page.OpenDropDown(FieldNames.FleetName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.OpenDropDown(FieldNames.FleetName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.SelectValueFirstRow(FieldNames.FleetName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.SearchAndSelectValueAfterOpen(FieldNames.FleetName, fleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));
            Page.ClickPageTitle();

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24768" })]
        public void TC_24768(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.GrossMarginCreditReport, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.GrossMarginCreditReport).ForEach(x => { Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                GrossMarginCreditReportUtil.GetData(out string from, out string to);
                Page.PopulateGrid(from, to);

                if (Page.IsAnyDataOnGrid())
                {
                    Page.ButtonClick(ButtonsAndMessages.Clear);
                    Assert.AreEqual(Page.GetAlertMessage(), ButtonsAndMessages.ConfirmResetAlert);
                    Page.DismissAlert();

                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                    string progInvNum = Page.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
                    Page.FilterTable(TableHeaders.ProgramInvoiceNumber, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.ProgramInvoiceNumber, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(Page.RenameMenuField(TableHeaders.DealerName), CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.ProgramInvoiceNumber, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.ProgramInvoiceNumber, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(Page.RenameMenuField(TableHeaders.DealerName), CommonUtils.RandomString(10));
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25878" })]
        public void TC_25878(string UserType)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, null));
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
