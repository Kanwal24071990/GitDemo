using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.AccountMaintenance;
using AutomationTesting_CorConnect.PageObjects.ASN;
using AutomationTesting_CorConnect.PageObjects.CreateAuthorization;
using AutomationTesting_CorConnect.PageObjects.DealerInvoices;
using AutomationTesting_CorConnect.PageObjects.DealerReleaseInvoices;
using AutomationTesting_CorConnect.PageObjects.FleetCreditLimit;
using AutomationTesting_CorConnect.PageObjects.FleetInvoices;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.FleetReleaseInvoices;
using AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancy;
using AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancyHistory;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations;
using AutomationTesting_CorConnect.PageObjects.OpenAuthorizations.CreateNewAuthorization;
using AutomationTesting_CorConnect.PageObjects.UpdateCredit;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Tests.AccountMaintenance;
using AutomationTesting_CorConnect.Tests.FleetInvoices;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.DealerReleaseInvoices;
using AutomationTesting_CorConnect.Utils.FleetInvoices;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V109.Debugger;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.InvoiceEntry
{
    [Binding]
    internal class IS_FleetCreditCheckStepDefinitions : DriverBuilderClass
    {
        string dealerInvNum = CommonUtils.RandomString(6);
        string discrepantDealerInv;
        string currentInvNum;
        int transactionDays;
        InvoiceEntryPage InvoiceEntryPage;
        CreateNewInvoicePage CreateNewInvoicePage;

        [Then(@"credit limit should be (.*) on ""([^""]*)""")]
        public void ThenCreditLimitShouldBeForFleetOn(int creditLimit, string pageName)
        {
            FleetCreditLimitPage FleetCreditLimitPage;
            UpdateCreditPage UpdateCreditPage;
            AccountMaintenanceAspx AspxPage;

            switch (pageName)
            {
                case "Account Configuration":
                    AspxPage = new AccountMaintenanceAspx(driver);
                    AspxPage.ClickListElements("TabList", "Account Configuration");
                    AspxPage.WaitForIframe();
                    AspxPage.SwitchIframe();
                    string creditLimitUI = AspxPage.GetValue(FieldNames.CreditLimit).Replace("_", "");
                    Assert.AreEqual(creditLimit, Convert.ToInt32(creditLimitUI), "Fleet Credit Mismatch");
                    break;
                case "Fleet Credit Limit":
                    FleetCreditLimitPage = new FleetCreditLimitPage(driver);
                    if (Convert.ToString(creditLimit).Contains('-'))
                    {
                        string creditLimits = Convert.ToString(creditLimit);
                        creditLimits = creditLimits.Replace("-", "");
                        creditLimits = "(" + creditLimits + ".00" + ")";
                        string creditLimitFCR = FleetCreditLimitPage.GetFirstRowData(TableHeaders.CreditLimit);
                        string creditLimitAvailable = FleetCreditLimitPage.GetFirstRowData(TableHeaders.CreditAvailable);
                        creditLimitFCR = creditLimitFCR.Replace(",", "");
                        Assert.AreEqual(creditLimits, creditLimitFCR, "Credit Limit Mismatch");
                        Assert.AreEqual(creditLimits, creditLimitAvailable, "Credit Available Mismatch");
                    }
                    else
                    {
                        if (creditLimit > 0)
                        {
                            string creditLimits = Convert.ToString(creditLimit);
                            creditLimits = creditLimits + ".00";
                            Assert.AreEqual(creditLimits, FleetCreditLimitPage.GetFirstRowData(TableHeaders.CreditLimit), "Credit Limit Mismatch");
                            Assert.AreEqual(creditLimits, FleetCreditLimitPage.GetFirstRowData(TableHeaders.CreditAvailable), "Credit Available Mismatch");
                        }
                        else
                        {
                            Assert.AreEqual(Convert.ToString(creditLimit), FleetCreditLimitPage.GetFirstRowData(TableHeaders.CreditLimit), "Credit Limit Mismatch");
                            Assert.AreEqual(Convert.ToString(creditLimit), FleetCreditLimitPage.GetFirstRowData(TableHeaders.CreditAvailable), "Credit Available Mismatch");
                        }
                    }
                    break;
                case "Update Credit":
                    UpdateCreditPage = new UpdateCreditPage(driver);
                    Assert.AreEqual(creditLimit, Convert.ToInt32(UpdateCreditPage.GetValue(FieldNames.CurrentCreditLimit)), "Fleet Credit Mismatch");
                    break;
            }
        }

        [When(@"Search fleet ""([^""]*)"" on ""([^""]*)""")]
        public void WhenSearchFleetOn(string fleetName, string pageName)
        {
            FleetCreditLimitPage FleetCreditLimitPage;
            UpdateCreditPage UpdateCreditPage;
            AccountMaintenancePage AccountMaintenance;
            AccountMaintenanceAspx AspxPage;
            switch (pageName)
            {
                case "Account Maintenance":
                    AccountMaintenance = new AccountMaintenancePage(driver);
                    AccountMaintenance.LoadDataOnGrid(fleetName, EntityType.Fleet);
                    AccountMaintenance.SetFilterCreiteria(TableHeaders.Name, FilterCriteria.Equals);
                    AccountMaintenance.FilterTable(TableHeaders.Name, fleetName);
                    AccountMaintenance.ClickHyperLinkOnGrid(TableHeaders.Name);
                    break;
                case "Fleet Credit Limit":
                    FleetCreditLimitPage = new FleetCreditLimitPage(driver);
                    FleetCreditLimitPage.PopulateGrid(fleetName);
                    break;
                case "Update Credit":
                    UpdateCreditPage = new UpdateCreditPage(driver);
                    UpdateCreditPage.SwitchIframe();
                    UpdateCreditPage.SearchAndSelectValue(FieldNames.BillingLocation, fleetName);
                    break;
            }
        }


        [Then(@"User should be able to update credit (.*) for fleet ""([^""]*)""")]
        public void ThenUserShouldBeAbleToUpdateCreditOn(string creditLimit, string fleetName)
        {
            UpdateCreditPage UpdateCreditPage;
            menu = new Menu(driver);
            menu.OpenNextPage(Pages.AccountMaintenance, Pages.UpdateCredit, true);
            UpdateCreditPage = new UpdateCreditPage(driver);
            UpdateCreditPage.SwitchIframe();
            UpdateCreditPage.SearchAndSelectValue(FieldNames.BillingLocation, fleetName);
            UpdateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, creditLimit);
            UpdateCreditPage.Click(FieldNames.Update);
            if (UpdateCreditPage.CheckForText("Please enter credit limit"))
            {
                UpdateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, creditLimit);
                UpdateCreditPage.Click(FieldNames.Update);
            }
            Assert.IsTrue(UpdateCreditPage.CheckForText(ButtonsAndMessages.UpdateCreditSuccessfully, true), "Credit Not Updated Sucessfully");
            Assert.AreEqual(Convert.ToInt32(creditLimit), Convert.ToInt32(UpdateCreditPage.GetValue(FieldNames.CurrentCreditLimit)), "Updated Credit Mismatch");
        }

        [Given(@"Relationship Financial Handling and Payment Method Are Not Available On Fleet ""([^""]*)""")]
        public void WhenUserShouldNotHaveFinancialHandlingAndPaymentMethodRelationship(string fleet)
        {
            AccountMaintenanceUtil.DeleteFinancialHandlingRel(fleet, FieldNames.Fleet);
            AccountMaintenanceUtil.DeletePaymentMethodRel(fleet, FieldNames.Fleet);
        }

        [Given(@"Remove Relationship ""([^""]*)"" for Fleet ""([^""]*)"" from DB")]
        public void RelationshipDeletionFromDB(string relationship, string fleetName)
        {
            AccountMaintenanceAspx AspxPage;
            AspxPage = new AccountMaintenanceAspx(driver);
            Assert.IsTrue(AspxPage.DeleteRelationshipFromDB(fleetName, FieldNames.Fleet, relationship));
        }

        [StepDefinition(@"Invoice is submitted from DMS with Fleet ""([^""]*)"" and Dealer ""([^""]*)"" with transaction amount (.*) and quantity (.*)")]
        public void InvoiceSubmissionFromDMS(string fleetName, string dealerName, double transactionAmount, int quantity)
        {
            new DMSServices().SubmitInvoiceWithTransactionAmount(dealerInvNum, dealerName, fleetName, transactionAmount, quantity);
        }

        [When(@"Invoice is submitted from DMS with Buyer ""([^""]*)"" and Supplier ""([^""]*)"" with current transaction date")]
        public void WhenInvoiceIsSubmittedFromDMSWithBuyerAndSupplierWithTransactionDate(string fleetName, string dealerName)
        {
            new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
        }

        [When(@"Invoice of transaction type ""([^""]*)"" is submitted from DMS with Supplier ""([^""]*)"" and Buyer ""([^""]*)"" with transaction date (.*) days from current date")]
        public void WhenInvoiceIsSubmittedFromDMSWithSupplierAndBuyerWithTransactionDateDaysFromCurrentDate(string transactionType, string dealerName, string fleetName, int days)
        {
            transactionDays = days;
            new DMSServices().SubmitInvoiceWithTransactionDate(transactionType, dealerInvNum, dealerName, fleetName, days);
        }

        [When(@"Invoice is submitted from DMS with buyer ""([^""]*)"" and supplier ""([^""]*)"" with transaction amount (.*) and transaction date (.*) from current date")]
        public void WhenInvoiceIsSubmittedFromDMSWithFleetAndDealerWithTransactionAmountAndTransactionDateFromCurrentDate(string fleetName, string dealerName, int transactionAmount, int days)
        {
            transactionDays = days;
            new DMSServices().SubmitInvWithTransactionAmountAndDate(dealerInvNum, dealerName, fleetName, transactionAmount, days);
        }

        [When(@"Authorization is submitted from DMS with Fleet ""([^""]*)"" and Dealer ""([^""]*)""")]
        public void AuthorizationSubmissionFromDMS(string fleetName, string dealerName)
        {
            new DMSServices().CreateAuthorization(dealerInvNum, dealerName, fleetName, out string authCode);
        }

        [When(@"Authorization is submitted from DMS with Fleet ""([^""]*)"" and Dealer ""([^""]*)"" with transaction amount (.*)")]
        public void WhenAuthorizationIsSubmittedFromDMSWithFleetAndDealerWithTransactionAmount(string fleetName, string dealerName, int transactionAmount)
        {
            new DMSServices().CreateAuthorizationWithTransactionAmount(dealerInvNum, dealerName, fleetName, out string authCode, transactionAmount);
        }


        [StepDefinition(@"invoice moved to discrepancy state with error ""([^""]*)"" for fleet ""([^""]*)"" with credit ""([^""]*)""")]
        public void InvoiceValidationBackend(string errorMsg, string fleetName, string credit)
        {
            switch (errorMsg)
            {
                case "Unit Number":
                    Assert.AreEqual(2, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(discrepantDealerInv, "Fleet Requires : Unit Number for Processing."), "Error Still Active In DB");
                    break;
                case "Credit not available":
                    Assert.AreEqual(4, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(discrepantDealerInv, "Credit not available"), "Error Still Active In DB");
                    break;
                case "Awaiting Fleet Release":
                    Assert.AreEqual(13, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(discrepantDealerInv, "Invoice Awaiting Fleet Release"), "Error Still Active In DB");
                    break;
                case "Awaiting Dealer Release":
                    Assert.AreEqual(9, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(discrepantDealerInv, "Invoice Awaiting Dealer Release"), "Error Still Active In DB");
                    break;
                case "Invoice on hold":
                    Assert.AreEqual(4, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(discrepantDealerInv, "Invoice on hold, awaiting physical copy"), "Error Still Active In DB");
                    break;
            }
            if (credit != string.Empty)
            {
                Assert.AreEqual(Convert.ToDecimal(credit), CommonUtils.GetAvailableCreditLimit(fleetName));
            }
        }

        [Then(@"invoice moved to discrepancy state with error ""([^""]*)"" for fleet ""([^""]*)"" and dealer ""([^""]*)"" with credit ""([^""]*)""")]
        public void ThenInvoiceMovedToDiscrepancyStateWithErrorForFleetAndSupplierWithCredit(string errorMsg, string fleetName, string dealerName, string credit)
        {
            switch (errorMsg)
            {
                case "Unit Number":
                    Assert.AreEqual(2, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Fleet Requires : Unit Number for Processing."), "Error Still Active In DB");
                    break;
                case "Credit not available":
                    Assert.AreEqual(4, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Credit not available"), "Error Still Active In DB");
                    break;
                case "Awaiting Fleet Release":
                    Assert.AreEqual(13, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Invoice Awaiting Fleet Release"), "Error Still Active In DB");
                    break;
                case "Awaiting Dealer Release":
                    Assert.AreEqual(9, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Invoice Awaiting Dealer Release"), "Error Still Active In DB");
                    break;
                case "Invoice on hold":
                    Assert.AreEqual(4, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Invoice on hold, awaiting physical copy"), "Error Still Active In DB");
                    break;
            }
            if (credit != string.Empty)
            {
                Assert.AreEqual(Convert.ToDecimal(credit), CommonUtils.GetAvailableCreditLimit(fleetName));
            }
        }

        [Then(@"Invoice should be in discrepancy with error ""([^""]*)"" for buyer ""([^""]*)"" and supplier ""([^""]*)""")]
        public void ThenInvoiceMovedToDiscrepancyStateWithErrorForBuyerAndSupplier(string errorMsg, string fleetName, string dealerName)
        {
            switch (errorMsg)
            {
                case "Unit Number":
                    Assert.AreEqual(2, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Fleet Requires : Unit Number for Processing."), "Error InActive In DB");
                    break;
                case "Credit not available":
                    Assert.AreEqual(4, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Credit not available"), "Error Not Found In DB");
                    break;
                case "Awaiting Fleet Release":
                    Assert.AreEqual(13, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Invoice Awaiting Fleet Release"), "Error Not Found In DB");
                    break;
                case "Awaiting Dealer Release":
                    Assert.AreEqual(9, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Invoice Awaiting Dealer Release"), "Error Not Found In DB");
                    break;
                case "Invoice on hold":
                    Assert.AreEqual(4, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Invoice on hold, awaiting physical copy"), "Error Not Found In DB");
                    break;
                case "Transaction date is invalid":
                    Assert.AreEqual(3, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Transaction Date is invalid or in future or earlier than"), "Error Not Found In DB");
                    CommonUtils.UpdateInvoiceValidityDays(60);
                    break;
            }
        }

        [Then(@"Invoice expiration date is (.*) days to transaction date")]
        public void ThenValidateInvoiceInDiscrepancyWithExpirationDateDaysThanTransactionDate(int days)
        {
            int expdays = transactionDays + days;
            Assert.AreEqual(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(expdays)), CommonUtils.GetInvoiceExpirationDate(dealerInvNum), "Expiration Date Mismatch");
        }

        [Then(@"invoice moved to discrepancy state with error ""([^""]*)"" for fleet ""([^""]*)"" and supplier ""([^""]*)""")]
        public void ThenInvoiceMovedToDiscrepancyStateWithErrorForFleetAndSupplier(string errorMsg, string fleetName, string dealerName)
        {
            switch (errorMsg)
            {
                case "Unit Number":
                    Assert.AreEqual(2, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(discrepantDealerInv, "Fleet Requires : Unit Number for Processing."), "Error Still Active In DB");
                    break;
                case "Credit not available":
                    Assert.AreEqual(4, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(discrepantDealerInv, "Credit not available"), "Error Still Active In DB");
                    break;
                case "Awaiting Fleet Release":
                    Assert.AreEqual(13, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.IsTrue(CommonUtils.ValidateTransactionErrorToken(discrepantDealerInv, "Invoice Awaiting Fleet Release:"), "Error Still Active In DB");
                    break;
            }
            Assert.AreEqual(0.00, CommonUtils.GetAvailableCreditLimit(fleetName));
        }


        [Then(@"Authorization should move to discrepancy state with error ""([^""]*)"" for fleet ""([^""]*)""")]
        public void AuthorizationValidationBackend(string errorMsg, string fleetName)
        {
            Assert.AreEqual(3, CommonUtils.GetValidationStatus(dealerInvNum));
            Assert.IsTrue(CommonUtils.ValidateTransactionError(dealerInvNum, errorMsg));
            Assert.AreEqual(0.00, CommonUtils.GetAvailableCreditLimit(fleetName));
        }

        [Then(@"Authorization should move to discrepancy state with error ""([^""]*)"" for fleet ""([^""]*)"" with credit ""([^""]*)""")]
        public void ThenAuthorizationShouldMoveToDiscrepancyStateWithErrorForFleetWithCredit(string errorMsg, string fleetName, string credit)
        {
            Assert.AreEqual(3, CommonUtils.GetValidationStatus(dealerInvNum));
            Assert.IsTrue(CommonUtils.ValidateTransactionError(dealerInvNum, errorMsg));
            Assert.AreEqual(Convert.ToDecimal(credit), CommonUtils.GetAvailableCreditLimit(fleetName));
        }


        [Then(@"Authorization should move to settle state without error ""([^""]*)"" for fleet ""([^""]*)"" with credit ""([^""]*)""")]
        public void ThenAuthorizationShouldMoveToSettleStateWithoutErrorForFleet(string errorMsg, string fleetName, string credit)
        {
            Assert.AreEqual(1, CommonUtils.GetValidationStatus(dealerInvNum));
            Assert.IsFalse(CommonUtils.ValidateTransactionError(dealerInvNum, errorMsg));
            switch (credit)
            {
                case "Negative":
                    decimal availCred = CommonUtils.GetAvailableCreditLimit(fleetName);
                    if (!Convert.ToString(availCred).Contains('-'))
                    {
                        Assert.Fail("Credit is in Positive Value");
                    }
                    break;
                case "Postive":
                    decimal availCreds = CommonUtils.GetAvailableCreditLimit(fleetName);
                    if (Convert.ToString(availCreds).Contains('-') == true)
                    {
                        Assert.Fail("Credit is Negative, Expected Positive Credit");
                    }
                    break;
            }

        }


        [Then(@"invoice should move to settle state without error ""([^""]*)"" for fleet ""([^""]*)""")]
        public void InvoiceValidationWithSettleState(string errorMsg, string fleetName)
        {
            Assert.AreEqual(1, CommonUtils.GetValidationStatus(dealerInvNum));
            Assert.IsFalse(CommonUtils.ValidateTransactionError(dealerInvNum, errorMsg));
            int sysType = CommonUtils.GetSystemType(dealerInvNum);
            if (sysType == 1 || sysType == 2 || sysType == 3)
            {
                //DoNothing
            }
            else
            {
                Assert.Fail("System Type Invalid");
            }
            string availCredit = Convert.ToString(CommonUtils.GetAvailableCreditLimit(fleetName));
            Assert.IsTrue(availCredit.Contains('-'));
        }

        [Then(@"Invoice should move to settle state without error ""([^""]*)""")]
        public void InvoiceValidationWithErrorMsg(string errorMsg)
        {
            Assert.AreEqual(1, CommonUtils.GetValidationStatus(dealerInvNum));
            Assert.IsFalse(CommonUtils.ValidateTransactionError(dealerInvNum, errorMsg));
            CommonUtils.UpdateInvoiceValidityDays(60);
        }

        [Then(@"Invoice should be settled successfully with no errors")]
        public void ThenInvoiceShouldBeSettledSuccessfullyWithNoErrors()
        {
            Assert.AreEqual(1, CommonUtils.GetValidationStatus(dealerInvNum));
            CommonUtils.UpdateInvoiceValidityDays(60);
        }


        [When(@"User Create Authorization with type ""([^""]*)"" for Supplier ""([^""]*)"" and Buyer ""([^""]*)"" with invoice amount ""([^""]*)""")]
        public void WhenUserCreateAuthorizationWithTypeForDealerAndFleetWithInvoiceAmount(string authType, string dealerName, string fleetName, string invAmount)
        {
            CreateAuthorizationPage Page;
            Menu menu;
            Page = new CreateAuthorizationPage(driver);
            menu = new Menu(driver);
            menu.SwitchToMainWindow();
            menu.SwitchIframe();
            Page.SelectValueByScroll(FieldNames.TransactionType, authType);
            Page.EnterDateInInvoiceDate();
            Page.EnterDealerCode(dealerName);
            Page.SearchAndSelectValue(FieldNames.FleetCode, fleetName);
            Page.ClickContinue();
            Page.WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            Page.WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            Page.EnterInvoiceAmount(invAmount);
            Page.EnterTextAfterClear(FieldNames.PurchaseOrderNumber, dealerInvNum);
            Page.EnterTextAfterClear(FieldNames.InvoiceNumber, dealerInvNum);
            Page.ClickCreateAuthorization();
        }

        [When(@"User Create Authorization with type ""([^""]*)"" for Supplier ""([^""]*)"" and Buyer ""([^""]*)"" with invoice amount ""([^""]*)"" from Open Authorization")]
        public void AuthorizatonCreationFromOpenAuthorization(string authType, string dealerName, string fleetName, string invAmount)
        {
            OpenAuthorizationsPage Page;
            Page = new OpenAuthorizationsPage(driver);
            Page.GridLoad();
            CreateNewAuthorizationPopUpPage aspxPage = Page.CreateNewAuthorization();
            aspxPage.SelectValueByScroll(FieldNames.TransactionType, authType);
            aspxPage.EnterDateInInvoiceDate();
            aspxPage.EnterDealerCode(dealerName);
            aspxPage.EnterFleetCode(fleetName);
            aspxPage.ClickContinue();
            aspxPage.WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            aspxPage.WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            aspxPage.EnterInvoiceAmount(invAmount);
            aspxPage.EnterTextAfterClear(FieldNames.PurchaseOrderNumber, dealerInvNum);
            aspxPage.EnterTextAfterClear(FieldNames.InvoiceNumber, dealerInvNum);
            aspxPage.ClickCreateAuthorization();

        }


        [StepDefinition(@"""([^""]*)"" message should appear on UI for page ""([^""]*)""")]
        public void ThenErrorShouldAppearOnUIForPage(string Msg, string pageName)
        {
            switch (pageName)
            {
                case "Create Authorization":
                    CreateAuthorizationPage CreateAuthorizationPage;
                    CreateAuthorizationPage = new CreateAuthorizationPage(driver);
                    Assert.AreEqual(Msg, CreateAuthorizationPage.GetText("Error Message").Trim());
                    break;
                case "Invoice Entry":
                    InvoiceEntryPage InvoiceEntryPage;
                    InvoiceEntryPage = new InvoiceEntryPage(driver);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    Assert.IsTrue(InvoiceEntryPage.CheckForText(Msg));
                    break;
                case "Offset Transaction":
                    OffsetTransactionPage OffsetTransactionPopUpPage;
                    OffsetTransactionPopUpPage = new OffsetTransactionPage(driver);
                    Assert.IsTrue(OffsetTransactionPopUpPage.CheckForTextForOffSetTransactions(Msg, true));
                    break;
                case "Create Authorization Popup":
                    CreateNewAuthorizationPopUpPage aspxPage;
                    aspxPage = new CreateNewAuthorizationPopUpPage(driver);
                    Assert.IsTrue(aspxPage.CheckForText(ButtonsAndMessages.SuccessfulTransaction));
                    break;
            }

        }

        [StepDefinition(@"Invoice is resolved from ""([^""]*)"" discrepancy for ""([^""]*)"" buyer and ""([^""]*)"" supplier")]
        public void InvoiceResolutionFromDB(string discrepancyState, string fleetName, string dealerName)
        {
            InvoiceDiscrepancyPage Page;
            Page = new InvoiceDiscrepancyPage(driver);
            CreateNewInvoicePage InvoiceEntryPage;
            InvoiceEntryPage = new CreateNewInvoicePage(driver);
            FleetReleaseInvoicesPage FRInvPage;
            FRInvPage = new FleetReleaseInvoicesPage(driver);
            DealerReleaseInvoicesPage DRInvPage;
            DRInvPage = new DealerReleaseInvoicesPage(driver);

            switch (discrepancyState)
            {
                case "Unit Number":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    Page.LoadDataOnGrid(fleetName, false);
                    string unitNumber = "12345";
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    InvoiceEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, unitNumber);
                    Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t.Wait();
                    t.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t.Wait();
                        t.Dispose();
                    }
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.WaitForLoadingIcon();

                    }
                    else if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.OK))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.OK);
                        InvoiceEntryPage.ClosePopupWindow();
                    }
                    break;
                case "Credit Not Available":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    Page.LoadDataOnGrid(fleetName, false);
                    CommonUtils.UpdateFleetCreditLimits(9999999, 9999999, fleetName);
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    Task t1 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t1.Wait();
                    t1.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t1 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t1.Wait();
                        t1.Dispose();
                    }
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.WaitForLoadingIcon();
                    }
                    else if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.OK))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.OK);
                        InvoiceEntryPage.ClosePopupWindow();
                    }
                    break;
                case "On hold for physical copy":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    Page.LoadDataOnGrid(fleetName, false);
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    Task t2 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t2.Wait();
                    t2.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t2 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t2.Wait();
                        t2.Dispose();
                    }
                    InvoiceEntryPage.CheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.WaitForLoadingIcon();
                    }
                    break;
                case "Awaiting Fleet Release":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    FRInvPage.GridLoad();
                    FRInvPage.SetFilterCreiteria(TableHeaders.DealerInvoiceNumber, FilterCriteria.Equals);
                    FRInvPage.FilterTable(TableHeaders.DealerInvoiceNumber, discrepantDealerInv);
                    FRInvPage.ClickHyperLinkOnGrid(TableHeaders.DealerInvoiceNumber);
                    FRInvPage.SwitchToPopUp();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Release);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.WaitForPopupWindowToClose();
                    }
                    break;
                case "Dealer Code Invalid":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName + '!', fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    Page.LoadDataOnGrid(fleetName, false);
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    InvoiceEntryPage.SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealerName);
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    InvoiceEntryPage.Click(ButtonsAndMessages.OK);
                    InvoiceEntryPage.ClosePopupWindow();
                    break;
                case "Awaiting Dealer Release":
                    DealerReleaseInvoicesUtils.GetData(dealerName, fleetName, out string TransactionNumber, out string FromDate, out string ToDate);
                    CreateInvoiceIfNotExistForDiscrepantInvoice(TransactionNumber, dealerName, fleetName);                    
                    DRInvPage.PopulateGrid(fleetName, FromDate, ToDate);
                    DRInvPage.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    DRInvPage.FilterTable(TableHeaders.DealerInv__spc, TransactionNumber);
                    DRInvPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    DRInvPage.SwitchToPopUp();
                    Task t3 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t3.Wait();
                    t3.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t3 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t3.Wait();
                        t3.Dispose();
                    }
                    InvoiceEntryPage.Click(ButtonsAndMessages.Release);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.AcceptWindowAlert(out string invoiceMsg1);
                        Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);
                        InvoiceEntryPage.WaitForPopupWindowToClose();
                    }
                    break;
            }
        }

        [When(@"Invoice is resolved from ""([^""]*)"" discrepancy for ""([^""]*)"" buyer and ""([^""]*)"" supplier by alert ""([^""]*)""")]
        public void WhenInvoiceIsResolvedFromDiscrepancyForBuyerAndSupplierIntoSettleState(string discrepancyState, string fleetName, string dealerName, string alertMsg)
        {
            InvoiceDiscrepancyPage Page;
            Page = new InvoiceDiscrepancyPage(driver);
            CreateNewInvoicePage InvoiceEntryPage;
            InvoiceEntryPage = new CreateNewInvoicePage(driver);
            FleetReleaseInvoicesPage FRInvPage;
            FRInvPage = new FleetReleaseInvoicesPage(driver);
            DealerReleaseInvoicesPage DRInvPage;
            DRInvPage = new DealerReleaseInvoicesPage(driver);

            switch (discrepancyState)
            {
                case "Unit Number":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    Page.LoadDataOnGrid(fleetName, false);
                    string unitNumber = "12345";
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    InvoiceEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, unitNumber);
                    Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t.Wait();
                    t.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t.Wait();
                        t.Dispose();
                    }
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                    InvoiceEntryPage.AcceptWindowAlert(out string invoiceMsg1);
                    Assert.AreEqual(alertMsg, invoiceMsg1);
                    break;
                case "Credit Not Available":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    Page.LoadDataOnGrid(fleetName, false);
                    CommonUtils.UpdateFleetCreditLimits(9999999, 9999999, fleetName);
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    Task t1 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t1.Wait();
                    t1.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t1 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t1.Wait();
                        t1.Dispose();
                    }
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                    InvoiceEntryPage.AcceptWindowAlert(out string invoiceMsg2);
                    Assert.AreEqual(alertMsg, invoiceMsg2);
                    break;
                case "On hold for physical copy":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    Page.LoadDataOnGrid(fleetName, false);
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    Task t2 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t2.Wait();
                    t2.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t2 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t2.Wait();
                        t2.Dispose();
                    }
                    InvoiceEntryPage.CheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                    InvoiceEntryPage.AcceptWindowAlert(out string invoiceMsg3);
                    Assert.AreEqual(alertMsg, invoiceMsg3);
                    break;
                case "Awaiting Fleet Release":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {
                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    FRInvPage.GridLoad();
                    FRInvPage.SetFilterCreiteria(TableHeaders.DealerInvoiceNumber, FilterCriteria.Equals);
                    FRInvPage.FilterTable(TableHeaders.DealerInvoiceNumber, discrepantDealerInv);
                    FRInvPage.ClickHyperLinkOnGrid(TableHeaders.DealerInvoiceNumber);
                    FRInvPage.SwitchToPopUp();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Release);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                    InvoiceEntryPage.WaitForPopupWindowToClose();
                    break;
                case "Dealer Code Invalid":
                    discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
                    if (discrepantDealerInv == null)
                    {

                        new DMSServices().SubmitInvoice(dealerInvNum, dealerName + '!', fleetName);
                        discrepantDealerInv = dealerInvNum;
                    }
                    Page.LoadDataOnGrid(fleetName, false);
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    InvoiceEntryPage.SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealerName);
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                    InvoiceEntryPage.AcceptWindowAlert(out string invoiceMsg4);
                    Assert.AreEqual(alertMsg, invoiceMsg4);
                    break;
                case "Awaiting Dealer Release":
                    DealerReleaseInvoicesUtils.GetData(dealerName, fleetName, out string TransactionNumber, out string FromDate, out string ToDate);
                    CreateInvoiceIfNotExistForDiscrepantInvoice(TransactionNumber, dealerName, fleetName);
                    DRInvPage.PopulateGrid(fleetName, FromDate, ToDate);
                    DRInvPage.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    DRInvPage.FilterTable(TableHeaders.DealerInv__spc, TransactionNumber);
                    DRInvPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    DRInvPage.SwitchToPopUp();
                    Task t3 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t3.Wait();
                    t3.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t3 = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t3.Wait();
                        t3.Dispose();
                    }
                    InvoiceEntryPage.Click(ButtonsAndMessages.Release);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                    InvoiceEntryPage.AcceptWindowAlert(out string invoiceMsg5);
                    Assert.AreEqual(alertMsg, invoiceMsg5);
                    break;
            }
        }


        [StepDefinition(@"Invoice is resolved from ""([^""]*)"" discrepancy for ""([^""]*)"" buyer")]
        public void InvoiceResolution(string discrepancyState, string fleetName)
        {
            InvoiceDiscrepancyPage Page;
            Page = new InvoiceDiscrepancyPage(driver);
            CreateNewInvoicePage InvoiceEntryPage;
            InvoiceEntryPage = new CreateNewInvoicePage(driver);
            FleetReleaseInvoicesPage FRInvPage;
            FRInvPage = new FleetReleaseInvoicesPage(driver);
            DealerReleaseInvoicesPage DRInvPage;
            DRInvPage = new DealerReleaseInvoicesPage(driver);

            switch (discrepancyState)
            {
                case "Unit Number":
                    string unitNumber = "12345";
                    Page.LoadDataOnGrid(fleetName);
                    Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvNum);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    InvoiceEntryPage.EnterTextAfterClear(FieldNames.UnitNumber, unitNumber);
                    Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t.Wait();
                    t.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t.Wait();
                        t.Dispose();
                    }
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.WaitForLoadingIcon();
                    }
                    else if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.OK))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.OK);
                        InvoiceEntryPage.ClosePopupWindow();
                    }
                    InvoiceEntryPage.ClosePopupWindow();
                    break;
                case "Credit Not Available":
                    CommonUtils.UpdateFleetCreditLimits(9999999, 9999999, fleetName);
                    Page.LoadDataOnGrid(fleetName);
                    Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvNum);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.WaitForLoadingIcon();
                    }
                    else if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.OK))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.OK);
                        InvoiceEntryPage.ClosePopupWindow();
                    }
                    InvoiceEntryPage.ClosePopupWindow();
                    break;
                case "Awaiting Reseller Release":
                    JobHelper.ExecuteJob("SettleResellerTransactions", "c1qsrs01-uspa01");
                    break;
                case "On hold for physical copy":
                    Page.LoadDataOnGrid(fleetName);
                    Page.FilterTable(TableHeaders.DealerInv__spc, dealerInvNum);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    Page.SwitchToPopUp();
                    InvoiceEntryPage.CheckCheckBox(FieldNames.DoNotPutHoldForDealerCopy);
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                    }
                    else if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.OK))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.OK);
                        InvoiceEntryPage.ClosePopupWindow();
                    }
                    InvoiceEntryPage.ClosePopupWindow();
                    break;
                case "Awaiting Fleet Release":
                    FRInvPage.GridLoad();
                    FRInvPage.FilterTable(TableHeaders.Fleet, fleetName);
                    FRInvPage.ClickHyperLinkOnGrid(TableHeaders.DealerInvoiceNumber);
                    FRInvPage.SwitchToPopUp();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Release);
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.AcceptWindowAlert(out string invoiceMsg1);
                        Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);
                    }
                    break;
                case "Awaiting Dealer Release":
                    DRInvPage.GridLoad();
                    DRInvPage.FilterTable(TableHeaders.DealerInv__spc, dealerInvNum);
                    DRInvPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    DRInvPage.SwitchToPopUp();
                    InvoiceEntryPage.Click(ButtonsAndMessages.Release);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.AcceptWindowAlert(out string invoiceMsg1);
                        Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);
                    }
                    break;
            }

            switch (discrepancyState)
            {
                case "Unit Number":
                    Assert.IsFalse(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Fleet Requires : Unit Number for Processing."), "Error Still Active In DB");
                    break;
                case "Credit not available":
                    Assert.IsFalse(CommonUtils.ValidateTransactionErrorToken(dealerInvNum, "Credit not available"), "Error Still Active In DB");
                    break;
            }

        }

        [StepDefinition(@"Invoice is submitted successfully for ""([^""]*)"" Location with ""([^""]*)"" credit for fleet ""([^""]*)""")]
        public void InvoiceBackendValidation(string invoiceType, string credit, string fleetName)
        {
            int sysType = CommonUtils.GetSystemType(dealerInvNum);
            switch (invoiceType)
            {
                case "Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(dealerInvNum));
                    Assert.AreEqual(0, sysType, "System Type Invaild");
                    switch (credit)
                    {
                        case "Negative":
                            decimal availCred = CommonUtils.GetAvailableCreditLimit(fleetName);
                            if (!Convert.ToString(availCred).Contains('-'))
                            {
                                Assert.Fail("Credit is Positive, Expected Negative Credit");
                            }
                            break;
                        case "Postive":
                            decimal availCreds = CommonUtils.GetAvailableCreditLimit(fleetName);
                            if (Convert.ToString(availCreds).Contains('-') == true)
                            {
                                Assert.Fail("Credit is Negative, Expected Positive Value");
                            }
                            break;
                    }
                    break;
                case "Non-Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(dealerInvNum));
                    switch (credit)
                    {
                        case "Negative":
                            decimal availCred = CommonUtils.GetAvailableCreditLimit(fleetName);
                            if (!Convert.ToString(availCred).Contains('-'))
                            {
                                Assert.Fail("Credit is Positive, Expected Negative Credit");
                            }
                            break;
                        case "Postive":
                            decimal availCreds = CommonUtils.GetAvailableCreditLimit(fleetName);
                            if (Convert.ToString(availCreds).Contains('-') == true)
                            {
                                Assert.Fail("Credit is Negative, Expected Positive Value");
                            }
                            break;
                    }
                    if (sysType == 1 || sysType == 2 || sysType == 3)
                    {
                        //DoNothing
                    }
                    else
                    {
                        Assert.Fail("System Type Invalid");
                    }
                    break;
            }
        }

        [Then(@"Invoice is submitted successfully for ""([^""]*)"" Location with ""([^""]*)"" credit for fleet ""([^""]*)"" and dealer ""([^""]*)""")]
        public void ThenInvoiceIsSubmittedSuccessfullyForLocationWithCreditForFleetAndDealer(string invoiceType, string credit, string fleetName, string dealerName)
        {
            int sysType = CommonUtils.GetSystemType(discrepantDealerInv);
            switch (invoiceType)
            {
                case "Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.AreEqual(0, sysType, "System Type Invaild");
                    switch (credit)
                    {
                        case "Negative":
                            decimal availCred = CommonUtils.GetAvailableCreditLimit(fleetName);
                            if (!Convert.ToString(availCred).Contains('-'))
                            {
                                Assert.Fail("Credit is Positive, Expected Negative Credit");
                            }
                            break;
                        case "Postive":
                            decimal availCreds = CommonUtils.GetAvailableCreditLimit(fleetName);
                            if (Convert.ToString(availCreds).Contains('-') == true)
                            {
                                Assert.Fail("Credit is Negative, Expected Positive Value");
                            }
                            break;
                    }
                    break;
                case "Non-Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    switch (credit)
                    {
                        case "Negative":
                            decimal availCred = CommonUtils.GetAvailableCreditLimit(fleetName);
                            if (!Convert.ToString(availCred).Contains('-'))
                            {
                                Assert.Fail("Credit is Positive, Expected Negative Credit");
                            }
                            break;
                        case "Postive":
                            decimal availCreds = CommonUtils.GetAvailableCreditLimit(fleetName);
                            if (Convert.ToString(availCreds).Contains('-') == true)
                            {
                                Assert.Fail("Credit is Negative, Expected Positive Value");
                            }
                            break;
                    }
                    if (sysType == 1 || sysType == 2 || sysType == 3)
                    {
                        //DoNothing
                    }
                    else
                    {
                        Assert.Fail("System Type Invalid");
                    }
                    break;
            }
        }


        [Then(@"Invoice is submitted successfully for ""([^""]*)"" Location with ""([^""]*)"" buyer")]
        public void ThenInvoiceIsSubmittedSuccessfullyForLocationWithFleet(string invoiceType, string fleetName)
        {
            CreateNewInvoicePage InvoiceEntryPage;
            InvoiceEntryPage = new CreateNewInvoicePage(driver);
            int sysType = CommonUtils.GetSystemType(discrepantDealerInv);
            switch (invoiceType)
            {
                case "Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    Assert.AreEqual(0, sysType, "System Type Invaild");
                    break;
                case "Non-Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(discrepantDealerInv));
                    if (sysType == 1 || sysType == 2 || sysType == 3)
                    {
                        //DoNothing
                    }
                    else
                    {
                        Assert.Fail("System Type Invalid");
                    }
                    break;
            }
        }

        [Then(@"Invoice is submitted successfully for ""([^""]*)"" Location with ""([^""]*)"" buyer after Create Rebill")]
        public void InvoiceValidatonWithCreateRebill(string invoiceType, string fleetName)
        {
            int sysType = CommonUtils.GetSystemType(currentInvNum + "C");
            switch (invoiceType)
            {
                case "Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(currentInvNum + "C"));
                    Assert.AreEqual(0, sysType, "System Type Invaild");
                    break;
                case "Non-Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(currentInvNum + "C"));
                    if (sysType == 1 || sysType == 2 || sysType == 3)
                    {
                        //DoNothing
                    }
                    else
                    {
                        Assert.Fail("System Type Invalid");
                    }
                    break;
            }
        }

        [Then(@"Invoice is submitted successfully for ""([^""]*)"" Location with ""([^""]*)"" buyer after Reversal")]
        public void ThenInvoiceIsSubmittedSuccessfullyForLocationWithBuyerAfterReversal(string invoiceType, string fleetName)
        {
            int sysType = CommonUtils.GetSystemType(currentInvNum + "R");
            switch (invoiceType)
            {
                case "Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(currentInvNum + "R"));
                    Assert.AreEqual(0, sysType, "System Type Invaild");
                    break;
                case "Non-Corcentric":
                    Assert.AreEqual(1, CommonUtils.GetValidationStatus(currentInvNum + "R"));
                    if (sysType == 1 || sysType == 2 || sysType == 3)
                    {
                        //DoNothing
                    }
                    else
                    {
                        Assert.Fail("System Type Invalid");
                    }
                    break;
            }
        }


        [When(@"User submit ""([^""]*)"" discrepant invoice for ""([^""]*)"" buyer and ""([^""]*)"" supplier")]
        public void WhenUserSubmitDiscrepantInvoiceForBuyer(string discrepancyState, string fleetName, string dealerName)
        {
            discrepantDealerInv = CommonUtils.GetDiscrepantInvoice(discrepancyState, dealerName, fleetName);
            if (discrepantDealerInv == null)
            {
                new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                discrepantDealerInv = dealerInvNum;
            }
            InvoiceDiscrepancyPage Page;
            Page = new InvoiceDiscrepancyPage(driver);
            CreateNewInvoicePage InvoiceEntryPage;
            InvoiceEntryPage = new CreateNewInvoicePage(driver);
            Page.LoadDataOnGrid(fleetName, false);
            Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
            Page.FilterTable(TableHeaders.DealerInv__spc, discrepantDealerInv);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            Page.SwitchToPopUp();
            switch (discrepancyState)
            {
                case "On hold for physical copy":
                    Task t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t.Wait();
                    t.Dispose();
                    if (!InvoiceEntryPage.IsCheckBoxChecked(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t = Task.Run(() => InvoiceEntryPage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        InvoiceEntryPage.Click(InvoiceEntryPage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t.Wait();
                        t.Dispose();
                    }
                    InvoiceEntryPage.Click(ButtonsAndMessages.SubmitInvoice);
                    InvoiceEntryPage.WaitForLoadingIcon();
                    if (InvoiceEntryPage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        InvoiceEntryPage.Click(ButtonsAndMessages.Continue);
                        InvoiceEntryPage.WaitForLoadingIcon();
                    }

                    break;
            }
        }

        [When(@"""([^""]*)"" from ""([^""]*)"" dealer and ""([^""]*)"" fleet")]
        public void InvoiceCloneAndReversal(string rebillType, string dealerName, string fleetName)
        {
            DealerInvoicesPage Page;
            CreateNewInvoicePage CreateNewInvoicePage;
            InvoiceOptionsPage InvoiceOptionPopUpPage;
            Page = new DealerInvoicesPage(driver);
            currentInvNum = FleetInvoicesUtils.GetCurrentInvoice(dealerName, fleetName);
            Page.LoadDataOnGrid(currentInvNum);
            Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
            Page.FilterTable(TableHeaders.DealerInv__spc, currentInvNum);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);

            switch (rebillType)
            {
                case "Rebill the invoice":
                    OffsetTransactionPage OffsetTransactionPopUpPage = InvoiceOptionPopUpPage.CreateRebill();
                    OffsetTransactionPopUpPage.Click(ButtonsAndMessages.RebillTheInvoice);
                    Assert.IsTrue(OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.RebillTheInvoice));
                    OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.Comments);
                    OffsetTransactionPopUpPage.EnterText(FieldNames.Comments, "Comments");

                    OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Rebill);
                    OffsetTransactionPopUpPage.SwitchToMainWindow();
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
                    Page.SwitchToPopUp();
                    CreateNewInvoicePage = new CreateNewInvoicePage(driver);
                    Task t = Task.Run(() => CreateNewInvoicePage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                    CreateNewInvoicePage.Click(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress));
                    t.Wait();
                    t.Dispose();
                    if (!CreateNewInvoicePage.IsCheckBoxChecked(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress)))
                    {
                        t = Task.Run(() => CreateNewInvoicePage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                        CreateNewInvoicePage.Click(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress));
                        t.Wait();
                        t.Dispose();
                    }
                    CreateNewInvoicePage.WaitForElementToBeVisible(ButtonsAndMessages.EditLineItem);
                    CreateNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
                    CreateNewInvoicePage.WaitForLoadingIcon();
                    if (CreateNewInvoicePage.IsElementVisible(ButtonsAndMessages.Continue))
                    {
                        CreateNewInvoicePage.Click(ButtonsAndMessages.Continue);
                        CreateNewInvoicePage.WaitForLoadingIcon();
                    }
                    break;

                case "Create a reversal":

                    OffsetTransactionPage OffsetTransactionPopUpPages = InvoiceOptionPopUpPage.CreateRebill();
                    OffsetTransactionPopUpPages.Click(ButtonsAndMessages.CreateAReversal);
                    Assert.IsTrue(OffsetTransactionPopUpPages.IsRadioButtonChecked(ButtonsAndMessages.CreateAReversal));
                    OffsetTransactionPopUpPages.WaitForElementToBePresent(FieldNames.ReversalReason);
                    Task t1 = Task.Run(() => OffsetTransactionPopUpPages.WaitForStalenessOfElement("Dealer For Reversal"));
                    OffsetTransactionPopUpPages.SelectValueTableDropDown(FieldNames.ReversalReason, "Billed twice");
                    t1.Wait();
                    t1.Dispose();
                    OffsetTransactionPopUpPages.EnterText(FieldNames.FleetOrDealerApprover, "Fleet");
                    OffsetTransactionPopUpPages.Click(ButtonsAndMessages.Reverse);

                    break;
            }

        }

        [When(@"Rebill the invoice from ""([^""]*)"" dealer and ""([^""]*)"" fleet for settle state")]
        public void WhenFromDealerAndFleetForSettleState(string dealerName, string fleetName)
        {
            DealerInvoicesPage Page;
            CreateNewInvoicePage CreateNewInvoicePage;
            InvoiceOptionsPage InvoiceOptionPopUpPage;
            Page = new DealerInvoicesPage(driver);
            currentInvNum = FleetInvoicesUtils.GetCurrentInvoice(dealerName, fleetName);
            CreateInvoiceIfNotExist(currentInvNum, dealerName, fleetName);
            Page.LoadDataOnGrid(currentInvNum);
            Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
            Page.FilterTable(TableHeaders.DealerInv__spc, currentInvNum);
            Page.SetFilterCreiteria(TableHeaders.FleetCode, FilterCriteria.Equals);
            Page.FilterTable(TableHeaders.FleetCode, fleetName);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
            InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
            OffsetTransactionPage OffsetTransactionPopUpPage = InvoiceOptionPopUpPage.CreateRebill();
            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.RebillTheInvoice);
            Assert.IsTrue(OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.RebillTheInvoice));
            OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.Comments);
            OffsetTransactionPopUpPage.EnterText(FieldNames.Comments, "Comments");

            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Rebill);
            OffsetTransactionPopUpPage.SwitchToMainWindow();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            Page.SwitchToPopUp();
            CreateNewInvoicePage = new CreateNewInvoicePage(driver);
            Task t = Task.Run(() => CreateNewInvoicePage.WaitForStalenessOfElement(FieldNames.UnitNumber));
            CreateNewInvoicePage.Click(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress));
            t.Wait();
            t.Dispose();
            if (!CreateNewInvoicePage.IsCheckBoxChecked(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress)))
            {
                t = Task.Run(() => CreateNewInvoicePage.WaitForStalenessOfElement(FieldNames.UnitNumber));
                CreateNewInvoicePage.Click(CreateNewInvoicePage.RenameMenuField(FieldNames.SameAsDealerAddress));
                t.Wait();
                t.Dispose();
            }
            CreateNewInvoicePage.WaitForElementToBeVisible(ButtonsAndMessages.EditLineItem);
            CreateNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.Continue);
            CreateNewInvoicePage.AcceptWindowAlert(out string invoiceMsg1);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);
        }



        [StepDefinition(@"User ""([^""]*)"" be able to update credit to (.*)  for fleet ""([^""]*)""")]
        public void ThenUserBeAbleToUpdateCreditCreditAmountForFleet(string should, string creditLimit, string fleetName)
        {
            UpdateCreditPage UpdateCreditPage;
            switch (should)
            {
                case "should":
                    UpdateCreditPage = new UpdateCreditPage(driver);
                    UpdateCreditPage.WaitForIframe();
                    UpdateCreditPage.SwitchIframe();
                    UpdateCreditPage.SearchAndSelectValue(FieldNames.BillingLocation, fleetName);
                    UpdateCreditPage.WaitForElementToBeClickable(FieldNames.NewCreditLimit);
                    UpdateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, creditLimit);
                    UpdateCreditPage.Click(FieldNames.Update);
                    if (UpdateCreditPage.CheckForText("Please enter credit limit"))
                    {
                        UpdateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, creditLimit);
                        UpdateCreditPage.Click(FieldNames.Update);
                    }
                    break;

                case "should not":
                    UpdateCreditPage = new UpdateCreditPage(driver);
                    UpdateCreditPage.WaitForIframe();
                    UpdateCreditPage.SwitchIframe();
                    UpdateCreditPage.SearchAndSelectValue(FieldNames.BillingLocation, fleetName);
                    UpdateCreditPage.WaitForElementToBeClickable(FieldNames.NewCreditLimit);
                    UpdateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, creditLimit);
                    UpdateCreditPage.Click(FieldNames.Update);
                    if (UpdateCreditPage.CheckForText("Please enter credit limit"))
                    {
                        UpdateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, creditLimit);
                        UpdateCreditPage.Click(FieldNames.Update);
                        UpdateCreditPage.WaitForMsg(ButtonsAndMessages.PleaseWait);
                    }
                    else
                    {
                        UpdateCreditPage.WaitForMsg(ButtonsAndMessages.PleaseWait);
                    }
                    break;
            }
        }

        [Then(@"Credit limit (.*) should be updated successfully")]
        public void ThenCreditLimitShouldBeUpdatedSuccessfully(int creditLimit)
        {
            UpdateCreditPage UpdateCreditPage;
            UpdateCreditPage = new UpdateCreditPage(driver);
            Assert.IsTrue(UpdateCreditPage.CheckForText(ButtonsAndMessages.UpdateCreditSuccessfully, true), "Credit Not Updated Sucessfully");
            Assert.AreEqual(Convert.ToInt32(creditLimit), Convert.ToInt32(UpdateCreditPage.GetValue(FieldNames.CurrentCreditLimit)), "Updated Credit Mismatch");
        }

        [Then(@"Credit limit (.*) should not be updated")]
        public void ThenCreditLimitShouldNotBeUpdated(int creditLimit)
        {
            UpdateCreditPage UpdateCreditPage;
            UpdateCreditPage = new UpdateCreditPage(driver);
            Assert.IsTrue(UpdateCreditPage.CheckForText("For Non Corcentric locations credit limit cannot be updated", true), "For Non Corcentric locations credit limit cannot be updated message not displayed");
            Assert.AreNotEqual(Convert.ToInt32(creditLimit), Convert.ToInt32(UpdateCreditPage.GetValue(FieldNames.CurrentCreditLimit)), "Updated Credit Mismatch");
        }


        [StepDefinition(@"invoice is sucessfully created using Dealer ""([^""]*)"" and Fleet ""([^""]*)"" with invoice type ""([^""]*)""")]
        public void InvoiceCreationwovalidation(string dealer, string fleet, string partType)
        {
            List<string> errorMsgs = new List<string>();
            menu = new Menu(driver);
            menu.OpenPage(Pages.InvoiceEntry);
            InvoiceEntryPage = new InvoiceEntryPage(driver);
            CreateNewInvoicePage = InvoiceEntryPage.OpenCreateNewInvoice();
            errorMsgs = CreateNewInvoicePage.CreateNewInvoiceWithLineItems(fleet, dealer, dealerInvNum, partType);
        }

        [When(@"Invoice of type ""([^""]*)"" is created using Supplier ""([^""]*)"" and Buyer ""([^""]*)"" with current transaction date")]
        public void WhenInvoiceIsSucessfullyCreatedUsingDealerAndFleetWithTransactionTypeAndTransactionDate(string transactionType, string dealer, string fleet)
        {
            List<string> errorMsgs = new List<string>();
            menu = new Menu(driver);
            menu.OpenPage(Pages.InvoiceEntry);
            InvoiceEntryPage = new InvoiceEntryPage(driver);
            CreateNewInvoicePage = InvoiceEntryPage.OpenCreateNewInvoice();
            errorMsgs = CreateNewInvoicePage.CreateNewInvoiceWithLineItems(fleet, dealer, dealerInvNum, transactionType);
        }



        [Then(@"Exported To Accounting should be ""([^""]*)""")]
        public void ThenExportedToAccountingShouldBe(string status)
        {
            JobHelper.ExecuteJob("InvoiceWinService", "c1qsrs01-uspa01");
            switch (status)
            {
                case "True":
                    Assert.IsTrue(CommonUtils.GetExportToAccounting(dealerInvNum), "Exported To Accounting is Invalid");
                    break;
                case "False":
                    Assert.IsFalse(CommonUtils.GetExportToAccounting(dealerInvNum), "Exported To Accounting is Invalid");
                    break;
            }
        }

        [Then(@"Invoice should be settled successfully with expiration date null")]
        public void ThenInvoiceShouldBeSettledSuccessfullyWithExpirationDateNull()
        {
            Assert.IsTrue(CommonUtils.IsInvoiceValidated(dealerInvNum));
            Assert.AreEqual("", CommonUtils.GetInvoiceExpirationDate(dealerInvNum), "Expiration Date is not NULL");
            Assert.AreEqual(CommonUtils.GetCurrentDate(), CommonUtils.GetInvoiceTransactionDate(dealerInvNum));
            CommonUtils.UpdateInvoiceValidityDays(60);
        }

        [Then(@"Invoice marked as eligible for Online Payment")]
        public void ThenInvoiceMarkedAsEligibleForOnlinePayment()
        {
            Assert.AreEqual(1, CommonUtils.GetValidationStatus(dealerInvNum));
            Assert.IsTrue(InvoiceEntryUtils.IsInvoiceEligiblForPaymentPortal(dealerInvNum));
        }

        [Then(@"Invoice not marked as eligible for Online Payment")]
        public void ThenInvoiceNotMarkedAsEligibleForOnlinePayment()
        {
            Assert.AreEqual(1, CommonUtils.GetValidationStatus(dealerInvNum));
            Assert.IsFalse(InvoiceEntryUtils.IsInvoiceEligiblForPaymentPortal(dealerInvNum));
        }

        private string CreateInvoiceIfNotExistForDiscrepantInvoice(string transactionNumber, string dealerName , string fleetName)
        {
            if (String.IsNullOrEmpty(transactionNumber))
            {
                new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                return discrepantDealerInv = dealerInvNum;

            }
            else
            {
                return discrepantDealerInv = transactionNumber;
            }
        }

        private string CreateInvoiceIfNotExist(string transactionNumber, string dealerName, string fleetName)
        {
            if (String.IsNullOrEmpty(transactionNumber))
            {
                new DMSServices().SubmitInvoice(dealerInvNum, dealerName, fleetName);
                return currentInvNum = dealerInvNum;

            }
            return currentInvNum = transactionNumber;
        }
    }
}
