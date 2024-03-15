using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.SummaryRemittanceReport
{
    internal class SummaryRemittanceReportPage : Commons
    {
        internal SummaryRemittanceReportPage(IWebDriver webDriver) : base(webDriver, Pages.SummaryRemittanceReport)
        {
        }

        internal void PopulateGrid(string from, string to)
        {
            EnterDateInFromDate(from);
            EnterDateInToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }
    }
}
