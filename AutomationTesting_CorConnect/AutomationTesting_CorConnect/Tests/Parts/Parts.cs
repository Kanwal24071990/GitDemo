using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.Parts;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.Parts;
using AutomationTesting_CorConnect.Utils.PartsCrossReference;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.Parts
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Parts")]
    internal class Parts : DriverBuilderClass
    {
        PartsPage Page;
        string partNumber = null;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.Parts);
            Page = new PartsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20258" })]
        public void TC_20258(string UserType)
        {
            Assert.Multiple(() =>
            {
                partNumber = CommonUtils.RandomString(6);
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.Parts), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.Parts).ForEach(x=>{ Assert.Fail(x); });

                Page.PopulateGrid(partNumber);
                var errorMsgs = Page.CreateNewDecentralizedPart(partNumber, null);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }

            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20259" })]
        public void TC_20259(string UserType)
        {
            LoggingHelper.LogMessage("Creating decentralized part");
            this.partNumber = CommonUtils.RandomString(6);
            Page.PopulateGrid(partNumber);
            var errorMsgs = Page.CreateNewDecentralizedPart(partNumber, null);
            if (errorMsgs.Count > 0)
            {
                throw new System.Exception($"Failed to create PartNumber [{partNumber}]");
            }

            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage("Editing the newly created decentralized part");
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.Parts), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.Parts).ForEach(x=>{ Assert.Fail(x); });

                var errorMsgs = Page.EditDecentralizedPart(partNumber);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }

            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20260" })]
        public void TC_20260(string UserType)
        {
            LoggingHelper.LogMessage("Creating decentralized part");
            this.partNumber = CommonUtils.RandomString(6);
            Page.PopulateGrid(partNumber);
            var errorMsgs = Page.CreateNewDecentralizedPart(partNumber, null);
            if (errorMsgs.Count > 0)
            {
                throw new System.Exception($"Failed to create PartNumber [{partNumber}]");
            }

            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage("Deleting the newly created decentralized part");
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.Parts), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                var errorMsgs = Page.DeleteDecentralizedPart(partNumber);

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }

            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20256" })]
        public void TC_20256(string UserType)
        {
            this.partNumber = CommonUtils.RandomString(6);
            Page.PopulateGrid(partNumber);
            Page.CreateNewPart(partNumber);

            var errorMsgs = Page.VerifyEditFields(Pages.Parts, ButtonsAndMessages.Edit);

            foreach (var errormsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errormsg));
            }

            Page.ClickEdit();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsFieldNotEmpty(FieldNames.PartNumber, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.FieldEmpty, FieldNames.PartNumber));
                Assert.IsTrue(Page.IsSpanNotEmpty(FieldNames.CompressedPartNumber), GetErrorMessage(ErrorMessages.FieldEmpty, FieldNames.CompressedPartNumber));
                Assert.IsTrue(Page.IsFieldNotEmpty(FieldNames.LongDescription, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.FieldEmpty, FieldNames.LongDescription));
                Assert.IsTrue(Page.IsFieldNotEmpty(FieldNames.UOM, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.FieldEmpty, FieldNames.UOM));
                Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.Active), GetErrorMessage(ErrorMessages.CheckBoxUnchecked, FieldNames.Active));
                Page.UpdateEditGrid();
                Assert.AreEqual(Page.GetEditMsg(), "The record has been updated successfully.\r\nPlease use Close button to exit from update form.");

            });

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20257" })]
        public void TC_20257(string UserType)
        {
            Page.PopulateGrid("test");
            partNumber = CommonUtils.RandomString(6);

            var errormsgs = Page.CreateNewPart(partNumber);

            foreach (var errormsg in errormsgs)
            {
                Assert.Fail(GetErrorMessage(errormsg));
            }

            Page.PopulateGrid(partNumber, false);
            var alertMsg = Page.DeleteEditField();
            Page.WaitForLoadingMessage();
            Assert.AreEqual(alertMsg, "Are you sure you want to delete this record?");
            Page.PopulateGrid(partNumber, false);

            Assert.AreEqual(Page.GetFirstRowData(TableHeaders.Active), "False");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22242" })]
        public void TC_22242(string UserType)
        {
            var companyName = PartsCrossReferenceUtil.GetCompanyNameCode();

            Page.OpenDropDown(FieldNames.PriceType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PriceType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PriceType));
            Page.OpenDropDown(FieldNames.PriceType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PriceType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PriceType));
            Page.SelectValueFirstRow(FieldNames.PriceType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PriceType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PriceType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.PriceType, "Centralized");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.PriceType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PriceType));

            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.OpenDropDown(FieldNames.CompanyName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SelectValueFirstRow(FieldNames.CompanyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));
            Page.SearchAndSelectValueAfterOpen(FieldNames.CompanyName, companyName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.CompanyName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.CompanyName));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23404" })]
        public void TC_23404(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.Parts), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.Parts).ForEach(x=>{ Assert.Fail(x); });
                Assert.IsTrue(Page.VerifyValueDropDown(FieldNames.PriceType, "All", "Centralized", "Decentralized"), $"{FieldNames.PriceType} DD: " + ErrorMessages.ListElementsMissing);
              
                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                string searchPartNumber = PartsUtil.GetPartNumber();
                Page.PopulateGrid(searchPartNumber);
                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                   
                    if (!Page.IsNestedGridOpen())
                    {
                        errorMsgs.Add("Level indicator not visible or not working");
                    }
                    else
                    {
                        errorMsgs.AddRange(Page.ValidateNestedGridTabs(TableHeaders.PartAttribute, TableHeaders.PriceByParts));
                        errorMsgs.AddRange(Page.ValidateNestedTableHeaders(TableHeaders.Commands, TableHeaders.Attribute, TableHeaders.Value, TableHeaders.Country));
                        errorMsgs.AddRange(Page.ValidateNestedTableDetails(true, true));
                        Assert.IsTrue(Page.IsNestedGridClosed(), ErrorMessages.NestedGridNotClosing);
                    }
                }

                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });
                string partNumber_ = Page.GetFirstRowData(TableHeaders.PartNumber);
                string longDescription = Page.GetFirstRowData(TableHeaders.LongDescription);
                Page.FilterTable(TableHeaders.PartNumber, partNumber_);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.LongDescription, longDescription), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.LongDescription, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.PartNumber, partNumber_);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.LongDescription, longDescription), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.LongDescription, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
            });
        }

        [TearDown]
        public void TearDown()
        {
            if (!string.IsNullOrEmpty(partNumber))
            {
                Page.DeletePart(partNumber, true);
            }

        }
    }
}
