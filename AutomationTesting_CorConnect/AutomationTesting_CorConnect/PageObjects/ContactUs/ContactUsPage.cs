using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ContactUs
{
    internal class ContactUsPage : StaticPage
    {
        internal ContactUsPage(IWebDriver webDriver) : base(webDriver, Pages.ContactUs)
        {
            WaitForIframe();
            SwitchIframe();
        }

        internal void OpenOnlineSupportCenter()
        {
            Click(ButtonsAndMessages.OnlineSupportCenter);
            SwitchToPopUp();
        }
    }
}
