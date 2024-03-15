using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.Sub_communityManagement;
using AutomationTesting_CorConnect.PageObjects.SubcommunityManagement;
using AutomationTesting_CorConnect.PageObjects.SubCommunityManagement;
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

namespace AutomationTesting_CorConnect.Tests.SubCommunityManagement
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Sub-community Management")]
    internal class SubcommunityManagement : DriverBuilderClass
    {
        SubcommunityManagementPage Page;

        private string subCommunityName = CommonUtils.RandomString(8);
        private string subCommunityCode = CommonUtils.RandomAlphabets(6);

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.Sub_communityManagement);
            Page = new SubcommunityManagementPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21322" })]
        public void TC_21322(string UserType)
        {
            CreateNewSubcommunityPage CreateNewSubCommunityPopUp = Page.OpenNewSubcommunityPage();
            CreateNewSubCommunityPopUp.PopulateSubCommunityFields(FieldNames.Fleet, subCommunityName, subCommunityCode, "", "");
            CreateNewSubCommunityPopUp.AssignLocationsByIndex(1);
            CreateNewSubCommunityPopUp.AssignLocationsByIndex(2);
            CreateNewSubCommunityPopUp.AssignLocationsByIndex(3);
            CreateNewSubCommunityPopUp.Click(ButtonsAndMessages.Save);
            CreateNewSubCommunityPopUp.CheckForText(ButtonsAndMessages.Recordsavedsuccessfully, true);
            CreateNewSubCommunityPopUp.ClosePopupWindow();
            CreateNewSubCommunityPopUp.SwitchToMainWindow();
            Page.PopulateGrid(subCommunityName);
            Assert.AreEqual(Page.GetRowCount(), 1);
            Assert.AreEqual(menu.RenameMenuField(FieldNames.Fleet), Page.GetFirstRowData(TableHeaders.Sub_communityType));
            Assert.AreEqual(subCommunityName, Page.GetFirstRowData(TableHeaders.Sub_communityName));
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21323" })]
        public void TC_21323(string UserType)
        {
            Page.PopulateGrid(subCommunityName);
            Assert.AreEqual(menu.RenameMenuField(FieldNames.Fleet), Page.GetFirstRowData(TableHeaders.Sub_communityType));
            Assert.AreEqual(subCommunityName, Page.GetFirstRowData(TableHeaders.Sub_communityName));
            Page.ClickHyperLinkOnGrid(FieldNames.Sub_communityName);
            string UpdatedsubCommunityName = CommonUtils.RandomString(8);
            string UpdatedsubCommunityCode = CommonUtils.RandomAlphabets(6);
            CreateNewSubcommunityPage CreateNewSubCommunityPopUp = Page.UpdateNewSubCommunityPage();
            CreateNewSubCommunityPopUp.UpdateSubCommunityFields(UpdatedsubCommunityName, UpdatedsubCommunityCode);
            CreateNewSubCommunityPopUp.CheckForText(ButtonsAndMessages.RecordUpdatedSuccessfully, true);
            CreateNewSubCommunityPopUp.ClosePopupWindow();
            CreateNewSubCommunityPopUp.SwitchToMainWindow();
            Page.PopulateGrid(UpdatedsubCommunityName);
            Assert.AreEqual(Page.GetRowCount(), 1);
            Assert.AreEqual(menu.RenameMenuField(FieldNames.Fleet), Page.GetFirstRowData(TableHeaders.Sub_communityType));
            Assert.AreEqual(UpdatedsubCommunityName, Page.GetFirstRowData(TableHeaders.Sub_communityName));
            Assert.AreEqual(UpdatedsubCommunityCode.ToUpper(), Page.GetFirstRowData(TableHeaders.Sub_communityCode));
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(3), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21320" })]
        public void TC_21320(string UserType)
        {
            string remitToName = CommonUtils.RandomAlphabets(8);
            string Address1 = CommonUtils.RandomString(14);
            subCommunityCode = CommonUtils.RandomAlphabets(6);
            CreateNewSubcommunityPage CreateNewSubCommunityPopUp = Page.OpenNewSubcommunityPage();
            CreateNewSubCommunityPopUp.PopulateSubCommunityFields(FieldNames.Dealer, subCommunityName, subCommunityCode, "", "");
            CreateNewSubCommunityPopUp.AssignLocationsByIndex(2);
            CreateNewSubCommunityPopUp.AssignLocationsByIndex(3);
            CreateNewSubCommunityPopUp.AssignLocationsByIndex(4);
            CreateNewSubCommunityPopUp.Click(ButtonsAndMessages.Save);
            CreateNewSubCommunityPopUp.CheckForText(ButtonsAndMessages.Recordsavedsuccessfully, true);
            AddRemittanceAddressPage CreateNewRemittanceAddress = CreateNewSubCommunityPopUp.OpenRemittanceAddressPage();
            CreateNewRemittanceAddress.PopulateRemitFields(remitToName);
            CreateNewRemittanceAddress.Click(ButtonsAndMessages.Save);
            CreateNewRemittanceAddress.CheckForText(ButtonsAndMessages.RecordHasBeenSavedSuccessfully, true);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22233" })]
        public void TC_22233(string UserType)
        {
            Page.OpenDropDown(FieldNames.SubCommunityType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SubCommunityType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SubCommunityType));

            Page.OpenDropDown(FieldNames.SubCommunityType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SubCommunityType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SubCommunityType));

            Page.SelectValueFirstRow(FieldNames.SubCommunityType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SubCommunityType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SubCommunityType));

            string subCommunityType = Page.GetValueDropDown(FieldNames.SubCommunityType).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.SubCommunityType, subCommunityType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.SubCommunityType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.SubCommunityType));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24766" })]
        public void TC_24766(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.Sub_communityManagement, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.CreateNewSub_community), GetErrorMessage(ErrorMessages.ButtonMissing));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.Sub_communityManagement).ForEach(x => { Assert.Fail(x); });
                Assert.AreEqual("All", Page.GetValueDropDown(FieldNames.SubCommunityType));

                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                Page.GridLoad();

                if (Page.IsAnyDataOnGrid())
                {
                    Page.ButtonClick(ButtonsAndMessages.Clear);
                    Assert.AreEqual(Page.GetAlertMessage(), ButtonsAndMessages.ConfirmResetAlert);
                    Page.DismissAlert();

                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                    string progInvNum = Page.GetFirstRowData(TableHeaders.Sub_communityName);
                    Page.FilterTable(TableHeaders.Sub_communityName, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Sub_communityName, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Sub_communityCode, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.Sub_communityName, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Sub_communityName, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Sub_communityCode, CommonUtils.RandomString(10));
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

            });
        }

    }
}
