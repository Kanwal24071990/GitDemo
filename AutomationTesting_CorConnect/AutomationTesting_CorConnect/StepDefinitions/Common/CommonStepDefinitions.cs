using Allure.Commons;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.AccountStatusChangeReport;
using AutomationTesting_CorConnect.PageObjects.AgedInvoiceReport;
using AutomationTesting_CorConnect.PageObjects.ASN;
using AutomationTesting_CorConnect.PageObjects.AssignUserCharts;
using AutomationTesting_CorConnect.PageObjects.BatchRequestStatus;
using AutomationTesting_CorConnect.PageObjects.BillingScheduleManagement;
using AutomationTesting_CorConnect.PageObjects.BookmarksMaintenance;
using AutomationTesting_CorConnect.PageObjects.CommunityFeeReport;
using AutomationTesting_CorConnect.PageObjects.DealerDiscountDateReport;
using AutomationTesting_CorConnect.PageObjects.DealerDueDateReport;
using AutomationTesting_CorConnect.PageObjects.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.PageObjects.DealerInvoices;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.DealerLocations;
using AutomationTesting_CorConnect.PageObjects.DealerLookup;
using AutomationTesting_CorConnect.PageObjects.DealerPartSummaryFleetBillTo;
using AutomationTesting_CorConnect.PageObjects.DealerPartSummaryFleetLocation;
using AutomationTesting_CorConnect.PageObjects.DealerPOPOQTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.DealerReleaseInvoices;
using AutomationTesting_CorConnect.PageObjects.DealerSalesSummaryBillTo;
using AutomationTesting_CorConnect.PageObjects.DealerSalesSummaryLocation;
using AutomationTesting_CorConnect.PageObjects.DealerStatements;
using AutomationTesting_CorConnect.PageObjects.Disputes;
using AutomationTesting_CorConnect.PageObjects.DraftStatementReport;
using AutomationTesting_CorConnect.PageObjects.DunningStatus;
using AutomationTesting_CorConnect.PageObjects.EntityCrossReferenceMaintenance;
using AutomationTesting_CorConnect.PageObjects.EntityGroupMaintenance;
using AutomationTesting_CorConnect.PageObjects.FleetBillToSalesSummary;
using AutomationTesting_CorConnect.PageObjects.FleetCreditLimit;
using AutomationTesting_CorConnect.PageObjects.FleetCreditLimitWatchListReport;
using AutomationTesting_CorConnect.PageObjects.FleetDiscountDateReport;
using AutomationTesting_CorConnect.PageObjects.FleetDueDateReport;
using AutomationTesting_CorConnect.PageObjects.FleetInvoicePreApprovalReport;
using AutomationTesting_CorConnect.PageObjects.FleetInvoices;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.FleetLocations;
using AutomationTesting_CorConnect.PageObjects.FleetLocationSalesSummary;
using AutomationTesting_CorConnect.PageObjects.FleetMasterInvoicesStatements;
using AutomationTesting_CorConnect.PageObjects.FleetPartCategorySalesSummaryBillTo;
using AutomationTesting_CorConnect.PageObjects.FleetPartCategorySalesSummaryLocation;
using AutomationTesting_CorConnect.PageObjects.FleetPartsCrossReference;
using AutomationTesting_CorConnect.PageObjects.FleetPartSummaryBillTo;
using AutomationTesting_CorConnect.PageObjects.FleetPartSummaryLocation;
using AutomationTesting_CorConnect.PageObjects.FleetPOPOQTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.FleetSalesSummaryBillTo;
using AutomationTesting_CorConnect.PageObjects.FleetSalesSummaryLocation;
using AutomationTesting_CorConnect.PageObjects.FleetStatements;
using AutomationTesting_CorConnect.PageObjects.FleetSummary;
using AutomationTesting_CorConnect.PageObjects.GPDraftStatements;
using AutomationTesting_CorConnect.PageObjects.GrossMarginCreditReport;
using AutomationTesting_CorConnect.PageObjects.IntercommunityInvoiceReport;
using AutomationTesting_CorConnect.PageObjects.InvoiceDetailReport;
using AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancyHistory;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceWatchList;
using AutomationTesting_CorConnect.PageObjects.LineItemReport;
using AutomationTesting_CorConnect.PageObjects.LocationSummary;
using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations;
using AutomationTesting_CorConnect.PageObjects.PCardTransactions;
using AutomationTesting_CorConnect.PageObjects.PartPriceFileUploadReport;
using AutomationTesting_CorConnect.PageObjects.PartPriceLookup;
using AutomationTesting_CorConnect.PageObjects.Parts;
using AutomationTesting_CorConnect.PageObjects.PartSalesbyFleetReport;
using AutomationTesting_CorConnect.PageObjects.PartSalesbyShopReport;
using AutomationTesting_CorConnect.PageObjects.PartsCrossReference;
using AutomationTesting_CorConnect.PageObjects.PartsLookup;
using AutomationTesting_CorConnect.PageObjects.PendingBillingManagementReport;
using AutomationTesting_CorConnect.PageObjects.PODiscrepancy;
using AutomationTesting_CorConnect.PageObjects.PODiscrepancyHistory;
using AutomationTesting_CorConnect.PageObjects.POEntry;
using AutomationTesting_CorConnect.PageObjects.POOrders;
using AutomationTesting_CorConnect.PageObjects.POQEntry;
using AutomationTesting_CorConnect.PageObjects.POTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.Price;
using AutomationTesting_CorConnect.PageObjects.PriceExceptionReport;
using AutomationTesting_CorConnect.PageObjects.PriceLookup;
using AutomationTesting_CorConnect.PageObjects.PurchaseOrders;
using AutomationTesting_CorConnect.PageObjects.PurchasingFleetSummary;
using AutomationTesting_CorConnect.PageObjects.RemittanceReport;
using AutomationTesting_CorConnect.PageObjects.ServiceRequests;
using AutomationTesting_CorConnect.PageObjects.SettlementFile;
using AutomationTesting_CorConnect.PageObjects.SettlementFileSummary;
using AutomationTesting_CorConnect.PageObjects.SummaryReconcileReport;
using AutomationTesting_CorConnect.PageObjects.SummaryRemittanceReport;
using AutomationTesting_CorConnect.PageObjects.TaxCodeConfiguration;
using AutomationTesting_CorConnect.PageObjects.TaxReviewReport;
using AutomationTesting_CorConnect.PageObjects.VendorSummary;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountStatusChangeReport;
using AutomationTesting_CorConnect.Utils.AgedInvoiceReport;
using AutomationTesting_CorConnect.Utils.ASN;
using AutomationTesting_CorConnect.Utils.BatchRequestStatus;
using AutomationTesting_CorConnect.Utils.BillingScheduleManagement;
using AutomationTesting_CorConnect.Utils.BookmarksMaintenance;
using AutomationTesting_CorConnect.Utils.CommunityFeeReport;
using AutomationTesting_CorConnect.Utils.DealerDiscountDateReport;
using AutomationTesting_CorConnect.Utils.DealerDueDateReport;
using AutomationTesting_CorConnect.Utils.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.Utils.DealerInvoices;
using AutomationTesting_CorConnect.Utils.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.Utils.DealerLocations;
using AutomationTesting_CorConnect.Utils.DealerLookup;
using AutomationTesting_CorConnect.Utils.DealerPartSummaryFleetBillTo;
using AutomationTesting_CorConnect.Utils.DealerPartSummaryFleetLocation;
using AutomationTesting_CorConnect.Utils.DealerPOPOQTransactionLookup;
using AutomationTesting_CorConnect.Utils.DealerReleaseInvoices;
using AutomationTesting_CorConnect.Utils.DealerSalesSummaryBillTo;
using AutomationTesting_CorConnect.Utils.DealerSalesSummaryLocation;
using AutomationTesting_CorConnect.Utils.DealerStatements;
using AutomationTesting_CorConnect.Utils.Disputes;
using AutomationTesting_CorConnect.Utils.DraftStatementReport;
using AutomationTesting_CorConnect.Utils.EntityCrossReferenceMaintenance;
using AutomationTesting_CorConnect.Utils.EntityGroupMaintenance;
using AutomationTesting_CorConnect.Utils.FleetBillToSalesSummary;
using AutomationTesting_CorConnect.Utils.FleetCreditLimit;
using AutomationTesting_CorConnect.Utils.FleetCreditLimitWatchListReport;
using AutomationTesting_CorConnect.Utils.FleetDiscountDateReport;
using AutomationTesting_CorConnect.Utils.FleetDueDateReport;
using AutomationTesting_CorConnect.Utils.FleetInvoicePreApprovalReport;
using AutomationTesting_CorConnect.Utils.FleetInvoices;
using AutomationTesting_CorConnect.Utils.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.Utils.FleetLocations;
using AutomationTesting_CorConnect.Utils.FleetLocationSalesSummary;
using AutomationTesting_CorConnect.Utils.FleetMasterInvoicesStatements;
using AutomationTesting_CorConnect.Utils.FleetPartCategorySalesSummaryLocation;
using AutomationTesting_CorConnect.Utils.FleetPartsCrossReference;
using AutomationTesting_CorConnect.Utils.FleetPartSummaryBillTo;
using AutomationTesting_CorConnect.Utils.FleetPartSummaryLocation;
using AutomationTesting_CorConnect.Utils.FleetPOPOQTransactionLookup;
using AutomationTesting_CorConnect.Utils.FleetSalesSummaryBillTo;
using AutomationTesting_CorConnect.Utils.FleetSalesSummaryLocation;
using AutomationTesting_CorConnect.Utils.FleetStatements;
using AutomationTesting_CorConnect.Utils.FleetSummary;
using AutomationTesting_CorConnect.Utils.GPDraftStatements;
using AutomationTesting_CorConnect.Utils.GrossMarginCreditReport;
using AutomationTesting_CorConnect.Utils.IntercommunityInvoiceReport;
using AutomationTesting_CorConnect.Utils.InvoiceDetailReport;
using AutomationTesting_CorConnect.Utils.InvoiceDiscrepancyHistory;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using AutomationTesting_CorConnect.Utils.InvoiceWatchList;
using AutomationTesting_CorConnect.Utils.LineItemReport;
using AutomationTesting_CorConnect.Utils.LocationSummary;
using AutomationTesting_CorConnect.Utils.OpenAuthorization;
using AutomationTesting_CorConnect.Utils.PartPriceFileUploadReport;
using AutomationTesting_CorConnect.Utils.PartPriceLookup;
using AutomationTesting_CorConnect.Utils.Parts;
using AutomationTesting_CorConnect.Utils.PartSalesByFleetReport;
using AutomationTesting_CorConnect.Utils.PartSalesByShopReport;
using AutomationTesting_CorConnect.Utils.PartsCrossReference;
using AutomationTesting_CorConnect.Utils.PCardTransactions;
using AutomationTesting_CorConnect.Utils.PendingBillingManagementReport;
using AutomationTesting_CorConnect.Utils.PODiscrepancy;
using AutomationTesting_CorConnect.Utils.PODiscrepancyHistory;
using AutomationTesting_CorConnect.Utils.POEntry;
using AutomationTesting_CorConnect.Utils.POOrders;
using AutomationTesting_CorConnect.Utils.POQEntry;
using AutomationTesting_CorConnect.Utils.POTransactionLookup;
using AutomationTesting_CorConnect.Utils.Price;
using AutomationTesting_CorConnect.Utils.PriceExceptionReport;
using AutomationTesting_CorConnect.Utils.PurchaseOrders;
using AutomationTesting_CorConnect.Utils.PurchasingFleetSummary;
using AutomationTesting_CorConnect.Utils.RemittanceReport;
using AutomationTesting_CorConnect.Utils.SettlementFile;
using AutomationTesting_CorConnect.Utils.SettlementFileSummary;
using AutomationTesting_CorConnect.Utils.SummaryReconcileReport;
using AutomationTesting_CorConnect.Utils.SummaryRemittanceReport;
using AutomationTesting_CorConnect.Utils.TaxCodeConfiguration;
using AutomationTesting_CorConnect.Utils.TaxReviewReport;
using AutomationTesting_CorConnect.Utils.VendorSummary;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using OpenQA.Selenium;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer;
using AutomationTesting_CorConnect.Helper;
using OpenQA.Selenium.Chrome;
using AutomationTesting_CorConnect.DriverBuilder;

