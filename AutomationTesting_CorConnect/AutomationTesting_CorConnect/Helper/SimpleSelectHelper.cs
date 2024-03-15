using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Helper
{
    internal class SimpleSelectHelper : PageOperations<Dictionary<string, PageObject>>
    {
        internal SimpleSelectHelper(IWebDriver driver) : base(driver)
        {
        }

        public override T LoadElements<T>(string page)
        {
            throw new NotImplementedException();
        }

        protected override By GetElement(string Name, string? Section = null)
        {
            throw new NotImplementedException();
        }

        internal void SelectOptionByText(By xpath, string optionText)
        {
            var element = driver.FindElement(xpath);
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(optionText);
        }

        internal void SelectOptionByValue(By xpath, string optionValue)
        {
            var element = driver.FindElement(xpath);
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(optionValue);
        }

        internal void SelectOptionByIndex(By xpath, int optionIndex)
        {
            if (optionIndex <= 0)
            {
                throw new IndexOutOfRangeException("Provide index starting from 1.");
            }
            var element = driver.FindElement(xpath);
            var selectElement = new SelectElement(element);
            selectElement.SelectByIndex(optionIndex - 1);

        }

        internal string GetSelectedText(By xpath)
        {
                var element = driver.FindElement(xpath);
            var selectElement = new SelectElement(element);
            string selectedVal = selectElement.SelectedOption.GetAttribute("text");
            return selectedVal;
        }

        public bool VerifyValue(By element, params string[] values)
        {
            WaitForElementToBePresent(element);
            var dropDown = driver.FindElement(element);
            var selectElement = new SelectElement(dropDown);
            List<string> els = selectElement.Options.Select(x => x.Text.Trim()).Where(x => ! string.IsNullOrEmpty(x)).ToList();
            
            foreach (var value in values)
            {
                if (els.Any(x => x == value))
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
    }
}
