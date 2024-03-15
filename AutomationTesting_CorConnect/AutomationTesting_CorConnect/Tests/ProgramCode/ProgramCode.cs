using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.AccountStatusChangeReport;
using AutomationTesting_CorConnect.PageObjects.AgedInvoiceReport;
using AutomationTesting_CorConnect.PageObjects.CashFlowReport;
using AutomationTesting_CorConnect.PageObjects.CreateAuthorization;
using AutomationTesting_CorConnect.PageObjects.DealerDiscountDateReport;
using AutomationTesting_CorConnect.PageObjects.DealerDueDateReport;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceDetail;
using AutomationTesting_CorConnect.PageObjects.DealerInvoices;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.DealerLocations;
using AutomationTesting_CorConnect.PageObjects.DealerPartSummaryFleetBillTo;
using AutomationTesting_CorConnect.PageObjects.DealerPartSummaryFleetLocation;
using AutomationTesting_CorConnect.PageObjects.DealerPOPOQTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.DealerPreAuthorization;
using AutomationTesting_CorConnect.PageObjects.DealerPurchasesReport;
using AutomationTesting_CorConnect.PageObjects.DealerReleaseInvoices;
using AutomationTesting_CorConnect.PageObjects.DealerSalesSummaryBillTo;
using AutomationTesting_CorConnect.PageObjects.DealerSalesSummaryLocation;
using AutomationTesting_CorConnect.PageObjects.DealerStatements;
using AutomationTesting_CorConnect.PageObjects.Disputes;
using AutomationTesting_CorConnect.PageObjects.FleetBillToSalesSummary;
using AutomationTesting_CorConnect.PageObjects.FleetDiscountDateReport;
using AutomationTesting_CorConnect.PageObjects.FleetDueDateReport;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceDetailReport;
using AutomationTesting_CorConnect.PageObjects.FleetInvoicePreApprovalReport;
using AutomationTesting_CorConnect.PageObjects.FleetInvoices;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.FleetLocations;
using AutomationTesting_CorConnect.PageObjects.FleetMasterInvoicesStatements;
using AutomationTesting_CorConnect.PageObjects.FleetPartCategorySalesSummaryBillTo;
using AutomationTesting_CorConnect.PageObjects.FleetPartCategorySalesSummaryLocation;
using AutomationTesting_CorConnect.PageObjects.FleetPartSummaryBillTo;
using AutomationTesting_CorConnect.PageObjects.FleetPartSummaryLocation;
using AutomationTesting_CorConnect.PageObjects.FleetPOPOQTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.FleetPreAuthorization;
using AutomationTesting_CorConnect.PageObjects.FleetSalesSummaryBillTo;
using AutomationTesting_CorConnect.PageObjects.FleetSalesSummaryLocation;
using AutomationTesting_CorConnect.PageObjects.FleetStatements;
using AutomationTesting_CorConnect.PageObjects.GrossMarginCreditReport;
using AutomationTesting_CorConnect.PageObjects.InvoiceDetailReport;
using AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancy;
using AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancyHistory;
using AutomationTesting_CorConnect.PageObjects.InvoiceDownload;
using AutomationTesting_CorConnect.PageObjects.InvoiceDownloadSetup;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.LineItemReport;
using AutomationTesting_CorConnect.PageObjects.LoyaltyStatus;
using AutomationTesting_CorConnect.PageObjects.ManageUsers;
using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations;
using AutomationTesting_CorConnect.PageObjects.OpenBalanceReport;
using AutomationTesting_CorConnect.PageObjects.PCardTransactions;
using AutomationTesting_CorConnect.PageObjects.PartPriceLookup;
using AutomationTesting_CorConnect.PageObjects.PartPurchasesReport;
using AutomationTesting_CorConnect.PageObjects.PartsLookup;
using AutomationTesting_CorConnect.PageObjects.PODiscrepancy;
using AutomationTesting_CorConnect.PageObjects.PODiscrepancyHistory;
using AutomationTesting_CorConnect.PageObjects.POEntry;
using AutomationTesting_CorConnect.PageObjects.POOrders;
using AutomationTesting_CorConnect.PageObjects.POTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.PriceExceptionReport;
using AutomationTesting_CorConnect.PageObjects.PriceLookup;
using AutomationTesting_CorConnect.PageObjects.PurchasingFleetSummary;
using AutomationTesting_CorConnect.PageObjects.RemittanceReport;
using AutomationTesting_CorConnect.PageObjects.SettlementFile;
using AutomationTesting_CorConnect.PageObjects.SettlementFileSummary;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.PageObjects.TaxReviewReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.ProgramCode
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Program Code")]
    internal class ProgramCode : DriverBuilderClass
    {
        ManageUsersPage manageUsersPage;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManageUsers);
            manageUsersPage = new ManageUsersPage(driver);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25053" })]
        public void TC_25053(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
            Commons Page = new DealerInvoiceTransactionLookupPage(driver);

            CommonUtils.ToggleValidateProgramCode(true);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(false);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.DealerInvoiceTransactionLookup;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                List<string> dealerLocations = CommonUtils.GetProgramCodeEntityLocations(userName)?.Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList());
                List<string> fleetLocations = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Dealer, out bool isProgramCodeAssigned).Select(x => x.DisplayName).ToList();
                List<EntityDetails> fleetDetailsNotListed = isProgramCodeAssigned ? CommonUtils.GetProgCdOppEntyLocForLoggedInEntyTypeFlsNeg(userName, EntityType.Dealer) : null;
                List<string> fleetLocationsNotListed = fleetDetailsNotListed?.Select(x => x.DisplayName).ToList();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(Page.VerifyDataNotInMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocationsNotListed.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoices;
                menu.OpenPage(pageName);
                Page = new DealerInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(Page.VerifyDataNotInMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocationsNotListed.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.Disputes;
                menu.OpenPage(pageName);
                Page = new DisputesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(Page.VerifyDataNotInMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocationsNotListed.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPOPOQTransactionLookup;
                menu.OpenPage(pageName);
                Page = new DealerPOPOQTransactionLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(Page.VerifyDataNotInMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocationsNotListed.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PurchasingFleetSummary;
                menu.OpenPage(pageName);
                Page = new PurchasingFleetSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(Page.VerifyDataNotInMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocationsNotListed.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPreAuthorization;
                menu.OpenPage(pageName);
                Page = new DealerPreAuthorizationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(Page.VerifyDataNotInMultipleColumnsDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocationsNotListed.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartPriceLookup;
                menu.OpenPage(pageName, false, true);
                menu.SwitchIframe();
                StaticPage staticPage = new PartPriceLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), staticPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.Name, dealerLocations.ToList()));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.Name, fleetLocations.ToList()));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(staticPage.VerifyDataNotInMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.Name, fleetLocationsNotListed.ToList()));
                Page.SwitchToMainWindow();
                Page.ClosePage(pageName);

                pageName = Pages.GrossMarginCreditReport;
                menu.OpenPage(pageName);
                Page = new GrossMarginCreditReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(Page.VerifyDataNotInMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocationsNotListed.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.CreateAuthorization;
                menu.OpenPage(pageName, false, true);
                menu.SwitchIframe();
                staticPage = new CreateAuthorizationPage(driver);
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.Name, dealerLocations.ToList(), true));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.Name, fleetLocations.ToList(), true));
                if (isProgramCodeAssigned)
                    errorMsgs.AddRange(staticPage.VerifyDataNotInMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.Name, fleetLocationsNotListed.ToList(), true));
                Page.SwitchToMainWindow();

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25054" })]
        public void TC_25054(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
            Commons Page = new DealerInvoiceTransactionLookupPage(driver);

            CommonUtils.ToggleValidateProgramCode(true);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.DealerInvoiceTransactionLookup;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                List<string> dealerLocations = CommonUtils.GetProgramCodeEntityLocations(userName)?.Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList());
                List<string> fleetLocations = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Dealer, out bool isProgramCodeAssigned).Select(x => x.DisplayName).ToList();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoices;
                menu.OpenPage(pageName);
                Page = new DealerInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.Disputes;
                menu.OpenPage(pageName);
                Page = new DisputesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPOPOQTransactionLookup;
                menu.OpenPage(pageName);
                Page = new DealerPOPOQTransactionLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PurchasingFleetSummary;
                menu.OpenPage(pageName);
                Page = new PurchasingFleetSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPreAuthorization;
                menu.OpenPage(pageName);
                Page = new DealerPreAuthorizationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartPriceLookup;
                menu.OpenPage(pageName, false, true);
                menu.SwitchIframe();
                StaticPage staticPage = new PartPriceLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), staticPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.Name, dealerLocations.ToList()));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.Name, fleetLocations.ToList()));
                Page.SwitchToMainWindow();
                Page.ClosePage(pageName);

                pageName = Pages.GrossMarginCreditReport;
                menu.OpenPage(pageName);
                Page = new GrossMarginCreditReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.CreateAuthorization;
                menu.OpenPage(pageName, false, true);
                menu.SwitchIframe();
                staticPage = new CreateAuthorizationPage(driver);
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.Name, dealerLocations.ToList(), true));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.Name, fleetLocations.ToList(), true));
                Page.SwitchToMainWindow();

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25055" })]
        public void TC_25055(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
            Commons Page = new DealerInvoiceTransactionLookupPage(driver);

            CommonUtils.ToggleValidateProgramCode(false);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.DealerInvoiceTransactionLookup;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                List<string> dealerLocations = CommonUtils.GetProgramCodeEntityLocations(userName)?.Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList());
                List<string> fleetLocations = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Dealer, out bool isProgramCodeAssigned).Select(x => x.DisplayName).ToList();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoices;
                menu.OpenPage(pageName);
                Page = new DealerInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.Disputes;
                menu.OpenPage(pageName);
                Page = new DisputesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPOPOQTransactionLookup;
                menu.OpenPage(pageName);
                Page = new DealerPOPOQTransactionLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PurchasingFleetSummary;
                menu.OpenPage(pageName);
                Page = new PurchasingFleetSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPreAuthorization;
                menu.OpenPage(pageName);
                Page = new DealerPreAuthorizationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartPriceLookup;
                menu.OpenPage(pageName, false, true);
                menu.SwitchIframe();
                StaticPage staticPage = new PartPriceLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), staticPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.Name, dealerLocations.ToList()));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.Name, fleetLocations.ToList()));
                Page.SwitchToMainWindow();
                Page.ClosePage(pageName);

                pageName = Pages.GrossMarginCreditReport;
                menu.OpenPage(pageName);
                Page = new GrossMarginCreditReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.CreateAuthorization;
                menu.OpenPage(pageName, false, true);
                menu.SwitchIframe();
                staticPage = new CreateAuthorizationPage(driver);
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.Name, dealerLocations.ToList(), true));
                errorMsgs.AddRange(staticPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.Name, fleetLocations.ToList(), true));
                Page.SwitchToMainWindow();

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25097" })]
        public void TC_25097(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            Commons Page = new FleetInvoiceTransactionLookupPage(driver);

            CommonUtils.ToggleValidateProgramCode(true);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(false);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.FleetInvoiceTransactionLookup;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                List<EntityDetails> dealerEntityLocations = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Fleet, out bool isProgramCodeAssigned);
                List<string> dealerLocations = dealerEntityLocations.Select(x => x.DisplayName).ToList();
                List<string> dealerBillingLocations = dealerEntityLocations.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList());
                List<string> fleetLocations = CommonUtils.GetProgramCodeEntityLocations(userName)?.Select(x => x.DisplayName).ToList();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetInvoices;
                menu.OpenPage(pageName);
                Page = new FleetInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.Disputes;
                menu.OpenPage(pageName);
                Page = new DisputesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPOPOQTransactionLookup;
                menu.OpenPage(pageName);
                Page = new FleetPOPOQTransactionLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.POEntry;
                menu.OpenPage(pageName);
                Page = new POEntryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPurchasesReport;
                menu.OpenPopUpPage(pageName);
                PopUpPage popUpPage = new DealerPurchasesReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), popUpPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                popUpPage.ClosePopupWindow();

                pageName = Pages.FleetInvoicePreApprovalReport;
                menu.OpenPage(pageName);
                Page = new FleetInvoicePreApprovalReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.LineItemReport;
                menu.OpenPage(pageName);
                Page = new LineItemReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPreAuthorization;
                menu.OpenPage(pageName);
                Page = new FleetPreAuthorizationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartPurchasesReport;
                menu.OpenPopUpPage(pageName);
                popUpPage = new PartPurchasesReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), popUpPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                popUpPage.ClosePopupWindow();

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25098" })]
        public void TC_25098(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            Commons Page = new FleetInvoiceTransactionLookupPage(driver);

            CommonUtils.ToggleValidateProgramCode(true);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.FleetInvoiceTransactionLookup;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                List<EntityDetails> dealerEntityLocations = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Fleet, out bool isProgramCodeAssigned);
                List<string> dealerLocations = dealerEntityLocations.Select(x => x.DisplayName).ToList();
                List<string> dealerBillingLocations = dealerEntityLocations.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList());
                List<string> fleetLocations = CommonUtils.GetProgramCodeEntityLocations(userName)?.Select(x => x.DisplayName).ToList();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetInvoices;
                menu.OpenPage(pageName);
                Page = new FleetInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.Disputes;
                menu.OpenPage(pageName);
                Page = new DisputesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPOPOQTransactionLookup;
                menu.OpenPage(pageName);
                Page = new FleetPOPOQTransactionLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.POEntry;
                menu.OpenPage(pageName);
                Page = new POEntryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPurchasesReport;
                menu.OpenPopUpPage(pageName);
                PopUpPage popUpPage = new DealerPurchasesReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), popUpPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                popUpPage.ClosePopupWindow();

                pageName = Pages.FleetInvoicePreApprovalReport;
                menu.OpenPage(pageName);
                Page = new FleetInvoicePreApprovalReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.LineItemReport;
                menu.OpenPage(pageName);
                Page = new LineItemReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPreAuthorization;
                menu.OpenPage(pageName);
                Page = new FleetPreAuthorizationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartPurchasesReport;
                menu.OpenPopUpPage(pageName);
                popUpPage = new PartPurchasesReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), popUpPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                popUpPage.ClosePopupWindow();

                pageName = Pages.PODiscrepancy;
                menu.OpenPage(pageName);
                Page = new PODiscrepancyPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PODiscrepancyHistory;
                menu.OpenPage(pageName);
                Page = new PODiscrepancyHistoryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.POOrders;
                menu.OpenPage(pageName);
                Page = new POOrdersPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.POTransactionLookup;
                menu.OpenPage(pageName);
                Page = new POTransactionLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25099" })]
        public void TC_25099(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            Commons Page = new FleetInvoiceTransactionLookupPage(driver);

            CommonUtils.ToggleValidateProgramCode(false);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.FleetInvoiceTransactionLookup;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                List<EntityDetails> dealerEntityLocations = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Fleet, out bool isProgramCodeAssigned);
                List<string> dealerLocations = dealerEntityLocations.Select(x => x.DisplayName).ToList();
                List<string> dealerBillingLocations = dealerEntityLocations.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList());
                List<string> fleetLocations = CommonUtils.GetProgramCodeEntityLocations(userName)?.Select(x => x.DisplayName).ToList();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetInvoices;
                menu.OpenPage(pageName);
                Page = new FleetInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                Page.SwitchToAdvanceSearch();
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.Disputes;
                menu.OpenPage(pageName);
                Page = new DisputesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPOPOQTransactionLookup;
                menu.OpenPage(pageName);
                Page = new FleetPOPOQTransactionLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.POEntry;
                menu.OpenPage(pageName);
                Page = new POEntryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPurchasesReport;
                menu.OpenPopUpPage(pageName);
                PopUpPage popUpPage = new DealerPurchasesReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), popUpPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                popUpPage.ClosePopupWindow();

                pageName = Pages.FleetInvoicePreApprovalReport;
                menu.OpenPage(pageName);
                Page = new FleetInvoicePreApprovalReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.LineItemReport;
                menu.OpenPage(pageName);
                Page = new LineItemReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCriteriaCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCriteriaCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPreAuthorization;
                menu.OpenPage(pageName);
                Page = new FleetPreAuthorizationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Fleet, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartPurchasesReport;
                menu.OpenPopUpPage(pageName);
                popUpPage = new PartPurchasesReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), popUpPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                popUpPage.ClosePopupWindow();

                pageName = Pages.PODiscrepancy;
                menu.OpenPage(pageName);
                Page = new PODiscrepancyPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PODiscrepancyHistory;
                menu.OpenPage(pageName);
                Page = new PODiscrepancyHistoryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.POOrders;
                menu.OpenPage(pageName);
                Page = new POOrdersPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.POTransactionLookup;
                menu.OpenPage(pageName);
                Page = new POTransactionLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.DealerCompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.FleetCompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25056" })]
        public void TC_25056(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.InvoiceDiscrepancy);
            Commons Page = new InvoiceDiscrepancyPage(driver);

            CommonUtils.ToggleValidateProgramCode(true);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(false);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.InvoiceDiscrepancy;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                List<EntityDetails> dealerEntityDetails = CommonUtils.GetProgramCodeEntityLocations(userName);
                List<string> dealerLocations = dealerEntityDetails?.Select(x => x.DisplayName).ToList();
                List<string> dealerBillingLocations = dealerEntityDetails?.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList());
                Page.ClosePage(pageName);

                pageName = Pages.SettlementFile;
                menu.OpenPage(pageName);
                Page = new SettlementFilePage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.SettlementFileSummary;
                menu.OpenPage(pageName);
                Page = new SettlementFileSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerStatements;
                menu.OpenPage(pageName);
                Page = new DealerStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.P_CardTransactions;
                menu.OpenPage(pageName);
                Page = new PCardTransactionsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);


                pageName = Pages.InvoiceDiscrepancyHistory;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyHistoryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenAuthorizations;
                menu.OpenPage(pageName);
                Page = new OpenAuthorizationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField("Open Authorization"), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceEntry;
                menu.OpenPage(pageName);
                Page = new InvoiceEntryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerReleaseInvoices;
                menu.OpenPage(pageName);
                Page = new DealerReleaseInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AccountStatusChangeReport;
                menu.OpenPage(pageName);
                Page = new AccountStatusChangeReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoiceDetail;
                menu.OpenPage(pageName);
                Page = new DealerInvoiceDetailPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerLocations;
                menu.OpenPage(pageName);
                Page = new DealerLocationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPartSummaryFleetBillTo;
                menu.OpenPage(pageName);
                Page = new DealerPartSummaryFleetBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPartSummaryFleetLocation;
                menu.OpenPage(pageName);
                Page = new DealerPartSummaryFleetLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.CashFlowReport;
                menu.OpenPage(pageName);
                Page = new CashFlowReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PriceExceptionReport;
                menu.OpenPage(pageName);
                Page = new PriceExceptionReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.RemittanceReport;
                menu.OpenPage(pageName);
                Page = new RemittanceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);


                pageName = Pages.TaxReviewReport;
                menu.OpenPage(pageName);
                Page = new TaxReviewReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerSalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new DealerSalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerSalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new DealerSalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDetailReport;
                menu.OpenPage(pageName);
                Page = new InvoiceDetailReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AgedInvoiceReport;
                menu.OpenPage(pageName);
                Page = new AgedInvoiceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                Page.ClosePage(pageName);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25057" })]
        public void TC_25057(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.InvoiceDiscrepancy);
            Commons Page = new InvoiceDiscrepancyPage(driver);

            CommonUtils.ToggleValidateProgramCode(true);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.InvoiceDiscrepancy;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                List<EntityDetails> dealerEntityDetails = CommonUtils.GetProgramCodeEntityLocations(userName);
                List<string> dealerLocations = dealerEntityDetails?.Select(x => x.DisplayName).ToList();
                List<string> dealerBillingLocations = dealerEntityDetails?.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<EntityDetails> fleetEntityDetails = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Dealer, out bool isProgramCodeAssigned);
                List<string> fleetLocations = fleetEntityDetails.Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList());
                Page.ClosePage(pageName);

                pageName = Pages.SettlementFile;
                menu.OpenPage(pageName);
                Page = new SettlementFilePage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.SettlementFileSummary;
                menu.OpenPage(pageName);
                Page = new SettlementFileSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerStatements;
                menu.OpenPage(pageName);
                Page = new DealerStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.P_CardTransactions;
                menu.OpenPage(pageName);
                Page = new PCardTransactionsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);


                pageName = Pages.InvoiceDiscrepancyHistory;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyHistoryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenAuthorizations;
                menu.OpenPage(pageName);
                Page = new OpenAuthorizationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField("Open Authorization"), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceEntry;
                menu.OpenPage(pageName);
                Page = new InvoiceEntryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerReleaseInvoices;
                menu.OpenPage(pageName);
                Page = new DealerReleaseInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AccountStatusChangeReport;
                menu.OpenPage(pageName);
                Page = new AccountStatusChangeReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoiceDetail;
                menu.OpenPage(pageName);
                Page = new DealerInvoiceDetailPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerLocations;
                menu.OpenPage(pageName);
                Page = new DealerLocationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPartSummaryFleetBillTo;
                menu.OpenPage(pageName);
                Page = new DealerPartSummaryFleetBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPartSummaryFleetLocation;
                menu.OpenPage(pageName);
                Page = new DealerPartSummaryFleetLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.CashFlowReport;
                menu.OpenPage(pageName);
                Page = new CashFlowReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PriceExceptionReport;
                menu.OpenPage(pageName);
                Page = new PriceExceptionReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.RemittanceReport;
                menu.OpenPage(pageName);
                Page = new RemittanceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.TaxReviewReport;
                menu.OpenPage(pageName);
                Page = new TaxReviewReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerSalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new DealerSalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerSalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new DealerSalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDetailReport;
                menu.OpenPage(pageName);
                Page = new InvoiceDetailReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AgedInvoiceReport;
                menu.OpenPage(pageName);
                Page = new AgedInvoiceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartsLookup;
                menu.OpenPage(pageName);
                Page = new PartsLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PriceLookup;
                menu.OpenPage(pageName);
                Page = new PriceLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDueDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDueDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25058" })]
        public void TC_25058(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.InvoiceDiscrepancy);
            Commons Page = new InvoiceDiscrepancyPage(driver);

            CommonUtils.ToggleValidateProgramCode(false);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.InvoiceDiscrepancy;
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                List<EntityDetails> dealerEntityDetails = CommonUtils.GetProgramCodeEntityLocations(userName);
                List<string> dealerLocations = dealerEntityDetails?.Select(x => x.DisplayName).ToList();
                List<string> dealerBillingLocations = dealerEntityDetails?.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<EntityDetails> fleetEntityDetails = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Dealer, out bool isProgramCodeAssigned);
                List<string> fleetLocations = fleetEntityDetails.Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList());
                Page.ClosePage(pageName);

                pageName = Pages.SettlementFile;
                menu.OpenPage(pageName);
                Page = new SettlementFilePage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.SettlementFileSummary;
                menu.OpenPage(pageName);
                Page = new SettlementFileSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerStatements;
                menu.OpenPage(pageName);
                Page = new DealerStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.P_CardTransactions;
                menu.OpenPage(pageName);
                Page = new PCardTransactionsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);


                pageName = Pages.InvoiceDiscrepancyHistory;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyHistoryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenAuthorizations;
                menu.OpenPage(pageName);
                Page = new OpenAuthorizationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField("Open Authorization"), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceEntry;
                menu.OpenPage(pageName);
                Page = new InvoiceEntryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerReleaseInvoices;
                menu.OpenPage(pageName);
                Page = new DealerReleaseInvoicesPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AccountStatusChangeReport;
                menu.OpenPage(pageName);
                Page = new AccountStatusChangeReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoiceDetail;
                menu.OpenPage(pageName);
                Page = new DealerInvoiceDetailPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerLocations;
                menu.OpenPage(pageName);
                Page = new DealerLocationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerCode, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPartSummaryFleetBillTo;
                menu.OpenPage(pageName);
                Page = new DealerPartSummaryFleetBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerPartSummaryFleetLocation;
                menu.OpenPage(pageName);
                Page = new DealerPartSummaryFleetLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.CashFlowReport;
                menu.OpenPage(pageName);
                Page = new CashFlowReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.Dealer, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PriceExceptionReport;
                menu.OpenPage(pageName);
                Page = new PriceExceptionReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.RemittanceReport;
                menu.OpenPage(pageName);
                Page = new RemittanceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.TaxReviewReport;
                menu.OpenPage(pageName);
                Page = new TaxReviewReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerSalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new DealerSalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerSalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new DealerSalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDetailReport;
                menu.OpenPage(pageName);
                Page = new InvoiceDetailReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AgedInvoiceReport;
                menu.OpenPage(pageName);
                Page = new AgedInvoiceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, dealerBillingLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartsLookup;
                menu.OpenPage(pageName);
                Page = new PartsLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PriceLookup;
                menu.OpenPage(pageName);
                Page = new PriceLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDueDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDueDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25100" })]
        public void TC_25100(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.FleetStatements);

            CommonUtils.ToggleValidateProgramCode(true);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(false);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.FleetStatements;
                Commons Page = new FleetStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                List<EntityDetails> fleetEntityDetails = CommonUtils.GetProgramCodeEntityLocations(userName);
                List<string> fleetLocations = fleetEntityDetails?.Select(x => x.DisplayName).ToList();
                List<string> fleetBillingLocations = fleetEntityDetails?.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<EntityDetails> dealerEntityDetails = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Fleet, out bool isProgramCodeAssigned);
                List<string> dealerLocations = dealerEntityDetails.Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList());
                Page.ClosePage(pageName);

                pageName = Pages.P_CardTransactions;
                menu.OpenPage(pageName);
                Page = new PCardTransactionsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDiscrepancy;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDiscrepancyHistory;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyHistoryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenAuthorizations;
                menu.OpenPage(pageName);
                Page = new OpenAuthorizationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField("Open Authorization"), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AccountStatusChangeReport;
                menu.OpenPage(pageName);
                Page = new AccountStatusChangeReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoiceDetail;
                menu.OpenPage(pageName);
                Page = new DealerInvoiceDetailPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetBillToSalesSummary;
                menu.OpenPage(pageName);
                Page = new FleetBillToSalesSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetInvoiceDetailReport;
                menu.OpenPage(pageName);
                Page = new FleetInvoiceDetailReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDueDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetLocations;
                menu.OpenPage(pageName);
                Page = new FleetLocationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartCategorySalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetPartCategorySalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartCategorySalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetPartCategorySalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetPartSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetPartSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AgedInvoiceReport;
                menu.OpenPage(pageName);
                Page = new AgedInvoiceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, fleetBillingLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenBalanceReport;
                menu.OpenPage(pageName);
                Page = new OpenBalanceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, fleetBillingLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDownload;
                menu.OpenPopUpPage(pageName);
                PopUpPage popUpPage = new InvoiceDownloadPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.AccountCode, fleetLocations.ToList(), true, FieldNames.FieldCompanyName, true, FieldNames.FieldCompanyNameClearButton));
                popUpPage.ClosePopupWindow();

                pageName = Pages.InvoiceDownloadSetup;
                menu.OpenPopUpPage(pageName);
                popUpPage = new InvoiceDownloadSetupPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.AccountCode, fleetLocations.ToList(), true, FieldNames.FieldCompanyName));
                popUpPage.ClosePopupWindow();

                pageName = Pages.LoyaltyStatus;
                menu.OpenPopUpPage(pageName);
                popUpPage = new LoyaltyStatusPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList(), true));
                popUpPage.ClosePopupWindow();

                pageName = Pages.FleetMasterInvoicesStatements;
                menu.OpenPage(pageName);
                Page = new FleetMasterInvoicesStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartsLookup;
                menu.OpenPage(pageName);
                Page = new PartsLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PriceLookup;
                menu.OpenPage(pageName);
                Page = new PriceLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDueDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetSalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetSalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetSalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetSalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25101" })]
        public void TC_25101(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.FleetStatements);

            CommonUtils.ToggleValidateProgramCode(true);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.FleetStatements;
                Commons Page = new FleetStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                List<EntityDetails> fleetEntityDetails = CommonUtils.GetProgramCodeEntityLocations(userName);
                List<string> fleetLocations = fleetEntityDetails?.Select(x => x.DisplayName).ToList();
                List<string> fleetBillingLocations = fleetEntityDetails?.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<EntityDetails> dealerEntityDetails = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Fleet, out bool isProgramCodeAssigned);
                List<string> dealerLocations = dealerEntityDetails.Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList());
                Page.ClosePage(pageName);

                pageName = Pages.P_CardTransactions;
                menu.OpenPage(pageName);
                Page = new PCardTransactionsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDiscrepancy;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDiscrepancyHistory;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyHistoryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenAuthorizations;
                menu.OpenPage(pageName);
                Page = new OpenAuthorizationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField("Open Authorization"), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AccountStatusChangeReport;
                menu.OpenPage(pageName);
                Page = new AccountStatusChangeReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoiceDetail;
                menu.OpenPage(pageName);
                Page = new DealerInvoiceDetailPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetBillToSalesSummary;
                menu.OpenPage(pageName);
                Page = new FleetBillToSalesSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetInvoiceDetailReport;
                menu.OpenPage(pageName);
                Page = new FleetInvoiceDetailReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDueDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetLocations;
                menu.OpenPage(pageName);
                Page = new FleetLocationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartCategorySalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetPartCategorySalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartCategorySalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetPartCategorySalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetPartSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetPartSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AgedInvoiceReport;
                menu.OpenPage(pageName);
                Page = new AgedInvoiceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, fleetBillingLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenBalanceReport;
                menu.OpenPage(pageName);
                Page = new OpenBalanceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, fleetBillingLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDownload;
                menu.OpenPopUpPage(pageName);
                PopUpPage popUpPage = new InvoiceDownloadPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.AccountCode, fleetLocations.ToList(), true, FieldNames.FieldCompanyName, true, FieldNames.FieldCompanyNameClearButton));
                popUpPage.ClosePopupWindow();

                pageName = Pages.InvoiceDownloadSetup;
                menu.OpenPopUpPage(pageName);
                popUpPage = new InvoiceDownloadSetupPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.AccountCode, fleetLocations.ToList(), true, FieldNames.FieldCompanyName));
                popUpPage.ClosePopupWindow();

                pageName = Pages.LoyaltyStatus;
                menu.OpenPopUpPage(pageName);
                popUpPage = new LoyaltyStatusPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList(), true));
                popUpPage.ClosePopupWindow();

                pageName = Pages.FleetMasterInvoicesStatements;
                menu.OpenPage(pageName);
                Page = new FleetMasterInvoicesStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartsLookup;
                menu.OpenPage(pageName);
                Page = new PartsLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PriceLookup;
                menu.OpenPage(pageName);
                Page = new PriceLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDueDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetSalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetSalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetSalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetSalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25102" })]
        public void TC_25102(string userName)
        {
            manageUsersPage.ImpersonateUser(userName);
            menu.OpenPage(Pages.FleetStatements);

            CommonUtils.ToggleValidateProgramCode(false);
            CommonUtils.ToggleEnablePrgmCodeAsgnOnSubCommunity(true);
            CommonUtils.FlushRedisDB(applicationContext.ServerName, applicationContext.RedisPort);

            Assert.Multiple(() =>
            {
                string pageName = Pages.FleetStatements;
                Commons Page = new FleetStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                List<EntityDetails> fleetEntityDetails = CommonUtils.GetProgramCodeEntityLocations(userName);
                List<string> fleetLocations = fleetEntityDetails?.Select(x => x.DisplayName).ToList();
                List<string> fleetBillingLocations = fleetEntityDetails?.Where(x => x.LocationTypeId == LocationType.Billing.Value).Select(x => x.DisplayName).ToList();
                List<EntityDetails> dealerEntityDetails = CommonUtils.GetProgCdOppEntyLocForLoggedInEntyType(userName, EntityType.Fleet, out bool isProgramCodeAssigned);
                List<string> dealerLocations = dealerEntityDetails.Select(x => x.DisplayName).ToList();
                List<string> errorMsgs = Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList());
                Page.ClosePage(pageName);

                pageName = Pages.P_CardTransactions;
                menu.OpenPage(pageName);
                Page = new PCardTransactionsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDiscrepancy;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDiscrepancyHistory;
                menu.OpenPage(pageName);
                Page = new InvoiceDiscrepancyHistoryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenAuthorizations;
                menu.OpenPage(pageName);
                Page = new OpenAuthorizationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField("Open Authorization"), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AccountStatusChangeReport;
                menu.OpenPage(pageName);
                Page = new AccountStatusChangeReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerInvoiceDetail;
                menu.OpenPage(pageName);
                Page = new DealerInvoiceDetailPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetBillToSalesSummary;
                menu.OpenPage(pageName);
                Page = new FleetBillToSalesSummaryPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetInvoiceDetailReport;
                menu.OpenPage(pageName);
                Page = new FleetInvoiceDetailReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetDueDateReport;
                menu.OpenPage(pageName);
                Page = new FleetDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.DealerName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetLocations;
                menu.OpenPage(pageName);
                Page = new FleetLocationsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetCode, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartCategorySalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetPartCategorySalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartCategorySalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetPartCategorySalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetPartSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetPartSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetPartSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.AgedInvoiceReport;
                menu.OpenPage(pageName);
                Page = new AgedInvoiceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, fleetBillingLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.OpenBalanceReport;
                menu.OpenPage(pageName);
                Page = new OpenBalanceReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.Location, FieldNames.DisplayName, fleetBillingLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.InvoiceDownload;
                menu.OpenPopUpPage(pageName);
                PopUpPage popUpPage = new InvoiceDownloadPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.AccountCode, fleetLocations.ToList(), true, FieldNames.FieldCompanyName, true, FieldNames.FieldCompanyNameClearButton));
                popUpPage.ClosePopupWindow();

                pageName = Pages.InvoiceDownloadSetup;
                menu.OpenPopUpPage(pageName);
                popUpPage = new InvoiceDownloadSetupPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.AccountCode, fleetLocations.ToList(), true, FieldNames.FieldCompanyName));
                popUpPage.ClosePopupWindow();

                pageName = Pages.LoyaltyStatus;
                menu.OpenPopUpPage(pageName);
                popUpPage = new LoyaltyStatusPage(driver);
                errorMsgs.AddRange(popUpPage.VerifyDataMultiSelectDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList(), true));
                popUpPage.ClosePopupWindow();

                pageName = Pages.FleetMasterInvoicesStatements;
                menu.OpenPage(pageName);
                Page = new FleetMasterInvoicesStatementsPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultiSelectDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PartsLookup;
                menu.OpenPage(pageName);
                Page = new PartsLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.PriceLookup;
                menu.OpenPage(pageName);
                Page = new PriceLookupPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, dealerLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDiscountDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDiscountDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.DealerDueDateReport;
                menu.OpenPage(pageName);
                Page = new DealerDueDateReportPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.FleetName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetSalesSummaryBillTo;
                menu.OpenPage(pageName);
                Page = new FleetSalesSummaryBillToPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                pageName = Pages.FleetSalesSummaryLocation;
                menu.OpenPage(pageName);
                Page = new FleetSalesSummaryLocationPage(driver);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(pageName), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMismatchForPage, pageName));
                errorMsgs.AddRange(Page.VerifyDataMultipleColumnsDropdown(pageName, FieldNames.CompanyName, FieldNames.DisplayName, fleetLocations.ToList()));
                Page.ClosePage(pageName);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }
    }
}
