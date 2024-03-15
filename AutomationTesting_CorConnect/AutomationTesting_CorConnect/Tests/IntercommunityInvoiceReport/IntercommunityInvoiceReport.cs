using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.IntercommunityInvoiceReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.IntercommunityInvoiceReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.IntercommunityInvoiceReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Intercommunity Invoice Report")]
    internal class IntercommunityInvoiceReport : DriverBuilderClass
    {
        IntercommunityInvoiceReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.IntercommunityInvoiceReport);
            Page = new IntercommunityInvoiceReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22203" })]
        public void TC_22203(string UserType)
        {
            string fleet = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string dealer = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            var currency = AccountMaintenanceUtil.GetCurrencyCodeDetails();
            

            Page.OpenMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCriteriaCompanyName));

            Page.OpenMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.DealerCriteriaCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName, TableHeaders.AccountCode, dealer);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCriteriaCompanyName));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCriteriaCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCriteriaCompanyName));


            Page.OpenMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCriteriaCompanyName));

            Page.OpenMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.FleetCriteriaCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName, TableHeaders.AccountCode, fleet);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCriteriaCompanyName));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCriteriaCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCriteriaCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCriteriaCompanyName));

            Page.OpenMultiSelectDropDown(FieldNames.Currency);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));

            Page.OpenMultiSelectDropDown(FieldNames.Currency);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.Currency));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.Currency, TableHeaders.Currency, currency.currency);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.Currency);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.Currency);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.Currency);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.Currency);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));


            Page.OpenMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));

            Page.OpenMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.TransactionStatus));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, "Current");
            Page.ClearSelectionMultiSelectDropDown(FieldNames.TransactionStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.TransactionStatus);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.TransactionStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.TransactionStatus);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.TransactionStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.TransactionStatus));


        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23584" })]
        public void TC_23584(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.IntercommunityInvoiceReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.SaveAsBookmark), GetErrorMessage(ErrorMessages.SaveAsBookmarkNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Page.AreFieldsAvailable(Pages.IntercommunityInvoiceReport).ForEach(x=>{ Assert.Fail(x); });

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            IntercommunityInvoiceReportUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);
           
            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
              
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
