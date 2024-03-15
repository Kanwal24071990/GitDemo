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
    internal class EmailHelper : PageOperations<Dictionary<string, PageObject>>
    {
        public static By EmailPhone_Edit = By.XPath("//input[@name='loginfmt']");
        public static By obj_EmailPhone_Next_btn = By.XPath("//input[@id='idSIButton9']");
        public static By obj_AccountType_Label = By.XPath("//div[@id='aadTileTitle']");
        public static By obj_Password_Edit = By.XPath("//input[@name='passwd']");
        public static By obj_Password_Submit_btn = By.XPath("//input[@id='idSIButton9']");
        public static By obj_DontShowthisAgain_Chkbox = By.XPath("//input[contains(@id,'KmsiCheckboxField')]");
        public static By obj_Inbox_Link = By.XPath("(//div[@draggable]//span[text()='Inbox'])[1]");
        public static By obj_Focused_Label = By.XPath("//span[text()='Focused']");
        public static By obj_Inbox_Mails_Scrollbar = By.XPath("//div[contains(@class, 'customScrollBar')]//div[contains(@class, 'customScrollBar')]");
        public static By obj_Inbox_Mails_Scrollbar2 = By.XPath("//div[contains(@class, 'customScrollBar')]//div[contains(@class, 'customScrollBar')]/div/div[contains(@aria-label, 'IndSCF SFTP credentials')]");
        public static string obj_Inbox_MailBody_Section = "//div[@id='ReadingPaneContainerId']";
        public static By obj_Delete_Btn = By.XPath("//button[@aria-label='Delete']");
        public static string obj_EmailMessage_Section = "//div[@*='Email message']";
        public static string obj_Attachment_MoreActions_Btn = "//div[@*='attachments']//button/span";
        public static string obj_Download_Btn = "//button[@name='Download']//span[text()='Download']";
        public static string obj_MessageBody_Section = "//div[@*='Message body']";
        public static string obj_To_Receipient_Label = "//a[@class='search-choice-close']";

        public EmailHelper(IWebDriver driver) : base(driver)
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

        public bool loginToOutlook()
        {
            try
            {
                driver.SwitchTo().Window(driver.WindowHandles.First());
                driver.Navigate().GoToUrl("https://outlook.office.com/mail/");
                WaitForButtonToBeClickable(EmailPhone_Edit);
                EnterText(EmailPhone_Edit, "CorconnectAutomation@outlook.com");
                Click(obj_EmailPhone_Next_btn);
                WaitForButtonToBeClickable(obj_Password_Edit);
                EnterText(obj_Password_Edit, "Corcentric#123");
                System.Threading.Thread.Sleep(2000);
                Click(obj_Password_Submit_btn);
                WaitForButtonToBeClickable(obj_DontShowthisAgain_Chkbox);
                Click(obj_DontShowthisAgain_Chkbox);
                Click(obj_Password_Submit_btn);
                WaitForButtonToBeClickable(obj_Inbox_Link);
                Click(obj_Inbox_Link);
                WaitForElementToBeVisible(obj_Inbox_Mails_Scrollbar);
            }

            catch (Exception ex)
            {
                return false;
            }
            return true;

        }

        public string GetEmailBody()
        {

            if (loginToOutlook())
            {
                Click(obj_Inbox_Mails_Scrollbar2);
                System.Threading.Thread.Sleep(2000);
                IWebElement emailBody = driver.FindElement(By.CssSelector("div[aria-label='Message body']"));
                return emailBody.Text;
            }
            else
            {
                return null;
            }
            return null;
        }
    }
}