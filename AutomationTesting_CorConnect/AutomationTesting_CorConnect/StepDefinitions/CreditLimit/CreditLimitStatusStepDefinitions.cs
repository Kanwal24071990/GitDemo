using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerInvoices;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.UpdateCredit;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.CreditLimitStatus;
using AutomationTesting_CorConnect.Utils.FleetInvoices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using AutomationTesting_CorConnect.PageObjects.CreateNewEntity;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.DriverBuilder;

namespace AutomationTesting_CorConnect.StepDefinitions.CreditLimit
{
    [Binding]
    internal class CreditLimitStatusStepDefinitions : DriverBuilderClass
    {
        string transactionInvNum = null;
        int transactionAmount;
        int creditLimit;
        int updatedCreditAmount;
        string previousLastRunTime = null;
        string DbLastRunTime = null;
        int availableCreditLimit;
        int totalAR;
        string buyerCode;
        string dealerCode;
        string currentInvNum;
        string Action;
        CreateNewInvoicePage CreateNewInvoicePage;
        CreateNewEntityPage CreateNewEntityPage;
        Menu menu;
        string fleetName = "Automation_Fleet" + CommonUtils.RandomString(4);
        int currentInvTransactionAmount;
        int stagingHistoryCountBeforeJobRun;
        int stagingHistoryCountAfterJobRun;
        int authTransactionAmount;
        string authorizationCode = null;



