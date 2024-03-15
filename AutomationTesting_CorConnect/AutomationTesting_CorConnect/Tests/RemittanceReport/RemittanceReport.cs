using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.RemittanceReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.RemittanceReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.RemittanceReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Remittance Report")]
    internal class RemittanceReport : DriverBuilderClass
    {
        RemittanceReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.RemittanceReport);
            Page = new RemittanceReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23294" })]
        public void TC_23294(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.RemittanceReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.RemittanceReport).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual("", Page.GetValueDropDown(FieldNames.Location), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Location));
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.FromDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.ToDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingRightPanelEmpty);
            List<string> gridValidatingErrors = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(gridValidatingErrors.Count == 3, ErrorMessages.RightPanelNotEmpty);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            RemittanceReportUtils.GetDateData(out string fromDate, out string toDate);
            Page.PopulateGrid(fromDate, toDate);

            if (Page.IsAnyDataOnGrid())
            {

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                
                string dealerInvoiceNumber = Page.GetFirstRowData(TableHeaders.DealerInvoiceNumber);
                string originalInvoiceAmount = Page.GetFirstRowData(TableHeaders.OriginalInvoiceAmount);
                Page.FilterTable(TableHeaders.DealerInvoiceNumber, dealerInvoiceNumber);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.OriginalInvoiceAmount, originalInvoiceAmount), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.OriginalInvoiceAmount, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerInvoiceNumber, dealerInvoiceNumber);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.OriginalInvoiceAmount, originalInvoiceAmount), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.OriginalInvoiceAmount, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25904" })]
        public void TC_25904(string UserType, string dealerUser)
        {
            var errorMsgs = Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Dealer, null);
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.RemittanceReport);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultipleColumnsDropdown(FieldNames.Location, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));

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
