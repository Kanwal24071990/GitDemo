using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.AgencyGroupApproversReport
{
    internal class AgencyGroupApproversReportPage : Commons
    {
        internal AgencyGroupApproversReportPage(IWebDriver webDriver) : base(webDriver, Pages.AgencyGroupApproversReport)
        {
        }
    }
}
