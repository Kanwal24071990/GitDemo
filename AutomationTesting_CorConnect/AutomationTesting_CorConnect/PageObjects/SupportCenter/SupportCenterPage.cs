using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
        
namespace AutomationTesting_CorConnect.PageObjects.SupportCenter
{
    internal class SupportCenterPage: Commons
    {
        internal SupportCenterPage(IWebDriver webDriver) : base(webDriver, Pages.SupportCenter)
        {
        }
    }
}
