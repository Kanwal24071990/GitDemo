using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.PageObjects.ASN;
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
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.Connections;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;

namespace AutomationTesting_CorConnect.Tests.Connections
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Connections")]
    internal class Connections : DriverBuilderClass
    {
        ConnectionsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.Connections);
            Page = new ConnectionsPage(driver);
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24396" })]
        public void TC_24396(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.Connections), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid();
            if (Page.IsAnyDataOnGrid())
            {
            
                Page.SetFilterCreiteria(TableHeaders.Vendor, FilterCriteria.Equals);

                string vendorName = Page.GetFirstRowData(TableHeaders.Vendor);
                Page.FilterTable(TableHeaders.Vendor, vendorName);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Vendor, vendorName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Vendor, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                Page.FilterTable(TableHeaders.Vendor, vendorName);
                errorMsgs = Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter);
                errorMsgs.AddRange(Page.ValidateTableDetails(true, false));

            }
            if (Page.IsNextPage())
            {
                Page.GoToPage(2);
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
