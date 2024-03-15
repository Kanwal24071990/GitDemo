using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.BookmarksMaintenance;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.BookmarksMaintenance;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.BookmarksMaintenance
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Bookmarks Maintenance")]
    internal class BookmarksMaintenance : DriverBuilderClass
    {
        BookmarksMaintenancePage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.BookmarksMaintenance);
            Page = new BookmarksMaintenancePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20209" })]
        public void TC_20209(string UserType)
        {
            var bookMarkId = BookmarksMaintenanceUtil.GetBookMark();
            Assert.IsNotNull(bookMarkId, GetErrorMessage(ErrorMessages.ErrorGettingData));

            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.BookmarksMaintenance, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.BookmarksMaintenance).ForEach(x => { Assert.Fail(x); });

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                Page.PopulateGrid(bookMarkId);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                  
                    
                    errorMsgs.AddRange(Page.VerifyEditFields(Pages.BookmarksMaintenance));
                    
                }
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22236" })]
        public void TC_22236(string UserType)
        {
            Page.OpenDropDown(FieldNames.BookmarkName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.BookmarkName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BookmarkName));

            Page.OpenDropDown(FieldNames.BookmarkName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.BookmarkName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BookmarkName));

            Page.SelectValueFirstRow(FieldNames.BookmarkName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.BookmarkName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BookmarkName));

            string bookmarkName = Page.GetValueDropDown(FieldNames.BookmarkName).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.BookmarkName, bookmarkName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.BookmarkName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BookmarkName));

        }

    }
}
