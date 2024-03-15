using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_CorConnect.src.main.com.corcentric.test.pageobjects.helpers;

namespace AutomationTesting_CorConnect.PageObjects
{
    internal class Commons : Page
    {
        internal Commons(IWebDriver webDriver, string page) : base(webDriver, page)
        {
            LoggingHelper.InitializePage(page);
            GetGridXpath(page);
        }

        internal void ClickSearch()
        {
            ButtonClick(ButtonsAndMessages.Search);
        }

        /// <summary>
        /// Click On Field Label
        /// </summary>
        internal void ClickFieldLabel(string fieldCaption)
        {
            fieldCaption = RenameMenuField(fieldCaption);
            var container = driver.FindElement(By.XPath("//span[text()='" + fieldCaption + "']"));
            container.Click();
        }

        internal void EnterFromDate(string Date)
        {
            if (String.IsNullOrEmpty(Date))
            {
                Date = CommonUtils.GetCurrentDate();
            }
            EnterTextAfterClear("From", Date);
        }

        internal void EnterStartFromDate(string Date)
        {
            if (String.IsNullOrEmpty(Date))
            {
                Date = CommonUtils.GetCurrentDate();
            }
            EnterTextAfterClear("Start Date From:", Date);
        }

        internal void EnterToDate(string Date)
        {
            if (String.IsNullOrEmpty(Date))
            {
                Date = CommonUtils.GetCurrentDate();
            }

            EnterTextAfterClear("To", Date);
        }

        internal void EnterStartDateTo(string Date)
        {
            if (String.IsNullOrEmpty(Date))
            {
                Date = CommonUtils.GetCurrentDate();
            }

            EnterTextAfterClear("Start Date To:", Date);
        }

        internal void EnterDateInFromDate(string Date)
        {
            if (String.IsNullOrEmpty(Date))
            {
                Date = CommonUtils.GetCurrentDate();
            }
            EnterTextAfterClear("From Date", Date);
        }

        internal void EnterDateInToDate(string Date)
        {
            if (String.IsNullOrEmpty(Date))
            {
                Date = CommonUtils.GetCurrentDate();
            }
            EnterTextAfterClear("To Date", Date);
        }

        internal void EnterFromDate(DateTime Date)
        {
            EnterTextAfterClear("From", CommonUtils.ConvertDate(Date));
        }

        internal void EnterToDate(DateTime Date)
        {
            EnterTextAfterClear("To", CommonUtils.ConvertDate(Date));
        }

        internal void ClickNew()
        {
            gridHelper.ClickNew();
        }

        internal void ClickEdit()
        {
            gridHelper.ClickEdit();
        }

        internal void ClickDelete()
        {
            gridHelper.ClickDelete();
        }

        internal void InsertEditGrid()
        {
            gridHelper.InsertEditGrid();
        }

        internal bool IsButtonEnabled(string buttonCaption)
        {
            return gridHelper.IsButtonEnabled(buttonCaption);
        }

        internal string GetEditMsg()
        {
            return gridHelper.GetEditMsg();
        }

        internal void UpdateEditGrid()
        {
            gridHelper.UpdateEditGrid();
        }

        internal void CloseEditGrid()
        {
            gridHelper.CloseEditGrid();
        }

        internal bool IsAnyDataOnGrid()
        {
            return gridHelper.IsAnyData();
        }

        internal bool IsAnyDataOnNestedGrid()
        {
            return gridHelper.IsAnyDataOnNestedGrid();
        }

