using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice
{
    internal class InvoiceHistory : PopUpPage
    {
        internal InvoiceHistory(IWebDriver driver) : base(driver, Pages.InvoiceHistory)
        {
        }
    }
}
