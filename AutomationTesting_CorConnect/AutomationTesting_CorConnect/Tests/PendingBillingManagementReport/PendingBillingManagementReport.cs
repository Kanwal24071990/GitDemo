using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.PendingBillingManagementReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.PendingBillingManagementReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.PendingBillingManagementReport
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Pending Billing Management Report")]
    internal class PendingBillingManagementReport : DriverBuilderClass
    {
        PendingBillingManagementReportPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.PendingBillingManagementReport);
            Page = new PendingBillingManagementReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23419" })]
        public void TC_23419(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.PendingBillingManagementReport, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.PendingBillingManagementReport).ForEach(x => { Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                PendingBillingManagementReportUtil.GetData(out string companyName);
                Assert.That(companyName, Is.Not.Null.Or.Not.Empty, $"Null or empty CompanyName returned from DB");
                if (!string.IsNullOrEmpty(companyName))
                {
                    Page.PopulateGrid(companyName);
                    if (Page.IsAnyDataOnGrid())
                    {
                        errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                        errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                        string fleetCode = Page.GetFirstRowData(TableHeaders.FleetCode);
                        Page.FilterTable(TableHeaders.FleetCode, fleetCode);
                        Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                        Page.ClearFilter();
                        Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                        Page.FilterTable(TableHeaders.FleetCode, fleetCode);
                        Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FleetCode, fleetCode), ErrorMessages.NoRowAfterFilter);
                        Page.ResetFilter();
                        Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ResetNotWorking);
                    }
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
