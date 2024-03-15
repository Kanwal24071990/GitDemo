using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AutomationTesting_CorConnect.PageObjects
{
    internal abstract class PageOperations<T>
    {
        private By Iframe = By.XPath("//*[contains(@id,'frame')]");
        private By SplitterGrid = By.XPath("//table[@id= 'ASPxSplitter1']");
        private By PageLabel = By.XPath("//span[contains(@id,'lblTitleGridView') or contains(@id,'lblTitle')]");
        protected By emptyGrid = By.XPath("//span[contains(text(),'There is no data available. Please retry with new criteria.')]");
        internal string textXpath = "//*[text()='{0}']";

        private string messageXpath = ".//span[.='{0}' and not(ancestor::table[contains(@style,'display:none')]) and not(ancestor::table[contains(@style,'display: none')])]";
        private string errorMessageXpath = ".//span[contains(@id, 'ErrorMessage')]";
        protected string button = ".//input[@value='{0}'][contains(@id, 'btn') or  contains(@id, 'Btn') ]";
        protected string inputByValue = ".//input[@value='{0}']";
        protected string anchor = ".//a[text()='{0}']";
        protected string anchorSpan = ".//span[text()='{0}']";
        private string text = "//*[text()='{0}']";
        private string displayedText = "//*[text()='{0}' and not(ancestor::td[contains(@style,'visibility: hidden')])]";
        private By loadingIconBy = By.XPath("//div[@id='upProgressgrid' or @id='divLoader'][not(contains(@style, 'display: none'))]");
        private By overlayDivBy = By.XPath("//div[@id='lp1_LD'][not(contains(@style, 'display: none'))]");
        private By errorCellBy = By.XPath("//td[contains(@class, 'ErrorCell') and contains(@style, 'visibility: visible')]");
        private By errorMessageLabelBy = By.XPath("//table//td//span[contains(@id, 'lblError') and not(contains(@style, 'display: none'))]");
        private By labelLoginUserName = By.XPath("//span[contains(@id, 'lblLoginUserName')]");
        private By modalAlert = By.XPath("//div[contains(@class,'ui-dialog ui-corner-all ui-widget ui-widget-content ui-front ui-dialog-buttons ui-draggable')][not(contains(@style, 'display: none'))]");
        private By modalAlertText = By.XPath(".//div[contains(@class,'ui-dialog-content')]");
        private string modalAlertButton = ".//div[contains(@class,'ui-dialog-buttonset')]//button[contains(text(), '{0}') or contains(text(), '{1}')]";

        By[] invisibleLoadingLocators = { By.XPath("//*[@id='upProgressgrid'][contains(@style, 'visibility: hidden')]"),
                By.XPath("//*[@id='UpdateProgress1'][contains(@style, 'display:none')]"),
                By.XPath("//*[@id='UpdateProgress1'][contains(@style, 'display: none')]"),
                By.XPath("//*[@id='lp1'][contains(@style, 'display: none')]"),
                By.XPath("//*[@id='lp1'][contains(@style, 'display:none')]")
        };

        By[] visibleLoadingLocators = {
                By.XPath("//*[@id='upProgressgrid'][contains(@style, 'visibility: visible')]"),
                By.XPath("//*[@id='UpdateProgress1'][contains(@style, 'display: block')]"),
                By.XPath("//*[@id='lp1'][not(contains(@style, 'display: none'))]"),
                By.XPath("//*[@id='lp1'][not(contains(@style, 'display:none'))]")
        };


        private string fileUploadControl = "//input[contains(@id,'UploadControl_TextBox{0}_Input')]";

        protected IWebDriver driver;
        protected IWait<IWebDriver> wait;
        protected IWait<IWebDriver> wait10Sec;
        protected IWait<IWebDriver> wait5Sec;
        internal ApplicationContext appContext;
        protected T PageElements;
        public BaseDataAccessLayer baseDataAccessLayer;
        internal LookupNames lookupNames;

        protected abstract By GetElement(string Name, string? Section = null);

        public abstract T LoadElements<T>(string page);

        internal PageOperations(IWebDriver driver)
        {
            this.driver = driver;

            if (wait == null)
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120.00));
            }
            if (wait10Sec == null)
            {
                wait10Sec = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
            }
            if (wait5Sec == null)
            {
                wait5Sec = new WebDriverWait(driver, TimeSpan.FromSeconds(5.00));
            }

            appContext = ApplicationContext.GetInstance();
            baseDataAccessLayer = BaseDataAccessLayer.GetInstance();
            lookupNames = LookupNames.Instance;

        }

        internal void SendTab(IWebElement element)
        {
            element.SendKeys(Keys.Tab);
        }

        #region Popup

        ///<summary>
        ///Changes Driver context to popup window
        ///</summary>
        internal void SwitchToPopUp()
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        ///<summary>
        ///Changes Driver context to popup window
        ///</summary>
        internal void SwitchToPopUpWithWait(int currentWindowCount)
        {
            wait.Until(d => driver.WindowHandles.Count > currentWindowCount);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        ///<summary>
        ///Changes Driver context to Main window
        ///</summary>
        internal void SwitchToMainWindow()
        {
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        internal void ClosePopupWindow()
        {
            if (driver.CurrentWindowHandle != driver.WindowHandles.Last())
            {
                SwitchToPopUp();
            }

            driver.Close();
            SwitchToMainWindow();
        }

        internal bool WaitForPopupWindowToClose(int waitSeconds = 30)
        {
            int windowCount = driver.WindowHandles.Count();
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds)).Until((x) =>
                {
                    int windowHandleCount = driver.WindowHandles.Count();
                    if (windowHandleCount == 1)
                    {
                        return true;
                    }
                    if (windowCount == windowHandleCount)
                    {
                        return false;
                    }
                    return true;
                });
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        #endregion

        #region Text

        ///<summary>
        ///Enter Text into textbox against Label provided.
        ///<para>In case of duplicate caption, provide section name</para>
        ///</summary>
        internal void EnterText(string fieldCaption, string value, string section = null)
        {
            if (!string.IsNullOrEmpty(value))
            {
                driver.FindElement(GetElement(fieldCaption, section)).SendKeys(value);
            }
        }

        internal void EnterText(By xpath, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                driver.FindElement(xpath).SendKeys(value);
            }
        }

        ///<summary>
        ///Return true if provided text box does not contain class disabled
        ///</summary>
        internal bool IsTextBoxEnabled(By element)
        {
            try
            {
                var inputElement = driver.FindElement(element);
                var disabledClass = inputElement.GetAttribute("class");
                if (!disabledClass.Contains("dxeDisabled"))
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

        internal string GetText(string fieldCaption)
        {
            WaitForElementToBeVisible(fieldCaption);
            return driver.FindElement(GetElement(fieldCaption)).Text;
        }

        internal string GetText(By path)
        {
            return driver.FindElement(path).Text;
        }

        internal bool IsFieldNotEmpty(string caption, string section = null)
        {
            return driver.FindElement(GetElement(caption, section)).GetAttribute("value").Length > 0;
        }

        internal bool IsSpanNotEmpty(string caption, string section = null)
        {
            return driver.FindElement(GetElement(caption, section)).Text.Length > 0;
        }

        internal bool IsCheckBoxChecked(string caption, string section = null)
        {
            IWebElement element = driver.FindElement(GetElement(caption, section));
            if (element.TagName == "input")
            {
                var parentNode = GetParentNode(element);
                return GetParentNode(parentNode).GetAttribute("class").Contains("CheckBoxChecked");
            }
            else
            {
                return element.GetAttribute("class").Contains("CheckBoxChecked");
            }
        }

        internal bool IsRadioButtonChecked(string caption, string section = null)
        {
            IWebElement element = driver.FindElement(GetElement(caption, section));
            if (element.TagName == "input")
            {
                var parentNode = GetParentNode(element);
                return GetParentNode(parentNode).GetAttribute("class").Contains("RadioButtonChecked");
            }
            else
            {
                return element.GetAttribute("class").Contains("RadioButtonChecked");
            }
        }

        internal bool IsCheckBoxUnchecked(string caption, string section = null)
        {
            IWebElement element = driver.FindElement(GetElement(caption, section));
            if (element.TagName == "input")
            {
                var parentNode = GetParentNode(element);
                return GetParentNode(parentNode).GetAttribute("class").Contains("CheckBoxUnchecked");
            }
            else
            {
                return element.GetAttribute("class").Contains("CheckBoxUnchecked");
            }
        }

        internal bool IsCheckBoxDisabled(string caption, string section = null)
        {
            IWebElement element = driver.FindElement(GetElement(caption, section));
            if (element.TagName == "span")
            {
                return element.GetAttribute("class").Contains("Disabled");
            }

            var parentNode = GetParentNode(element);
            return GetParentNode(parentNode).GetAttribute("class").Contains("Disabled");
        }

        private WebElement GetParentNode(IWebElement element)
        {
            return (WebElement)((IJavaScriptExecutor)driver).ExecuteScript(
                                    "return arguments[0].parentNode;", element);
        }

        internal virtual string GetValue(By element)
        {
            return driver.FindElement(element).GetAttribute("value");
        }

        internal virtual string GetValue(string element, string section = null)
        {
          return driver.FindElement(GetElement(element, section)).GetAttribute("value");
        }

        ///<summary>
        ///Return Src attribute
        ///</summary>
        internal string GetSrc(string caption, bool wait = false)
        {
            if (wait)
            {
                WaitForElementToBeVisible(caption);
            }

            return driver.FindElement(GetElement(caption)).GetAttribute("src");
        }

        internal string GetPageLabel()
        {
            WaitForElementToBeVisible(PageLabel);
            return driver.FindElement(PageLabel).Text;
        }

        internal void EnterTextAfterClear(string fieldCaption, string value, string section = null)
        {
            var element = driver.FindElements(GetElement(fieldCaption, section)).Where(x => x.GetAttribute("type") != "hidden").FirstOrDefault();
            element.Clear();
            element.SendKeys(value);
        }

        internal void SetValue(string element, string value, string section = null)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript($"arguments[0].value='{value}';", driver.FindElement(GetElement(element, section)));
        }

        internal void EnterTextAfterClear(IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }

        internal void PressEnterKey(IWebElement element)
        {
            element.SendKeys(Keys.Enter);
        }
        internal void EnterText(IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        internal void ClearText(string fieldCaption, string section = null)
        {
            var element = driver.FindElements(GetElement(fieldCaption, section)).Where(x => x.GetAttribute("type") != "hidden").FirstOrDefault();
            element.Clear();
        }

        internal void ClearText(IWebElement element)
        {
            element.Clear();
        }

        internal void InsertTimeStamp(string caption)
        {
            EnterText(caption, CommonUtils.GetTimeStamp());
        }

        internal void InsertTimeStamp(string caption, out string timeStamp)
        {
            timeStamp = CommonUtils.GetTimeStamp();
            EnterText(caption, timeStamp);
        }

        #endregion

        #region Click

        internal void RecursiveClickWithLoadingGrid(string Caption)
        {
            try
            {
                Click(Caption);
                WaitForLoadingGridToBeVisible();

            }
            catch (WebDriverTimeoutException)
            {
                RecursiveClickWithLoadingGrid(Caption);

            }
        }

        ///<summary>
        ///clicks against Label provided.
        ///<para>In case of duplicate caption, provide section name</para>
        ///</summary>
        internal void Click(string element, string section = null)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(GetElement(element, section)));
        }

        public void Click(By element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(element));
        }

        public void Click(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        internal void ButtonClick(string buttonCaption)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath(string.Format(button, buttonCaption))));
        }

        internal void AnchorClick(string anchorText)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath(string.Format(anchor, anchorText))));
        }

        internal void StaleElementClick(IWebElement element)
        {
            int attempts = 0;

            while (attempts < 2)
            {
                try
                {
                    element.Click();
                    break;
                }
                catch (StaleElementReferenceException)
                {

                }
                attempts++;
            }
        }

        public bool ClickListElements(string list, string option)
        {
            var listElements = driver.FindElement(GetElement(list)).FindElements(By.XPath(".//li"));

            try
            {
                listElements.Where(x => x.Text.Trim() == option).First().Click();
                return true;

            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return false;
            }
        }

        internal void ClickElement(string caption)
        {
            WaitForElementToBeClickable(caption);
            driver.FindElement(GetElement(caption)).Click();
        }

        internal void ClickElement(IWebElement element)
        {
            WaitForElementToBeClickable(element);
            element.Click();
        }

        internal void ClickElementByIndex(string caption, int index)
        {
            WaitForElementToBeClickable(caption);
            var criteria = By.XPath(GetElement(caption).Criteria + $"[{index}]");
            Scroll(criteria);
            driver.FindElement(criteria).Click();
        }

        #endregion

        #region iFrame
        public void WaitForIframe()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(Iframe));
        }

        ///<summary>
        ///Changes Driver Context to iframe
        ///</summary>
        public void SwitchIframe(int index = 0)
        {
            driver.SwitchTo().Frame(index);
        }

        /// <summary>
        /// Changes Driver Context to iframe by IWebElement
        /// </summary>
        /// <param name="frameKey"></param>
        public void SwitchIframe(string frameKey)
        {
            driver.SwitchTo().Frame(driver.FindElement(GetElement(frameKey)));
        }

        #endregion

        #region Wait

        protected void WaitForElementToBeVisible(By xpath)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(xpath));
        }

        internal bool WaitForVisibilityOfAllElementsLocatedBy(ReadOnlyCollection<IWebElement> element)
        {
            try
            {
                wait10Sec.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                LoggingHelper.LogException($"Wait timeout after 10 seconds");
                return false;
            }
        }

        internal void WaitForVisibilityOfAllElementsLocatedBy(By element)
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(element));
        }
        ///<summary>
        ///Wait for Element To Be Visisble
        ///<para>Pass field caption as param</para>
        ///</summary>
        internal void WaitForElementToBeVisible(string caption, string section = null)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(GetElement(caption, section)));
        }

        protected void WaitForElementsCountToBe(IWebElement li, By locator)
        {
            wait.Until(d =>
            {
                return li.FindElements(locator).Count > 0;
            });
        }

        protected void WaitForElementsCountToBe(IWebElement li, By locator, int count)
        {
            wait.Until(d =>
            {
                return li.FindElements(locator).Count == count;
            });
        }

        internal void WaitForMsg(string message)
        {
            try
            {
                wait10Sec.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(string.Format(messageXpath, message))));
            }
            catch (WebDriverTimeoutException)
            {
                LoggingHelper.LogException($"Wait for loading message timed out. Loading message [{message}] not appeared with in 10 seconds.");
            }
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(string.Format(messageXpath, message))));
        }

        protected void WaitForElementToBePresent(By element)
        {
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
        }

        protected void WaitForElementToBeVisibleShortWait(By xpath, int seconds = 3)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementIsVisible(xpath));
        }

        protected void WaitForElementToBeInvisible(By xpath)
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(xpath));
        }

        internal void WaitForElementToBeInvisible(string element)
        {
            WaitForElementToBeInvisible(GetElement(element));
        }

        internal void WaitForStalenessOfElement(string caption, string section = null)
        {
            wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(GetElement(caption, section))));
        }

        internal void WaitForStalenessOfElementBy(By elem)
        {
            wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(elem)));
        }

        protected void WaitForElementToBeInvisibleCustomWait(By xpath, double fromSeconds = 5)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(fromSeconds)).Until(ExpectedConditions.InvisibilityOfElementLocated(xpath));
        }

        protected void WaitForElementToBeClickable(IWebElement element)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        protected void WaitForElementToBeClickable(By element)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        internal void WaitForElementToBeClickable(string caption, string section = null)
        {
            WaitForElementToBeClickable(GetElement(caption, section));
        }

        private void WaitForLoadingGridTobeInvisible()
        {
            wait.Until(AnyElementExists(invisibleLoadingLocators));
        }

        private bool WaitForLoadingGridToBeVisible()
        {
            try
            {
                wait10Sec.Until(AnyElementExists(visibleLoadingLocators));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                LoggingHelper.LogException("Wait for grid load timed out.");
                return false;
            }
        }

        ///<summary>
        ///<para>Wait For Loading Icon to disapear</para>
        ///</summary>
        internal void WaitForLoadingGrid()
        {
            if (WaitForLoadingGridToBeVisible())
            {
                WaitForLoadingGridTobeInvisible();
            }
        }

        internal void WaitForLoadingMessage()
        {

            By[] locators = { By.XPath(string.Format(messageXpath, ButtonsAndMessages.Loading.TrimEnd('…'))), By.XPath(string.Format(messageXpath, ButtonsAndMessages.Loading)), By.XPath(string.Format(messageXpath, ButtonsAndMessages.LoadingWithElipsis)) };
            try
            {
                wait10Sec.Until(AnyElementExists(locators));
            }
            catch (WebDriverTimeoutException)
            {
                LoggingHelper.LogException("Wait for loading message timed out. Loading icon not appeared or hid with in 10 secconds.");
            }
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(string.Format(messageXpath, ButtonsAndMessages.Loading.TrimEnd('…')))));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(string.Format(messageXpath, ButtonsAndMessages.Loading))));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(string.Format(messageXpath, ButtonsAndMessages.LoadingWithElipsis))));


        }

        internal void WaitForAnyElementLocatedBy(string caption, string section = null)
        {
            wait.Until(AnyElementExists(new By[] { GetElement(caption, section) }));
        }

        internal void WaitForAnyElementLocatedBy(By path)
        {
            wait.Until(AnyElementExists(new By[] { path }));
        }

        internal Func<IWebDriver, IWebElement> AnyElementExists(By[] locators)
        {
            return (driver) =>
            {
                foreach (By locator in locators)
                {
                    IReadOnlyCollection<IWebElement> e = driver.FindElements(locator);
                    if (e.Any())
                    {
                        return e.ElementAt(0);
                    }
                }

                return null;
            };
        }

        internal void WaitForTextInTableRow(IWebElement element, By tableRow)
        {
            wait.Until(element => element.FindElements(tableRow).Any(x => !string.IsNullOrEmpty(x.Text)));
        }

        private static Func<IWebDriver, bool> NotClassAttribute(By locator, string className)
        {
            return (driver) =>
            {
                try
                {
                    return !driver.FindElement(locator).GetAttribute("class").Contains(className);
                }
                catch (Exception e)
                {
                    LoggingHelper.LogException(e.Message);
                    return false;
                }
            };
        }

        private static Func<IWebDriver, bool> ContainsClassAttribute(By locator, string className)
        {
            return (driver) =>
            {
                try
                {
                    return driver.FindElement(locator).GetAttribute("class").Contains(className);
                }
                catch (Exception e)
                {
                    LoggingHelper.LogException(e.Message);
                    return false;
                }
            };
        }

        public void WaitForDisabledClass(string element)
        {
            wait.Until(ContainsClassAttribute(GetElement(element), "Disabled"));
        }
        public void WaitForElementNotToHaveClass(string element, string className)
        {
            wait.Until(NotClassAttribute(GetElement(element), className));
        }
        public void WaitForClassChange(string element, string className)
        {
            wait.Until(ContainsClassAttribute(GetElement(element), className));
        }

        protected void WaitForClassChange(IWebElement element, string className)
        {
            wait.Until((driver) =>
            {
                try
                {
                    return element.GetAttribute("class").Contains(className);
                }
                catch (Exception e)
                {
                    LoggingHelper.LogException(e.Message);
                    return false;
                }
            });
        }

        /// <summary>
        /// Custom Wait. Default is 300 Miliseconds
        /// </summary>
        /// <param name="timeSpan">TimeSpan for wait object tobe initialized with.</param>
        /// <returns>IWait object initialized with provided TimeSpan parameter.</returns>
        internal IWait<IWebDriver> CustomWait(TimeSpan? timeSpan = null)
        {
            if (timeSpan == null) { timeSpan = TimeSpan.FromMilliseconds(300); }
            return new WebDriverWait(driver, (TimeSpan)timeSpan);
        }

        internal void WaitForLoginUserLabelToHaveText(string userName)
        {
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(labelLoginUserName, userName));
        }

        internal void WaitForElementToHaveText(string field, string text)
        {
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(GetElement(field), text));
        }

        internal void WaitForButtonToBePresent(string buttonCaption)
        {
            WaitForElementToBePresent(By.XPath(string.Format(button, buttonCaption)));
        }

        internal void WaitForButtonToBeClickable(string buttonCaption)
        {
            WaitForElementToBeClickable(By.XPath(string.Format(button, buttonCaption)));
        }

        internal void WaitForButtonToBeClickable(By xpath)
        {
            WaitForElementToBeClickable(xpath);
        }

        #endregion

        #region actions

        internal void DragAndDrop(IWebElement Sourcelocator, IWebElement Destinationlocator)
        {
            Actions action = new Actions(driver);
            action.DragAndDrop(Sourcelocator, Destinationlocator).Build().Perform();
        }

        internal void MoveToElement(IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Build().Perform();
        }

        #endregion

        #region button

        public bool IsButtonVisible(string buttonCaption)
        {
            try
            {
                return driver.FindElement(By.XPath(string.Format(button, buttonCaption))) != null;
            }
            catch
            {
                return false;
            }
        }

        public string GetTextOnHover(string caption)
        {
            string text = driver.FindElement(GetElement(caption)).GetAttribute("title");
            return text;

        }

        public bool IsInputVisible(string buttonCaption)
        {
            try
            {
                return driver.FindElement(By.XPath(string.Format(inputByValue, buttonCaption))) != null;
            }
            catch
            {
                return false;
            }
        }

        public void ClickInputButton(string buttonCaption)
        {
            try
            {
                Click(By.XPath(string.Format(inputByValue, buttonCaption)));

            }
            catch (Exception ex)
            {

            }
        }

        public bool IsAnchorVisible(string anchorCaption, bool checkAnchorSpan = false)
        {
            try
            {
                if (checkAnchorSpan)
                {
                    return driver.FindElement(By.XPath(string.Format(anchorSpan, anchorCaption))) != null;
                }

                else
                {
                    return driver.FindElement(By.XPath(string.Format(anchor, anchorCaption))) != null;
                }

            }
            catch
            {
                return false;
            }
        }

        #endregion

        ///<summary>
        ///Check if text is visible
        ///</summary>
        internal bool CheckForText(string text, bool WaitForElementToBeVisible = false)
        {
            if (WaitForElementToBeVisible)
            {
                try
                {
                    WaitForElementToBePresent(By.XPath(string.Format(this.text, text)));
                }
                catch (Exception e)
                {
                    LoggingHelper.LogException(e.Message);
                    return false;
                }
            }

            return driver.FindElements(By.XPath(string.Format(this.text, text))).Count == 1;

        }

        //CheckThatElementisDisplayed
        internal bool CheckForTextByVisibility(string text, bool WaitForElementToBeVisible = false)
        {
            if (WaitForElementToBeVisible)
            {
                try
                {
                    WaitForElementToBePresent(By.XPath(string.Format(this.displayedText, text)));
                }
                catch (Exception e)
                {
                    LoggingHelper.LogException(e.Message);
                    return false;
                }
            }

            return driver.FindElements(By.XPath(string.Format(this.displayedText, text))).Count == 1;

        }

        ///<summary>
        ///Return Count of Windows on browser
        ///</summary>
        internal int GetWindowsCount()
        {
            return driver.WindowHandles.Count;
        }

        ///<summary>
        ///Return Count of Elements visible on screen
        ///</summary>
        internal int GetElementCount(string caption)
        {
            return driver.FindElements(GetElement(caption)).Count;
        }

        public bool IsElementNotVisible(string caption)
        {
            return driver.FindElements(GetElement(caption)).Count == 0;
        }

        public bool IsElementVisible(string caption)
        {
            try
            {
                return driver.FindElement(GetElement(caption)) != null;
            }
            catch
            {
                return false;
            }
        }

        public bool IsElementVisible(By element)
        {
            try
            {
                return driver.FindElement(element) != null;
            }
            catch
            {
                return false;
            }
        }

        protected bool IsTextBoxDisplayed(string element, bool WaitForElement = false)
        {
            if (WaitForElement)
            {
                WaitForElementToBePresent(GetElement(element));
            }

            return driver.FindElements(GetElement(element)).Count > 0;
        }

        protected bool IsLabelDisplayed(string labelText, bool WaitForElement = false)
        {
            if (WaitForElement)
            {
                WaitForElementToBePresent(GetElement(labelText));
            }

            return driver.FindElements(GetElement(labelText)).Any(x => x.Displayed);
        }

        internal bool IsTextVisible(string text, bool WaitForElement = false)
        {
            if (WaitForElement)
            {
                WaitForElementToBePresent(By.XPath(string.Format(textXpath, text)));
            }
            return driver.FindElements(By.XPath(string.Format(textXpath, text))).Count > 0;
        }

        /// <summary>
        /// Checks if the checkbox displayed and enabled on page 
        /// </summary>
        /// <param name="element">Caption from Page.json</param>
        /// <param name="WaitForElement">Should wait before checking for display</param>
        /// <returns></returns>
        internal bool IsCheckBoxDisplayed(string element, bool WaitForElement = false, string section = null)
        {
            if (WaitForElement)
            {
                WaitForElementToBePresent(GetElement(element, section));
            }

            return driver.FindElements(GetElement(element, section)).Any(x => x.Enabled);
        }

        public bool IsElementDisplayed(string element, bool WaitForElement = false)
        {
            if (WaitForElement)
            {
                WaitForElementToBePresent(GetElement(element));
            }

            return driver.FindElements(GetElement(element)).Any(x => x.Displayed || x.Enabled);
        }

        public bool IsElementVisibleOnScreen(string element, bool WaitForElement = false)
        {
            Point points = driver.FindElement(GetElement(element)).Location;
            return (points.X > 0 && points.Y > 0);
        }

        public bool IsElementVisibleOnScreen(IWebElement element)
        {
            Point points = element.Location;
            return (points.X > 0 && points.Y > 0);
        }

        public bool IsElementDisplayed(string element, string section)
        {
            return driver.FindElements(GetElement(element, section)).Any(x => x.Displayed || x.Enabled);
        }


        public bool IsElementEnabled(string element, bool WaitForElement = false, string section = null)
        {
            if (WaitForElement)
            {
                WaitForElementToBePresent(GetElement(element, section));
            }

            return driver.FindElements(GetElement(element, section)).Any(x => x.Enabled);
        }

        public bool IsElementEnabledByClass(string element, bool WaitForElement = false)
        {
            if (WaitForElement)
            {
                WaitForElementToBePresent(GetElement(element));
            }

            return !driver.FindElement(GetElement(element)).GetAttribute("class").Contains("Disabled");
        }

        public bool IsElementDisplayed(By element, bool WaitForElement = false)
        {
            if (WaitForElement)
            {
                WaitForElementToBePresent(element);
            }

            return driver.FindElements(element).Any(x => x.Displayed || x.Enabled);
        }

        public bool IsInputFieldVisible(string caption)
        {
            return driver.FindElement(GetElement(caption)).Displayed;
        }

        public bool IsInputFieldEmpty(string caption)
        {
            return driver.FindElement(GetElement(caption)).Text.Equals(string.Empty);
        }

        public bool IsFieldEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public ReadOnlyCollection<IWebElement> FindElements(string element)
        {
            return driver.FindElements(GetElement(element));
        }

        public IWebElement FindElement(By elementBy)
        {
            return driver.FindElement(elementBy);
        }

        public bool GetListElements(string list, params string[] options)
        {
            var listElements = driver.FindElement(GetElement(list)).FindElements(By.XPath(".//li"));

            foreach (var option in options)
            {
                if (listElements.Any(a => a.Text.Trim() == option))
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

        #region Alert
        internal void AcceptWindowAlert(out string alertMessage)
        {
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert al = driver.SwitchTo().Alert();
            alertMessage = al.Text;
            al.Accept();
        }

        internal void AcceptWindowAlert()
        {
            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
        }

        internal void DismissWindowAlert()
        {
            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Dismiss();
        }

        internal void AcceptWindowAlert(string alertMessage)
        {
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert al = driver.SwitchTo().Alert();
            alertMessage = al.Text;
            al.Accept();
        }

        internal virtual string GetWindowAlertMessage()
        {
            wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert al = driver.SwitchTo().Alert();
            return al.Text;
        }

        internal virtual string GetAlertMessage()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(modalAlert));
            var modal = driver.FindElement(modalAlert);
            var alertText = modal.FindElement(modalAlertText).Text;
            return alertText;
        }

        internal void AcceptAlert(bool waitForLoading = true)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(modalAlert));
            var modal = driver.FindElement(modalAlert);
            var btn = modal.FindElement(By.XPath(string.Format(modalAlertButton, "OK".Localize(), "Ok".Localize())));
            btn.Click();
            if (waitForLoading)
            {
                WaitForLoadingMessage();
            }
        }

        internal void AlertClick(string aleartBtnText)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(modalAlert));
            var modal = driver.FindElement(modalAlert);
            var btn = modal.FindElement(By.XPath(string.Format(modalAlertButton, aleartBtnText.Localize(), aleartBtnText.Localize())));
            btn.Click();
        }

        internal void AcceptAlert(out string alertMessage)
        {
            alertMessage = GetAlertMessage();
            AcceptAlert();
        }

        internal void AcceptAlertWithoutLoading(out string alertMessage)
        {
            alertMessage = GetAlertMessage();
            AcceptAlert(false);
        }

        internal void AcceptAlert(string alertMessage)
        {
            alertMessage = GetAlertMessage();
            AcceptAlert();
        }

        internal void DismissAlert()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(modalAlert));
            var modal = driver.FindElement(modalAlert);
            var btn = modal.FindElement(By.XPath(string.Format(modalAlertButton, "Cancel".Localize(), "CANCEL".Localize())));
            btn.Click();
        }
        internal bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException Ex)
            {
                return false;
            }
        }
        #endregion

        internal void Scroll(string caption, string section = null)
        {
            WaitForElementToBeVisible(GetElement(caption, section));
            new Actions(driver).MoveToElement(driver.FindElement(GetElement(caption, section))).Perform();
        }

        protected void Scroll(By element)
        {
            new Actions(driver).MoveToElement(driver.FindElement(element)).Perform();
        }

        protected void Scroll(IWebElement element)
        {
            Actions actions = new(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        protected void SendDownKeys()
        {
            Actions actions = new(driver);
            actions.SendKeys(Keys.Down);
            actions.Perform();
        }

        protected void SendUpKeys()
        {
            Actions actions = new(driver);
            actions.SendKeys(Keys.Up);
            actions.Perform();
        }

        /// <summary>
        /// Click On Page Title
        /// </summary>
        internal void ClickPageTitle()
        {
            var container = driver.FindElement(PageLabel);
            container.Click();
        }
        /// <summary>
        /// Check Title Of Page
        /// </summary>
        internal bool ValidatePageTitle(string pageName)
        {
            string pageTitle = driver.FindElement(PageLabel).Text;
            if (pageName == pageTitle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// Click On Field Label
        /// </summary>
        internal void ClickFieldLabel(string fieldCaption)
        {
            var container = driver.FindElement(GetElement(fieldCaption));
            container.Click();
        }

        /// <summary>
        /// Scoll Div on Two Grid Page
        /// </summary>
        internal void ScrollDiv()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            var ScollableDiv = driver.FindElement(By.XPath("//div[contains(@class,'verticalScroll')]"));
            executor.ExecuteScript("arguments[0].scrollTo(0, 100)", ScollableDiv);
            executor.ExecuteScript("arguments[0].scrollTo(0, 0)", ScollableDiv);
        }

        /// <summary>
        /// Scoll Table on Two Grid Page
        /// </summary>
        internal void ScrollTable()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            var ScollableTable = driver.FindElement(By.XPath("//td[contains(@class, 'scroll')]"));
            executor.ExecuteScript("arguments[0].scrollTo(0, 100)", ScollableTable);
            executor.ExecuteScript("arguments[0].scrollTo(0, 0)", ScollableTable);
        }

        internal string GetPageObjectPath()
        {
            return appContext.PageObjectPath;
        }

        internal string GetPageObjectPath(string Client)
        {
            return appContext.PageObjectPath + Client;
        }

        internal void WaitForLoadingIcon()
        {
            try
            {
                wait10Sec.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(loadingIconBy));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loadingIconBy));
            }
            catch (WebDriverTimeoutException)
            {
                LoggingHelper.LogException("Wait for loading icon timed out. Loading icon not appeared or hid with in 10 secconds.");
            }
        }

        public string GetPerformedOperationMsg()
        {
            var message = driver.FindElements(By.XPath(errorMessageXpath)).Where(x => IsElementVisibleOnScreen(x)).First();
            return message.Text.Trim();
        }

        ///<summary>
        ///Upload file in input using field caption.
        ///<para>In case of duplicate caption, provide section name</para>
        ///</summary>
        internal void UploadFile(string fieldCaption, string file, string section = null)
        {
            string fileName = @file;
            FileInfo fi = new FileInfo(fileName);
            string filePath = fi.FullName;
            if (!string.IsNullOrEmpty(filePath))
            {
                driver.FindElement(GetElement(fieldCaption, section)).SendKeys(filePath);
            }
        }

        ///<summary>
        ///Verify file has been uploaded by validating success message.
        ///</summary>
        internal string VerifyFileUpload()
        {
            try
            {
                return GetAlertMessage();
            }
            catch (UnhandledAlertException)
            {
                return GetAlertMessage();
            }
        }

        internal void WaitForElementToBePresent(string caption)
        {
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(GetElement(caption)));
        }

        internal void WaitForOverlayToBeInvisible()
        {
            WaitForElementToBeInvisible(overlayDivBy);
        }

        ///<summary>
        ///Upload file in input using upload control input index.
        ///<para>In case of duplicate caption, provide section name</para>
        ///</summary>
        internal void UploadFile(int fileUploadControlIndex, string file, string section = null)
        {
            string fileName = @file;
            FileInfo fi = new FileInfo(fileName);
            string filePath = fi.FullName;
            if (!string.IsNullOrEmpty(filePath))
            {
                By uploadControlBy = By.XPath(string.Format(fileUploadControl, fileUploadControlIndex));
                WaitForElementToBePresent(uploadControlBy);
                driver.FindElement(uploadControlBy).SendKeys(filePath);
            }
        }

        internal void WaitForTextToBePresentInElementLocated(string caption, string text)
        {
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(GetElement(caption), text));
        }

        public void WaitForSplitterGridRefresh()
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(SplitterGrid));
            wait.Until(ExpectedConditions.ElementIsVisible(SplitterGrid));
        }

        internal string RenameMenuField(string value)
        {
            var values = new Dictionary<string, string>();

            if (CommonUtils.GetClientLower() != appContext.DefaultClient.ToLower())
            {
                lookupNames.LookupProperty.TryGetValue(CommonUtils.GetClientLower(), out values);

                if (value.Contains("Dealer") && !value.Contains("\\Dealer ") && !value.Contains("/Dealer"))
                {
                    values.TryGetValue("DLR", out string caption);
                    value = value.Replace("Dealer", caption);
                }
                if (value.Contains("Fleet") && !value.Contains("\\Fleet "))
                {
                    values.TryGetValue("FLEET", out string caption);
                    value = value.Replace("Fleet", caption);
                }
                //var tempExceptionMenus = Constants.MenuException.MenuExceptions.
                //   FirstOrDefault(x => x.ClientNameLower == CommonUtils.GetClientLower() && value.Contains(x.OriginalMenuName));
                //if (tempExceptionMenus != null)
                //{
                //    value = value.Replace(tempExceptionMenus.OriginalMenuName, tempExceptionMenus.ExceptionalMenuName);
                //}
                var synonymException = ApplicationContext.GetInstance().SynonymExceptions.FirstOrDefault(x => x.ClientNameLower == CommonUtils.GetClientLower());
                if (synonymException != null)
                {
                    synonymException.RegexReplacements.ForEach(x =>
                    {
                        value = Regex.Replace(value, x.Pattern, x.Replacement);
                    });
                }
            }
            return value;
        }

        public void WaitForElementToHaveFocus(string element)
        {
            wait.Until((x) =>
            {
                try
                {
                    var webElement = driver.FindElement(GetElement(element));
                    if (webElement.GetAttribute("id").Equals(driver.SwitchTo().ActiveElement().GetAttribute("id")))
                        return true;
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }

        internal bool IsErrorCellVisibleWithMessage(string message)
        {
            try
            {
                var errorCells = driver.FindElements(errorCellBy);
                if (errorCells.Count > 0)
                {
                    return errorCells.Any(x => x.Text.Trim().Contains(message));
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        internal bool IsErrorIconVisibleWithTitle(string title)
        {
            try
            {
                var errorIcon = driver.FindElement(By.XPath("//td[contains(@class, 'ErrorCell') and not(contains(@style, 'visibility:hidden')) " +
                    "and not(contains(@style, 'display:none')) and not(contains(@style, 'display: none'))]//img"));
                return errorIcon.GetAttribute("title").Trim().Contains(title);
            }
            catch
            {
                return false;
            }
        }

        internal bool IsErrorLabelMessageVisibleWithMsg(string message, out string actualMessage)
        {
            actualMessage = null;
            try
            {
                WaitForElementToBeVisible(errorMessageLabelBy);
                var errorCells = driver.FindElements(errorMessageLabelBy);
                if (errorCells.Count > 0)
                {
                    actualMessage = errorCells[0].Text.Trim();
                    return errorCells.Any(x => x.Text.Trim().Contains(message));
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public void Refresh()
        {
            driver.Navigate().Refresh();
        }

        internal void WaitForElementToHaveValue(string field, string value)
        {
            wait.Until((driver) =>
            {
                return driver.FindElement(GetElement(field)).GetAttribute("value").Equals(value);
            });
        }

        internal bool IsTextBoxEnabled(string caption)
        { 
            return IsTextBoxEnabled(GetElement(caption));
        }

        internal string GetURL()
        {
            return driver.Url;
        }
    }
}
