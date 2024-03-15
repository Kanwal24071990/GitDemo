using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerLocations;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerLocations;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.DealerLocations
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Locations")]
    internal class DealerLocations : DriverBuilderClass
    {
        DealerLocationsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerLocations);
            Page = new DealerLocationsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20665" })]
        public void TC_20665(string UserType)
        {

            Page.OpenDropDown(FieldNames.DealerCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
            Page.OpenDropDown(FieldNames.DealerCode);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
            Page.SelectValueFirstRow(FieldNames.DealerCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));

            Page.OpenDropDown(FieldNames.Country);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.OpenDropDown(FieldNames.Country);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.SelectValueFirstRow(FieldNames.Country);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));

            Page.OpenDropDown(FieldNames.LocationType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.OpenDropDown(FieldNames.LocationType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SelectValueFirstRow(FieldNames.LocationType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));

            Page.OpenDropDown(FieldNames.AccountStatus);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AccountStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AccountStatus));
            Page.OpenDropDown(FieldNames.AccountStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AccountStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AccountStatus));
            Page.SelectValueFirstRow(FieldNames.AccountStatus);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AccountStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AccountStatus));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21512" })]
        public void TC_21512(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.DealerLocations), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.DealerLocations).ForEach(x=>{ Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                DealerLocationsUtil.GetData(out string dealerCoder);
                Page.LoadDataOnGrid(dealerCoder);

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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25897" })]
        public void TC_25897(string UserType, string dealerUser, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerCode, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.DealerLocations);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerCode, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));
            
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
