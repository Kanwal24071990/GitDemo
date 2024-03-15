using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DunningStatus;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DunningStatus;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.DunningStatus
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dunning Status")]
    internal class DunningStatus : DriverBuilderClass
    {
        DunningStatusPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DunningStatus);
            Page = new DunningStatusPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19232" })]
        public void TC_19232(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.DunningStatus, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.DunningStatus).ForEach(x=>{ Assert.Fail(x); });
                Assert.AreEqual(Page.GetValueDropDown("Dunning Status"), "All", GetErrorMessage(ErrorMessages.InvalidDefaultValue, "Dunning Status"));
                Assert.AreEqual(Page.GetValue("From"), CommonUtils.GetDefaultFromDate(), GetErrorMessage(ErrorMessages.InvalidDefaultValue, "From"));
                Assert.AreEqual(Page.GetValue("To"), CommonUtils.GetCurrentDate(), GetErrorMessage(ErrorMessages.InvalidDefaultValue, "To"));

                DunningStatusUtil.GetData(out string corcentricCode, out string from, out string to);

                if (!string.IsNullOrEmpty(corcentricCode))
                {
                    Page.PopulateGrid(corcentricCode, from, to);

                    if (Page.IsAnyDataOnGrid())
                    {
                        var errorMsgs = Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                        errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                        errorMsgs.AddRange(Page.ValidateTableHeaders(Pages.DunningStatus));

                        foreach (var errorMsg in errorMsgs)
                        {
                            Assert.Fail(GetErrorMessage(errorMsg));
                        }

                        Assert.IsTrue(Page.ValidateHyperlink("Display Name"), GetErrorMessage(ErrorMessages.UnableToOpenHyperLink));
                    }
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Category(TestCategory.Premier)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22113" })]
        public void TC_22113(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.DunningStatus, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.DunningStatus).ForEach(x=>{ Assert.Fail(x); });
                List<string> headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.FleetBilling);
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(headerNames.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, FieldNames.ContainsLocation));
                });
                Assert.AreEqual(Page.GetValueDropDown("Dunning Status"), "All", GetErrorMessage(ErrorMessages.InvalidDefaultValue, "Dunning Status"));
                Assert.AreEqual(Page.GetValue("From"), CommonUtils.GetDefaultFromDate(), GetErrorMessage(ErrorMessages.InvalidDefaultValue, "From"));
                Assert.AreEqual(Page.GetValue("To"), CommonUtils.GetCurrentDate(), GetErrorMessage(ErrorMessages.InvalidDefaultValue, "To"));

                DunningStatusUtil.GetData(out string corcentricCode, out string from, out string to);

                if (string.IsNullOrEmpty(corcentricCode))
                {
                    Assert.Fail("No CorcentricCode returned from query");
                }
                Page.PopulateGrid(corcentricCode, from, to);

                if (Page.IsAnyDataOnGrid())
                {
                    var errorMsgs = Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                    errorMsgs.AddRange(Page.ValidateTableHeaders(Pages.DunningStatus));

                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }

                    string displayName = Page.GetFirstRowData(TableHeaders.DisplayName);
                    string accountingCode = Page.GetFirstRowData(TableHeaders.AccountingCode);
                    Page.FilterTable(TableHeaders.DisplayName, displayName);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.AccountingCode, accountingCode), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.AccountingCode, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.DisplayName, displayName);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.AccountingCode, accountingCode), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.AccountingCode, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ResetNotWorking);
                    Page.ButtonClick(ButtonsAndMessages.Clear);
                    Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
                    Page.AcceptAlert();
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22220" })]
        public void TC_22220(string UserType)
        {
            Page.OpenMultiSelectDropDown(FieldNames.FleetBilling);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetBilling), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.FleetBilling));

            Page.OpenMultiSelectDropDown(FieldNames.FleetBilling);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetBilling), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.FleetBilling));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetBilling);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetBilling);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetBilling), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.FleetBilling));

            DunningStatusUtil.GetData(out string corcentricCode, out string from, out string to);

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetBilling, TableHeaders.AccountCode, corcentricCode);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetBilling);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetBilling), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetBilling));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.FleetBilling);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetBilling);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetBilling), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetBilling));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetBilling);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetBilling), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetBilling));
        }
    }
}