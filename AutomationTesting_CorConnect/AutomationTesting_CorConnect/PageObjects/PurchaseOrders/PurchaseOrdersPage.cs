using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.PurchaseOrders
{
    internal class PurchaseOrdersPage : Commons
    {
        internal PurchaseOrdersPage(IWebDriver webDriver) : base(webDriver, Pages.PurchaseOrders)
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
