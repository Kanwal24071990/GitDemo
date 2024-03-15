using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.POTransactionLookup
{
    internal class POTransactionLookupPage : Commons
    {

        internal POTransactionLookupPage(IWebDriver webDriver) : base(webDriver, Pages.POTransactionLookup)
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
