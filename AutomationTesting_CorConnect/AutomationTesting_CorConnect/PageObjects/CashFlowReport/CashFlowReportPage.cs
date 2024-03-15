using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.CashFlowReport
{
    internal class CashFlowReportPage : Commons
    {
        internal CashFlowReportPage(IWebDriver webDriver) : base(webDriver, Pages.CashFlowReport)
        {
           
        }

        internal void LoadDataOnGrid()
        {
            ClickElement(FieldNames.EnableGroupBy);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}
