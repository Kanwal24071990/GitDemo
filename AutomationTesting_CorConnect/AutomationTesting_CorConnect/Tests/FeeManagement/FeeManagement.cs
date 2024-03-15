using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FeeManagement;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.FeeManagement
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fee Management")]
    internal class FeeManagement : DriverBuilderClass
    {
        FeeManagementPage Page;
        FeeManagementAspx aspxPage;
        string feeDisplayName = string.Empty;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FeeManagement);
            Page = new FeeManagementPage(driver);
            aspxPage = new FeeManagementAspx(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test,Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21057" })]
        public void TC_21057(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.FeeManagement), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            Page.ButtonClick(ButtonsAndMessages.AddNewFee);
            Page.SwitchToPopUp();
            Assert.Multiple(() =>
            {
                var errorMsgs = aspxPage.VerifyFormFields();
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
            feeDisplayName = CommonUtils.RandomString(10);
            aspxPage.EnterTextAfterClear(FieldNames.FeeName, feeDisplayName);
            aspxPage.EnterTextAfterClear(FieldNames.DisplayName, feeDisplayName);
            aspxPage.EnterTextAfterClear(FieldNames.Description, feeDisplayName + " description.");
            aspxPage.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetCurrentDate());
            aspxPage.SelectValueTableDropDown(FieldNames.Currency, "All");
            aspxPage.SelectValueTableDropDown(FieldNames.InvoiceType, "All");
            aspxPage.SelectFirstRowMultiSelectDropDown(FieldNames.TransactionType, TableHeaders.TransactionType, "All");
            aspxPage.SelectValueTableDropDown(FieldNames.FeeValueType, "Amount");
            aspxPage.EnterTextAfterClear(FieldNames.FeeValue, "100.00");
            aspxPage.EnterTextAfterClear(FieldNames.MinInvoiceAmount, "0.00");
            aspxPage.EnterTextAfterClear(FieldNames.MaxInvoiceAmount, "0.00");
            Task t = Task.Run(() => aspxPage.WaitForStalenessOfElement(FieldNames.Assigned));
            aspxPage.SelectValueTableDropDown(FieldNames.SubCommunity, "All");
            t.Wait();
            t.Dispose();
            aspxPage.SimpleSelectOptionByIndex(FieldNames.Unassigned, 1);
            aspxPage.ButtonClick(CommonUtils.HtmlEncode(ButtonsAndMessages.SymbolGreaterThan));
            aspxPage.SearchAndSelectValue(FieldNames.FeesApplicableToTransactionsFrom, menu.RenameMenuField("All Dealer"));
            aspxPage.ButtonClick(ButtonsAndMessages.Save);
            Assert.IsTrue(aspxPage.CheckForText("Record saved successfully.", true), ErrorMessages.SaveOperationFailed);
            aspxPage.ButtonClick(ButtonsAndMessages.Cancel);
            Page.SwitchToMainWindow();
            Page.PopulateGrid(feeDisplayName);
            Assert.IsTrue(Page.IsAnyDataOnGrid(), ErrorMessages.NoDataOnGrid);
            Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, GetErrorMessage(ErrorMessages.FeeNotFound, feeDisplayName));
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21058" })]
        public void TC_21058(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.FeeManagement), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            Page.PopulateGrid(feeDisplayName);
            Assert.AreEqual(feeDisplayName, Page.GetFirstRowData(TableHeaders.DisplayName));
            Page.ClickHyperLinkOnGrid(TableHeaders.FeeName);
            Page.SwitchToPopUp();
            aspxPage.EnterTextAfterClear(FieldNames.Description, feeDisplayName + "Updated");
            aspxPage.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetCurrentDate());
            aspxPage.EnterTextAfterClear(FieldNames.DisplayName, feeDisplayName + "Updated");
            aspxPage.SelectValueTableDropDown(FieldNames.FeeValueType, "Percentage");
            aspxPage.EnterTextAfterClear(FieldNames.FeeValue, "120.00");
            aspxPage.ButtonClick(ButtonsAndMessages.Update);
            aspxPage.WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            aspxPage.WaitForElementToBeVisible("Update Note Dialog");
            aspxPage.EnterText("Update Note","This Note Has Been Automated");
            aspxPage.Click(ButtonsAndMessages.Continue);
            aspxPage.WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            Assert.IsTrue(aspxPage.CheckForText("Record saved successfully.", true), ErrorMessages.SaveOperationFailed);
            aspxPage.ButtonClick(ButtonsAndMessages.Cancel);
            Page.SwitchToMainWindow();
            Page.PopulateGrid(feeDisplayName);
            Assert.IsTrue(Page.IsAnyDataOnGrid(), ErrorMessages.NoDataOnGrid);
            Page.ClickHyperLinkOnGrid(TableHeaders.FeeName);
            Page.SwitchToPopUp();
            Assert.AreEqual(feeDisplayName + "Updated", aspxPage.GetValue(FieldNames.Description));
            Assert.AreEqual(CommonUtils.GetCurrentDate(), aspxPage.GetValue(FieldNames.EffectiveDate));
            Assert.AreEqual(feeDisplayName + "Updated", aspxPage.GetValue(FieldNames.DisplayName));
            Assert.AreEqual("Percentage", aspxPage.GetValueOfDropDown(FieldNames.FeeValueType));
            Assert.AreEqual("120.00000000", aspxPage.GetValue(FieldNames.FeeValue));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24763" })]
        public void TC_24763(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.FeeManagement, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.AddNewFee), GetErrorMessage(ErrorMessages.AddButtonMissing));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.FeeManagement).ForEach(x => { Assert.Fail(x); });
                Assert.AreEqual("All", Page.GetValueDropDown(FieldNames.CurrencyCode));
                Assert.AreEqual("All", Page.GetValueDropDown(FieldNames.SubCommunity));
                
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
                    string progInvNum = Page.GetFirstRowData(TableHeaders.DisplayName);
                    Page.FilterTable(TableHeaders.DisplayName, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DisplayName, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.FeeName, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.DisplayName, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DisplayName, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.FeeName, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                    string effectiveDate = Page.GetFirstRowData(TableHeaders.EffectiveDate);
                    Page.EnterTextAfterClear(FieldNames.EffectiveDate, effectiveDate);
                    Page.GridLoad();
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.EffectiveDate, effectiveDate), ErrorMessages.NoRowAfterFilter);

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
