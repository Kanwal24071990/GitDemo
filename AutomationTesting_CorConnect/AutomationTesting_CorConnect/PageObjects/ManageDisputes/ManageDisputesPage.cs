using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.ManageDisputes
{
    internal class ManageDisputesPage : Commons
    {
        internal ManageDisputesPage(IWebDriver webDriver) : base(webDriver, Pages.ManageDisputes)
        {

        }
    }
}
