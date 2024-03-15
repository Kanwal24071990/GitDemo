using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.OpenBalanceReport
{
    internal class OpenBalanceReportPage : Commons
    {
        internal OpenBalanceReportPage(IWebDriver webDriver) : base(webDriver, Pages.OpenBalanceReport)
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
