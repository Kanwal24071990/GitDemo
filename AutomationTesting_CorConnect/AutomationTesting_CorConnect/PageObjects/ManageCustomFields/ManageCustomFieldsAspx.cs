using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManageCustomFields
{
    internal class ManageCustomFieldsAspx : PopUpPage
    {
        internal ManageCustomFieldsAspx(IWebDriver webDriver) : base(webDriver, Pages.ManageCustomFieldsAspx)
        {
        }
    }
}
