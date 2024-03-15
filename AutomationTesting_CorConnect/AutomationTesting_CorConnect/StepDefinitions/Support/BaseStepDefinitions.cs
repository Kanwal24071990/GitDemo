using AutomationTesting_CorConnect.DriverBuilder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.Support
{
    [Binding]
    internal class BaseStepDefinitions : DriverBuilderClass
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            Initialize();
            CreateDriver();
            InitializeDriver();
        }

        public void InitializeDriver()
        {
            if (driver == null && applicationContext.DriverList.Count > 0)
            {
                driver = applicationContext.DriverList.First(x => x.TestScenario == ScenarioContext.Current.ScenarioInfo.Title).Driver;
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (applicationContext.DriverList.Count > 0)
            {
                driver = applicationContext.DriverList.FirstOrDefault().Driver;
                applicationContext.DriverList.Remove(applicationContext.DriverList.First(x => x.TestScenario == ScenarioContext.Current.ScenarioInfo.Title));
            }
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");

                foreach (var chromeDriverProcess in chromeDriverProcesses)
                {
                    chromeDriverProcess.Kill();
                }
            }

        }
    }
}
