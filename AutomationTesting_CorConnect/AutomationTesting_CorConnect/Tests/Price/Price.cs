using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.Parts;
using AutomationTesting_CorConnect.PageObjects.Price;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.Parts;
using AutomationTesting_CorConnect.Utils.PartsCrossReference;
using AutomationTesting_CorConnect.Utils.Price;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.Price
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Price")]
    internal class Price : DriverBuilderClass
    {
        PricePage Page;
        PartsPage partPage;
        PriceDetailPage priceDetPage;
        string partNumber = string.Empty;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.Price);
            Page = new PricePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22245" })]
        public void TC_22245(string UserType)
        {
            var companyName = PartsCrossReferenceUtil.GetCompanyNameCode();

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

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23406" })]
        public void TC_23406(string UserType)
        {
            
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.Price, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.Price).ForEach(x=>{ Assert.Fail(x); });
                Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.PriceType, "All", "Centralized", "Decentralized"), $"{FieldNames.PriceType} DD: " + ErrorMessages.ListElementsMissing);
                Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.CurrencyCode, CommonUtils.GetActiveCurrencies().ToArray()), $"{FieldNames.CurrencyCode} DD: " + ErrorMessages.ListElementsMissing);
                Page.SelectValueTableDropDown(FieldNames.CurrencyCode, "All");
                LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
                List<string> gridValidatingErrors = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(gridValidatingErrors.Count == 3, ErrorMessages.RightPanelNotEmpty);

                string searchPartNumber = PriceUtils.GetPartNumber();
                if (string.IsNullOrEmpty(searchPartNumber))
                {
                    Assert.Fail("Part Number returned empty from DB");
                }

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile()); 

                Page.PopulateGrid(searchPartNumber);
                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                                        
                    if (!Page.IsNestedGridOpen())
                    {
                        errorMsgs.Add("Level indicator not visible or not working");
                    }
                    else
                    {
                        errorMsgs.AddRange(Page.ValidateNestedGridTabs("Price Detail"));
                        errorMsgs.AddRange(Page.ValidateNestedTableHeaders(TableHeaders.Commands, TableHeaders.PriceLevel, TableHeaders.UnitPrice, TableHeaders.CorePrice, TableHeaders.FET, TableHeaders.Active));
                        errorMsgs.AddRange(Page.ValidateNestedTableDetails(true, true));
                        Assert.IsTrue(Page.IsNestedGridClosed(), ErrorMessages.NestedGridNotClosing);
                    }   
                }
                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });
                string partNumber_ = Page.GetFirstRowData(TableHeaders.PartNumber);
                string description = Page.GetFirstRowData(TableHeaders.Description);
                Page.FilterTable(TableHeaders.PartNumber, partNumber_);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Description, description), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Description, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.PartNumber, partNumber_);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Description, description), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Description, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
            
        }

        [TearDown]
        public void TearDown()
        {
            if (!string.IsNullOrEmpty(partNumber))
            {
                PriceUtils.DeletePartAndPrice(partNumber);
            }
        }
    }
}
