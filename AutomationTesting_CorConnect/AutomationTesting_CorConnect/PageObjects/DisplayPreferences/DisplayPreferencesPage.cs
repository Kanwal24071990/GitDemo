using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DisplayPreferences
{
    internal class DisplayPreferencesPage : StaticPage
    {
        public DisplayPreferencesPage(IWebDriver driver) : base(driver, Pages.DisplayPreferences)
        {
        }

        internal void SaveDisplayPreferences()
        {
            ButtonClick(ButtonsAndMessages.Save);
            WaitForLoadingIcon();
        }
    }
}
