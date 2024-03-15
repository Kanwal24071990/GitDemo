using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.PageObjects.OrderRequisition;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.Helper;

namespace AutomationTesting_CorConnect.Tests.OrderRequisition
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Order Requisition")]
    internal class OrderRequisition : DriverBuilderClass
    {
        OrderRequisitionPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.OrderRequisition);
            Page = new OrderRequisitionPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22959" })]
        public void TC_22959(string UserType)
        {
            if (UserType.ToLower() == "fleet")
            {
                Assert.Multiple(() =>
                {
                    LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DealerCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DealerCode));
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FleetCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FleetCode));
                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Close), GetErrorMessage(ErrorMessages.ButtonMissing));
                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.ViewCatalogue), GetErrorMessage(ErrorMessages.ButtonMissing));
                    Page.ButtonClick(ButtonsAndMessages.Close);
                    Assert.IsTrue(driver.WindowHandles.Count == 1);
                    Page.SwitchToMainWindow();
                    menu.OpenPopUpPage(Pages.OrderRequisition);
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DealerCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DealerCode));
                    Page.ClosePopupWindow();
                    Assert.IsTrue(driver.WindowHandles.Count == 1);
                });
            }
            else
            {
                string alertMsg;
                Page.AcceptAlert(out alertMsg);
                Assert.AreEqual(alertMsg, ErrorMessages.OrderRequistionNotAvailable);
            }
        }
    }
}
