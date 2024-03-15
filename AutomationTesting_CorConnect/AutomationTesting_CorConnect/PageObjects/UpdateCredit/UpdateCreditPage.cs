using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.PageObjects.UpdateCredit.CreditHistory;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.UpdateCredit
{
    internal class UpdateCreditPage : StaticPage
    {
        public UpdateCreditPage(IWebDriver webDriver) : base(webDriver, Pages.UpdateCredit)
        {

        }

        public CreditHistoryPage OpenCreditHistory()
        {
            SwitchIframe();
            Click(GetElement("Credit History"));
            SwitchToPopUp();
            return new CreditHistoryPage(driver);
        }
    }
}
