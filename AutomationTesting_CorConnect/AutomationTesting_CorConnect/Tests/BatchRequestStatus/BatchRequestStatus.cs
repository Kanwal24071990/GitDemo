using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.BatchRequestStatus;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.BatchRequestStatus;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.BatchRequestStatus
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Batch Request Status")]
    internal class BatchRequestStatus : DriverBuilderClass
    {
        BatchRequestStatusPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.BatchRequestStatus);
            Page = new BatchRequestStatusPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20750" })]
        public void TC_20750(string UserType)
        {
            Page.OpenDropDown(FieldNames.Status);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            Page.OpenDropDown(FieldNames.Status);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            Page.SelectValueFirstRow(FieldNames.Status);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            Page.OpenDatePicker(FieldNames.From);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.OpenDatePicker(FieldNames.From);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.SelectDate(FieldNames.From);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.From), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.From));
            Page.OpenDatePicker(FieldNames.To);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            Page.OpenDatePicker(FieldNames.To);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
            Page.SelectDate(FieldNames.To);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.To), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.To));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23288" })]
        public void TC_23288(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.BatchRequestStatus), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.BatchRequestStatus).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual("All", Page.GetValueDropDown(FieldNames.Status), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Status));
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.From), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.From));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.To), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.To));
           
            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            BatchRequestStatusUtils.GetDateData(out string fromDate, out string toDate);
            Page.PopulateGrid(fromDate, toDate);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                                
                string status = Page.GetFirstRowData(TableHeaders.Status);
                string scheduledDateTime = Page.GetFirstRowData(TableHeaders.ScheduledDateTime);
                Page.FilterTable(TableHeaders.Status, status);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.ScheduledDateTime, scheduledDateTime), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.ScheduledDateTime, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.Status, status);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.ScheduledDateTime, scheduledDateTime), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.ScheduledDateTime, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ResetNotWorking);
                Page.ButtonClick(ButtonsAndMessages.Clear);
                Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
                Page.AcceptAlert();
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
