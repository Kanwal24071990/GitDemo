using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.IntercommunityInvoiceReport
{
    internal class IntercommunityInvoiceReportPage : Commons
    {
        internal IntercommunityInvoiceReportPage(IWebDriver webDriver) : base(webDriver, Pages.IntercommunityInvoiceReport)
        {
        }

        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            WaitForElementToBeClickable(GetElement(FieldNames.From));
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
