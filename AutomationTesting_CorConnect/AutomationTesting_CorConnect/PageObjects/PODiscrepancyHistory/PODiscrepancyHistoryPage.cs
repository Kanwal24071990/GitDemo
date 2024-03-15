using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.PODiscrepancyHistory
{
    internal class PODiscrepancyHistoryPage : Commons
    {
        internal PODiscrepancyHistoryPage(IWebDriver webDriver) : base(webDriver, Pages.PODiscrepancyHistory)
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
        internal void InvoiceMoveToPODiscrepancy(out string msg1, out string msg2)
        {
            ButtonClick(ButtonsAndMessages.MovetoPODiscrepancy);
            AcceptAlert(out msg1);
            AcceptAlert(out msg2);
        }
    }
}
