using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.LoyaltyStatus;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.LoyaltyStatus
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Loyalty Status")]
    internal class LoyaltyStatus : DriverBuilderClass
    {
        LoyaltyStatusPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.LoyaltyStatus);
            Page = new LoyaltyStatusPage(driver);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25846" })]
        public void TC_25846(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null);
            Page.ClosePopupWindow();
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPopUpPage(Pages.LoyaltyStatus);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
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