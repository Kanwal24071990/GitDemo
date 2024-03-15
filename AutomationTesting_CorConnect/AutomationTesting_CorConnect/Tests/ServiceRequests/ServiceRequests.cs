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
using AutomationTesting_CorConnect.PageObjects.ServiceRequests;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;

namespace AutomationTesting_CorConnect.Tests.ServiceRequests
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Service Requests")]
    internal class ServiceRequests : DriverBuilderClass
    {
        ServiceRequestsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ServiceRequests);
            Page = new ServiceRequestsPage(driver);
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24401" })]
        public void TC_24401(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ServiceRequests), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));
            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.From), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));
            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.To), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-365)), CommonUtils.GetCurrentDate());
            if (Page.IsAnyDataOnGrid())
            {
                Page.SetFilterCreiteria(TableHeaders.ResponseID, FilterCriteria.Equals);

                string responseID = Page.GetFirstRowData(TableHeaders.ResponseID);
                Page.FilterTable(TableHeaders.ResponseID, responseID);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.ResponseID, responseID), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.ResponseID, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                Page.FilterTable(TableHeaders.ResponseID, responseID);
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
