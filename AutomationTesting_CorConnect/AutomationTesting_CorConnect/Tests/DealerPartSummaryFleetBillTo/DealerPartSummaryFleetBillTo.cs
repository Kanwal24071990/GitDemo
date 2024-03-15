using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerPartSummaryFleetBillTo;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerPartSummaryFleetBillTo;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.DealerPartSummaryFleetBillTo
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Part Summary - Fleet Bill To")]
    internal class DealerPartSummaryFleetBillTo : DriverBuilderClass
    {
        DealerPartSummaryFleetBillToPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DealerPartSummaryFleetBillTo);
            Page = new DealerPartSummaryFleetBillToPage(driver);
        }


        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23290" })]
        public void TC_23290(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.DealerPartSummaryFleetBillTo), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.DealerPartSummaryFleetBillTo).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual("", Page.GetValueDropDown(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.CompanyName));
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.From), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.From));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.To), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.To));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            List<string> gridValidatingErrors = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(gridValidatingErrors.Count > 0, ErrorMessages.RightPanelNotEmpty);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            DealerPartSummaryFleetBillToUtils.GetDateData(out string fromDate, out string toDate);
            Page.PopulateGrid(fromDate, toDate);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
               
                string fleetCode = Page.GetFirstRowData(TableHeaders.FleetBillToCode);
                string fleetBillTo = Page.GetFirstRowData(TableHeaders.FleetBillTo);
                Page.FilterTable(TableHeaders.FleetBillToCode, fleetCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetBillTo, fleetBillTo), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.FleetBillTo, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.FleetBillToCode, fleetCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetBillTo, fleetBillTo), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.FleetBillTo, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ResetNotWorking);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25899" })]
        public void TC_25899(string UserType, string dealerUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.DealerPartSummaryFleetBillTo);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));

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