        internal void GridLoad()
        {
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal void LoadDataOnGrid()
        {
            GridLoad();
        }


        ///<summary>
        ///Verify Data on Grid for the Dealer Inv #
        ///</summary>

        public bool VerifyDataOnGrid(string DealerInvoiceNumber)
        {
            if (IsAnyDataOnGrid() && GetRowCountCurrentPage() > 0)
            {
                if (DealerInvoiceNumber == GetFirstRowData(TableHeaders.DealerInv__spc))
                {
                    return true;
                }
            }
            return false;
        }

        public bool VerifyFilterDataOnGridByHeader(string headerName, string value)
        {
            int rowCountCurrentPg = GetRowCountCurrentPage();
            if (rowCountCurrentPg == 0 && string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            if (IsAnyDataOnGrid() && rowCountCurrentPg > 0)
            {
                if (value == GetFirstRowData(headerName))
                {
                    return true;
                }
            }
            return false;
        }

        public bool VerifyFilterDataOnGridByNestedHeader(string headerName, string value, int tableNumber = 1)
        {
            int rowCountCurrentPg = GetRowCountNestedPage(tableNumber);
            if (rowCountCurrentPg == 0 && string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            if (IsAnyDataOnGrid() && rowCountCurrentPg > 0)
            {
                if (value == GetFirstRowDataNestedTable(headerName))
                {
                    return true;
                }
            }
            return false;
        }

        public void SelectValueTableDropDown(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SelectValue(GetElement(caption, section), searchValue);
            }
        }

        internal void EnterTextDropDown(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.EnterTextDropDown(GetElement(caption, section), searchValue);
            }
        }

        internal List<string> ValidateTableHeaders(params string[] headerNames)
        {
            //return gridHelper.ValidateHeaders(headerNames);
            return new List<string>();
        }

        //internal List<string> ValidateTableHeadersFromFile(string pageName)
        //{
        //    if (CommonUtils.GetClientLower() != applicationContext.ApplicationContext.GetInstance().DefaultClient)
        //    {
        //        //return gridHelper.ValidateHeadersFromFile(pageName);
        //        return new List<string>();
        //    }
        //    else
        //    {
        //        return new List<string>();
        //    }
        //}

        internal List<string> ValidateTableHeadersFromFile(bool checkClickSearchText = true)
        {
            if (CommonUtils.GetClientLower() != applicationContext.ApplicationContext.GetInstance().DefaultClient)
            {
                return gridHelper.ValidateHeadersFromFile(PageName, checkClickSearchText);
            }
            else
            {
                return new List<string>();
            }
        }

        internal string ValidateDelete()
        {
            ClickAnchorbutton(TableHeaders.Commands, ButtonsAndMessages.Delete, false);
            return GetAlertMessage();
        }

        internal string DeleteEditField()
        {
            return gridHelper.DeleteEditField();
        }

        internal void FilterTable(string headerName, string value)
        {
            gridHelper.Filter(headerName, value);
        }

        internal void FilterTableByColumnCount(string headerName, string value)
        {
            gridHelper.FilterTableByColumnCount(headerName, value);
        }

        internal void ClearFilter()
        {
            gridHelper.ClearFilter();
        }

        internal void ClearNestedFilter(int tableNumber)
        {
            gridHelper.ClearNestedFilter(tableNumber: tableNumber);
        }

        internal void ClearSelection()
        {
            gridHelper.ClearSelection();
        }

        internal void ReleaseInvoices(out string msg1, out string msg2)
        {
            gridHelper.ReleaseInvoices(out msg1, out msg2);
        }

        internal void MoveToHistory(out string msg1, out string msg2)
        {
            gridHelper.InvoiceMoveToHistory(out msg1, out msg2);
        }

        internal void ResetFilter()
        {
            gridHelper.ResetFilter();
        }

        internal void ResetNestedFilter(int tableNumber)
        {
            gridHelper.ResetNestedFilter(tableNumber: tableNumber);
        }

        ///<summary>
        ///Return first row data by header Name
        ///</summary>
        internal string GetFirstRowData(string headerName)
        {
            return gridHelper.GetElementByIndex(headerName);
        }

        ///<summary>
        ///Return number of Rows on Grid
        ///</summary>
        internal int GetRowCount()
        {
            return gridHelper.GetRowCount();
        }

        ///<summary>
        ///Return number of Rows on Current Page of Grid
        ///</summary>
        internal int GetRowCountCurrentPage()
        {
            return gridHelper.GetRowCountCurrentPage();
        }

        internal int GetRowCountNestedPage(int tableNumber = 1)
        {
            return gridHelper.GetRowCountNestedPage(tableNumber: tableNumber);
        }

        internal List<string> ValidateTableHeaders(string page)
        {
            //return gridHelper.ValidateHeaders(page);
            return new List<string>();
        }

        internal bool IsNestedGridOpen(int index = 1)
        {
            return gridHelper.IsNestedGridOpen(index);
        }

        internal bool IsNestedGridOpenByLevel(int index, int level)
        {
            return gridHelper.IsNestedGridOpenByLevel(2, level);
        }

        internal bool IsNestedGridClosed()
        {
            return gridHelper.IsNestedGridClosed(1);
        }

        internal bool IsGroupGridOpen()
        {
            return gridHelper.IsGroupGridOpen(1);
        }

        internal string GetValueDropDown(string caption)
        {
            return tableDropDownHelper.GetValue(GetElement(caption));
        }

        internal bool IsDataExistsInDropdown(string caption)
        {
            return tableDropDownHelper.IsDataExistsInDropdown(GetElement(caption));
        }

        internal void SearchAndSelectValue(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SearchAndSelectValue(GetElement(caption, section), searchValue);
            }
        }

        internal void SearchAndSelectValueWithoutLoadingMsg(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SearchAndSelectValue(GetElement(caption, section), searchValue, false);
            }
        }

        internal void SearchAndSelectValueAfterOpen(string caption, string searchValue, string section = null)
        {
            tableDropDownHelper.SearchAndSelectValueAfterOpen(GetElement(caption, section), searchValue);
        }

        /// <summary>
        /// Select Value After Searching and without clearing DropDown
        /// </summary>
        /// <returns>returns true or false</returns>
        internal void SearchAndSelectValueAfterOpenWithoutClear(string caption, string searchValue)
        {
            tableDropDownHelper.SearchAndSelectValueAfterOpen(GetElement(caption), searchValue, false);
        }

        internal List<string> GetElementsList(string header)
        {
            return gridHelper.GetElementsList(header);
        }

        /// <summary>
        /// Select First Row of DropDown
        /// </summary>
        internal void SelectValueFirstRow(string caption, string section = null)
        {
            tableDropDownHelper.SelectValueFirstRow(GetElement(caption, section), true);
        }

        /// <summary>
        /// Select  MultiSelect First Row of DropDown
        /// </summary>
        internal void SelectValueMultiSelectFirstRow(string caption, string section = null)
        {
            multiSelectHelper.SelectFirstRow(GetElement(caption, section));
        }

        /// <summary>
        /// Select First Row of DropDown
        /// <para>Opens Drop down by Clicking input field</para>
        /// </summary>
        internal void SelectValueFirstRowOpenByFieldClick(string caption, string section = null)
        {
            tableDropDownHelper.SelectValueFirstRow(GetElement(caption, section), false);
        }

        internal List<string> ValidateGridButtons(params string[] buttonCaptions)
        {
            return gridHelper.ValidateButtons(true, true, buttonCaptions);
        }

        internal List<string> ValidateGridButtonsWithoutExport(params string[] buttonCaptions)
        {
            return gridHelper.ValidateButtons(false, true, buttonCaptions);
        }

        internal List<string> ValidateNestedGridButtonsWithoutExport(params string[] buttonCaptions)
        {
            return gridHelper.NestedValidateButtons(false, true, buttonCaptions);
        }

