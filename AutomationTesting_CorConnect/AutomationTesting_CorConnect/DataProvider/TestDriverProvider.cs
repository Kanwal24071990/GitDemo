using AutomationTesting_CorConnect.StepDefinitions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataProvider
{
    public class TestDriverProvider
    {
        public IWebDriver Driver { get; set; }
        public string TestScenario { get; set; }

    }
}