        [Given(@"Credit Limit and Available credit limit is updated")]
        public void GivenAvailableCreditLimitIsUpdated(Table table)
        {
            var transactiondataobject = table.CreateInstance<TransactionDetails>();
            dealerCode = transactiondataobject.SUP;
            buyerCode = transactiondataobject.BYR;
            creditLimit = transactiondataobject.CreditAmount;
            transactionAmount = transactiondataobject.TransactionAmount;
            updatedCreditAmount = transactiondataobject.TransactionAmount;
            Action = transactiondataobject.ActionType;

            CommonUtils.ToggleEntityActivationFlag(buyerCode, true);
            CommonUtils.ToggleEntityTerminationFlag(buyerCode, false);



            switch (Action)
            {
                case "Create Authorization":
                case "Negative AvailCredit on Create Auth":
                case "Create Auth & Suspend Account":
                case "Create Auth & Inactivate Account":
                    if (Action == "Create Auth & Suspend Account")
                    {
                        CommonUtils.ToggleEntitySuspensionRelationship(buyerCode, false);
                    }
                    CommonUtils.UpdateFleetCreditLimits(transactiondataobject.CreditAmount, transactiondataobject.CreditAmount, buyerCode);
                    transactionInvNum = CommonUtils.RandomString(6);
                    Assert.IsTrue(new DMSServices().CreateAuthorizationWithTransactionAmount(transactionInvNum, dealerCode, buyerCode, out string authCode, transactiondataobject.TransactionAmount));
                    break;
                case "Create Invoice":
                case "Negative AvailCredit on Create Inv":
                    CommonUtils.UpdateFleetCreditLimits(transactiondataobject.CreditAmount, transactiondataobject.CreditAmount, buyerCode);
                    transactionInvNum = CommonUtils.RandomString(6);
                    int quantity = 1;
                    Assert.IsTrue(new DMSServices().SubmitInvoiceWithTransactionAmount(transactionInvNum, dealerCode, buyerCode, transactionAmount, quantity));
                    break;
                case "Use Auth in Inv":
                    CommonUtils.UpdateFleetCreditLimits(transactiondataobject.CreditAmount, transactiondataobject.CreditAmount, transactiondataobject.BYR);
                    transactionInvNum = CommonUtils.RandomString(6);
                    TransactionDetails authDetails = CommonUtils.GetAuthTrasactionDetails(dealerCode, buyerCode);
                    string authTransactionNumber = authDetails.TransactionNumber;
                    authorizationCode = authDetails.AuthCode;
                    authTransactionAmount = CommonUtils.GetInvoiceTransactionAmount(authTransactionNumber);
                    Assert.IsTrue(new DMSServices().SubmitInvoiceWithAuthCode(transactionInvNum, dealerCode, buyerCode, transactionAmount, authorizationCode));
                    break;
                case "Invoice Reversal":
                    CommonUtils.UpdateFleetCreditLimits(transactiondataobject.CreditAmount, transactiondataobject.CreditAmount, transactiondataobject.BYR);
                    DealerInvoicesPage Page;
                    CreateNewInvoicePage CreateNewInvoicePage;
                    InvoiceOptionsPage InvoiceOptionPopUpPage;
                    Page = new DealerInvoicesPage(driver);
                    currentInvNum = FleetInvoicesUtils.GetCurrentInvoice(dealerCode, buyerCode);
                    currentInvTransactionAmount = CommonUtils.GetInvoiceTransactionAmount(currentInvNum);
                    Page.LoadDataOnGrid(currentInvNum);
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, currentInvNum);
                    Page.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                    InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
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
                case "Credit Limit is Increased":
                case "Credit Limit is Descreased":
                    CommonUtils.UpdateFleetCreditLimits(transactiondataobject.CreditAmount, transactiondataobject.CreditAmount, transactiondataobject.BYR);
                    UpdateCreditPage UpdateCreditPage;
                    UpdateCreditPage = new UpdateCreditPage(driver);
                    UpdateCreditPage.SwitchIframe();
                    UpdateCreditPage.SearchAndSelectValueAfterOpen(FieldNames.BillingLocation, transactiondataobject.BYR);
                    UpdateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, Convert.ToString(transactiondataobject.TransactionAmount));
                    UpdateCreditPage.Click(FieldNames.Update);
                    if (UpdateCreditPage.CheckForText("Please enter credit limit"))
                    {
                        UpdateCreditPage.EnterTextAfterClear(FieldNames.NewCreditLimit, Convert.ToString(transactiondataobject.TransactionAmount));
                        UpdateCreditPage.Click(FieldNames.Update);
                        //WaitForMsg(ButtonsAndMessages.UpdateCreditSuccessfully);

                    }
                    Assert.IsTrue(UpdateCreditPage.CheckForText(ButtonsAndMessages.UpdateCreditSuccessfully, true), "Credit Not Updated Sucessfully");
                    break;
                case "New BYR Created":
                    CreateNewEntityPage = new CreateNewEntityPage(driver);
                    menu = new Menu(driver);
                    string entityLocation = "Buyer Billing";
                    Task t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Fleet));
                    t.Wait();
                    t.Dispose();
                    t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.ClickElement(FieldNames.MasterLocation);
                    t.Wait();
                    t.Dispose();
                    CreateNewEntityPage.InputMandatoryFieldsFleet(entityLocation, fleetName);
                    CreateNewEntityPage.ButtonClick(ButtonsAndMessages.Save);
                    break;

            }
            stagingHistoryCountBeforeJobRun = CreditLimitStatusUtil.GetCreditStagingHistoryCount(buyerCode, CommonUtils.GetCommunityIDOfClient());


        }

        [When(@"Job ""([^""]*)"" is executed")]

        public void WhenTheJobIsExecuted(string jobName)
        {
            string serverLastRunTime;
            ClientConfiguration clients = ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            previousLastRunTime = CreditLimitStatusUtil.GetLastRunDateOfCreditLimitJob(CommonUtils.GetCommunityIDOfClient());

            if (clients.Client == "QA" || clients.Client == "FunctionalAutomation")
            {
                serverLastRunTime = JobHelper.GetJobLastRunTime(jobName, "c1qsrs01-uspa01").Value.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                serverLastRunTime = JobHelper.GetJobLastRunTime(jobName, "c1usrs01 -usea1a").Value.ToString("yyyy-MM-dd HH:mm");
            }
            Assert.IsNotNull(serverLastRunTime);
            Console.WriteLine(serverLastRunTime);
            DbLastRunTime = CreditLimitStatusUtil.GetLastRunDateOfCreditLimitJob(CommonUtils.GetCommunityIDOfClient());
            Assert.AreEqual(serverLastRunTime, DbLastRunTime, "Last Run is not updated after job execution");
        }

        [Then(@"Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table")]
        public void ThenUpdatedCreditAmountAvailableCreditAndTotalARValuesShouldBePlacedInStagingHistoryTable()
        {
            CreditStagingHistoryDetails creditstaginghistory = CreditLimitStatusUtil.GetCreditLimitDataFromStagingHistory(buyerCode, CommonUtils.GetCommunityIDOfClient());
            switch (Action)
            {
                case "Create Authorization":
                case "Create Invoice":
                case "Negative AvailCredit on Create Auth":
                case "Negative AvailCredit on Create Inv":
                case "Create Auth & Inactivate Account":
                case "Create Auth & Suspend Account":
                    availableCreditLimit = creditLimit - transactionAmount;
                    totalAR = creditLimit - availableCreditLimit;
                    Assert.AreEqual(creditLimit, creditstaginghistory.CreditLimit, "Credit Limit is Incorrect");
                    Assert.AreEqual(availableCreditLimit, creditstaginghistory.AvailableCreditLimit, "AvailableCredit Limit is Incorrect");
                    Assert.AreEqual(totalAR, creditstaginghistory.TotalAR, "TotalAR is Incorrect");
                    break;
                case "Credit Limit is Increased":
                case "Credit Limit is Descreased":
                    Assert.AreEqual(updatedCreditAmount, creditstaginghistory.CreditLimit, "Credit Limit is Incorrect");
                    Assert.AreEqual(updatedCreditAmount, creditstaginghistory.AvailableCreditLimit, "AvailableCredit Limit is Incorrect");
                    Assert.AreEqual(0, creditstaginghistory.TotalAR, "TotalAR is Incorrect");
                    break;
                case "Invoice Reversal":
                    availableCreditLimit = creditLimit + currentInvTransactionAmount;
                    totalAR = creditLimit - availableCreditLimit;
                    Assert.AreEqual(creditLimit, creditstaginghistory.CreditLimit, "Credit Limit is Incorrect");
                    Assert.AreEqual(availableCreditLimit, creditstaginghistory.AvailableCreditLimit, "AvailableCredit Limit is Incorrect");
                    Assert.AreEqual(totalAR, creditstaginghistory.TotalAR, "TotalAR is Incorrect");
                    break;
                case "New BYR Created":
                    CreditStagingHistoryDetails creditstaginghistory1 = CreditLimitStatusUtil.GetCreditLimitDataFromStagingHistory(fleetName, CommonUtils.GetCommunityIDOfClient());
                    Assert.AreEqual(55555555, creditstaginghistory1.CreditLimit, "Credit Limit is Incorrect");
                    Assert.AreEqual(55555555, creditstaginghistory1.AvailableCreditLimit, "AvailableCredit Limit is Incorrect");
                    Assert.AreEqual(0, creditstaginghistory1.TotalAR, "TotalAR is Incorrect");
                    break;
                case "Use Auth in Inv":
                    availableCreditLimit = creditLimit + (authTransactionAmount - transactionAmount);
                    totalAR = creditLimit - availableCreditLimit;
                    Assert.AreEqual(creditLimit, creditstaginghistory.CreditLimit, "Credit Limit is Incorrect");
                    Assert.AreEqual(availableCreditLimit, creditstaginghistory.AvailableCreditLimit, "AvailableCredit Limit is Incorrect");
                    Assert.AreEqual(totalAR, creditstaginghistory.TotalAR, "TotalAR is Incorrect");
                    break;
            }
            stagingHistoryCountAfterJobRun = CreditLimitStatusUtil.GetCreditStagingHistoryCount(buyerCode, CommonUtils.GetCommunityIDOfClient());
            Assert.IsTrue(stagingHistoryCountBeforeJobRun < stagingHistoryCountAfterJobRun, "Staging History Count before job run is equal after job run");
        }

        [Given(@"CreditLimitVarianceThreshHoldPct is set to ""([^""]*)""")]
        public void GivenCreditLimitVarianceThreshHoldPctIsSetTo(int percent)
        {
            CommonUtils.UpdateCreditLimitVarianceThreshHoldPct(percent, CommonUtils.GetCommunityIDOfClient());
        }

        [Then(@"Only Latest Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table")]
        public void ThenOnlyLatestUpdatedCreditAmountAvailableCreditAndTotalARValuesShouldBePlacedInStagingHistoryTable()
        {
            CreditStagingHistoryDetails creditstaginghistory = CreditLimitStatusUtil.GetCreditLimitDataFromStagingHistory(buyerCode, CommonUtils.GetCommunityIDOfClient());
            availableCreditLimit = creditLimit - transactionAmount;
            totalAR = creditLimit - availableCreditLimit;
            Assert.AreEqual(creditLimit, creditstaginghistory.CreditLimit, "Credit Limit is Incorrect");
            Assert.AreEqual(availableCreditLimit, creditstaginghistory.AvailableCreditLimit, "AvailableCredit Limit is Incorrect");
            Assert.AreEqual(totalAR, creditstaginghistory.TotalAR, "TotalAR is Incorrect");

        }

        [Given(@"Entity ""([^""]*)"" is set to ""([^""]*)""")]
        public void GivenBuyerIsSetTo(string buyerCode, string tokenstate)
        {
            switch (tokenstate)
            {
                case "InActive":
                    if (tokenstate == "Active")
                    {
                        CommonUtils.ToggleEntityActivationFlag(buyerCode, true);
                    }
                    else if (tokenstate == "InActive")
                    {
                        CommonUtils.ToggleEntityActivationFlag(buyerCode, false);
                    }
                    break;
                case "Terminated":
                    if (tokenstate == "Terminated")
                    {
                        CommonUtils.ToggleEntityTerminationFlag(buyerCode, true);
                    }
                    else if (tokenstate == "UnTerminated")
                    {
                        CommonUtils.ToggleEntityTerminationFlag(buyerCode, false);
                    }
                    break;
                case "Suspended":
                    if (tokenstate == "Suspended")
                    {
                        CommonUtils.ToggleEntitySuspensionRelationship(buyerCode, true);
                    }
                    else if (tokenstate == "UnSuspend")
                    {
                        CommonUtils.ToggleEntitySuspensionRelationship(buyerCode, false);
                    }
                    break;
            }
        }

    }
}
