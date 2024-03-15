using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.FleetPartSummaryLocation
{
    internal class FleetPartSummaryLocationPage : Commons
    {
        internal FleetPartSummaryLocationPage(IWebDriver webDriver) : base(webDriver, Pages.FleetPartSummaryLocation) { }

        internal void PopulateGrid(string from, string to)
        {
            EnterFromDate(from);
            EnterToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