namespace AutomationTesting_CorConnect.StepDefinitions.Common
{
    [Binding]
    [Parallelizable(ParallelScope.Fixtures)]
    internal class CommonStepDefinitions : DriverBuilderClass
    {
        public string PageName { get; private set; }
        DunningStatusPage DunningStatusPage;
        DisputesPage DisputesPage;
        AssignUserChartsPage AssignUserChartsPage;
        InvoiceWatchListPage InvoiceWatchListPage;
        Commons CommonPages;
        OpenAuthorizationsPage OpenAuthorizationsPage;
        DealerLookupPage DealerLookupPage;
        DealerInvoicesPage DealerInvoicesPage;
        DealerInvoiceTransactionLookupPage DealerInvoiceTransactionLookupPage;
        DealerStatementsPage DealerStatementsPage;
        FleetInvoicesPage FleetInvoicesPage;
        FleetInvoiceTransactionLookupPage FleetInvoiceTransactionLookupPage;
        FleetMasterInvoicesStatementsPage FleetMasterInvoicesStatementsPage;
        FleetStatementsPage FleetStatementsPage;
        GPDraftStatementsPage GPDraftStatementsPage;
        PCardTransactionsPage PCardTransactionsPage;
        SettlementFilePage SettlementFilePage;
        SettlementFileSummaryPage SettlementFileSummaryPage;
        AgedInvoiceReportPage AgedInvoiceReportPage;
        CommunityFeeReportPage CommunityFeeReportPage;
        DraftStatementReportPage DraftStatementReportPage;
        FleetCreditLimitPage FleetCreditLimitPage;
        FleetCreditLimitWatchListReportPage FleetCreditLimitWatchListReportPage;
        IntercommunityInvoiceReportPage IntercommunityInvoiceReportPage;
        PendingBillingManagementReportPage PendingBillingManagementReportPage;
        PartSalesbyShopReportPage PartSalesbyShopReportPage;
        SummaryReconcileReportPage SummaryReconcileReportPage;
        InvoiceDiscrepancyHistoryPage InvoiceDiscrepancyHistoryPage;
        DealerDueDateReportPage DealerDueDateReportPage;
        DealerDiscountDateReportPage DealerDiscountDateReportPage;
        DealerInvoicePreApprovalReportPage DealerInvoicePreApprovalReportPage;
        DealerLocationsPage DealerLocationsPage;
        DealerPartSummaryFleetBillToPage DealerPartSummaryFleetBillToPage;
        DealerPartSummaryFleetLocationPage DealerPartSummaryFleetLocationPage;
        DealerSalesSummaryBillToPage DealerSalesSummaryBillToPage;
        DealerSalesSummaryLocationPage DealerSalesSummaryLocationPage;
        GrossMarginCreditReportPage GrossMarginCreditReportPage;
        PriceExceptionReportPage PriceExceptionReportPage;
        PurchasingFleetSummaryPage PurchasingFleetSummaryPage;
        PartPriceLookupPage PartPriceLookupPage;
        RemittanceReportPage RemittanceReportPage;
        SummaryRemittanceReportPage SummaryRemittanceReportPage;
        TaxReviewReportPage TaxReviewReportPage;
        ASNPage ASNPage;
        PurchaseOrdersPage PurchaseOrdersPage;
        POTransactionLookupPage POTransactionLookupPage;
        ServiceRequestsPage ServiceRequestsPage;
        VendorSummaryPage VendorSummaryPage;
        LocationSummaryPage LocationSummaryPage;
        FleetSummaryPage FleetSummaryPage;
        FleetBillToSalesSummaryPage FleetBillToSalesSummaryPage;
        FleetDueDateReportPage FleetDueDateReportPage;
        FleetDiscountDateReportPage FleetDiscountDateReportPage;
        FleetInvoicePreApprovalReportPage FleetInvoicePreApprovalReportPage;
        FleetLocationsPage FleetLocationsPage;
        FleetLocationSalesSummaryPage FleetLocationSalesSummaryPage;
        FleetPartCategorySalesSummaryBillToPage FleetPartCategorySalesSummaryBillToPage;
        FleetPartCategorySalesSummaryLocationPage FleetPartCategorySalesSummaryLocationPage;
        FleetPartSummaryBillToPage FleetPartSummaryBillToPage;
        FleetPartSummaryLocationPage FleetPartSummaryLocationPage;
        FleetSalesSummaryBillToPage FleetSalesSummaryBillToPage;
        FleetSalesSummaryLocationPage FleetSalesSummaryLocationPage;
        LineItemReportPage LineItemReportPage;
        DealerReleaseInvoicesPage DealerReleaseInvoicesPage;
        InvoiceEntryPage InvoiceEntryPage;
        PODiscrepancyPage PODiscrepancyPage;
        PODiscrepancyHistoryPage PODiscrepancyHistoryPage;
        POOrdersPage POOrdersPage;
        PriceLookupPage PriceLookupPage;
        PartsLookupPage PartsLookupPage;
        DealerPOPOQTransactionLookupPage DealerPOPOQTransactionLookupPage;
        POEntryPage POEntryPage;
        POQEntryPage POQEntryPage;
        FleetPOPOQTransactionLookupPage FleetPOPOQTransactionLookupPage;
        BillingScheduleManagementPage BillingScheduleManagementPage;
        FleetPartsCrossReferencePage FleetPartsCrossReferencePage;
        PartsPage PartsPage;
        PartsCrossReferencePage PartsCrossReferencePage;
        PartPriceFileUploadReportPage PartPriceFileUploadReportPage;
        PricePage PricePage;
        AccountStatusChangeReportPage AccountStatusChangeReportPage;
        BatchRequestStatusPage BatchRequestStatusPage;
        InvoiceDetailReportPage InvoiceDetailReportPage;
        PartSalesbyFleetReportPage PartSalesbyFleetReportPage;
        EntityCrossReferenceMaintenancePage EntityCrossReferenceMaintenancePage;
        TaxCodeConfigurationPage TaxCodeConfigurationPage;
        BookmarksMaintenancePage BookmarksMaintenancePage;
        EntityGroupMaintenancePage EntityGroupMaintenancePage;

