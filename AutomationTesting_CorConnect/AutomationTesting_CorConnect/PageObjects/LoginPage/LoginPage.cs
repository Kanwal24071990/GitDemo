using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using AutomationTesting_CorConnect.DataObjects;
using System.Security.Policy;

namespace AutomationTesting_CorConnect.PageObjects.LoginPage
{
    internal class LoginPage : StaticPage
    {
        internal LoginPage(IWebDriver driver) : base(driver, Pages.Login)
        {
            var values = new Dictionary<string, string>();
            lookupNames.LookupProperty.TryGetValue(CommonUtils.GetClientLower(), out values);

            if (values == null)
            {
                try
                {
                    Console.WriteLine(CommonUtils.GetClientLower());
                    lookupNames.LookupProperty.Add(CommonUtils.GetClientLower(), baseDataAccessLayer.GetCaptions());
                }

                catch (ArgumentException)
                {

                }
            }
        }

        internal void PerformLogin(string userName)
        {
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
            Console.WriteLine(client.URL);
            driver.Navigate().GoToUrl(client.URL);
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            //var user = appContext.UserDatas.Find(x => x.User.ToUpper() == userName.ToUpper());
            var user = client.Users.First(x => x.User.ToUpper() == userName.ToUpper());
            applicationContext.ApplicationContext.GetInstance().UserData = new UserData(user.User, user.Password, UserType.GetObject(user.Type));
            EnterText(FieldNames.UserID, user.User);
            EnterText(FieldNames.Password, user.Password);
            Click(ButtonsAndMessages.SUBMITALLCAPS);
        }
        internal void OpenLoginPage(string url)
        {
            Console.WriteLine(url);
            driver.Navigate().GoToUrl(url);
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            
          
        }
        internal void PerformLogin(Users user, string url)
        {
            Console.WriteLine(url);
            driver.Navigate().GoToUrl(url);
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            applicationContext.ApplicationContext.GetInstance().UserData = new UserData(user.User, user.Password, UserType.GetObject(user.Type));
            EnterText(FieldNames.UserID, user.User);
            EnterText(FieldNames.Password, user.Password);
            Click(ButtonsAndMessages.SUBMITALLCAPS);
        }

        internal void Login( string clientName)
        {
            
            ClientConfiguration client = appContext.ClientConfigurations.First(x => x.Client.ToLower() == clientName);
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            var user = client.Users.First(u => u.Type == "admin");
            EnterText(FieldNames.UserID, user.User);
            EnterText(FieldNames.Password, user.Password);
            Click(ButtonsAndMessages.SUBMITALLCAPS);
            WaitForLoginUserLabelToHaveText(user.User.First().ToString().ToUpper());
            
        }
    }
}
