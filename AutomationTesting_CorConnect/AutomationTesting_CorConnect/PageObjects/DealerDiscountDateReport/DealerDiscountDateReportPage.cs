using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.DealerDiscountDateReport
{
    internal class DealerDiscountDateReportPage : Commons
    {
        internal DealerDiscountDateReportPage(IWebDriver webDriver) : base(webDriver, Pages.DealerDiscountDateReport)
        {
        }

        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            WaitForElementToBeClickable(GetElement(FieldNames.From));
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

    }
}
