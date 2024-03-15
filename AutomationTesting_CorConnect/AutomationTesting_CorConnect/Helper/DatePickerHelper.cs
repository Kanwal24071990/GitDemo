using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.PageObjects;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Helper
{
    internal class DatePickerHelper : PageOperations<Dictionary<string, PageObject>>
    {
        private By DropDownButton = By.XPath(".//td[contains(@class,'dxeButtonEditButton')]");
        internal DatePickerHelper(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Open Date Picker
        /// </summary>
        internal void OpenDatePicker(By element)
        {
            Scroll(element);
            IWebElement table;
            var inputField = driver.FindElement(element);
            try
            {
                if (element.Criteria.Contains("StaticSearch"))
                {
                    table = inputField.FindElement(By.XPath("./ancestor::table[contains(@id,'StaticSearch_dxedit_date')]"));
                }
                else
                {
                    table = inputField.FindElement(By.XPath("./ancestor::table[contains(@id,'dateFilter')]"));
                }
            }
            catch (NoSuchElementException)
            {
                table = inputField.FindElement(By.XPath("./ancestor::table[1]"));
            }
            table.FindElement(DropDownButton).Click();
        }

        /// <summary>
        /// Check If date Picker Closed
        /// </summary>
        /// <returns>true or false</returns>
        internal bool IsDatePickerClosed(By element)
        {
            var id = element.Criteria.Substring(element.Criteria.IndexOf(',') + 2, element.Criteria.LastIndexOf("'") - (element.Criteria.IndexOf(',') + 2));
            var tables = driver.FindElements(By.XPath("//table[contains(@id,'" + id + "')]"));
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_C_mt")).FirstOrDefault();
            var ParentDiv = dataTable.FindElement(By.XPath("./ancestor::div[contains(@class,'dxpcDropDown')]"));
            return ParentDiv.GetAttribute("style").Contains("display: none");
        }

        /// <summary>
        /// Selects first date
        /// </summary>
        internal void SelectDate(By element)
        {
            IWebElement table;
            var inputField = driver.FindElement(element);
            try
            {
                if (element.Criteria.Contains("StaticSearch"))
                {
                    table = inputField.FindElement(By.XPath("./ancestor::table[contains(@id,'StaticSearch_dxedit_date')]"));
                }
                else
                {
                    table = inputField.FindElement(By.XPath("./ancestor::table[contains(@id,'dateFilter')]"));
                }
            }
            catch (NoSuchElementException)
            {
                table = inputField.FindElement(By.XPath("./ancestor::table[1]"));
            }

            table.FindElement(DropDownButton).Click();
            var id = element.Criteria.Substring(element.Criteria.IndexOf(',') + 2, element.Criteria.LastIndexOf("'") - (element.Criteria.IndexOf(',') + 2));
            var tables = driver.FindElements(By.XPath("//table[contains(@id,'" + id + "')]"));
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_C_mt")).FirstOrDefault();
            var days = dataTable.FindElements(By.XPath(".//td[contains(@class, 'dxeCalendarDay_')]"));
            if (days.Any(x => x.GetAttribute("class").Contains("dxeCalendarOutOfRange")))
            {
                days = new System.Collections.ObjectModel.ReadOnlyCollection<IWebElement>(days.Where(x => x.GetAttribute("class").Contains("dxeDayInRange")).ToList());
            }
            wait.Until(ExpectedConditions.ElementToBeClickable(days[0]));
            days[0].Click();

        }

        /// <summary>
        /// Selects first by Today button
        /// </summary>
        internal void SelectDateToday(By element)
        {
            IWebElement table;
            var inputField = driver.FindElement(element);
            try
            {
                if (element.Criteria.Contains("StaticSearch"))
                {
                    table = inputField.FindElement(By.XPath("./ancestor::table[contains(@id,'StaticSearch_dxedit_date')]"));
                }
                else
                {
                    table = inputField.FindElement(By.XPath("./ancestor::table[contains(@id,'dateFilter')]"));
                }
            }
            catch (NoSuchElementException)
            {
                table = inputField.FindElement(By.XPath("./ancestor::table[1]"));
            }

            table.FindElement(DropDownButton).Click();
            var id = element.Criteria.Substring(element.Criteria.IndexOf(',') + 2, element.Criteria.LastIndexOf("'") - (element.Criteria.IndexOf(',') + 2));
            var tables = driver.FindElements(By.XPath("//table[contains(@id,'" + id + "')]"));
            var dataTable = tables.Where(x => x.GetAttribute("id").Contains("DDD_C")).FirstOrDefault();
            var btnToday = dataTable.FindElement(By.XPath("//button[(text()='Today')]"));
            wait.Until(ExpectedConditions.ElementToBeClickable(btnToday));
            btnToday.Click();

        }

        public override T LoadElements<T>(string page)
        {
            throw new NotImplementedException();
        }

        protected override By GetElement(string Name, string? Section = null)
        {
            throw new NotImplementedException();
        }
    }
}
