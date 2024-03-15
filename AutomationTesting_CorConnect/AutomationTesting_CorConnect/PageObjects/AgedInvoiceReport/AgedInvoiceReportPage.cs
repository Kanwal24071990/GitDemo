using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.AgedInvoiceReport
{
    internal class AgedInvoiceReportPage : Commons
    {
        internal AgedInvoiceReportPage(IWebDriver webDriver) : base(webDriver, Pages.AgedInvoiceReport)
        {
        }

        internal void PopulateGrid(string location)
        {
            SelectValueMultiSelectDropDown(FieldNames.AgedBucket, "Aged Bucket", "Current");
            SearchAndSelectValueAfterOpen(FieldNames.Location, location);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }
    }
}
