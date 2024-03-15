using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Test_CorConnect.src.main.com.corcentric.test.pageobjects.helpers;

namespace AutomationTesting_CorConnect.PageObjects.StaticPages
{
    internal class PopUpPage : PageOperations<Dictionary<string, PageObject>>
    {
        private GridHelper gridHelper;
        private MultiSelectHelper multiSelectHelper;
        private TableDropDownHelper tableDropDownHelper;
        private ListBoxItemHelper listBoxItemHelper;
        private SimpleSelectHelper simpleSelectHelper;
        private DatePickerHelper datePickerHelper;

        internal PopUpPage(IWebDriver driver, string page) : base(driver)
        {
            LoggingHelper.InitializePage(page);
            PageElements = LoadElements<Dictionary<string, PageObject>>(page);
            gridHelper = new GridHelper(driver);
            tableDropDownHelper = new TableDropDownHelper(driver);
            multiSelectHelper = new MultiSelectHelper(driver);
            listBoxItemHelper = new ListBoxItemHelper(driver);
            simpleSelectHelper = new SimpleSelectHelper(driver);
            datePickerHelper = new DatePickerHelper(driver);

        }

        public override T LoadElements<T>(string page)
        {
            try
            {
                using (StreamReader r = new StreamReader("Resources\\PopupPages\\" + page.Replace("/", "") + ".json"))
                {
                    string jsonString = r.ReadToEnd();
                    var serializedJson = JsonConvert.DeserializeObject<List<PageObject>>(jsonString);
                    return (T)Convert.ChangeType(serializedJson.Distinct().ToDictionary(x => x.ID, x => x), typeof(T));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LoggingHelper.LogException(ex);
                return default(T);
            }
        }

        protected override By GetElement(string Name, string section = null)
        {
            if (section != null)
            {
                Name = section + "_" + Name;
            }

            PageObject value;
            PageElements.TryGetValue(Name, out value);
            return value.by;
        }

        /// <summary>
        /// searchValue will be intered in input of drop down and 1st row will be selected from resulted Multi-Column Table 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="searchValue"></param>
        internal void SelectValueMultipleColumns(string caption, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SelectValueMultipleColumns(GetElement(caption), searchValue, 1);
            }
        }

        internal void ClickTitle(string caption)
        {
            StaleElementClick(FindElement(GetElement(caption)));
        }

        internal void SearchAndSelectValueAfterOpen(string caption, string searchValue, string section = null)
        {
            tableDropDownHelper.SearchAndSelectValueAfterOpen(GetElement(caption, section), searchValue);
        }

        internal bool IsNextPageMultiSelectDropdown(string element)
        {
            return multiSelectHelper.IsNextPage(GetElement(element));
        }


