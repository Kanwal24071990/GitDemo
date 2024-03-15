using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice
{
    internal class CreateNewInvoicePage : PopUpPage
    {
        public CreateNewInvoicePage(IWebDriver webDriver) : base(webDriver, "Create New Invoice")
        {
        }


        internal void PopulateInvoice(string dealerCode, string fleetCode, out string dealerInvoiceNumber)
        {
            dealerInvoiceNumber = CommonUtils.RandomString(11);
            SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealerCode);
            SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleetCode);
            EnterText("Dealer Invoice Number", dealerInvoiceNumber);
            EnterText("Invoice Date", CommonUtils.GetCurrentDate());
            Click("Save and Continue");
            WaitForLoadingGrid();
        }

        public void PopulateInvoiceWithHeader(string dealerCode, string fleetCode, out string dealerInvoiceNumber)
        {
            PopulateInvoice(dealerCode, fleetCode, out dealerInvoiceNumber);
            WaitForDisabledClass("Country_InputField");
            Click("Save and Continue");
            WaitForLoadingGrid();
        }

        internal string GetCountry()
        {
            return GetValue(GetElement( "Country_InputField"));
        }

        internal string GetState()
        {
            return GetValue(GetElement("State/Province_InputField"));
        }

        internal void ClickSameAsDealerAddress()
        {
            WaitForDisabledClass("Country_InputField");
            Click("Same as Dealer Address");
            WaitForLoadingGrid();
        }

        internal void SelectLineItemValue(string Item)
        {
            WaitForClassChange("Note", "dxh0");
            SelectValueMultipleColumns("Item", Item);
            Click("Add Line Item");
            WaitForLoadingGrid();
        }

        internal void SubmitInvoice(out string alertMessage)
        {
            Click("Submit Invoice");
            WaitForLoadingGrid();
            Click("Continue");
            AcceptAlert(out alertMessage);
        }

        internal InvoiceHistory ClickInvoiceHistory()
        {
            Click(ButtonsAndMessages.InvoiceHistory);
            SwitchToPopUp();
            return new InvoiceHistory(driver);
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

        internal List<string> CreateNewInvoice(string fleet, string dealer, string dealerInvNum , string InvoiceType = "Parts")
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                if (GetValueOfDropDown(FieldNames.InvoiceType) != InvoiceType)
                {
                    SelectValueByScroll(FieldNames.InvoiceType, InvoiceType);
                }

                SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealer);
                var entityDetails = CommonUtils.GetEntityDetails(dealer);
                SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.DealerInvoiceNumber, dealerInvNum);
                SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleet);
                var entityDetailsFleet = CommonUtils.GetEntityDetails(fleet);
                SetDropdownTableSelectInputValue(FieldNames.FleetCode, entityDetailsFleet.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.InvoiceDate, CommonUtils.GetCurrentDate());
                
                Click(ButtonsAndMessages.SaveAndContinue);
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("INVOICE_Save and Continue");
                Click("INVOICE_Save and Continue");
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("Type");
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.ToString());
                errorMsgs.Add(ex.ToString());
            }
            return errorMsgs;
        }

        internal List<string> CreateNewInvoiceWithLineItems(string fleet, string dealer, string dealerInvNum, string InvoiceType = "Parts")
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                if (GetValueOfDropDown(FieldNames.InvoiceType) != InvoiceType)
                {
                    SelectValueByScroll(FieldNames.InvoiceType, InvoiceType);
                }

                SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealer);
                var entityDetails = CommonUtils.GetEntityDetails(dealer);
                SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.DealerInvoiceNumber, dealerInvNum);
                SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleet);
                var entityDetailsFleet = CommonUtils.GetEntityDetails(fleet);
                SetDropdownTableSelectInputValue(FieldNames.FleetCode, entityDetailsFleet.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.InvoiceDate, CommonUtils.GetCurrentDate());
                Click(ButtonsAndMessages.SaveAndContinue);
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("INVOICE_Save and Continue");
                Click("INVOICE_Save and Continue");
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("Type");
                SelectValueByScroll(FieldNames.Type, "Rental");
                WaitForLoadingIcon();
                EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
                EnterText(FieldNames.ItemDescription, "Rental Auto description");
                SetValue(FieldNames.UnitPrice, "100.0000");
                Click(ButtonsAndMessages.SaveLineItem);
                WaitForLoadingIcon();
                Click(ButtonsAndMessages.SubmitInvoice);
                WaitForLoadingIcon();
                Click(ButtonsAndMessages.Continue);
                AcceptAlert(out string invoiceMsg1);
                Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.ToString());
                errorMsgs.Add(ex.ToString());
            }
            return errorMsgs;
        }

        internal List<string> CreateNewCreditInvoiceWithLineItems(string fleet, string dealer, string dealerInvNum, string InvoiceType = "Parts", string credit = "100.0000")
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                if (GetValueOfDropDown(FieldNames.InvoiceType) != InvoiceType)
                {
                    SelectValueByScroll(FieldNames.InvoiceType, InvoiceType);
                }

                SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealer);
                var entityDetails = CommonUtils.GetEntityDetails(dealer);
                SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.DealerInvoiceNumber, dealerInvNum);
                SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleet);
                var entityDetailsFleet = CommonUtils.GetEntityDetails(fleet);
                SetDropdownTableSelectInputValue(FieldNames.FleetCode, entityDetailsFleet.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.InvoiceDate, CommonUtils.GetCurrentDate());
                Click(ButtonsAndMessages.SaveAndContinue);
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("INVOICE_Save and Continue");
                Click("INVOICE_Save and Continue");
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("Type");
                SelectValueByScroll(FieldNames.Type, "Rental");
                WaitForLoadingIcon();
                EnterText(FieldNames.Item, "Rental Auto", TableHeaders.New);
                EnterText(FieldNames.ItemDescription, "Rental Auto description");
                SetValue(FieldNames.UnitPrice, credit);
                Click(ButtonsAndMessages.SaveLineItem);
                WaitForLoadingIcon();
                Click(ButtonsAndMessages.SubmitInvoice);
                WaitForLoadingIcon();
                Click(ButtonsAndMessages.Continue);
                AcceptAlert(out string invoiceMsg1);
                Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg1);

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.ToString());
                errorMsgs.Add(ex.ToString());
            }
            return errorMsgs;
        }


        internal List<string> CreateNewAuthInvoice(string fleet, string dealer, string authCode, string dealerInvNum)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                if (GetValueOfDropDown(FieldNames.InvoiceType) != "Parts")
                {
                    SelectValueByScroll(FieldNames.InvoiceType, "Parts");
                }

                SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealer);
                var entityDetails = CommonUtils.GetEntityDetails(dealer);
                SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.DealerInvoiceNumber, dealerInvNum);
                SearchAndSelectValue(FieldNames.OpenAuthorization, authCode);
                WaitForLoadingIcon();
                string fleetPopulated =  GetValueOfDropDown(FieldNames.FleetCode);
                if (fleetPopulated != fleet)
                {
                    errorMsgs.Add("Error Occured While Creating Auth Invoice");
                    
                }
                EnterTextAfterClear(FieldNames.InvoiceDate, CommonUtils.GetCurrentDate());
                Click(ButtonsAndMessages.SaveAndContinue);
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("INVOICE_Save and Continue");
                Click("INVOICE_Save and Continue");
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("Type");

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.ToString());
                errorMsgs.Add(ex.ToString());
            }
            return errorMsgs;
        }
        internal bool IsEachElementDisabled(params string[] captions)
        {
            foreach (string caption in captions)
            {
                if (!FindElements(caption).All(x => x.GetAttribute("class").Contains("Disabled")))
                {
                    return false;
                }
            }
            return true;
        }
      
    }
}
    