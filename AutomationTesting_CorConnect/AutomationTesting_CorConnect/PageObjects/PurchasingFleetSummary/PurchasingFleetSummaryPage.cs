using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.PurchasingFleetSummary
{
    internal class PurchasingFleetSummaryPage : Commons
    {
        internal PurchasingFleetSummaryPage(IWebDriver webDriver) : base(webDriver, Pages.PurchasingFleetSummary)
        {
        }
        internal void LoadDataOnGrid(string dealerCode, string fleetCode, string fromDate, string toDate)
        {
            if (!String.IsNullOrEmpty(dealerCode)) 
            {
                SelectFirstRowMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerCode);
            }

            if (!String.IsNullOrEmpty(fleetCode))
            {
                SelectFirstRowMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, fleetCode);
            }

            if (!String.IsNullOrEmpty(fromDate))
            {
                WaitForElementToBeClickable(GetElement(FieldNames.From));
                EnterFromDate(fromDate);
            }

            if (!String.IsNullOrEmpty(toDate))
            {
                EnterToDate(toDate);
            }

            ClickElement(FieldNames.EnableGroupBy);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
