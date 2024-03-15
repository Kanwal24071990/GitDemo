using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ManageCustomFields;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AutomationTesting_CorConnect.Tests.ManageCustomFields
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Custom Fields")]
    internal class ManageCustomFields : DriverBuilderClass
    {
        ManageCustomFieldsPage Page;
        ManageCustomFieldsAspx aspxPage;
        string fieldLabel = string.Empty;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManageCustomFields);
            Page = new ManageCustomFieldsPage(driver);
            aspxPage = new ManageCustomFieldsAspx(driver);
        }

        [Order(1)]
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21061" })]
        public void TC_21061(string UserType)
        {
            Page.ButtonClick(ButtonsAndMessages.AddNew);
            fieldLabel = CommonUtils.RandomString(10);
            Page.SwitchToPopUp();
            aspxPage.EnterText(FieldNames.Label, fieldLabel);
            aspxPage.EnterTextAfterClear(FieldNames.Description, fieldLabel + " Description.");
            aspxPage.SelectValueFirstRow(FieldNames.EntityType);
            aspxPage.WaitForAnyElementLocatedBy(FieldNames.DataType);
            try
            {
                aspxPage.SelectValueTableDropDown(FieldNames.DataType, "Text");
            }
            catch (StaleElementReferenceException)
            {
                aspxPage.SelectValueTableDropDown(FieldNames.DataType, "Text");
            }
            aspxPage.WaitForAnyElementLocatedBy(FieldNames.MaxLength);
            Thread.Sleep(TimeSpan.FromSeconds(4));
            aspxPage.SimpleSelectOptionByIndex(FieldNames.VisibleOn_Available, 1);
            aspxPage.ButtonClick(CommonUtils.HtmlEncode(ButtonsAndMessages.SymbolGreaterThan));
            aspxPage.Click(FieldNames.Active);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            aspxPage.WaitForLoadingIcon();
            Assert.IsTrue(aspxPage.IsTextVisible(ButtonsAndMessages.CustomFieldAddedSuccessfully), ErrorMessages.CustomFieldAddFailed);
            aspxPage.ButtonClick(ButtonsAndMessages.Cancel);
            Page.SwitchToMainWindow();
            Assert.AreEqual(1, Page.GetWindowsCount(), ErrorMessages.PopupWinowNotClosed);
        }

        [Order(2)]
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21062" })]
        public void TC_21062(string UserType)
        {
            Page.PopulateGrid(fieldLabel);
            fieldLabel = CommonUtils.RandomString(10);
            Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.NoDataOnGrid);
            Page.ClickHyperLinkOnGrid(TableHeaders.Label);
            Page.SwitchToPopUp();
            aspxPage.EnterTextAfterClear(FieldNames.Label, fieldLabel);
            aspxPage.ButtonClick(ButtonsAndMessages.Update);
            Assert.IsTrue(aspxPage.IsTextVisible(ButtonsAndMessages.CustomFieldUpdatedSuccessfully, true), ErrorMessages.CustomFieldUpdateFailed);
            aspxPage.ButtonClick(ButtonsAndMessages.Cancel);
            Page.SwitchToMainWindow();
            Page.PopulateGrid(fieldLabel);
            Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.NoDataOnGrid);
        }

        [Order(3)]
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21063" })]
        public void TC_21063(string UserType)
        {
            Page.PopulateGrid(fieldLabel);
            Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.NoDataOnGrid);
            Assert.AreEqual("Yes", Page.GetFirstRowData(TableHeaders.Active), ErrorMessages.CustomFieldAlreadyDisabled);
            Page.ClickHyperLinkOnGrid(TableHeaders.Label);
            Page.SwitchToPopUp();
            if (aspxPage.IsCheckBoxChecked(FieldNames.Active))
            {
                aspxPage.Click(FieldNames.Active);
            }
            aspxPage.ButtonClick(ButtonsAndMessages.Update);
            Assert.IsTrue(aspxPage.IsTextVisible(ButtonsAndMessages.CustomFieldUpdatedSuccessfully, true), ErrorMessages.CustomFieldUpdateFailed);
            aspxPage.ButtonClick(ButtonsAndMessages.Cancel);
            Page.SwitchToMainWindow();
            Page.PopulateGrid(fieldLabel);
            Assert.AreEqual("No", Page.GetFirstRowData(TableHeaders.Active), ErrorMessages.CustomFieldNotDisabled);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24764" })]
        public void TC_24764(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.ManageCustomFields, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.AddNew), GetErrorMessage(ErrorMessages.ButtonMissing));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.ManageCustomFields).ForEach(x => { Assert.Fail(x); });
                Assert.AreEqual(string.Empty, Page.GetValue(FieldNames.Label));
                Assert.AreEqual("All", Page.GetValueDropDown(FieldNames.Active));

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
                    string progInvNum = Page.GetFirstRowData(TableHeaders.Label);
                    Page.FilterTable(TableHeaders.Label, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Label, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Description, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.Label, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Label, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Description, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                    Page.ClickHyperLinkOnGrid(TableHeaders.Label);
                    Assert.AreEqual(progInvNum, aspxPage.GetValue(FieldNames.Label));
                    aspxPage.ClosePopupWindow();
                    Page.SwitchToMainWindow();
                    Page.ButtonClick(ButtonsAndMessages.AddNew);
                    Page.SwitchToPopUp();
                    Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.Label));
                    aspxPage.ClosePopupWindow();
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