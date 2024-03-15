using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.BillingScheduleManagement
{
    internal class BillingScheduleManagementPage : Commons
    {
        internal BillingScheduleManagementPage(IWebDriver webDriver) : base(webDriver, Pages.BillingScheduleManagement)
        { }

        internal void PopulateGrid(string companyName, string effectiveDate)
        {
            if (!IsMultiSelectDropDownClosed(FieldNames.CompanyName))
            {
                ClickPageTitle();
            }
            SelectFirstRowMultiSelectDropDown(FieldNames.CompanyName, TableHeaders.DisplayName, companyName);
            if (!string.IsNullOrEmpty(effectiveDate))
            {
                EnterTextAfterClear(FieldNames.EffectiveDate, effectiveDate);
            }
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal CreateNewBillingScheduleManagementPage OpenCreateBillingSchedule()
        {
            ButtonClick(ButtonsAndMessages.CreateBillingSchedule);
            SwitchToPopUp();
            return new CreateNewBillingScheduleManagementPage(driver);
        }
    }
}
