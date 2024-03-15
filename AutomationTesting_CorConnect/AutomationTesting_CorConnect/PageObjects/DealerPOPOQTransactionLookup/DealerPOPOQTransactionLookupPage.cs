using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.DealerPOPOQTransactionLookup
{
    internal class DealerPOPOQTransactionLookupPage : Commons
    {
        internal DealerPOPOQTransactionLookupPage(IWebDriver webDriver) : base(webDriver, Pages.DealerPOPOQTransactionLookup)
        {
        }

        internal void PopulateGrid(string DocumentType, string DateType, string FromDate, string ToDate, string TransactionStatus)
        {
            SelectValueTableDropDown(FieldNames.DocumentType, DocumentType);
            SelectValueTableDropDown(FieldNames.DateType, DateType);
            EnterFromDate(FromDate);
            EnterToDate(ToDate);
            SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, TransactionStatus);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal void LoadDataOnDrid(string DocumentNumber)
        {
            EnterTextAfterClear(FieldNames.DocumentNumber, DocumentNumber);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal void PopulateGrid(string fromDate, string toDate)
        {
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void SelectDealerGroupFirstRow()
        {
            SelectValueFirstRow(FieldNames.DealerGroup);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void SelectLoadBookmarkFirstRow()
        {
            SelectValueFirstRow(FieldNames.LoadBookmark);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void SelectFleetGroupFirstRow()
        {
            SelectValueFirstRow(FieldNames.FleetGroup);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void SelectDateRangeFirstRow()
        {
            SelectValueFirstRowOpenByFieldClick(FieldNames.DateRange);
        }
    }
}
