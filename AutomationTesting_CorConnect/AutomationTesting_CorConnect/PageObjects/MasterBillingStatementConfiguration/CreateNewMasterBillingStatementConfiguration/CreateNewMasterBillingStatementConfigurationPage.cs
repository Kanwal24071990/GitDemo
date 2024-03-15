using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.MasterBillingStatementConfiguration.CreateNewMasterBillingStatementConfiguration
{
    internal class CreateNewMasterBillingStatementConfigurationPage : PopUpPage
    {
        internal CreateNewMasterBillingStatementConfigurationPage(IWebDriver driver) : base(driver, Pages.CreateMasterBillingStatementConfiguration)
        {
        }

        internal List<string> CreateNewStatementConfiguration(string communityStatementName)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                SelectValueRowByIndex(FieldNames.Fleet, 2, null, true);
                if (IsMultipleColumnDropDownEnabled(FieldNames.FleetGroup))
                {
                    SelectValueFirstRow(FieldNames.FleetGroup);
                }
                SelectFirstRowMultiSelectDropDown(FieldNames.Dealer);
                if (IsMultipleColumnDropDownEnabled(FieldNames.DealerGroup))
                {
                    SelectValueFirstRow(FieldNames.DealerGroup);
                }
                SelectFirstRowMultiSelectDropDown(FieldNames.InvoiceType);
                SelectValueRowByIndex(FieldNames.StatementType, 1);
                WaitForLoadingIcon();
                EnterTextAfterClear(FieldNames.StartDate, CommonUtils.GetCurrentDate());
                SelectValueRowByIndex(FieldNames.PaymentTerms, 1);
                WaitForLoadingIcon();
                SelectValueRowByIndex(FieldNames.BillingCycle, 1);
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
                EnterTextAfterClear(FieldNames.CommunityStatementName, communityStatementName);
                EnterTextAfterClear(FieldNames.DealerAccountingCode, communityStatementName);
                if (!IsButtonVisible(ButtonsAndMessages.Save))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Save));
                }

                if (!IsButtonVisible(ButtonsAndMessages.Cancel))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                }

                if (IsButtonVisible(ButtonsAndMessages.Activate))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Activate));
                }

                if (IsButtonVisible(ButtonsAndMessages.Deactivate))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Deactivate));
                }

                ButtonClick(ButtonsAndMessages.Save);
                WaitForLoadingIcon();
                string msg = GetPerformedOperationMsg();
                if (msg != ButtonsAndMessages.Recordsavedsuccessfully)
                {
                    errorMsgs.Add(ErrorMessages.SaveOperationFailed + string.Format(LoggerMesages.ExpectedMessage, ButtonsAndMessages.Recordsavedsuccessfully, msg));
                }

                if (!IsButtonVisible(ButtonsAndMessages.Activate))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Activate));
                }

                ButtonClick(ButtonsAndMessages.Activate);
                WaitForLoadingIcon();
                msg = GetPerformedOperationMsg();
                if (msg != ButtonsAndMessages.ActivatedSuccessfully)
                {
                    errorMsgs.Add(ErrorMessages.ActivationFailed + string.Format(LoggerMesages.ExpectedMessage, ButtonsAndMessages.ActivatedSuccessfully, msg));
                }

                ButtonClick(ButtonsAndMessages.Cancel);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> CreateStatementConfiguration(string fleet, string dealer, string dealerAccountingCode, string communityStatementName)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                SearchAndSelectValue(FieldNames.Fleet, fleet);
                SelectFirstRowMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealer);
                SelectFirstRowMultiSelectDropDown(FieldNames.InvoiceType);
                SelectValueTableDropDown(FieldNames.StatementType, "One statement per due date");
                WaitForLoadingIcon();
                EnterTextAfterClear(FieldNames.StartDate, CommonUtils.GetCurrentDate());
                SelectValueRowByIndex(FieldNames.PaymentTerms, 1);
                WaitForLoadingIcon();
                SelectValueTableDropDown(FieldNames.BillingCycle, "Bi-Weekly");
                WaitForLoadingIcon();
                SelectValueFirstRow(FieldNames.StatementEndDay);
                EnterTextAfterClear(FieldNames.CommunityStatementName, communityStatementName);
                EnterTextAfterClear(FieldNames.DealerAccountingCode, dealerAccountingCode);
                if (!IsButtonVisible(ButtonsAndMessages.Save))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Save));
                }

                if (!IsButtonVisible(ButtonsAndMessages.Cancel))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                }

                if (IsButtonVisible(ButtonsAndMessages.Activate))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Activate));
                }

                if (IsButtonVisible(ButtonsAndMessages.Deactivate))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Deactivate));
                }
      
                ButtonClick(ButtonsAndMessages.Save);
                WaitForLoadingIcon();
                string msg = GetPerformedOperationMsg();
                if (msg != ButtonsAndMessages.Recordsavedsuccessfully)
                {
                    errorMsgs.Add(ErrorMessages.SaveOperationFailed + string.Format(LoggerMesages.ExpectedMessage, ButtonsAndMessages.Recordsavedsuccessfully, msg));
                }

                if (!IsButtonVisible(ButtonsAndMessages.Activate))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Activate));
                }

                ButtonClick(ButtonsAndMessages.Activate);
                WaitForLoadingIcon();
                msg = GetPerformedOperationMsg();
                if (msg != ButtonsAndMessages.ActivatedSuccessfully)
                {
                    errorMsgs.Add(ErrorMessages.ActivationFailed + string.Format(LoggerMesages.ExpectedMessage, ButtonsAndMessages.ActivatedSuccessfully, msg));
                }
                
                ButtonClick(ButtonsAndMessages.Cancel);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.ToString());
                errorMsgs.Add(ex.ToString());
            }
            return errorMsgs;
        }

        internal List<string> DeactivateStatement(string communityStatementName)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                WaitForLoadingMessage();
                if (GetValue(FieldNames.CommunityStatementName).Trim() != communityStatementName)
                {
                    ButtonClick(ButtonsAndMessages.Cancel);
                    throw new Exception(ErrorMessages.ValueMisMatch);
                }

                if (IsButtonVisible(ButtonsAndMessages.Deactivate))
                {
                    ButtonClick(ButtonsAndMessages.Deactivate);
                }
                else
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Deactivate));
                }
                AcceptAlert(out string alertMsg);
                WaitForLoadingIcon();
                string msg = GetPerformedOperationMsg();
                if (msg != ButtonsAndMessages.DeactivatedSuccessfully)
                {
                    errorMsgs.Add(ErrorMessages.DeactivationFailed + string.Format(LoggerMesages.ExpectedMessage, ButtonsAndMessages.DeactivatedSuccessfully, msg));
                }

                if (IsButtonVisible(ButtonsAndMessages.Deactivate))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Deactivate));
                }

                ButtonClick(ButtonsAndMessages.Cancel);
                
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> VerifyCreateFields()
        {
            List<string> errorMsgs = new List<string>();
            string requiredLabel = " Required Label";

            if (!IsElementDisplayed(FieldNames.Fleet + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.Fleet)); ;
            }
            if (!IsElementDisplayed(FieldNames.Dealer + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.Dealer));
            }
            if (!IsElementDisplayed(FieldNames.FleetGroup + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FleetGroup));
            }
            if (!IsElementDisplayed(FieldNames.DealerGroup + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.DealerGroup));
            }
            if (!IsElementDisplayed(FieldNames.InvoiceType + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.InvoiceType));
            }
            if (!IsElementDisplayed(FieldNames.StatementType + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.StatementType));
            }
            if (!IsElementDisplayed(FieldNames.StartDate + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.StartDate));
            }
            if (!IsElementDisplayed(FieldNames.PaymentTerms + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.PaymentTerms));
            }
            if (!IsElementDisplayed(FieldNames.BillingCycle + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.BillingCycle));
            }
            if (!IsElementDisplayed(FieldNames.CommunityStatementName + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldDisplayed, FieldNames.CommunityStatementName));
            }
            if (!IsButtonEnabled(ButtonsAndMessages.Save))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Save));
            }
            if (!IsButtonEnabled(ButtonsAndMessages.Cancel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
            }
            if (IsButtonEnabled(ButtonsAndMessages.Activate))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Activate));
            }
            if (IsButtonEnabled(ButtonsAndMessages.Deactivate))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Deactivate));
            }
            return errorMsgs;
        }

    }
}
