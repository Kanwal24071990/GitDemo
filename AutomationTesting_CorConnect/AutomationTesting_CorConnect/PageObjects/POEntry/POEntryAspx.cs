using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.PageObjects.POEntry
{
    internal class POEntryAspx : PopUpPage
    {
        internal POEntryAspx(IWebDriver webDriver):base(webDriver, Pages.POEntryAspx)
        {
        }

        internal List<string> CreateNewPO(string fleet, string dealer, string fleetPONum, string POType = "Parts")
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                if (GetValueOfDropDown(FieldNames.POType) != POType)
                {
                    SelectValueByScroll(FieldNames.POType, POType);
                }
                SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealer);
                var entityDetails = CommonUtils.GetEntityDetails(dealer);
                SetDropdownTableSelectInputValue(FieldNames.DealerCode, entityDetails.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.FleetPONumber, fleetPONum);
                SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleet);
                var entityDetailsFleet = CommonUtils.GetEntityDetails(fleet);
                SetDropdownTableSelectInputValue(FieldNames.FleetCode, entityDetailsFleet.EntityDetailId.ToString());
                EnterTextAfterClear(FieldNames.PODate, CommonUtils.GetCurrentDate());

                Click(ButtonsAndMessages.SaveAndContinue);
                WaitForLoadingIcon();
                WaitForAnyElementLocatedBy("PO_Save and Continue");
                Click("PO_Save and Continue");
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
