using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.PageObjects.FeeManagement
{
    internal class FeeManagementAspx : PopUpPage
    {
        internal FeeManagementAspx(IWebDriver webDriver) : base(webDriver, Pages.FeeManagementAspx)
        {
        }

        internal List<string> VerifyFormFields()
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.FeeName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.TextBoxMissing, FieldNames.FeeName));
            }
            if (!IsElementDisplayed(FieldNames.Description))
            {
                errorMsgs.Add(string.Format(ErrorMessages.TextBoxMissing, FieldNames.Description));
            }
            if (!IsElementDisplayed(FieldNames.DisplayName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.TextBoxMissing, FieldNames.DisplayName));
            }
            if (!IsElementDisplayed(FieldNames.EffectiveDate))
            {
                errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, FieldNames.EffectiveDate));
            }
            if (!IsElementDisplayed(FieldNames.EndDate))
            {
                errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, FieldNames.EndDate));
            }
            if (!IsDropDownClosed(FieldNames.Currency))
            {
                errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, FieldNames.Currency));
            }
            if (!IsDropDownClosed(FieldNames.InvoiceType))
            {
                errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, FieldNames.InvoiceType));
            }
            if (!IsMultiSelectDropDownClosed(FieldNames.TransactionType))
            {
                errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, FieldNames.TransactionType));
            }
            if (!IsDropDownClosed(FieldNames.FeeValueType))
            {
                errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, FieldNames.FeeValueType));
            }
            if (!IsElementDisplayed(FieldNames.FeeValue))
            {
                errorMsgs.Add(string.Format(ErrorMessages.TextBoxMissing, FieldNames.FeeValue));
            }
            if (!IsElementDisplayed(FieldNames.MinInvoiceAmount))
            {
                errorMsgs.Add(string.Format(ErrorMessages.TextBoxMissing, FieldNames.MinInvoiceAmount));
            }
            if (!IsElementDisplayed(FieldNames.MaxInvoiceAmount))
            {
                errorMsgs.Add(string.Format(ErrorMessages.TextBoxMissing, FieldNames.MaxInvoiceAmount));
            }
            if (!IsDropDownClosed(FieldNames.FeeChargedTo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, FieldNames.FeeChargedTo));
            }
            if (!IsDropDownClosed(FieldNames.SubCommunity))
            {
                errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, FieldNames.SubCommunity));
            }
            if (!IsElementDisplayed(FieldNames.Assigned))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Assigned));
            }
            if (!IsElementDisplayed(FieldNames.Unassigned))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Unassigned));
            }
            if (!IsButtonVisible(ButtonsAndMessages.Save))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
            }
            if (!IsButtonVisible(ButtonsAndMessages.Cancel))
            {
                errorMsgs.Add(ErrorMessages.CancelButtonMissing);
            }
            if (!IsElementVisible(FieldNames.Assigned + "_ExportBtn") || !IsElementVisible(FieldNames.Unassigned + "_ExportBtn"))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Export));
            }

            return errorMsgs;
        }
    }
}
