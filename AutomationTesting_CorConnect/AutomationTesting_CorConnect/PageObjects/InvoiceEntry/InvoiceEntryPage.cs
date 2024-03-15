using AutomationTesting_CorConnect.PageObjects.CreateAuthorization;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceEntry
{
    internal class InvoiceEntryPage : Commons
    {
        internal InvoiceEntryPage(IWebDriver webDriver) : base(webDriver, Pages.InvoiceEntry)
        {
        }

        internal CreateNewInvoicePage OpenNewInvoice()
        {
            ButtonClick(ButtonsAndMessages.CreateNewInvoice);
            SwitchToPopUp();
            return new CreateNewInvoicePage(driver);
        }

        internal CreateNewInvoicePage OpenExistingInvoice(DateTime date, string transactionNumber, string header)
        {
            EnterFromDate(date);
            EnterToDate(date);
            ClickSearch();
            WaitForGridLoad();
            FilterTable(header, transactionNumber);
            ClickHyperLinkOnGrid(header);
            return new CreateNewInvoicePage(driver);
        }

        internal CreateNewInvoicePage OpenCreateNewInvoice()
        {
            ButtonClick(ButtonsAndMessages.CreateNewInvoice);
            SwitchToPopUp();
            return new CreateNewInvoicePage(driver);
        }

        internal void LoadDataOnGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
        }

        internal void LoadDataOnGrid(string EntityName)
        {
            SetDropdownTableSelectInputValue(FieldNames.CompanyName, EntityName);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
        }

        internal void CreateAuthorization(string Dealer, string Fleet, out string dealerInvNum, out string authCode)
        {
            ClosePage(Pages.InvoiceEntry);
            Menu menu = new Menu(driver); 
            menu.OpenPage(Pages.CreateAuthorization, false, true);
            menu.SwitchIframe();
            var CreateAuthorizationPage = new CreateAuthorizationPage(driver);
            CreateAuthorizationPage.CreateAuthorization(Dealer, Fleet, out dealerInvNum, out authCode);
            menu.SwitchToMainWindow();
            ClosePage(Pages.CreateAuthorization);
            menu.OpenPage(Pages.InvoiceEntry);
        }

    }
}