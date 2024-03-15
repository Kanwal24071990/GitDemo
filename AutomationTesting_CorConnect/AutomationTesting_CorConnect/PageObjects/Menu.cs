using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ManageUsers;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.IO;
using Test_CorConnect.src.main.com.corcentric.test.pageobjects.helpers;

namespace AutomationTesting_CorConnect.PageObjects
{
    internal class Menu : Page
    {
        internal Menu(IWebDriver webDriver) : base(webDriver, "Menu")
        {
        }

        internal void OpenPopUpPage(string page)
        {
            var PageElement = PageElements[page];
            var menuName = RenameMenuField(PageElement["MenuName"].ToString());
            var pageName = RenameMenuField(PageElement["Caption"].ToString());
            MoveToElement(driver.FindElement(By.XPath("//span[text()='" + menuName + "']")));
            Click(driver.FindElement(By.XPath("//span[text()='" + pageName + "']")));

            SwitchToPopUp();
        }

        internal void OpenPage(string page, bool waitForGridLoad = true, bool waitForIframe = false, bool isAnotherPage = false)
        {
            var PageElement = PageElements[page];
            var menuName = RenameMenuField(PageElement["MenuName"].ToString());
            var pageName = RenameMenuField(PageElement["Caption"].ToString());
            WaitForElementToBeVisible(By.XPath("//span[text()='" + menuName + "']"));
            MoveToElement(driver.FindElement(By.XPath("//span[text()='" + menuName + "']")));
            if (menuName == pageName)
            {
                Click(driver.FindElements(By.XPath("//span[translate(text(), '\u00a0','')='" + pageName + "']//parent::div[contains(@id, 'DXI')]"))[1]);
            }
            else
            {
                Click(driver.FindElement(By.XPath("//span[translate(text(), '\u00a0','')='" + pageName + "']")));
            }
            if (waitForGridLoad)
            {
                GetGridXpath(pageName);
                WaitForGridLoad();
            }

            if (waitForIframe)
            {
                WaitForIframe();
            }
            if (isAnotherPage)
            {
                WaitForMsg(ButtonsAndMessages.LoadingWithElipsis);
                WaitForGridLoad();
            }
        }

        internal void OpenNextPage(string currentPage, string nextPage, bool isStaticPage = false, bool isSingleGrid = false)
        {
            if (isStaticPage)
            {
                SwitchToMainWindow();
                driver.FindElement(By.XPath("//li[contains(@class,'activeTab')]//span[text()='" + currentPage + "' and contains(@id,'label')]/parent::td//following-sibling::td//span")).Click();
                WaitforGridToHide(currentPage);
                OpenPage(nextPage, false, true);
            }
            else if (isSingleGrid)
            {
                SwitchToMainWindow();
                WaitForAnyElementLocatedBy(By.XPath("//li[contains(@class,'activeTab') and not(contains(@style, 'display:none'))]//span[normalize-space()='" + currentPage + "' and contains(@id,'label')]/parent::td//following-sibling::td//span[text()='x']"));
                driver.FindElement(By.XPath("//li[contains(@class,'activeTab') and not(contains(@style, 'display:none'))]//span[normalize-space()='" + currentPage + "' and contains(@id,'label')]/parent::td//following-sibling::td//span[text()='x']")).Click();
                System.Threading.Thread.Sleep(2000);
                OpenSingleGridPage(nextPage);
            }
            else
            {
                SwitchToMainWindow();
                WaitForAnyElementLocatedBy(By.XPath("//li[contains(@class,'activeTab') and not(contains(@style, 'display:none'))]//span[normalize-space()='" + currentPage + "' and contains(@id,'label')]/parent::td//following-sibling::td//span[text()='x']"));
                driver.FindElement(By.XPath("//li[contains(@class,'activeTab')]//span[normalize-space()='" + currentPage + "' and contains(@id,'label')]/parent::td//following-sibling::td//span")).Click();
                WaitforGridToHide(currentPage);

                if (nextPage == "Fleet Credit Limit")
                {
                    OpenPage(nextPage, false);
                }
                else
                {
                    OpenPage(nextPage);
                }

            }

        }

        internal void SwitchToTab(string tabName)
        {
            driver.FindElement(By.XPath("//li[contains(@class,'dxtc-tab')]//span[text()='" + tabName + "' and contains(@id,'MainPgCntrl')]/parent::td//span")).Click();
        }

        internal void CloseCurrentTab(string tabName)
        {
            driver.FindElement(By.XPath("//li[contains(@class,'activeTab')]//span[text()='" + tabName + "' and contains(@id,'MainPgCntrl')]/parent::td//following-sibling::td//span")).Click();
            WaitForMsg(ButtonsAndMessages.WaitWhileFunctionClosing);
        }

        internal void OpenSingleGridPage(string page)
        {
            var PageElement = PageElements[page];
            var menuName = RenameMenuField(PageElement["MenuName"].ToString());
            var pageName = RenameMenuField(PageElement["Caption"].ToString());
            MoveToElement(driver.FindElement(By.XPath("//span[text()='" + menuName + "']")));
            Click(driver.FindElement(By.XPath("//span[text()='" + pageName + "']")));
            new GridHelper(driver).WaitForTable();
        }

        internal string GetPageCaption(string pageCaption)
        {
            var PageElement = PageElements[pageCaption];
            return RenameMenuField(PageElement["Caption"].ToString());
        }


        protected override By GetElement(string Name, string section = null)
        {
            throw new NotImplementedException();
        }

        public override T LoadElements<T>(string page)
        {
            try
            {
                using (StreamReader r = new StreamReader(GetPageObjectPath() + "\\" + page + ".json"))
                {
                    string jsonString = r.ReadToEnd();
                    var jsonData = JObject.Parse(jsonString);
                    return (T)Convert.ChangeType(jsonData, typeof(T));
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return default;
            }
        }

        internal void ImpersonateUser(string userName, UserType userType = null)
        {
            LogoutImpersonatedUser();
            OpenPage(Pages.ManageUsers);
            ManageUsersPage manageUsers = new ManageUsersPage(driver);
            manageUsers.ImpersonateUser(userName);
            appContext.ImpersonatedUserData = new DataObjects.UserData(userName, null, userType);
        }

        internal void Logout()
        {
            Click(By.XPath("//div[contains(@id, 'btnLogOut')]"));
            if (appContext.ImpersonatedUserData != null)
            {
                appContext.ImpersonatedUserData = null;
            }
            WaitForLoginUserLabelToHaveText(appContext.UserData.User.ToUpper());
        }

        internal void LogoutImpersonatedUser()
        {
            if (appContext.ImpersonatedUserData != null)
            {
                Logout();
            }
        }

        internal int GetTabCount(string tabName)
        {
            tabName = RenameMenuField(tabName);
            return driver.FindElements(By.XPath("//li[contains(@class,'dxtc-tab')]//span[text()='" + tabName + "']")).Count;
        }
    }
}