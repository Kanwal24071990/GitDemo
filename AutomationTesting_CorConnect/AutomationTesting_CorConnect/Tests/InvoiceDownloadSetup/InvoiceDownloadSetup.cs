using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.InvoiceDownloadSetup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceDownloadSetup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;

namespace AutomationTesting_CorConnect.Tests.InvoiceDownloadSetup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Invoice Download Setup")]
    internal class InvoiceDownloadSetup: DriverBuilderClass
    {
        InvoiceDownloadSetupPage Page;


        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.InvoiceDownloadSetup);
            Page = new InvoiceDownloadSetupPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "14001" })]
        public void TC_14001(string UserType)
        {
            string exportName = CommonUtils.RandomString(5);
            Page.DefineExport(exportName);
            Page.AddColumn(FieldNames.InvoiceDate, true, true);
            Page.AddColumn(FieldNames.ProgramInvoiceNumber, true, false);
            Page.AddColumn(FieldNames.DealerInvoiceNumber, true, false);
            Page.Click(ButtonsAndMessages.Save);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            Assert.IsTrue(InvoiceDownloadSetupUtil.GetInvoice(exportName));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25853" })]
        public void TC_25853(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null, TableHeaders.Name);
            Page.ClosePopupWindow();
            menu.OpenPopUpPage(Pages.InvoiceDownloadSetup);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null, TableHeaders.Name));
            Page.ClosePopupWindow();
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPopUpPage(Pages.InvoiceDownloadSetup);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser, TableHeaders.Name));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
        }

        [TearDown]
        public void DeleteSetup()
        {

        }
    }
}
