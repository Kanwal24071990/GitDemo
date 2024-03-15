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
    internal class BookmarkDialogHelper : PageOperations<Dictionary<string, PageObject>>
    {
        private By bookmarkDialogBy = By.XPath("//div[contains(@id, 'pcBookmark') and contains(@class, 'mainDiv')]");
        private By bookmarkNameBy = By.XPath("//input[contains(@id, 'pcBookmark') and contains(@id, 'txtName')]");
        private By bookmarkDescBy = By.XPath("//textarea[contains(@id, 'pcBookmark') and contains(@id, 'txtDescription')]");
        private By bookmarkCreateBy = By.XPath("//input[@value='Create'][contains(@id, 'btSave')]");
        private By bookmarkCancelBy = By.XPath("//input[@value='Cancel'][contains(@id, 'btCancel')]");
        private By bookmarkUpdateBy = By.XPath("//input[@value='Update'][contains(@id, 'btBookmarkUpdate')]");
        private By bookmarkCrossBtnBy = By.XPath("//input[contains(@id,'ASPxSplitter1_RightPaneBodyPanel_CallbackPanel_MainPgCntrl_uc_gridview_vertical2_pcBookmark_HCB-1')]");
        public BookmarkDialogHelper(IWebDriver driver) : base(driver)
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

        internal bool SaveBookmark(string bmName, string bmDesc)
        {
            bool isSaved = true;
            wait.Until(AnyElementExists(new By[] { bookmarkDialogBy }));
            EnterTextAfterClear(driver.FindElement(bookmarkNameBy), bmName);
            EnterTextAfterClear(driver.FindElement(bookmarkDescBy), bmDesc);
            Click(bookmarkCreateBy);
            WaitForLoadingMessage();
            if (!CheckForText(string.Format("Bookmark with the name \"{0}\" has been created successfully.", bmName), true))
            {
                isSaved = false;
            }
            Click(bookmarkCancelBy);
            return isSaved;
        }
    }
}
