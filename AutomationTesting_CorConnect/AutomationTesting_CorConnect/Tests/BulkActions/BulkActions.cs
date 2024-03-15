using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.BulkActions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.BulkActions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutomationTesting_CorConnect.Tests.BulkActions
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Bulk Actions")]
    internal class BulkActions : DriverBuilderClass
    {
        BulkActionsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.BulkActions);
            Page = new BulkActionsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17282" })]
        public void TC_17282(string UserType)
        {
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.Action), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.IsTrue(Page.VerifyValue(FieldNames.Action, "Resend Invoices", "Update Credit", "Invoice Update", "Dispute Creation"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            });
        }


        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21239" })]
        public void TC_21239(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//fileUploadOnUpdateCredit.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile1), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile2), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields1), "Account Code");
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields2), "Credit Limit");
            });
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(Page.GetText(FieldNames.FieldOnFile1MandatoryMessage), ButtonsAndMessages.PleaseSelectAValue);
                Assert.AreEqual(Page.GetText(FieldNames.FieldOnFile2MandatoryMessage), ButtonsAndMessages.PleaseSelectAValue);
                Assert.AreEqual(Page.VerifyDropDownIsDisabled(FieldNames.CorConnectFields1), "true");
                Assert.AreEqual(Page.VerifyDropDownIsDisabled(FieldNames.CorConnectFields2), "true");
            });

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20275" })]
        public void TC_20275(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);

            var imagesrc = Page.GetSrc(FieldNames.DownloadDisputeTemplate, true);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(imagesrc.EndsWith("/download.png"), GetErrorMessage(ErrorMessages.SrcMisMatch));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FileName), GetErrorMessage(ErrorMessages.ElementNotPresent));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19813" })]
        public void TC_19813(string UserType)
        {
            Page.SelectAction(FieldNames.ResendInvoices);
            Page.SelectDistributionMethod("Email");
            Page.EnterBatchSize("10");
            Page.EnterEmailAddress("test1@corcentic.com;test2@corcentric.com");
            Page.UploadFileInFileName("UploadFiles//EmailSubjectGroupingMaxEmail.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Email Subject");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.EmailSubjectMoreThan300Chars);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19814" })]
        public void TC_19814(string UserType)
        {
            Page.SelectAction(FieldNames.ResendInvoices);
            Page.SelectDistributionMethod("Email");
            Page.EnterBatchSize("10");
            Page.EnterEmailAddress("test1@corcentic.com;test2@corcentric.com");
            Page.UploadFileInFileName("UploadFiles//EmailSubjectGroupingEmptySubject.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Email Subject");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.EmailSubjectEmpty);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19835" })]
        public void TC_19835(string UserType)
        {
            Page.SelectAction(FieldNames.ResendInvoices);
            Page.SelectDistributionMethod("Email");
            Page.EnterBatchSize("10");
            Page.EnterEmailAddress("test1@corcentic.com;test2@corcentric.com");
            Page.UploadFileInFileName("UploadFiles//EmailSubjectGroupingAllErrors.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Email Subject");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.AllErrors);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19815" })]
        public void TC_19815(string UserType)
        {
            Page.SelectAction(FieldNames.ResendInvoices);
            Page.SelectDistributionMethod("Email");
            Page.EnterBatchSize("10");
            Page.EnterEmailAddress("test1@corcentic.com;test2@corcentric.com");
            Page.UploadFileInFileName("UploadFiles//EmailSubjectGroupingEmptyInvoice.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Email Subject");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.InvoiceNumberEmpty);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19816" })]
        public void TC_19816(string UserType)
        {
            Page.SelectAction(FieldNames.ResendInvoices);
            Page.SelectDistributionMethod("Email");
            Page.EnterBatchSize("10");
            Page.EnterEmailAddress("test1@corcentic.com;test2@corcentric.com");
            Page.UploadFileInFileName("UploadFiles//EmailSubjectGroupingInvalidInvoice.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Email Subject");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.InvalidInvoiceNumber);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20083" })]
        public void TC_20083(string UserType)
        {
            Page.SelectAction(FieldNames.ResendInvoices);
            Page.SelectDistributionMethod("Email");
            Page.EnterBatchSize("10");
            Page.EnterEmailAddress("test1@corcentic.com;test2@corcentric.com");
            Page.UploadFileInFileName("UploadFiles//EmailSubjectGroupingMaxInvoice.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Email Subject");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.MaxInvoiceNumber);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19877" })]
        public void TC_19877(string UserType)
        {
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsAnchorVisible(ButtonsAndMessages.BrowseWithElipsis));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Upload));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Submit));
                Assert.IsTrue(Page.IsElementVisible(FieldNames.FileName));
                Page.UploadFileInFileName("UploadFiles//InvoiceUpdateSampleFile.xlsx");
                Page.ButtonClick(ButtonsAndMessages.Upload);
                Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
                Page.AcceptAlertMessage();
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile1), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile2), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields1), "Program Invoice Number");
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields2), "PO Number");
                Page.ButtonClick(ButtonsAndMessages.Submit);
                Assert.AreEqual(Page.GetText(FieldNames.FieldOnFile1MandatoryMessage), ButtonsAndMessages.PleaseSelectAValue);
                Assert.AreEqual(Page.GetText(FieldNames.FieldOnFile2MandatoryMessage), ButtonsAndMessages.PleaseSelectAValue);
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19882" })]
        public void TC_19882(string UserType)
        {
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsAnchorVisible(ButtonsAndMessages.BrowseWithElipsis));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Upload));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Submit));
                Assert.IsTrue(Page.IsElementVisible(FieldNames.FileName));
                Page.UploadFileInFileName("UploadFiles//InvoiceUpdateSampleFile.xlsx");
                Page.ButtonClick(ButtonsAndMessages.Upload);
                Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
                Page.AcceptAlertMessage();
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile1), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile2), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields1), "Program Invoice Number");
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields2), "PO Number");
                Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile1, "ColumnName1", "F2", "ColumnName2"), GetErrorMessage(ErrorMessages.ListElementsMissing));
                Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile2, "ColumnName1", "F2", "ColumnName2"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19878" })]
        public void TC_19878(string UserType)
        {
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsAnchorVisible(ButtonsAndMessages.BrowseWithElipsis));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Upload));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Submit));
                Assert.IsTrue(Page.IsElementVisible(FieldNames.FileName));
                Page.UploadFileInFileName("UploadFiles//SampleExcel.xlsx");
                Page.ButtonClick(ButtonsAndMessages.Upload);
                Assert.AreEqual(Page.GetAlertMessage(), ButtonsAndMessages.UploadedExcelFileIsEmpty);
                Page.AcceptAlertMessage();
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19892" })]
        public void TC_19892(string UserType)
        {
            var rowCountFromTableBulkActionsMaster = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetail = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Page.UploadFileInFileName("UploadFiles//BulkActionsDuplicateInvoiceNumber.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "PO Number Mapping");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DuplicateInvoiceNumbers);
            var rowCountFromTableBulkActionsMasterAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetailAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Assert.AreEqual(rowCountFromTableBulkActionsDetail, rowCountFromTableBulkActionsDetailAfter, GetErrorMessage(ErrorMessages.DataMisMatch));
            Assert.AreEqual(rowCountFromTableBulkActionsMaster, rowCountFromTableBulkActionsMasterAfter, GetErrorMessage(ErrorMessages.DataMisMatch));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19889" })]
        public void TC_19889(string UserType)
        {
            var rowCountFromTableBulkActionsMaster = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetail = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Page.UploadFileInFileName("UploadFiles//BulkActionsMaxPO.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "PO Number Mapping");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.PONumbersMaxLimit);
            var rowCountFromTableBulkActionsMasterAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetailAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Assert.AreEqual(rowCountFromTableBulkActionsDetail, rowCountFromTableBulkActionsDetailAfter, GetErrorMessage(ErrorMessages.DataMisMatch));
            Assert.AreEqual(rowCountFromTableBulkActionsMaster, rowCountFromTableBulkActionsMasterAfter, GetErrorMessage(ErrorMessages.DataMisMatch));

        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19887" })]
        public void TC_19887(string UserType)
        {
            var rowCountFromTableBulkActionsMaster = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetail = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Page.UploadFileInFileName("UploadFiles//BulkActionsEmptyInvoicePO.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "PO Number Mapping");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.InvoiceNumberEmpty);
            var rowCountFromTableBulkActionsMasterAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetailAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Assert.AreEqual(rowCountFromTableBulkActionsDetail, rowCountFromTableBulkActionsDetailAfter, GetErrorMessage(ErrorMessages.DataMisMatch));
            Assert.AreEqual(rowCountFromTableBulkActionsMaster, rowCountFromTableBulkActionsMasterAfter, GetErrorMessage(ErrorMessages.DataMisMatch));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19890" })]
        public void TC_19890(string UserType)
        {
            var rowCountFromTableBulkActionsMaster = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetail = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Page.UploadFileInFileName("UploadFiles//BulkActionsMultipleErrorsPO.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "PO Number Mapping");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.BulkActionsMultipleErrors);
            var rowCountFromTableBulkActionsMasterAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetailAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Assert.AreEqual(rowCountFromTableBulkActionsDetail, rowCountFromTableBulkActionsDetailAfter, GetErrorMessage(ErrorMessages.DataMisMatch));
            Assert.AreEqual(rowCountFromTableBulkActionsMaster, rowCountFromTableBulkActionsMasterAfter, GetErrorMessage(ErrorMessages.DataMisMatch));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19886" })]
        public void TC_19886(string UserType)
        {
            var rowCountFromTableBulkActionsMaster = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetail = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Page.UploadFileInFileName("UploadFiles//BulkActionsInvalidInvoicePO.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "PO Number Mapping");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.InvalidInvoiceNumberPO);
            var rowCountFromTableBulkActionsMasterAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            var rowCountFromTableBulkActionsDetailAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsdetail_tb");
            Assert.AreEqual(rowCountFromTableBulkActionsDetail, rowCountFromTableBulkActionsDetailAfter, GetErrorMessage(ErrorMessages.DataMisMatch));
            Assert.AreEqual(rowCountFromTableBulkActionsMaster, rowCountFromTableBulkActionsMasterAfter, GetErrorMessage(ErrorMessages.DataMisMatch));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19883" })]
        public void TC_19883(string UserType)
        {
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Page.UploadFileInFileName("UploadFiles//SamplePDF.pdf");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            string errorText = Page.GetFileUploadErrorMsg(ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Assert.AreEqual(errorText, ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Page.UploadFileInFileName("UploadFiles//SampleWordDoc.docx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            errorText = Page.GetFileUploadErrorMsg(ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Assert.AreEqual(errorText, ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Page.UploadFileInFileName("UploadFiles//InvoiceUpdateSampleFile.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19880" })]
        public void TC_19880(string UserType)
        {

            Page.SelectAction(FieldNames.InvoiceUpdate);
            Page.UploadFileInFileName("UploadFiles//BulkActionsEmptyExcel.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.UploadedExcelFileIsEmpty);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19884" })]
        public void TC_19884(string UserType)
        {
            Page.SelectAction(FieldNames.InvoiceUpdate);
            Page.UploadFileInFileName("UploadFiles//BulkActionsHeadersOnRowTwo.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile1, "Invoice Number", "PO Number Mapping"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile2, "Invoice Number", "PO Number Mapping"), GetErrorMessage(ErrorMessages.ListElementsMissing));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "19812" })]
        public void TC_19812(string UserType)
        {
            Page.SelectAction(FieldNames.ResendInvoices);
            Page.SelectDistributionMethod("Email");
            Assert.AreEqual(Page.GetValue(FieldNames.BatchSize), "0");
            Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.AttachmentType).Trim(), "PDF");
            Page.UploadFileInFileName("UploadFiles//fileUploadOnUpdateCredit.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(Page.GetText(FieldNames.FieldOnFile1MandatoryMessage), ButtonsAndMessages.PleaseSelectAValue);
                Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile1, "Invoice Number", "Email Subject"), GetErrorMessage(ErrorMessages.ListElementsMissing));
                Assert.AreEqual(Page.VerifyDropDownIsDisabled(FieldNames.CorConnectFields1), "true");
                Assert.AreEqual(Page.VerifyDropDownIsDisabled(FieldNames.CorConnectFields2), "true");
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields1), "Program Invoice Number");
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields2), "Email Subject");
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17365" })]
        public void TC_17365(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCredit10kInvalidValues.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields1), "Account Code");
            Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields2), "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.UpdateCreditInvalid10kRows);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17359" })]
        public void TC_17359(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//fileUploadOnUpdateCredit.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);

            Page.UploadFileInFileName("UploadFiles//ExcelFile5MB.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlert(out string fileUploadMsg5MBFile);
            Assert.AreEqual(fileUploadMsg5MBFile.Trim(), ButtonsAndMessages.FileUploadSuccessfully);

            Page.UploadFileInFileName("UploadFiles//ExcelFileGreaterThan5MB.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            string errorText = Page.GetFileUploadErrorMsg(ButtonsAndMessages.MaxFileSize);
            Assert.AreEqual(errorText, ButtonsAndMessages.MaxFileSize);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17292" })]
        public void TC_17292(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditInactiveAccounts.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.InactiveAccounts);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17303" })]
        public void TC_17303(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditBillingAndDealerLocations.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.CreditUpdateOnlyForBillingLocations1);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17302" })]
        public void TC_17302(string UserType)
        {
            Page.SelectAction("Update Credit");
            Page.UploadFileInFileName("UploadFiles//UpdateCreditBillingAndDealerLocations2.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.CreditUpdateOnlyForBillingLocations2);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17301" })]
        public void TC_17301(string UserType)
        {
            Page.SelectAction("Update Credit");
            Page.UploadFileInFileName("UploadFiles//UpdateCreditBillingAndDealerLocations3.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.CreditUpdateOnlyForBillingLocations3);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17304" })]
        public void TC_17304(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditInvalidCreditLimit.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.InvalidCreditLimit);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17351" })]
        public void TC_17351(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditDuplicateAccount.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.PleaseRemoveDuplicateAccounts);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17360" })]
        public void TC_17360(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditHeaderLength.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile1, "Corcentric Account Number Corcentric Account Number Corcentricc", "Corcentric Account Number Corcentric Account Number Corcentriccc", "Corcentric Account Number Corcentric Account Number Corcentriccc"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile2, "Corcentric Account Number Corcentric Account Number Corcentricc", "Corcentric Account Number Corcentric Account Number Corcentriccc", "Corcentric Account Number Corcentric Account Number Corcentriccc"), GetErrorMessage(ErrorMessages.ListElementsMissing));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17356" })]
        public void TC_17356(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditCreditLimitDecimalPlaces.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.CreditLimitUpdated1Accounts);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17290" })]
        public void TC_17290(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//BulkActionsHeadersOnRowTwo.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile1, "Invoice Number", "PO Number Mapping"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile2, "Invoice Number", "PO Number Mapping"), GetErrorMessage(ErrorMessages.ListElementsMissing));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17289" })]
        public void TC_17289(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//SamplePDF.pdf");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            string errorText = Page.GetFileUploadErrorMsg(ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Assert.AreEqual(errorText, ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Page.UploadFileInFileName("UploadFiles//SampleWordDoc.docx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            errorText = Page.GetFileUploadErrorMsg(ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Assert.AreEqual(errorText, ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Page.UploadFileInFileName("UploadFiles//InvoiceUpdateSampleFile.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17286" })]
        public void TC_17286(string UserType)
        {
            var rowCountFromTableBulkActionsMaster = BulkActionsUtil.GetRowCountFromTable("transactionCreditAudit_tb");
            var rowCountFromTableBulkActionsDetail = BulkActionsUtil.GetRowCountFromTable("auditTrail_tb");
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//BulkActionsEmptyExcel.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.UploadedExcelFileIsEmpty);
            var rowCountFromTableBulkActionsMasterAfter = BulkActionsUtil.GetRowCountFromTable("transactionCreditAudit_tb");
            var rowCountFromTableBulkActionsDetailAfter = BulkActionsUtil.GetRowCountFromTable("auditTrail_tb");
            Assert.AreEqual(rowCountFromTableBulkActionsDetail, rowCountFromTableBulkActionsDetailAfter, GetErrorMessage(ErrorMessages.DataMisMatch));
            Assert.AreEqual(rowCountFromTableBulkActionsMaster, rowCountFromTableBulkActionsMasterAfter, GetErrorMessage(ErrorMessages.DataMisMatch));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17288" })]
        public void TC_17288(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//InvoiceUpdateSampleFile.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Assert.AreEqual(Page.VerifyFileUpload(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.AcceptAlertMessage();

            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile1, "ColumnName1", "F2", "ColumnName2"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile2, "ColumnName1", "F2", "ColumnName2"), GetErrorMessage(ErrorMessages.ListElementsMissing));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17284" })]
        public void TC_17284(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditDuplicateHeaders.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile1, "Corcentric Account Number", "Credit Limit", "Corcentric Account Number1"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(Page.VerifyValue(FieldNames.FieldOnFile2, "Corcentric Account Number", "Credit Limit", "Corcentric Account Number1"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.AreEqual("Account Code", Page.GetValueOfDropDown(FieldNames.CorConnectFields1));
            Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields2), "Credit Limit");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.CreditLimitUpdated1Accounts);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17283" })]
        public void TC_17283(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditHeadersOnly.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Corcentric Account Number");
            Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Credit Limit");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.TheFileContainsNoDataForAccounts);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20279" })]
        public void TC_20279(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateEmptyTemplate.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationEmptyTemplate);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20282" })]
        public void TC_20282(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            string fileName = new DownloadHelper(driver).DownloadFile(applicationContext.downloadsDirectory, "xls", Page.GetButton());
            var file = applicationContext.downloadsDirectory + "\\" + fileName;
            var headers = Page.GetHeaderRowFromFile(file);
            List<string> headersList = new List<string>() { "rowNumber", "InvoiceNumber", "UserName", "Email", "Phone", "Reason", "Notes", "AdditionalInfo1", "AdditionalInfo2", "AdditionalInfo3" };
            Assert.IsTrue(headersList.All(items => headers.Contains(items)));
            Page.UploadFileInFileName("UploadFiles//DisputeCreateMandatoryColumns.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationMandatoryFieldsEmpty);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20288" })]
        public void TC_20288(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            string fileName = new DownloadHelper(driver).DownloadFile(applicationContext.downloadsDirectory, "xls", Page.GetButton());
            var file = applicationContext.downloadsDirectory + "\\" + fileName;
            var headers = Page.GetHeaderRowFromFile(file);
            List<string> headersList = new List<string>() { "rowNumber", "InvoiceNumber", "UserName", "Email", "Phone", "Reason", "Notes", "AdditionalInfo1", "AdditionalInfo2", "AdditionalInfo3" };
            Assert.IsTrue(headersList.All(items => headers.Contains(items)));
            Page.UploadFileInFileName("UploadFiles//DisputeCreateEmptyReason.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationMandatoryFieldsEmpty);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20289" })]
        public void TC_20289(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            string fileName = new DownloadHelper(driver).DownloadFile(applicationContext.downloadsDirectory, "xls", Page.GetButton());
            var file = applicationContext.downloadsDirectory + "\\" + fileName;
            var headers = Page.GetHeaderRowFromFile(file);
            List<string> headersList = new List<string>() { "rowNumber", "InvoiceNumber", "UserName", "Email", "Phone", "Reason", "Notes", "AdditionalInfo1", "AdditionalInfo2", "AdditionalInfo3" };
            Assert.IsTrue(headersList.All(items => headers.Contains(items)));
            Page.UploadFileInFileName("UploadFiles//DisputeCreateEmptyNotes.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationMandatoryFieldsEmpty);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20290" })]
        public void TC_20290(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            string fileName = new DownloadHelper(driver).DownloadFile(applicationContext.downloadsDirectory, "xls", Page.GetButton());
            var file = applicationContext.downloadsDirectory + "\\" + fileName;
            var headers = Page.GetHeaderRowFromFile(file);
            List<string> headersList = new List<string>() { "rowNumber", "InvoiceNumber", "UserName", "Email", "Phone", "Reason", "Notes", "AdditionalInfo1", "AdditionalInfo2", "AdditionalInfo3" };
            Assert.IsTrue(headersList.All(items => headers.Contains(items)));
            Page.UploadFileInFileName("UploadFiles//DisputeCreateDuplicateInvoice.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationDuplicateInvoiceNumbers);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20307" })]
        public void TC_20307(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//SamplePDF.pdf");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            string errorText = Page.GetFileUploadErrorMsg(ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
            Assert.AreEqual(errorText, ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20308" })]
        public void TC_20308(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateTemplateMoreThan5MB.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            string errorText = Page.GetFileUploadErrorMsg(ButtonsAndMessages.MaxFileSize);
            Assert.AreEqual(errorText, ButtonsAndMessages.MaxFileSize);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateTemplate5MB.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationSaved);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20309" })]
        public void TC_20309(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateTemplateReasonHeaderMissing.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationHeaderMissing);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20310" })]
        public void TC_20310(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateTemplateEmptyRows.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationMandatoryFieldsEmpty);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20900" })]
        public void TC_20900(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateTemplateNonMandatoryHeaderMissing.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationHeaderMissing);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20903" })]
        public void TC_20903(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateTemplateEmptyRowNum.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationSaved);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20904" })]
        public void TC_20904(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateTemplateInvalidRowNum.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationMandatoryFieldsEmpty);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20899" })]
        public void TC_20899(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateEmptyNotesHeaderMissing.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationHeaderMissing);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20280" })]
        public void TC_20280(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            Page.UploadFileInFileName("UploadFiles//DisputeCreateTemplateOnlyHeaders.xls");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptAlertMessage(out string msg);
            Assert.AreEqual(msg.Trim(), ButtonsAndMessages.DisputeCreationMandatoryFieldsEmpty);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22268" })]
        public void TC_22268(string UserType)
        {
            Page.OpenDropDown(FieldNames.Action);
            Page.ClickFieldLabel(FieldNames.Action);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Action), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Action));
            Page.SelectValueFirstRow(FieldNames.Action);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Action), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Action));
            Page.SelectAction(FieldNames.DisputeCreation);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Action), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Action));
            Page.SelectAction(FieldNames.ResendInvoices);
            Page.OpenDropDown(FieldNames.DistributionMethod);
            Page.ClickFieldLabel(FieldNames.DistributionMethod);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DistributionMethod), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DistributionMethod));
            Page.SelectValueFirstRow(FieldNames.DistributionMethod);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DistributionMethod), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DistributionMethod));
            Page.SelectDistributionMethod("Email");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DistributionMethod), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DistributionMethod));
            Page.OpenDropDown(FieldNames.AttachmentType);
            Page.ClickFieldLabel(FieldNames.AttachmentType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AttachmentType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AttachmentType));
            Page.SelectAttachmentType("PDF");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.AttachmentType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.AttachmentType));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20276" })]
        public void TC_20276(string UserType)
        {
            Page.SelectAction(FieldNames.DisputeCreation);
            string fileName = new DownloadHelper(driver).DownloadFile(applicationContext.downloadsDirectory, "xls", Page.GetButton());
            var file = applicationContext.downloadsDirectory + "\\" + fileName;
            var headers = Page.GetHeaderRowFromFile(file);
            List<string> headersList = new List<string>() { "rowNumber", "InvoiceNumber", "UserName", "Email", "Phone", "Reason", "Notes", "AdditionalInfo1", "AdditionalInfo2", "AdditionalInfo3" };
            Assert.IsTrue(headersList.All(items => headers.Contains(items)));
            var username = Page.GetUserNameFromFile(file);
            Assert.AreEqual("Support", username);
            var reasons = Page.GetReasonsFromTemplateFile(file);
            var reasonsFromDb = BulkActionsUtil.GetReasons();
            Assert.AreEqual(reasonsFromDb, reasons);
        }

        [Category(TestCategory.Smoke)]
        [Category(TestCategory.Premier)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22115" })]
        public void TC_22115(string UserType)
        {
            Page.SelectAction(FieldNames.UpdateCredit);
            Page.UploadFileInFileName("UploadFiles//UpdateCreditHeadersOnly.xlsx");
            Page.ButtonClick(ButtonsAndMessages.Upload);
            Page.AcceptAlertMessage(out string fileUploadMsg);
            Assert.AreEqual(fileUploadMsg.Trim(), ButtonsAndMessages.FileUploadSuccessfully);
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile1), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile2), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields1), "Account Code");
                Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields2), "Credit Limit");
            });
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(Page.GetText(FieldNames.FieldOnFile1MandatoryMessage), ButtonsAndMessages.PleaseSelectAValue);
                Assert.AreEqual(Page.GetText(FieldNames.FieldOnFile2MandatoryMessage), ButtonsAndMessages.PleaseSelectAValue);
                Assert.AreEqual(Page.VerifyDropDownIsDisabled(FieldNames.CorConnectFields1), "true");
                Assert.AreEqual(Page.VerifyDropDownIsDisabled(FieldNames.CorConnectFields2), "true");
            });
        }
    }
}
