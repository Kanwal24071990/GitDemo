using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetDueDateReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetDueDateReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.FleetDueDateReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Due Date Report")]
    internal class FleetDueDateReport : DriverBuilderClass
    {
        FleetDueDateReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetDueDateReport);
            Page = new FleetDueDateReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23301" })]
        public void TC_23301(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.FleetDueDateReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.FleetDueDateReport).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual("", Page.GetValueDropDown(FieldNames.DealerName), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DealerName));
            Assert.AreEqual("Customized date", Page.GetValueDropDown(FieldNames.DateRange), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DateRange));
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.From), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.From));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.To), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.To));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            FleetDueDateReportUtils.GetDateData(out string from, out string to);
            Page.PopulateGrid(from, to);

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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25833" })]
        public void TC_25833(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetDueDateReport);
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
