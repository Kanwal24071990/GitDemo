using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.CommunityFeeReport
{
    internal class CommunityFeeReportPage : Commons
    {
        internal CommunityFeeReportPage(IWebDriver webDriver) : base(webDriver, Pages.CommunityFeeReport)
        {
        }

        internal void PopulateGrid(string from, string to, string currencyCode)
        {
            SelectValueTableDropDown(FieldNames.CurrencyCode, currencyCode);
            EnterDateInFromDate(from);
            EnterDateInToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }
    }
}
