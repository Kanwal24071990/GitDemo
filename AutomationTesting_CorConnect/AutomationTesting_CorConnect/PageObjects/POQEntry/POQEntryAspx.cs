using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.PageObjects.POQEntry
{
    internal class POQEntryAspx : PopUpPage
    {
        public POQEntryAspx(IWebDriver webDriver) : base(webDriver, Pages.POQEntryAspx)
        {
        }

        internal List<string> CreateNewPOQ(string fleet, string dealer, string dealerPOQNum,string quoteType = "Parts")
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                if (GetValueOfDropDown(FieldNames.QuoteType) != quoteType)
                {
                    SelectValueByScroll(FieldNames.QuoteType, quoteType);
                }
                SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealer);
                var entityDetails = CommonUtils.GetEntityDetails(dealer);
                SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.DealerPOQNumber, dealerPOQNum);
                SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleet);
                var entityDetailsFleet = CommonUtils.GetEntityDetails(fleet);
                SetDropdownTableSelectInputValue(FieldNames.FleetCode, entityDetailsFleet.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.POQDate, CommonUtils.GetCurrentDate());

                Click(ButtonsAndMessages.SaveAndContinue);
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("POQ_Save and Continue");
                Click("POQ_Save and Continue");
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
    }
}
