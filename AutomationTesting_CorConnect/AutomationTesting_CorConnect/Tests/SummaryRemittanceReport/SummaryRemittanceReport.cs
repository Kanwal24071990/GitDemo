using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.SummaryReconcileReport;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.Resources;
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
using AutomationTesting_CorConnect.PageObjects.SummaryRemittanceReport;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.SummaryReconcileReport;
using AutomationTesting_CorConnect.Utils.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.Utils.SummaryRemittanceReport;

namespace AutomationTesting_CorConnect.Tests.SummaryRemittanceReport
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Summary Remittance Report")]
    internal class SummaryRemittanceReport : DriverBuilderClass
    {
        SummaryRemittanceReportPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.SummaryRemittanceReport);
            Page = new SummaryRemittanceReportPage(driver);
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24769" })]
        public void TC_24769(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.SummaryRemittanceReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.SummaryRemittanceReport).ForEach(x => { Assert.Fail(x); });
            Assert.AreEqual("", Page.GetValueDropDown(FieldNames.Location), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Location));
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.FromDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.ToDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            SummaryRemittanceReportUtils.GetData(out string FromDate, out string ToDate);
            Page.PopulateGrid(FromDate, ToDate);
            if (Page.IsAnyDataOnGrid())
            {                
                Page.SetFilterCreiteria(TableHeaders.AccountName, FilterCriteria.Equals);
                string accountName = Page.GetFirstRowData(TableHeaders.AccountName);
                Page.FilterTable(TableHeaders.AccountName, accountName);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.AccountName, accountName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.AccountName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.AccountName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                Page.FilterTable(TableHeaders.AccountName, accountName);
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                if (Page.IsNextPage())
                {
                    Page.GoToPage(2);
                }
            }
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }
    }
}
