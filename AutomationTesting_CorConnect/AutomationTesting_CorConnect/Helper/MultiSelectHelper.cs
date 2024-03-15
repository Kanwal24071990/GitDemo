using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.Resources;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutomationTesting_CorConnect.Helper
{
    internal class MultiSelectHelper : PageOperations<JObject>
    {
        private By Header = By.XPath(".//td[contains(@class,'dxgvHeader')]");
        private string SearchColumn = ".//tr[3]/td[{0}]//input";
        private By CheckBox = By.XPath(".//span[contains(@class,'dxICheckBox') ]");
        private By ClearSelectionBtnBy = By.XPath(".//input[@value='Clear Selection']");
        private By MultiSelectDDBtnBy = By.XPath(".//ancestor::div[contains(@id, 'DDD_Panel')]//div[contains(@id, 'DDD_Button')]");
        private By DropDownButton = By.XPath(".//td[contains(@class,'dxeButtonEditButton')][not(contains(@class, 'EditClearButton'))][not(contains(@style,'display:none'))][not(contains(@style,'display: none'))]");
        private By Datarow = By.XPath(".//tr[contains(@class,'DataRow')]");
        private string inputTextBox = ".//input[contains(@id,'DXFREditor{0}')]";

        internal MultiSelectHelper(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Open MultiSelect DropDown
        /// </summary>
        internal void OpenDropDown(By element)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            tables = driver.FindElements(element);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).ToList().AsReadOnly();
            WaitForVisibilityOfAllElementsLocatedBy(mainTable);
        }

        /// <summary>
        /// Check if MultiSelect DropDown is Closed
        /// </summary>
        /// <returns>returns true or false</returns>
        internal bool IsDropDownClosed(By element)
        {
            var tables = driver.FindElements(element);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).First();
            var ParentDiv = mainTable.FindElement(By.XPath("./ancestor::div[contains(@class,'dxpcDropDown')]"));
            return ParentDiv.GetAttribute("style").Contains("display: none");
        }

        internal void SelectFirstRow(By element, bool clear = true)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            tables = driver.FindElements(element);

            if (clear)
            {
                ClearSelection(tables);
            }
            tables = driver.FindElements(element);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            SelectRow(mainTable, 0);
            inputField.Click();
        }

        internal void SelectFirstRow(By element, string headerName, string value)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            var tables = driver.FindElements(element);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            if (IsDropDownClosed(element))
            {
                inputField.Click();
            }
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            headerTable.FindElement(By.XPath(string.Format(SearchColumn, index))).SendKeys(value);
            wait.Until(ExpectedConditions.StalenessOf(mainTable));
            tables = driver.FindElements(element);
            mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            SelectRow(mainTable, 0);
            headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            headers = headerTable.FindElements(Header);
            index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            headerTable.FindElement(By.XPath(string.Format(SearchColumn, index))).Clear();
            wait.Until(ExpectedConditions.StalenessOf(mainTable));
            inputField.Click();
        }

        internal bool VerifyDataInDropDown(By element, string headerName, string value)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            headerTable.FindElement(By.XPath(string.Format(SearchColumn, index))).SendKeys(value);
            wait.Until(ExpectedConditions.StalenessOf(mainTable));
            tables = driver.FindElements(element);
            mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var rows = mainTable.FindElements(Datarow);
            if ((rows.Count == 1 && rows[0].Text.Trim().ToLower() == ButtonsAndMessages.NoDataToDisplay.ToLower()))
            {
                headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
                headers = headerTable.FindElements(Header);
                index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
                headerTable.FindElement(By.XPath(string.Format(SearchColumn, index))).Clear();
                wait.Until(ExpectedConditions.StalenessOf(mainTable));
                inputField.Click();
                return false;
            }
            headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            headers = headerTable.FindElements(Header);
            index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            headerTable.FindElement(By.XPath(string.Format(SearchColumn, index))).Clear();
            try
            {
                wait.Until(ExpectedConditions.StalenessOf(mainTable));
            }
            catch (StaleElementReferenceException e)
            { }
            inputField.Click();
            return true;
        }

        internal void SelectAllRows(By element, bool alert = false)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var checkBox = headers.Where(h => h.FindElement(CheckBox) != null).FirstOrDefault();
            WaitForElementToBeClickable(checkBox);
            if (alert == true)
            {
                checkBox.Click();
                AcceptAlert();
                inputField.Click();
            }
            else
            {
                checkBox.Click();
                inputField.Click();
            }
        }

        internal void SelectAllRowsWithoutOpen(By element, bool alert = false)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            var tables = driver.FindElements(element);
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var checkBox = headers.Where(h => h.FindElement(CheckBox) != null).FirstOrDefault();
            WaitForElementToBeClickable(checkBox);
            if (alert == true)
            {
                checkBox.Click();
                AcceptAlert();
            }
            else
            {
                checkBox.Click();
            }
        }

        internal void CloseMultiselectDropDown(By element)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
        }

        internal void SelectValue(By element, string headerName, string value)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            Scroll(element);
            var tables = driver.FindElements(element);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            headerTable.FindElement(By.XPath(string.Format(SearchColumn, index))).SendKeys(value);
            try
            {
                wait.Until(ExpectedConditions.StalenessOf(mainTable));
            }
            catch (WebDriverException e)
            {
                LoggingHelper.LogException(e);  

            }
            tables = driver.FindElements(element);
            mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var rows = mainTable.FindElements(By.XPath(".//tr[contains(@class,'DataRow')]"));
            SelectRow(mainTable, rows.IndexOf(rows.FirstOrDefault(a => a.Text.Trim() == value)));
            ClearSearchValue(tables, headerName);
            inputField.Click();
        }

        private void ClearSearchValue(ReadOnlyCollection<IWebElement> tables, string headerName)
        {
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            headerTable.FindElement(By.XPath(string.Format(SearchColumn, index))).Clear();
            try {
                wait.Until(ExpectedConditions.StalenessOf(mainTable));
            }catch(WebDriverException e)
            {
                LoggingHelper.LogException(e);

            }
        }

        internal bool VerifyDropDown(By element)
        {
            try
            {
                var tables = driver.FindElements(element);
                var input = tables.FirstOrDefault(x => x.GetAttribute("class").Contains("ButtonEditSys")).FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
                if (input == null)
                {
                    return false;
                }
                input.Click();
                var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
                var headers = headerTable.FindElements(Header);
                if (headers.Count() == 0)
                {
                    return false;
                }
                var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
                var checkBox = mainTable.FindElements(CheckBox);
                if (checkBox.Count() == 0)
                {
                    var rows = mainTable.FindElements(Datarow);
                    if (!(rows.Count == 1 && rows[0].Text.Trim().ToLower() == "no data to display"))
                    {
                        return false;
                    }
                }
                input.Click();
                try
                {
                    WaitForDropDownClose(element);
                }
                catch (WebDriverTimeoutException)
                {
                    var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElement(DropDownButton);
                    inputField.Click();
                    WaitForDropDownClose(element);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void SelectRow(IWebElement element, int index)
        {
            var checkBox = element.FindElements(CheckBox);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", checkBox[index]);
        }

        internal bool VerifyData(By element, params string[] values)
        {
            wait.Until(ExpectedConditions.ElementExists(element));
            var tableElement = driver.FindElements(element);
            var inputField = tableElement.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            var mainTable = tableElement.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var tableRows = mainTable.FindElements(By.XPath(".//tr"));
            var tableRowsNonEmpty = tableRows.Where(x => x.Text != string.Empty);
            if (tableRowsNonEmpty.Count() == 0)
            {
                return false;
            }

            foreach (var value in values)
            {
                if (tableRowsNonEmpty.Any(x => x.Text.Trim() == value))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        internal void ClearSelection(ReadOnlyCollection<IWebElement> tables)
        {
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var checkbox = headers.FirstOrDefault().FindElement(CheckBox);
            if (checkbox.GetAttribute("class").Contains("CheckBoxChecked"))
            {
                var table = tables.Where(x => x.FindElements(MultiSelectDDBtnBy).Where(x => x.Text.Trim().Contains(ButtonsAndMessages.ClearSelection)).Count() > 0).FirstOrDefault();
                var btn = table.FindElements(MultiSelectDDBtnBy).Where(x => x.Text.Trim().Contains(ButtonsAndMessages.ClearSelection)).First();
                Click(btn);
                AcceptAlert();
                wait.Until(ExpectedConditions.StalenessOf(mainTable));
            }
        }

        internal void ClearSelection(By element, bool waitForDropDownClose = true)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            tables = driver.FindElements(element);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var table = tables.Where(x => x.FindElements(MultiSelectDDBtnBy).Where(x => x.Text.Trim().Contains(ButtonsAndMessages.ClearSelection)).Count() > 0).FirstOrDefault();
            var btn = table.FindElements(MultiSelectDDBtnBy).Where(x => x.Text.Trim().Contains(ButtonsAndMessages.ClearSelection)).First();
            Click(btn);
            AcceptAlert();
            try
            {
                try
                {
                    wait.Until(ExpectedConditions.StalenessOf(mainTable));
                }
                catch (WebDriverException ex) { }
            }
            catch (StaleElementReferenceException)
            { }
            inputField.Click();
            if (waitForDropDownClose)
            {
                WaitForDropDownClose(element);
            }
        }

        internal void WaitForDropDownClose(By element)
        {
            var tables = driver.FindElements(element);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).First();
            var ParentDiv = mainTable.FindElement(By.XPath("./ancestor::div[contains(@class,'dxpcDropDown')]"));
            CustomWait().Until(x => ParentDiv.GetAttribute("style").Contains("display: none"));
        }

        protected override By GetElement(string Name, string? Section = null)
        {
            throw new NotImplementedException();
        }

        public override T LoadElements<T>(string page)
        {
            throw new NotImplementedException();
        }

        internal List<string> GetHeaderNames(By element)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(element.Criteria + "[contains(@class, 'ButtonEditSys')]")));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var selectAllHeader = headers.Where(x => x.FindElements(CheckBox).Count() > 0);
            var headerNames = headers.Except(selectAllHeader).Select(x => x.Text.Trim()).ToList();
            inputField.Click();
            return headerNames;
        }

        /// <summary>
        /// Get Total Number of Entities in MultiSelect DropDown
        /// </summary>
        /// <returns>returns total items in string</returns>
        internal int GetTotalCount(By element)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(element.Criteria + "[contains(@class, 'ButtonEditSys')]")));
            var tables = driver.FindElements(element);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            var innerTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_Grid_ddeFilter")).FirstOrDefault();
            var bottomPanels = innerTable.FindElements(By.XPath(".//b[contains(@class, 'dxp-summary')]"));
            string itemCount = "0";
            if (bottomPanels.Count > 0)
            {
                var bottomPanel = bottomPanels.FirstOrDefault();
                itemCount = bottomPanel.Text.Trim().ToString();
                string expr = @"\(\d.*\)";
                string numOnly = @"\d+";
                Regex regex = new Regex(expr, RegexOptions.IgnoreCase);
                itemCount = Regex.Match(regex.Match(itemCount).Value, numOnly).Value;
            }
            else
            {
                var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
                var rows = mainTable.FindElements(Datarow);
                if (rows.Count == 1 && rows[0].Text.Trim().ToLower() == ButtonsAndMessages.NoDataToDisplay.ToLower())
                {
                    itemCount = "0";
                }
                else
                {
                    itemCount = mainTable.FindElements(Datarow).Count.ToString();
                }
            }
            inputField.Click();
            return int.Parse(itemCount);
        }

        internal string GetSelectedRowCount(By element)
        {
            var tables = driver.FindElements(element);
            var rowsSelected = driver.FindElement(By.XPath(element.Criteria + "[contains(@id, 'TextBox_ddeFilter')]//input"));
            var pageCounter = rowsSelected.GetAttribute("value").ToString();
            string expr = @"^[-+]?[0-9]*\.?[0-9]+";
            Regex regex = new Regex(expr, RegexOptions.IgnoreCase);
            pageCounter = Regex.Match(regex.Match(pageCounter).Value, expr).Value;
            return pageCounter;
        }

        internal bool IsNextPage(By mainTable)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tables = driver.FindElements(mainTable);
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
            var bottomPanel = tables.Where(x => x.GetAttribute("id").Contains("DDD_Grid")).FirstOrDefault();
            if (bottomPanel.FindElements(By.XPath("//a//img[contains(@class,'pNext')]")).Count > 0)
            {
                GoToPage(mainTable);
                inputField.Click();
                return true;
            }
            else
            {
                inputField.Click();
                return false;
            }
        }

        internal void GoToPage(By mainTable)
        {
            var tables = driver.FindElements(mainTable);
            var innerMainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var bottomPanel = driver.FindElement(By.XPath(mainTable.Criteria + "[contains(@id, 'DDD_Grid')]//a//img[contains(@class,'pNext')]"));
            bottomPanel.Click();
            wait.Until(ExpectedConditions.StalenessOf(innerMainTable));
        }

        internal void OpenAndFilterDropdown(By element, string headerName, string value)
        {
            OpenDropDown(element);
            var tables = driver.FindElements(element);
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            EnterTextAfterClear(headerTable.FindElement(By.XPath(string.Format(SearchColumn, index))), value);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            try
            {
                wait.Until(ExpectedConditions.StalenessOf(mainTable));
            }
            catch (WebDriverException ex) { }
            tables = driver.FindElements(element);
            mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var inputField = tables.Where(x => x.GetAttribute("class").Contains("ButtonEditSys")).FirstOrDefault().FindElements(By.XPath(".//input")).Where(x => x.Displayed == true).First();
            inputField.Click();
        }

        private bool IsAnyRowSelected(ReadOnlyCollection<IWebElement> tables)
        {
            var headerTable = tables.Where(x => x.GetAttribute("id").Contains("DXHeaderTable")).FirstOrDefault();
            var headers = headerTable.FindElements(Header);
            var checkBox = headers.Where(h => h.FindElement(CheckBox) != null).FirstOrDefault();
            WaitForElementToBeClickable(checkBox);
            return !checkBox.GetAttribute("class").Contains("CheckBoxUnChecked");
        }

        internal string GetRecordsSelectedText(By dropDown)
        {
            By SelectedRecordsTextBy = By.XPath(".//input[contains(@id, 'DDD_TextBox')]");
            var tables = driver.FindElements(dropDown);
            var table = tables.First(x => x.FindElements(SelectedRecordsTextBy).Count() > 0);
            return table.FindElement(SelectedRecordsTextBy).GetAttribute("value");
        }

        internal IReadOnlyCollection<IWebElement> GetFirstPageCheckBoxesMultiSelectDropDown(By dropDown)
        {
            var tables = driver.FindElements(dropDown);
            var innerTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_Grid_")).FirstOrDefault();
            return innerTable.FindElements(By.XPath(".//span[contains(@id, 'DXSelBtn')]"));
        }

        internal bool AreFiltersAvailableForMultiSelectDropDown(By dropDown)
        {
            var tables = driver.FindElements(dropDown);
            var innerTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_Grid_")).FirstOrDefault();
            return innerTable.FindElements(By.XPath(".//input[contains (@id, 'DXFREditorcol')]")).Count > 0;
        }

        internal bool IsClearSelectionButtonVisible(By element)
        {
            var tables = driver.FindElements(element);
            var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
            var table = tables.Where(x => x.FindElements(MultiSelectDDBtnBy).Where(x => x.Text.Trim().Contains(ButtonsAndMessages.ClearSelection)).Count() > 0).FirstOrDefault();
            return table.FindElements(MultiSelectDDBtnBy).Where(x => x.Text.Trim().Contains(ButtonsAndMessages.ClearSelection)).First() != null;

        }

        internal string GetTotalCountTextMultiSelectDropDown(By dropDown)
        {
            var tables = driver.FindElements(dropDown);
            var innerTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_Grid_")).FirstOrDefault();
            var bottomPanels = innerTable.FindElements(By.XPath(".//b[contains(@class, 'dxp-summary')]"));
            string countText;
            if (bottomPanels.Count > 0)
            {
                var bottomPanel = bottomPanels.FirstOrDefault();
                countText = bottomPanel.Text.Trim().ToString();

            }
            else
            {
                var mainTable = tables.Where(x => x.GetAttribute("id").Contains("MainTable")).FirstOrDefault();
                var rows = mainTable.FindElements(Datarow);
                if (rows.Count == 1 && rows[0].Text.Trim().ToLower() == ButtonsAndMessages.NoDataToDisplay.ToLower())
                {
                    countText = "";
                }
                else
                {
                    countText = mainTable.FindElements(Datarow).Count.ToString();
                }
            }
            return countText;
        }

        internal bool IsPaginationAvailableMultiSelectDropDown(By dropDown)
        {
            var tables = driver.FindElements(dropDown);
            var innerTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_Grid")).FirstOrDefault();
            return innerTable.FindElements(By.XPath(".//a[@class='dxp-num']")).Count > 0;
        }

        internal bool GoToNextPageMultiSelectDropDown(By dropDown)
        {
            var tables = driver.FindElements(dropDown);
            var innerTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_Grid")).FirstOrDefault();
            var bottomPanel = innerTable.FindElement(By.XPath(".//div[contains(@id, 'DXPagerBottom')]"));
            int currentPage;
            int.TryParse(bottomPanel.FindElement(By.XPath(".//b[contains(@class, 'dxp-current')]")).Text, out currentPage);
            if (bottomPanel.FindElements(By.LinkText((currentPage + 1).ToString())).Count > 0)
            {
                bottomPanel.FindElement(By.LinkText((currentPage + 1).ToString())).Click();
                WaitForMsg(ButtonsAndMessages.Loading);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
