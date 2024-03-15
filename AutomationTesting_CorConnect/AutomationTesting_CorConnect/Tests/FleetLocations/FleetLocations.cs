using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetLocations;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetLocations;
using AutomationTesting_CorConnect.Utils.FleetLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.FleetLocations
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Locations")]
    internal class FleetLocations : DriverBuilderClass
    {
        FleetLocationsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetLocations);
            Page = new FleetLocationsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22182" })]
        public void TC_22182(string UserType)
        {
            var corcentricCode = FleetLookupUtils.GetCorcenticCode();

            Page.OpenDropDown(FieldNames.FleetCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.OpenDropDown(FieldNames.FleetCode);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.SelectValueFirstRow(FieldNames.FleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.SearchAndSelectValueAfterOpen(FieldNames.FleetCode, corcentricCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));

            Page.OpenDropDown(FieldNames.Country);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.OpenDropDown(FieldNames.Country);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.SelectValueFirstRow(FieldNames.Country);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.Country, CommonUtils.GetCountry());
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));

            Page.OpenDropDown(FieldNames.LocationType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.OpenDropDown(FieldNames.LocationType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SelectValueFirstRow(FieldNames.LocationType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.LocationType, "Master");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));

            Page.OpenDropDown(FieldNames.AccountStatus);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AccountStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AccountStatus));
            Page.OpenDropDown(FieldNames.AccountStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AccountStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AccountStatus));
            Page.SelectValueFirstRow(FieldNames.AccountStatus);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AccountStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AccountStatus));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.AccountStatus, "Active");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AccountStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AccountStatus));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21511" })]
        public void TC_21511(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.FleetLocations), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.FleetLocations).ForEach(x=>{ Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                FleetLocationsUtil.GetData(out string fleetCode);
                Page.LoadDataOnGrid(fleetCode);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                    
                    Assert.IsTrue(Page.IsNestedGridOpen());
                    Assert.IsTrue(Page.IsNestedGridClosed());

                    string dealerInvoiceNum = Page.GetFirstRowData(TableHeaders.AccountCode);
                    Page.FilterTable(TableHeaders.AccountCode, dealerInvoiceNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.AccountCode, dealerInvoiceNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Name, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.AccountCode, dealerInvoiceNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.AccountCode, dealerInvoiceNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Name, CommonUtils.RandomString(10));
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25849" })]
        public void TC_25849(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.FleetCode, LocationType.ParentShop, Constants.UserType.Fleet, null);
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetLocations);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.FleetCode, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
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
