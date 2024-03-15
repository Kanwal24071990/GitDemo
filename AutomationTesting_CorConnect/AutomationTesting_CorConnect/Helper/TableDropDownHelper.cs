using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Test_CorConnect.src.main.com.corcentric.test.pageobjects.helpers
{
    internal class TableDropDownHelper : PageOperations<Dictionary<string, PageObject>>
    {
        private By DataRow = By.XPath(".//tr[contains(@Class,'ListBoxItemRow')]");
        private By DropDownButton = By.XPath(".//td[contains(@class,'dxeButtonEditButton')][not(contains(@class, 'EditClearButton'))][not(contains(@style,'display:none'))][not(contains(@style,'display: none'))]");
        private By DataRowColumns = By.XPath(".//tr[contains(@class, 'dxeListBoxItemRow')]//td");
        private By Header = By.XPath(".//td[contains(@class,'dxeListBoxItem') and contains(@class,'dxeH')]");
        internal TableDropDownHelper(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Open DropDown
        /// </summary>
        internal ReadOnlyCollection<IWebElement> OpenDropDown(By element)
        {
            WaitforInputField(element);
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElement(DropDownButton);
            inputField.Click();
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var ParentDiv = dataTable.FindElements(By.XPath("./ancestor::div[contains(@class,'dxpcDropDown')]"));
            if (WaitForVisibilityOfAllElementsLocatedBy(ParentDiv) == false)
            {
                WaitforInputField(element);
                tables = driver.FindElements(element);
                inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElement(DropDownButton);
                inputField.Click();
                dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
                ParentDiv = dataTable.FindElements(By.XPath("./ancestor::div[contains(@class,'dxpcDropDown')]"));
                WaitForVisibilityOfAllElementsLocatedBy(ParentDiv);
            }

            return tables;
        }


        /// <summary>
        /// Open Dropwn By Clicking on Input Field
        /// </summary>
        internal ReadOnlyCollection<IWebElement> OpenDropDownByInputField(By element)
        {
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            Scroll(inputField);
            inputField.Click();
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var ParentDiv = dataTable.FindElements(By.XPath("./ancestor::div[contains(@class,'dxpcDropDown')]"));
            WaitForVisibilityOfAllElementsLocatedBy(ParentDiv);
            return tables;
        }


        /// <summary>
        /// Search the value in Multiple columns Dropdown but not selects any.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        internal IWebElement OpenMultipleColumnsDropDown(By element, string searchValue, bool hasClearButton)
        {
            wait.Until(AnyElementExists(new By[] { element }));
            var tables = driver.FindElements(element);
            if (hasClearButton)
            {
                ClickElement(driver.FindElement(By.XPath(element.Criteria + "//td[contains(@class, 'EditClearButton')]")));
                WaitForLoadingMessage();
            }
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            EnterTextAfterClear(inputField, searchValue);
            WaitForMsg(ButtonsAndMessages.Loading);
            return tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
        }

        /// <summary>
        /// Clear the input field of multiple columns DropDown
        /// </summary>
        /// <param name="element"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        internal void ClearInputMultipleColumnsDropDown(By element)
        {
            wait.Until(AnyElementExists(new By[] { element }));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Clear();
        }

        /// <summary>
        /// Check if DropDown is Closed
        /// </summary>
        /// <returns>returns true or false</returns>
        internal bool IsDropDownClosed(By element)
        {
            var tables = driver.FindElements(element);
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var ParentDiv = dataTable.FindElement(By.XPath("./ancestor::div[contains(@class,'dxpcDropDown')]"));
            return ParentDiv.GetAttribute("style").Contains("display: none");
        }

        internal void SelectValueMultipleColumns(By element, string searchValue, int index)
        {
            wait.Until(AnyElementExists(new By[] { element }));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            EnterTextAfterClear(inputField, searchValue);
            WaitForMsg(ButtonsAndMessages.Loading);
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var dataRows = dataTable.FindElements(DataRow);
            dataRows[index - 1].Click();
        }

        internal void SearchAndSelectValueMultipleColumnAfterOpen(By element, string searchValue)
        {
            var tables = OpenDropDown(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            EnterText(inputField, searchValue);
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            wait.Until(d => dataTable.FindElements(DataRowColumns).Any(x => x.Text.Trim().ToLower().Contains(searchValue.ToLower())));
            dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var tableItem = dataTable.FindElement(By.XPath(".//tr[contains(@class, 'dxeListBoxItemRow')]"));
            tableItem.Click();
        }

        internal bool IsMultipleColumnDropDownEnabled(By element)
        {
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys"))?.FirstOrDefault().FindElements(By.XPath(".//input"));
            var displayedInputFields = inputField?.Where(x => x.Displayed == true && x.Enabled)?.FirstOrDefault();
            return displayedInputFields != null;
        }

        internal void SelectValue(By element, string searchValue)
        {
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            Scroll(inputField);
            inputField.Click();
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            wait.Until(d => dataTable.Text.Contains(searchValue));
            StaleElementClick(dataTable.FindElements(By.XPath(".//tr")).Where(x => x.Text.Trim() == searchValue).First());
        }

        internal void SearchAndSelectValueAfterOpen(By element, string searchValue, bool textClear = true)
        {
            var tables = OpenDropDown(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            if (textClear)
            {
                EnterTextAfterClear(inputField, searchValue);
                var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
                wait.Until(d => dataTable.Text.Trim().ToLower().Contains(searchValue.ToLower()));
                StaleElementClick(dataTable.FindElements(By.XPath(".//tr")).First());
            }
            else
            {
                EnterText(inputField, searchValue);
                var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
                wait.Until(d => dataTable.Text.Trim().ToLower().Contains(searchValue.ToLower()));
                dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
                var tableItem = dataTable.FindElements(By.XPath(".//tr[contains(@class, 'dxeListBoxItemRow')]")).Where(x => x.Text == searchValue).First();
                tableItem.Click();
            }
        }

        internal void SearchAndSelectValue(By element, string searchValue, bool waitForLoadingMsg = true)
        {
            try
            {
                var tables = driver.FindElements(element);
                var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
                EnterTextAfterClear(inputField, searchValue);

                if (waitForLoadingMsg)
                {
                    WaitForLoadingMessage();
                }

                var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
                //wait.Until(d => dataTable.Text.Trim().ToLower().Contains(searchValue.ToLower()));
                wait.Until(d => dataTable.FindElements(DataRowColumns).Any(x => x.Text.Trim().ToLower().Contains(searchValue.ToLower())));
                StaleElementClick(dataTable.FindElements(DataRowColumns).First(x => x.Text.Trim().ToLower().Equals(searchValue.ToLower())));
            }
            catch (StaleElementReferenceException ex)
            {
                LoggingHelper.LogException(ex);
                SearchAndSelectValue(element, searchValue, waitForLoadingMsg);
            }
        }

        internal void EnterTextDropDown(By element, string Value)
        {
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            EnterTextAfterClear(inputField, Value);
            SendTab(inputField);
        }

        /// <summary>
        /// Select First Row of DropDown
        /// <para>To Open by button click pass set openByButton=true</para>
        /// </summary>
        internal void SelectValueFirstRow(By element, bool openByButton = false)
        {
            ReadOnlyCollection<IWebElement> tables;

            if (openByButton)
            {
                tables = OpenDropDown(element);
            }
            else
            {
                tables = driver.FindElements(element);
                var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
                Scroll(inputField);
                inputField.Click();
            }

            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            WaitForElementsCountToBe(dataTable, By.XPath(".//tr"));
            StaleElementClick(dataTable.FindElements(By.XPath(".//tr//td[1]")).First());
        }

        internal void SelectValueRowByIndex(By element, int index, bool openByButton = false)
        {
            ReadOnlyCollection<IWebElement> tables;
            if (openByButton)
            {
                tables = OpenDropDown(element);
            }
            else
            {
                tables = driver.FindElements(element);
                var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
                inputField.Click();
            }
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            WaitForElementsCountToBe(dataTable, By.XPath(".//tr"));
            StaleElementClick(dataTable.FindElements(By.XPath(".//tr"))[index]);
        }

        public string SelectValue(By element)
        {
            var tables = driver.FindElements(element);
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            inputField.SendKeys(Keys.Down);
            WaitForElementToBePresent(By.XPath("//td[contains(@class, 'ItemSelected')]"));
            var tableRow = dataTable.FindElement(By.XPath("//td[contains(@class, 'ItemSelected')]")).Text.Trim();
            inputField.SendKeys(Keys.Enter);
            return tableRow;
        }

        internal bool VerifyDropDown(By element)
        {
            try
            {
                var tables = driver.FindElements(element);

                if (tables.FirstOrDefault(x => x.GetAttribute("class").Contains("ButtonEditSys")).FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First() == null)
                {
                    return false;
                }

                if (!tables.Any(x => x.GetAttribute("id").Contains("DDD_L_LBT")))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal void SelectValueByScroll(By element, string value)
        {
            WaitforInputField(element);
            var tables = OpenDropDown(element);
            WaitForElementToBePresent(By.XPath("//td[contains(@class, 'ItemSelected')]"));

            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            string previousSelectedItem = null;

            while (true)
            {
                SendUpKeys();
                var selectedItem = dataTable.FindElement(By.XPath(".//tr//td[contains(@class, 'ItemSelected')]")).Text.Trim();
                if (selectedItem == previousSelectedItem)
                {
                    break;
                }
                previousSelectedItem = selectedItem;
            }

            previousSelectedItem = null;

            while (true)
            {
                var tableRow = dataTable.FindElement(By.XPath(".//td[contains(@class, 'ItemSelected')]"));

                if (tableRow.Text.Trim() == value)
                {
                    tableRow.Click();
                    break;
                }
                else
                {
                    if (tableRow.Text.Trim() == previousSelectedItem)
                        break;
                    SendDownKeys();
                }
                previousSelectedItem = tableRow.Text.Trim();
            }
        }

        internal void WaitforInputField(By element)
        {
            var id = element.Criteria.Substring(element.Criteria.IndexOf(',') + 3, element.Criteria.LastIndexOf("'") - (element.Criteria.IndexOf(',') + 3));
            WaitForElementToBeVisible(By.XPath("//input[contains(@id,'" + id + "') and contains(@id, '_I')]"));
        }

        internal bool VerifyValueByScrollDown(By element, params string[] values)
        {
            WaitForElementToBePresent(element);
            var tables = driver.FindElements(element);

            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            WaitForElementToBeClickable(inputField);
            inputField.Click();
            inputField.SendKeys(Keys.Down);
            var tableRows = new List<string>();
            var dropdown = tables.FirstOrDefault(x => x.GetAttribute("id").Contains("DDD_L_LBT"));
            var tableRow = dropdown.FindElement(By.XPath(".//td[contains(@class, 'ItemSelected')]")).Text.Trim();
            tableRows.Add(tableRow);

            while (true)
            {
                inputField.SendKeys(Keys.Down);
                tableRow = dropdown.FindElement(By.XPath(".//td[contains(@class, 'ItemSelected')]")).Text.Trim();

                if (tableRow == tableRows.Last())
                {
                    break;
                }

                tableRows.Add(tableRow);
            }

            foreach (var value in values)
            {
                if (tableRows.Any(x => x == value))
                {
                    continue;
                }
                else
                {
                    inputField.Click();
                    return false;
                }
            }

            inputField.Click();
            return true;
        }

        public bool VerifyValueScrollable(By element, params string[] values)
        {
            WaitForElementToBePresent(element);
            var tables = driver.FindElements(element);

            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            WaitForElementToBeClickable(inputField);
            inputField.Click();
            var tableRows = new List<string>();
            inputField.SendKeys(Keys.Down);
            inputField.SendKeys(Keys.Up);
            WaitForElementToBePresent(By.XPath("//td[contains(@class, 'ItemSelected')]"));
            var tableRow = driver.FindElement(By.XPath("//td[contains(@class, 'ItemSelected')]")).Text.Trim();
            tableRows.Add(tableRow);

            while (true)
            {
                inputField.SendKeys(Keys.Up);
                tableRow = driver.FindElement(By.XPath("//td[contains(@class, 'ItemSelected')]")).Text.Trim();

                if (tableRow == tableRows.Last())
                {
                    break;
                }

                tableRows.Add(tableRow);
            }

            while (true)
            {
                inputField.SendKeys(Keys.Down);
                tableRow = driver.FindElement(By.XPath("//td[contains(@class, 'ItemSelected')]")).Text.Trim();

                if (tableRow == tableRows.Last())
                {
                    break;
                }

                tableRows.Add(tableRow);
            }

            foreach (var value in values)
            {
                if (tableRows.Any(x => x == value))
                {
                    continue;
                }
                else
                {
                    inputField.Click();
                    return false;
                }
            }

            inputField.Click();
            return true;
        }

        public bool VerifyValue(By element, params string[] values)
        {
            WaitForElementToBePresent(element);
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            Scroll(inputField);
            inputField.Click();
            var dataTable = tables.First(x => x.GetAttribute("id").Contains("DDD_L_LBT"));
            var tableRows = dataTable.FindElements(By.XPath(".//tr"));

            if (tableRows.Any(x => string.IsNullOrEmpty(x.Text.Trim())))
            {
                WaitForTextInTableRow(dataTable, By.XPath(".//tr"));
                tableRows = dataTable.FindElements(By.XPath(".//tr"));
            }

            values = values.Select(x => RenameMenuField(x)).ToArray();

            foreach (var value in values)
            {
                if (tableRows.Any(x => x.Text.Trim() == value))
                {
                    continue;
                }
                else
                {
                    inputField.Click();
                    return false;
                }
            }

            inputField.Click();
            return true;
        }

        public bool VerifyValueMultiSelectDropDown(By element, params string[] values)
        {
            WaitForElementToBePresent(element);
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            Scroll(inputField);
            inputField.Click();
            var dataTable = tables.First(x => x.GetAttribute("id").Contains("DDD_Grid"));
            var tableRows = dataTable.FindElements(By.XPath(".//tr"));

            if (tableRows.Any(x => string.IsNullOrEmpty(x.Text.Trim())))
            {
                WaitForTextInTableRow(dataTable, By.XPath(".//tr"));
                tableRows = dataTable.FindElements(By.XPath(".//tr"));
            }

            values = values.Select(x => RenameMenuField(x)).ToArray();

            foreach (var value in values)
            {
                if (tableRows.Any(x => x.Text.Trim() == value))
                {
                    continue;
                }
                else
                {
                    inputField.Click();
                    return false;
                }
            }

            inputField.Click();
            return true;
        }

        internal override string GetValue(By element)
        {
            WaitforInputField(element);
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            return inputField.GetAttribute("value").Trim();
        }

        internal string GetSelectedValue(By element)
        {
            WaitForElementToBePresent(element);
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            WaitForElementToBeClickable(inputField);
            inputField.Click();
            inputField.SendKeys(Keys.Down);
            inputField.SendKeys(Keys.Up);
            WaitForElementToBePresent(By.XPath("//td[contains(@class, 'ItemSelected')]"));
            string selectedVal = driver.FindElement(By.XPath("//td[contains(@class, 'ItemSelected')]")).Text.Trim();
            inputField.Click();
            return selectedVal;
        }

        protected override By GetElement(string Name, string section = null)
        {
            throw new NotImplementedException();
        }

        public override T LoadElements<T>(string page)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Verify Drop Down is disabled by getting the disabled attribute from element.
        ///</summary>
        internal string DropDownDisabled(By element)
        {
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            return inputField.GetAttribute("disabled");
        }

        internal bool IsDropDownDisabled(By element)
        {
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            return inputField.GetAttribute("disabled") == "true";
        }

        internal void SetDropdownTableSelectInputValue(By element, string value)
        {
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElement(By.XPath(".//input[@type='hidden']"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].value = '" + value + "';", inputField);
        }

        internal List<string> GetHeaderNames(By element)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(element.Criteria + "[contains(@class, 'ButtonEditSys')]")));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElement(DropDownButton);
            inputField.Click();
            var headerTable = tables.Where(x => x.GetAttribute("class").Contains("dxeListBox")).FirstOrDefault();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(Header));
            var headers = headerTable.FindElements(Header);
            List<string> headerNames = new List<string>();
            foreach (var header in headers)
            {
                if (header.Text != String.Empty)
                {
                    headerNames.Add(header.Text.Trim());
                }
            }
            inputField.Click();
            return headerNames;
        }

        internal string GetFirstValueOfHeaderMultipleColumns(By element, string headerName)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(element.Criteria + "[contains(@class, 'ButtonEditSys')]")));
            var tables = driver.FindElements(element);
            var headerTable = tables.Where(x => x.GetAttribute("class").Contains("dxeListBox")).FirstOrDefault();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(Header));
            var headers = headerTable.FindElements(Header);
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var dataRows = dataTable.FindElements(DataRow);
            var columns = dataRows[0].FindElements(By.XPath(".//td"));
            int columnIndex = headers.IndexOf(headers.First(x => x.Text.Trim() == headerName));
            if (columnIndex == -1)
            {
                throw new NotFoundException($"Header [{headerName}] not found in table.");
            }
            return columns[columnIndex].Text;
        }


        internal bool VerifyDataInMultipleColumnDropdown(By element, string headerName, string value)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(element.Criteria + "[contains(@class, 'ButtonEditSys')]")));
            var tables = driver.FindElements(element);
            var headerTable = tables.Where(x => x.GetAttribute("class").Contains("dxeListBox")).FirstOrDefault();
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(Header));
            var headers = headerTable.FindElements(Header);
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var dataRows = dataTable.FindElements(DataRow);
            if (dataRows.Count == 0)
            {
                return false;
            }
            var columns = dataRows[0].FindElements(By.XPath(".//td"));
            int columnIndex = headers.IndexOf(headers.First(x => x.Text.Trim() == headerName));
            if (columnIndex == -1)
            {
                throw new NotFoundException($"Header [{headerName}] not found in table.");
            }
            return columns[columnIndex].Text.Trim() == value;
        }

        internal bool IsDataExistsInDropdown(By element)
        {
            WaitForElementToBePresent(element);
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input[contains(@class, 'EditArea')]")).Where(x => x.Displayed == true).First();
            return inputField.GetAttribute("value").Trim().ToLower() != ButtonsAndMessages.NoDataExists.ToLower();
        }

        internal bool IsAnyRowsInDropdown(By element)
        {
            ReadOnlyCollection<IWebElement> tables;
            tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            Scroll(inputField);
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_L_LBT")).FirstOrDefault();
            var dataRows = dataTable.FindElements(DataRow);
            return dataRows.Count > 0;
        }
    }
}
