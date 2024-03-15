using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancyHistory;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Tests.InvoiceDiscrepancyHistory;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceDiscrepancyHistory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.InvoiceDiscrepancyHistory
{
    [Binding]
    internal class InvoiceDiscrepancyHistoryStepDefinitions : DriverBuilderClass
    {
        InvoiceDiscrepancyHistoryPage Page;
        string dealerInv = CommonUtils.RandomString(6);



        [When(@"User select ""([^""]*)"" invoice to move out of history")]
        public void WhenUserSelectInvoicesToMoveOutOfHistory(string expiration)
        {
            Page = new InvoiceDiscrepancyHistoryPage(driver);
            switch (expiration)
            {
                case "Non-Expired":
                    Page.LoadDataOnGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-60)), CommonUtils.GetCurrentDate());
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, dealerInv);
                    Page.CheckTableRowCheckBoxByIndex(1);
                    Page.ButtonClick(ButtonsAndMessages.MoveOutofHistory);
                    break;
                case "Expired":
                    InvoiceDiscrepancyHistoryUtils.UpdateInvoiceToExpire(dealerInv);
                    Page.LoadDataOnGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-60)), CommonUtils.GetCurrentDate());
                    Page.SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                    Page.FilterTable(TableHeaders.DealerInv__spc, dealerInv);
                    Page.CheckTableRowCheckBoxByIndex(1);
                    Page.ButtonClick(ButtonsAndMessages.MoveOutofHistory);
                    break;
            }
        }

        [Then(@"""([^""]*)"" Invoice should be successfully move out of history")]
        public void ThenInvoiceShouldBeSuccessfullyMoveOutOfHistory(string expirationDate)
        {
            Page.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.MoveOutOfHistorySuccess, invoiceMsg);

            switch (expirationDate)
            {
                case "Non-Expired":
                    Assert.IsFalse(InvoiceDiscrepancyHistoryUtils.ValidateInvoiceMovedFromHistory(dealerInv));
                    Assert.IsFalse(InvoiceDiscrepancyHistoryUtils.ValidateInvoiceInHistory(dealerInv));
                    break;
                case "Expired":
                    Assert.IsTrue(InvoiceDiscrepancyHistoryUtils.ValidateInvoiceMovedFromHistory(dealerInv));
                    Assert.IsFalse(InvoiceDiscrepancyHistoryUtils.ValidateInvoiceInHistory(dealerInv));
                    break;

            }

        }

        [Given(@"Invoice exist in system with ""([^""]*)"" Buyer and ""([^""]*)"" Supplier with ""([^""]*)"" Discrepancy")]
        public void GivenInvoiceIsSubmittedFromDMSWithBuyerAndSupplierWithDiscrepancy(string fleetName, string dealerName, string discrepancyState)
        {
            switch (discrepancyState)
            {
                case "Not in balance":
                    string invNo = InvoiceDiscrepancyHistoryUtils.GetInvoiceNotInBalance(dealerName);
                    if (string.IsNullOrEmpty(invNo))
                    {
                        new DMSServices().SubmitInvoiceNotInBalance(dealerInv, dealerName, fleetName);
                    }
                    else
                    {
                        dealerInv = invNo;
                    }

                    break;
            }

        }

        [Given(@"Invoice is in history")]
        public void GivenInvoiceIsInHistory()
        {
            if (InvoiceDiscrepancyHistoryUtils.ValidateInvoiceInHistory(dealerInv) == false)
            {
                CommonUtils.MoveInvoiceToHistory(dealerInv);
            }
        }


    }
}
