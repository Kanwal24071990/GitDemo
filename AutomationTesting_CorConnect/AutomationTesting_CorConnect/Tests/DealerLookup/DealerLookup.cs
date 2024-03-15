using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.PageObjects.DealerLookup;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.Utils.DealerLookup;
using AutomationTesting_CorConnect.Helper;
using System.Collections.Generic;
using System;
using AutomationTesting_CorConnect.Utils;

namespace AutomationTesting_CorConnect.Tests.DealerLookup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Lookup")]
    internal class DealerLookup : DriverBuilderClass
    {
        DealerLookupPage Page;


        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerLookup);
            Page = new DealerLookupPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16374" })]
        public void TC_16374(string UserType)
        {
            string dealerCode = DealerLookupUtil.GetDealerCode();
            Page.AreFieldsAvailable(Pages.DealerLookup).ForEach(x => { Assert.Fail(x); });
            Page.LoadDataOnGrid(dealerCode);
            var errorMsgs = Page.ValidateTableHeaders(TableHeaders.AccountCode, TableHeaders.Name, TableHeaders.LegalName, TableHeaders.LocationType, TableHeaders.AccountStatus,
                TableHeaders.TerminationDate, TableHeaders.Address, TableHeaders.City, TableHeaders.State, TableHeaders.Zip, TableHeaders.Country, TableHeaders.Phone, TableHeaders.VendorType,
                TableHeaders.ProgramCode, TableHeaders.FranchiseCodes);

            foreach (var error in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(error));
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22209" })]
        public void TC_22209(string UserType)
        {
            var corcentricCode = DealerLookupUtil.GetCorcenticCode();
            Assert.IsNotNull(corcentricCode, GetErrorMessage(ErrorMessages.ErrorGettingData));

            Page.OpenDropDown(FieldNames.DealerCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
            Page.OpenDropDown(FieldNames.DealerCode);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
            Page.SelectValueFirstRow(FieldNames.DealerCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
            Page.SearchAndSelectValueAfterOpen(FieldNames.DealerCode, corcentricCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));

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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21513" })]
        public void TC_21513(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.DealerLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.DealerLookup).ForEach(x => { Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                DealerLookupUtil.GetData(out string dealerCode);

                Page.LoadDataOnGrid(dealerCode);

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
    }
}
