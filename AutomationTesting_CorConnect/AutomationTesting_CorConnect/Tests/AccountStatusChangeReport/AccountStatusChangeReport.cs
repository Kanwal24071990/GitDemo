using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.AccountStatusChangeReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountStatusChangeReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.AccountStatusChangeReport
{
    [TestFixture]
    [AllureNUnit]
    [AllureSuite("Account Status Change Report")]
    internal class AccountStatusChangeReport : DriverBuilderClass
    {
        AccountStatusChangeReportPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.AccountStatusChangeReport);
            Page = new AccountStatusChangeReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20748" })]
        public void TC_20748(string UserType)
        {
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDatePicker(FieldNames.FromDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.OpenDatePicker(FieldNames.FromDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.SelectDate(FieldNames.FromDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.FromDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.FromDate));
            Page.OpenDatePicker(FieldNames.ToDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
            Page.OpenDatePicker(FieldNames.ToDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
            Page.SelectDate(FieldNames.ToDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.ToDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.ToDate));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23287" })]
        public void TC_23287(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.AccountStatusChangeReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.AccountStatusChangeReport).ForEach(x=>{ Assert.Fail(x); });
            Assert.AreEqual("", Page.GetValueDropDown(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.CompanyName));
            Assert.AreEqual(CommonUtils.GetDefaultFromDate(), Page.GetValue(FieldNames.FromDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.FromDate));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetValue(FieldNames.ToDate), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.ToDate));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            AccountStatusChangeReportUtils.GetDateData(out string fromDate, out string toDate);
            Page.PopulateGrid(fromDate, toDate);

            if (Page.IsAnyDataOnGrid())
            {

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                
                string dateTimeOfChange = Page.GetFirstRowData(TableHeaders.DateTimeOfChange);
                string accountName = Page.GetFirstRowData(TableHeaders.AccountName);
                Page.FilterTable(TableHeaders.DateTimeOfChange, dateTimeOfChange);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.AccountName, accountName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.AccountName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DateTimeOfChange, dateTimeOfChange);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.AccountName, accountName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.AccountName, CommonUtils.RandomString(10));
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


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25818" })]
        public void TC_25818(string UserType, string dealerUser, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            menu.ImpersonateUser(dealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.AccountStatusChangeReport);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, dealerUser));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.AccountStatusChangeReport);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
        }
    }
}
