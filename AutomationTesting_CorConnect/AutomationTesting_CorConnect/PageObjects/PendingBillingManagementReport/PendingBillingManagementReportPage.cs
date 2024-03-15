using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.PendingBillingManagementReport
{
    internal class PendingBillingManagementReportPage : Commons
    {
        internal PendingBillingManagementReportPage(IWebDriver webDriver) : base(webDriver, Pages.PendingBillingManagementReport)
        {
        }

        internal void PopulateGrid(string companyName)
        {
            SearchAndSelectValueAfterOpen(FieldNames.CompanyName, companyName);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }
    }
}
