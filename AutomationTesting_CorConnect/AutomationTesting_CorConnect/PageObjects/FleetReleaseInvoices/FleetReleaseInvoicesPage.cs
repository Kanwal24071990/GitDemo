using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.FleetReleaseInvoices
{
    internal class FleetReleaseInvoicesPage : Commons
    {
        internal FleetReleaseInvoicesPage(IWebDriver webDriver) : base(webDriver, Pages.FleetReleaseInvoices) 
        { 
        }
    }
}
