using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace Test_CorConnect.src.main.com.corcentric.test.pageobjects.helpers
{
    internal enum fileTypes { pdf, xls, xlsx }

    internal class GridHelper : PageOperations<Dictionary<string, PageObject>>
    {


        private string tableRow = ".//td[{0}]";
        private string inputTextBox = ".//input[contains(@id,'DXFREditor{0}')]";
        private string hyperLink = ".//td[{0}]/a";
        private string groupByDiv = "(//div[contains(@class,'dxgvGroupPanel')])[{0}]";

        private string searchColumn = ".//input[contains(@id,'DXFREditorcol{0}_')]";
        private string searchInputByColumn = ".//input[contains(@id,'DXFREditorcol')]";
        private string pagination = "(//div[contains(@id,'PagerTop')])[{0}]//a[text()='{1}']";
        private string PageNext = "(//div[contains(@id,'PagerTop')])[{0}]//a//img[contains(@class,'pNext')]";
        private string PagePrev = "(//div[contains(@id,'PagerTop')])[{0}]//a//img[contains(@class,'pPrev')]";
        private string headerColumn = ".//td[contains(@class,'columnHeader')]//td[text()='{0}']";
        private string selectAllXpath = "//*[@id=\"ASPxSplitter1_RightPaneBodyPanel_CallbackPanel_MainPgCntrl_uc_gridview_vertical2_ASPxSplitter1_ASPxRoundPanel2_callbkPnl_GridView_UC_GridView_Data1_grid_dxdt0_ctl00_0_Details_uc_gridview2_78_0_callbkPnl_GridView_0_UC_GridView_Data1_0_grid_0_dxdt0_0_ctl00_0_Details_uc_gridview0_106_0_callbkPnl_GridView_0_UC_GridView_Data1_0_grid_0_col0\"]";

        private By mainTable = By.XPath("//table[contains(@id,'grid_DXMainTable') or (contains(@id,'DXMainTable') and not(ancestor::div[contains(@style,'display:none')]) and not(ancestor::div[contains(@style,'display: none')]))]");
        private By mainTableBeforeSearch = By.XPath("//table[contains(@id,'DXMainTable') and not(ancestor::div[contains(@style,'display:none')]) and not(ancestor::div[contains(@style,'display: none')])]");
        private By header = By.XPath(".//td[contains(@class,'dxgvHeader') or contains(@class,'columnHeader')]");
        private By datarow = By.XPath(".//tr[contains(@class,'DataRow')]");
        private By expandButton = By.XPath("//td[contains(@class,'DetailButton')]//img");
        private By expandNestedGridButton = By.XPath("//img[contains(@class,'CollapsedButton')]/parent::td[contains(@class,'dxgv')]");
        private By nestedTable = By.XPath("//table[contains(@id,'DXMainTable') and contains(@id,'Details')]");
        private By nestedGrid = By.XPath("//tr[contains(@class,'DetailRow')]");
        private By pageCounter = By.XPath("//div[contains(@id,'DXPagerTop')]//b[contains(@class,'summary')]");
        private By pageCounterNestedPage = By.XPath("(//div[contains(@id,'DXPagerTop')]//b[contains(@class,'summary')])[last()]");
        private By selectedCounter = By.XPath("//span[contains(@ID,'lblSelectedCounter')]");
        private By groupPanel = By.XPath("//div[contains(@id,'grouppanel')]");
        protected By closeEdits = By.XPath("//*[text()='Close']/ancestor::a");
        protected By insertEdits = By.XPath("//*[text()='Insert']/ancestor::a");
        protected By updateEdits = By.XPath("//*[text()='Update']/ancestor::a");
        private By mainTablePopUpPage = By.XPath("//table[contains(@id,'tvAccounts')]");
        private By headerPopUpPage = By.XPath(".//th[contains(@class,'dxtlHeader')]");
        private By datarowPopUpPage = By.XPath(".//tr[contains(@id,'tvAccounts_R')][2]");
        private string tablerowPopUpPage = ".//td[{0}]";

        private By exportPdfButton = By.XPath("//div[contains(@id,'btnPdfExport_CD')]");
        private By exportXlsButton = By.XPath("//div[contains(@id,'btnXlsExport_CD')]");
        private By exportXlsxButton = By.XPath("//div[contains(@id,'btnXlsxExport_CD')]");
        private By ButtonHeader = By.XPath("//*[contains(@id, 'tblHeaderButtons')]");
        private By Buttonheader = By.XPath("//table[contains(@id, 'tblHeaderButtons')]//td");
        private By ButtonDisputeHeader = By.XPath("//*[contains(@id, 'tblGridExports')]");
        private By NestedbuttonHeader = By.XPath(" (//*[contains(@id, 'tblHeaderButtons')])[2]");
        private By EditGrid = By.XPath("//tr[contains(@class, 'EditForm_')]");
        private By EditMsg = By.XPath("//tr[contains(@class,'EditingErrorRow')]");
        private By PagingRow = By.XPath("//*[contains(@id,'GridView_0_UC_GridView_Data1_0_grid_0_dxdt0_0_ctl00_0_Details_uc_gridview0_106_0_callbkPnl_GridView_0_UC_GridView_Data1_0_grid_0_DXPagerTop')]/a");

        private By newButtonInGrid = By.XPath("//span[text()='New']//parent::a");
        private By nestedGridMessage = By.XPath("//tr[contains(@id, 'EditingErrorItem')]/td[@class='dxgv']");
        private By deleteAnchor = By.XPath("//*[text()='Delete']/ancestor::a");
        private By footerColumns = By.XPath(".//tr[contains(@id, '_DXFooterRow')]//td");

        private By mainCheckBoxList = By.XPath("//table[contains(@class, 'TAR')]");
        private string labelWithTextCheckBox = "//label[text()='{0}']//ancestor::tr[1]//span[contains(@class, 'CheckBox')]";
        private string checkBoxCheckedClass = "[contains(@class, 'CheckBoxChecked')]";
        private string checkBoxUncheckedClass = "[contains(@class, 'CheckBoxUnchecked')]";
        private string tableCheckBox = ".//span[contains(@class, 'CheckBox')]//ancestor::td[1]";
        private string checkBox = ".//span[contains(@class, 'CheckBox')]";
        private string toggleReadOnly = ".//span[contains(@id, 'ReadOnly') and contains(@id, 'S_D')]";
        private By filterCiteriaBtnBy = By.XPath(".//ancestor::table[2]//img[contains(@class, 'FilterRow')]");
        private By filterCiteriaMenuBy = By.XPath("//div[contains(@id, 'FilterRowMenu') and contains(@style, 'visibility: visible')]");
        private By filterCiteriaMenuItemsBy = By.XPath(".//ul//li[contains(@class, 'dxm-item') and not(contains(@style, 'display: none'))]");
        private By nestedGridTabsBy = By.XPath(".//ul[contains(@class, 'stripContainer')]//li[(contains(@class, 'tab') or contains(@class, 'activeTab')) and not(contains(@style, 'display:none'))]");
        private By radioButton = By.XPath("//td//span[contains(@class, 'RadioButton')]");

        private By EditDialog = By.XPath("//div[contains(@id, 'DXPEForm') and contains(@class, 'dxpclW') and not(contains(@style, 'none;'))]");

        protected By btnFirst = By.XPath("//div[contains(@id ,'ctl00_First')]");
        protected By btnPrevious = By.XPath("//div[contains(@id ,'ctl00_Previous')]");
        protected By btnNext = By.XPath("//div[contains(@id ,'ctl00_Next')]");
        protected By btnLast = By.XPath("//div[contains(@id ,'ctl00_Last')]");
        protected By searchField = By.XPath("//input[contains(@title ,'Find Text in Report')]");
        protected By btnFind = By.XPath("//a[@title = 'Find']");
        protected By btnFindNext = By.XPath("//a[@title = 'Find Next']");
        protected By btnExport = By.XPath("//a[contains(@id , 'ctl00_ButtonLink')]");
        protected By btnRefresh = By.XPath("//input[@title = 'Refresh']");
        protected By exportMenu = By.XPath("//div[contains(@id ,'ctl00_Menu')]");
        protected By totalPages = By.XPath("//span[contains(@id, 'ctl00_TotalPages')]");
        protected By currentPage = By.XPath("//input[contains(@id, 'ctl00_CurrentPage')]");
        protected By popupPageGrid = By.XPath("//div[contains(@id, 'ReportDiv')]");
        private By popupPageNoDataAvailable = By.XPath("//div[contains(text(), 'There is no data')]");

        DownloadHelper downloadHelper;


        internal GridHelper(IWebDriver driver) : base(driver)
        {
            downloadHelper = new DownloadHelper(driver);
        }

        internal void WaitForEditMsg()
        {
            WaitForElementToBeVisibleShortWait(EditMsg);
        }

        internal List<string> ValidateButtons(bool validateExport = true, bool ClosePopUp = true, params string[] buttonCaptions)
        {
            var buttonHeader = driver.FindElements(ButtonHeader)[0];
            var errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingGridButtons);

            foreach (var button in buttonCaptions)
            {
                if (buttonHeader.FindElements(By.XPath(string.Format(this.button, button))).Count != 1)
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, button));
                };
            }

            if (validateExport)
            {
                errorMsgs.AddRange(ValidateExport(appContext.downloadsDirectory, fileTypes.pdf, exportPdfButton, ClosePopUp));
                errorMsgs.AddRange(ValidateExport(appContext.downloadsDirectory, fileTypes.xls, exportXlsButton, ClosePopUp));
                errorMsgs.AddRange(ValidateExport(appContext.downloadsDirectory, fileTypes.xlsx, exportXlsxButton, ClosePopUp));
            }

            return errorMsgs;
        }

        internal List<string> DisputeExportButtons(bool ClosePopUp = true)
        {
            var errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingGridButtons);

            errorMsgs.AddRange(ValidateDisputeExport(appContext.downloadsDirectory, fileTypes.xls, exportXlsButton, ClosePopUp));
            errorMsgs.AddRange(ValidateDisputeExport(appContext.downloadsDirectory, fileTypes.xlsx, exportXlsxButton, ClosePopUp));

            return errorMsgs;
        }


        internal List<string> NestedValidateButtons(bool validateExport = true, bool ClosePopUp = true, params string[] buttonCaptions)
        {
            var buttonHeader = driver.FindElements(NestedbuttonHeader)[0];
            var errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingNestedGridButtons);

            foreach (var button in buttonCaptions)
            {
                if (buttonHeader.FindElements(By.XPath(string.Format(this.button, button))).Count != 1)
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, button));
                };
            }

            if (validateExport)
            {
                errorMsgs.AddRange(ValidateExport(appContext.downloadsDirectory, fileTypes.pdf, exportPdfButton, ClosePopUp));
                errorMsgs.AddRange(ValidateExport(appContext.downloadsDirectory, fileTypes.xls, exportXlsButton, ClosePopUp));
                errorMsgs.AddRange(ValidateExport(appContext.downloadsDirectory, fileTypes.xlsx, exportXlsxButton, ClosePopUp));
            }

            return errorMsgs;
        }
        internal void ClickNew(int recursionCount = 0)
        {
            if (recursionCount > 4)
            {
                return;
            }
            try
            {
                recursionCount += 1;
                ClickAnchorButton(TableHeaders.Commands, ButtonsAndMessages.New);
                WaitForEditGrid();
            }
            catch (WebDriverTimeoutException)
            {
                ClickNew(recursionCount);
            }
        }

        internal void ClickEdit(int recursionCount = 0)
        {
            if (recursionCount > 4)
            {
                return;
            }
            try
            {
                recursionCount += 1;
                ClickAnchorButton(TableHeaders.Commands, ButtonsAndMessages.Edit);
                WaitForEditGrid();
            }
            catch (WebDriverTimeoutException e)
            {
                ClickEdit(recursionCount);
            }
        }

        internal void ClickDelete(int recursionCount = 0)
        {
            if (recursionCount > 4)
            {
                return;
            }
            try
            {
                recursionCount += 1;
                ClickAnchorButton(TableHeaders.Commands, ButtonsAndMessages.Delete);
            }
            catch (WebDriverTimeoutException)
            {
                ClickDelete(recursionCount);
            }
        }

        internal string DeleteEditField()
        {
            ClickAnchorButton(TableHeaders.Commands, ButtonsAndMessages.Delete);
            AcceptAlert(out string msg);
            return msg;
        }

        internal void ClickAnchorButton(By table, By header, string headerName, string buttonCaption, bool doWaitForInvisibility)
        {
            headerName = RenameMenuField(headerName);
            WaitForElementToBeVisible(table);
            try
            {
                var buttons = GetAnchorButtons(table, header, headerName);
                var button = buttons.First(x => x.Text == buttonCaption);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                Click(button);
            }
            catch (WebDriverTimeoutException)
            {
                ClickAnchorButton(table, header, headerName, buttonCaption, doWaitForInvisibility);
            }
            catch (StaleElementReferenceException)
            {
                ClickAnchorButton(table, header, headerName, buttonCaption, doWaitForInvisibility);
            }
        }

        internal void ClickAnchorButton(string headerName, string Value, bool waitForDataRow = true)
        {
            try
            {
                headerName = RenameMenuField(headerName);
                var buttons = GetAnchorButtons(headerName, waitForDataRow);
                Click(buttons.First(x => x.Text == Value));
            }
            catch (StaleElementReferenceException e)
            {
                ClickAnchorButton(headerName, Value, waitForDataRow = true);
            }
        }

        internal bool IsAnchorButtonsNotVisible(By table, By header, string headerName)
        {
            WaitForElementToBeVisible(table);
            headerName = RenameMenuField(headerName);
            var buttons = GetAnchorButtons(table, header, headerName);

            return buttons.Count == 0;
        }

        internal void WaitForTable()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
        }
        internal void WaitForStalenessTable()
        {
            wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(mainTable)));
        }

        private ReadOnlyCollection<IWebElement> GetAnchorButtons(string headerName, bool waitForDataRow)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));

            if (waitForDataRow)
            {
                WaitForElementToBePresent(datarow);
            }

            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            return rows.First().FindElements(By.XPath(string.Format(hyperLink, index)));
        }

        private ReadOnlyCollection<IWebElement> GetAnchorButtons(By table, By header, string headerName)
        {
            var tableElement = driver.FindElement(table);
            headerName = RenameMenuField(headerName);
            var headers = tableElement.FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            return rows.First().FindElements(By.XPath(string.Format(hyperLink, index)));
        }

        private bool ValidateDownloadButton(By button)
        {
            var buttonHeader = driver.FindElements(ButtonHeader)[0];
            return buttonHeader.FindElements(button).Count == 1;
        }

        public void ClickGridButtons(string value)
        {
            var buttonHeader = driver.FindElements(Buttonheader);
            buttonHeader.First(x => x.Text == value.ToString()).Click();
        }
        private bool ValidateDownloadDisputeButton(By button)
        {
            var buttonHeader = driver.FindElement(ButtonDisputeHeader);
            return buttonHeader.FindElements(button).Count == 1;
        }

        private bool ValidatePageCounter()
        {
            Regex regex = new Regex(@"Page 1 of \d+ \(Total Count : \d+ items\)|^No data to paginate$");

            try
            {
                return regex.IsMatch(driver.FindElement(mainTable).FindElement(pageCounter).Text);
            }
            catch (NoSuchElementException ex)
            {
                return regex.IsMatch(driver.FindElement(By.XPath("//table[contains(@id,'DXMainTable')]")).FindElement(pageCounter).Text);

            }
        }
        internal string GetPageCounterTotal()
        {
            Regex regex = new Regex(@"(Total Count : \d+)");

            try
            {
                string pagingDetails = driver.FindElement(mainTable).FindElement(pageCounter).Text;

                if (regex.IsMatch(pagingDetails))
                {
                    return regex.Match(driver.FindElement(mainTable).FindElement(pageCounter).Text).Value.Split(":")[1].Trim();
                }
                else
                {
                    throw new NoSuchElementException();
                }

            }
            catch (NoSuchElementException ex)
            {
                try
                {
                    string pagingDetails = driver.FindElement(By.XPath("//table[contains(@id,'DXMainTable')]")).FindElement(pageCounter).Text;
                    return regex.Match(driver.FindElement(By.XPath("//table[contains(@id,'DXMainTable')]")).FindElement(pageCounter).Text).Value.Split(":")[1].Trim();
                }
                catch (NoSuchElementException)
                {
                    return "0";
                }
            }
        }

        private bool ValidateNestedPageCounter()
        {
            Regex regex = new Regex(@"Page 1 of \d+ \(Total Count : \d+ items\)");

            try
            {
                return regex.IsMatch(driver.FindElement(nestedTable).FindElement(pageCounter).Text);
            }
            catch (NoSuchElementException ex)
            {
                return regex.IsMatch(driver.FindElement(By.XPath("//table[contains(@id,'DXMainTable') and contains(@id,'Details')]")).FindElement(pageCounter).Text);
            }
        }

        private bool ValidateGroupPanel()
        {
            return driver.FindElement(groupPanel).Text.Trim() == "Drag a column header here to group by that column";
        }

        private bool ValidateNestedGroupPanel()
        {
            return driver.FindElement(nestedGrid).FindElement(groupPanel).Text.Trim() == "Drag a column header here to group by that column";
        }

        private bool ValidateSelectCounter()
        {
            return driver.FindElement(selectedCounter).Text.Trim() == "You have selected 0 out of max 200 selection limit.";
        }

        internal void ClickHyperLinkOnGrid(string headerName, bool openPopup = true)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var rowValue = rows.First().FindElements(By.XPath(string.Format(hyperLink, index)));
            Click(rowValue.First());

            if (openPopup)
            {
                SwitchToPopUp();
            }
        }

        internal void ClickHyperLinkOnGrid(By table, By header, string headerName)
        {
            headerName = RenameMenuField(headerName);
            var tableElement = driver.FindElement(table);
            var headers = tableElement.FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var rowValue = rows.First().FindElements(By.XPath(string.Format(hyperLink, index)));
            Click(rowValue.First());
        }

        ///<summary>
        ///Return true if provided relation exists on the table
        ///</summary>
        internal bool CheckHyperlinkIsDisabledOnGrid(By table, By header, string headerName)
        {
            try
            {
                headerName = RenameMenuField(headerName);
                var tableElement = driver.FindElement(table);
                var headers = tableElement.FindElements(header);
                var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
                var rows = tableElement.FindElements(datarow);
                var rowValue = rows.First().FindElements(By.XPath(string.Format(hyperLink, index)));
                var disabledClass = rowValue.First().GetAttribute("class");
                if (disabledClass.Contains("dxbDisabled"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (InvalidOperationException)
            {
                return false;
            }

        }

        internal void ClickButtonOnGrid(By table, By header, string headerName, string buttonCaption)
        {
            headerName = RenameMenuField(headerName);
            var tableElement = driver.FindElement(table);
            var headers = tableElement.FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var rowValue = rows.First().FindElements(By.XPath(string.Format(button, buttonCaption)));
            Click(rowValue.First());
        }

        ///<summary>
        ///Check if hyperlink is disabled by verifying class
        ///</summary>
        internal bool IsRelationExist(By table, By header, string headerName, string buttonCaption)
        {
            headerName = RenameMenuField(headerName);
            var tableElement = driver.FindElement(table);
            var headers = tableElement.FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var rowValue = rows.First().FindElements(By.XPath(string.Format(button, buttonCaption)));
            var Disabled = CheckHyperlinkIsDisabledOnGrid(table, header, "#");
            if (rowValue.Count != 0 && Disabled == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateHyperlink(string headerName)
        {
            headerName = RenameMenuField(headerName);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingHyperlink);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var rowValue = rows.First().FindElements(By.XPath(string.Format(hyperLink, index)));
            rowValue.First().Click();
            return driver.WindowHandles.Count == 2;
        }

        public bool ValidateHyperlinkNestedGrid(string headerName)
        {
            headerName = RenameMenuField(headerName);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingHyperlink);
            wait.Until(ExpectedConditions.ElementIsVisible(nestedTable));
            var tableElement = driver.FindElement(nestedTable);
            var headers = driver.FindElement(nestedTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var rowValue = rows.First().FindElements(By.XPath(string.Format(hyperLink, index)));
            rowValue.First().Click();
            return driver.WindowHandles.Count == 2;
        }

        ///<summary>
        ///Return true if provided button does not contain class disabled
        ///</summary>
        internal bool IsButtonEnabled(string buttonCaption)
        {
            try
            {
                var rowValue = driver.FindElement(By.XPath(string.Format(button, buttonCaption)));
                var disabledClass = rowValue.GetAttribute("class");
                if (!disabledClass.Contains("dxbDisabled"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                return false;
            }

        }


        ///<summary>
        ///Return first row data by header Name on provided rowNum
        ///</summary>
        internal string GetElementByIndex(string headerName, int rowNum = 1)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);

            var rowVal = rows[rowNum - 1].FindElements(By.XPath(string.Format(tableRow, index)));
            return rowVal.FirstOrDefault().Text;
        }

        internal string GetElementByIndexPoPupPage(string headerName, int rowNum = 1)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTablePopUpPage));
            var tableElement = driver.FindElement(mainTablePopUpPage);
            var headers = driver.FindElement(mainTablePopUpPage).FindElements(headerPopUpPage);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarowPopUpPage);

            var rowVal = rows[rowNum - 1].FindElements(By.XPath(string.Format(tablerowPopUpPage, index)));
            return rowVal.FirstOrDefault().Text;
        }

        public string GetElementByIndex(By table, By header, string headerName, int rowNum = 1)
        {
            headerName = RenameMenuField(headerName);
            List<string> elements = new List<string>();
            wait.Until(ExpectedConditions.ElementIsVisible(table));
            var tableElement = driver.FindElement(table);
            var headers = driver.FindElement(table).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var rowVal = rows[rowNum - 1].FindElements(By.XPath(string.Format(tableRow, index)));
            return rowVal.FirstOrDefault().Text;
        }


        public string GetElementByIndex(By table, string headerName, int rowNum = 1)
        {
            headerName = RenameMenuField(headerName);
            List<string> elements = new List<string>();
            wait.Until(ExpectedConditions.ElementIsVisible(table));
            var tableElement = driver.FindElement(table);
            var headers = driver.FindElement(table).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var rowVal = rows[rowNum - 1].FindElements(By.XPath(string.Format(tableRow, index)));
            return rowVal.FirstOrDefault().Text;
        }

        internal List<string> GetElementsList(string headerName)
        {
            headerName = RenameMenuField(headerName);
            List<string> elements = new List<string>();
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);

            foreach (var row in rows)
            {
                elements.Add(row.FindElement(By.XPath(string.Format(tableRow, index))).Text.Trim());
            }

            return elements;
        }

        internal List<string> GetElementsList(By table, By header, string headerName)
        {
            headerName = RenameMenuField(headerName);
            List<string> elements = new List<string>();
            wait.Until(ExpectedConditions.ElementIsVisible(table));
            var tableElement = driver.FindElement(table);
            var headers = driver.FindElement(table).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);

            foreach (var row in rows)
            {
                elements.Add(row.FindElement(By.XPath(string.Format(tableRow, index))).Text);
            }

            Console.WriteLine("check " + elements.Count);
            foreach (var element in elements)
            {
                Console.WriteLine("inside " + element);
            }

            return elements;
        }

        internal List<string> GetElementsList(By table, string headerName)
        {
            headerName = RenameMenuField(headerName);
            List<string> elements = new List<string>();
            wait.Until(ExpectedConditions.ElementIsVisible(table));
            var tableElement = driver.FindElement(table);
            var headers = tableElement.FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);

            foreach (var row in rows)
            {
                elements.Add(row.FindElement(By.XPath(string.Format(tableRow, index))).Text);
            }

            return elements;
        }

        ///<summary>
        ///Return number of Rows on Grid
        ///</summary>
        internal int GetRowCount()
        {
            int rowCount;

            if (!IsAnyData())
            {
                return 0;
            }

            else
            {

                wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
                var tableElement = driver.FindElement(mainTable);
                var rows = tableElement.FindElements(datarow);
                rowCount = rows.Count;

                if (rowCount == 1)
                {
                    string rowText = rows[0].Text.Trim();
                    if (rowText == "New\r\nNo data to display" || rowText == "No data to display")
                    {
                        return 0;
                    }
                }

                bool goToHomepage = false;

                while (driver.FindElements(By.XPath(string.Format(PageNext, 1))).Count > 0)
                {
                    NextPage(true);
                    tableElement = driver.FindElement(mainTable);
                    rowCount += tableElement.FindElements(datarow).Count();
                    goToHomepage = true;
                }

                if (goToHomepage)
                {
                    GoToPage(true, 1);
                }

                return rowCount;
            }

        }

        ///<summary>
        ///Return number of Rows on Current Page of Grid
        ///</summary>
        internal int GetRowCountCurrentPage()
        {
            int rowCount;
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var rows = tableElement.FindElements(datarow);
            rowCount = rows.Count;

            if (rowCount == 1)
            {
                string rowText = rows[0].Text.Trim();
                var pageCount = driver.FindElement(pageCounter).Text.Trim();
                if (rowText == "New\r\nNo data to display" || rowText == "No data to display" || pageCount == "No data to paginate")
                {
                    return 0;
                }
            }
            return rowCount;
        }

        internal int GetRowCountNestedPage(int tableNumber = 1)
        {
            int rowCount;
            wait.Until(ExpectedConditions.ElementIsVisible(nestedTable));
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }
            var rows = tableElement.FindElements(datarow);
            rowCount = rows.Count;

            if (rowCount == 1)
            {
                string rowText = rows[0].Text.Trim();
                var pageCount = driver.FindElement(pageCounterNestedPage).Text.Trim();
                if (rowText == "New\r\nNo data to display" || rowText == "No data to display" || pageCount == "No data to paginate")
                {
                    return 0;
                }
            }
            return rowCount;
        }

        internal List<string> ValidateExport(string folderPath, fileTypes fileType, By button, bool ClosePopUp)
        {
            LoggingHelper.LogMessage(string.Format(LoggerMesages.ValidatingFileDownload, fileType.ToString()));
            var errorMsgs = new List<string>();

            if (ValidateDownloadButton(button) == false)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, fileType.ToString()));
            }

            string fileName = downloadHelper.WaitForDownload(folderPath, fileType.ToString(), button, 60000, 80000);

            if (ClosePopUp)
            {
                ClosePopupWindow();
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FileNotDownloaded, fileType.ToString()));
            }

            return errorMsgs;
        }

        internal List<string> ValidateDisputeExport(string folderPath, fileTypes fileType, By button, bool ClosePopUp)
        {
            LoggingHelper.LogMessage(string.Format(LoggerMesages.ValidatingFileDownload, fileType.ToString()));
            var errorMsgs = new List<string>();

            if (ValidateDownloadDisputeButton(button) == false)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, fileType.ToString()));
            }

            string fileName = downloadHelper.WaitForDownload(folderPath, fileType.ToString(), button, 60000, 80000);

            if (ClosePopUp)
            {
                ClosePopupWindow();
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FileNotDownloaded, fileType.ToString()));
            }

            return errorMsgs;
        }

        public void ExpandByValue(IWebDriver driver, string column, string value)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(this.header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == column)) + 1;

            var rows = tableElement.FindElements(datarow);

            var row = rows.IndexOf(rows.Where(a => a.FindElement(By.XPath(string.Format(tableRow, index))).Text == value).FirstOrDefault());

            tableElement.FindElements(expandButton)[row].Click();
        }

        internal void Filter(string headerName, string value)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = tableElement.FindElement(mainTable).FindElements(header);
            Regex regex = new Regex(@"col\d+");
            var desiredHeader = headers.First(h => h.Text.Trim() == headerName);
            string idAttribute = desiredHeader.GetAttribute("id");
            string column = regex.Match(idAttribute).Value + "_";

            EnterTextAfterClear(tableElement.FindElement(By.XPath(string.Format(inputTextBox, column))), value);
            ButtonClick(ButtonsAndMessages.ApplyFilter);
            

            wait.Until(ExpectedConditions.StalenessOf(tableElement));
        }

        internal void Filter(string headerName, string value, bool pressEnterKey)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = tableElement.FindElement(mainTable).FindElements(header);
            Regex regex = new Regex(@"col\d+");
            var desiredHeader = headers.First(h => h.Text.Trim() == headerName);
            string idAttribute = desiredHeader.GetAttribute("id");
            string column = regex.Match(idAttribute).Value + "_";

            EnterTextAfterClear(tableElement.FindElement(By.XPath(string.Format(inputTextBox, column))), value);
            if (pressEnterKey == true)
            {
                PressEnterKey(tableElement.FindElement(By.XPath(string.Format(inputTextBox, column))));
            }
            else
            {
                ButtonClick(ButtonsAndMessages.ApplyFilter);
            }
            wait.Until(ExpectedConditions.StalenessOf(tableElement));
        }

        internal void FilterTableByColumnCount(string headerName, string value)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = tableElement.FindElement(mainTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            int searchInputCount = tableElement.FindElements(By.XPath(string.Format(searchInputByColumn, index))).Count;
            int searchInputIndex = Math.Abs(index - (headers.Count - searchInputCount));
            EnterTextAfterClear(tableElement.FindElement(By.XPath(string.Format(searchColumn, searchInputIndex))), value);
            ButtonClick(ButtonsAndMessages.ApplyFilter);
            WaitForLoadingMessage();
            tableElement = driver.FindElement(mainTable);
            WaitForElementToBeClickable(tableElement.FindElement(By.XPath(string.Format(searchColumn, searchInputIndex))));
        }

        public void NestedGrid(IWebDriver driver, string headerName)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(nestedTable));
            var tableElement = driver.FindElement(nestedTable);
            var check = tableElement.FindElements(header);
            var index = check.IndexOf(check.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);

            foreach (var row in rows)
            {
                var c = row.FindElements(By.XPath(string.Format(tableRow, index)));
            }
        }

        internal List<string> ValidateTableDetails(bool validatePagingDetails, bool validateGroupBy, bool ValidateSelectedCounter = false)
        {
            var errorMsg = new List<string>();

            if (validatePagingDetails)
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCounter);

                if (!ValidatePageCounter())
                {
                    errorMsg.Add(ErrorMessages.IncorrectPageCounter);
                }
            }

            if (validateGroupBy)
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingGroupPanel);

                if (!ValidateGroupPanel())
                {
                    errorMsg.Add(ErrorMessages.GroupPanelNotExist);
                }
            }

            if (ValidateSelectedCounter)
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingSelectedCounter);

                if (!ValidateSelectCounter())
                {
                    errorMsg.Add(ErrorMessages.IncorrectSelectedCounter);
                }
            }

            return errorMsg;
        }

        internal List<string> ValidateHeaders(params string[] headerNames)
        {
            for (int i = 0; i < headerNames.Length; i++)
            {
                if (headerNames[i].ToLower().Contains("dealer") || headerNames[i].ToLower().Contains("fleet"))
                {
                    headerNames[i] = RenameMenuField(headerNames[i]);
                }
            }

            LoggingHelper.LogMessage(LoggerMesages.ValidatingTableHeaders);
            var errorMsgs = new List<string>();

            if (headerNames != null)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
                var tableElement = driver.FindElement(mainTable);
                var headers = tableElement.FindElements(header).Where(s => !string.IsNullOrWhiteSpace(s.Text)).ToList();

                if (headerNames.Count() != headers.Count())
                {
                    errorMsgs.Add(ErrorMessages.HeaderCountMisMatch);
                }

                foreach (string header in headerNames)
                {
                    if (headers.Any(a => a.Text.TrimEnd() == header))
                    {
                        continue;
                    }
                    else
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.TableHeaderMissing, header));
                    }

                }

            }

            return errorMsgs;
        }

        internal List<string> ValidateHeadersFromFile(string pageName, bool checkClickSearchText)
        {
            pageName = RenameMenuField(pageName);
            List<string> headerNames = CommonUtils.GetPageHeaders(pageName).ToList();
            if (checkClickSearchText == true)
            {
                headerNames.Remove("Commands");
            }
            for (int i = 0; i < headerNames.Count; i++)
            {
                if (headerNames[i].ToLower().Contains("dealer") || headerNames[i].ToLower().Contains("fleet"))
                {
                    headerNames[i] = RenameMenuField(headerNames[i]);
                }
            }

            LoggingHelper.LogMessage("Validating headers from file");
            var errorMsgs = new List<string>();            
            if (headerNames != null)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(mainTableBeforeSearch));
                var tableElement = driver.FindElement(mainTableBeforeSearch);
                var uiHeadersElem = tableElement.FindElements(header);
                var uiHeaders = uiHeadersElem.Where(s => !string.IsNullOrWhiteSpace(s.Text)).Select(x => x.Text.Trim()).ToList();
                
                if (headerNames.Count() != uiHeaders.Count())
                {
                    errorMsgs.Add(ErrorMessages.HeaderCountMisMatch);
                }

                uiHeaders.ForEach(h =>
                {
                    if (!headerNames.Any(x => x.AreEqualCustom(h)))
                    {
                        errorMsgs.Add(pageName +": "+string.Format(ErrorMessages.TableHeaderNotInFile, h));
                    }
                });

                headerNames.ForEach(h =>
                {
                    if (!uiHeaders.Any(x => x.AreEqualCustom(h)))
                    {
                        errorMsgs.Add(pageName+": " +string.Format(ErrorMessages.TableHeaderNotOnUI, h));
                    }
                });
            }
            if (checkClickSearchText)
            {
                if (!CheckForText("Click on Search to view data."))
                {
                    errorMsgs.Add("Click on Search to view data is not visible.");
                }
            }
           
            return errorMsgs;
        }


        internal List<string> ValidateHeaders(By table, params string[] headerNames)
        {
            for (int i = 0; i < headerNames.Length; i++)
            {
                if (headerNames[i].ToLower().Contains("dealer") || headerNames[i].ToLower().Contains("fleet"))
                {
                    headerNames[i] = RenameMenuField(headerNames[i]);
                }
            }

            LoggingHelper.LogMessage(LoggerMesages.ValidatingTableHeaders);
            var errorMsgs = new List<string>();

            if (headerNames != null)
            {

                wait.Until(ExpectedConditions.ElementIsVisible(table));
                var tableElement = driver.FindElement(table);
                var headers = tableElement.FindElements(header).Where(s => !string.IsNullOrWhiteSpace(s.Text)).ToList();

                if (headerNames.Count() != headers.Count())
                {
                    errorMsgs.Add(ErrorMessages.HeaderCountMisMatch);
                }

                foreach (string header in headerNames)
                {
                    if (headers.Any(a => a.Text.TrimEnd() == header))
                    {
                        continue;
                    }
                    else
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.TableHeaderMissing, header));
                    }
                }
            }

            return errorMsgs;
        }

        internal List<string> ValidateHeaders(string pageCaption)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingTableHeaders);
            var errorMsgs = new List<string>();
            var headerNames = baseDataAccessLayer.GetHeaders(pageCaption);

            if (headerNames != null)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
                var tableElement = driver.FindElement(mainTable);
                var headers = tableElement.FindElements(header);
                var a = headers.Where(s => !string.IsNullOrWhiteSpace(s.Text));

                if (headerNames.Count != headers.Count)
                {
                    errorMsgs.Add(ErrorMessages.HeaderCountMisMatch);
                }

                foreach (string header in headerNames)
                {
                    if (headers.Any(a => a.Text.TrimEnd().ToUpper() == header.ToUpper()))
                    {
                        continue;
                    }
                    else
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.TableHeaderMissing, header));
                    }
                }
            }

            return errorMsgs;
        }

        internal bool IsNestedGridOpen(int index)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            tableElement.FindElements(expandButton)[index - 1].Click();
            wait.Until(ExpectedConditions.ElementIsVisible(nestedGrid));
            return driver.FindElements(nestedGrid).Count == 1;
        }

        internal bool IsNestedGridOpenByLevel(int index, int level)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            string criteria = mainTable.Criteria;
            while (level >= 2)
            {
                tableElement = tableElement.FindElement(nestedTable);
                criteria = criteria + nestedTable.Criteria;
                level--;
            }
            driver.FindElements(By.XPath(criteria + expandButton.Criteria))[index - 1].Click();
            criteria = criteria + nestedGrid.Criteria;
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(criteria)));
            return driver.FindElements(By.XPath(criteria)).Count == 1;
        }

        internal bool IsNestedGridClosed(int index)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            tableElement.FindElements(expandButton)[index - 1].Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(nestedGrid));
            return driver.FindElements(nestedGrid).Count == 0;
        }

        internal bool IsGroupGridOpen(int index)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var collapseIcons = tableElement.FindElements(expandNestedGridButton);
            if (collapseIcons != null && collapseIcons.Count > 0)
            {
                collapseIcons[index - 1].Click();
                wait.Until(ExpectedConditions.ElementIsVisible(expandButton));
                return driver.FindElements(expandButton).Count > 0;
            }
            return false;
        }

        public void GoToPage(bool isMainTable, int num)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            if (isMainTable)
            {
                driver.FindElement(By.XPath(string.Format(pagination, 1, num))).Click();
            }
            else
            {
                driver.FindElement(By.XPath(string.Format(pagination, 2, num))).Click();
            }
            WaitForMsg("Loading…");
        }

        public void GoToPageNestedGrid(int num, string tabName, int tableNumber = 1)
        {
           
            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }
            
            var pagerow = tableElement.FindElements(PagingRow);
            
            pagerow.First(x => x.Text == num.ToString()).Click();


            WaitForMsg("Loading…");
           

        }

        public void VerifyNextPage(bool isMainTable, int num)
        {
            var errorMsgs = new List<string>();
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            if (isMainTable)
            {
                var element = driver.FindElement(By.XPath(string.Format(pagination, 2, num))).Text;
                if (element == null || element == "" || !element.Equals(2))
                {
                    errorMsgs.Add("Failed to naviagte to next page");
                }

            }
            WaitForMsg("Loading…");
        }

        public void GoToPage(By mainTable, bool isMainTable, int num)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));

            if (isMainTable)
            {
                driver.FindElement(By.XPath(string.Format(pagination, 1, num))).Click();
            }
            else
            {
                driver.FindElement(By.XPath(string.Format(pagination, 2, num))).Click();
            }

            WaitForMsg("Please wait...");
        }

        public void NextPage(bool isMainTable)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));

            if (isMainTable)
            {
                driver.FindElement(By.XPath(string.Format(PageNext, 1))).Click();
            }
            else
            {
                driver.FindElement(By.XPath(string.Format(PageNext, 2))).Click();
            }

            WaitForMsg("Loading…");
        }

        public void NextPage(By mainTable, bool isMainTable)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            if (isMainTable)
            {
                driver.FindElement(By.XPath(string.Format(PageNext, 1))).Click();
            }
            else
            {
                driver.FindElement(By.XPath(string.Format(PageNext, 2))).Click();
            }
            WaitForMsg("Please wait...");
        }

        internal bool IsNextPage(By mainTable, bool isMainTable)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            if (isMainTable)
            {
                return driver.FindElements(By.XPath(string.Format(PageNext, 1))).Count > 0;
            }
            else
            {
                return driver.FindElements(By.XPath(string.Format(PageNext, 2))).Count > 0;
            }
        }
        internal bool IsNextPage(bool isMainTable)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            if (isMainTable)
            {
                return driver.FindElements(By.XPath(string.Format(PageNext, 1))).Count > 0;
            }
            else
            {
                return driver.FindElements(By.XPath(string.Format(PageNext, 2))).Count > 0;
            }
        }

        internal bool IsNextPageNestedGrid(bool isMainTable)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(nestedGrid));
            if (isMainTable)
            {
                return driver.FindElements(By.XPath(string.Format(PageNext, 1))).Count > 0;
            }
            else
            {
                return driver.FindElements(By.XPath(string.Format(PageNext, 2))).Count > 0;
            }
        }

        public void PrevPage(bool isMainTable)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));

            if (isMainTable)
            {
                driver.FindElement(By.XPath(string.Format(PagePrev, 1))).Click();
            }
            else
            {
                driver.FindElement(By.XPath(string.Format(PagePrev, 2))).Click();
            }

            WaitForMsg("Loading…");
        }

        public void GroupBy(bool isMainTable, string column)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));

            var tableElement = driver.FindElement(isMainTable ? mainTable : nestedTable);
            var check = tableElement.FindElements(header);
            var row = check.Where(a => a.Text.Trim() == column).FirstOrDefault();
            DragAndDrop(row, driver.FindElement(By.XPath(string.Format(groupByDiv, isMainTable ? 1 : 2))));
            WaitForMsg("Loading…");
        }

        internal void WaitForEditGrid()
        {
            WaitForElementToBeVisibleShortWait(EditGrid);
        }

        internal void WaitForEditGridClose()
        {
            WaitForElementToBeInvisible(EditGrid);
        }

        internal void CloseEditGrid()
        {
            try
            {
                Click(closeEdits);
                WaitForElementToBeInvisible(EditGrid);
            }
            catch (WebDriverTimeoutException)
            {
                CloseEditGrid();
            }

        }

        internal string GetEditMsg()
        {
            return driver.FindElement(EditMsg).Text.Trim();
        }

        internal string WaitForEditMsgChangeText()
        {
            string oldMsg = driver.FindElement(EditMsg).Text.Trim();
            wait.Until(ExpectedConditions.InvisibilityOfElementWithText(EditMsg, oldMsg));
            return driver.FindElement(EditMsg).Text.Trim();
        }

        internal void InsertEditGrid(bool waitForMsg = true)
        {
            try
            {
                Click(insertEdits);
                if (waitForMsg)
                {
                    WaitForElementToBeVisibleShortWait(EditMsg, 10);
                }
            }
            catch (WebDriverTimeoutException)
            {
                InsertEditGrid();
            }
        }

        internal void UpdateEditGrid(bool waitForMsg = true)
        {
            try
            {
                Click(updateEdits);

                if (waitForMsg)
                {
                    WaitForElementToBeVisibleShortWait(EditMsg);
                }
            }
            catch (WebDriverTimeoutException)
            {
                UpdateEditGrid();
            }
        }

        internal bool IsInsertDisplayed()
        {
            return driver.FindElements(insertEdits).Count() == 1;
        }

        internal bool IsCloseDisplayed()
        {
            return driver.FindElements(closeEdits).Count() == 1;
        }

        internal bool IsUpdateDisplayed()
        {
            return driver.FindElements(updateEdits).Count() == 1;
        }

        internal void ClearFilter()
        {
            ButtonClick(ButtonsAndMessages.ClearFilter);
            try
            {
                wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(mainTable)));
            }
            catch (WebDriverTimeoutException)
            {
                LoggingHelper.LogException($"Wait for loading message timed out. Loading message not appeared with in 60 seconds.");
            }
        }
        internal void ClearNestedFilter(int tableNumber = 1)
        {

            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }

            string idAttribute = tableElement.GetAttribute("id").ToString();
            Regex regex = new Regex(@"Details_uc_gridview\d_");
            Click(By.XPath("//input[contains(@id, '" + regex.Match(idAttribute).Value + "') and @value='Clear Filter']"));

            wait.Until(ExpectedConditions.StalenessOf(tableElement));
        }

        internal void ClearSelection()
        {
            ButtonClick(ButtonsAndMessages.ClearSelection);
            wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(mainTable)));
        }

        internal void ReleaseInvoices(out string msg1, out string msg2)
        {
            ButtonClick(ButtonsAndMessages.ReleaseInvoices);
            AcceptAlert(out msg1);
            AcceptAlert(out msg2);
        }

        internal void InvoiceMoveToHistory(out string msg1, out string msg2)
        {
            ButtonClick(ButtonsAndMessages.MoveToHistory_);
            AcceptAlert(out msg1);
            AcceptAlert(out msg2);
        }

        internal void ResetFilter()
        {
            ButtonClick(ButtonsAndMessages.Reset);
            try
            {
                wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(mainTable)));
            }
            catch (WebDriverTimeoutException)
            {
                LoggingHelper.LogException($"Wait for loading message timed out. Loading message not appeared with in 60 seconds.");
            }
        }
        internal void ResetNestedFilter(int tableNumber)
        {

            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }

            string idAttribute = tableElement.GetAttribute("id").ToString();
            Regex regex = new Regex(@"Details_uc_gridview\d_");
            Click(By.XPath("//input[contains(@id, '" + regex.Match(idAttribute).Value + "') and @value='Reset']"));

            wait.Until(ExpectedConditions.StalenessOf(tableElement));
        }

        public bool IsAnyData()
        {
            if (GetRowCountCurrentPage() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
           // return driver.FindElements(emptyGrid).Count == 0;
        }

        public bool IsAnyDataOnNestedGrid()
        {
            if (GetRowCountNestedPage() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            // return driver.FindElements(emptyGrid).Count == 0;
        }

        //Returns true if no data available on Popup page grid
        public bool IsAnyDataOnPopupPage()
        {

            if (IsElementVisible(popupPageNoDataAvailable))               
            {
                return true;
            }
            else
            {
                return false;
                
            }

        }
        
        protected override By GetElement(string Name, string section = null)
        {
            throw new NotImplementedException();
        }

        public override T LoadElements<T>(string page)
        {
            throw new NotImplementedException();
        }

        public void ClickNewButton()
        {
            Click(newButtonInGrid);
        }

        public void ClickDeleteButton()
        {
            Click(deleteAnchor);
        }

        internal void ClickInsertButton()
        {
            Click(insertEdits);
        }

        internal string GetMessageOfPerformedOperation()
        {
            WaitForElementToBeVisible(nestedGridMessage);
            return GetText(nestedGridMessage);
        }

        public void ClickTableHeader(string headerName)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            var reqHeader = headers.Where(a => a.Text.Trim() == headerName).FirstOrDefault();
            Scroll(reqHeader);
            reqHeader.Click();
            wait.Until(ExpectedConditions.StalenessOf(tableElement));
        }

        public bool IsTableHeaderExists(string headerName)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            return headers.Where(a => a.Text.Trim() == headerName).FirstOrDefault() == null ? false : true;
        }

        public void ClickTableHeader(By table, string headerName)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(table));
            var tableElement = driver.FindElement(table);
            var headers = driver.FindElement(table).FindElements(header);
            IWebElement reqHeader = null;
            if (headerName == TableHeaders.SelectAllCheckBox)
            {
                reqHeader = headers.Where(h => h.FindElement(By.XPath(checkBox)) != null).FirstOrDefault();
            }
            else
            {
                reqHeader = headers.Where(a => a.Text.Trim() == headerName).FirstOrDefault();
            }
            Scroll(reqHeader);
            reqHeader.Click();
            WaitForLoadingGrid();
            WaitForOverlayToBeInvisible();
        }

        internal int GetRowCount(By mainTable, bool isMainTable = true)
        {
            int rowCount;
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var rows = tableElement.FindElements(datarow);
            rowCount = rows.Count;

            if (rowCount == 1)
            {
                string rowText = rows[0].Text.Trim();
                if (rowText == "New\r\nNo data to display" || rowText == "No data to display")
                {
                    return 0;
                }
            }

            bool goToHomepage = false;

            while (IsNextPage(mainTable, isMainTable))
            {
                NextPage(mainTable, isMainTable);
                tableElement = driver.FindElement(mainTable);
                rowCount += tableElement.FindElements(datarow).Count();
                goToHomepage = true;
            }

            if (goToHomepage)
            {
                GoToPage(mainTable, isMainTable, 1);
            }

            return rowCount;
        }

        internal void ScrollToHeader(By table, string headerName)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(table));
            var tableElement = driver.FindElement(table);
            var header = tableElement.FindElement(By.XPath(string.Format(headerColumn, headerName)));
            Scroll(header);
        }

        internal void Filter(By mainTable, string headerName, string value)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = tableElement.FindElement(mainTable).FindElements(header);
            Regex regex = new Regex(@"col\d+");
            var desiredHeader = headers.First(h => h.Text.Trim() == headerName);
            string idAttribute = desiredHeader.GetAttribute("id");
            string column = regex.Match(idAttribute).Value + "_";

            var input = tableElement.FindElement(By.XPath(string.Format(inputTextBox, column)));
            EnterTextAfterClear(input, value);
            wait.Until(ExpectedConditions.StalenessOf(input));
            WaitForOverlayToBeInvisible();
        }

        internal void Filter(By mainTable, By header, string headerName, string value)
        {
            headerName = RenameMenuField(headerName);
            value = RenameMenuField(value);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = tableElement.FindElement(mainTable).FindElements(header);
            Regex regex = new Regex(@"col\d+");
            var desiredHeader = headers.First(h => h.Text.Trim() == headerName);
            string idAttribute = desiredHeader.GetAttribute("id");
            string column = regex.Match(idAttribute).Value + "_";

            var input = tableElement.FindElement(By.XPath(string.Format(inputTextBox, column)));
            EnterTextAfterClear(input, value);
            wait.Until(ExpectedConditions.StalenessOf(input));
            WaitForOverlayToBeInvisible();
        }

        internal int GetRecordsCount(By mainTable, string footerName)
        {
            var tableElement = driver.FindElement(mainTable);
            var footerColumns = tableElement.FindElement(mainTable).FindElements(this.footerColumns);
            string text = footerColumns.Where(f => f.Text.Trim().Contains(footerName)).FirstOrDefault().Text;
            return int.Parse(text.Substring(text.IndexOf("=") + 1));
        }

        internal void ClickSelectAllCheckBox(string headerName, int tableNumber = 1)
        {
            headerName = RenameMenuField(headerName);
            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);

            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }
            var headers = tableElement.FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;

            var box = FindElement(By.XPath(checkBox));
            string attribute = box.GetAttribute("Class");
            if(attribute.Contains("CheckBoxUnchecked"))
            {
                driver.FindElement(By.XPath(selectAllXpath)).Click();
            }
            driver.FindElement(By.XPath(selectAllXpath)).Click();
        }

        internal void ClickTableRowByValue(By mainTable, string headerName, string value)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var row = rows.IndexOf(rows.Where(a => a.FindElement(By.XPath(string.Format(tableRow, index))).Text == value).FirstOrDefault());
            rows[row].Click();
            WaitForLoadingGrid();
        }

        internal void ClickTableRowByIndex(By mainTable, string headerName, int rowIndex)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = driver.FindElement(mainTable).FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);
            var column = rows[rowIndex - 1].FindElement(By.XPath(string.Format(tableRow, index)));
            column.Click();
            WaitForLoadingGrid();
        }

        internal void CheckCheckBox(string checkBoxLabel)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainCheckBoxList));
            var checkBoxListElement = driver.FindElement(mainCheckBoxList);
            var checkBoxElement = checkBoxListElement.FindElement(By.XPath(string.Format(labelWithTextCheckBox, checkBoxLabel)));
            string classAttribute = checkBoxElement.GetAttribute("class");
            if (!classAttribute.Contains("CheckBoxChecked"))
            {
                checkBoxElement.Click();
                WaitForElementToBePresent(By.XPath(string.Format(labelWithTextCheckBox, checkBoxLabel) + checkBoxCheckedClass));
            }
        }

        internal void UncheckCheckBox(string checkBoxLabel)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainCheckBoxList));
            var checkBoxListElement = driver.FindElement(mainCheckBoxList);
            var checkBoxElement = checkBoxListElement.FindElement(By.XPath(string.Format(labelWithTextCheckBox, checkBoxLabel)));
            string classAttribute = checkBoxElement.GetAttribute("class");
            if (!classAttribute.Contains("CheckBoxUnchecked"))
            {
                checkBoxElement.Click();
                WaitForElementToBePresent(By.XPath(string.Format(labelWithTextCheckBox, checkBoxLabel) + checkBoxUncheckedClass));
            }
        }

        internal bool IsCheckBoxCheckedWithLabel(string checkBoxLabel)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainCheckBoxList));
            var checkBoxListElement = driver.FindElement(mainCheckBoxList);
            var checkBoxElement = checkBoxListElement.FindElement(By.XPath(string.Format(labelWithTextCheckBox, checkBoxLabel)));
            string classAttribute = checkBoxElement.GetAttribute("class");
            return classAttribute.Contains("CheckBoxChecked");
        }

        internal bool IsCheckBoxExistsWithLabel(string checkBoxLabel)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainCheckBoxList));
            var checkBoxListElement = driver.FindElement(mainCheckBoxList);
            var checkBoxElement = checkBoxListElement.FindElement(By.XPath(string.Format(labelWithTextCheckBox, checkBoxLabel)));
            return checkBoxElement != null;
        }

        internal void CheckTableRowCheckBoxByIndex(By mainTable, int rowNum)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var rows = tableElement.FindElements(datarow);
            var checkBoxElement = rows[rowNum - 1].FindElement(By.XPath(checkBox));
            string classAttribute = checkBoxElement.GetAttribute("class");
            if (!classAttribute.Contains("CheckBoxChecked"))
            {
                var tableCheckBoxElement = rows[rowNum - 1].FindElement(By.XPath(tableCheckBox));
                Scroll(tableCheckBoxElement);
                tableCheckBoxElement.Click();
                WaitForLoadingGrid();
                WaitForOverlayToBeInvisible();
            }
        }

        internal void CheckTableRowCheckBoxByIndex(int rowNum, bool waitForGrid = false)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var rows = tableElement.FindElements(datarow);
            var checkBoxElement = rows[rowNum - 1].FindElement(By.XPath(checkBox));
            string classAttribute = checkBoxElement.GetAttribute("class");
            if (!classAttribute.Contains("CheckBoxChecked"))
            {
                var tableCheckBoxElement = rows[rowNum - 1].FindElement(By.XPath(tableCheckBox));
                Scroll(tableCheckBoxElement);
                tableCheckBoxElement.Click();
                if (waitForGrid)
                {
                    WaitForLoadingGrid();
                    WaitForOverlayToBeInvisible();
                }

            }
        }

        internal void CheckNestedTableRowCheckBoxByIndex(int rowNum, bool waitForGrid = false, int tableNumber = 1)
        {
            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }
            var rows = tableElement.FindElements(datarow);
            var checkBoxElement = rows[rowNum - 1].FindElement(By.XPath(checkBox));
            string classAttribute = checkBoxElement.GetAttribute("class");
            if (classAttribute.Contains("CheckBoxChecked"))
            {
                var tableCheckBoxElement = rows[rowNum - 1].FindElement(By.XPath(tableCheckBox));
                Scroll(tableCheckBoxElement);
                tableCheckBoxElement.Click();
                if (waitForGrid)
                {
                    WaitForLoadingGrid();
                    WaitForOverlayToBeInvisible();
                }

            }
            if (classAttribute.Contains("CheckBoxUnchecked"))
            {
                var tableCheckBoxElement = rows[rowNum - 1].FindElement(By.XPath(tableCheckBox));
                Scroll(tableCheckBoxElement);
                tableCheckBoxElement.Click();
                if (waitForGrid)
                {
                    WaitForLoadingGrid();
                    WaitForOverlayToBeInvisible();
                }

            }

        }

        internal bool IsNestedTableRowCheckBoxUnChecked(int tableNumber = 1)
        {
            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }
            var rows = tableElement.FindElements(datarow);
            var checkBoxElement = rows[0].FindElement(By.XPath(checkBox));
            string classAttribute = checkBoxElement.GetAttribute("class");
            if (classAttribute.Contains("CheckBoxUnchecked"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool IsNestedTableRowCheckBoxDisabled(int tableNumber = 1)
        {
            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }
            var rows = tableElement.FindElements(datarow);
            var checkBoxElement = rows[0].FindElement(By.XPath(checkBox));
            string classAttribute = checkBoxElement.GetAttribute("class");
            if (classAttribute.Contains("CheckBoxCheckedDisabled"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void SetFilterCreiteria(string headerName, string filterCriteria)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = tableElement.FindElement(mainTable).FindElements(header);
            Regex regex = new Regex(@"col\d+");
            var desiredHeader = headers.First(h => h.Text.Trim() == headerName);
            string idAttribute = desiredHeader.GetAttribute("id");
            string column = regex.Match(idAttribute).Value + "_";
            var input = tableElement.FindElement(By.XPath(string.Format(inputTextBox, column)));
            wait.Until(ExpectedConditions.ElementToBeClickable(filterCiteriaBtnBy));
            input.FindElement(filterCiteriaBtnBy).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(filterCiteriaMenuBy));
            var filterCreteriaMenu = driver.FindElement(filterCiteriaMenuBy);
            var items = filterCreteriaMenu.FindElements(filterCiteriaMenuItemsBy);
            items.First(x => x.Text.Trim() == filterCriteria).Click();
            WaitForOverlayToBeInvisible();
        }

        internal void SetFilterCreiteria(By mainTable, string headerName, string filterCriteria)
        {
            headerName = RenameMenuField(headerName);
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            var headers = tableElement.FindElement(mainTable).FindElements(header);
            Regex regex = new Regex(@"col\d+");
            var desiredHeader = headers.First(h => h.Text.Trim() == headerName);
            string idAttribute = desiredHeader.GetAttribute("id");
            string column = regex.Match(idAttribute).Value + "_";
            var input = tableElement.FindElement(By.XPath(string.Format(inputTextBox, column)));
            input.FindElement(filterCiteriaBtnBy).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(filterCiteriaMenuBy));
            var filterCreteriaMenu = driver.FindElement(filterCiteriaMenuBy);
            var items = filterCreteriaMenu.FindElements(filterCiteriaMenuItemsBy);
            items.First(x => x.Text.Trim() == filterCriteria).Click();
            WaitForOverlayToBeInvisible();
        }

        internal void ClickRadioButton(int index)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(mainTable));
            var tableElement = driver.FindElement(mainTable);
            tableElement.FindElements(radioButton)[index - 1].Click();
        }


        #region Nested Table Functions

        internal string GetNestedPageLabel()
        {
            return driver.FindElement(nestedGrid).FindElement(By.XPath(".//span[contains(@id,'lblTitleGridView')]")).Text.Trim();
        }

        internal List<string> ValidateNestedTableHeaders(params string[] headerNames)
        {
            for (int i = 0; i < headerNames.Length; i++)
            {
                if (headerNames[i].ToLower().Contains("dealer") || headerNames[i].ToLower().Contains("fleet"))
                {
                    headerNames[i] = RenameMenuField(headerNames[i]);
                }
            }

            LoggingHelper.LogMessage(LoggerMesages.ValidatingNestedTableHeaders);
            var errorMsgs = new List<string>();

            if (headerNames != null)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(nestedTable));
                var tableElement = driver.FindElement(nestedTable);
                var headers = tableElement.FindElements(header).Where(s => !string.IsNullOrWhiteSpace(s.Text)).ToList();

                if (headerNames.Count() != headers.Count())
                {
                    errorMsgs.Add(ErrorMessages.NestedHeadersCountMismatch);
                }

                foreach (string header in headerNames)
                {
                    if (headers.Any(a => a.Text.TrimEnd() == header))
                    {
                        continue;
                    }
                    else
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.NestedTableHeaderMissing, header));
                    }
                }
            }
            return errorMsgs;
        }

        internal List<string> ValidateNestedTableHeadersByLevel(int level, params string[] headerNames)
        {
            for (int i = 0; i < headerNames.Length; i++)
            {
                if (headerNames[i].ToLower().Contains("dealer") || headerNames[i].ToLower().Contains("fleet"))
                {
                    headerNames[i] = RenameMenuField(headerNames[i]);
                }
            }

            LoggingHelper.LogMessage(LoggerMesages.ValidatingNestedTableHeaders);
            var errorMsgs = new List<string>();

            if (headerNames != null)
            {
                string criteria = nestedTable.Criteria;
                while (level >= 2)
                {
                    criteria = criteria + nestedTable.Criteria;
                    level--;
                }
                By nestedTableCriteria = By.XPath(criteria);
                wait.Until(ExpectedConditions.ElementIsVisible(nestedTableCriteria));
                var tableElement = driver.FindElement(nestedTableCriteria);
                var headers = tableElement.FindElements(header).Where(s => !string.IsNullOrWhiteSpace(s.Text)).ToList();

                if (headerNames.Count() != headers.Count())
                {
                    errorMsgs.Add(ErrorMessages.NestedHeadersCountMismatch);
                }

                foreach (string header in headerNames)
                {
                    if (headers.Any(a => a.Text.TrimEnd() == header))
                    {
                        continue;
                    }
                    else
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.NestedTableHeaderMissing, header));
                    }
                }
            }
            return errorMsgs;
        }

        internal List<string> ValidateNestedGridTabs(params string[] tabNames)
        {
            for (int i = 0; i < tabNames.Length; i++)
            {
                tabNames[i] = RenameMenuField(tabNames[i]);
            }

            LoggingHelper.LogMessage(LoggerMesages.ValidatingTabs);
            var errorMsgs = new List<string>();

            if (tabNames != null)
            {
                wait.Until(ExpectedConditions.ElementIsVisible(nestedGrid));
                var nestedGridElement = driver.FindElement(nestedGrid);
                var tabs = nestedGridElement.FindElements(nestedGridTabsBy).Where(s => !string.IsNullOrWhiteSpace(s.Text)).ToList();

                if (tabNames.Count() != tabs.Count())
                {
                    errorMsgs.Add(ErrorMessages.HeaderCountMisMatch);
                }

                foreach (string tab in tabNames)
                {
                    if (tabs.Any(a => a.Text.TrimEnd() == tab))
                    {
                        continue;
                    }
                    else
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.TabMissing, tab));
                    }
                }
            }
            return errorMsgs;
        }

        internal void ClickNestedGridTab(string tabName)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(nestedGrid));
            var nestedGridElement = driver.FindElement(nestedGrid);
            var tabs = nestedGridElement.FindElements(nestedGridTabsBy).Where(s => !string.IsNullOrWhiteSpace(s.Text.Trim())).ToList();
            var tab = tabs.First(x => x.Text == tabName);
            if (!tab.GetAttribute("class").Contains("activeTab"))
            {
                tab.Click();
                WaitForLoadingMessage();
            }
        }

        ///<summary>
        ///Return first row data by header Name on provided rowNum
        ///</summary>
        internal string GetNestedTableElementByIndex(string headerName, int rowNum = 1, int tableNumber = 1)
        {
            headerName = RenameMenuField(headerName);
            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }
            var headers = tableElement.FindElements(header);
            var index = headers.IndexOf(headers.FirstOrDefault(a => a.Text.Trim() == headerName)) + 1;
            var rows = tableElement.FindElements(datarow);

            var rowVal = rows[rowNum - 1].FindElements(By.XPath(string.Format(tableRow, index)));
            return rowVal.FirstOrDefault().Text;
        }

        internal void FilterNestedTable(string headerName, string value, int tableNumber = 1)
        {
            headerName = RenameMenuField(headerName);
            WaitForAnyElementLocatedBy(nestedTable);
            var tableElements = driver.FindElements(nestedTable);
            IWebElement tableElement;
            if (tableElements.Count > 1)
                tableElement = tableElements[tableNumber - 1];
            else
            {
                tableElement = tableElements[0];
            }
            var headers = tableElement.FindElements(header);
            Regex regex = new Regex(@"col\d+");
            var desiredHeader = headers.First(h => h.Text.Trim() == headerName);
            string idAttribute = desiredHeader.GetAttribute("id");
            string column = regex.Match(idAttribute).Value + "_";
            EnterTextAfterClear(tableElement.FindElement(By.XPath(string.Format(inputTextBox, column))), value);
            tableElements = driver.FindElements(nestedTable);
            idAttribute = tableElement.GetAttribute("id").ToString();
            regex = new Regex(@"Details_uc_gridview\d_");
            Click(By.XPath("//input[contains(@id, '" + regex.Match(idAttribute).Value + "') and @value='Apply Filter']"));

            wait.Until(ExpectedConditions.StalenessOf(tableElement));
        }

        internal List<string> ValidateNestedTableDetails(bool validatePagingDetails, bool validateGroupBy)
        {
            var errorMsg = new List<string>();

            if (validatePagingDetails)
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingNestedPageCounter);

                if (!ValidateNestedPageCounter())
                {
                    errorMsg.Add(ErrorMessages.IncorrectNestedPageCounter);
                }
            }

            if (validateGroupBy)
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingNestedGroupPanel);

                if (!ValidateNestedGroupPanel())
                {
                    errorMsg.Add(ErrorMessages.NestedGroupPanelNotFound);
                }
            }
            return errorMsg;
        }

        #endregion Nested Table Functions

        internal void WaitForEditDialog()
        {
            WaitForElementToBeVisibleShortWait(EditDialog);
        }

        internal void CloseEditDialog()
        {
            try
            {
                Click(closeEdits);
                WaitForElementToBeInvisible(EditDialog);
            }
            catch (WebDriverTimeoutException)
            {
                CloseEditDialog();
            }
        }

        internal void ClosePage(string pageName)
        {
            ScrollDiv();
            var element = driver.FindElement(By.XPath("//li[contains(@class, 'activeTab')]//span[normalize-space()='" + pageName + "']/ancestor::td[1]/following-sibling::td/div[contains(@id, 'btnCloseTab')]"));
            element.Click();
            WaitForElementToBeInvisible(By.XPath("//li[contains(@class, 'activeTab')]//span[normalize-space()='" + pageName + "']"));
        }

        internal void WaitForWelcomePage()
        {
            WaitForElementToBeVisible(By.XPath("//li[contains(@class, 'activeTab') and not(contains(@style, 'display:none;'))]//span[text()='Welcome']"));
        }

        internal void WaitForPopupPageGrid()
        {
            WaitForElementToBeVisible(popupPageGrid);
        }

        internal List<string> ValidateGridControlsPopUpPage()
        {
            var errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingGridButtons);

            if (driver.FindElements(btnFirst).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "First"));
            }
            if (driver.FindElements(btnPrevious).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "Previous"));
            }

            if (driver.FindElements(btnNext).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "Next"));
            }
            if (driver.FindElements(btnLast).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "Last"));
            }

            if (driver.FindElements(searchField).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "Search Field"));
            }
            if (driver.FindElements(btnFind).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "Find"));
            }

            if (driver.FindElements(btnFindNext).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "Find Next"));
            }

            if (driver.FindElements(btnExport).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "Export"));
            }

            if (driver.FindElements(btnRefresh).Count == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonNotFoundOnGrid, "Refresh"));
            }

            return errorMsgs;
        }

        internal bool ValidateExportButtonPopUpPage()
        {
            try
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingFileDownload);
                wait.Until(ExpectedConditions.ElementToBeClickable(btnExport));
                var exportButton = driver.FindElement(btnExport);
                var menu = driver.FindElement(exportMenu);
                var filesToDownload = menu.FindElements(By.TagName("a"));
                foreach (var downloadBtn in filesToDownload)
                {
                    exportButton.Click();
                    if (downloadBtn.Text.Contains("CSV"))
                    {
                        downloadHelper.DownloadFile(ApplicationContext.GetInstance().downloadsDirectory, ".csv", By.LinkText(downloadBtn.Text));
                    }
                    else if (downloadBtn.Text.Contains("PDF"))
                    {
                        downloadHelper.DownloadFile(ApplicationContext.GetInstance().downloadsDirectory, ".pdf", By.LinkText(downloadBtn.Text));
                    }
                    else if (downloadBtn.Text.Contains("MHTML"))
                    {
                        downloadHelper.DownloadFile(ApplicationContext.GetInstance().downloadsDirectory, ".mhtml", By.LinkText(downloadBtn.Text));
                    }
                    else if (downloadBtn.Text.Contains("Excel 2003"))
                    {
                        downloadHelper.DownloadFile(ApplicationContext.GetInstance().downloadsDirectory, ".xls", By.LinkText(downloadBtn.Text));
                    }
                    else if (downloadBtn.Text.Contains("Excel"))
                    {
                        downloadHelper.DownloadFile(ApplicationContext.GetInstance().downloadsDirectory, ".xlsx", By.LinkText(downloadBtn.Text));
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                LoggingHelper.LogException(e.Message);
                return false;
            }
        }

        internal string ValidateSearchPopUpPage(string pageTitle)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(searchField));
            var inputField = driver.FindElement(searchField);
            EnterTextAfterClear(inputField, pageTitle);

            driver.FindElement(btnFind).Click();
            WaitForMsg(ButtonsAndMessages.Loading);

            if (driver.FindElements(By.XPath("//span[contains(@id , 'Hit0')]")).Count == 0)
            {
                return String.Empty;
            }

            driver.FindElement(btnFindNext).Click();

            AcceptAlert(out string alertMsg);

            return alertMsg;
        }

        internal bool ValidatePaginationPopUpPage()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(totalPages));
            var totalPagesLabel = driver.FindElement(totalPages);
            int pageCount = 0;
            int.TryParse(totalPagesLabel.Text, out pageCount);
            if (pageCount > 1)
            {
                int currentPageNumber;
                driver.FindElement(btnNext).Click();
                WaitForMsg(ButtonsAndMessages.Loading);

                int.TryParse(driver.FindElement(currentPage).Text, out currentPageNumber);
                if (currentPageNumber != 2)
                {
                    return false;
                }

                driver.FindElement(btnPrevious).Click();
                WaitForMsg(ButtonsAndMessages.Loading);

                int.TryParse(driver.FindElement(currentPage).Text, out currentPageNumber);
                if (currentPageNumber != 1)
                {
                    return false;
                }

                driver.FindElement(btnLast).Click();
                WaitForMsg(ButtonsAndMessages.Loading);

                int.TryParse(driver.FindElement(currentPage).Text, out currentPageNumber);
                if (currentPageNumber != pageCount)
                {
                    return false;
                }

                driver.FindElement(btnFirst).Click();
                WaitForMsg(ButtonsAndMessages.Loading);

                int.TryParse(driver.FindElement(currentPage).Text, out currentPageNumber);
                if (currentPageNumber != 1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Get Total Number of rows displayed in the Grid
        /// </summary>
        /// <returns>returns total number of rows</returns>
        internal int GetTotalCount()
        {
            int count = 0;
            if (IsAnyData())
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//tr[contains(@id, 'DXFooterRow')]")));
                var bottomPanel = driver.FindElements(By.XPath("//tr[contains(@id, 'DXFooterRow')]//td")).FirstOrDefault();
                string pageCounter = bottomPanel.Text.Split('=')[1];
                if (pageCounter.Contains(','))
                {
                    pageCounter = pageCounter.Replace(",", "");
                }

                int.TryParse(pageCounter, out count);
            }
            return count;
        }
    }
}
