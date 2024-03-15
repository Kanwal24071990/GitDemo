using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerPreAuthorization;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.DealerPreAuthorization;
using AutomationTesting_CorConnect.Utils.PartPriceLookup;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.DealerPreAuthorization
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Pre Authorization")]
    internal class DealerPreAuthorization : DriverBuilderClass
    {
        DealerPreAuthorizationPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerPreAuthorization);
            Page = new DealerPreAuthorizationPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15792" })]
        public void TC_15792(string UserType)
        {
            DealerPreAuthorizationUtils.GetData(out string dealerCode, out string fleetCode);

            if (!string.IsNullOrEmpty(dealerCode) && !string.IsNullOrEmpty(fleetCode))
            {

                
                Assert.Multiple(() =>
                {
                    LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                    Assert.AreEqual(Page.GetPageLabel(), Page.RenameMenuField(Pages.DealerPreAuthorization), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                    LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.RequestNew), GetErrorMessage(ErrorMessages.RequestNewButtonNotFound));

                    LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                    Page.AreFieldsAvailable(Pages.DealerPreAuthorization).ForEach(x => { Assert.Fail(x); });
                    Assert.AreEqual(Page.GetValueDropDown(FieldNames.Status), "All", GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Status));

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

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22163" })]
        public void TC_22163(string UserType)
        {
            DealerPreAuthorizationUtils.GetData(out string dealerCode, out string fleetCode);

            Page.OpenDropDown(FieldNames.Dealer);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Dealer), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Dealer));
            Page.OpenDropDown(FieldNames.Dealer);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Dealer), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Dealer));
            Page.SelectValueFirstRow(FieldNames.Dealer);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Dealer), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Dealer));
            Page.SearchAndSelectValueAfterOpen(FieldNames.Dealer, dealerCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Dealer), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Dealer));
            
            Page.OpenDropDown(FieldNames.Fleet);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Fleet), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Fleet));
            Page.OpenDropDown(FieldNames.Fleet);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Fleet), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Fleet));
            Page.SelectValueFirstRow(FieldNames.Fleet);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Fleet), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Fleet));
            Page.SearchAndSelectValueAfterOpen(FieldNames.Fleet, fleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Fleet), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Fleet));

            Page.OpenDropDown(FieldNames.Status);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            Page.OpenDropDown(FieldNames.Status);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            Page.SelectValueFirstRow(FieldNames.Status);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.Status, "Approved");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25876" })]
        public void TC_25876(string UserType, string DealerName)
        {
            var errorMsgs = Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(DealerName, Constants.UserType.Dealer);
            menu.OpenPage(Pages.DealerPreAuthorization);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, DealerName));
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, null));
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
