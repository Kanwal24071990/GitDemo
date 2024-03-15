using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.FleetInvoices;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetInvoices;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.FleetInvoices
{
    [Binding]
    [Scope(Feature = "FleetInvoices")]
    internal class FleetInvoicesStepDefinitions : DriverBuilderClass
    {
        string dealerInvNum = CommonUtils.RandomString(6);
        string currentInvNum;
        FleetInvoicesPage FIPage;
        string InvoiceNumber1;
        string InvoiceNumber2;
        string batchID;
        InvoiceEntryPage InvoiceEntryPage;
        CreateNewInvoicePage CreateNewInvoicePage;


        [When(@"Advanced Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.SwitchToAdvanceSearch();
            FIPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            FIPage.LoadDataOnGrid();

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {
            if (FIPage.IsAnyDataOnGrid())
            {
                string gridCount = FIPage.GetPageCounterTotal();
                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = FleetInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = FleetInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = FleetInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = FleetInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = FleetInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = FleetInvoicesUtils.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }

        }

        [When(@"User navigates to Advanced Search")]
        public void WhenNavigatesToAdvancedSearch()
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.SwitchToAdvanceSearch();
        }

        [Then(@"The message ""([^""]*)"" is shown")]
        public void ThenTheMessageIsShown(string expectedLast185DaysMessage)
        {
            string actualLast185DaysMessage = FIPage.GetText(By.XPath("//table//span[contains(@id, 'AdvancedSearchNotes')]"));
            Assert.AreEqual(actualLast185DaysMessage, expectedLast185DaysMessage, ErrorMessages.MessageMismatch);

        }

        [When(@"User selects From date greater than 185 days on Advanced Search")]
        public void WhenUserSelectsFromDateGreaterThanDaysOnAdvancedSearch()
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.SwitchToAdvanceSearch();
            FIPage.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-185)));

        }

        [Then(@"The message Date Range cannot exceed 185 days is shown as a tooltip")]
        public void ThenTheMessageIsShownAsATooltip()
        {
            Assert.IsTrue(FIPage.IsErrorIconVisibleWithTitle(ButtonsAndMessages.DateRangeError), "No error displayed for date range exceeding 185 days.");

        }

        [Then(@"Error message ""([^""]*)"" should be displayed")]
        public void ThenErrorMessageShouldBeDisplayed(string alertMessage)
        {
            FIPage = new FleetInvoicesPage(driver);
            Assert.AreEqual(alertMessage, FIPage.GetWindowAlertMessage());
            FIPage.AcceptWindowAlert();
        }

        [Then(@"On Success the ""([^""]*)"" Status is Initiated and Paymeny Portal is launched")]
        public void ThenOnSuccessTheStatusIsInitiatedAndPaymenyPortalIsLaunched(string invoiceCount)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.WaitForLoadingIcon();
            Assert.IsFalse(FIPage.IsAlertPresent());
            FIPage.SwitchToPopUp();
            string url = "/payment/request/";
            Assert.IsTrue(FIPage.GetURL().Contains(url));
            batchID = FleetInvoicesUtils.GetBatchGuID(InvoiceNumber1);
            Assert.NotNull(batchID);
            Assert.AreEqual("Initiated", FleetInvoicesUtils.GetInvoiceStatus(batchID));
            if (invoiceCount == "Invoices")
            {
                batchID = FleetInvoicesUtils.GetBatchGuID(InvoiceNumber2);
                Assert.NotNull(batchID);
                Assert.AreEqual("initiated", FleetInvoicesUtils.GetInvoiceStatus(batchID));
            }
        }

        [Given(@"User Populates PayOnline Eligible Invoices from Past ""([^""]*)"" days with single Currency ""([^""]*)""")]
        public void GivenUserPopulatesPayOnlineEligibleInvoicesFromPastDaysWithSingleCurrency(int days, string Currecny)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.SwitchToAdvanceSearch();
            FIPage.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-days)));
            FIPage.SelectValueTableDropDown(FieldNames.PaymentMethod, "Online");
            FIPage.SelectValueMultiSelectDropDown(FieldNames.Currency, TableHeaders.Currency, Currecny);
            FIPage.LoadDataOnGrid();
        }

        [Given(@"Fresh Invoices are submitted from DMS with Buyer ""([^""]*)"" and Supplier ""([^""]*)""")]
        public void GivenFreshInvoicesAreSubmittedFromDMSWithBuyerAndSupplier(string Buyer, string Supplier)
        {
            new DMSServices().SubmitInvoiceWithTransactionAmount(dealerInvNum, Supplier, Buyer, 100, 1);
        }

        [Given(@"Invoice is submitted from DMS with Buyer ""([^""]*)"" Seller ""([^""]*)"" and Buyer ""([^""]*)"" Seller ""([^""]*)""")]
        public void GivenInvoiceIsSubmittedFromDMSWithBuyerSellerAndBuyerSeller(string Buyer1, string Supplier1, string Buyer2, string Supplier2)
        {
            new DMSServices().SubmitInvoiceWithTransactionAmount(dealerInvNum, Supplier1, Buyer1, 100, 1);
            currentInvNum = dealerInvNum;
            dealerInvNum = CommonUtils.RandomString(6);
            new DMSServices().SubmitInvoiceWithTransactionAmount(dealerInvNum, Supplier2, Buyer2, 100, 1);
        }

        [Given(@"Fresh Invoices are submitted from DMS having Transaction Type as ""([^""]*)""")]
        public void GivenFreshInvoicesAreSubmittedFromDMSHavingTransactionTypeAs(string invoiceType)
        {
            string Supplier = CommonUtils.GetDealerCodeforLocationType("Billing");
            string Buyer = CommonUtils.GetFleetCodeforLocationType("Billing");
            string description = null;
            switch (invoiceType)
            {
                case "Service":
                    description = "R";
                    break;
                case "Miscellaneous":
                    description = "M";
                    break;
                case "Fixed":
                    description = "X";
                    break;
            }
            new DMSServices().SubmitInvoiceWithTransactionAmountAndType(dealerInvNum, Supplier, Buyer, 100, 1, description);
        }

        [When(@"User Populates Grid and Initiates Payment for invoice belong to Buyer ""([^""]*)"" and ""([^""]*)""")]
        public void WhenUserPopulatesGridAndInitiatesPaymentForInvoiceBelongToBuyerAnd(string Buyer1, string Buyer2)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.SwitchToAdvanceSearch();
            FIPage.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-100)));
            FIPage.SelectValueTableDropDown(FieldNames.PaymentMethod, "Online");
            FIPage.LoadDataOnGrid();
            FIPage.WaitForGridLoad();
            FIPage.FilterTable(TableHeaders.FleetCode, Buyer1);
            FIPage.FilterTable(TableHeaders.DealerInvhash, currentInvNum);
            InvoiceNumber1 = FIPage.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
            FIPage.CheckTableRowCheckBoxByIndex(1);
            FIPage.FilterTable(TableHeaders.DealerInvhash, dealerInvNum);
            FIPage.FilterTable(TableHeaders.FleetCode, Buyer2);
            InvoiceNumber2 = FIPage.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
            FIPage.CheckTableRowCheckBoxByIndex(1);
            FIPage.ClickGridButtons(ButtonsAndMessages.PayInvoices);
        }

        [When(@"User Populates Grid and Initiates Payment for invoice belong to Buyer")]
        public void WhenUserPopulatesGridAndInitiatesPaymentForInvoiceBelongToBuyer()
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.SwitchToAdvanceSearch();
            FIPage.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-100)));
            FIPage.SelectValueTableDropDown(FieldNames.PaymentMethod, "Online");
            FIPage.LoadDataOnGrid();
            FIPage.FilterTable(TableHeaders.DealerInvhash, dealerInvNum);
            InvoiceNumber1 = FIPage.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
            FIPage.CheckTableRowCheckBoxByIndex(1);
            FIPage.ClickGridButtons(ButtonsAndMessages.PayInvoices);
        }

        [When(@"User initiates Payment for invoices belong to Buyer ""([^""]*)"" and ""([^""]*)""")]
        public void WhenUserInitiatesPaymentForInvoicesBelongToBuyerAnd(string Buyer1, string Buyer2)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.WaitForLoadingMessage();
            FIPage.FilterTable(TableHeaders.FleetCode, Buyer1);
            FIPage.CheckTableRowCheckBoxByIndex(1);
            FIPage.FilterTable(TableHeaders.FleetCode, Buyer2);
            FIPage.WaitForGridLoad();
            FIPage.FilterTable(TableHeaders.DealerInvhash, dealerInvNum);
            InvoiceNumber1 = FIPage.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
            FIPage.CheckTableRowCheckBoxByIndex(1);
            FIPage.ClickGridButtons(ButtonsAndMessages.PayInvoices);
        }

        [When(@"User Populates Grid and Initiates Payment for invoice belong to Buyer ""([^""]*)""")]
        public void WhenUserPopulatesGridAndInitiatesPaymentForInvoiceBelongToBuyer(string Buyer)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.SwitchToAdvanceSearch();
            FIPage.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-100)));
            FIPage.SelectValueTableDropDown(FieldNames.PaymentMethod, "Online");
            FIPage.LoadDataOnGrid();
            FIPage.WaitForGridLoad();
            FIPage.FilterTable(TableHeaders.FleetCode, Buyer);
            FIPage.FilterTable(TableHeaders.DealerInvhash, dealerInvNum);
            InvoiceNumber1 = FIPage.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
            FIPage.CheckTableRowCheckBoxByIndex(1);
            FIPage.ClickGridButtons(ButtonsAndMessages.PayInvoices);
        }

        [When(@"User initiates Payment for invoices belong to Single Currency ""([^""]*)""")]
        public void WhenUserInitiatesPaymentForInvoicesBelongToSingleCurrency(string currecny)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.FilterTable(TableHeaders.Currency, currecny);
            FIPage.FilterTable(TableHeaders.TransactionStatus, "Current");
            if (FIPage.IsAnyDataOnGrid())
            {
                InvoiceNumber1 = FIPage.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
                FIPage.CheckTableRowCheckBoxByIndex(1);
                FIPage.ClickGridButtons(ButtonsAndMessages.PayInvoices);
            }
        }

        [Given(@"Fresh Invoices are submitted from DMS with Buyer having Location Type as ""([^""]*)""")]
        public void GivenFreshInvoicesAreSubmittedFromDMSWithBuyerHavingLocationTypeAs(string locationType)
        {
            string Supplier = CommonUtils.GetDealerCodeforLocationType(locationType);
            string Buyer = CommonUtils.GetFleetCodeforLocationType(locationType);
            new DMSServices().SubmitInvoiceWithTransactionAmount(dealerInvNum, Supplier, Buyer, 100, 1);
        }

        [When(@"User initiates Payment for invoices belong to Buyer ""([^""]*)""")]
        public void WhenUserInitiatesPaymentForInvoicesBelongToBuyer(string Buyer1)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.WaitForGridLoad();
            FIPage.FilterTable(TableHeaders.FleetCode, Buyer1);
            InvoiceNumber1 = FIPage.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
            FIPage.CheckTableRowCheckBoxByIndex(1);
            FIPage.ClickGridButtons(ButtonsAndMessages.PayInvoices);
        }

        [When(@"User initiates Payment for Invoices with Transaction Status ""([^""]*)""")]
        public void WhenUserInitiatesPaymentForInvoicesWithTransactionStatus(string status)
        {
            FIPage = new FleetInvoicesPage(driver);
            FIPage.WaitForLoadingMessage();
            FIPage.FilterTable(TableHeaders.TransactionStatus, status);
            FIPage.CheckTableRowCheckBoxByIndex(1);
            FIPage.ClickGridButtons(ButtonsAndMessages.PayInvoices);
        }

        [Then(@"Statues ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" should be visible under Transaction Status Dropdown")]
        public void ThenStatuesShouldBeVisibleUnderTransactionStatusDropdown(string status1, string status2, string status3, string status4, string status5, string status6, string status7, string status8, string status9)
        {
            FIPage = new FleetInvoicesPage(driver);
            Assert.IsTrue(FIPage.VerifyValueMultiSelectDropDown(FieldNames.TransactionStatus, status1, status2, status3, status4, status5, status6, status7, status8, "Paid-Closed", "Paid-Disputed", "Paid-Resolved", status9, "Past due-Closed", "Past due-Disputed", "Past due-Hold", "Past due-Hold Released", "Past due-Resolved"));
        }

        [When(@"User Searches by Dealer Invoice Number ""([^""]*)""")]
        public void WhenUserSearchesByDealerInvoiceNumber(string invoiceNumber)
        {

            FIPage = new FleetInvoicesPage(driver);
            FleetInvoicesUtils.GetInvoiceDate(invoiceNumber, out string invoiceDate);
            FIPage.LoadDataOnGridWithProgramInvoiceNumber(invoiceNumber, invoiceDate);
        }

        [Then(@"Inovice Transaction status should be ""([^""]*)""")]
        public void ThenInoviceTransactionStatusShouldBe(string Status)
        {
            FIPage.WaitForLoadingMessage();
            string result = FIPage.GetFirstRowData(TableHeaders.TransactionStatus);
            Assert.AreEqual(Status, result);
        }
    }
}
