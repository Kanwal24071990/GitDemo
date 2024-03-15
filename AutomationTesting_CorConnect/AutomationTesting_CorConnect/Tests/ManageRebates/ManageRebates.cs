using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ManageRebates;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.ManageRebates
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Rebates")]
    internal class ManageRebates : DriverBuilderClass
    {

        ManageRebatesPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManageRebates);
            Page = new ManageRebatesPage(driver);

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22214" })]
        public void TC_22214(string UserType)
        {
            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.OpenDropDown(FieldNames.LoadBookmark);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.SelectLoadBookmarkFirstRow();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            Page.ResetFields();
            Page.SearchAndSelectValueAfterOpen(FieldNames.LoadBookmark, "automation");
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));

            Page.OpenDropDown(FieldNames.ContractName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ContractName));
            Page.OpenDropDown(FieldNames.ContractName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ContractName));
            Page.SelectValueFirstRow(FieldNames.ContractName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ContractName));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ContractName, "TestContract01");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ContractName));

            Page.OpenDropDown(FieldNames.ParentContractName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ParentContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ParentContractName));
            Page.OpenDropDown(FieldNames.ParentContractName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ParentContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ParentContractName));
            Page.SelectValueFirstRow(FieldNames.ParentContractName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ParentContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ParentContractName));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentContractName, "Parent - TestContract01-22");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ParentContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ParentContractName));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22215" })]
        public void TC_22215(string UserType)
        {
            Assert.Multiple(() =>
            {
                Page.OpenMultiSelectDropDown(FieldNames.Currency);
                Page.ClickPageTitle();
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
                Page.OpenMultiSelectDropDown(FieldNames.Currency);
                Page.ScrollDiv();
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
                Page.SelectFirstRowMultiSelectDropDown(FieldNames.Currency, TableHeaders.Currency, "USD");
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
                Page.ClearSelectionMultiSelectDropDown(FieldNames.Currency);
                Page.SelectFirstRowMultiSelectDropDown(FieldNames.Currency);
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
                Page.ClearSelectionMultiSelectDropDown(FieldNames.Currency);
                Page.SelectAllRowsMultiSelectDropDown(FieldNames.Currency);
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
                Page.ClearSelectionMultiSelectDropDown(FieldNames.Currency);
                Page.SelectFirstRowMultiSelectDropDown(FieldNames.Currency);
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));

                Page.OpenMultiSelectDropDown(FieldNames.Status);
                Page.ClickPageTitle();
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
                Page.OpenMultiSelectDropDown(FieldNames.Status);
                Page.ScrollDiv();
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
                Page.SelectFirstRowMultiSelectDropDown(FieldNames.Status, TableHeaders.Status, "Active");
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
                Page.ClearSelectionMultiSelectDropDown(FieldNames.Status);
                Page.SelectFirstRowMultiSelectDropDown(FieldNames.Status);
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
                Page.ClearSelectionMultiSelectDropDown(FieldNames.Status);
                Page.SelectAllRowsMultiSelectDropDown(FieldNames.Status);
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
                Page.ClearSelectionMultiSelectDropDown(FieldNames.Status);
                Page.SelectFirstRowMultiSelectDropDown(FieldNames.Status);
                Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));

            });
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24612" })]
        public void TC_24612(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ManageRebates), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            if (Page.IsDataExistsInDropdown(FieldNames.LoadBookmark))
            {
                Page.OpenDropDown(FieldNames.LoadBookmark);
                Page.ClickPageTitle();
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LoadBookmark), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LoadBookmark));
            }
            Page.OpenDropDown(FieldNames.ContractName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ContractName));
            Page.OpenDropDown(FieldNames.ParentContractName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.ParentContractName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ParentContractName));
            Page.OpenMultiSelectDropDown(FieldNames.Currency);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            Page.OpenMultiSelectDropDown(FieldNames.Status);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid();

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string contractName = Page.GetFirstRowData(TableHeaders.ContractName);

                Page.SetFilterCreiteria(TableHeaders.ContractName, FilterCriteria.Equals);

                Page.FilterTable(TableHeaders.ContractName, contractName);
                Assert.IsTrue(Page.IsInputVisible(ButtonsAndMessages.AddNewContract));
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.AddNewContract));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                Page.ClickHyperLinkOnGrid(FieldNames.ContractName);
                Page.ClosePopupWindow();
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
