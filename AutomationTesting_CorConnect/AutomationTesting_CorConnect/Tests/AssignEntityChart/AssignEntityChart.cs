using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.AssignEntityChart;
using AutomationTesting_CorConnect.PageObjects.ManageUserNotifications;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.AssignEntityChart
{


    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Assign Entity Chart")]
    internal class AssignEntityChart : DriverBuilderClass
    {
        AssignEntityChartPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenSingleGridPage(Pages.AssignEntityChart);
            Page = new AssignEntityChartPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24223" })]
        public void TC_24223(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
           Assert.AreEqual(menu.RenameMenuField("Assign Charts"), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            if (Page.IsAnyDataOnGrid())
            {
                var errorMsgs = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                string entityTypes = Page.GetFirstRowData(TableHeaders.EntityType);
                Page.FilterTable(TableHeaders.EntityType, entityTypes);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.EntityType, entityTypes), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.EntityType, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.EntityType, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

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
}
