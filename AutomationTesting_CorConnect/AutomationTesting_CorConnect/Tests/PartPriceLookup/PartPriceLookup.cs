using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.PartPriceLookup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.PartPriceLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.PartPriceLookup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Part Price Lookup")]
    internal class PartPriceLookup : DriverBuilderClass
    {
        PartPriceLookupPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.PartPriceLookup, false, true);
            menu.SwitchIframe();
            Page = new PartPriceLookupPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20245" })]
        public void TC_20245(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.PartPriceLookup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Submit), GetErrorMessage(ErrorMessages.SubmitButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                string dealerCode = PartPriceLookupUtil.GetDealerCode();
                string fleetCode = PartPriceLookupUtil.GetFleetCode();
                string partNumber = PartPriceLookupUtil.GetPartNumber();
                string date = CommonUtils.GetCurrentDate();

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                if (string.IsNullOrEmpty(dealerCode))
                    Assert.Fail("Dealer Code is empty");
                if (string.IsNullOrEmpty(fleetCode))
                    Assert.Fail("Fleet Code is empty");
                if (string.IsNullOrEmpty(partNumber))
                    Assert.Fail("Part Number is empty");

                Page.PopulateGrid(dealerCode, fleetCode, partNumber, date);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                    
                }

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22167" })]
        public void TC_22167(string UserType)
        {
            string dealerCode = PartPriceLookupUtil.GetDealerCode();
            string fleetCode = PartPriceLookupUtil.GetFleetCode();
            string partNumber = PartPriceLookupUtil.GetPartNumber();

            Page.OpenDatePicker(FieldNames.Date);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.Date), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.Date));
            Page.SelectDate(FieldNames.Date);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.Date), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.Date));

            Page.OpenDropDown(FieldNames.DealerCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
            Page.SelectValueFirstRow(FieldNames.DealerCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));
            Page.SearchAndSelectValueAfterOpen(FieldNames.DealerCode, dealerCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCode));

            Page.OpenDropDown(FieldNames.FleetCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.SelectValueFirstRow(FieldNames.FleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));

            Page.SearchAndSelectValue(FieldNames.PartNumber, partNumber);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PartNumber), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PartNumber));
            Page.OpenDropDown(FieldNames.PartNumber);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PartNumber), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PartNumber));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25877" })]
        public void TC_25877(string UserType)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerCode, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.FleetCode, LocationType.ParentShop, Constants.UserType.Fleet, null));
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
