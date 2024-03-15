using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.FleetSalesSummaryLocation
{
    internal class FleetSalesSummaryLocationPage : Commons
    {
        internal FleetSalesSummaryLocationPage(IWebDriver webDriver) : base(webDriver, Pages.FleetSalesSummaryLocation)
        {

        }

        internal void PopulateGrid(string FromDate, string ToDate)
        {
            EnterFromDate(FromDate);
            EnterToDate(ToDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
