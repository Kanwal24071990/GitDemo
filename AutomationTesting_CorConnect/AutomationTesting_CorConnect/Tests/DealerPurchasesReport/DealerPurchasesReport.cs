using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.DealerPurchasesReport;
using AutomationTesting_CorConnect.PageObjects.PartPurchasesReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.DealerPurchaseReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.DealerPurchasesReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Dealer Purchases Report")]
    internal class DealerPurchasesReport : DriverBuilderClass
    {
        DealerPurchasesReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.DealerPurchasesReport);
            Page = new DealerPurchasesReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20664" })]
        public void TC_20664(string UserType)
        {

            Page.OpenMultiSelectDropDown(FieldNames.DealerCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCompanyName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));

            string fleet = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string dealer = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCompanyName, TableHeaders.AccountCode, dealer);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.DealerCompanyName, true);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerCompanyName));

            Page.OpenMultiSelectDropDown(FieldNames.FleetCompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCompanyName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCompanyName, TableHeaders.AccountCode, fleet);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.FleetCompanyName, true);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetCompanyName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetCompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCompanyName));

        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24614" })]
        public void TC_24614(string UserType)
        {
            List<string> errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.DealerPurchasesReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.SaveAsBookmark), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            errorMsgs = Page.AreFieldsAvailable();
            Page.ClickPageTitle();
            if (Page.IsDataExistsInDropdown(FieldNames.LoadBookmark))
            {
                List<string> loadBookmarkHeaders = Page.GetHeaderNamesTableDropDown(FieldNames.LoadBookmark);
                Assert.IsTrue(loadBookmarkHeaders.Contains(TableHeaders.Name), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Name, FieldNames.LoadBookmark));
                Assert.IsTrue(loadBookmarkHeaders.Contains(TableHeaders.Description), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Description, FieldNames.LoadBookmark));
            }

            string[] dropDowns = { "Dealer Company Name", "Fleet Company Name" };
            foreach (string dropDown in dropDowns)
            {
                List<string> headerNames = null;
                string currentDropDown = null;
                if (dropDown.Equals("Dealer Company Name"))
                {
                    currentDropDown = FieldNames.DealerCompanyName;
                    headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.DealerCompanyName);
                }

                else if (dropDown.Equals("Fleet Company Name"))
                {
                    currentDropDown = FieldNames.FleetCompanyName;
                    headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.FleetCompanyName);
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

            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateType, "Program invoice date", "Dealer invoice date", "Fleet statement period start date", "Fleet statement period end date", "Fleet approval date", "System received date Fleet", "Settlement Date"), $"{FieldNames.DateType} : " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateRange, "Customized date", "Last 7 days", "Current month", "Current Quarter", "YTD", "Last 12 months"), $"{FieldNames.DateRange} : " + ErrorMessages.ListElementsMissing);
            Page.ClickPageTitle();
            var currencyList = CommonUtils.GetActiveCurrencies().ToArray();
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.Currency, currencyList), $"{FieldNames.Currency} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.GroupBy, "Date-Weekly", "Date-Monthly", "Date-Quarterly"), $"{FieldNames.GroupBy}: " + ErrorMessages.ListElementsMissing);
            Page.ButtonClick(ButtonsAndMessages.Clear);
            Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
            Page.AcceptAlert();
            Page.WaitForMsg(ButtonsAndMessages.Loading);
            Page.SelectAllRowsMultiSelectDropDown(FieldNames.FleetCompanyName, true);
            DealerPurchaseReportUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridControls());
                Assert.IsTrue(Page.ValidateExportButton());
                Assert.AreEqual(Page.ValidateSearch("Report"), ButtonsAndMessages.SearchCompleted);
                Assert.IsTrue(Page.ValidatePagination());
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25889" })]
        public void TC_25889(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationNotInMultiSelectDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            Page.ClosePopupWindow();
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPopUpPage(Pages.DealerPurchasesReport);
            errorMsgs.AddRange(Page.VerifyLocationNotInMultiSelectDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, FleetName));
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
