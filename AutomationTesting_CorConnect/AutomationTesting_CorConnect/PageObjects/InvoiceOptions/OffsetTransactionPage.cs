using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceOptions
{
    internal class OffsetTransactionPage : PopUpPage
    {

        internal OffsetTransactionPage(IWebDriver driver) : base(driver, "Offset Transaction Aspx")
        {
        }

        internal bool CheckForTextForOffSetTransactions(string text, bool WaitForElementToBeVisible = false)
        {
            string textXpath = "//*[text()='{0}']";

            if (WaitForElementToBeVisible)
            {
                try
                {
                    WaitForElementToBePresent(By.XPath(string.Format(textXpath, text)));
                }
                catch (Exception e)
                {
                    LoggingHelper.LogException(e.Message);
                    return false;
                }
            }
            
            return driver.FindElements(By.XPath(string.Format(textXpath, text))).FirstOrDefault().Text.Equals(text);
        }
    }
}
