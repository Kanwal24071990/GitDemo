using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.FleetLocations
{
    internal class FleetLocationsPage : Commons
    {
        internal FleetLocationsPage(IWebDriver driver) : base(driver, Pages.FleetLocations)
        {

        }

        public void LoadDataOnGrid(string fleetCode)
        {
            ClickElement("Enable Group By");
            SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleetCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        public void LoadDataOnGrid()
        {
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
        public void LoadDataOnGridwithCode(string fleetCode)
        {
            SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleetCode);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
