using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.FranchiseCodeManagement
{
    internal class FranchiseCodeManagementPage : Commons
    {
        internal FranchiseCodeManagementPage(IWebDriver webDriver) : base(webDriver, Pages.FranchiseCodeManagement)
        {
        }
    }
}
