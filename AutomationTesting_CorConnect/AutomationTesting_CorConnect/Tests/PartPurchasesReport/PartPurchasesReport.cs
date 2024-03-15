using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.PartPurchasesReport;
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
using AutomationTesting_CorConnect.Utils.PartPurchasesReport;

namespace AutomationTesting_CorConnect.Tests.PartPurchasesReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Part Purchases Report")]
    internal class PartPurchasesReport : DriverBuilderClass
    {
        PartPurchasesReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.PartPurchasesReport);
            Page = new PartPurchasesReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24617" })]
        public void TC_24617(string UserType)
        {
            List<string> errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.PartPurchasesReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.SaveAsBookmark), GetErrorMessage(ErrorMessages.SaveAsBookmarkNotFound));

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

            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateType, "Program invoice date", "Dealer invoice date", "Fleet statement period start date", "Fleet statement period end date", "Fleet approval date", "System received date Fleet", "Settlement Date"), $"{FieldNames.DateType} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.DateRange, "Customized date", "Last 7 days", "Current month", "Current Quarter", "YTD", "Last 12 months"), $"{FieldNames.DateRange} DD: " + ErrorMessages.ListElementsMissing);
            Page.ClickPageTitle();
            var currencyList = CommonUtils.GetActiveCurrencies().ToArray();
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.Currency, currencyList), $"{FieldNames.Currency} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.GroupBy, "Date-Weekly", "Date-Monthly", "Date-Quarterly"), $"{FieldNames.GroupBy} DD: " + ErrorMessages.ListElementsMissing);

            //Page.ButtonClick(ButtonsAndMessages.Clear);
            //Assert.AreEqual(ButtonsAndMessages.ClearSearchCriteriaAlertMessage, Page.GetAlertMessage(), ErrorMessages.AlertMessageMisMatch);
            //Page.AcceptAlert();
            //Page.WaitForMsg(ButtonsAndMessages.Loading);

            Page.ClickPageTitle();
            string dealerCompanyName = PartPurchasesReportUtils.GetDealerCompanyName(UserType);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCompanyName, TableHeaders.AccountCode, dealerCompanyName);

            Page.ClickPageTitle();
            string fleetCompanyName = PartPurchasesReportUtils.GetFleetCompanyName(UserType);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCompanyName, TableHeaders.AccountCode, fleetCompanyName);

            PartPurchasesReportUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(from, to);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridControls());
                Assert.IsTrue(Page.ValidateExportButton());
                Assert.AreEqual(Page.ValidateSearch(Pages.PartPurchasesReport), ButtonsAndMessages.SearchCompleted);
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25896" })]
        public void TC_25896(string UserType, string FleetName)
        {
            var errorMsgs = Page.VerifyLocationNotInMultiSelectDropdown(FieldNames.DealerCompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.FleetCompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null));
            Page.ClosePopupWindow();
            menu.ImpersonateUser(FleetName, Constants.UserType.Fleet);
            menu.OpenPopUpPage(Pages.PartPurchasesReport);
            Page = new PartPurchasesReportPage(driver);
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
