using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.DraftStatementReport
{
    internal class DraftStatementReportPage : Commons
    {
        internal DraftStatementReportPage(IWebDriver webDriver) : base(webDriver, Pages.DraftStatementReport)
        {
        }

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
