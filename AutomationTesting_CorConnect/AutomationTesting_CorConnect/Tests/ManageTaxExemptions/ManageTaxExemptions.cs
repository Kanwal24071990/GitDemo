using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetLookup;
using AutomationTesting_CorConnect.PageObjects.ManageTaxExemptions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.ManageTaxExemptions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutomationTesting_CorConnect.Tests.ManageTaxExemptions
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Tax Exemptions")]
    internal class ManageTaxExemptions : DriverBuilderClass
    {
        ManageTaxExemptionsPage Page;
        FleetLookupPage fleetLookupPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.ManageTaxExemptions);
            Page = new ManageTaxExemptionsPage(driver);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17963" })]
        public void TC_17963(string editType, string fleetCode, int editTypeId)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Page.PopulateGrid(editType, fleetCode);

                List<string> errorMsgs = new List<string>();
                List<ManageTaxExemptionsObject> dbRecordList = ManageTaxExemptionsUtils.GetTaxDocsRecordsList(fleetCode, editTypeId);
                int rowCount = Page.GetRowCount(FieldNames.ClientTaxDocumentsTable, true);
                if (rowCount != dbRecordList.Count)
                {
                    errorMsgs.Add(ErrorMessages.GridRowCountMisMatch + $" for Table [{FieldNames.ClientTaxDocumentsTable}]");
                }

                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, dbRecordList.Select(x => x.CorcentricCode).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.UserName, dbRecordList.Select(x => x.UserName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.LegalName, dbRecordList.Select(x => x.LegalName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.ParentName, dbRecordList.Select(x => x.ParentName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.City, dbRecordList.Select(x => x.City).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.State, dbRecordList.Select(x => x.State).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.LocationType, dbRecordList.Select(x => x.LocationType).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.BillingLocation, dbRecordList.Select(x => x.BillingLocation).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.MasterLocation, dbRecordList.Select(x => x.MasterLocation).ToList()));

                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.UserName));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.LegalName));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.LocationType));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.BillingLocation));

                rowCount = Page.GetRowCount(FieldNames.AttachExistingDocumentsTable, false);
                var uniqueFilesNames = dbRecordList.GroupBy(x => x.DisplayFileName).Select(x => x.First().DisplayFileName).ToList();
                uniqueFilesNames.RemoveAll(x => string.IsNullOrWhiteSpace(x));
                if (uniqueFilesNames.Count > 0)
                {
                    dbRecordList = new List<ManageTaxExemptionsObject>();
                    foreach (var fileName in uniqueFilesNames)
                    {
                        dbRecordList.AddRange(ManageTaxExemptionsUtils.GetTaxDocsRecordsList(fleetCode, editTypeId, taxDoc: fileName));
                    }

                    if (rowCount != dbRecordList.Count)
                    {
                        errorMsgs.Add(ErrorMessages.GridRowCountMisMatch + $" for Table [{FieldNames.AttachExistingDocumentsTable}]");
                    }

                    if (rowCount > 0)
                    {
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.AccountCode, dbRecordList.Select(x => x.CorcentricCode).ToList()));
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.UserName, dbRecordList.Select(x => x.UserName).ToList()));
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.LegalName, dbRecordList.Select(x => x.LegalName).ToList()));
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.ParentName, dbRecordList.Select(x => x.ParentName).ToList()));
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.City, dbRecordList.Select(x => x.City).ToList()));
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.State, dbRecordList.Select(x => x.State).ToList()));
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.LocationType, dbRecordList.Select(x => x.LocationType).ToList()));
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.BillingLocation, dbRecordList.Select(x => x.BillingLocation).ToList()));
                        errorMsgs.AddRange(Page.ValidateSorting(FieldNames.AttachExistingDocumentsTable, TableHeaders.MasterLocation, dbRecordList.Select(x => x.MasterLocation).ToList()));

                        errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.AttachExistingDocumentsTable, TableHeaders.AccountCode, false));
                        errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.AttachExistingDocumentsTable, TableHeaders.UserName, false));
                        errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.AttachExistingDocumentsTable, TableHeaders.LegalName, false));
                        errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.AttachExistingDocumentsTable, TableHeaders.City, false));
                        errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.AttachExistingDocumentsTable, TableHeaders.LocationType, false));
                        errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.AttachExistingDocumentsTable, TableHeaders.BillingLocation, false));
                        errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.AttachExistingDocumentsTable, TableHeaders.MasterLocation, false));
                    }
                }

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20820" })]
        public void TC_20820(string editType, string fleetCode)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Page.PopulateGrid(editType, fleetCode);

                Page.ClickTableRowByValue(FieldNames.TaxExemptsForFleetsTable, TableHeaders.FleetCode, fleetCode);

                if (Page.IsCheckBoxCheckedWithLabel(FieldNames.DonotinheritsettingsfromParentLocation))
                {
                    Page.UncheckCheckBox(FieldNames.DonotinheritsettingsfromParentLocation);
                }

                Assert.IsFalse(Page.IsCheckBoxCheckedWithLabel(FieldNames.DonotinheritsettingsfromParentLocation), ErrorMessages.ElementNotPresent + ". Element [Do not inherit settings from Parent Location] check box.");
                Assert.IsFalse(Page.IsElementDisplayed(FieldNames.TaxExemptedStateCheckBoxList), string.Format(ErrorMessages.ElementEnabled, "Tax Exempted State CheckBox List"));
                Page.CheckCheckBox(FieldNames.DonotinheritsettingsfromParentLocation);
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.TaxExemptedStateCheckBoxList), string.Format(ErrorMessages.ElementNotEnabled, "Tax Exempted State CheckBox List"));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20821" })]
        public void TC_20821(string editType, string fleetCode, string states)
        {

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            Page.PopulateGrid(editType, fleetCode);
            Page.ClickTableRowByValue(FieldNames.TaxExemptsForFleetsTable, TableHeaders.FleetCode, fleetCode);
            List<string> statesList = states.Split(',').ToList();
            var errorMsgs = Page.AddStatesToExemption(statesList);
            errorMsgs.AddRange(Page.VerifyStatesInExemptionsColumn(FieldNames.TaxExemptsForFleetsTable, statesList));
            Page.RemoveStatesFromExemption(statesList);
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20822" })]
        public void TC_20822(string editType, string fleetCode, string states)
        {
            Page.PopulateGrid(editType, fleetCode);
            Page.ClickTableRowByValue(FieldNames.TaxExemptsForFleetsTable, TableHeaders.FleetCode, fleetCode);
            List<string> statesList = states.Split(',').ToList();
            Page.AddStatesToExemption(statesList);
            Page.ClosePopupWindow();
            menu.OpenPopUpPage(Pages.ManageTaxExemptions);
            Page = new ManageTaxExemptionsPage(driver);
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
                Page.PopulateGrid(editType, fleetCode);
                Page.ClickTableRowByValue(FieldNames.TaxExemptsForFleetsTable, TableHeaders.FleetCode, fleetCode);
                var errorMsgs = Page.RemoveStatesFromExemption(statesList);
                errorMsgs.AddRange(Page.VerifyStatesInExemptionsColumn(FieldNames.TaxExemptsForFleetsTable, statesList, false));

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17954" })]
        public void TC_17954(string editType, string fleetCode)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Page.PopulateGrid(editType, fleetCode);
                Assert.IsTrue(Page.IsCheckBoxCheckedWithLabel(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations));
                Page.CheckTableRowCheckBoxByIndex(FieldNames.ClientTaxDocumentsTable, 1);
                Page.CheckTableRowCheckBoxByIndex(FieldNames.AttachExistingDocumentsTable, 1);
                Page.UncheckCheckBox(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations);
                Assert.IsFalse(Page.IsCheckBoxCheckedWithLabel(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20816" })]
        public void TC_20816(string editType, string fleetCode)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Page.PopulateGrid(editType, fleetCode);
                Assert.IsFalse(Page.IsCheckBoxCheckedWithLabel(FieldNames.DeleteTheDocumentFromLocationsThatAreUnderTheAboveSelectedLocations),
                    string.Format(ErrorMessages.CheckBoxChecked, FieldNames.DeleteTheDocumentFromLocationsThatAreUnderTheAboveSelectedLocations));

            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20817" })]
        public void TC_20817(string editType, string fleetCode, int editTypeId, string uploadFile)
        {
            Page.PopulateGrid(Page.UploadTaxDocumentsEditType, fleetCode);
            int recordCountDB = Page.GetRecordsCountFromDB(fleetCode, editTypeId);
            Page.CheckCheckBox(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations);
            Page.UploadDocument(uploadFile, fleetCode);
            Page.ButtonClick(ButtonsAndMessages.Close);
            menu.SwitchToMainWindow();
            menu.OpenPopUpPage(Pages.ManageTaxExemptions);
            Page = new ManageTaxExemptionsPage(driver);
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Page.PopulateGrid(editType, fleetCode);
                Page.CheckCheckBox(FieldNames.DeleteTheDocumentFromLocationsThatAreUnderTheAboveSelectedLocations);
                var errorMsgs = Page.DeleteDocument(fleetCode, editTypeId, uploadFile);
                int recordCountDBNew = Page.GetRecordsCountFromDB(fleetCode, editTypeId);
                if (recordCountDB != recordCountDBNew)
                {
                    errorMsgs.Add(string.Format(ErrorMessages.DataMisMatch, $" Records count mismatch after deleting documents to that of before uploading documents."));
                }
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20818" })]
        public void TC_20818(string editType, string fleetCode, int editTypeId, string uploadFile)
        {
            Page.PopulateGrid(Page.UploadTaxDocumentsEditType, fleetCode);
            Page.CheckCheckBox(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations);
            Page.UploadDocument(uploadFile, fleetCode);
            int recordCountDB = Page.GetRecordsCountFromDB(fleetCode, editTypeId);
            Page.ButtonClick(ButtonsAndMessages.Close);
            menu.SwitchToMainWindow();
            menu.OpenPopUpPage(Pages.ManageTaxExemptions);
            Page = new ManageTaxExemptionsPage(driver);
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Page.PopulateGrid(editType, fleetCode);
                Page.UncheckCheckBox(FieldNames.DeleteTheDocumentFromLocationsThatAreUnderTheAboveSelectedLocations);
                var errorMsgs = Page.DeleteDocument(fleetCode, editTypeId, uploadFile);
                int recordCountDBNew = Page.GetRecordsCountFromDB(fleetCode, editTypeId);
                if ((recordCountDB - 1) != recordCountDBNew)
                {
                    errorMsgs.Add(string.Format(ErrorMessages.DataMisMatch, $" Records count mismatch after deleting documents to that of before uploading documents."));
                }
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
                Page.DeleteRemainingDocuments(uploadFile);
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17952" })]
        public void TC_17952(string editType, string fleetCode, int editTypeId, string uploadFile)
        {
            List<string> files = uploadFile.Split(',').ToList();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            Page.PopulateGrid(editType, fleetCode);

            Assert.IsTrue(Page.IsCheckBoxCheckedWithLabel(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations),
                string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations));

            // trying to upload .xlsx file
            var errorMsgs = Page.UploadDocument(files.First(x => x.Contains(".xlsx")), fleetCode, false);

            // trying to upload .docs file
            errorMsgs.AddRange(Page.UploadDocument(files.First(x => x.Contains(".docx")), fleetCode, false));
            Page.Click(FieldNames.RemoveAnchor);

            //trying to upload five .pdf files
            Page.Filter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, fleetCode);
            Page.CheckTableRowCheckBoxByIndex(FieldNames.ClientTaxDocumentsTable, 1);
            string pdfFile = files.First(x => x.Contains(".pdf"));
            for (int i = 0; i < 5; i++)
            {
                Page.AnchorClick(ButtonsAndMessages.Add_pascal);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
                Page.UploadFile(i, pdfFile);
            }

            // checking if add button is still visible
            Assert.IsTrue(Page.IsElementVisible(FieldNames.AddAnchor), string.Format(ErrorMessages.ElementEnabled, ButtonsAndMessages.Add_pascal));

            int recordCountDB = Page.GetRecordsCountFromDB(fleetCode, editTypeId);
            Page.UncheckCheckBox(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations);
            Page.ButtonClick(ButtonsAndMessages.UploadTaxDocuments);
            Page.AcceptAlert(out string alertMessage);
            if (Page.VerifyFileUpload() != ButtonsAndMessages.SavedSuccessfully)
            {
                throw new Exception(ErrorMessages.FileUploadingFailed);
            }
            Page.AcceptAlert(ButtonsAndMessages.SavedSuccessfully);
            Page.WaitForLoadingGrid();
            Page.ClearFilter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode);
            int recordCountDBNew = Page.GetRecordsCountFromDB(fleetCode, editTypeId);
            if ((recordCountDB + 5) != recordCountDBNew)
            {
                errorMsgs.Add(string.Format(ErrorMessages.DataMisMatch, $" Records count mismatch after uploading five documents to that of before uploading documents."));
            }
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }
            Page.ButtonClick(ButtonsAndMessages.Close);
            menu.SwitchToMainWindow();
            menu.OpenPopUpPage(Pages.ManageTaxExemptions);
            Page = new ManageTaxExemptionsPage(driver);
            Page.PopulateGrid(Page.DeleteTaxDocumentsEditType, fleetCode);
            Page.UncheckCheckBox(FieldNames.DeleteTheDocumentFromLocationsThatAreUnderTheAboveSelectedLocations);
            Page.DeleteRemainingDocuments(files.First(x => x.Contains(".pdf")));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17949" })]
        public void TC_17949(string editType, string fleetCode)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                Page.PopulateGrid(editType, fleetCode);
                Assert.IsTrue(Page.IsCheckBoxCheckedWithLabel(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations),
                    string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations));
                Page.CheckTableRowCheckBoxByIndex(FieldNames.ClientTaxDocumentsTable, 1);
                Page.UncheckCheckBox(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations);
                Assert.IsFalse(Page.IsCheckBoxCheckedWithLabel(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations),
                    string.Format(ErrorMessages.CheckBoxChecked, FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20823" })]
        public void TC_20823(string editType, string fleetCode, string name, int editTypeId)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
                List<string> errorMsgs = new List<string>();

                Page.PopulateGrid(editType, fleetCode, name: name);
                List<ManageTaxExemptionsObject> dbRecordList = ManageTaxExemptionsUtils.GetTaxDocsRecordsList(fleetCode, editTypeId);
                int rowCount = Page.GetRowCount(FieldNames.TaxExemptsForFleetsTable, true);
                if (rowCount != dbRecordList.Count)
                {
                    errorMsgs.Add(ErrorMessages.GridRowCountMisMatch + $" for Table [{FieldNames.TaxExemptsForFleetsTable}]");
                }

                errorMsgs.AddRange(Page.ValidateSortingCustomWait(FieldNames.TaxExemptsForFleetsTable, TableHeaders.FleetCode, dbRecordList.Select(x => x.FleetCode).ToList()));
                errorMsgs.AddRange(Page.ValidateSortingCustomWait(FieldNames.TaxExemptsForFleetsTable, TableHeaders.LegalName, dbRecordList.Select(x => x.LegalName).ToList()));
                errorMsgs.AddRange(Page.ValidateSortingCustomWait(FieldNames.TaxExemptsForFleetsTable, TableHeaders.ParentName, dbRecordList.Select(x => x.ParentName).ToList()));
                errorMsgs.AddRange(Page.ValidateSortingCustomWait(FieldNames.TaxExemptsForFleetsTable, TableHeaders.Exemptions, dbRecordList.Select(x => x.Exemptions).ToList()));
                errorMsgs.AddRange(Page.ValidateSortingCustomWait(FieldNames.TaxExemptsForFleetsTable, TableHeaders.City, dbRecordList.Select(x => x.City).ToList()));
                errorMsgs.AddRange(Page.ValidateSortingCustomWait(FieldNames.TaxExemptsForFleetsTable, TableHeaders.State, dbRecordList.Select(x => x.State).ToList()));
                errorMsgs.AddRange(Page.ValidateSortingCustomWait(FieldNames.TaxExemptsForFleetsTable, TableHeaders.LocationType, dbRecordList.Select(x => x.LocationType).ToList()));
                errorMsgs.AddRange(Page.ValidateSortingCustomWait(FieldNames.TaxExemptsForFleetsTable, TableHeaders.BillingLocation, dbRecordList.Select(x => x.BillingLocation).ToList()));

                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.TaxExemptsForFleetsTable, TableHeaders.FleetCode));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.TaxExemptsForFleetsTable, TableHeaders.LegalName));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.TaxExemptsForFleetsTable, TableHeaders.City));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.TaxExemptsForFleetsTable, TableHeaders.LocationType));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.TaxExemptsForFleetsTable, TableHeaders.BillingLocation));

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20819" })]
        public void TC_20819(string editType, string fleetCode, int editTypeId)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
                List<string> errorMsgs = new List<string>();

                Page.PopulateGrid(editType, fleetCode);
                List<ManageTaxExemptionsObject> dbRecordList = ManageTaxExemptionsUtils.GetTaxDocsRecordsList(fleetCode, editTypeId);
                int rowCount = Page.GetRowCount(FieldNames.ClientTaxDocumentsTable, true);
                if (rowCount != dbRecordList.Count)
                {
                    errorMsgs.Add(ErrorMessages.GridRowCountMisMatch + $" for Table [{FieldNames.ClientTaxDocumentsTable}]");
                }

                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.TaxDocument, dbRecordList.Select(x => x.DisplayFileName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.UserName, dbRecordList.Select(x => x.UserName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.UploadedDate, dbRecordList.Select(x => x.UploadedDate).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, dbRecordList.Select(x => x.CorcentricCode).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.LegalName, dbRecordList.Select(x => x.LegalName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.ParentName, dbRecordList.Select(x => x.ParentName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.City, dbRecordList.Select(x => x.City).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.State, dbRecordList.Select(x => x.State).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.LocationType, dbRecordList.Select(x => x.LocationType).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.BillingLocation, dbRecordList.Select(x => x.BillingLocation).ToList()));

                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.TaxDocument));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.UserName));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.LegalName));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.LocationType));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.BillingLocation));

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17953" })]
        public void TC_17953(string editType, string fleetCode, int editTypeId)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Pages.ManageTaxExemptions, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
                List<string> errorMsgs = new List<string>();

                Page.PopulateGrid(editType, fleetCode);
                List<ManageTaxExemptionsObject> dbRecordList = ManageTaxExemptionsUtils.GetTaxDocsRecordsList(fleetCode, editTypeId);
                int rowCount = Page.GetRowCount(FieldNames.ClientTaxDocumentsTable, true);
                if (rowCount != dbRecordList.Count)
                {
                    errorMsgs.Add(ErrorMessages.GridRowCountMisMatch + $" for Table [{FieldNames.ClientTaxDocumentsTable}]");
                }

                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.TaxDocument, dbRecordList.Select(x => x.DisplayFileName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.UserName, dbRecordList.Select(x => x.UserName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.UploadedDate, dbRecordList.Select(x => x.UploadedDate).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, dbRecordList.Select(x => x.CorcentricCode).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.LegalName, dbRecordList.Select(x => x.LegalName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.ParentName, dbRecordList.Select(x => x.ParentName).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.City, dbRecordList.Select(x => x.City).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.State, dbRecordList.Select(x => x.State).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.LocationType, dbRecordList.Select(x => x.LocationType).ToList()));
                errorMsgs.AddRange(Page.ValidateSorting(FieldNames.ClientTaxDocumentsTable, TableHeaders.BillingLocation, dbRecordList.Select(x => x.BillingLocation).ToList()));

                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.TaxDocument));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.UserName));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.LegalName));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.LocationType));
                errorMsgs.AddRange(Page.ValidateFiltering(FieldNames.ClientTaxDocumentsTable, TableHeaders.BillingLocation));

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22216" })]
        public void TC_22216(string UserType)
        {
            Page.OpenDropDown(FieldNames.EditType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EditType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.EditType));
            Page.SelectValueFirstRow(FieldNames.EditType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EditType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.EditType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.EditType, "Upload Tax Documents");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EditType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.EditType));

            Page.OpenDropDown(FieldNames.FleetCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.SelectValueFirstRow(FieldNames.FleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));
            Page.SearchAndSelectValueAfterOpen(FieldNames.FleetCode, "bsm_flt1");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));


        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22217" })]
        public void TC_22217(string UserType)
        {
            Page.OpenMultiSelectDropDown(FieldNames.State);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.State), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.State));
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.State);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.State), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.State));
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.State, TableHeaders.States, "Wyoming");
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.State), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.State));
            Page.ClearSelectionMultiSelectDropDown(FieldNames.State);
            Page.SelectAllRowsMultiSelectDropDown(FieldNames.State);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.State), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.State));
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.State);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.State), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.State));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17950" })]
        [NonParallelizable]
        public void TC_17950(string editType, string uploadFile, string masterLocation, string billingLocation)
        {
            string date = CommonUtils.ConvertDate(DateTime.Now, "MM/dd/yyyy");
            string[] splitted = uploadFile.Split("//");
            string fileName = splitted[splitted.Length - 1];

            List<AuditTrailTable> auditTrailOldList = ManageTaxExemptionsUtils.GetAuditTrails();
            Page.PopulateGrid(editType, masterLocation);
            Assert.IsTrue(Page.IsCheckBoxCheckedWithLabel(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations));
            Page.SetFilterCreiteria(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, FilterCriteria.Equals);
            Page.Filter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, masterLocation);
            Page.CheckTableRowCheckBoxByIndex(FieldNames.ClientTaxDocumentsTable, 1);
            Page.ClearFilter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode);
            Page.Filter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, billingLocation);
            Page.CheckTableRowCheckBoxByIndex(FieldNames.ClientTaxDocumentsTable, 1);
            Page.UncheckCheckBox(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations);
            Page.UploadFile(FieldNames.FileName, uploadFile);
            Page.ButtonClick(ButtonsAndMessages.UploadTaxDocuments);
            Page.AcceptAlert();
            if (Page.VerifyFileUpload() != ButtonsAndMessages.SavedSuccessfully)
            {
                throw new Exception(ErrorMessages.FileUploadingFailed);
            }
            Page.AcceptAlert(ButtonsAndMessages.SavedSuccessfully);
            Page.WaitForLoadingGrid();
            Page.ClosePopupWindow();

            menu.OpenPage(Pages.FleetLookup);
            fleetLookupPage = new FleetLookupPage(driver);
            fleetLookupPage.PopulateGrid(masterLocation);
            Assert.IsTrue(fleetLookupPage.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
            var errorMsgs = fleetLookupPage.ValidateNestedGridTabs(FieldNames.PricingLevel, FieldNames.DataRequirements, FieldNames.AuthorizationContacts, FieldNames.RelatedAccounts,
                FieldNames.TaxForms, FieldNames.Notes);
            fleetLookupPage.ClickNestedGridTab(FieldNames.TaxForms);
            fleetLookupPage.FilterNestedTable(TableHeaders.DateUploaded, date, 2);
            fleetLookupPage.FilterNestedTable(TableHeaders.FileName, fileName, 2);
            Assert.AreEqual(fileName, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.FileName, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.FileName + " in Tax Forms tab"));
            Assert.AreEqual(TestContext.CurrentContext.Test.Properties["UserName"].First().ToString().ToUpper(), fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.User, 2).ToUpper(),
                string.Format(ErrorMessages.IncorrectValue, TableHeaders.User + " in Tax Forms tab"));
            Assert.AreEqual(date, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.DateUploaded, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.UploadedDate + " in Tax Forms tab"));

            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail($" MasterLocation [{masterLocation}]:" + GetErrorMessage(errorMsg));
                }
            });

            fleetLookupPage.PopulateGrid(billingLocation);
            Assert.IsTrue(fleetLookupPage.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
            errorMsgs = fleetLookupPage.ValidateNestedGridTabs(FieldNames.PricingLevel, FieldNames.DataRequirements, FieldNames.AuthorizationContacts, FieldNames.RelatedAccounts,
                FieldNames.TaxForms, FieldNames.Notes);
            fleetLookupPage.ClickNestedGridTab(FieldNames.TaxForms);
            fleetLookupPage.FilterNestedTable(TableHeaders.DateUploaded, date, 2);
            fleetLookupPage.FilterNestedTable(TableHeaders.FileName, fileName, 2);
            Assert.AreEqual(fileName, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.FileName, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.FileName + " in Tax Forms tab"));
            Assert.AreEqual(TestContext.CurrentContext.Test.Properties["UserName"].First().ToString().ToUpper(), fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.User, 2).ToUpper(),
                string.Format(ErrorMessages.IncorrectValue, TableHeaders.User + " in Tax Forms tab"));
            Assert.AreEqual(date, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.DateUploaded, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.UploadedDate + " in Tax Forms tab"));
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail($" BillingLocation [{billingLocation}]:" + GetErrorMessage(errorMsg));
                }

                List<AuditTrailTable> auditTrailNewList = ManageTaxExemptionsUtils.GetAuditTrails();
                var tempList = auditTrailNewList.Where(x => x.AuditId > auditTrailOldList.First().AuditId).ToList();
                if (tempList.Where(x => x.AuditAction == "I" && x.AuditStatement == ButtonsAndMessages.UploadTaxDocuments && x.AuditOriginalValue == "1" && x.AuditNewValue == "0").Count() <= 0)
                {
                    Assert.Fail(string.Format(ErrorMessages.AuditTrailNotEntryNotFound, ButtonsAndMessages.UploadTaxDocuments));
                }
            });

            menu.OpenPopUpPage(Pages.ManageTaxExemptions);
            Page = new ManageTaxExemptionsPage(driver);
            Page.PopulateGrid(Page.DeleteTaxDocumentsEditType, masterLocation);
            Page.UncheckCheckBox(FieldNames.DeleteTheDocumentFromLocationsThatAreUnderTheAboveSelectedLocations);
            Page.DeleteRemainingDocuments(fileName);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17951" })]
        [NonParallelizable]
        public void TC_17951(string editType, string uploadFile, string masterLocation, string billingLocation, string shopLocation)
        {
            string date = CommonUtils.ConvertDate(DateTime.Now, "MM/dd/yyyy");
            string[] splitted = uploadFile.Split("//");
            string fileName = splitted[splitted.Length - 1];

            List<AuditTrailTable> auditTrailOldList = ManageTaxExemptionsUtils.GetAuditTrails();
            Page.PopulateGrid(editType, masterLocation);
            Assert.IsTrue(Page.IsCheckBoxCheckedWithLabel(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations));
            Page.SetFilterCreiteria(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, FilterCriteria.Equals);
            Page.Filter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, masterLocation);
            Page.CheckTableRowCheckBoxByIndex(FieldNames.ClientTaxDocumentsTable, 1);
            Page.CheckCheckBox(FieldNames.UploadToLocationsThatAreUnderTheAboveSelectedLocations);
            Page.UploadFile(FieldNames.FileName, uploadFile);
            Page.ButtonClick(ButtonsAndMessages.UploadTaxDocuments);
            Page.AcceptAlert();
            if (Page.VerifyFileUpload() != ButtonsAndMessages.SavedSuccessfully)
            {
                throw new Exception(ErrorMessages.FileUploadingFailed);
            }
            Page.AcceptAlert(ButtonsAndMessages.SavedSuccessfully);
            Page.WaitForLoadingGrid();
            Page.ClosePopupWindow();

            menu.OpenPage(Pages.FleetLookup);
            fleetLookupPage = new FleetLookupPage(driver);
            fleetLookupPage.PopulateGrid(masterLocation);
            Assert.IsTrue(fleetLookupPage.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
            var errorMsgs = fleetLookupPage.ValidateNestedGridTabs(FieldNames.PricingLevel, FieldNames.DataRequirements, FieldNames.AuthorizationContacts, FieldNames.RelatedAccounts,
                FieldNames.TaxForms, FieldNames.Notes);
            fleetLookupPage.ClickNestedGridTab(FieldNames.TaxForms);
            fleetLookupPage.FilterNestedTable(TableHeaders.DateUploaded, date, 2);
            fleetLookupPage.FilterNestedTable(TableHeaders.FileName, fileName, 2);
            Assert.AreEqual(fileName, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.FileName, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.FileName + " in Tax Forms tab"));
            Assert.AreEqual(TestContext.CurrentContext.Test.Properties["UserName"].First().ToString().ToUpper(), fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.User, 2).ToUpper(),
                string.Format(ErrorMessages.IncorrectValue, TableHeaders.User + " in Tax Forms tab"));
            Assert.AreEqual(date, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.DateUploaded, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.UploadedDate + " in Tax Forms tab"));
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail($" MasterLocation [{masterLocation}]:" + GetErrorMessage(errorMsg));
                }
            });

            fleetLookupPage.PopulateGrid(billingLocation);
            Assert.IsTrue(fleetLookupPage.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
            errorMsgs = fleetLookupPage.ValidateNestedGridTabs(FieldNames.PricingLevel, FieldNames.DataRequirements, FieldNames.AuthorizationContacts, FieldNames.RelatedAccounts,
                FieldNames.TaxForms, FieldNames.Notes);
            fleetLookupPage.ClickNestedGridTab(FieldNames.TaxForms);
            fleetLookupPage.FilterNestedTable(TableHeaders.DateUploaded, date, 2);
            fleetLookupPage.FilterNestedTable(TableHeaders.FileName, fileName, 2);
            Assert.AreEqual(fileName, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.FileName, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.FileName + " in Tax Forms tab"));
            Assert.AreEqual(TestContext.CurrentContext.Test.Properties["UserName"].First().ToString().ToUpper(), fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.User, 2).ToUpper(),
                string.Format(ErrorMessages.IncorrectValue, TableHeaders.User + " in Tax Forms tab"));
            Assert.AreEqual(date, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.DateUploaded, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.UploadedDate + " in Tax Forms tab"));
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail($" BillingLocation [{billingLocation}]:" + GetErrorMessage(errorMsg));
                }
            });

            fleetLookupPage.PopulateGrid(shopLocation);
            Assert.IsTrue(fleetLookupPage.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
            errorMsgs = fleetLookupPage.ValidateNestedGridTabs(FieldNames.PricingLevel, FieldNames.DataRequirements, FieldNames.AuthorizationContacts, FieldNames.RelatedAccounts,
                FieldNames.TaxForms, FieldNames.Notes);
            fleetLookupPage.ClickNestedGridTab(FieldNames.TaxForms);
            fleetLookupPage.FilterNestedTable(TableHeaders.DateUploaded, date, 2);
            fleetLookupPage.FilterNestedTable(TableHeaders.FileName, fileName, 2);
            Assert.AreEqual(fileName, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.FileName, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.FileName + " in Tax Forms tab"));
            Assert.AreEqual(TestContext.CurrentContext.Test.Properties["UserName"].First().ToString().ToUpper(), fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.User, 2).ToUpper(),
                string.Format(ErrorMessages.IncorrectValue, TableHeaders.User + " in Tax Forms tab"));
            Assert.AreEqual(date, fleetLookupPage.GetFirstRowDataNestedTable(TableHeaders.DateUploaded, 2), string.Format(ErrorMessages.IncorrectValue, TableHeaders.UploadedDate + " in Tax Forms tab"));
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail($" ShopLocation [{shopLocation}]:" + GetErrorMessage(errorMsg));
                }

                List<AuditTrailTable> auditTrailNewList = ManageTaxExemptionsUtils.GetAuditTrails();
                var tempList = auditTrailNewList.Where(x => x.AuditId > auditTrailOldList.First().AuditId).ToList();
                if (tempList.Where(x => x.AuditAction == "I" && x.AuditStatement == ButtonsAndMessages.UploadTaxDocuments && x.AuditOriginalValue == "1" && x.AuditNewValue == "1").Count() <= 0)
                {
                    Assert.Fail(string.Format(ErrorMessages.AuditTrailNotEntryNotFound, ButtonsAndMessages.UploadTaxDocuments));
                }
            });

            menu.OpenPopUpPage(Pages.ManageTaxExemptions);
            Page = new ManageTaxExemptionsPage(driver);
            Page.PopulateGrid(Page.DeleteTaxDocumentsEditType, masterLocation);
            Page.UncheckCheckBox(FieldNames.DeleteTheDocumentFromLocationsThatAreUnderTheAboveSelectedLocations);
            Page.DeleteRemainingDocuments(fileName);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20212" })]
        public void TC_20212(string UserType)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(FieldNames.ManageStateExemption, Page.GetValueOfDropDown(FieldNames.EditType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.EditType));
                Assert.IsTrue(Page.VerifyValue(FieldNames.EditType, FieldNames.ManageStateExemption, FieldNames.UploadTaxDocuments, FieldNames.DeleteTaxDocuments, FieldNames.AttachExistingTaxDocuments)
                    , GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.EditType));
                Assert.AreEqual("", Page.GetValueOfDropDown(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.FleetCode));
                Assert.AreEqual("", Page.GetValue(FieldNames.Name), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Name));
                Assert.AreEqual("", Page.GetValue(FieldNames.Address), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Address));
                Assert.AreEqual("", Page.GetValue(FieldNames.City), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.City));
                Assert.AreEqual("", Page.GetValueOfDropDown(FieldNames.State), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.State));
                List<string> states = CommonUtils.GetStates();
                Page.VerifyDataMultiSelectDropDown(FieldNames.State, states.ToArray());

                Page.SelectValueTableDropDown(FieldNames.EditType, FieldNames.UploadTaxDocuments);
                Assert.IsTrue(Page.IsElementVisibleOnScreen(FieldNames.TaxDocumentName), GetErrorMessage(ErrorMessages.FieldNotDisplayed, $"{FieldNames.TaxDocumentName} for {FieldNames.UploadTaxDocuments}"));
                Page.SelectValueTableDropDown(FieldNames.EditType, FieldNames.DeleteTaxDocuments);
                Assert.IsTrue(Page.IsElementVisibleOnScreen(FieldNames.TaxDocumentName), GetErrorMessage(ErrorMessages.FieldNotDisplayed, $"{FieldNames.TaxDocumentName} for {FieldNames.DeleteTaxDocuments}"));
                Page.SelectValueTableDropDown(FieldNames.EditType, FieldNames.AttachExistingTaxDocuments);
                Assert.IsTrue(Page.IsElementVisibleOnScreen(FieldNames.TaxDocumentName), GetErrorMessage(ErrorMessages.FieldNotDisplayed, $"{FieldNames.TaxDocumentName} for {FieldNames.AttachExistingTaxDocuments}"));
                Page.SelectValueTableDropDown(FieldNames.EditType, FieldNames.ManageStateExemption);
                Assert.IsFalse(Page.IsElementVisibleOnScreen(FieldNames.TaxDocumentName), GetErrorMessage(ErrorMessages.FieldDisplayed, $"{FieldNames.TaxDocumentName} for {FieldNames.ManageStateExemption}"));

                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                Page.PopulateGrid(FieldNames.ManageStateExemption, null);
                var rowCount = Page.GetRowCount(FieldNames.TaxExemptsForFleetsTable, true);
                if (rowCount > 0)
                {
                    var errorMsgs = Page.ValidateHeadersByScroll(FieldNames.TaxExemptsForFleetsTable, TableHeaders.FleetCode, TableHeaders.LegalName, TableHeaders.ParentName, TableHeaders.Exemptions, TableHeaders.Address,
                        TableHeaders.City, TableHeaders.State, TableHeaders.Country, TableHeaders.LocationType, TableHeaders.BillingLocation);
                    Page.ScrollToHeader(FieldNames.TaxExemptsForFleetsTable, TableHeaders.FleetCode);
                    Page.ClickTableRowByIndex(FieldNames.TaxExemptsForFleetsTable, TableHeaders.ParentName, 1);

                    Assert.IsTrue(Page.IsCheckBoxExistsWithLabel(FieldNames.DonotinheritsettingsfromParentLocation), ErrorMessages.ElementNotPresent + $". Element [{FieldNames.DonotinheritsettingsfromParentLocation}] check box.");
                    Assert.IsFalse(Page.IsElementDisplayed(FieldNames.TaxExemptedStateCheckBoxList), string.Format(ErrorMessages.ElementEnabled, "Tax Exempted State CheckBox List"));
                    errorMsgs.AddRange(Page.VerifyStatesCheckBoxAvailability(states));
                    if (!Page.IsCheckBoxExistsWithLabel("Select All"))
                    {
                        errorMsgs.Add($"Select All checkbox not exists.");
                    }

                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }

                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.SaveChanges), GetErrorMessage(ErrorMessages.ButtonMissing, ButtonsAndMessages.SaveChanges));
                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Close), GetErrorMessage(ErrorMessages.ButtonMissing, ButtonsAndMessages.Close));
                }
            });
        }
    }
}