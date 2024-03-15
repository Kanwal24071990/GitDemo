using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.PriceLookup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.PartPriceLookup;
using AutomationTesting_CorConnect.Utils.Parts;
using AutomationTesting_CorConnect.Utils.PartsCrossReference;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.PriceLookup
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Price Lookup")]
    internal class PriceLookup : DriverBuilderClass
    {
        PriceLookupPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.PriceLookup);
            Page = new PriceLookupPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22246" })]
        public void TC_22246(string UserType)
        {
            var companyName = PartsCrossReferenceUtil.GetCompanyNameCode();

            Page.OpenDropDown(FieldNames.CurrencyCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CurrencyCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CurrencyCode));
            Page.OpenDropDown(FieldNames.CurrencyCode);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CurrencyCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CurrencyCode));
            Page.SelectValueFirstRow(FieldNames.CurrencyCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CurrencyCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CurrencyCode));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.CurrencyCode, "CAD");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CurrencyCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CurrencyCode));

            Page.OpenDatePicker(FieldNames.EffectiveDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));
            Page.OpenDatePicker(FieldNames.EffectiveDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));
            Page.SelectDate(FieldNames.EffectiveDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));

            Page.OpenDropDown(FieldNames.PriceType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PriceType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PriceType));
            Page.OpenDropDown(FieldNames.PriceType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PriceType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PriceType));
            Page.SelectValueFirstRow(FieldNames.PriceType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PriceType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PriceType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.PriceType, "Centralized");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PriceType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PriceType));

            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SearchAndSelectValueAfterOpen(FieldNames.CompanyName, companyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));


        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24619" })]
        public void TC_24619(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.PriceLookup, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.PriceLookup).ForEach(x => { Assert.Fail(x); });

            Page.ClickPageTitle();
            List<string> companyNameHeaders = Page.GetHeaderNamesTableDropDown(FieldNames.CompanyName);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(companyNameHeaders.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, FieldNames.ContainsLocation));
                Assert.IsTrue(companyNameHeaders.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.ContainsLocation));
                Assert.IsTrue(companyNameHeaders.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, FieldNames.ContainsLocation));
                Assert.IsTrue(companyNameHeaders.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.ContainsLocation));
                Assert.IsTrue(companyNameHeaders.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.ContainsLocation));
                Assert.IsTrue(companyNameHeaders.Contains(TableHeaders.EntityType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, FieldNames.ContainsLocation));
                Assert.IsTrue(companyNameHeaders.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, FieldNames.ContainsLocation));
            });

            Page.ClickPageTitle();
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.PriceType, "All", "Centralized", "Decentralized"), $"{FieldNames.PriceType} : " + ErrorMessages.ListElementsMissing);

            Page.ClickPageTitle();
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.CurrencyCode, CommonUtils.GetActiveCurrencies().ToArray()), $"{FieldNames.CurrencyCode} : " + ErrorMessages.ListElementsMissing);

            Page.ButtonClick(ButtonsAndMessages.Clear);
            Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
            Page.AcceptAlert();
            Page.WaitForMsg(ButtonsAndMessages.Loading);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            string partNum = PartsUtil.GetPartNumber();
            Page.PopulateGrid(partNum);

            if (Page.IsAnyDataOnGrid())
            {
                
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                string partNumber = Page.GetFirstRowData(TableHeaders.PartNumber);
                Page.FilterTable(TableHeaders.PartNumber, partNumber);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.PartNumber, partNumber), ErrorMessages.NoRowAfterFilter);

                Page.FilterTable(TableHeaders.PartNumber, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);

                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                Page.FilterTable(TableHeaders.PartNumber, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);

                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                if (Page.IsNextPage())
                {
                    Page.GoToPage(2);
                }
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25848" })]
        public void TC_25848(string UserType, string DealerName, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(DealerName, Constants.UserType.Dealer);
            menu.OpenPage(Pages.PriceLookup);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, DealerName));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.PriceLookup);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, FleetName));
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
