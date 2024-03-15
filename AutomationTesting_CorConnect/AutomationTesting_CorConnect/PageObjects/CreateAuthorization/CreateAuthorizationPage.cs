using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.CreateAuthorization
{

    internal class CreateAuthorizationPage : StaticPage
    {
        internal CreateAuthorizationPage(IWebDriver webDriver) : base(webDriver, Pages.CreateAuthorization)
        {
        }

        internal void CreateAuthorization(string dealerCode, string fleetCode, out string transactionInvNum, out string authCode)
        {
            transactionInvNum = CommonUtils.RandomString(8) + CommonUtils.GetTime();
            SelectValueByScroll(FieldNames.TransactionType, "Service");
            EnterDateInInvoiceDate();
            EnterDealerCode(dealerCode);
            EnterFleetCode(fleetCode);
            ClickContinue();
            WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            EnterInvoiceAmount("50.00");
            EnterTextAfterClear(FieldNames.PurchaseOrderNumber, "po_1");
            EnterTextAfterClear(FieldNames.InvoiceNumber, transactionInvNum);
            EnterTextAfterClear(FieldNames.UnitNumber, "123");
            EnterTextAfterClear(FieldNames.VehicleID, "435");
            EnterTextAfterClear(FieldNames.VehicleYear, "1964");
            EnterTextAfterClear(FieldNames.VehicleLicense, "POU2456");
            ClickCreateAuthorization();
            Assert.IsTrue(IsElementVisibleOnScreen("Authorization Code Label"));
            authCode = GetText("Authorization Code Label");
            Assert.IsNotNull(authCode);
            Assert.AreNotEqual("Not Authorized", authCode);
        }
        
        internal void EnterDateInInvoiceDate()
        {
            string date = CommonUtils.GetCurrentDate();
            WaitForElementToBeClickable(FieldNames.InvoiceDate);
            EnterTextAfterClear(FieldNames.InvoiceDate, date);
            ClickTitle(FieldNames.Title);
        }

        internal void EnterDealerCode(string dealerCode)
        {
            SearchAndSelectValue(FieldNames.DealerCode, dealerCode);
            ClickTitle(FieldNames.Title);
            WaitForLoadingIcon();
        }

        internal void EnterFleetCode(string fleetCode)
        {
            SelectValueMultipleColumns(FieldNames.FleetCode, fleetCode);
            ClickTitle(FieldNames.Title);
            WaitForLoadingIcon();
        }

        internal void EnterInvoiceAmount(string amount)
        {
            EnterTextAfterClear(FieldNames.InvoiceAmount, amount);
            ClickTitle(FieldNames.Title);
            WaitForLoadingIcon();
        }

        internal void ClickContinue()
        {
            ButtonClick(ButtonsAndMessages.Continue);
            WaitForLoadingIcon();
        }

        internal void ClickCreateAuthorization()
        {
            ButtonClick(ButtonsAndMessages.CreateAuthorization);
            WaitForLoadingIcon();
        }
    }
}
