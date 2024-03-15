using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetPartSummaryLocation;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetPartSummaryLocation;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.FleetPartSummaryLocation
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Part Summary - Location")]
    internal class FleetPartSummaryLocation : DriverBuilderClass
    {
        FleetPartSummaryLocationPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetPartSummaryLocation);
            Page = new FleetPartSummaryLocationPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23307" })]
        public void TC_23307(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.FleetPartSummaryLocation), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.FleetPartSummaryLocation).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual("", Page.GetValueDropDown(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.CompanyName));
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.From), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.From));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.To), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.To));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            FleetPartSummaryLocationUtils.GetData(out string from, out string to);
            Page.PopulateGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));               
                string fleetCode = Page.GetFirstRowData(TableHeaders.FleetBillToCode);
                string partNumber = Page.GetFirstRowData(TableHeaders.PartNumber);
                Page.FilterTable(TableHeaders.FleetBillToCode, fleetCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.PartNumber, partNumber), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.PartNumber, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.FleetBillToCode, fleetCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.PartNumber, partNumber), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.PartNumber, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ResetNotWorking);
                Page.ButtonClick(ButtonsAndMessages.Clear);
                Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
                Page.AcceptAlert();
            }

            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25840" })]
        public void TC_25840(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetPartSummaryLocation);
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
