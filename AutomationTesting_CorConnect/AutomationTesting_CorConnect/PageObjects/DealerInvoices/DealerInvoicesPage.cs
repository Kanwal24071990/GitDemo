using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;

namespace AutomationTesting_CorConnect.PageObjects.DealerInvoices
{
    internal class DealerInvoicesPage : Commons
    {
        internal DealerInvoicesPage(IWebDriver webDriver) : base(webDriver, Pages.DealerInvoices)
        {
        }
        public void LoadDataOnGrid(string DealerInvoiceNumber)
        {
            ClearText(FieldNames.DealerInvoiceNumber);
            EnterText(FieldNames.DealerInvoiceNumber, DealerInvoiceNumber);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();

        }

        public void LoadDataOnGridWithProgramInvoiceNumber(string ProgramInvoiceNumber)
        {
            ClearText(FieldNames.ProgramInvoiceNumber);
            EnterText(FieldNames.ProgramInvoiceNumber, ProgramInvoiceNumber);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();

        }

        public void LoadDataOnGridWithFilter(string DealerInvoiceNumber)
        {
            EnterTextAfterClear(FieldNames.DealerInvoiceNumber, DealerInvoiceNumber);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            if (GetRowCount() > 0)
            {
                SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                FilterTable(TableHeaders.DealerInv__spc, DealerInvoiceNumber);
            }
        }

        internal void SelectDateRange(string value)
        {
            if (value != GetValueDropDown(FieldNames.DateRange))
            {
                SelectValueTableDropDown(FieldNames.DateRange, value);
                WaitForMsg(ButtonsAndMessages.PleaseWait);
            }
        }

        internal void SwitchToLastSevenDayDateRange(string value = "Last 7 days")
        {
            if (value != GetValueDropDown(FieldNames.DateRange))
            {
                SelectValueTableDropDown(FieldNames.DateRange, value);
                WaitForMsg(ButtonsAndMessages.PleaseWait);
            }
            else
            {
                SelectValueTableDropDown(FieldNames.DateRange, "Customized date");
                WaitForMsg(ButtonsAndMessages.PleaseWait);
                SelectValueTableDropDown(FieldNames.DateRange, value);
                WaitForMsg(ButtonsAndMessages.PleaseWait);
            }
        }

        internal void LoadDataOnGrid(string fromDate, string toDate, bool enableGroupBy = true)
        {
            WaitForElementToBeClickable(GetElement(FieldNames.From));
            if (enableGroupBy == true)
            {
                ClickElement(FieldNames.EnableGroupBy);
            }
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void SelectTransactionStatusAndDateType(string transactionStatus, string dateType, string dateRange, int from = 0)
        {
            if (GetValueDropDown(FieldNames.DateType) != dateType)
            {
                SelectValueByScroll(FieldNames.DateType, dateType);
            }

            if (GetValueDropDown(FieldNames.DateRange) != dateRange)
            {
                SearchAndSelectValueAfterOpenWithoutClear(FieldNames.DateRange, dateRange);
                WaitForMsg(ButtonsAndMessages.PleaseWait);
            }
            if (from != 0)
            {
                EnterTextAfterClear(FieldNames.From, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(from)));
            }
            EnterTextAfterClear(FieldNames.To, CommonUtils.GetCurrentDate());
            SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, transactionStatus);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

    }
}
