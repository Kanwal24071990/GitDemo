﻿using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.PriceLookup
{
    internal class PriceLookupPage : Commons
    {
        internal PriceLookupPage(IWebDriver webDriver) : base(webDriver, Pages.PriceLookup)
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
