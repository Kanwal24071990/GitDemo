using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManageApprovals
{
    internal class ManageApprovalsPage : Commons
    {
        internal ManageApprovalsPage(IWebDriver webDriver) : base(webDriver, Pages.ManageApprovals)
        {
        }
    }
}
