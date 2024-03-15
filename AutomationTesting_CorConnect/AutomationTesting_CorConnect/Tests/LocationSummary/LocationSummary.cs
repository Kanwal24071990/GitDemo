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
using AutomationTesting_CorConnect.PageObjects.LocationSummary;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils.ASN;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.LocationSummary;

namespace AutomationTesting_CorConnect.Tests.LocationSummary
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("LocationSummary")]
    internal class LocationSummary : DriverBuilderClass
    {
        LocationSummaryPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.LocationSummary);
            Page = new LocationSummaryPage(driver);
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24397" })]
        public void TC_24397(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.LocationSummary), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));
            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.From), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));
            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.To), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            LocationSummaryUtils.GetData(out string FromDate, out string ToDate);
            Page.LoadDataOnGrid(FromDate, ToDate);
            if (Page.IsAnyDataOnGrid())
            {
               
                Page.SetFilterCreiteria(TableHeaders.Location, FilterCriteria.Equals);

                string location = Page.GetFirstRowData(TableHeaders.Location);
                Page.FilterTable(TableHeaders.Location, location);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Location, location), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Location, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                Page.FilterTable(TableHeaders.Location, location);

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
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
