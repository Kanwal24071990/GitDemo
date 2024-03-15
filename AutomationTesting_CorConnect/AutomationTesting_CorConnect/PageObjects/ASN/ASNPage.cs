using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ASN
{
    internal class ASNPage : Commons
    {
        internal ASNPage(IWebDriver webDriver) : base(webDriver, Pages.ASN)
        {
        }
        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            EnterDateInFromDate(fromDate);
            EnterDateInToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
