using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerDueDateReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerDueDateReport;
using AutomationTesting_CorConnect.Utils.PartPriceLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.DealerDueDateReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Due Date Report")]
    internal class DealerDueDateReport : DriverBuilderClass
    {
        DealerDueDateReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerDueDateReport);
            Page = new DealerDueDateReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22157" })]
        public void TC_22157(string UserType)
        {
            string fleetCode = PartPriceLookupUtil.GetFleetCode();


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

            Page.OpenDropDown(FieldNames.DateRange);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.OpenDropDown(FieldNames.DateRange);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.SelectValueFirstRow(FieldNames.DateRange);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DateRange), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DateRange));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.DateRange, "Last month");
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
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


        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23587" })]
        public void TC_23587(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.DealerDueDateReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));


                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.DealerDueDateReport).ForEach(x => { Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                DealerDueDateReportUtils.GetData(out string from, out string to);
                Page.LoadDataOnGrid(from, to);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                                                           
                    Page.ClickTableHeader(TableHeaders.ProgramInvoiceNumber);
                    Page.ClickTableHeader(TableHeaders.ProgramInvoiceNumber);
                    string programInvoiceNumber = Page.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
                    Page.FilterTable(TableHeaders.ProgramInvoiceNumber, programInvoiceNumber);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.ProgramInvoiceNumber, programInvoiceNumber), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.PO_, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.ProgramInvoiceNumber, programInvoiceNumber);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.ProgramInvoiceNumber, programInvoiceNumber), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.PO_, CommonUtils.RandomString(10));
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
            });


        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25852" })]
        public void TC_25852(string UserType)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, null);
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
