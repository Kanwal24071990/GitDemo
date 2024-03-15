using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.OpenAuthorizations.CreateNewAuthorization
{
    internal class CreateNewAuthorizationPopUpPage : PopUpPage
    {
        internal CreateNewAuthorizationPopUpPage(IWebDriver driver) : base(driver, Pages.CreateAuthorization)
        {
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
            SearchAndSelectValue(RenameMenuField(FieldNames.DealerCode), dealerCode);
            ClickTitle(FieldNames.Title);
            WaitForLoadingIcon();
        }

        internal void EnterFleetCode(string fleetCode)
        {
            SelectValueMultipleColumns(RenameMenuField(FieldNames.FleetCode), fleetCode);
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

        internal void ClickResubmitAuthorization()
        {
            ButtonClick(ButtonsAndMessages.ResubmitAuthorization);
            
        }

        internal void AcceptAlertMessage(out string msg)
        {
            try
            {
                AcceptAlert(out msg);
            }
            catch (UnhandledAlertException ex)
            {
                AcceptAlert(out msg);
            }
        }
    }
}
