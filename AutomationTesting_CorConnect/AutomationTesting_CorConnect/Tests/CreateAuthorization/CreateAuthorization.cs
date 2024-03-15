using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.AccountStatusChangeReport;
using AutomationTesting_CorConnect.PageObjects.AgedInvoiceReport;
using AutomationTesting_CorConnect.PageObjects.AssignUserCharts;
using AutomationTesting_CorConnect.PageObjects.CreateAuthorization;
using AutomationTesting_CorConnect.PageObjects.InvoiceDetailReport;
using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations;
using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations.CreateNewAuthorization;
using AutomationTesting_CorConnect.PageObjects.PartSalesbyShopReport;
using AutomationTesting_CorConnect.PageObjects.PendingBillingManagementReport;
using AutomationTesting_CorConnect.PageObjects.ShopSummaryReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountStatusChangeReport;
using AutomationTesting_CorConnect.Utils.AgedInvoiceReport;
using AutomationTesting_CorConnect.Utils.CreateAuthorization;
using AutomationTesting_CorConnect.Utils.InvoiceDetailReport;
using AutomationTesting_CorConnect.Utils.PartSalesByShopReport;
using AutomationTesting_CorConnect.Utils.PendingBillingManagementReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace AutomationTesting_CorConnect.Tests.CreateAuthorization
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Create Authorization")]
    internal class CreateAuthorization : DriverBuilderClass
    {
        CreateAuthorizationPage Page;
        OpenAuthorizationsPage OpenAuthorizationPage;
        CreateNewAuthorizationPopUpPage aspxPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.CreateAuthorization, false, true);
            menu.SwitchIframe();
            Page = new CreateAuthorizationPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20743" })]
        public void TC_20743(string UserType)
        {
            Assert.Multiple(() =>
            {
                Page.OpenDropDown(FieldNames.TransactionType);
                Page.ClickTitle(FieldNames.Title);
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.TransactionType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionType));
                Page.SelectValueFirstRow(FieldNames.TransactionType);
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.TransactionType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionType));

                List<string> dealerDisplayNames = CreateAuthorizationUtils.GetDisplayNames(EntityType.Dealer);
                dealerDisplayNames.RemoveAll(string.IsNullOrEmpty);
                Page.OpenMultipleColumnsDropDown(FieldNames.DealerCode, dealerDisplayNames[0]);
                Page.ClickTitle(FieldNames.Title);
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
                Page.ClearInputMultipleColumnsDropDown(FieldNames.DealerCode);
                Page.ClickTitle(FieldNames.Title);
                Page.WaitForLoadingIcon();
                Page.SelectValueMultipleColumns(FieldNames.DealerCode, dealerDisplayNames[1]);
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
                Page.WaitForLoadingIcon();

                Page.OpenDatePicker(FieldNames.InvoiceDate);
                Page.ClickTitle(FieldNames.Title);
                Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.InvoiceDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.InvoiceDate));
                Page.SelectDate(FieldNames.InvoiceDate);
                Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.InvoiceDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.InvoiceDate));

                List<string> fleetDisplayNames = CreateAuthorizationUtils.GetDisplayNames(EntityType.Fleet);
                fleetDisplayNames.RemoveAll(string.IsNullOrEmpty);
                Page.OpenMultipleColumnsDropDown(FieldNames.FleetCode, fleetDisplayNames[0]);
                Page.ClickTitle(FieldNames.Title);
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
                Page.ClearInputMultipleColumnsDropDown(FieldNames.FleetCode);
                Page.ClickTitle(FieldNames.Title);
                Page.WaitForLoadingIcon();
                Page.SelectValueMultipleColumns(FieldNames.FleetCode, fleetDisplayNames[1]);
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            });
        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23496" })]
        public void TC_23496(string dealerCode, string fleetCode)
        {
            string poNumber = CommonUtils.RandomString(6);
            string invNum = CommonUtils.RandomString(6);
            Page.SelectValueByScroll(FieldNames.TransactionType, "Service");
            Page.EnterDateInInvoiceDate();
            Page.EnterDealerCode(dealerCode);
            Page.EnterFleetCode(fleetCode);
            Page.ClickContinue();
            Page.WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            Page.WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            Page.EnterInvoiceAmount("50.00");
            Page.EnterTextAfterClear(FieldNames.PurchaseOrderNumber, poNumber);
            Page.EnterTextAfterClear(FieldNames.InvoiceNumber, invNum);
            Page.ClickCreateAuthorization();
            Assert.IsTrue(Page.CheckForText(ButtonsAndMessages.SuccessfulTransaction));
            var authcode = Page.GetText("Authorization Code Label");
            menu.OpenNextPage(Pages.CreateAuthorization, Pages.OpenAuthorizations, true);
            OpenAuthorizationPage = new OpenAuthorizationsPage(driver);
            OpenAuthorizationPage.PopulateGrid(fleetCode);
            OpenAuthorizationPage.FilterTable(TableHeaders.Auth_Code, authcode);
            StringAssert.Contains("*", OpenAuthorizationPage.GetFirstRowData(TableHeaders.Currency));
            OpenAuthorizationPage.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            aspxPage = new CreateNewAuthorizationPopUpPage(driver);
            Assert.IsTrue(aspxPage.CheckForText(authcode));
            Assert.IsFalse(aspxPage.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23498" })]
        public void TC_23498(string dealerCode, string fleetCode)
        {
            string poNumber = CommonUtils.RandomString(6);
            string invNum = CommonUtils.RandomString(6);
            Page.SelectValueByScroll(FieldNames.TransactionType, "Service");
            Page.EnterDateInInvoiceDate();
            Page.EnterDealerCode(dealerCode);
            Page.EnterFleetCode(fleetCode);
            Page.ClickContinue();
            Page.WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            Page.WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            Page.EnterInvoiceAmount("50.00");
            Page.EnterTextAfterClear(FieldNames.PurchaseOrderNumber, poNumber);
            Page.EnterTextAfterClear(FieldNames.InvoiceNumber, invNum);
            Page.ClickCreateAuthorization();
            Assert.IsTrue(Page.CheckForText(ButtonsAndMessages.SuccessfulTransaction));
            Assert.IsFalse(Page.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
            var authcode = Page.GetText("Authorization Code Label");
            menu.OpenNextPage(Pages.CreateAuthorization, Pages.OpenAuthorizations, true);
            OpenAuthorizationPage = new OpenAuthorizationsPage(driver);
            OpenAuthorizationPage.GridLoad();
            OpenAuthorizationPage.FilterTable(TableHeaders.Auth_Code, authcode);
            StringAssert.Contains("*", OpenAuthorizationPage.GetFirstRowData(TableHeaders.Currency));
            OpenAuthorizationPage.ClickHyperLinkOnGrid(TableHeaders.Auth_Code);
            aspxPage = new CreateNewAuthorizationPopUpPage(driver);
            Assert.IsTrue(aspxPage.CheckForText(authcode));
            Assert.IsFalse(aspxPage.CheckForText("Submitting an authorization where Dealer and Fleet currencies are different, conversion rates applied."));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25698" })]
        public void TC_25698(string dealerCode, string fleetCode)
        {
            Page.CreateAuthorization(dealerCode, fleetCode, out string transactionInvNum, out string authCode);
            Assert.IsTrue(Page.CheckForText("Successful transaction."), "Failed to Create Authorization");
            Console.WriteLine($"Authorization Created Successfully: [{authCode}]. Invoice Number: [{transactionInvNum}]");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25901" })]
        public void TC_25901(string UserType)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.FleetCode, LocationType.ParentShop, Constants.UserType.Fleet, null);
            Page.ClickFieldLabel(FieldNames.DealerCode);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerCode, LocationType.ParentShop, Constants.UserType.Dealer, null));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });



            //List<string> errorMsgs = new List<string>();
            string[] AllTabs = { "Part Sales by Shop Report", "Aged Invoice Report", "Shop Summary Report", "Pending Billing Management Report", "Assign User Charts"};
            for (int i = 0; i < AllTabs.Length; i++)
            {
                Page.SwitchToMainWindow();
                menu.OpenPage(AllTabs[i]);
                if (AllTabs[i] == "Part Sales by Shop Report")
                {
                    PartSalesbyShopReportPage PSBSRPage;
                    PSBSRPage = new PartSalesbyShopReportPage(driver);

                    PartSalesByShopReportUtils.GetDateData(out string fromDate, out string toDate);
                    PSBSRPage.PopulateGrid(fromDate, toDate);
                }
             
                else if (AllTabs[i] == "Aged Invoice Report")
                {
                    AgedInvoiceReportPage AIRPage;
                    AIRPage = new AgedInvoiceReportPage(driver);

                    AgedInvoiceReportUtil.GetData(out string location);
                    AIRPage.PopulateGrid(location);
                }
                
                else if (AllTabs[i] == "Shop Summary Report")
                {
                    ShopSummaryReportPage SSRPage;
                    SSRPage = new ShopSummaryReportPage(driver);
                    SSRPage.PopulateGrid();
                }
                else if (AllTabs[i] == "Pending Billing Management Report")
                {
                    PendingBillingManagementReportUtil.GetData(out string companyName);
                    Assert.That(companyName, Is.Not.Null.Or.Not.Empty, $"Null or empty CompanyName returned from DB");
                    if (!string.IsNullOrEmpty(companyName))
                    {
                        PendingBillingManagementReportPage PBMRPage;
                        PBMRPage = new PendingBillingManagementReportPage(driver);
                        PBMRPage.PopulateGrid(companyName);
                    }
                }
                else if (AllTabs[i]== "Assign User Charts")
                {
                    AssignUserChartsPage AUCPage;
                    AUCPage = new AssignUserChartsPage(driver);
                    AUCPage.LoadDataOnGrid();
                }
            }
            for (int i = AllTabs.Length -1; i >= 0; i--)
            {
                if (AllTabs[i] == "Assign User Charts")
                {
                    Assert.True(Page.IsAnyDataOnGrid(),ErrorMessages.NoDataOnGrid);
                    //menu.CloseCurrentTab(AllTabs[i]);
                }
                else if (AllTabs[i] == "Pending Billing Management Report")
                {
                    menu.SwitchToTab(AllTabs[i]);
                    Assert.True(Page.IsAnyDataOnGrid(), ErrorMessages.NoDataOnGrid);
                }

                else if (AllTabs[i] == "Shop Summary Report")
                {
                    menu.SwitchToTab(AllTabs[i]);
                    Assert.True(Page.IsAnyDataOnGrid(), ErrorMessages.NoDataOnGrid);
                }

               else if (AllTabs[i] == "Aged Invoice Report")
                {
                    menu.SwitchToTab(AllTabs[i]);
                    Assert.True(Page.IsAnyDataOnGrid(), ErrorMessages.NoDataOnGrid);
                }

                else if (AllTabs[i] == "Aged Invoice Report")
                {
                    menu.SwitchToTab(AllTabs[i]);
                    Assert.True(Page.IsAnyDataOnGrid(), ErrorMessages.NoDataOnGrid);
                }
               else  if (AllTabs[i] == "Part Sales by Shop Report")
                {
                    menu.SwitchToTab(AllTabs[i]);
                    Assert.True(Page.IsAnyDataOnGrid(), ErrorMessages.NoDataOnGrid);
                }

                else
                {
                    errorMsgs.Add("Data is not presend on Grid for Page: " + Pages.CreateAuthorization);
                }
            }
        }
    }
}
