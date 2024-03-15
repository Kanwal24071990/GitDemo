using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.Resources;
using Microsoft.VisualBasic;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.Disputes
{
    internal class DisputesPage : Commons
    {
        internal DisputesPage(IWebDriver webDriver) : base(webDriver, Pages.Disputes)
        {

        }

        internal void SelectDateRangeFirstRow()
        {
            SelectValueFirstRow(FieldNames.DateRange);
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

        public void LoadDataOnGrid()
        {
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal void GoToInvoiceOptions(string programInvNumber)
        {
            SetFilterCreiteria(TableHeaders.ProgramInvoiceNumber, FilterCriteria.Equals);
            FilterTable(TableHeaders.ProgramInvoiceNumber, programInvNumber);
            ClickHyperLinkOnGrid(TableHeaders.ProgramInvoiceNumber);
        }

        internal void PopulateGrid(string from, string to)
        {
            SelectValueTableDropDown(FieldNames.DateType, "Program Invoice Date");
            SelectDateRange("Customized date");
            EnterFromDate(from);
            EnterToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }

        internal void PopulateGrid(string from, string to, string dateType)
        {
            SelectValueTableDropDown(FieldNames.DateType, dateType);
            SelectDateRange("Customized date");
            EnterFromDate(from);
            EnterToDate(to);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }

        internal void PopulateGrid(string status, string from, string to, string dateType)
        {
            EnterFromDate(from);
            EnterToDate(to);
            SelectValueTableDropDown(FieldNames.DateType, dateType);
            SelectValueTableDropDown(FieldNames.Status, status);
            LoadDataOnGrid();
        }

        internal void LoadDataOnGrid(string dateType, string currency, string from, string to)
        {
            EnterFromDate(from);
            EnterToDate(to);
            SelectValueTableDropDown(FieldNames.DateType, dateType);
            SelectValueTableDropDown(FieldNames.Currency, currency);
            LoadDataOnGrid();
        }

        internal void LoadDataOnGrid(string dealer, string fleet, string status, string dateType, string dateRange, string from, string to, string currency, string resolutionInformation)
        {
            if (!string.IsNullOrEmpty(dealer))
            {
                SelectFirstRowMultiSelectDropDown(FieldNames.Dealer, FieldNames.AccountCode, dealer);
            }
            if (!string.IsNullOrEmpty(fleet))
            {
                SelectFirstRowMultiSelectDropDown(FieldNames.Fleet, FieldNames.AccountCode, fleet);
            }
            if (!string.IsNullOrEmpty(status))
            {
                SelectValueByScroll(FieldNames.Status, status);
            }
            if (!string.IsNullOrEmpty(dateType))
            {
                SelectValueByScroll(FieldNames.DateType, status);
            }
            if (!string.IsNullOrEmpty(dateRange))
            {
                SelectValueByScroll(FieldNames.DateRange, status);
            }
            if (!string.IsNullOrEmpty(from))
            {
                EnterFromDate(from);
            }
            if (!string.IsNullOrEmpty(to))
            {
                EnterToDate(to);
            }
            if (!string.IsNullOrEmpty(currency))
            {
                SelectValueByScroll(FieldNames.Currency, currency);
            }
            if (!string.IsNullOrEmpty(resolutionInformation))
            {
                SelectValueByScroll(FieldNames.ResolutionInformation, resolutionInformation);
            }

            LoadDataOnGrid();
        }
    }
}
