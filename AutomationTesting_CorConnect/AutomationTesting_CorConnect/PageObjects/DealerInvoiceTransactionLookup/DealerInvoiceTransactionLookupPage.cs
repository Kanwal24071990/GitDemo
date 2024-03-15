﻿using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup
{
    internal class DealerInvoiceTransactionLookupPage : Commons
    {
        internal DealerInvoiceTransactionLookupPage(IWebDriver webDriver) : base(webDriver, Pages.DealerInvoiceTransactionLookup)
        {
        }

        public void LoadDataOnGrid(string DealerInvoiceNumber)
        {
            EnterTextAfterClear(FieldNames.DealerInvoiceNumber, DealerInvoiceNumber);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        public void LoadDataOnGridWithFilter(string DealerInvoiceNumber)
        {
            EnterTextAfterClear(FieldNames.DealerInvoiceNumber, DealerInvoiceNumber);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            if(GetRowCount() > 0)
            {
                SetFilterCreiteria(TableHeaders.DealerInv__spc, FilterCriteria.Equals);
                FilterTable(TableHeaders.DealerInv__spc, DealerInvoiceNumber);
            }
        }

        internal void SelectLoadBookmarkFirstRow()
        {
            SelectValueFirstRow(FieldNames.LoadBookmark);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void SelectDateRangeFirstRow()
        {
            SelectValueFirstRowOpenByFieldClick(FieldNames.DateRange);
        }

        internal void SelectDealerGroupFirstRow()
        {
            SelectValueFirstRow(FieldNames.DealerGroup);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void SelectFleetGroupFirstRow()
        {
            SelectValueFirstRow(FieldNames.FleetGroup);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
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

        internal void PopulateGrid(string from, string to, bool enableGroupBy = true)
        {
            if (enableGroupBy == true)
            {
                ClickElement("Enable Group By");
            }
            EnterFromDate(from);
            EnterToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
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