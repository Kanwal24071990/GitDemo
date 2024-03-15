using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using StackExchange.Redis;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceDiscrepancy
{
    internal class InvoiceDiscrepancyPage : Commons
    {
        internal InvoiceDiscrepancyPage(IWebDriver webDriver) : base(webDriver, Pages.InvoiceDiscrepancy)
        {
        }

        internal void LoadDataOnGrid()
        {
            ClickElement(FieldNames.EnableGroupBy);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void LoadDataOnGrid(string entityName,bool enableGroupBy = true)
        {
            SetDropdownTableSelectInputValue(FieldNames.CompanyName, entityName);
            if(enableGroupBy) { 
            ClickElement(FieldNames.EnableGroupBy);
            }
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
