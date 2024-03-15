using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.POQEntry
{
    internal class POQEntryPage : Commons
    {
        internal POQEntryPage(IWebDriver webDriver) : base(webDriver, Pages.POQEntry)
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


        internal POQEntryAspx OpenCreateNewPOQ()
        {
            ButtonClick(ButtonsAndMessages.CreateNewPOQ);
            SwitchToPopUp();
            return new POQEntryAspx(driver);
        }

    }
}
