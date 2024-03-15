using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.PartsLookup
{
    internal class PartsLookupPage : Commons
    {
        internal PartsLookupPage(IWebDriver webDriver) : base(webDriver, Pages.PartsLookup)
        {
        }

        internal void PopulateGrid(string partNumber, bool waitforGrid = true)
        {
            EnterTextAfterClear(FieldNames.PartNumber, partNumber);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);

            if (waitforGrid)
            {
                WaitForGridLoad();
            }
        }
    }
}
