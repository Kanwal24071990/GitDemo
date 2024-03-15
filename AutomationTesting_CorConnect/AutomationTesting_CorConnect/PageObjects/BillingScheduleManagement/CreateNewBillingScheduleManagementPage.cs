using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.BillingScheduleManagement
{
    internal class CreateNewBillingScheduleManagementPage : PopUpPage
    {
        internal CreateNewBillingScheduleManagementPage(IWebDriver driver) : base(driver, Pages.CreateNewBillingScheduleManagement)
        {
        }

        internal List<string> CreateBillingSchedule(string fleet, string dealer, string adjustmentOfPrice)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetCurrentDate());
                SearchAndSelectValueAfterOpen(FieldNames.Dealer, dealer);
                SearchAndSelectValueAfterOpen(FieldNames.Fleet, fleet);
                WaitForDisabledClass(FieldNames.PricingGroup);
                SelectValueByScroll(FieldNames.AdjustmentOfPrice, adjustmentOfPrice);

                if (GetValueOfDropDown(FieldNames.AccountingDocumentType) != "All")
                {
                    SelectValueByScroll(FieldNames.AccountingDocumentType, "All");
                }
                if (GetValueOfDropDown(FieldNames.TransactionType) != "All")
                {
                    SelectValueByScroll(FieldNames.TransactionType, TableHeaders.TransactionType, "All");
                }
                if (GetValueOfDropDown(FieldNames.Currency) != "All")
                {
                    SelectValueByScroll(FieldNames.Currency, "All");
                }
                if (GetValueOfDropDown(FieldNames.Type) != "Amount")
                {
                    SelectValueByScroll(FieldNames.Type, "Amount");
                }
                EnterTextAfterClear(FieldNames.Value, "10.2355");
                SelectValueByScroll(FieldNames.PriceLevel, "AFLD");
                if (GetValueOfDropDown(FieldNames.TierType) != "No Tier")
                {
                    SelectValueByScroll(FieldNames.TierType, "No Tier");
                }
                EnterTextAfterClear(FieldNames.AdjustmentName, dealer + "Adj");
                ButtonClick(ButtonsAndMessages.Save);
                WaitForLoadingIcon();
                CheckForText(ButtonsAndMessages.Recordsavedsuccessfully);
                ClosePopupWindow();
                SwitchToMainWindow();
         
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.ToString());
                errorMsgs.Add(ex.ToString());
            }
            return errorMsgs;
        }

        internal void UpdateBSMFields(string value, string adjustmentName)
        {
            EnterTextAfterClear(FieldNames.Value, value);
            EnterTextAfterClear(FieldNames.AdjustmentName, adjustmentName);
            ButtonClick(ButtonsAndMessages.Save);
            WaitForLoadingIcon();
            CheckForText("Record saved successfully.");
            ClosePopupWindow();
            SwitchToMainWindow();
        }
    }
}
