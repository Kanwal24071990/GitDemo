using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.PartSalesbyFleetReport
{
    internal class PartSalesbyFleetReportPage : Commons
    {
        internal PartSalesbyFleetReportPage(IWebDriver webDriver) : base(webDriver, Pages.PartSalesByFleetReport)
        {
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterDateInFromDate(fromDate);
            EnterDateInToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
