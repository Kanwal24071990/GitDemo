using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.PartSalesbyShopReport
{
    internal class PartSalesbyShopReportPage : Commons
    {
        internal PartSalesbyShopReportPage(IWebDriver webDriver) : base(webDriver, Pages.PartSalesByShopReport)
        {
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterDateInFromDate(fromDate);
            EnterDateInToDate(toDate);
            ClickElement("Enable Group By");
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
