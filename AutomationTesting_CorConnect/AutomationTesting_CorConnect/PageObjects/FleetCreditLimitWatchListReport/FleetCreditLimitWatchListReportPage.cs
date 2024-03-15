using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.FleetCreditLimitWatchListReport
{
    internal class FleetCreditLimitWatchListReportPage : Commons
    {
        internal FleetCreditLimitWatchListReportPage(IWebDriver webDriver) : base(webDriver, Pages.FleetCreditLimitWatchListReport)
        {
        }

        internal void PopulateGrid(string location)
        {
            SearchAndSelectValueAfterOpen(FieldNames.Location, location);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }
    }
}
