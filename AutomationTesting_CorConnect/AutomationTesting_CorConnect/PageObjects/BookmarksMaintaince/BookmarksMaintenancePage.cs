using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.BookmarksMaintenance
{
    internal class BookmarksMaintenancePage : Commons
    {
        internal BookmarksMaintenancePage(IWebDriver webDriver) : base(webDriver, Pages.BookmarksMaintenance)
        {
        }

        internal void PopulateGrid(string bookMarkId)
        {
            SearchAndSelectValueMultipleColumnAfterOpen(FieldNames.BookmarkName, bookMarkId);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

    }
}
