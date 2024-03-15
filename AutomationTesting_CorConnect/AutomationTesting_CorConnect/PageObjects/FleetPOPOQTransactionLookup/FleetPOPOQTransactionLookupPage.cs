using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.FleetPOPOQTransactionLookup
{
    internal class FleetPOPOQTransactionLookupPage : Commons
    {
        internal FleetPOPOQTransactionLookupPage(IWebDriver webDriver) : base(webDriver, Pages.FleetPOPOQTransactionLookup)
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
            WaitForGridLoad();
        }

        internal void PopulateGridWithDocumentNumber(string documentNumber)
        {
            EnterTextAfterClear(FieldNames.DocumentNumber, documentNumber);
            ClickSearch();
            WaitForGridLoad();
        }

        internal void LoadDataOnGrid(string fromDate, string toDate)
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
