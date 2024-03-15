using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.InvoiceOptions
{
    internal class InvoiceOptionsPage : PopUpPage
    {
        internal InvoiceOptionsPage(IWebDriver driver) : base(driver, "Invoice Options")
        {
        }

        internal OffsetTransactionPage CreateRebill()
        {
            SwitchIframe();
            Click(ButtonsAndMessages.CreateRebill);
            SwitchToPopUp();
            return new OffsetTransactionPage(driver);
        }
        internal CreateNewInvoicePage ChangeInvoice()
        {
            SwitchIframe();
            Click(ButtonsAndMessages.ChangeInvoice);
            SwitchToPopUp();
            return new CreateNewInvoicePage(driver);
        }

        internal void SwitchTab(string tabName)
        {
            SwitchIframe(1);
            driver.FindElement(By.XPath(string.Format("//span[text()='{0}']", tabName))).Click();
        }

        

    }
}
