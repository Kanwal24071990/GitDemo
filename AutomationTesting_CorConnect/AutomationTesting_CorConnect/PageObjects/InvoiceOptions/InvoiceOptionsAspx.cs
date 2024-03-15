using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceOptions
{
    internal class InvoiceOptionsAspx : PopUpPage
    {
 internal InvoiceOptionsAspx(IWebDriver driver) : base(driver, Pages.InvoiceOptionsAspx)
        {

        }
        internal List<string> VerifyDisputeInfoFields()
        {
            List<string> errorMsgs = new List<string>();

           if (!IsElementDisplayed(FieldNames.InvoiceNumberLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceNumberLabel));
            } 
            if (!IsElementDisplayed(FieldNames.Company))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Company));
            }
            if (!IsElementDisplayed(FieldNames.Name))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Name));
            }
            if (!IsElementDisplayed(FieldNames.Phone))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Phone));
            }
            if (!IsElementDisplayed(FieldNames.Email_Cc))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email_Cc));
            }
            if (!IsElementDisplayed(FieldNames.Reason))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Reason));
            }
            if (!IsElementDisplayed(FieldNames.Notes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Notes));
            }
            if (!IsElementDisplayed(FieldNames.Uploadfile))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Uploadfile));
            }

            return errorMsgs;
        }

        internal List<string> VerifyUpdateDisputeInfoFields()
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.InvoiceNumberLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceNumberLabel));
            }
            if (!IsElementDisplayed(FieldNames.Company))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Company));
            }
            if (!IsElementDisplayed(FieldNames.Email_Cc))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email_Cc));
            }
            if (!IsElementDisplayed(FieldNames.Reason))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Reason));
            }
           /* if (!IsElementDisplayed(FieldNames.Who_Was_Paid))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Who_Was_Paid));
            }
            if (!IsElementDisplayed(FieldNames.Date_Paid))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Date_Paid));
            }
            if (!IsElementDisplayed(FieldNames.Paid_By_Check_Number))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Paid_By_Check_Number));
            }  */
            if (!IsElementDisplayed(FieldNames.Notes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Notes));
            }
            if (!IsElementDisplayed(FieldNames.Uploadfile))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Uploadfile));
            }

            return errorMsgs;
        }

        internal List<string> VerifyDisputeResolutionFields()
        {
            List<string> errorMsgs = new List<string>();
            string userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();





            if (!IsElementDisplayed(FieldNames.DisputeResolutionInvoiceNumberLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisputeResolutionInvoiceNumberLabel));
            }
            if (!IsElementDisplayed(FieldNames.Status_Label))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Status_Label));
            }
            
            if (!IsElementDisplayed(FieldNames.DisputedByLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisputedByLabel));
            }
            if (!IsElementDisplayed(FieldNames.ReasonLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ReasonLabel));
            }
            if (!IsElementDisplayed(FieldNames.ReasonDetailLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ReasonDetailLabel));
            }
            if (!IsElementDisplayed(FieldNames.DisputeNoteLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisputeNoteLabel));
            }

            if (userType == "ADMIN")
            {
                if (!IsElementDisplayed(FieldNames.PendingActionBy))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PendingActionBy));
                }
                if (!IsElementDisplayed(FieldNames.OwnedBy))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.OwnedBy));
                }
                if (!IsElementDisplayed(FieldNames.FollowUpBy))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FollowUpBy));
                }
                if (!IsElementDisplayed(FieldNames.NotifyAndVisibleToFleet))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.NotifyAndVisibleToFleet));
                }
                if (!IsElementDisplayed(FieldNames.NotifyAndVisibleToDealer))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.NotifyAndVisibleToDealer));
                }
            }
            if (userType == "ADMIN" || userType == "FLEET")
            {
                if (!IsElementDisplayed(FieldNames.Uploadfile))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Uploadfile));
                }
                if (!IsElementDisplayed(FieldNames.Action))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Action));
                }
                if (!IsElementDisplayed(FieldNames.NotesRequiredLabel))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.NotesRequiredLabel));
                }
            }
            return errorMsgs;
        }

        internal List<string> VerifyReDisputeFields()
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.InvoiceNumberLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceNumberLabel));
            }
            if (!IsElementDisplayed(FieldNames.Company))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Company));
            }
            if (!IsElementDisplayed(FieldNames.Name))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Name));
            }
            if (!IsElementDisplayed(FieldNames.Phone))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Phone));
            }
            if (!IsElementDisplayed(FieldNames.Email_Cc))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email_Cc));
            }
            if (!IsElementDisplayed(FieldNames.Reason))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Reason));
            }
            if (!IsElementDisplayed(FieldNames.Notes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Notes));
            }
            if (!IsElementDisplayed(FieldNames.Uploadfile))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Uploadfile));
            }
        /*    if (!IsElementDisplayed(FieldNames.Dispute_Acknowledge))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Dispute_Acknowledge));
            }
        */

            return errorMsgs;
        }

    }
}
