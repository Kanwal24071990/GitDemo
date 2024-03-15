using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.SummaryReconcileReport
{
    internal class SummaryReconcileReportPage : Commons
    {
        internal SummaryReconcileReportPage(IWebDriver webDriver) : base(webDriver, Pages.SummaryReconcileReport)
        {
        }

        internal void PopulateGrid(string from, string to)
        {
            EnterFromDate(from);
            EnterToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }
    }
}
