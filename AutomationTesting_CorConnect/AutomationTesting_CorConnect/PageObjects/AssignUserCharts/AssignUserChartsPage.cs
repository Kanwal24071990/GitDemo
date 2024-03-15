using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.AssignUserCharts
{
    internal class AssignUserChartsPage : Commons
    {
        internal AssignUserChartsPage(IWebDriver webDriver) : base(webDriver, Pages.AssignUserCharts)
        {
        }

        internal void LoadDataOnGrid()
        {
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
