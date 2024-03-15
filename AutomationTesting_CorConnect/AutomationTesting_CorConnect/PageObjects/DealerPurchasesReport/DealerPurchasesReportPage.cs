using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.PageObjects.DealerPurchasesReport
{
    internal class DealerPurchasesReportPage : PopUpPage
    {
        internal DealerPurchasesReportPage(IWebDriver driver) : base(driver, Pages.DealerPurchasesReport)
        {
        }
        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.Processing);
            WaitForMsg(ButtonsAndMessages.Loading);
            WaitForGridLoad();
        }
        internal List<string> AreFieldsAvailable()
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.LoadBookmark))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LoadBookmark));
            }
            if (!IsElementVisible(FieldNames.FleetCompanyName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FleetCompanyName));
            }
            if (!IsElementDisplayed(FieldNames.DealerCompanyName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DealerCompanyName));
            }
            if (!IsElementVisible(FieldNames.DateType))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DateType));
            }
            if (!IsElementDisplayed(FieldNames.DateRange))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DateRange));
            }
            if (!IsElementVisible(FieldNames.From))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.From));
            }
            if (!IsElementVisible(FieldNames.To))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.To));
            }
            if (!IsElementVisible(FieldNames.Currency))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
            }
            if (!IsElementVisible(FieldNames.GroupBy))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.GroupBy));
            }

            return errorMsgs;
        }
    }
}
