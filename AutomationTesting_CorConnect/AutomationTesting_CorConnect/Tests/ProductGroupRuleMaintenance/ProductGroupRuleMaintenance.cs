using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ProductGroupRuleMaintenance;
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

namespace AutomationTesting_CorConnect.Tests.ProductGroupRuleMaintenance
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Product Group Rule Maintenance")]
    internal class ProductGroupRuleMaintenance : DriverBuilderClass
    {
        ProductGroupRuleMaintenancePage Page;


        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ProductGroupRuleMaintenance);
            Page = new ProductGroupRuleMaintenancePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24228" })]
        public void TC_24228(string UserType)
        {

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ProductGroupRuleMaintenance), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.ProductGroupRuleMaintenance).ForEach(x => { Assert.Fail(x); });

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.NewProductGroupRule);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid();

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.NewProductGroupRule));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                string groupDisplay = Page.GetFirstRowData(TableHeaders.GroupDisplay);
                Page.FilterTable(TableHeaders.GroupDisplay, groupDisplay);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.GroupDisplay, groupDisplay), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.GroupDisplay, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.GroupDisplay, CommonUtils.RandomString(10));
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
    }
}
