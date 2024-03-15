using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetInvoicePreApprovalReport;
using AutomationTesting_CorConnect.PageObjects.FleetInvoices;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetInvoicePreApprovalReport;
using AutomationTesting_CorConnect.Utils.PODiscrepancy;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.FleetInvoicePreApprovalReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Invoice Pre-Approval Report")]
    internal class FleetInvoicePreApprovalReport : DriverBuilderClass
    {
        FleetInvoicePreApprovalReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetInvoicePreApprovalReport);
            Page = new FleetInvoicePreApprovalReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24615" })]
        public void TC_24615(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.FleetInvoicePreApprovalReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.FleetInvoicePreApprovalReport).ForEach(x => { Assert.Fail(x); });
            string[] dropDowns = { "Dealer Name", "Fleet Name" };
            string currentDropDown = null;
            foreach (string dropDown in dropDowns)
            {
                List<string> headerNames = null;
                if (dropDown.Equals("Dealer Name"))
                {
                    currentDropDown = FieldNames.DealerName;
                    headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.DealerName);
                }
                else if (dropDown.Equals("Fleet Name"))
                {
                    currentDropDown = FieldNames.FleetName;
                    headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.FleetName);
                }

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(headerNames.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, currentDropDown));
                    Assert.IsTrue(headerNames.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, currentDropDown));
                });
            }

            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateType, "Program invoice date", "Received Date", "Rejection Date", "Released Date", "Settlement Date"), $"{FieldNames.DateType} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateRange, "Customized date", "Last 7 days", "Last 14 days", "Current month", "Last month"), $"{FieldNames.DateRange} DD: " + ErrorMessages.ListElementsMissing);
            Page.SelectValueFirstRow(FieldNames.DateRange);
            Assert.AreEqual(menu.RenameMenuField(Pages.FleetInvoicePreApprovalReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            Page.ButtonClick(ButtonsAndMessages.Clear);
            Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
            Page.AcceptAlert();
            Page.WaitForLoadingMessage();
            Page.WaitForGridLoad();

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            FleetInvoicePreApprovalReportUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                string dealerCode = Page.GetFirstRowData(TableHeaders.DealerCode);
                Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerCode, dealerCode), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerCode, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerCode, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                if (Page.IsNextPage())
                {
                    Page.GoToPage(2);
                }
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25893" })]
        public void TC_25893(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetInvoicePreApprovalReport);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.DealerName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetName, LocationType.ParentShop, Constants.UserType.Fleet, FleetName));
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
