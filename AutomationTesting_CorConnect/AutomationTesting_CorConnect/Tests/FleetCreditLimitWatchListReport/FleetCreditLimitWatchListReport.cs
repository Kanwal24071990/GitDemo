using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetCreditLimitWatchListReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetCreditLimitWatchListReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.FleetCreditLimitWatchListReport
{


    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Credit Limit Watch List Report")]
    internal class FleetCreditLimitWatchListReport : DriverBuilderClass
    {
        FleetCreditLimitWatchListReportPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetCreditLimitWatchListReport);
            Page = new FleetCreditLimitWatchListReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23417" })]
        public void TC_23417(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.FleetCreditLimitWatchListReport, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.FleetCreditLimitWatchListReport).ForEach(x=>{ Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                FleetCreditLimitWatchListReportUtil.GetData(out string location);
                Page.PopulateGrid(location);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                                      

                    string dealerInvoiceNum = Page.GetFirstRowData(TableHeaders.FleetName);
                    Page.FilterTable(TableHeaders.FleetName, dealerInvoiceNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetName, dealerInvoiceNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Community, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.FleetName, dealerInvoiceNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetName, dealerInvoiceNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Community, CommonUtils.RandomString(10));
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
            });
        }
    }
}
