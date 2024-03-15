using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.FleetReleaseInvoices;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;

namespace AutomationTesting_CorConnect.Tests.FleetReleaseInvoices
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Release Invoices")]
    internal class FleetReleaseInvoices : DriverBuilderClass
    {
        FleetReleaseInvoicesPage Page;
        CreateNewInvoicePage popupPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetReleaseInvoices);
            Page = new FleetReleaseInvoicesPage(driver);
            popupPage = new CreateNewInvoicePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20746" })]
        public void TC_20746(string UserType)
        {
            Page.OpenDropDown(FieldNames.Status);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status));

            Page.OpenDropDown(FieldNames.Status);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status));

            Page.SelectValueFirstRow(FieldNames.Status);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20639" })]
        public void TC_20639(string DealerInvoiceNumber)
        {
            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.FilterTable(TableHeaders.DealerInvoiceNumber, DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInvoiceNumber);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(popupPage.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                Assert.IsTrue(popupPage.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Reject));
                Assert.IsTrue(popupPage.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Release));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20679" })]
        public void TC_20679(string DealerInvoiceNumber)
        {
            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.FilterTable(TableHeaders.DealerInvoiceNumber, DealerInvoiceNumber);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInvoiceNumber);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(popupPage.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
                Assert.IsTrue(popupPage.IsButtonEnabled(ButtonsAndMessages.Reject), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Reject));
                Assert.IsFalse(popupPage.IsButtonEnabled(ButtonsAndMessages.Release), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Release));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25435" })]
        public void TC_25435(string UserType, string DealerName ,string FleetName)
        {
            string invNum = CommonUtils.RandomString(3) + CommonUtils.RandomString(3) + CommonUtils.GetTime();
            Part part = new Part()
            {
                PartNumber = "17TPart",
                CorePrice = 5.00,
                UnitPrice = 5.00
            };
            Assert.IsTrue(new DMSServices().SubmitInvoiceMultipleSections(invNum, DealerName, FleetName, 30, part));

            Page.SelectValueByScroll(FieldNames.Status, "Awaiting Fleet Release");
            Page.LoadDataOnGrid();
            Page.FilterTable(TableHeaders.DealerInvoiceNumber, invNum);
            Page.ClickHyperLinkOnGrid(TableHeaders.DealerInvoiceNumber);
            Page.SwitchToPopUp();
 
            Assert.AreEqual(30, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            Assert.IsFalse(popupPage.IsElementVisible(ButtonsAndMessages.AddSection),"Add Section Button Visible");

            popupPage.Click(ButtonsAndMessages.Release);
            popupPage.WaitForLoadingIcon();
            popupPage.Click(ButtonsAndMessages.Continue);
           
            if (!popupPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Releasing Awaiting Fleet Invoice [{invNum}]");
            }

            Assert.AreEqual(30, InvoiceEntryUtils.GetDiscrepantInvoiceSectionCount(invNum));

            Console.WriteLine($"Successfully Released Awaiting Fleet Invoice: [{invNum}]");
        }
    }
}
