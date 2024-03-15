using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.Enrollment
{
    internal class EnrollmentPage : PopUpPage
    {
        internal EnrollmentPage(IWebDriver webDriver) : base(webDriver, Pages.Enrollment)
        {
        }
        internal void LoadDataOnGrid(string accountCode)
        {
            EnterTextAfterClear(FieldNames.AccountName, accountCode);
            ClickElement("GO");
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }
    }
}