        internal List<string> ValidateNestedGridButtons(params string[] buttonCaptions)
        {
            return gridHelper.NestedValidateButtons(true, true, buttonCaptions);
        }

        internal List<string> ValidateTableDetails(bool validatePagingDetails, bool validateGroupBy)
        {
            return gridHelper.ValidateTableDetails(validatePagingDetails, validateGroupBy);
        }

        internal List<string> ValidateTableDetailsWithSelectedCounter(bool validatePagingDetails, bool validateGroupBy)
        {
            return gridHelper.ValidateTableDetails(validatePagingDetails, validateGroupBy, true);
        }

        public void ClickHyperLinkOnGrid(string header, bool openPopup = true)
        {           
            gridHelper.ClickHyperLinkOnGrid(header, openPopup);
        }

        internal void WaitForEditGrid()
        {
            gridHelper.WaitForEditGrid();
        }

        internal void ClickAnchorbutton(string headerName, string button, bool waitForDataRow = true)
        {
            gridHelper.ClickAnchorButton(headerName, button, waitForDataRow);
        }

        public bool ValidateHyperlink(string header)
        {
            return gridHelper.ValidateHyperlink(header);
        }

        public bool ValidateHyperlinkNestedGrid(string header)
        {
            return gridHelper.ValidateHyperlinkNestedGrid(header);
        }

