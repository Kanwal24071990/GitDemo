using AutomationTesting_CorConnect.PageObjects.ManagePaymentTerms.CreateNewPaymentTerm;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManagePaymentTerms
{
    internal class ManagePaymentTermsPage : Commons
    {
        internal ManagePaymentTermsPage(IWebDriver webDriver) : base(webDriver, Pages.ManagePaymentTerms)
        {
        }

        internal CreateNewPaymentTermPage OpenCreateNewPaymentTerm()
        {
            ButtonClick(ButtonsAndMessages.CreateNewPaymentTerm);
            SwitchToPopUp();
            return new CreateNewPaymentTermPage(driver);
        }

        internal void SearchByPaymentTermName(string paymentTerm)
        {
            EnterText(FieldNames.PaymentTermName, paymentTerm);
            LoadDataOnGrid();
        }
    }
}
