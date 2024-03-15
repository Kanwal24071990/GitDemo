using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.CommunityFeeReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.CommunityFeeReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.CommunityFeeReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Community Fee Report")]
    internal class CommunityFeeReport : DriverBuilderClass
    {
        CommunityFeeReportPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.CommunityFeeReport);
            Page = new CommunityFeeReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23408" })]
        public void TC_23408(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.CommunityFeeReport, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.CommunityFeeReport).ForEach(x => { Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                CommunityFeeReportUtil.GetData(out string from, out string to);
                CommunityFeeReportUtil.GetCurrencyCode(out string currencyCode);

                Page.PopulateGrid(from, to, currencyCode);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                   
                    string filterHeaderName = TableHeaders.ProgramInvoiceNumber;
                    if (!Page.IsTableHeaderExists(filterHeaderName))
                    {
                        filterHeaderName = TableHeaders.ProgramInv_;
                    }
                    Page.ClickTableHeader(filterHeaderName);
                    Page.ClickTableHeader(filterHeaderName);
                    string programInvoiceNum = Page.GetFirstRowData(filterHeaderName);
                    Page.FilterTable(filterHeaderName, programInvoiceNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(filterHeaderName, programInvoiceNum), ErrorMessages.NoRowAfterFilter);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(filterHeaderName, programInvoiceNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(filterHeaderName, programInvoiceNum), ErrorMessages.NoRowAfterFilter);
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