        public void SelectValueMultipleColumns(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SelectValueMultipleColumns(GetElement(caption, section), searchValue, 1);
            }
        }

        public void SearchAndSelectValueMultipleColumnAfterOpen(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SearchAndSelectValueMultipleColumnAfterOpen(GetElement(caption, section), searchValue);
            }
        }

        internal void SelectFirstRowMultiSelectDropDown(string caption, string headerName, string value)
        {
            multiSelectHelper.SelectFirstRow(GetElement(caption), headerName, value);
        }

        internal bool VerifyDataInMultiSelectDropDown(string caption, string headerName, string value)
        {
            return multiSelectHelper.VerifyDataInDropDown(GetElement(caption), headerName, value);
        }

        internal void SelectFirstRowMultiSelectDropDown(string caption)
        {
            multiSelectHelper.SelectFirstRow(GetElement(caption));
        }

        internal void SelectValueMultiSelectDropDown(string caption, string headerName, string value)
        {
            multiSelectHelper.SelectValue(GetElement(caption), headerName, value);
        }

        internal void ClearSelectionMultiSelectDropDown(string caption)
        {
            multiSelectHelper.ClearSelection(GetElement(caption));
        }

        internal void SelectAllRowsMultiSelectDropDown(string caption)
        {
            multiSelectHelper.SelectAllRows(GetElement(caption));
        }

        /// <summary>
        /// Open DropDown
        /// </summary>
        internal void OpenDropDown(string caption, string section = null)
        {
            tableDropDownHelper.OpenDropDown(GetElement(caption, section));
        }

        /// <summary>
        /// Open MultiSelect DropDown
        /// </summary>
        internal void OpenMultiSelectDropDown(string caption)
        {
            multiSelectHelper.OpenDropDown(GetElement(caption));
        }

        /// <summary>
        /// Open Dropwn By Clicking on Input Field
        /// </summary>
        internal void OpenDropDownByInputField(string caption)
        {
            tableDropDownHelper.OpenDropDownByInputField(GetElement(caption));
        }

        /// <summary>
        /// Check if DropDown is Closed
        /// </summary>
        /// <returns>returns true or false</returns>
        internal bool IsDropDownClosed(string caption, string section = null)
        {
            return tableDropDownHelper.IsDropDownClosed(GetElement(caption, section));
        }

        /// <summary>
        /// Check if MultiSelect DropDown is Closed
        /// </summary>
        /// <returns>returns true or false</returns>
        internal bool IsMultiSelectDropDownClosed(string caption)
        {
            return multiSelectHelper.IsDropDownClosed(GetElement(caption));
        }


        /// <summary>
        /// Open Date Picker
        /// </summary>
        internal void OpenDatePicker(string caption)
        {
            datePickerHelper.OpenDatePicker(GetElement(caption));
        }

        /// <summary>
        /// Check If date Picker Closed
        /// </summary>
        /// <returns>true or false</returns>
        internal bool IsDatePickerClosed(string caption)
        {
            return datePickerHelper.IsDatePickerClosed(GetElement(caption));
        }

        /// <summary>
        /// Selects first date
        /// </summary>
        internal void SelectDate(string caption)
        {
            datePickerHelper.SelectDate(GetElement(caption));
        }

        public string SelectValueTableDropDown(string caption)
        {
            return tableDropDownHelper.SelectValue(GetElement(caption));
        }

        internal List<string> ValidateNestedTableHeaders(params string[] headerNames)
        {
            //return gridHelper.ValidateNestedTableHeaders(headerNames);
            return new List<string>();
        }

        internal List<string> ValidateNestedTableHeadersByLevel(int level, params string[] headerNames)
        {
            //return gridHelper.ValidateNestedTableHeadersByLevel(level, headerNames);
            return new List<string>();
        }

        internal string GetNestedPageLabel()
        {
            return gridHelper.GetNestedPageLabel();
        }

        internal List<string> ValidateNestedGridTabs(params string[] tabNames)
        {
            return gridHelper.ValidateNestedGridTabs(tabNames);
        }

        internal void ClickNestedGridTab(string tabName)
        {
            gridHelper.ClickNestedGridTab(tabName);
        }

        internal string GetFirstRowDataNestedTable(string headerName, int tableNumber = 1)
        {
            try
            {
                return gridHelper.GetNestedTableElementByIndex(headerName, tableNumber: tableNumber);
            }
            catch (StaleElementReferenceException)
            {
                return gridHelper.GetNestedTableElementByIndex(headerName, tableNumber: tableNumber);
            }
        }

        internal void FilterNestedTable(string headerName, string value, int tableNumber = 1)
        {
            gridHelper.FilterNestedTable(headerName, value, tableNumber);
        }

        internal void ClickRadioButton()
        {
            gridHelper.ClickRadioButton(1);
        }

        internal void SetFilterCreiteria(string headerName, string filterCriteria)
        {
            gridHelper.SetFilterCreiteria(headerName, filterCriteria);
        }

        internal List<string> GetHeaderNamesMultiSelectDropDown(string element)
        {
            return multiSelectHelper.GetHeaderNames(GetElement(element));
        }

        internal List<string> GetHeaderNamesTableDropDown(string element)
        {
            return tableDropDownHelper.GetHeaderNames(GetElement(element));
        }

        internal int GetTotalCountMultiSelectDropDown(string element)
        {
            return multiSelectHelper.GetTotalCount(GetElement(element));
        }

        internal string GetSelectedRowCountMultiSelectDropDown(string element)
        {
            return multiSelectHelper.GetSelectedRowCount(GetElement(element));
        }

        internal bool IsNextPageMultiSelectDropdown(string element)
        {
            return multiSelectHelper.IsNextPage(GetElement(element));
        }

        public bool VerifyValueDropDown(string caption, params string[] values)
        {
            return tableDropDownHelper.VerifyValue(GetElement(caption), values);
        }

        public bool VerifyValueMultiSelectDropDown(string caption, params string[] values)
        {
            return tableDropDownHelper.VerifyValueMultiSelectDropDown(GetElement(caption), values);
        }

        public bool VerifyValueDropDownByEdit(string caption, string section, params string[] values)
        {
            return tableDropDownHelper.VerifyValue(GetElement(caption, section), values);
        }

        public bool VerifyValueDropDownScrollable(string caption, params string[] values)
        {
            return tableDropDownHelper.VerifyValueScrollable(GetElement(caption), values);
        }

        public bool VerifyDataMultiSelectDropDown(string caption, params string[] values)
        {
            return multiSelectHelper.VerifyData(GetElement(caption), values);
        }

        internal List<string> ValidateNestedTableDetails(bool validatePagingDetails, bool validateGroupBy)
        {
            return gridHelper.ValidateNestedTableDetails(validatePagingDetails, validateGroupBy);
        }

        internal void ClickNewButton()
        {
            gridHelper.ClickNewButton();
        }

        internal void WaitForEditDialog()
        {
            gridHelper.WaitForEditDialog();
        }

        internal void ClickInsertButton()
        {
            gridHelper.ClickInsertButton();
        }

        internal string GetMessageOfPerformedOperation()
        {
            return gridHelper.GetMessageOfPerformedOperation();
        }

        internal void ClosePage(string pageName)
        {
            gridHelper.ClosePage(pageName);
            gridHelper.WaitForWelcomePage();
        }

        internal void SetDropdownTableSelectInputValue(string caption, string value, string section = null)
        {
            tableDropDownHelper.SetDropdownTableSelectInputValue(GetElement(caption, section), value);
        }

        internal bool SaveBookmark(string bmName, string bmDesc)
        {
            return bookmarkDialogHelper.SaveBookmark(bmName, bmDesc);

        }

        internal bool IsNextPage(bool isMainTable = true)
        {
            return gridHelper.IsNextPage(isMainTable);
        }
    
        internal bool IsNextPageNestedGrid(bool isMainTable = false)
        {
            return gridHelper.IsNextPageNestedGrid(isMainTable);
        }
        public void GoToPage(int num, bool isMainTable = true)
        {
            gridHelper.GoToPage(isMainTable, num);
        }

        public void GoToPageNestedGrid(int num, string tabName, int tableNumber = 1)
        {
            gridHelper.GoToPageNestedGrid(num, tabName, tableNumber);
        }
        internal void ClickSelectAllCheckBox(string headerName)
        {
            gridHelper.ClickSelectAllCheckBox(headerName);
        }
        internal void ClickGridButtons(string value)
        {
            gridHelper.ClickGridButtons(value);
        }

        public void VerifyNextPage(int num, bool isMainTable = true)
        {
            gridHelper.VerifyNextPage(isMainTable, num);
        }
        internal void ClickTableHeader(string headerName)
        {
            gridHelper.ClickTableHeader(headerName);
        }

        internal void CheckTableRowCheckBoxByIndex(int rowNum)
        {
            gridHelper.CheckTableRowCheckBoxByIndex(rowNum);
        }

        internal void CheckNestedTableRowCheckBoxByIndex(int rowNum, int tableNumber = 1)
        {
            gridHelper.CheckNestedTableRowCheckBoxByIndex(rowNum, tableNumber: tableNumber);
        }

        public bool IsTableHeaderExists(string headerName)
        {
            return gridHelper.IsTableHeaderExists(headerName);
        }

        internal bool IsNestedTableRowCheckBoxUnChecked(int tableNumber = 1)
        {
            return gridHelper.IsNestedTableRowCheckBoxUnChecked(tableNumber: tableNumber);
        }

        internal bool IsNestedTableRowCheckBoxDisabled(int tableNumber = 1)
        {
            return gridHelper.IsNestedTableRowCheckBoxDisabled(tableNumber: tableNumber);
        }

        internal void WaitForStalenessTable()
        {
            gridHelper.WaitForStalenessTable();
        }

        internal int GetGridTotalRowCount()
        {
            return gridHelper.GetTotalCount();
        }

        internal void SelectValueByScroll(string caption, string value, string section = null)
        {
            tableDropDownHelper.SelectValueByScroll(GetElement(caption, section), value);
        }

        internal string GetPageCounterTotal()
        {
            return gridHelper.GetPageCounterTotal();
        }

        internal void OpenMultipleColumnsDropDown(string caption, string searchValue)
        {
            tableDropDownHelper.OpenMultipleColumnsDropDown(GetElement(caption), searchValue, false);
        }

        internal string GetFirstValueOfHeaderMultipleColumnsDropdown(string caption, string headerName)
        {
            return tableDropDownHelper.GetFirstValueOfHeaderMultipleColumns(GetElement(caption), headerName);
        }

        internal bool VerifyDataInMultipleColumnDropdown(string caption, string headerName, string value)
        {
            return tableDropDownHelper.VerifyDataInMultipleColumnDropdown(GetElement(caption), headerName, value);
        }

        internal List<string> VerifyDataMultipleColumnsDropdown(string pageName, string fieldName, string headerName, List<string> values)
        {
            List<string> errorMsgs = new List<string>();
            if (values == null || values.Count <= 0)
            {
                errorMsgs.Add($"No values in array to verify from dropdown for page: {pageName}");
                return errorMsgs;
            }

            int upperLimit = values.Count > 5 ? 4 : values.Count - 1;
            for (int i = 0; i <= upperLimit; i++)
            {
                string displayName = values[CommonUtils.GenerateRandom(0, values.Count - 1)];
                values.Remove(displayName);
                OpenMultipleColumnsDropDown(fieldName, displayName);
                if (GetFirstValueOfHeaderMultipleColumnsDropdown(fieldName, headerName) != displayName)
                {
                    errorMsgs.Add($"Page [{pageName}]: Value [{displayName}] not found in [{fieldName}] dropdown.");
                }
                ClickPageTitle();
                IsDropDownClosed(fieldName);
            }
            return errorMsgs;
        }

        internal List<string> VerifyDataNotInMultipleColumnsDropdown(string pageName, string fieldName, string headerName, List<string> values)
        {
            List<string> errorMsgs = new List<string>();
            if (values == null || values.Count <= 0)
            {
                errorMsgs.Add($"No values in array to verify from dropdown for page: {pageName}");
                return errorMsgs;
            }

            int upperLimit = values.Count > 5 ? 4 : values.Count - 1;
            for (int i = 0; i <= upperLimit; i++)
            {
                string displayName = values[CommonUtils.GenerateRandom(0, values.Count - 1)];
                values.Remove(displayName);
                OpenMultipleColumnsDropDown(fieldName, displayName);
                if (VerifyDataInMultipleColumnDropdown(fieldName, headerName, displayName))
                {
                    errorMsgs.Add($"Page [{pageName}]: Value [{displayName}] found in [{fieldName}] dropdown. (False negative case)");
                }
                ClickPageTitle();
                IsDropDownClosed(fieldName);
            }
            return errorMsgs;
        }

        internal List<string> VerifyDataMultiSelectDropdown(string pageName, string fieldName, string headerName, List<string> values)
        {
            List<string> errorMsgs = new List<string>();
            if (values == null || values.Count <= 0)
            {
                errorMsgs.Add($"No values in array to verify from dropdown [{fieldName}] for page: {pageName}");
                return errorMsgs;
            }

            int upperLimit = values.Count > 5 ? 4 : values.Count - 1;
            for (int i = 0; i <= upperLimit; i++)
            {
                string displayName = values[CommonUtils.GenerateRandom(0, values.Count - 1)];
                values.Remove(displayName);
                if (!VerifyDataInMultiSelectDropDown(fieldName, headerName, displayName))
                {
                    errorMsgs.Add($"Page [{pageName}]: Value [{displayName}] not found in [{fieldName}] dropdown.");
                }
                ClickPageTitle();
                IsMultiSelectDropDownClosed(fieldName);
            }
            return errorMsgs;
        }

        internal List<string> VerifyDataNotInMultiSelectDropdown(string pageName, string fieldName, string headerName, List<string> values)
        {
            List<string> errorMsgs = new List<string>();
            if (values == null || values.Count <= 0)
            {
                errorMsgs.Add($"No values in array to verify from dropdown [{fieldName}] for page: {pageName}");
                return errorMsgs;
            }

            int upperLimit = values.Count > 5 ? 4 : values.Count - 1;
            for (int i = 0; i <= upperLimit; i++)
            {
                string displayName = values[CommonUtils.GenerateRandom(0, values.Count - 1)];
                values.Remove(displayName);
                if (VerifyDataInMultiSelectDropDown(fieldName, headerName, displayName))
                {
                    errorMsgs.Add($"Page [{pageName}]: Value [{displayName}] found in [{fieldName}] dropdown. (False negative case)");
                }
                ClickPageTitle();
                IsMultiSelectDropDownClosed(fieldName);
            }
            return errorMsgs;
        }

        internal bool IsAnyRowsInDropdown(string element, string section = null)
        {
            return tableDropDownHelper.IsAnyRowsInDropdown(GetElement(element, section));
        }

        internal void OpenAndFilterMultiSelectDropdown(string caption, string headerName, string value, string section = null)
        {
            multiSelectHelper.OpenAndFilterDropdown(GetElement(caption, section), headerName, value);
        }

        internal List<string> VerifyLocationCountMultiSelectDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName)
        {
            List<string> errorMsgs = new List<string>();
            if (IsElementVisible(FieldNames.SearchType) && GetValueDropDown(FieldNames.SearchType) == "Quick Search")
            {
                SwitchToAdvanceSearch();
            }
            OpenAndFilterMultiSelectDropdown(dropdownCaption, FieldNames.LocationType, locationType.DisplayName);
            int locationCountUI = GetTotalCountMultiSelectDropDown(dropdownCaption);
            List<EntityDetails> entityDetails = string.IsNullOrEmpty(userName) ? CommonUtils.GetActiveLocations(locationType, dropdownUserType) : CommonUtils.GetActiveLocations(locationType, userName);
            UserType currentUserType = appContext.ImpersonatedUserData == null ? appContext.UserData.Type : appContext.ImpersonatedUserData.Type;
            if (locationCountUI != entityDetails.Count)
            {
                errorMsgs.Add($"{currentUserType.Name} User: [{dropdownCaption}] dropdown location [{locationType.DisplayName}] count mismatch. UI Count = {locationCountUI}, DB Count = {entityDetails.Count}");
            }
            return errorMsgs;
        }

        internal List<string> VerifyLocationNotInMultiSelectDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName)
        {
            List<string> errorMsgs = new List<string>();
            if (IsElementVisible(FieldNames.SearchType) && GetValueDropDown(FieldNames.SearchType) == "Quick Search")
            {
                SwitchToAdvanceSearch();
            }
            OpenAndFilterMultiSelectDropdown(dropdownCaption, FieldNames.LocationType, locationType.DisplayName);
            int locationCountUI = GetTotalCountMultiSelectDropDown(dropdownCaption);
            UserType currentUserType = appContext.ImpersonatedUserData == null ? appContext.UserData.Type : appContext.ImpersonatedUserData.Type;
            if (locationCountUI != 0)
            {
                errorMsgs.Add($"{currentUserType.Name} User: Location [{locationType.DisplayName}] found in [{dropdownCaption}] dropdown. Location Count = {locationCountUI}");
            }
            return errorMsgs;
        }

        internal List<string> VerifyLocationMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName)
        {
            return VerifyLocationMultipleColumnsDropdown(dropdownCaption, locationType, dropdownUserType, userName, canLocationExist: true);
        }

        internal List<string> VerifyLocationNotInMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName)
        {
            return VerifyLocationMultipleColumnsDropdown(dropdownCaption, locationType, dropdownUserType, userName, canLocationExist: false);
        }

        private List<string> VerifyLocationMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName, int verifyNoOfRows = 1, bool canLocationExist = true, bool hasClearButton = false)
        {
            List<string> errorMsgs = new List<string>();
            if (IsElementVisible(FieldNames.SearchType) && GetValueDropDown(FieldNames.SearchType) == "Quick Search")
            {
                SwitchToAdvanceSearch();
            }
            List<EntityDetails> entityDetails = string.IsNullOrEmpty(userName) ? CommonUtils.GetActiveLocations(locationType, dropdownUserType) : CommonUtils.GetActiveLocations(locationType, userName);
            if (entityDetails.Count > 0)
            {
                UserType currentUserType = appContext.ImpersonatedUserData == null ? appContext.UserData.Type : appContext.ImpersonatedUserData.Type;
                string displayName = string.Empty;
                for (int i = 1; i <= 1; i++)
                {
                    displayName = entityDetails[CommonUtils.GenerateRandom(0, entityDetails.Count - 1)].DisplayName;
                    OpenMultipleColumnsDropDown(dropdownCaption, displayName);
                    if (canLocationExist)
                    {
                        if (!VerifyDataInMultipleColumnDropdown(dropdownCaption, FieldNames.DisplayName, displayName))
                        {
                            errorMsgs.Add($"{currentUserType.Name} User: [{locationType.DisplayName}] location [{displayName}] not found in [{dropdownCaption}] dropdown");
                        }
                    }
                    else
                    {
                        if (VerifyDataInMultipleColumnDropdown(dropdownCaption, FieldNames.DisplayName, displayName))
                        {
                            errorMsgs.Add($"{currentUserType.Name} User: [{locationType.DisplayName}] location [{displayName}] found in [{dropdownCaption}] dropdown");
                        }
                    }
                    ClickPageTitle();
                    IsDropDownClosed(dropdownCaption);
                }
            }
            return errorMsgs;
        }

        internal List<string> CreateDispute(string invoiceToSearch, string hyperLinkHeader)
        {
            var errorMsgs = new List<string>();
            try
            {
                ClickHyperLinkOnGrid(hyperLinkHeader);
                InvoiceOptionsPage popupPage = new InvoiceOptionsPage(driver);
                popupPage.SwitchIframe();
                popupPage.Click(ButtonsAndMessages.InvoiceOptions);
                driver.SwitchTo().Frame(1);
                driver.SwitchTo().Frame(0);
                InvoiceOptionsAspx invoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                Task t = Task.Run(() => invoiceOptionsAspxPage.WaitForStalenessOfElement(FieldNames.Notes));
                invoiceOptionsAspxPage.SimpleSelectOptionByText(FieldNames.Reason, "Not Our Invoice");
                t.Wait();
                t.Dispose();
                invoiceOptionsAspxPage.EnterText(FieldNames.Notes, "This Invoice does not belong to us");
                invoiceOptionsAspxPage.UploadFile("Upload file", "UploadFiles//SamplePDF.pdf");
                invoiceOptionsAspxPage.Click(ButtonsAndMessages.Submit);
                invoiceOptionsAspxPage.WaitForLoadingGrid();
                invoiceOptionsAspxPage.ClosePopupWindow();
                SwitchToMainWindow();
                FilterTable(hyperLinkHeader, invoiceToSearch);
                if (GetFirstRowData(TableHeaders.TransactionStatus) != "Current-Disputed")
                {
                    errorMsgs.Add(ErrorMessages.DisputeCreationFailed);
                }
                return errorMsgs;
            }
            catch (Exception e)
            {
                errorMsgs.Add(ErrorMessages.DisputeCreationFailed + " Reason: " + e.Message);
                return errorMsgs;
            }
        }

        internal List<string> ResolveDispute(string invoiceToSearch, string hyperLinkHeader)
        {
            var errorMsgs = new List<string>();
            try
            {
                ClickHyperLinkOnGrid(hyperLinkHeader);
                InvoiceOptionsPage popupPage = new InvoiceOptionsPage(driver);
                popupPage.SwitchIframe();
                popupPage.Click(ButtonsAndMessages.InvoiceOptions);
                driver.SwitchTo().Frame(1);
                driver.SwitchTo().Frame(0);
                InvoiceOptionsAspx invoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                Task t = Task.Run(() => invoiceOptionsAspxPage.WaitForStalenessOfElement("Resolution Note"));
                invoiceOptionsAspxPage.SimpleSelectOptionByText("Action", "Resolve Dispute");
                t.Wait();
                t.Dispose();
                invoiceOptionsAspxPage.EnterText("Resolution Note", "Issue Solved");
                invoiceOptionsAspxPage.Click(ButtonsAndMessages.Save);
                invoiceOptionsAspxPage.WaitForLoadingGrid();
                invoiceOptionsAspxPage.ClosePopupWindow();
                SwitchToMainWindow();
                FilterTable(hyperLinkHeader, invoiceToSearch);
                if (GetFirstRowData(TableHeaders.TransactionStatus) != "Current-Resolved")
                {
                    errorMsgs.Add(ErrorMessages.DisputeNotResolved);
                }
                return errorMsgs;
            }
            catch (Exception e)
            {
                errorMsgs.Add(ErrorMessages.DisputeNotResolved + " Reason: " + e.Message);
                return errorMsgs;
            }
        }

        internal List<string> ReDispute(string invoiceToSearch, string hyperLinkHeader)
        {
            var errorMsgs = new List<string>();
            try
            {
                ClickHyperLinkOnGrid(hyperLinkHeader);
                InvoiceOptionsPage popupPage = new InvoiceOptionsPage(driver);
                popupPage.SwitchIframe();
                popupPage.Click(ButtonsAndMessages.InvoiceOptions);
                driver.SwitchTo().Frame(1);
                driver.SwitchTo().Frame(1);
                InvoiceOptionsAspx invoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                if (invoiceOptionsAspxPage.GetSelectedValueSimpleSelect("Reason") != "Pricing Error")
                {
                    Task t = Task.Run(() => invoiceOptionsAspxPage.WaitForStalenessOfElement(FieldNames.Notes));
                    invoiceOptionsAspxPage.SimpleSelectOptionByText("Reason", "Pricing Error");
                    t.Wait();
                    t.Dispose();
                }
                invoiceOptionsAspxPage.EnterText(FieldNames.Notes, "This Pricing is incorrect");
                invoiceOptionsAspxPage.UploadFile("Upload file", "UploadFiles//SamplePDF.pdf");
                invoiceOptionsAspxPage.Click(ButtonsAndMessages.ReDispute);
                invoiceOptionsAspxPage.WaitForLoadingGrid();
                invoiceOptionsAspxPage.ClosePopupWindow();
                SwitchToMainWindow();
                FilterTable(hyperLinkHeader, invoiceToSearch);
                if (GetFirstRowData(TableHeaders.TransactionStatus) != "Current-Disputed")
                {
                    errorMsgs.Add(ErrorMessages.RedisputeOperationFailed);
                }
                return errorMsgs;
            }
            catch (Exception e)
            {
                errorMsgs.Add(ErrorMessages.RedisputeOperationFailed + " Reason: " + e.Message);
                return errorMsgs;
            }
        }

        internal List<string> ReverseInvoice(string invoiceToSearch)
        {
            var errorMsgs = new List<string>();
            try
            {
                InvoiceOptionsPage popupPage = new InvoiceOptionsPage(driver);
                OffsetTransactionPage OffsetTransactionPopUpPage = popupPage.CreateRebill();
                OffsetTransactionPopUpPage.Click(ButtonsAndMessages.CreateAReversal);
                Assert.IsTrue(OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.CreateAReversal));
                OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.ReversalReason);
                Task t = Task.Run(() => OffsetTransactionPopUpPage.WaitForStalenessOfElement("Dealer For Reversal"));
                OffsetTransactionPopUpPage.SelectValueTableDropDown(FieldNames.ReversalReason, "Billed twice");
                t.Wait();
                t.Dispose();
                OffsetTransactionPopUpPage.EnterText(FieldNames.FleetOrDealerApprover, "Fleet");
                OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Reverse);
                if (!OffsetTransactionPopUpPage.IsTextVisible(ButtonsAndMessages.ReversalTransactionCompletedSuccessfully, true))
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed);
                }
                return errorMsgs;
            }
            catch (Exception e)
            {
                errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + e.Message);
                return errorMsgs;
            }
        }

        internal List<string> CloneInvoice(string hyperLinkHeader)
        {
            var errorMsgs = new List<string>();
            try
            {
                ClickHyperLinkOnGrid(hyperLinkHeader);
                InvoiceOptionsPage popupPage = new InvoiceOptionsPage(driver);
                OffsetTransactionPage OffsetTransactionPopUpPage = popupPage.CreateRebill();
                OffsetTransactionPopUpPage.Click(ButtonsAndMessages.RebillTheInvoice);
                if (!OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.RebillTheInvoice))
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + ButtonsAndMessages.RebillTheInvoice + " button not checked");
                    return errorMsgs;
                }
                OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.Comments);
                OffsetTransactionPopUpPage.EnterText(FieldNames.Comments, "Comments");
                OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Rebill);
                OffsetTransactionPopUpPage.SwitchToMainWindow();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
                SwitchToPopUp();
                CreateNewInvoicePage popupPageIE = new CreateNewInvoicePage(driver);
                popupPageIE.WaitForElementToBeVisible(ButtonsAndMessages.EditLineItem);
                popupPageIE.Click(ButtonsAndMessages.EditLineItem);
                popupPageIE.WaitForLoadingIcon();
                popupPageIE.SelectValueByScroll(FieldNames.Type, "Rental", ButtonsAndMessages.Edit);
                popupPageIE.WaitForLoadingIcon();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                if (popupPageIE.GetValue(FieldNames.Item, ButtonsAndMessages.Edit) != string.Empty)
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + ErrorMessages.FieldEmpty);
                    return errorMsgs;
                }
                string partNumber = CommonUtils.RandomString(6);
                popupPageIE.EnterText(FieldNames.Item, partNumber, ButtonsAndMessages.Edit);
                popupPageIE.EnterText(FieldNames.ItemDescription, partNumber, ButtonsAndMessages.Edit);
                popupPageIE.Click(ButtonsAndMessages.SaveLineItem);
                popupPageIE.WaitForLoadingIcon();
                popupPageIE.Click(ButtonsAndMessages.AddTax);
                popupPageIE.WaitForLoadingIcon();
                popupPageIE.SelectValueByScroll(FieldNames.TaxType, "QST");
                popupPageIE.SetValue(FieldNames.Amount, "2.00");
                popupPageIE.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
                popupPageIE.Click(ButtonsAndMessages.SaveTax);
                popupPageIE.WaitForLoadingIcon();
                if (popupPageIE.GetRowCount("Tax Info Table", true) != 1)
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + ErrorMessages.TaxInfoAdditionFailed);
                    return errorMsgs;
                }
                popupPageIE.UploadFile(0, "UploadFiles//SamplePDF.pdf");
                popupPageIE.Click(ButtonsAndMessages.UploadAttachments);
                popupPageIE.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);
                popupPageIE.Click(ButtonsAndMessages.DeleteLineItem);
                if ((popupPageIE.GetAlertMessage() != ButtonsAndMessages.DeleteLineItemAlert))
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + ErrorMessages.AlertMessageMisMatch);
                    return errorMsgs;
                }
                popupPageIE.AcceptAlert();
                popupPageIE.WaitForLoadingIcon();
                popupPageIE.Click(ButtonsAndMessages.DeleteTax);
                if ((popupPageIE.GetAlertMessage() != ButtonsAndMessages.ConfirmDeleteAlert))
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + ErrorMessages.AlertMessageMisMatch);
                    return errorMsgs;
                }
                popupPageIE.AcceptAlert();
                popupPageIE.WaitForLoadingIcon();
                popupPageIE.Click(ButtonsAndMessages.DeleteAttachment);
                if ((popupPageIE.GetAlertMessage() != ButtonsAndMessages.DeleteAttachmentAlert))
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + ErrorMessages.AlertMessageMisMatch);
                    return errorMsgs;
                }
                popupPageIE.AcceptAlert();
                popupPageIE.Click(ButtonsAndMessages.SubmitInvoice);
                popupPageIE.WaitForLoadingIcon();
                popupPageIE.Click(ButtonsAndMessages.Continue);
                popupPageIE.AcceptAlert(out string invoiceMsg);
                if (ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully != invoiceMsg)
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + ErrorMessages.AlertMessageMisMatch);
                    return errorMsgs;
                }
                if (!popupPageIE.WaitForPopupWindowToClose())
                {
                    errorMsgs.Add(ErrorMessages.InvoiceReversalFailed);
                }
                return errorMsgs;
            }
            catch (Exception e)
            {
                errorMsgs.Add(ErrorMessages.InvoiceReversalFailed + " Reason: " + e.Message);
                return errorMsgs;
            }
        }

        internal List<string> ValidateLeftGridSearchFields(string pageName)
        {
            var errorMsgs = new List<string>();
            if (!IsButtonVisible(ButtonsAndMessages.Search))
            {
                errorMsgs.Add(ErrorMessages.SearchButtonNotFound);
            }
            if (!IsButtonVisible(ButtonsAndMessages.Clear))
            {
                errorMsgs.Add(ErrorMessages.ClearButtonNotFound);
            }
            
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
           errorMsgs.AddRange(AreFieldsAvailable(pageName));

            return errorMsgs;
        }

        internal List<string> ValidateGridFilters(string tableHeader)
        {
            var errorMsgs = new List<string>();
            string headerValue = GetFirstRowData(tableHeader);
            FilterTable(tableHeader, headerValue);
            if (!VerifyFilterDataOnGridByHeader(tableHeader, headerValue))
            {
                errorMsgs.Add(ErrorMessages.NoRowAfterFilter);
            }
            ClearFilter();
            if (GetRowCountCurrentPage()! > 0)
            {
                errorMsgs.Add(ErrorMessages.ClearFilterNotWorking);
            }
            FilterTable(tableHeader, CommonUtils.RandomString(10));
            if (GetRowCountCurrentPage()! <= 0)
            {
                errorMsgs.Add(ErrorMessages.FilterNotWorking);
            }
            ResetFilter();
            if (GetRowCountCurrentPage()! > 0)
            {
                errorMsgs.Add(ErrorMessages.ResetNotWorking);
            }

            return errorMsgs;
        }

        internal List<string> ValidateNavigationToNextPage(bool validatePagingDetails)
        {
            var errorMsgs = new List<string>();
            try
            {
                if (IsNextPage())
                {
                    GoToPage(2);

                    VerifyNextPage(2);
                }
            }
            catch
            {
                errorMsgs.Add("Failed to naviagte to next page.");
            }
            return errorMsgs;
        }
    }
}