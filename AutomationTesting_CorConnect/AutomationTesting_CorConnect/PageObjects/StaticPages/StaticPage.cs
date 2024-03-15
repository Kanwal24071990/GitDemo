using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using Newtonsoft.Json;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test_CorConnect.src.main.com.corcentric.test.pageobjects.helpers;

namespace AutomationTesting_CorConnect.PageObjects.StaticPages
{
    internal class StaticPage : PageOperations<Dictionary<string, PageObject>>
    {
        private By exportPdfButton = By.XPath("//div[contains(@id,'btnPdfExport_CD')]");
        private By Grid = null;
        private string GridXpath = "(//span[text()='{0}']/ancestor::table)[1]//following-sibling::table[contains(@class,'MySplitter')]";
        public TableDropDownHelper tableDropDownHelper;
        public GridHelper gridHelper;
        protected DatePickerHelper datePickerHelper;
        public string PageName { get; private set; }

        public StaticPage(IWebDriver driver, string page) : base(driver)
        {
            PageName = page;
            LoggingHelper.InitializePage(page);
            PageElements = LoadElements<Dictionary<string, PageObject>>(page);
            tableDropDownHelper = new TableDropDownHelper(driver);
            gridHelper = new GridHelper(driver);
            datePickerHelper = new DatePickerHelper(driver);
            GetGridXpath(page);
        }

        public override T LoadElements<T>(string page)
        {
            using (StreamReader r = new StreamReader("Resources\\StaticPages\\" + page.Replace("/", "") + ".json"))
            {
                string jsonString = r.ReadToEnd();
                var serializedJson = JsonConvert.DeserializeObject<List<PageObject>>(jsonString);
                return (T)Convert.ChangeType(serializedJson.Distinct().ToDictionary(x => x.ID, x => x), typeof(T));
            }
        }

        protected override By GetElement(string Name, string section = null)
        {
            PageObject value;
            PageElements.TryGetValue(Name, out value);
            return value.by;
        }

        public void SelectValueMultipleColumns(string caption, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SelectValueMultipleColumns(GetElement(caption), searchValue, 1);
            }
        }

