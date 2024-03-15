using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerPartSummaryFleetBillTo
{
    internal class DealerPartSummaryFleetBillToPage : Commons
    {
        internal DealerPartSummaryFleetBillToPage(IWebDriver webDriver) : base(webDriver, Pages.DealerPartSummaryFleetBillTo)
        { }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
