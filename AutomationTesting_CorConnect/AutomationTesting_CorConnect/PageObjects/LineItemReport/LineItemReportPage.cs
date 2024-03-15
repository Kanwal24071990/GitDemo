using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.LineItemReport
{
    internal class LineItemReportPage : Commons
    {
        internal LineItemReportPage(IWebDriver webDriver) : base(webDriver, Pages.LineItemReport)
        {
        }

        internal void SelectLoadBookmarkFirstRow()
        {
            SelectValueFirstRow(FieldNames.LoadBookmark);
        }

        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