        /// <summary>
        /// searchValue will be intered in input of drop down and provided index row will be selected from resulted Multi-Column Table 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="searchValue"></param>
        internal void SelectValueMultipleColumns(string caption, string searchValue, int index)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SelectValueMultipleColumns(GetElement(caption), searchValue, index);
            }
        }

        internal void ClickAnchorButton(string table, string header, string headerName, string buttonCaption, bool doWaitForInvisibility = false)
        {
            gridHelper.ClickAnchorButton(GetElement(table), GetElement(header), headerName, buttonCaption, doWaitForInvisibility);
        }

        internal bool IsAnchorButtonsNotVisible(string table, string header, string headerName)
        {
            return gridHelper.IsAnchorButtonsNotVisible(GetElement(table), GetElement(header), headerName);
        }

        public bool VerifyValueScrollable(string caption, params string[] values)
        {
            return tableDropDownHelper.VerifyValueScrollable(GetElement(caption), values);
        }

        public bool VerifyDataMultiSelectDropDown(string caption, params string[] values)
        {
            return multiSelectHelper.VerifyData(GetElement(caption), values);
        }

        public List<string> GetElementsList(string table, string header, string headerName)
        {
            return gridHelper.GetElementsList(GetElement(table), GetElement(header), headerName);
        }

        internal void ClickButtonOnGrid(string table, string header, string headerName, string buttonCaption)
        {
            gridHelper.ClickButtonOnGrid(GetElement(table), GetElement(header), headerName, buttonCaption);
        }

        internal bool IsRelationExist(string table, string header, string headerName, string buttonCaption)
        {
            return gridHelper.IsRelationExist(GetElement(table), GetElement(header), headerName, buttonCaption);
        }

        public string GetFirstValueFromGrid(string table, string header, string headerName)
        {
            return gridHelper.GetElementByIndex(GetElement(table), GetElement(header), headerName);
        }

        internal void InsertEditGrid(bool waitForMsg = true)
        {
            gridHelper.InsertEditGrid(waitForMsg);
        }

        internal void UpdateEditGrid(bool waitForMsg = true)
        {
            gridHelper.UpdateEditGrid(waitForMsg);
        }

        internal void CloseEditGrid()
        {
            gridHelper.CloseEditGrid();
        }

        internal string GetEditMsg()
        {
            return gridHelper.GetEditMsg();
        }

        internal string WaitForEditMsgChangeText()
        {
            return gridHelper.WaitForEditMsgChangeText();
        }

        internal void ClickHyperLinkOnGrid(string table, string header, string headerName)
        {
            gridHelper.ClickHyperLinkOnGrid(GetElement(table), GetElement(header), headerName);
        }

        internal bool CheckHyperlinkIsDisabledOnGrid(string table, string header, string headerName)
        {
            return gridHelper.CheckHyperlinkIsDisabledOnGrid(GetElement(table), GetElement(header), headerName);
        }

        internal string GetValueOfDropDown(string element, string section = null)
        {
            return tableDropDownHelper.GetValue(GetElement(element, section));
        }

        internal void OpenDropDown(string caption)
        {
            tableDropDownHelper.OpenDropDown(GetElement(caption));
        }

        /// <summary>
        /// Open MultiSelect DropDown
        /// </summary>
        internal void OpenMultiSelectDropDown(string caption)
        {
            multiSelectHelper.OpenDropDown(GetElement(caption));
        }

        internal bool IsDropDownClosed(string caption)
        {
            return tableDropDownHelper.IsDropDownClosed(GetElement(caption));
        }

        /// <summary>
        /// Check if MultiSelect DropDown is Closed
        /// </summary>
        /// <returns>returns true or false</returns>
        internal bool IsMultiSelectDropDownClosed(string caption)
        {
            return multiSelectHelper.IsDropDownClosed(GetElement(caption));
        }

        ///<summary>
        ///Verify Drop Down Is disabled, returns "true" if disabled attribute is present.
        ///</summary>
        internal string VerifyDropDownIsDisabled(string element, string section = null)
        {
            return tableDropDownHelper.DropDownDisabled(GetElement(element, section));
        }

        internal bool IsDropDownDisabled(string caption, string section = null)
        {
            return tableDropDownHelper.IsDropDownDisabled(GetElement(caption, section));
        }

        internal string GetFirstValueFromGrid(string headerName)
        {
            return gridHelper.GetElementByIndex(headerName);
        }

        internal string GetFirstRowDataPopUpPage(string headerName)
        {
            return gridHelper.GetElementByIndexPoPupPage(headerName);
        }

        internal string GetSecondValueFromGrid(string headerName)
        {
            return gridHelper.GetElementByIndex(headerName, 2);
        }
        internal string GetValueFromSubGridByIndex(string headerName, int index)
        {
            return gridHelper.GetElementByIndex(headerName, index);
        }

        internal void SelectValueTableDropDown(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SelectValue(GetElement(caption, section), searchValue);
            }
        }

        internal void SearchAndSelectValue(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SearchAndSelectValue(GetElement(caption, section), searchValue);
            }
        }
        public void SearchAndSelectValueMultipleColumnAfterOpen(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SearchAndSelectValueMultipleColumnAfterOpen(GetElement(caption, section), searchValue);
            }
        }

        internal void EnterTextDropDown(string caption, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.EnterTextDropDown(GetElement(caption), searchValue);
            }
        }

        internal void SearchAndSelectValueWithoutLoadingMsg(string caption, string searchValue, string section = null)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SearchAndSelectValue(GetElement(caption, section), searchValue, false);
            }
        }

        internal void SelectFirstRowMultiSelectDropDown(string caption, bool clear = true)
        {
            multiSelectHelper.SelectFirstRow(GetElement(caption), clear);
        }

        internal void SelectFirstRowMultiSelectDropDownWithoutClear(string caption)
        {
            multiSelectHelper.SelectFirstRow(GetElement(caption), false);
        }

        internal void SelectFirstRowMultiSelectDropDown(string caption, string headerName, string value)
        {
            multiSelectHelper.SelectFirstRow(GetElement(caption), headerName, value);
        }

        internal void SelectAllRowsMultiSelectDropDown(string caption, bool alert = false)
        {
            multiSelectHelper.SelectAllRows(GetElement(caption), alert);
        }
        internal void SelectAllRowsMultiSelectDropDownWithoutOpen(string caption, bool alert = false)
        {
            multiSelectHelper.SelectAllRowsWithoutOpen(GetElement(caption), alert);
        }

        internal void ClearSelectionMultiSelectDropDown(string caption)
        {
            multiSelectHelper.ClearSelection(GetElement(caption));
        }

        internal void SelectValueMultiSelectDropDown(string caption, string headerName, string value)
        {
            multiSelectHelper.SelectValue(GetElement(caption), headerName, value);
        }

        public bool VerifyValue(string caption, params string[] values)
        {
            return tableDropDownHelper.VerifyValue(GetElement(caption), values);
        }

        public bool VerifyValueSimpleSelect(string caption, params string[] values)
        {
            return simpleSelectHelper.VerifyValue(GetElement(caption), values);
        }

        public bool VerifyValueByEdit(string caption, string section, params string[] values)
        {
            return tableDropDownHelper.VerifyValue(GetElement(caption, section), values);
        }

        public bool ValidateDropDown(string caption)
        {
            return tableDropDownHelper.VerifyDropDown(GetElement(caption));
        }

        public bool VerifyValueByScrollDown(string caption, params string[] values)
        {
            return tableDropDownHelper.VerifyValueByScrollDown(GetElement(caption), values);
        }

        public bool VerifyValueByScrollDown(string caption, string section = null, params string[] values)
        {
            return tableDropDownHelper.VerifyValueByScrollDown(GetElement(caption, section), values);
        }

        public string SelectValueTableDropDown(string caption)
        {
            return tableDropDownHelper.SelectValue(GetElement(caption));
        }

        internal void SelectValueByScroll(string caption, string value, string section = null)
        {
            tableDropDownHelper.SelectValueByScroll(GetElement(caption, section), value);
        }

        internal void SetDropdownTableSelectInputValue(string caption, string value, string section = null)
        {
            tableDropDownHelper.SetDropdownTableSelectInputValue(GetElement(caption, section), value);
        }

        internal List<string> ValidateTableDetails(bool validatePagingDetails, bool validateGroupBy)
        {
            return gridHelper.ValidateTableDetails(validatePagingDetails, validateGroupBy);
        }

        internal List<string> ValidateTableHeaders(params string[] headerNames)
        {
            //return gridHelper.ValidateHeaders(By.XPath("//table[contains(@id,'DXMainTable')]"), headerNames);
            return new List<string>();
        }

        internal List<string> ValidateTableHeaders(string caption, params string[] headerNames)
        {
            //return gridHelper.ValidateHeaders(GetElement(caption), headerNames);
            return new List<string>();
        }

        internal bool IsMultipleColumnDropDownEnabled(string caption)
        {
            return tableDropDownHelper.IsMultipleColumnDropDownEnabled(GetElement(caption));
        }

        /// <summary>
        /// 1st row will be selected from dropdown
        /// </summary>
        /// <param name="caption"></param>
        internal void SelectValueFirstRow(string caption, string section = null)
        {
            tableDropDownHelper.SelectValueFirstRow(GetElement(caption, section), true);
        }

        internal void SearchAndSelectValueAfterOpenWithoutClear(string caption, string searchValue, string section = null)
        {
            tableDropDownHelper.SearchAndSelectValueAfterOpen(GetElement(caption, section), searchValue, false);
        }

        internal void SelectValueRowByIndex(string caption, int index, string section = null, bool openByButton = false)
        {
            tableDropDownHelper.SelectValueRowByIndex(GetElement(caption, section), index, openByButton);
        }

        internal void ClickTableHeader(string headerName)
        {
            gridHelper.ClickTableHeader(headerName);
        }

        internal void ClickTableHeader(string table, string headerName)
        {
            gridHelper.ClickTableHeader(GetElement(table), headerName);
        }

        internal List<string> GetElementsList(string table, string headerName)
        {
            return gridHelper.GetElementsList(GetElement(table), headerName);
        }

        internal int GetRowCount(string table, bool isMainTable)
        {
            return gridHelper.GetRowCount(GetElement(table), isMainTable);
        }

        internal void ScrollToHeader(string table, string headerName)
        {
            gridHelper.ScrollToHeader(GetElement(table), headerName);
        }

        internal string GetElementByIndex(string table, string headerName, int rowNum = 1)
        {
            return gridHelper.GetElementByIndex(GetElement(table), headerName, rowNum);
        }

        internal string GetElementByIndex(By table, string headerName, int rowNum = 1)
        {
            return gridHelper.GetElementByIndex(table, headerName, rowNum);
        }

        internal void Filter(string mainTable, string headerName, string value)
        {
            gridHelper.Filter(GetElement(mainTable), headerName, value);
        }

        internal void Filter(string mainTable, string header, string headerName, string value)
        {
            gridHelper.Filter(GetElement(mainTable), GetElement(header), headerName, value);
        }

        internal void ClearFilter(string mainTable, string headerName)
        {
            gridHelper.Filter(GetElement(mainTable), headerName, string.Empty);
        }

        internal int GetRecordsCount(string mainTable, string footerName = "Count")
        {
            return gridHelper.GetRecordsCount(GetElement(mainTable), footerName);
        }

        internal void ClickTableRowByValue(string table, string headerName, string value)
        {
            gridHelper.ClickTableRowByValue(GetElement(table), headerName, value);
        }

        internal void ClickTableRowByIndex(string table, string headerName, int rowIndex = 1)
        {
            gridHelper.ClickTableRowByIndex(GetElement(table), headerName, rowIndex);
        }

        internal void CheckCheckBox(string checkBoxLabel)
        {
            gridHelper.CheckCheckBox(checkBoxLabel);
        }

        internal void UncheckCheckBox(string checkBoxLabel)
        {
            gridHelper.UncheckCheckBox(checkBoxLabel);
        }

        internal bool IsCheckBoxCheckedWithLabel(string checkBoxLabel)
        {
            return gridHelper.IsCheckBoxCheckedWithLabel(checkBoxLabel);
        }

        /// <summary>
        /// Check whether the check with given label exists on page.
        /// </summary>
        /// <param name="checkBoxLabel">Checkbox label</param>
        /// <returns>boolean</returns>
        internal bool IsCheckBoxExistsWithLabel(string checkBoxLabel)
        {
            return gridHelper.IsCheckBoxExistsWithLabel(checkBoxLabel);
        }

        internal void CheckTableRowCheckBoxByIndex(string mainTable, int rowNum)
        {
            gridHelper.CheckTableRowCheckBoxByIndex(GetElement(mainTable), rowNum);
        }

        internal bool IsButtonEnabled(string buttonCaption)
        {
            return gridHelper.IsButtonEnabled(buttonCaption);
        }

        internal bool IsTextBoxEnabled(string fieldCaption, string section = null)
        {
            return IsTextBoxEnabled(GetElement(fieldCaption, section));
        }

        internal void SetFilterCreiteria(string mainTable, string headerName, string filterCriteria)
        {
            gridHelper.SetFilterCreiteria(GetElement(mainTable), headerName, filterCriteria);
        }

        internal string GetSelectedValueDorpDown(string caption)
        {
            return tableDropDownHelper.GetSelectedValue(GetElement(caption));
        }

        internal void SelectValueListBoxByScroll(string caption, string value, string section = null)
        {
            listBoxItemHelper.SelectValueByScroll(GetElement(caption, section), value);
        }

        internal void SelectFirstValueListBox(string caption)
        {
            listBoxItemHelper.SelectFirstValue(GetElement(caption));
        }

        internal List<string> GetHeaderNamesMultiSelectDropDown(string element)
        {
            return multiSelectHelper.GetHeaderNames(GetElement(element));
        }

        internal void SimpleSelectOptionByText(string caption, string optionText)
        {
            simpleSelectHelper.SelectOptionByText(GetElement(caption), optionText);
        }

        internal void SimpleSelectOptionByValue(string caption, string optionValue)
        {
            simpleSelectHelper.SelectOptionByValue(GetElement(caption), optionValue);
        }

        internal void SimpleSelectOptionByIndex(string caption, int optionIndex)
        {
            simpleSelectHelper.SelectOptionByIndex(GetElement(caption), optionIndex);
        }

        internal string GetSelectedValueSimpleSelect(string caption)
        {
            return simpleSelectHelper.GetSelectedText(GetElement(caption));
        }

        internal List<string> GetHeaderNamesTableDropDown(string element)
        {
            return tableDropDownHelper.GetHeaderNames(GetElement(element));
        }
        public bool VerifyValueDropDown(string caption, params string[] values)
        {
            return tableDropDownHelper.VerifyValue(GetElement(caption), values);
        }

        internal void EnterFromDate(string Date)
        {
            if (String.IsNullOrEmpty(Date))
            {
                Date = CommonUtils.GetCurrentDate();
            }
            EnterTextAfterClear("From", Date);
        }

        internal void EnterToDate(string Date)
        {
            if (String.IsNullOrEmpty(Date))
            {
                Date = CommonUtils.GetCurrentDate();
            }

            EnterTextAfterClear("To", Date);
        }
        internal void ClickSearch()
        {
            ButtonClick(ButtonsAndMessages.Search);
        }
        internal void WaitForGridLoad()
        {
            gridHelper.WaitForPopupPageGrid();
        }
        internal bool IsAnyDataOnGrid()
        {
            return gridHelper.IsAnyData();
        }
        internal bool IsAnyDataOnPopupPageGrid()
        {
            return gridHelper.IsAnyDataOnPopupPage();
        }
        public List<string> ValidateGridControls()
        {
            return gridHelper.ValidateGridControlsPopUpPage();
        }
        public bool ValidateExportButton()
        {
            return gridHelper.ValidateExportButtonPopUpPage();
        }
        public string ValidateSearch(string pageTitle)
        {
            return gridHelper.ValidateSearchPopUpPage(pageTitle);
        }

        public bool ValidatePagination()
        {
            return gridHelper.ValidatePaginationPopUpPage();
        }

        internal bool IsDatePickerClosed(string caption)
        {
            return datePickerHelper.IsDatePickerClosed(GetElement(caption));
        }

        internal void SelectDate(string caption)
        {
            datePickerHelper.SelectDate(GetElement(caption));
        }

        internal void SelectDateToday(string caption)
        {
            datePickerHelper.SelectDateToday(GetElement(caption));
        }
        internal List<string> ValidateDisputeExportButtons(bool ClosePopUp = true)
        {
            return gridHelper.DisputeExportButtons(ClosePopUp);
        }

        internal bool VerifyDataInMultiSelectDropDown(string caption, string headerName, string value)
        {
            return multiSelectHelper.VerifyDataInDropDown(GetElement(caption), headerName, value);
        }

        internal void OpenMultipleColumnsDropDown(string caption, string searchValue, bool hasClearButton)
        {
            tableDropDownHelper.OpenMultipleColumnsDropDown(GetElement(caption), searchValue, hasClearButton);
        }

        internal string GetFirstValueOfHeaderMultipleColumnsDropdown(string caption, string headerName)
        {
            return tableDropDownHelper.GetFirstValueOfHeaderMultipleColumns(GetElement(caption), headerName);
        }

        internal void ClearInputMultipleColumnsDropDown(string caption)
        {
            tableDropDownHelper.ClearInputMultipleColumnsDropDown(GetElement(caption));
        }

        internal List<string> VerifyDataMultipleColumnsDropdown(string pageName, string fieldName, string headerName, List<string> values, bool clickCustomTitle = false, string fieldToClick = "", bool hasClearButton = false, string clearButtonField = "")
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
                OpenMultipleColumnsDropDown(fieldName, displayName, hasClearButton);
                if (GetFirstValueOfHeaderMultipleColumnsDropdown(fieldName, headerName) != displayName)
                {
                    errorMsgs.Add($"Page [{pageName}]: Value [{displayName}] not found in [{fieldName}] dropdown.");
                }
                if (hasClearButton)
                {
                    WaitForElementToBePresent(clearButtonField);
                    ClickElement(clearButtonField);
                    WaitForLoadingMessage();
                }
                else
                {
                    if (clickCustomTitle)
                    {
                        if (string.IsNullOrEmpty(fieldToClick))
                        {
                            ClickTitle(FieldNames.Title);
                        }
                        else
                        {
                            ClickFieldLabel(fieldToClick);
                        }
                    }
                    else
                    {
                        ClickPageTitle();
                    }
                    IsDropDownClosed(fieldName);
                }
            }
            return errorMsgs;
        }

        internal List<string> VerifyDataMultiSelectDropdown(string pageName, string fieldName, string headerName, List<string> values, bool clickCustomTitle = false)
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
                if (clickCustomTitle)
                {
                    ClickTitle(FieldNames.Title);
                }
                else
                {
                    ClickPageTitle();
                }
                IsMultiSelectDropDownClosed(fieldName);
            }
            return errorMsgs;
        }

        internal bool IsDataExistsInDropdown(string caption)
        {
            return tableDropDownHelper.IsDataExistsInDropdown(GetElement(caption));
        }
        internal bool IsAnyRowsInDropdown(string element)
        {
            return tableDropDownHelper.IsAnyRowsInDropdown(GetElement(element));
        }

        /// Click On Field Label
        /// </summary>
        internal void ClickFieldLabelWithText(string fieldCaption)
        {
            fieldCaption = RenameMenuField(fieldCaption);
            var container = driver.FindElement(By.XPath("//span[text()='" + fieldCaption + "']"));
            container.Click();
        }

        internal void AddLineItem(string Part)
        {
            if (GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            SearchAndSelectValue(FieldNames.Item, Part);
            Click(ButtonsAndMessages.SaveLineItem);
            WaitForLoadingIcon();

            Click(ButtonsAndMessages.AddLineItem);
            WaitForLoadingIcon();
            SearchAndSelectValue(FieldNames.Item, Part);
            Click(ButtonsAndMessages.SaveLineItem);
            WaitForLoadingIcon();
        }
        internal void AddTax()
        {
            Click(ButtonsAndMessages.AddTax);
            WaitForLoadingIcon();
            if (GetValueOfDropDown(FieldNames.TaxType) != "State")
            {
                SelectValueByScroll(FieldNames.TaxType, "State");
            }
            SetValue(FieldNames.Amount, "1.00");
            EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            Click(ButtonsAndMessages.SaveTax);
            WaitForLoadingIcon();
            Click(ButtonsAndMessages.NewTax);
            WaitForLoadingIcon();
            SelectValueByScroll(FieldNames.TaxType, "PST");
            EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            SetValue(FieldNames.Amount, "1.00");
            Click(ButtonsAndMessages.SaveTax);
            WaitForLoadingIcon();
            Assert.AreEqual(2, GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            UploadFile(0, "UploadFiles//SamplePDF.pdf");
            Click(ButtonsAndMessages.UploadAttachments);
            WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            UploadFile(0, "UploadFiles//SamplePDF.pdf");
            Click(ButtonsAndMessages.UploadAttachments);
            WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);
        }
        internal void SubmitInvoice(string dealerInvNum)
        {
            Click(ButtonsAndMessages.SubmitInvoice);
            WaitForLoadingIcon();
            Click(ButtonsAndMessages.Continue);
            AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            if (!WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Creating Auth Invoice invoice [{dealerInvNum}]");
            }
        }
        internal string GetValueDropDown(string caption)
        {
            return tableDropDownHelper.GetValue(GetElement(caption));
        }
        internal void OpenAndFilterMultiSelectDropdown(string caption, string headerName, string value, string section = null)
        {
            multiSelectHelper.OpenAndFilterDropdown(GetElement(caption, section), headerName, value);
        }
        internal int GetTotalCountMultiSelectDropDown(string element)
        {
            return multiSelectHelper.GetTotalCount(GetElement(element));
        }
        internal void SwitchToAdvanceSearch()
        {
            tableDropDownHelper.SelectValue(GetElement(FieldNames.SearchType), "Advanced Search");
            WaitForLoadingMessage();
            WaitForGridLoad();
        }

        internal List<string> VerifyLocationCountMultiSelectDropdown(string dropdownCaption, LocationType locationType, UserType userType, string userName)
        {
            List<string> errorMsgs = new List<string>();
            if (IsElementVisible(FieldNames.SearchType) && GetValueDropDown(FieldNames.SearchType) == "Quick Search")
            {
                SwitchToAdvanceSearch();
            }
            OpenAndFilterMultiSelectDropdown(dropdownCaption, FieldNames.LocationType, locationType.DisplayName);
            int locationCountUI = GetTotalCountMultiSelectDropDown(dropdownCaption);
            List<EntityDetails> entityDetails = string.IsNullOrEmpty(userName) ? CommonUtils.GetActiveLocations(locationType, userType) : CommonUtils.GetActiveLocations(locationType, userName);
            UserType currentUserType = appContext.ImpersonatedUserData == null ? appContext.UserData.Type : appContext.ImpersonatedUserData.Type;
            if (locationCountUI != entityDetails.Count)
            {
                errorMsgs.Add($"{currentUserType.Name} User: [{dropdownCaption}] dropdown location [{locationType.DisplayName}] count mismatch. UI Count = {locationCountUI}, DB Count = {entityDetails.Count}");
            }
            return errorMsgs;
        }

        internal List<string> VerifyLocationNotInMultiSelectDropdown(string dropdownCaption, LocationType locationType, UserType userType, string userName)
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

        internal bool VerifyDataInMultipleColumnDropdown(string caption, string headerName, string value)
        {
            return tableDropDownHelper.VerifyDataInMultipleColumnDropdown(GetElement(caption), headerName, value);
        }

        internal List<string> VerifyLocationMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName)
        {
            return VerifyLocationMultipleColumnsDropdown(dropdownCaption, locationType, dropdownUserType, userName, canLocationExist: true);
        }

        internal List<string> VerifyLocationMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName, string headerName)
        {
            return VerifyLocationMultipleColumnsDropdown(dropdownCaption, locationType, dropdownUserType, userName, headerName: headerName, canLocationExist: true);
        }

        internal List<string> VerifyLocationNotInMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName)
        {
            return VerifyLocationMultipleColumnsDropdown(dropdownCaption, locationType, dropdownUserType, userName, canLocationExist: false);
        }

        protected List<string> VerifyLocationMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName, string headerName = null, int verifyNoOfRows = 1, bool canLocationExist = true, bool hasClearButton = false)
        {
            List<string> errorMsgs = new List<string>();
            headerName = string.IsNullOrEmpty(headerName) ? TableHeaders.DisplayName : headerName;
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
                    OpenMultipleColumnsDropDown(dropdownCaption, displayName, hasClearButton);
                    if (canLocationExist)
                    {
                        if (!VerifyDataInMultipleColumnDropdown(dropdownCaption, headerName, displayName))
                        {
                            errorMsgs.Add($"{currentUserType.Name} User: [{locationType.DisplayName}] location [{displayName}] not found in [{dropdownCaption}] dropdown");
                        }
                    }
                    else
                    {
                        if (VerifyDataInMultipleColumnDropdown(dropdownCaption, headerName, displayName))
                        {
                            errorMsgs.Add($"{currentUserType.Name} User: [{locationType.DisplayName}] location [{displayName}] found in [{dropdownCaption}] dropdown");
                        }
                    }
                    try
                    {
                        ClickPageTitle();
                        IsDropDownClosed(dropdownCaption);
                    }
                    catch (NoSuchElementException)
                    {
                        CloseDropdown(dropdownCaption);
                    }
                }
            }
            return errorMsgs;
        }

        internal void CloseDropdown(string caption)
        {
            if (IsDropDownClosed(caption))
            {
                tableDropDownHelper.IsDropDownClosed(GetElement(caption));
            }
        }

        internal bool IsClearSelectionButtonVisible(string caption)
        {
            return multiSelectHelper.IsClearSelectionButtonVisible(GetElement(caption));
        }

        internal string GetRecordsSelectedText(string dropDownName)
        {
            return multiSelectHelper.GetRecordsSelectedText(GetElement(dropDownName));
        }

        internal IReadOnlyCollection<IWebElement> GetFirstPageCheckBoxesMultiSelectDropDown(string dropDownName)
        {
            return multiSelectHelper.GetFirstPageCheckBoxesMultiSelectDropDown(GetElement(dropDownName));
        }

        internal bool AreFiltersAvailableForMultiSelectDropDown(string dropDownCaption)
        {
            return multiSelectHelper.AreFiltersAvailableForMultiSelectDropDown(GetElement(dropDownCaption));
        }

        internal string GetTotalCountTextMultiSelectDropDown(string dropDownCaption)
        {
            return multiSelectHelper.GetTotalCountTextMultiSelectDropDown(GetElement(dropDownCaption));
        }

        internal bool IsPaginationAvailableMultiSelectDropDown(string dropDownCaption)
        {
            return multiSelectHelper.IsPaginationAvailableMultiSelectDropDown(GetElement(dropDownCaption));
        }

        internal bool GoToNextPageMultiSelectDropDown(string dropDownCaption)
        {
            return multiSelectHelper.GoToNextPageMultiSelectDropDown(GetElement(dropDownCaption));
        }

        internal void CloseMultiselectDropDown(string dropDownCaption)
        {
            multiSelectHelper.CloseMultiselectDropDown(GetElement(dropDownCaption));
        }

        internal void FilterTable(string headerName, string value)
        {
            gridHelper.Filter(headerName, value);
        }

        internal void FilterRightGrid(string headerName, string value, bool pressEnterKey = true)
        {
            gridHelper.Filter(headerName, value, pressEnterKey);
        }

        internal void ClearFilter()
        {
            gridHelper.ClearFilter();
        }

        internal int GetRowCount()
        {
            return gridHelper.GetRowCount();
        }

        internal bool IsNextPage(bool isMainTable = true)
        {
            return gridHelper.IsNextPage(isMainTable);
        }

        public void GoToPage(int num, bool isMainTable = true)
        {
            gridHelper.GoToPage(isMainTable, num);
        }

        internal List<string> ValidateGridButtons(params string[] buttonCaptions)
        {
            return gridHelper.ValidateButtons(true, true, buttonCaptions);
        }
    }
}

