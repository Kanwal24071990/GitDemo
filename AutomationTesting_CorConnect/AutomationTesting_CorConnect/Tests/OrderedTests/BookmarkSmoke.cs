using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.BookmarksMaintenance;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerInvoiceTransactionLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.OrderedTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Bookmark Smoke")]
    internal class BookmarkSmoke : DriverBuilderClass
    {


        private string bookmarkName = CommonUtils.RandomString(9).ToLower();
        DealerInvoiceTransactionLookupPage DITLPage;
        BookmarksMaintenancePage BookmarksMaintenancePage;


        [Category(TestCategory.Smoke)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20558" })]
        public void TC_20558(string UserType)
        {
            menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.DealerInvoiceTransactionLookup), DITLPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            DITLPage.SwitchToAdvanceSearch();
            Assert.IsTrue(DITLPage.IsButtonVisible(ButtonsAndMessages.SaveAsBookmark), GetErrorMessage(ErrorMessages.SaveAsBookmarkNotFound));
            DealerInvoiceTransactionLookupUtil.GetData(out string from, out string to);
            DITLPage.PopulateGrid(from, to);
            Assert.IsTrue(DITLPage.IsAnyDataOnGrid(), GetErrorMessage(ErrorMessages.NoDataOnGrid));
            DITLPage.ButtonClick(ButtonsAndMessages.SaveAsBookmark);
            Assert.IsTrue(DITLPage.SaveBookmark(bookmarkName, bookmarkName), GetErrorMessage(ErrorMessages.CreationBookmarkError));
            Console.WriteLine($"Bookmark Created: [{bookmarkName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20559" })]
        public void TC_20559(string UserType)
        {
            menu.OpenPage(Pages.BookmarksMaintenance);
            BookmarksMaintenancePage = new BookmarksMaintenancePage(driver);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.BookmarksMaintenance, BookmarksMaintenancePage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            BookmarksMaintenancePage.PopulateGrid(bookmarkName);
            Assert.AreEqual(bookmarkName, BookmarksMaintenancePage.GetFirstRowData(TableHeaders.Name), ErrorMessages.ValueMisMatch);
            BookmarksMaintenancePage.ClickEdit();
            BookmarksMaintenancePage.EnterTextAfterClear(FieldNames.Description, "Updated BK");
            BookmarksMaintenancePage.UpdateEditGrid();
            Assert.AreEqual(BookmarksMaintenancePage.GetEditMsg(), "The record has been updated successfully.\r\nPlease use Close button to exit from update form.");
            Console.WriteLine($"Bookmark Updated Successfully: [{bookmarkName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(3), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20560" })]
        public void TC_20560(string UserType)
        {
            menu.OpenPage(Pages.BookmarksMaintenance);
            BookmarksMaintenancePage = new BookmarksMaintenancePage(driver);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.BookmarksMaintenance, BookmarksMaintenancePage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            BookmarksMaintenancePage.PopulateGrid(bookmarkName);
            Assert.AreEqual(bookmarkName, BookmarksMaintenancePage.GetFirstRowData(TableHeaders.Name), ErrorMessages.ValueMisMatch);
            BookmarksMaintenancePage.ClickEdit();
            BookmarksMaintenancePage.ClickElement(FieldNames.IsActive);
            BookmarksMaintenancePage.UpdateEditGrid();
            Assert.AreEqual(BookmarksMaintenancePage.GetEditMsg(), "The record has been updated successfully.\r\nPlease use Close button to exit from update form.");
            Assert.IsTrue(BookmarksMaintenancePage.IsCheckBoxUnchecked(FieldNames.IsActive));
            Console.WriteLine($"Bookmark UnActivated Successfully: [{bookmarkName}]");
        }
    }
}