        public void SearchAndSelectValue(string caption, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.SearchAndSelectValue(GetElement(caption), searchValue);
            }
        }

        internal void EnterTextDropDown(string caption, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                tableDropDownHelper.EnterTextDropDown(GetElement(caption), searchValue);
            }
        }

        public void ClickSubmit()
        {
            ButtonClick(ButtonsAndMessages.Submit);
        }
        internal void SelectValueByScroll(string caption, string value, string section = null)
        {
            tableDropDownHelper.SelectValueByScroll(GetElement(caption, section), value);
        }

        internal void SearchAndSelectValueAfterOpenWithoutClear(string caption, string searchValue)
        {
            tableDropDownHelper.SearchAndSelectValueAfterOpen(GetElement(caption), searchValue, false);
        }

        public bool IsAnyDataOnGrid()
        {
            return gridHelper.IsAnyData();
        }

        internal List<string> ValidateGridButtons(params string[] buttonCaptions)
        {
            return gridHelper.ValidateButtons(true, false, buttonCaptions);
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

        internal void WaitForPDFButtonToLoad()
        {
            if (driver.FindElements(emptyGrid).Count == 0)
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(exportPdfButton));
            }
        }

        internal void SearchAndSelectValueAfterOpen(string caption, string searchValue)
        {
            tableDropDownHelper.SearchAndSelectValueAfterOpen(GetElement(caption), searchValue);
        }

        /// <summary>
        /// Check if DropDown is Closed
        /// </summary>
        /// <returns>returns true or false</returns>
        internal bool IsDropDownClosed(string caption)
        {
            return tableDropDownHelper.IsDropDownClosed(GetElement(caption));
        }

        /// <summary>
        /// Open DropDown
        /// </summary>
        internal void OpenDropDown(string caption)
        {
            tableDropDownHelper.OpenDropDown(GetElement(caption));
        }

        internal void OpenMultipleColumnsDropDown(string caption, string searchValue)
        {
            tableDropDownHelper.OpenMultipleColumnsDropDown(GetElement(caption), searchValue, false);
        }
        internal string GetValueDropDown(string caption)
        {
            return tableDropDownHelper.GetValue(GetElement(caption));
        }

        internal void ClearInputMultipleColumnsDropDown(string caption)
        {
            tableDropDownHelper.ClearInputMultipleColumnsDropDown(GetElement(caption));
        }

        /// <summary>
        /// Select First Row of DropDown
        /// </summary>
        internal void SelectValueFirstRow(string caption, string section = null)
        {
            tableDropDownHelper.SelectValueFirstRow(GetElement(caption, section), true);
        }

        internal void WaitForElementToBeClickable(string caption)
        {
            WaitForElementToBeClickable(GetElement(caption));
        }

        internal void ClickTitle(string caption)
        {
            StaleElementClick(FindElement(GetElement(caption)));
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

        internal void WaitForEditGrid()
        {
            gridHelper.WaitForEditGrid();
        }

        internal void WaitForEditGridClose()
        {
            gridHelper.WaitForEditGridClose();
        }

        internal string GetMessageOfPerformedOperation()
        {
            return gridHelper.GetMessageOfPerformedOperation();
        }

        internal string GetFirstValueOfHeaderMultipleColumnsDropdown(string caption, string headerName)
        {
            return tableDropDownHelper.GetFirstValueOfHeaderMultipleColumns(GetElement(caption), headerName);
        }

        internal bool VerifyDataInMultipleColumnDropdown(string caption, string headerName, string value)
        {
            return tableDropDownHelper.VerifyDataInMultipleColumnDropdown(GetElement(caption), headerName, value);
        }

        internal List<string> VerifyDataMultipleColumnsDropdown(string pageName, string fieldName, string headerName, List<string> values, bool clickTitle = false)
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
                if (!clickTitle)
                {
                    ClickPageTitle();
                    IsDropDownClosed(fieldName);
                }
                else
                {
                    ClickTitle(FieldNames.Title);
                    IsDropDownClosed(fieldName);
                    ClearInputMultipleColumnsDropDown(fieldName);
                    ClickTitle(FieldNames.Title);
                    WaitForLoadingIcon();
                }
            }
            return errorMsgs;
        }

        internal List<string> VerifyDataNotInMultipleColumnsDropdown(string pageName, string fieldName, string headerName, List<string> values, bool clickTitle = false)
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
                if (!clickTitle)
                {
                    ClickPageTitle();
                    IsDropDownClosed(fieldName);
                }
                else
                {
                    ClickTitle(FieldNames.Title);
                    IsDropDownClosed(fieldName);
                    ClearInputMultipleColumnsDropDown(fieldName);
                    ClickTitle(FieldNames.Title);
                    WaitForLoadingIcon();
                }
            }
            return errorMsgs;
        }
        internal void SetDropdownTableSelectInputValue(string caption, string value, string section = null)
        {
            tableDropDownHelper.SetDropdownTableSelectInputValue(GetElement(caption, section), value);
        }

        protected void GetGridXpath(string PageName)
        {
            Grid = By.XPath(string.Format(GridXpath, RenameMenuField(PageName)));
        }

        internal void WaitForGridLoad()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(Grid));
        }

        internal void SwitchToAdvanceSearch()
        {
            tableDropDownHelper.SelectValue(GetElement(FieldNames.SearchType), "Advanced Search");
            WaitForLoadingMessage();
            WaitForGridLoad();
        }

        internal List<string> VerifyLocationMultipleColumnsDropdown(string dropdownCaption, LocationType locationType, UserType dropdownUserType, string userName, int verifyNoOfRows = 1, bool canLocationExist = true)
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
                        if (!VerifyDataInMultipleColumnDropdown(dropdownCaption, FieldNames.Name, displayName))
                        {
                            errorMsgs.Add($"{currentUserType.Name} User: [{locationType.DisplayName}] location [{displayName}] not found in [{dropdownCaption}] dropdown");
                        }
                    }
                    else
                    {
                        if (VerifyDataInMultipleColumnDropdown(dropdownCaption, FieldNames.Name, displayName))
                        {
                            errorMsgs.Add($"{currentUserType.Name} User: [{locationType.DisplayName}] location [{displayName}] found in [{dropdownCaption}] dropdown");
                        }
                    }
                    ClickFieldLabel(dropdownCaption);
                    IsDropDownClosed(dropdownCaption);
                }
            }
            return errorMsgs;
        }
    }
}