        List<string> errorMsgs = new List<string>();

        [Given(@"User is on login page")]
        public void NavigatetoLoginPage()
        {
            OpenURL();
        }

        [Given(@"User ""([^""]*)"" is on ""(.*)"" page")]
        public void PerformLoginAndNavigateToPage(string userType, string pageName)
        {
            UserLogsIn(userType);
            PageNavigation(pageName);
        }

        [Given(@"Username ""([^""]*)"" is on ""([^""]*)"" page")]
        public void GivenUsernameIsOnPage(string userName, string pageName)
        {
            UserNameLogsIn(userName);
            PageNavigation(pageName);
        }


        [Given(@"User is on ""([^""]*)"" page")]
        public void GivenUserIsOnPage(string pageName)
        {
            PageNavigation(pageName);
        }

        [Given(@"User ""([^""]*)"" logs in")]
        public void UserLogsIn(string userType)
        {
            PerformLogin(userType);
        }

        [Given(@"Username ""([^""]*)"" logs in")]
        public void UserNameLogsIn(string userName)
        {
            PerformLoginWithUserName(userName);
        }

        [Given(@"User ""([^""]*)"" is on ""([^""]*)"" popup page")]
        public void GivenUserIsOnPopupPage(string userType, string pageName)
        {
            UserLogsIn(userType);
            PopupPageNavigation(pageName);
        }


        [Given(@"User navigates to ""(.*)"" page")]
        public void PageNavigation(string pageTitle)
        {
            PageName = pageTitle;
            string[] singlePages = { "Manage User Notifications", "Assign Entity Chart", "Assign Entity Function", "Franchise Code Management", "User Group Setup", "Part Categories" };
            string[] noGridMenu = { "Create Authorization", "Update Credit" };
            //if (!menu.IsScreenExemptedForClientUserType(pageTitle.Replace(" ", "")))
            //{
            if (singlePages.Contains(pageTitle))
            {
                menu.OpenPage(pageTitle, false);
            }
            else if (noGridMenu.Contains(pageTitle))
            {
                menu.OpenPage(pageTitle, false, true);
            }
            else
            {
                menu.OpenPage(pageTitle);
            }
        }

        protected void OpenAnotherSingleGridPage(string pageTitle)
        {
            menu.OpenPage(pageTitle, false);
            menu.WaitForMsg(ButtonsAndMessages.LoadingWithElipsis);
        }
        protected void OpenAnotherPage(string pageTitle)
        {
            menu.OpenPage(pageTitle, false, false, true);
        }

        [Given(@"User is on new ""([^""]*)"" page")]
        public void GivenUserIsOnNewPage(string pageTitle)
        {
            PageName = pageTitle;
            string[] singlePages = { "Manage User Notifications", "Assign Entity Chart", "Assign Entity Function", "Franchise Code Management", "User Group Setup", "Part Categories" };
            if (singlePages.Contains(pageTitle))
            {
                OpenAnotherSingleGridPage(pageTitle);
            }
            else
            {
                OpenAnotherPage(pageTitle);
            }
        }


        [Given(@"User is on ""([^""]*)"" page and populates grid")]
        public void GivenUserIsOnPageAndPopulatesGrid(string pageTitle)
        {
            PageName = pageTitle;
            GivenUserIsOnPage(pageTitle);
            PopulateGrid();
        }

        [Given(@"User is on new ""([^""]*)"" page and populates grid")]
        public void GivenUserIsOnNewPageAndPopulatesGrid(string pageTitle)
        {
            PageName = pageTitle;
            GivenUserIsOnNewPage(pageTitle);
            PopulateGrid();
        }

        [Given(@"User is on new ""([^""]*)"" page and populates grid with Search Criteria")]
        public void GivenUserIsOnNewPageAndPopulatesGridWithSearchCriteria(string pageTitle)
        {
            PageName = pageTitle;
            GivenUserIsOnNewPage(pageTitle);
            if (PageName.Equals("Fleet Statements"))
            {
                FleetStatementsPage fltPage = new FleetStatementsPage(driver);
                FleetStatementsUtil.GetData(out string from, out string to);
                fltPage.PopulateGrid(from, to);
            }
            else
            {
                PopulateGrid();
            }
        }



        [When(@"User opens ""([^""]*)"" page again")]
        public void WhenUserOpensPageAgain(string pageTitle)
        {
            PageName = pageTitle;
            string[] singlePages = { "Manage User Notifications", "Assign Entity Chart", "Assign Entity Function", "Franchise Code Management", "User Group Setup", "Part Categories" };
            if (singlePages.Contains(pageTitle))
            {
                OpenAnotherSingleGridPage(pageTitle);
            }
            else
            {
                OpenAnotherPage(pageTitle);
            }
        }


        [Given(@"User is on single grid ""([^""]*)"" page")]
        public void GivenUserIsOnSingleGridPage(string pageName)
        {
            menu.OpenSingleGridPage(pageName);
        }

        [Given(@"User is on new single grid ""([^""]*)"" page")]
        public void GivenUserIsOnNewSingleGridPage(string pageName)
        {
            menu.OpenSingleGridPage(pageName);
            menu.WaitForMsg(ButtonsAndMessages.LoadingWithElipsis);
        }

        [Given(@"User is on new popup page ""([^""]*)""")]
        public void GivenUserIsOnNewPopupPage(string pageTitle)
        {
            PageName = pageTitle;
            menu.OpenPopUpPage(PageName);
            string windowTitle = driver.Title;
            while (windowTitle.ToLower().Contains("untitled"))
            {
                windowTitle = driver.Title;
            }
        }

        [Given(@"User Closes Popup page")]
        public void GivenUserClosesPopupPage()
        {
            menu.ClosePopupWindow();
        }

        [Given(@"User Clears Search Criteria")]
        public void GivenUserClearsSearchCriteria()
        {
            menu.ButtonClick(ButtonsAndMessages.Clear);
            menu.AcceptAlert();
            menu.WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            menu.WaitForGridLoad();
        }

