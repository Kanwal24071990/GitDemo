using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetLookup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.FleetLookup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Lookup")]
    internal class FleetLookup : DriverBuilderClass
    {
        FleetLookupPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetLookup);
            Page = new FleetLookupPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16375" })]
        public void TC_16375(string UserType)
        {
            var corcentricCode = FleetLookupUtils.GetCorcenticCode();
            Assert.IsNotNull(corcentricCode, GetErrorMessage(ErrorMessages.ErrorGettingData));

            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Page.RenameMenuField(Pages.FleetLookup), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.FleetLookup).ForEach(x => { Assert.Fail(x); });
                Assert.AreEqual("Shop", Page.GetValueDropDown("Location Type"), GetErrorMessage(ErrorMessages.InvalidDefaultValue, "Location Type"));
                Assert.AreEqual(Page.GetValueDropDown("Account Status"), "Active", GetErrorMessage(ErrorMessages.InvalidDefaultValue, "Account Status"));

                if (!string.IsNullOrEmpty(corcentricCode))
                {
                    Page.SelectFleetCode(corcentricCode);
                }

                if (Page.IsAnyDataOnGrid())
                {
                    var errorMsgs = Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.SaveLayout);
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, false));
                    errorMsgs.AddRange(Page.ValidateTableHeaders(TableHeaders.AccountCode, TableHeaders.Name, TableHeaders.LegalName, TableHeaders.LocationType, TableHeaders.AccountStatus, TableHeaders.TerminationDate, TableHeaders.Transactionable, TableHeaders.Address, TableHeaders.City, TableHeaders.State, TableHeaders.Country, TableHeaders.Phone, TableHeaders.Zip, TableHeaders.Currency, TableHeaders.ProgramCode));

                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }

                    Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22210" })]
        public void TC_22210(string UserType)
        {

            Page.OpenDropDown(FieldNames.FleetCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.OpenDropDown(FieldNames.FleetCode);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.SelectValueFirstRow(FieldNames.FleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.SearchAndSelectValueAfterOpen(FieldNames.FleetCode, FleetLookupUtils.GetCorcenticCode());
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));

            Page.OpenDropDown(FieldNames.Country);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.OpenDropDown(FieldNames.Country);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.SelectValueFirstRow(FieldNames.Country);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.Country, "US");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));

            Page.OpenDropDown(FieldNames.LocationType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.OpenDropDown(FieldNames.LocationType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SelectValueFirstRow(FieldNames.LocationType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.LocationType, "Billing");
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20211" })]
        public void TC_20211(string UserType)
        {
            List<string> errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.GetPageLabel(), Page.RenameMenuField(Pages.FleetLookup), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.FleetLookup).ForEach(x => { Assert.Fail(x); });
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.LocationType, "All", "Billing", "Master", "Shop", "Parent Shop"));
            Assert.IsTrue(Page.CheckForText(ButtonsAndMessages.ClickSearchToViewData));
            var parentShops = CommonUtils.GetActiveLocations(LocationType.ParentShop, Constants.UserType.Fleet); 
            Assert.IsTrue(parentShops.Count > 0, ErrorMessages.NoRecordInDB);
            Page.PopulateGrid(parentShops[0].DisplayName);
            if (Page.IsAnyDataOnGrid())
            {
                string accountCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                Page.FilterTable(TableHeaders.AccountCode, accountCode);
                Assert.IsTrue(Page.GetRowCountCurrentPage() == 1, ErrorMessages.FilterNotWorking);
                Page.FilterTable(TableHeaders.AccountCode, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
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