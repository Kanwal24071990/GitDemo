using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.FleetInvoiceDetailReport
{
    internal class FleetInvoiceDetailReportPage : Commons
    {
        internal FleetInvoiceDetailReportPage(IWebDriver webDriver) : base(webDriver, Pages.FleetInvoiceDetailReport)
        {
        }
        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            WaitForElementToBeClickable(GetElement(FieldNames.From));
            ClickElement(FieldNames.EnableGroupBySearch);
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
        }
    }
}
