using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.RelationshipSearch
{
    internal class RelationshipSearchPage : PopUpPage
    {
        public RelationshipSearchPage(IWebDriver webDriver) : base(webDriver, Pages.RelationshipSearch)
        {
        }
        internal void LoadDataOnGrid()
        {
            ButtonClick(ButtonsAndMessages.Search);
        }
    }
}
