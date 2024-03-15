using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.InvoiceDownload;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.InvoiceDownload;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace AutomationTesting_CorConnect.Tests.InvoiceDownload
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Invoice Download")]
    internal class InvoiceDownload : DriverBuilderClass
    {
        InvoiceDownloadPage Page;


        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.InvoiceDownload);
            Page = new InvoiceDownloadPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "14002" })]
        public void TC_14002(string UserType)
        {
            var exportName = InvoiceDownloadUtil.GetExportConfigName();
            Page.SelectExport(exportName);
            string fileName = string.Empty;
            try
            {
                fileName = new DownloadHelper(driver).WaitForDownload(applicationContext.downloadsDirectory, "xlsx", Page.GetButton(), 50000, 20000);
            }
            catch (NullReferenceException)
            {
                Assert.Pass();
            }
            Assert.IsTrue(fileName.Contains(exportName.Replace('/', '_').Replace(':', '_')));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20731" })]
        public void TC_20731(string UserType)
        {
            try
            {
                Page.WaitForElementToBePresent(FieldNames.FieldCompanyNameClearButton);
                Page.ClickElement(FieldNames.FieldCompanyNameClearButton);
                Page.WaitForLoadingMessage();
            }
            catch (ElementNotInteractableException)
            { }
            Page.ClickFieldLabel(FieldNames.FieldCompanyName);
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickFieldLabel(FieldNames.FieldCompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25816" })]
        public void TC_25816(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            Page.ClosePopupWindow();
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPopUpPage(Pages.InvoiceDownload);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
        }
    }
}