        [Given(@"User navigates to ""([^""]*)"" popup page")]
        public void PopupPageNavigation(string pageTitle)
        {
            menu.OpenPopUpPage(pageTitle);
        }


        [Given(@"Loads data on ""([^""]*)"" page:")]
        public void LoadsDataOnPage(string pageTitle, Table table)
        {
            var details = table.CreateInstance<PageDataObjects>();
            switch (pageTitle)
            {
                case "Dunning Status":
                    DunningStatusPage = new DunningStatusPage(driver);
                    DunningStatusPage.LoadDataOnGrid(details.Fleet, details.DunningStatus, details.From, details.To);
                    break;
                case "Disputes":
                    DisputesPage = new DisputesPage(driver);
                    DisputesPage.LoadDataOnGrid(details.Dealer, details.Fleet, details.Status, details.DateType, details.DateRange, details.From, details.To, details.Currency, details.ResolutionInformation);
                    break;

            }

        }

        [Given(@"Loads data on ""([^""]*)"" page")]
        public void LoadsDataOnPageFromUtils(string pageTitle)
        {
            switch (pageTitle)
            {
                case "Invoice Watch List":
                    InvoiceWatchListPage = new InvoiceWatchListPage(driver);
                    InvoiceWatchListUtils.GetData(out string FromDate, out string ToDate);
                    InvoiceWatchListPage.LoadDataOnGrid(FromDate, ToDate);
                    break;
                case "Open Authorizations":
                    OpenAuthorizationsPage = new OpenAuthorizationsPage(driver);
                    OpenAuthorizationUtils.GetData(out FromDate, out ToDate);
                    OpenAuthorizationsPage.LoadDataOnGrid(FromDate, ToDate);
                    break;
                case "Dealer Lookup":
                    DealerLookupPage = new DealerLookupPage(driver);
                    DealerLookupUtil.GetData(out string dealerCode);
                    DealerLookupPage.LoadDataOnGrid(dealerCode);
                    break;
                case "Disputes":
                    DisputesPage = new DisputesPage(driver);
                    DisputesUtil.GetData(out string from, out string to);
                    DisputesPage.PopulateGrid(from, to);
                    break;
                case "Dealer Invoices":
                    DealerInvoicesPage = new DealerInvoicesPage(driver);
                    DealerInvoicesUtils.GetData(out from, out to);
                    DealerInvoicesPage.LoadDataOnGrid(from, to, false);
                    break;
                case "Dealer Invoice Transaction Lookup":
                    DealerInvoiceTransactionLookupPage = new DealerInvoiceTransactionLookupPage(driver);
                    DealerInvoiceTransactionLookupUtil.GetData(out from, out to);
                    DealerInvoiceTransactionLookupPage.PopulateGrid(from, to, false);
                    break;
                case "Dealer Statements":
                    DealerStatementsPage = new DealerStatementsPage(driver);
                    DealerStatementsUtils.GetData(out from, out to);
                    DealerStatementsPage.LoadDataOnGrid(from, to);
                    break;
                case "Fleet Invoices":
                    FleetInvoicesPage = new FleetInvoicesPage(driver);
                    FleetInvoicesUtils.GetData(out from, out to);
                    FleetInvoicesPage.LoadDataOnGrid(from, to, false);
                    break;
                case "Fleet Invoice Transaction Lookup":
                    FleetInvoiceTransactionLookupPage = new FleetInvoiceTransactionLookupPage(driver);
                    FleetInvoiceTransactionLookupUtil.GetData(out from, out to);
                    FleetInvoiceTransactionLookupPage.PopulateGrid(from, to, false);
                    break;
                case "Fleet Master Invoices/Statements":
                    FleetMasterInvoicesStatementsPage = new FleetMasterInvoicesStatementsPage(driver);
                    FleetMasterInvoicesStatementsUtil.GetData(out from, out to);
                    FleetMasterInvoicesStatementsPage.PopulateGrid(from, to);
                    break;
                case "Fleet Statements":
                    FleetStatementsPage = new FleetStatementsPage(driver);
                    FleetStatementsUtil.GetData(out from, out to);
                    FleetStatementsPage.PopulateGrid(from, to);
                    break;
                case "GP Draft Statements":
                    GPDraftStatementsPage = new GPDraftStatementsPage(driver);
                    GPDraftStatementsUtil.GetData(out from, out to);
                    GPDraftStatementsPage.PopulateGrid(from, to);
                    break;
                case "P-Card Transactions":
                    PCardTransactionsPage = new PCardTransactionsPage(driver);
                    PCardTransactionsUtil.GetData(out from, out to);
                    PCardTransactionsPage.PopulateGrid(from, to);
                    break;
                case "Settlement File":
                    SettlementFilePage = new SettlementFilePage(driver);
                    SettlementFileUtil.GetData(out from, out to);
                    SettlementFilePage.PopulateGrid(from, to);
                    break;
                case "Settlement File Summary":
                    SettlementFileSummaryPage = new SettlementFileSummaryPage(driver);
                    SettlementFileSummaryUtil.GetData(out from, out to);
                    SettlementFileSummaryPage.PopulateGrid(from, to);
                    break;
                case "Aged Invoice Report":
                    AgedInvoiceReportPage = new AgedInvoiceReportPage(driver);
                    AgedInvoiceReportUtil.GetData(out string location);
                    AgedInvoiceReportPage.PopulateGrid(location);
                    break;
                case "Community Fee Report":
                    CommunityFeeReportPage = new CommunityFeeReportPage(driver);
                    CommunityFeeReportUtil.GetData(out from, out to);
                    CommunityFeeReportUtil.GetCurrencyCode(out string currencyCode);
                    CommunityFeeReportPage.PopulateGrid(from, to, currencyCode);
                    break;
                case "Draft Statement Report":
                    DraftStatementReportPage = new DraftStatementReportPage(driver);
                    DraftStatementReportUtil.GetData(out from, out to);
                    DraftStatementReportPage.PopulateGrid(from, to);
                    break;
                case "Fleet Credit Limit":
                    FleetCreditLimitPage = new FleetCreditLimitPage(driver);
                    FleetCreditLimitUtil.GetData(out location);
                    FleetCreditLimitPage.PopulateGrid(location);
                    break;
                case "Fleet Credit Limit Watch List Report":
                    FleetCreditLimitWatchListReportPage = new FleetCreditLimitWatchListReportPage(driver);
                    FleetCreditLimitWatchListReportUtil.GetData(out location);
                    FleetCreditLimitWatchListReportPage.PopulateGrid(location);
                    break;
                case "Intercommunity Invoice Report":
                    IntercommunityInvoiceReportPage = new IntercommunityInvoiceReportPage(driver);
                    IntercommunityInvoiceReportUtils.GetData(out from, out to);
                    IntercommunityInvoiceReportPage.LoadDataOnGrid(from, to);
                    break;
                case "Pending Billing Management Report":
                    PendingBillingManagementReportPage = new PendingBillingManagementReportPage(driver);
                    PendingBillingManagementReportUtil.GetData(out string companyName);
                    Assert.That(companyName, Is.Not.Null.Or.Not.Empty, $"Null or empty CompanyName returned from DB");
                    if (!string.IsNullOrEmpty(companyName))
                    {
                        PendingBillingManagementReportPage.PopulateGrid(companyName);
                    }
                    break;
                case "Part Sales by Shop Report":
                    PartSalesbyShopReportPage = new PartSalesbyShopReportPage(driver);
                    PartSalesByShopReportUtils.GetDateData(out string fromDate, out string toDate);
                    PartSalesbyShopReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Summary Reconcile Report":
                    SummaryReconcileReportPage = new SummaryReconcileReportPage(driver);
                    SummaryReconcileReportUtil.GetData(out from, out to);
                    SummaryReconcileReportPage.PopulateGrid(from, to);
                    break;
                case "Invoice Discrepancy History":
                    InvoiceDiscrepancyHistoryPage = new InvoiceDiscrepancyHistoryPage(driver);
                    InvoiceDiscrepancyHistoryUtils.GetData(out from, out to);
                    InvoiceDiscrepancyHistoryPage.LoadDataOnGrid(from, to);
                    break;
                case "Dealer Due Date Report":
                    DealerDueDateReportPage = new DealerDueDateReportPage(driver);
                    DealerDueDateReportUtils.GetData(out from, out to);
                    DealerDueDateReportPage.LoadDataOnGrid(from, to);
                    break;
                case "Dealer Discount Date Report":
                    DealerDiscountDateReportPage = new DealerDiscountDateReportPage(driver);
                    DealerDiscountDateReportUtils.GetData(out from, out to);
                    DealerDiscountDateReportPage.LoadDataOnGrid(from, to);
                    break;
                case "Dealer Invoice Pre-Approval Report":
                    DealerInvoicePreApprovalReportPage = new DealerInvoicePreApprovalReportPage(driver);
                    DealerInvoicePreApprovalReportUtils.GetData(out FromDate, out ToDate);
                    DealerInvoicePreApprovalReportPage.LoadDataOnGrid(FromDate, ToDate);
                    break;
                case "Dealer Locations":
                    DealerLocationsPage = new DealerLocationsPage(driver);
                    DealerLocationsUtil.GetData(out string dealerCoder);
                    DealerLocationsPage.LoadDataOnGrid(dealerCoder);
                    break;
                case "Dealer Part Summary - Fleet Bill To":
                    DealerPartSummaryFleetBillToPage = new DealerPartSummaryFleetBillToPage(driver);
                    DealerPartSummaryFleetBillToUtils.GetDateData(out fromDate, out toDate);
                    DealerPartSummaryFleetBillToPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Dealer Part Summary - Fleet Location":
                    DealerPartSummaryFleetLocationPage = new DealerPartSummaryFleetLocationPage(driver);
                    DealerPartSummaryFleetLocationUtils.GetDateData(out fromDate, out toDate);
                    DealerPartSummaryFleetLocationPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Dealer Sales Summary - Bill To":
                    DealerSalesSummaryBillToPage = new DealerSalesSummaryBillToPage(driver);
                    DealerSalesSummaryBillToUtils.GetDateData(out fromDate, out toDate);
                    DealerSalesSummaryBillToPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Dealer Sales Summary - Location":
                    DealerSalesSummaryLocationPage = new DealerSalesSummaryLocationPage(driver);
                    DealerSalesSummaryLocationUtil.GetData(out from, out to);
                    DealerSalesSummaryLocationPage.PopulateGrid(from, to);
                    break;
                case "Gross Margin Credit Report":
                    GrossMarginCreditReportPage = new GrossMarginCreditReportPage(driver);
                    GrossMarginCreditReportUtil.GetData(out from, out to);
                    GrossMarginCreditReportPage.PopulateGrid(from, to);
                    break;
                case "Price Exception Report":
                    PriceExceptionReportPage = new PriceExceptionReportPage(driver);
                    PriceExceptionReportUtils.GetDateData(out fromDate, out toDate);
                    PriceExceptionReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Purchasing Fleet Summary":
                    PurchasingFleetSummaryPage = new PurchasingFleetSummaryPage(driver);
                    PurchasingFleetSummaryUtils.GetData(out from, out to);
                    PurchasingFleetSummaryPage.LoadDataOnGrid(string.Empty, string.Empty, from, to);
                    break;
                case "Part Price Lookup":
                    PartPriceLookupPage = new PartPriceLookupPage(driver);
                    dealerCode = PartPriceLookupUtil.GetDealerCode();
                    string fleetCode = PartPriceLookupUtil.GetFleetCode();
                    string partNumber = PartPriceLookupUtil.GetPartNumber();
                    string date = CommonUtils.GetCurrentDate();
                    if (string.IsNullOrEmpty(dealerCode))
                        Assert.Fail("Dealer Code is empty");
                    if (string.IsNullOrEmpty(fleetCode))
                        Assert.Fail("Fleet Code is empty");
                    if (string.IsNullOrEmpty(partNumber))
                        Assert.Fail("Part Number is empty");
                    PartPriceLookupPage.PopulateGrid(dealerCode, fleetCode, partNumber, date);
                    break;
                case "Remittance Report":
                    RemittanceReportPage = new RemittanceReportPage(driver);
                    RemittanceReportUtils.GetDateData(out fromDate, out toDate);
                    RemittanceReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Summary Remittance Report":
                    SummaryRemittanceReportPage = new SummaryRemittanceReportPage(driver);
                    SummaryRemittanceReportUtils.GetData(out FromDate, out ToDate);
                    SummaryRemittanceReportPage.PopulateGrid(FromDate, ToDate);
                    break;
                case "Tax Review Report":
                    TaxReviewReportPage = new TaxReviewReportPage(driver);
                    TaxReviewReportUtils.GetDateData(out fromDate, out toDate);
                    TaxReviewReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "ASN":
                    ASNPage = new ASNPage(driver);
                    ASNUtils.GetData(out FromDate, out ToDate);
                    ASNPage.LoadDataOnGrid(FromDate, ToDate);
                    break;
                case "Purchase Orders":
                    PurchaseOrdersPage = new PurchaseOrdersPage(driver);
                    PurchaseOrdersUtils.GetData(out FromDate, out ToDate);
                    PurchaseOrdersPage.LoadDataOnGrid(FromDate, ToDate);
                    break;
                case "Service Requests":
                    ServiceRequestsPage = new ServiceRequestsPage(driver);
                    ServiceRequestsPage.LoadDataOnGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-365)), CommonUtils.GetCurrentDate());
                    break;
                case "Vendor Summary":
                    VendorSummaryPage = new VendorSummaryPage(driver);
                    VendorSummaryUtils.GetData(out FromDate, out ToDate);
                    VendorSummaryPage.LoadDataOnGrid(FromDate, ToDate);
                    break;
                case "Location Summary":
                    LocationSummaryPage = new LocationSummaryPage(driver);
                    LocationSummaryUtils.GetData(out FromDate, out ToDate);
                    LocationSummaryPage.LoadDataOnGrid(FromDate, ToDate);
                    break;
                case "Fleet Summary":
                    FleetSummaryPage = new FleetSummaryPage(driver);
                    FleetSummaryUtils.GetData(out FromDate, out ToDate);
                    FleetSummaryPage.LoadDataOnGrid(FromDate, ToDate);
                    break;
                case "Fleet Bill To Sales Summary":
                    FleetBillToSalesSummaryPage = new FleetBillToSalesSummaryPage(driver);
                    FleetBillToSalesSummaryUtils.GetDateData(out fromDate, out toDate);
                    FleetBillToSalesSummaryPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Fleet Due Date Report":
                    FleetDueDateReportPage = new FleetDueDateReportPage(driver);
                    FleetDueDateReportUtils.GetDateData(out from, out to);
                    FleetDueDateReportPage.PopulateGrid(from, to);
                    break;
                case "Fleet Discount Date Report":
                    FleetDiscountDateReportPage = new FleetDiscountDateReportPage(driver);
                    FleetDiscountDateReportUtils.GetDateData(out fromDate, out toDate);
                    FleetDiscountDateReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Fleet Invoice Pre-Approval Report":
                    FleetInvoicePreApprovalReportPage = new FleetInvoicePreApprovalReportPage(driver);
                    FleetInvoicePreApprovalReportUtils.GetData(out from, out to);
                    FleetInvoicePreApprovalReportPage.LoadDataOnGrid(from, to);
                    break;
                case "Fleet Locations":
                    FleetLocationsPage = new FleetLocationsPage(driver);
                    FleetLocationsUtil.GetData(out fleetCode);
                    FleetLocationsPage.LoadDataOnGrid(fleetCode);
                    break;
                case "Fleet Location Sales Summary":
                    FleetLocationSalesSummaryPage = new FleetLocationSalesSummaryPage(driver);
                    FleetLocationSalesSummaryUtils.GetDateData(out from, out to);
                    FleetLocationSalesSummaryPage.PopulateGrid(from, to);
                    break;
                case "Fleet Part Category Sales Summary - Bill To":
                    FleetPartCategorySalesSummaryBillToPage = new FleetPartCategorySalesSummaryBillToPage(driver);
                    FleetPartCategorySalesSummaryLocationUtils.GetDateData(out from, out to);
                    FleetPartCategorySalesSummaryBillToPage.PopulateGrid(from, to);
                    break;
                case "Fleet Part Category Sales Summary - Location":
                    FleetPartCategorySalesSummaryLocationPage = new FleetPartCategorySalesSummaryLocationPage(driver);
                    FleetPartCategorySalesSummaryLocationUtils.GetDateData(out from, out to);
                    FleetPartCategorySalesSummaryLocationPage.PopulateGrid(from, to);
                    break;
                case "Fleet Part Summary - Bill To":
                    FleetPartSummaryBillToPage = new FleetPartSummaryBillToPage(driver);
                    FleetPartSummaryBillToUtils.GetData(out from, out to);
                    FleetPartSummaryBillToPage.PopulateGrid(from, to);
                    break;
                case "Fleet Part Summary - Location":
                    FleetPartSummaryLocationPage = new FleetPartSummaryLocationPage(driver);
                    FleetPartSummaryLocationUtils.GetData(out from, out to);
                    FleetPartSummaryLocationPage.PopulateGrid(from, to);
                    break;
                case "Fleet Sales Summary - Bill To":
                    FleetSalesSummaryBillToPage = new FleetSalesSummaryBillToPage(driver);
                    FleetSalesSummaryBillToUtils.GetData(out FromDate, out ToDate);
                    FleetSalesSummaryBillToPage.PopulateGrid(FromDate, ToDate);
                    break;
                case "Fleet Sales Summary - Location":
                    FleetSalesSummaryLocationPage = new FleetSalesSummaryLocationPage(driver);
                    FleetSalesSummaryLocationUtils.GetDateData(out from, out to);
                    FleetSalesSummaryLocationPage.PopulateGrid(from, to);
                    break;
                case "Line Item Report":
                    LineItemReportPage = new LineItemReportPage(driver);
                    LineItemReportUtils.GetData(out from, out to);
                    LineItemReportPage.LoadDataOnGrid(from, to);
                    break;
                case "Dealer Release Invoices":
                    DealerReleaseInvoicesPage = new DealerReleaseInvoicesPage(driver);
                    DealerReleaseInvoicesUtils.GetData(out from, out to);
                    DealerReleaseInvoicesPage.LoadDataOnGrid(from, to);
                    break;
                case "Invoice Entry":
                    InvoiceEntryPage = new InvoiceEntryPage(driver);
                    InvoiceEntryUtils.GetData(out from, out to);
                    InvoiceEntryPage.LoadDataOnGrid(from, to);
                    break;
                case "PO Discrepancy":
                    PODiscrepancyPage = new PODiscrepancyPage(driver);
                    PODiscrepancyUtils.GetData(out from, out to);
                    PODiscrepancyPage.LoadDataOnGrid(from, to);
                    break;
                case "PO Discrepancy History":
                    PODiscrepancyHistoryPage = new PODiscrepancyHistoryPage(driver);
                    PODiscrepancyHistoryUtils.GetData(out from, out to);
                    PODiscrepancyHistoryPage.LoadDataOnGrid(from, to);
                    break;
                case "PO Orders":
                    POOrdersPage = new POOrdersPage(driver);
                    POOrdersUtils.GetData(out from, out to);
                    POOrdersPage.LoadDataOnGrid(from, to);
                    break;
                case "PO Transaction Lookup":
                    POTransactionLookupPage = new POTransactionLookupPage(driver);
                    POTransactionLookupUtils.GetData(out from, out to);
                    POTransactionLookupPage.LoadDataOnGrid(from, to);
                    break;
                case "Price Lookup":
                    PriceLookupPage = new PriceLookupPage(driver);
                    string partNum = PartsUtil.GetPartNumber();
                    PriceLookupPage.PopulateGrid(partNum);
                    break;
                case "Parts Lookup":
                    PartsLookupPage = new PartsLookupPage(driver);
                    partNum = PartsUtil.GetPartNumber();
                    PartsLookupPage.PopulateGrid(partNum);
                    break;
                case "Dealer PO/POQ Transaction Lookup":
                    DealerPOPOQTransactionLookupPage = new DealerPOPOQTransactionLookupPage(driver);
                    DealerPOPOQTransactionLookupUtil.GetDateData(out from, out to);
                    DealerPOPOQTransactionLookupPage.PopulateGrid(from, to);
                    break;
                case "PO Entry":
                    POEntryPage = new POEntryPage(driver);
                    POEntryUtils.GetDateData(out from, out to);
                    POEntryPage.PopulateGrid(from, to);
                    break;
                case "POQ Entry":
                    POQEntryPage = new POQEntryPage(driver);
                    POQEntryUtils.GetDateData(out from, out to);
                    POQEntryPage.PopulateGrid(from, to);
                    break;
                case "Fleet PO/POQ Transaction Lookup":
                    FleetPOPOQTransactionLookupPage = new FleetPOPOQTransactionLookupPage(driver);
                    FleetPOPOQTransactionLookupUtil.GetData(out from, out to);
                    FleetPOPOQTransactionLookupPage.LoadDataOnGrid(from, to);
                    break;
                case "Billing Schedule Management":
                    BillingScheduleManagementPage = new BillingScheduleManagementPage(driver);
                    BillingScheduleManagementUtils.GetFilterData(out companyName, out string effectiveDate);
                    if (string.IsNullOrEmpty(companyName))
                        Assert.Fail("Company Name returned empty from DB");
                    BillingScheduleManagementPage.PopulateGrid(companyName, string.Empty);
                    break;
                case "Fleet Parts Cross Reference":
                    FleetPartsCrossReferencePage = new FleetPartsCrossReferencePage(driver);
                    FleetPartsCrossReferenceUtil.GetData(out fleetCode);
                    FleetPartsCrossReferencePage.PopolateGrid(fleetCode);
                    break;
                case "Parts":
                    PartsPage = new PartsPage(driver);
                    string searchPartNumber = PartsUtil.GetPartNumber();
                    PartsPage.PopulateGrid(searchPartNumber);
                    break;
                case "Parts Cross Reference":
                    PartsCrossReferencePage = new PartsCrossReferencePage(driver);
                    companyName = PartsCrossReferenceUtil.GetCompanyNameCode();
                    PartsCrossReferencePage.PopulateGrid(companyName);
                    break;
                case "Part/Price File Upload Report":
                    PartPriceFileUploadReportPage = new PartPriceFileUploadReportPage(driver);
                    PartPriceFileUploadReportUtils.GetDateData(out fromDate, out toDate);
                    PartPriceFileUploadReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Price":
                    PricePage = new PricePage(driver);
                    searchPartNumber = PriceUtils.GetPartNumber();
                    if (string.IsNullOrEmpty(searchPartNumber))
                    {
                        Assert.Fail("Part Number returned empty from DB");
                    }
                    PricePage.PopulateGrid(searchPartNumber);
                    break;
                case "Account Status Change Report":
                    AccountStatusChangeReportPage = new AccountStatusChangeReportPage(driver);
                    AccountStatusChangeReportUtils.GetDateData(out fromDate, out toDate);
                    AccountStatusChangeReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Batch Request Status":
                    BatchRequestStatusPage = new BatchRequestStatusPage(driver);
                    BatchRequestStatusUtils.GetDateData(out fromDate, out toDate);
                    BatchRequestStatusPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Invoice Detail Report":
                    InvoiceDetailReportPage = new InvoiceDetailReportPage(driver);
                    InvoiceDetailReportUtils.GetDateData(out fromDate, out toDate);
                    InvoiceDetailReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Part Sales By Fleet Report":
                    PartSalesbyFleetReportPage = new PartSalesbyFleetReportPage(driver);
                    PartSalesByFleetReportUtils.GetDateData(out fromDate, out toDate);
                    PartSalesbyFleetReportPage.PopulateGrid(fromDate, toDate);
                    break;
                case "Entity Cross Reference Maintenance":
                    EntityCrossReferenceMaintenancePage = new EntityCrossReferenceMaintenancePage(driver);
                    EntityCrossReferenceMaintenanceUtil.GetCorCentricCode(out string corCentricCode, out string type);
                    if (corCentricCode == null || type == null)
                    {
                        Assert.Fail(GetErrorMessage(ErrorMessages.ErrorGettingData));
                    }
                    EntityCrossReferenceMaintenancePage.PopulateGrid(corCentricCode);
                    break;
                case "Tax Code Configuration":
                    TaxCodeConfigurationPage = new TaxCodeConfigurationPage(driver);
                    string lineType = TaxCodeConfigurationUtils.GetLineType();
                    Assert.NotNull(lineType, GetErrorMessage(ErrorMessages.ErrorGettingData));
                    TaxCodeConfigurationPage.PopulateGrid(lineType);
                    break;
                case "Bookmarks Maintenance":
                    BookmarksMaintenancePage = new BookmarksMaintenancePage(driver);
                    var bookMarkId = BookmarksMaintenanceUtil.GetBookMark();
                    Assert.IsNotNull(bookMarkId, GetErrorMessage(ErrorMessages.ErrorGettingData));
                    BookmarksMaintenancePage.PopulateGrid(bookMarkId);
                    break;
                case "Entity Group Maintenance":
                    EntityGroupMaintenancePage = new EntityGroupMaintenancePage(driver);
                    var groupName = EntityGroupMaintenanceUtil.GetGroupName();
                    Assert.IsNotNull(groupName, GetErrorMessage(ErrorMessages.ErrorGettingData));
                    EntityGroupMaintenancePage.PopulateGrid(groupName, string.Empty);
                    break;
            }

        }

        [Given(@"Loads data on ""([^""]*)"" page with invoice number")]
        public void LoadsDataOnPageFromInvoiceNumber(string pageTitle)
        {
            string InvNum = null;
            switch (pageTitle)
            {
                case "Dealer Invoices":
                    DealerInvoicesPage = new DealerInvoicesPage(driver);
                    InvNum = CommonUtils.GetInvoiceNumberForCurrentTransaction();
                    DealerInvoicesPage.LoadDataOnGrid(InvNum);
                    break;
                case "Fleet Invoices":
                    FleetInvoicesPage = new FleetInvoicesPage(driver);
                    InvNum = CommonUtils.GetInvoiceNumberForCurrentTransaction();
                    FleetInvoicesPage.GridLoadWithInvNum(InvNum);
                    break;
                case "Fleet Invoice Transaction Lookup":
                    FleetInvoiceTransactionLookupPage = new FleetInvoiceTransactionLookupPage(driver);
                    InvNum = CommonUtils.GetInvoiceNumberForCurrentTransaction();
                    FleetInvoiceTransactionLookupPage.LoadDataOnGrid(InvNum);
                    break;
                case "Dealer Invoice Transaction Lookup":
                    DealerInvoiceTransactionLookupPage = new DealerInvoiceTransactionLookupPage(driver);
                    InvNum = CommonUtils.GetInvoiceNumberForCurrentTransaction();
                    DealerInvoiceTransactionLookupPage.LoadDataOnGrid(InvNum);
                    break;
            }
        }

        [Given(@"Verify Headers ""([^""]*)"" Search for Page ""([^""]*)""")]
        public void VerifyHeadersForPage(string msgValidation, string pageTitle)
        {
            object p = CreateInstance();
            object result = null;
            if (msgValidation == "Before")
            {
                result = InvokeMethod("ValidateTableHeadersFromFile", p, true);
                errorMsgs.AddRange((List<string>)result);
            }
            else if (msgValidation == "After")
            {
                //int CurrentRowCount = (int)InvokeMethod("GetRowCountCurrentPage", p);
                //if (CurrentRowCount > 0)
                //{
                result = InvokeMethod("ValidateTableHeadersFromFile", p, false);
                errorMsgs.AddRange((List<string>)result);
                //}
                //else 
                //{
                //    errorMsgs.Add("No Data Present After Search");
                //}
            }
            //Assert.Multiple(() =>
            //{
            //    foreach (var errorMsg in errorMsgs)
            //    {
            //        Assert.Fail(GetErrorMessage(errorMsg));
            //    }
            //});
        }

        [Then(@"Verify Errors for all pages")]
        public void ValidatesErrorsForHeaders()
        {
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }


        [StepDefinition(@"User closes ""([^""]*)"" and navigates to ""([^""]*)"" page")]
        public void GivenUserClosesAndNavigatesToPage(string currentPage, string nextPage)
        {
            PageName = nextPage;
            string[] staticPages = { "Part Price Lookup", "Create Authorization", "Price Detail", "Update Credit", "User Profile", "Community Documents", "Contact Us", "Login", "QlikView Dashboard" };
            string[] singlePages = { "Manage User Notifications", "Assign Entity Chart", "Assign Entity Function", "Franchise Code Management", "User Group Setup", "Part Categories" };
            if (staticPages.Contains(nextPage))
            {
                menu.OpenNextPage(currentPage, nextPage, true);
                menu.SwitchIframe();
            }
            else if (singlePages.Contains(nextPage))
            {
                menu.OpenNextPage(currentPage, nextPage, false, true);
            }
            else
            {
                menu.OpenNextPage(currentPage, nextPage);
            }

        }



        [Given(@"User Populate Grid")]
        public void PopulateGrid()
        {
            object p = CreateInstance();
            InvokeMethod("ClickSearch", p);
            InvokeMethod("WaitForMsg", p, ButtonsAndMessages.ProcessingRequestMessage);
            InvokeMethod("WaitForGridLoad", p);

        }


        [Then(@"Dropdown ""([^""]*)"" should have valid values on ""([^""]*)"" page")]
        public void ThenDropdownShouldHaveValidValues(string dropDownName, string pageName)
        {
            object p = CreateInstance();
            string[] advanceSearchPages = { "Dealer Invoices", "Dealer Invoice Transaction Lookup", "Fleet Invoices", "Fleet Invoice Transaction Lookup" };
            // string[] popUpPages = { "Dealer Purchases Report", "Part Purchases Report", "Line Item Report" };
            if (advanceSearchPages.Contains(pageName))
            {
                InvokeMethod("SwitchToAdvanceSearch", p);
            }
            switch (dropDownName)
            {
                case "Date Range":
                    string[] options = { "Last 7 days", "Last 14 days", "Last 185 days", "Current month", "Last month", "Customized date" };
                    string[] lineItemDateRangeDropdownOptions = { "Customized date", "Last 7 days", "Last 185 days", "Current month", "Current Quarter", "YTD", "Last 12 months" };
                    if (pageName == "Line Item Report")
                    {

                        var dateRangeDropdownValues = InvokeMethod("VerifyValueDropDown", p, "Date Range", lineItemDateRangeDropdownOptions);
                        Assert.IsTrue((bool)dateRangeDropdownValues);

                    }
                    else
                    {
                        var dateRangeDropdownValues = InvokeMethod("VerifyValueDropDown", p, "Date Range", options);
                        Assert.IsTrue((bool)dateRangeDropdownValues);
                    }
                    break;
            }
        }


        private object CreateInstance()
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string file = PageName.Replace(" ", "").Replace("/", "").Replace("-", "");
            file = $"AutomationTesting_CorConnect.PageObjects.{file}.{file}Page";
            Type t = Type.GetType(file);
            object p = Activator.CreateInstance(t, bindingFlags, null, new object[] { driver }, null);
            return p;
        }

        private object? InvokeMethod(string methodName, object p, params object[] parameters)
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo m = p.GetType().GetMethod(methodName, bindingFlags);
            return m.Invoke(p, parameters);
        }
        private object? InvokeMethod(string methodName, object p, Type[] types, params object[] parameters)
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo m = p.GetType().GetMethod(methodName, bindingFlags, types);
            return m.Invoke(p, parameters);
        }
        [Given(@"Validate hyperlink ""([^""]*)"" on Grid")]
        public void ValidateHyperlinkOnGrid(string headerName)
        {
            Assert.IsTrue(DunningStatusPage.ValidateHyperlink(headerName), GetErrorMessage(ErrorMessages.UnableToOpenHyperLink));
        }

        [Given(@"User switch to ""([^""]*)"" search")]
        public void SearchTypeSwitch(string searchType)
        {
            object p = CreateInstance();
            if (searchType == "Quick")
            {
                InvokeMethod("SwitchToQuickSearch", p);
            }
            else if (searchType == "Advance")
            {
                InvokeMethod("SwitchToAdvanceSearch", p);
            }

        }

        [Given(@"User switches to ""([^""]*)"" menu item")]
        public void GivenUserSwitchesToMenuItem(string pageName)
        {
            PageName = pageName;
            string renamedPageName = menu.RenameMenuField(PageName);
            menu.SwitchToTab(renamedPageName);
            object p = CreateInstance();
            Assert.AreEqual((string)InvokeMethod("GetPageLabel", p), renamedPageName, ErrorMessages.PageCaptionMisMatch);
        }



        [Given(@"Verify data on right grid")]
        public void VerifyDataOnRightGrid()
        {
            object p = CreateInstance();
            var a = InvokeMethod("IsAnyDataOnGrid", p);
            Assert.IsTrue((bool)a);
        }

        [Given(@"User closes ""([^""]*)"" page")]
        public void GivenUserClosesPage(string pageName)
        {
            pageName = menu.RenameMenuField(pageName);
            menu.CloseCurrentTab(pageName);
        }

        [Then(@"Single menu item should open for ""([^""]*)"" page")]
        public void ThenSingleMenuItemShouldOpenForPage(string tabName)
        {
            Assert.IsTrue(menu.GetTabCount(tabName) == 1);
        }

        [Then(@"Max tabs error message is not displayed")]
        public void ThenMaxTabsErrorMessageIsNotDisplayed()
        {
            Assert.IsFalse(driver.FindElement(By.XPath("//td[contains (@id, 'tdError')]")).Text.Contains(ErrorMessages.MaxTabsViewed), ErrorMessages.MaxTabsViewed);
        }

        [StepDefinition(@"Fleet ""([^""]*)"" Credit Limit is Updated to (.*)")]
        public void GivenFleetCreditLimitIsUpdatedTo(string fleetName, int creditLimit)
        {
            CommonUtils.UpdateFleetCreditLimits(creditLimit, creditLimit, fleetName);
        }

        [Given(@"User ""([^""]*)"" is on pop-up page ""([^""]*)"" page")]
        public void PerformLoginAndNavigateToPopupPage(string userType, string pageName)
        {
            UserLogsIn(userType);
            menu.OpenPopUpPage(pageName);
        }

        [Then(@"""([^""]*)"" Buyer credit should be (.*)")]
        public void CreditCheck(string fleetName, int creditLimit)
        {
            decimal availCred = CommonUtils.GetAvailableCreditLimit(fleetName);
            if (creditLimit != availCred)
            {
                Assert.Fail("Credit Mismatch");
            }
        }
        [When(@"User selects ""([^""]*)"" from DateRange dropdown on Advanced Search")]
        public void WhenUserSelectsValueFromDateRangeDropdownOnAdvancedSearch(string dateRangeValue)
        {
            object p = CreateInstance();
            Type[] argTypes = new Type[] { typeof(string), typeof(string), typeof(string) };
            InvokeMethod("SwitchToAdvanceSearch", p);
            InvokeMethod("SelectValueTableDropDown", p, argTypes, "Date Range", dateRangeValue, null);
        }
        [Then(@"Empty grid message is displayed")]
        public void ThenEmptyGridMessageIsDisplayed()
        {
            object p = CreateInstance();
            Assert.IsTrue((bool)InvokeMethod("CheckForText", p, new object[] { ButtonsAndMessages.ClickSearchToViewData, false }), ErrorMessages.ValueMisMatch);
        }

        [When(@"User selects ""([^""]*)"" from DateRange dropdown")]
        public void WhenUserSelectsValueFromDateRangeDropdown(string dateRangeValue)
        {
            object p = CreateInstance();
            Type[] argTypes = new Type[] { typeof(string), typeof(string), typeof(string) };
            InvokeMethod("SelectValueTableDropDown", p, argTypes, "Date Range", dateRangeValue, null);
        }
        [Then(@"Grid data is displayed")]
        public void ThenGridDataIsDisplayed()
        {
            Assert.IsTrue(driver.FindElements(By.XPath("//span[contains (@id, 'NoGridData')]")).Count == 0, ErrorMessages.NoDataOnGrid);
        }

        [Then(@"The From Date and To Date are set correctly for the ""([^""]*)"" option")]
        public void ThenTheFromDateAndToDateAreSetCorrectlyForTheOption(string selectedDateRange)
        {
            string expectedFromDate = null;
            string expectedToDate = null;
            object p = CreateInstance();
            Type[] argTypes = new Type[] { typeof(string) };
            InvokeMethod("WaitForMsg", p, argTypes, ButtonsAndMessages.ProcessingRequestMessage);
            switch (selectedDateRange)
            {
                case "Last 7 days":
                    {
                        expectedFromDate = CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-6));
                        expectedToDate = CommonUtils.GetCurrentDate();
                        break;
                    }
                case "Last 14 days":
                    {
                        expectedFromDate = CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-13));
                        expectedToDate = CommonUtils.GetCurrentDate();
                        break;
                    }
                case "Last 185 days":
                    {
                        expectedFromDate = CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-184));
                        expectedToDate = CommonUtils.GetCurrentDate();
                        break;
                    }
                case "Current month":
                    {
                        expectedFromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString(CommonUtils.GetClientDateFormat());
                        expectedToDate = CommonUtils.GetCurrentDate();
                        break;
                    }
                case "Last month":
                    {
                        expectedFromDate = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1).ToString(CommonUtils.GetClientDateFormat());
                        expectedToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1).ToString(CommonUtils.GetClientDateFormat());
                        break;
                    }
                case "Customized date":
                    {
                        int customizedDaysValue = Convert.ToInt32(CommonUtils.GetCustomizedDateDays()) - 1;
                        expectedFromDate = CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-customizedDaysValue));
                        expectedToDate = CommonUtils.GetCurrentDate();
                        break;
                    }
                case "Last 12 months":
                    {
                        expectedFromDate = DateTime.Now.AddMonths(-12).AddDays(-1).ToString(CommonUtils.GetClientDateFormat());
                        expectedToDate = CommonUtils.GetCurrentDate();
                        break;
                    }
                case "YTD":
                    {
                        expectedFromDate = new DateTime(DateTime.Now.Year, 1, 1).ToString(CommonUtils.GetClientDateFormat());
                        expectedToDate = CommonUtils.GetCurrentDate();
                        break;
                    }
                case "Current Quarter":
                    {
                        DateTime currentDate = DateTime.Now;
                        int currentQuarter = (currentDate.Month - 1) / 3 + 1;
                        expectedFromDate = new DateTime(currentDate.Year, 3 * currentQuarter - 2, 1).ToString(CommonUtils.GetClientDateFormat());
                        expectedToDate = Convert.ToDateTime(expectedFromDate).AddMonths(3).AddDays(-1).ToString(CommonUtils.GetClientDateFormat());
                        break;
                    }
            }
            object actualToDate = InvokeMethod("GetValue", p, argTypes, FieldNames.To);
            object actualFromDate = InvokeMethod("GetValue", p, argTypes, FieldNames.From);
            Assert.AreEqual(expectedFromDate, actualFromDate, "From Date Mismatch");
            Assert.AreEqual(expectedToDate, actualToDate, "To Date Mismatch");
        }

        [Given(@"Invoice validity setup is (.*) days")]
        public void GivenUserUpdatedInvoiceValidityDays(int validityDays)
        {
            CommonUtils.UpdateInvoiceValidityDays(validityDays);
        }

        [When(@"Email is verified")]
        public void WhenEmailIsVerified()
        {
            EmailHelper emailHelper = new EmailHelper(driver);
            string emailContent = emailHelper.GetEmailBody();
            Console.WriteLine(emailContent);

        }

        [Then(@"User ""([^""]*)"" is logged in")]
        public void LoginSuccessfull(string userName)
        {
            menu.WaitForLoginUserLabelToHaveText(userName.ToUpper());


        }

        [Given(@"Token ""([^""]*)"" is ""([^""]*)""")]
        public void GivenTokenIs(string tokenName, string action)
        {
            if (action.ToLower() == "active")
            {
                CommonUtils.ToggleLookupTbToken(tokenName, true);
            }
            else if (action.ToLower() == "deactive")
            {
                CommonUtils.ToggleLookupTbToken(tokenName, false);
            }

        }


    }
}