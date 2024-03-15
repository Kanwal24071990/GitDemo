using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.LoyaltyStatus
{
    internal class LoyaltyStatusPage : PopUpPage
    {
        internal LoyaltyStatusPage(IWebDriver webDriver) : base(webDriver, Pages.LoyaltyStatus)
        {
        }
    }
}
