using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManagePaymentTerms.CreateNewPaymentTerm
{
    internal class CreateNewPaymentTermPage : PopUpPage
    {
        internal CreateNewPaymentTermPage(IWebDriver driver) : base(driver, Pages.CreateNewPaymentTerm)
        {
        }

        internal bool CreateNewPaymentTerm(string paymentTerm, string netDueDays)
        {
            EnterText(FieldNames.PaymentTermName, paymentTerm);
            EnterText(FieldNames.PaymentTermDescription, paymentTerm);
            EnterText(FieldNames.NetDueDays, netDueDays);
            Click(FieldNames.APOnly);
            Click(FieldNames.AROnly);
            Click(ButtonsAndMessages.SaveTerm);
            var isMsgDisplayed = CheckForText(ButtonsAndMessages.Recordsavedsuccessfully, true);
            Click(ButtonsAndMessages.Cancel);
            SwitchToMainWindow();
            return isMsgDisplayed;
        }
    }
}
