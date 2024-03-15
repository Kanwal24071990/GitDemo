using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.OrderRequisition
{
    internal class OrderRequisitionPage : PopUpPage
    {
        internal OrderRequisitionPage(IWebDriver driver) : base(driver, Pages.OrderRequisition)
        {
        }
    }
}
