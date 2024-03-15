using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.CreateInvoiceWatchList
{
    internal class CreateInvoiceWatchListPage : PopUpPage
    {
        internal CreateInvoiceWatchListPage(IWebDriver driver) : base(driver, Pages.CreateInvoiceWatchList)
        {
        }
    }
}
