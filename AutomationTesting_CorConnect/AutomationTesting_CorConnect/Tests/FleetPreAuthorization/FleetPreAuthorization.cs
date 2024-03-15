using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetPreAuthorization;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.FleetPreAuthorization;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.FleetPreAuthorization
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Pre Authorization")]
    internal class FleetPreAuthorization : DriverBuilderClass
    {
        FleetPreAuthorizationPage Page;


        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetPreAuthorization);
            Page = new FleetPreAuthorizationPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15793" })]
        public void TC_15793(string UserType)
        {
            FleetPreAuthorizationUtil.GetData(out string dealerCode, out string fleetCode);

            if (!string.IsNullOrEmpty(dealerCode) && !string.IsNullOrEmpty(fleetCode))
            {
                Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Page.AreFieldsAvailable(Pages.FleetPreAuthorization);

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.RequestNew), GetErrorMessage(ErrorMessages.RequestNewButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.FleetPreAuthorization).ForEach(x => { Assert.Fail(x); });
                Assert.AreEqual(Page.GetValueDropDown(FieldNames.Status), "All", GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Status));
                FleetPreAuthorizationUtil.GetData(out string dealerCode, out string fleetCode);
                Page.PopulateGrid(dealerCode, fleetCode);


                if (Page.IsAnyDataOnGrid())
                {
                    var errorMsgs = Page.ValidateGridButtons(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.ClearSelection, ButtonsAndMessages.RequestNew, ButtonsAndMessages.Approve, ButtonsAndMessages.Reject);
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                    errorMsgs.AddRange(Page.ValidateTableHeaders(TableHeaders.DealerCode, TableHeaders.DealerName, TableHeaders.DealerAddress, TableHeaders.FleetCode, TableHeaders.FleetName, TableHeaders.Status, TableHeaders.Amount, TableHeaders.RequestedDate, TableHeaders.RequestedLocation, TableHeaders.RequestedName, TableHeaders.Approved_RejectedByName, TableHeaders.Approved_RejectedLocation));

                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }

                    Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                }
            });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25895" })]
        public void TC_25895(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetPreAuthorization);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, null));
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, FleetName));
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