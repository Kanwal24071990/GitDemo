using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.PODiscrepancy
{
    internal class PODiscrepancyPage : Commons
    {
        internal PODiscrepancyPage(IWebDriver webDriver) : base(webDriver, Pages.PODiscrepancy)
        {
        }
        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
