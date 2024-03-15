using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.UpdateCredit.CreditHistory;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.UpdateCredit.CreditHistory
{
    internal class CreditHistoryPage : PopUpPage
    {
        CreditHistoryUtil creditHistoryUtil=new CreditHistoryUtil();

        internal CreditHistoryPage(IWebDriver webDriver) : base(webDriver, Pages.CreditHistory)
        {
        }

        internal void SelectBillingValue(out string corCentricCode)
        {
            corCentricCode = creditHistoryUtil.GetCorCentricCode();
            SearchAndSelectValueAfterOpen("Billing Location", corCentricCode);
        }
    }
}
