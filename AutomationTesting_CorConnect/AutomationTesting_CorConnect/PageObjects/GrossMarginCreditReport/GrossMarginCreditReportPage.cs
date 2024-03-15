using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.GrossMarginCreditReport
{
    internal class GrossMarginCreditReportPage : Commons
    {
        internal GrossMarginCreditReportPage(IWebDriver webDriver) : base(webDriver, Pages.GrossMarginCreditReport)
        {
        }

        internal void PopulateGrid(string from, string to)
        {
            ClickElement(FieldNames.EnableGroupBy);
            EnterDateInFromDate(from);
            EnterDateInToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }   
    }
}
