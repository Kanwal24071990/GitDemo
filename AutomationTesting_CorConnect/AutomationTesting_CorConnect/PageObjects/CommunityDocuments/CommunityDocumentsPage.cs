using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.CommunityDocuments
{
    internal class CommunityDocumentsPage : StaticPage
    {
        internal CommunityDocumentsPage(IWebDriver driver) : base(driver, Pages.CommunityDocuments)
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
