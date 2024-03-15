using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.MasterBillingStatementConfiguration;
using AutomationTesting_CorConnect.PageObjects.MasterBillingStatementConfiguration.CreateNewMasterBillingStatementConfiguration;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.MasterBillingStatementConfiguration
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Master/Billing Statement Configuration")]
    internal class MasterBillingStatementConfig : DriverBuilderClass
    {
        MasterBillingStatementConfigurationPage Page;
        string communityStatementName;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.MasterBillingStatementConfiguration);
            Page = new MasterBillingStatementConfigurationPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21060" })]
        public void TC_21060(string UserType)
        {
            communityStatementName += CommonUtils.RandomString(8);
            Page.PopulateGrid();
            CreateNewMasterBillingStatementConfigurationPage newStatementPage = Page.OpenCreateNewStatement();
            newStatementPage.CreateNewStatementConfiguration(communityStatementName);
            Page.SwitchToMainWindow();

            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage($"CommunityStateName: [{communityStatementName}]");
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.MasterBillingStatementConfiguration, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                Page.PopulateGrid(communityStatementName);
                CreateNewMasterBillingStatementConfigurationPage editStatementPage = Page.OpenEditStatement();
                var errorMsgs = editStatementPage.DeactivateStatement(communityStatementName);
                Page.SwitchToMainWindow();

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
            Page.DeleteStatementConfiguration(communityStatementName);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22230" })]
        public void TC_22230(string UserType)
        {
            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.FleetName));

            Page.OpenMultiSelectDropDown(FieldNames.FleetName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.FleetName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.FleetName));

            string fleet = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string dealer = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetName, TableHeaders.AccountCode, fleet);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.FleetName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.FleetName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.FleetName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetName));

            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.DealerName));

            Page.OpenMultiSelectDropDown(FieldNames.DealerName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.DealerName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownClosed, FieldNames.DealerName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerName, TableHeaders.AccountCode, dealer);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));

            Page.SelectAllRowsMultiSelectDropDown(FieldNames.DealerName);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));

            Page.SelectFirstRowMultiSelectDropDown(FieldNames.DealerName);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.DealerName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerName));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24227" })]
        public void TC_24227(string UserType)
        {

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.MasterBillingStatementConfiguration), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.MasterBillingStatementConfiguration).ForEach(x => { Assert.Fail(x); });

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateConfiguration);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.PopulateGrid();

            if (Page.IsAnyDataOnGrid())
            {

                string dealerName = Page.GetFirstRowData(TableHeaders.DealerName);
                Page.FilterTable(TableHeaders.DealerName, dealerName);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerName, dealerName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);


                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateConfiguration));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                CreateNewMasterBillingStatementConfigurationPage newStatementPage = Page.OpenCreateNewStatement();
                errorMsgs.AddRange(newStatementPage.VerifyCreateFields());

                newStatementPage.ClosePopupWindow();
                Page.SwitchToMainWindow();
                string communityStatementName = Page.GetFirstRowData(TableHeaders.CommunityStatementName);
                string paymentTerms = Page.GetFirstRowData(TableHeaders.PaymentTerms);

                CreateNewMasterBillingStatementConfigurationPage editStatementPage = Page.OpenEditStatement();
                Assert.AreEqual(communityStatementName, editStatementPage.GetValue(FieldNames.CommunityStatementName), ErrorMessages.ValueMisMatch);
                Assert.AreEqual(paymentTerms, editStatementPage.GetValueOfDropDown(FieldNames.PaymentTerms), ErrorMessages.ValueMisMatch);
            }
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22229" })]
        public void TC_22229(string UserType)
        {
            Page.OpenDatePicker(FieldNames.EffectiveDate);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));

            Page.OpenDatePicker(FieldNames.EffectiveDate);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));

            Page.SelectDate(FieldNames.EffectiveDate);
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.EffectiveDate), GetErrorMessage(ErrorMessages.DatePickerNotClosed, FieldNames.EffectiveDate));

            Page.OpenDropDown(FieldNames.FleetGroup);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetGroup));

            Page.OpenDropDown(FieldNames.FleetGroup);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetGroup));

            Page.SelectValueFirstRow(FieldNames.FleetGroup);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetGroup));

            string fleetGroup = Page.GetValueDropDown(FieldNames.FleetGroup).Trim().Split(' ')[0];
            Page.SearchAndSelectValueAfterOpen(FieldNames.FleetGroup, fleetGroup);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetGroup));

            Page.OpenDropDown(FieldNames.DealerGroup);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerGroup));

            Page.OpenDropDown(FieldNames.DealerGroup);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerGroup));

            Page.SelectValueFirstRow(FieldNames.DealerGroup);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerGroup));

            string dealerGroup = Page.GetValueDropDown(FieldNames.DealerGroup).Trim().Split(' ')[0];
            Page.SearchAndSelectValueAfterOpen(FieldNames.DealerGroup, dealerGroup);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DealerGroup));

            Page.OpenMultiSelectDropDown(FieldNames.StatementType);
            Page.ClickFieldLabel(FieldNames.StatementType);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));
            
            Page.OpenMultiSelectDropDown(FieldNames.StatementType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));
            
            Page.SelectValueMultiSelectFirstRow(FieldNames.StatementType);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.StatementType);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));

            Page.SelectValueMultiSelectDropDown(FieldNames.StatementType, TableHeaders.StatementType, "Master Invoice");
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.StatementType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.StatementType));
        }
    }
}
