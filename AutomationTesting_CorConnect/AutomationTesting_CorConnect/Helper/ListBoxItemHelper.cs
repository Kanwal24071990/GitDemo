using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Helper
{
    internal class ListBoxItemHelper : PageOperations<Dictionary<string, PageObject>>
    {
        By listItemBy = By.XPath(".//td[contains(@class, 'dxeListBoxItem')]");
        By listItemSelectedBy = By.XPath(".//td[contains(@class, 'dxeListBoxItemSelected')]");

        internal ListBoxItemHelper(IWebDriver driver) : base(driver)
        {
        }

        protected override By GetElement(string Name, string section = null)
        {
            throw new NotImplementedException();
        }

        public override T LoadElements<T>(string page)
        {
            throw new NotImplementedException();
        }

        internal void SelectValueByScroll(By element, string value)
        {
            ScrollUp(element);
            var listBox = driver.FindElement(element);
            string previousSelectedItem = null;
            while (true)
            {
                var listItem = listBox.FindElement(listItemSelectedBy);
                if (listItem.Text.Trim() == value)
                {
                    listItem.Click();
                    break;
                }
                else
                {
                    if (listItem.Text.Trim() == previousSelectedItem)
                        break;
                    SendDownKeys();
                }
                previousSelectedItem = listItem.Text.Trim();
            }
        }


        internal void SelectFirstValue(By element)
        {
            ScrollUp(element);

            var listBox = driver.FindElement(element);
            listBox.FindElements(listItemBy).First().Click();
        }

        private void ScrollUp(By element)
        {
            WaitForElementToBePresent(element);
            var listBox = driver.FindElement(element);
            listBox.FindElements(listItemBy).First().Click();
            string previousSelectedItem = null;
            while (true)
            {
                SendUpKeys();
                var selectedItem = listBox.FindElement(listItemSelectedBy).Text.Trim();
                if (selectedItem == previousSelectedItem)
                {
                    break;
                }
                previousSelectedItem = selectedItem;
            }
        }
    }
}
