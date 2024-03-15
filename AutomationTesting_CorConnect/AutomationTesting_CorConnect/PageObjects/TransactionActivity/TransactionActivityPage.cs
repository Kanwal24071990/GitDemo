using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.TransactionActivity
{
    internal class TransactionActivityPage : Commons
    {
        internal TransactionActivityPage(IWebDriver webDriver) : base(webDriver, Pages.TransactionActivity)
        {
        }

        internal void PopulateGrid(string fromDate, string sellerName)
        {
            EnterDateInFromDate(fromDate);
            SearchAndSelectValue(FieldNames.CompanyName, sellerName);
            ClickSearch();
            GridLoad();
        }

    }
}
