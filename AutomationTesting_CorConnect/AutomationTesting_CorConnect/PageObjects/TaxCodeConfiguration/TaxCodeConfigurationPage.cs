using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.TaxCodeConfiguration
{
    internal class TaxCodeConfigurationPage : Commons
    {
        internal TaxCodeConfigurationPage(IWebDriver webDriver) : base(webDriver, Pages.TaxCodeConfiguration)
        {
        }

        internal void PopulateGrid(string lineType)
        {
            SelectValueTableDropDown("Line Type", lineType);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
    }
}