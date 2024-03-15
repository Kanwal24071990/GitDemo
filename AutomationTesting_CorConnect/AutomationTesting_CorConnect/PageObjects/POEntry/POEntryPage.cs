using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.POEntry
{
    internal class POEntryPage : Commons
    {
        internal POEntryPage(IWebDriver webDriver) : base(webDriver, Pages.POEntry)
        {
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterDateInFromDate(fromDate);
            EnterDateInToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal POEntryAspx OpenCreateNewPO()
        {
            ButtonClick(ButtonsAndMessages.CreateNewPO);
            SwitchToPopUp();
            return new POEntryAspx(driver);
        }
    }
}
