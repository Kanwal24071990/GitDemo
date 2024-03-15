using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.QlikViewDashboard
{
    internal class QlikViewDashboardPage : Commons
    {
        public QlikViewDashboardPage(IWebDriver driver) : base(driver, Pages.QlikViewDashboard)
        { }

        public string GetPageUrl()
        {
            string pageUrl = driver.Title;
            return pageUrl.Substring(pageUrl.IndexOf(".com/"));
        }

        public List<string> VerifyAlertMessage(string message)
        {
            var errors = new List<string>();
            AcceptAlert(out string alertMsg);
            if (alertMsg != message)
            {
                errors.Add(string.Format("Alert message \"{0}\" is not equals \"{1}\"", alertMsg, message));
            }
            return errors;
        }
    }
}
