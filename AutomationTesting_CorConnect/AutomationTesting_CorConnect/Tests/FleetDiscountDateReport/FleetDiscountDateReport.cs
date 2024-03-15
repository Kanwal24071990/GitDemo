using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetDiscountDateReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetDiscountDateReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.FleetDiscountDateReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Discount Date Report")]
    internal class FleetDiscountDateReport : DriverBuilderClass
    {
        FleetDiscountDateReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetDiscountDateReport);
            Page = new FleetDiscountDateReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23296" })]
        public void TC_23296(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.FleetDiscountDateReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.FleetDiscountDateReport).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual("", Page.GetValueDropDown(FieldNames.DealerName), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DealerName));
            Assert.AreEqual("Customized date", Page.GetValueDropDown(FieldNames.DateRange), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DateRange));
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.From), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.From));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.To), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.To));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            FleetDiscountDateReportUtils.GetDateData(out string fromDate, out string toDate);
            Page.PopulateGrid(fromDate, toDate);

            if (Page.IsAnyDataOnGrid())
            {

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                Page.ClickTableHeader(TableHeaders.ProgramInvoiceNumber);
                Page.ClickTableHeader(TableHeaders.ProgramInvoiceNumber);
                string programInvoiceNumber = Page.GetFirstRowData(TableHeaders.ProgramInvoiceNumber);
                string fleetCode = Page.GetFirstRowData(TableHeaders.FleetCode);
                Page.FilterTable(TableHeaders.ProgramInvoiceNumber, programInvoiceNumber);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.FleetCode, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.ProgramInvoiceNumber, programInvoiceNumber);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.FleetCode, CommonUtils.RandomString(10));
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25838" })]
        public void TC_25838(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetDiscountDateReport);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null));
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
