using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DunningStatus
{
    internal class DunningStatusPage : Commons
    {
        internal DunningStatusPage(IWebDriver webDriver) : base(webDriver, Pages.DunningStatus)
        {
        }

        internal void PopulateGrid(string corcentricCode, string from, string to)
        {
            SelectFirstRowMultiSelectDropDown(FieldNames.FleetBilling, FieldNames.AccountCode, corcentricCode);
            EnterFromDate(from);
            EnterToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal void LoadDataOnGrid(string corcentricCode, string status, string from, string to)
        {
            if (!string.IsNullOrEmpty(corcentricCode))
            {
                SelectFirstRowMultiSelectDropDown(FieldNames.FleetBilling, FieldNames.AccountCode, corcentricCode);
            }
            if (!string.IsNullOrEmpty(status))
            {
                //SelectValueTableDropDown(FieldNames.DunningStatus, status);
            }
            if (!string.IsNullOrEmpty(from))
            {
                EnterFromDate(from);
            }
            if (!string.IsNullOrEmpty(to))
            {
                EnterToDate(to);
            }

            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
