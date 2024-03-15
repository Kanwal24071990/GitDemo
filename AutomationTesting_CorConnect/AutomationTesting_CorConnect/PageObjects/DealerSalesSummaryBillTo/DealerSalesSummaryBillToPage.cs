using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.DealerSalesSummaryBillTo
{
    internal class DealerSalesSummaryBillToPage : Commons
    {
        internal DealerSalesSummaryBillToPage(IWebDriver webDriver) : base(webDriver, Pages.DealerSalesSummaryBillTo)
        {
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
