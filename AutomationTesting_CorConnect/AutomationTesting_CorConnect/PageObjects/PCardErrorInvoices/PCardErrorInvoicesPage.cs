using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.PCardErrorInvoices
{
    internal class PCardErrorInvoicesPage : Commons
    {
        internal PCardErrorInvoicesPage(IWebDriver webDriver) : base(webDriver, Pages.PCardErrorInvoices)
        {
        }
    }
}
