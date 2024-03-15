using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.ProductGroupRuleMaintenance
{
    internal class ProductGroupRuleMaintenancePage : Commons
    {
        internal ProductGroupRuleMaintenancePage(IWebDriver webDriver) : base(webDriver, Pages.ProductGroupRuleMaintenance)
        {
        }
    }
}
