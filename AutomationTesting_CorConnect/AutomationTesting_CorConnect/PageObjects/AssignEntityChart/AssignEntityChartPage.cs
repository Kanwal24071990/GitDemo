using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.AssignEntityChart
{
    internal class AssignEntityChartPage : Commons
    {
        internal AssignEntityChartPage(IWebDriver webDriver) : base(webDriver, Pages.AssignEntityChart)
        {
        }
    }
}
