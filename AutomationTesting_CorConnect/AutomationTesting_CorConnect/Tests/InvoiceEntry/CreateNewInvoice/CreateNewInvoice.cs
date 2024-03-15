using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceEntry.CreateNewInvoice;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;

namespace AutomationTesting_CorConnect.Tests.InvoiceEntry.CreateNewInvoice
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Create New Invoice")]
    internal class CreateNewInvoice : DriverBuilderClass
    {
        CreateNewInvoicePage page;
        CreateNewInvoiceUtils utils = new CreateNewInvoiceUtils();

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.InvoiceEntry);
            page = new InvoiceEntryPage(driver).OpenNewInvoice();
        }

        [Category( TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "13599" })]
        public void TC_13599(string dealerCode, string fleetCode)
        {
            if (utils.UpdateFields() == 0)
            {
                Assert.Fail(ErrorMessages.UnableToExecuteQuery);
            }

            page.PopulateInvoiceWithHeader(dealerCode, fleetCode, out string dealerInvoiceNumber);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(page.IsInputFieldVisible("PO Line #"), GetErrorMessage(ErrorMessages.FieldsNotDisplayed, "PO Line #"));
                Assert.IsTrue(page.IsInputFieldVisible("Fleet Item"), GetErrorMessage(ErrorMessages.FieldsNotDisplayed, "Fleet Item"));
                Assert.IsTrue(page.IsInputFieldVisible("VMRS"), GetErrorMessage(ErrorMessages.FieldsNotDisplayed, "VMRS"));
                Assert.IsTrue(page.IsInputFieldVisible("UNSPSC"), GetErrorMessage(ErrorMessages.FieldsNotDisplayed, "UNSPSC"));
                Assert.IsTrue(page.IsInputFieldVisible("Manufacturer Name"), GetErrorMessage(ErrorMessages.FieldsNotDisplayed, "Manufacturer Name"));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "14268" })]
        public void TC_14268(string dealerCode, string fleetCode)
        {
            page.PopulateInvoice(dealerCode, fleetCode, out string dealerInvoiceNumber);

            utils.GetDealerStateCountry(dealerCode, out string country, out string state);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(country, page.GetCountry(), GetErrorMessage(ErrorMessages.CountryMisMatch));
                Assert.AreEqual(state, page.GetState(), GetErrorMessage(ErrorMessages.StateMisMatch));

                page.ClickSameAsDealerAddress();

                Assert.IsTrue(page.VerifyValueByScrollDown("Country", utils.GetCountries().ToArray()), GetErrorMessage(ErrorMessages.CountriesListMisMatch));

                page.WaitForLoadingGrid();
                page.SelectValueTableDropDown("Country");

                Assert.IsTrue(page.VerifyValueByScrollDown("State/Province", utils.GetStates(page.SelectValueTableDropDown("Country")).ToArray()), GetErrorMessage(ErrorMessages.StateListMisMatch));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15942" })]
        public void TC_15942(string dealerCode, string fleetCode)
        {
            page.PopulateInvoiceWithHeader(dealerCode, fleetCode, out string dealerInvoiceNumber);
            Assert.IsTrue(page.IsElementNotVisible("Discrepancy Information"));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "13993" })]
        public void TC_13993(string dealerCode, string fleetCode, string Item)
        {
            page.PopulateInvoiceWithHeader(dealerCode, fleetCode, out string dealerInvoiceNumber);
            page.SelectLineItemValue(Item);
            page.SubmitInvoice(out string alertMessage);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(alertMessage, "Invoice submission completed successfully.", GetErrorMessage(ErrorMessages.AlertMessageMisMatch));
                Assert.AreEqual(15, CommonUtils.GetNumberOfWorkingDays(Convert.ToDateTime(CommonUtils.GetCurrentDate()),utils.GetApDueDate(dealerInvoiceNumber)),GetErrorMessage(ErrorMessages.NumberofWorkingDaysDiffer));
            });
        }
    }
}


