using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.PartsCrossReference
{
    internal class PartsCrossReferencePage : Commons
    {
        internal PartsCrossReferencePage(IWebDriver driver) : base(driver, Pages.PartsCrossReference) { }

        internal void PopulateGrid(string companyName)
        {
            SearchAndSelectValueAfterOpen(FieldNames.CompanyName, companyName);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
