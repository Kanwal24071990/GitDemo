using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutomationTesting_CorConnect.Utils.BulkActions;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.PageObjects.BulkActions
{
    internal class BulkActionsPage : PopUpPage
    {
        internal BulkActionsPage(IWebDriver driver) : base(driver, Pages.BulkActions)
        {

        }

        internal void SelectAction(string action)
        {
            SelectValueTableDropDown(FieldNames.Action, action);
        }

        internal void UploadFileInFileName(string file)
        {
            UploadFile(FieldNames.FileName, file);
        }

        internal override string GetAlertMessage()
        {
            try
            {
                return base.GetAlertMessage();
            }
            catch (UnhandledAlertException)
            {
                return base.GetAlertMessage();
            }
        }

        internal void AcceptAlertMessage()
        {
            try
            {
                AcceptAlert();
            }
            catch (UnhandledAlertException ex)
            {
                AcceptAlert();
            }
        }

        internal void AcceptAlertMessage(out string fileUploadMsg)
        {
            try
            {
                AcceptAlert(out fileUploadMsg);
            }
            catch (UnhandledAlertException ex)
            {
                AcceptAlert(out fileUploadMsg);
            }
        }

        internal void SelectDistributionMethod(string method)
        {
            SelectValueTableDropDown(FieldNames.DistributionMethod, method);
        }

        internal void SelectAttachmentType(string type)
        {
            SelectValueTableDropDown(FieldNames.AttachmentType, type);
        }

        internal void EnterBatchSize(string batchSize)
        {
            EnterText(FieldNames.BatchSize, batchSize);
        }

        internal void EnterEmailAddress(string emailAddress)
        {
            EnterText(FieldNames.EmailAddress, emailAddress);
        }

        internal string GetFileUploadErrorMsg(string msg)
        {
            
            WaitForTextToBePresentInElementLocated(FieldNames.FileUploadError, msg);
            string errorText = GetText(FieldNames.FileUploadError);

            return errorText;
        }        
        internal string GetCorConnectFieldErrorMsg(string msg)
        {
            
            WaitForTextToBePresentInElementLocated(FieldNames.CorConnectFields1Msg, msg);
            string errorText = GetText(FieldNames.CorConnectFields1Msg);

            return errorText;
        }

        internal By GetButton()
        {
            return GetElement(FieldNames.DownloadDisputeTemplate);
        }

        internal List<string> GetHeaderRowFromFile(string file) {

            List<string> headers = new List<string>();
            var data = new DataTable();
            try
            {
                ExcelParser parser = new();
                data = parser.ReadData(file, "Dispute$");

                headers = data.Rows[0].ItemArray.Select(x => x.ToString()).ToList();
            }
            catch (Exception ex)
            {

            }
            return headers;
        }

        internal string GetUserNameFromFile(string file)
        {

            string user = "";
            var data = new DataTable();
            try
            {
                ExcelParser parser = new();
                data = parser.ReadData(file, "Dispute$");

                for (int i = 1; i < data.Rows.Count; i++)
                {
                    var row = data.Rows[i];
                    user = row[2].ToString();
                }
               
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        internal List<string> GetReasonsFromTemplateFile(string file)
        {

            List<string> reasons = new List<string>();
            var data = new DataTable();
            try
            {
                ExcelParser parser = new();
                data = parser.ReadData(file, "Lookup$");
                
                for (int i = 1; i < data.Rows.Count; i++)
                {
                    var row = data.Rows[i];
                    reasons.Add(row[0].ToString().Split("-")[1]);
                }

            }
            catch (Exception ex)
            {

            }
            return reasons;
        }

        internal void verifyErrorMsgAndDB(int rowCountFromTableBulkActionsMaster, string error)
        {
            AcceptWindowAlert(out string msg);
            Assert.AreEqual(error, msg.Trim(), ErrorMessages.ErrorMessageMismatch);
            var rowCountFromTableBulkActionsMasterAfter = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
            Assert.AreEqual(rowCountFromTableBulkActionsMaster, rowCountFromTableBulkActionsMasterAfter, ErrorMessages.DataMisMatch);
        }
        internal void VerifyFileUploaded()
        {
            string[] corcentricFileFields = { "Invoice Number", "PO Number", "Vehicle ID", "Unit Number", "Vin Number", "Vehicle Mileage", "Ref #" };
            AcceptWindowAlert(out string msg);
            Assert.AreEqual(ButtonsAndMessages.FileHasBeenProcessedAndUploadedSuccessfully, msg, ErrorMessages.ErrorMessageMismatch);

            Assert.IsEmpty(GetValueOfDropDown(FieldNames.FieldOnFile1), ErrorMessages.FieldNotEmpty);
            Assert.IsEmpty(GetValueOfDropDown(FieldNames.FieldOnFile2), ErrorMessages.FieldNotEmpty);
            Assert.IsEmpty(GetValueOfDropDown(FieldNames.FieldOnFile3), ErrorMessages.FieldNotEmpty);
            Assert.IsEmpty(GetValueOfDropDown(FieldNames.FieldOnFile4), ErrorMessages.FieldNotEmpty);
            Assert.IsEmpty(GetValueOfDropDown(FieldNames.FieldOnFile5), ErrorMessages.FieldNotEmpty);
            Assert.IsEmpty(GetValueOfDropDown(FieldNames.FieldOnFile6), ErrorMessages.FieldNotEmpty);
            Assert.IsEmpty(GetValueOfDropDown(FieldNames.FieldOnFile7), ErrorMessages.FieldNotEmpty);

            Assert.AreEqual(GetValueOfDropDown(FieldNames.CorConnectFields1), "Program Invoice Number");
            Assert.AreEqual(GetValueOfDropDown(FieldNames.CorConnectFields2), "PO Number");
            Assert.AreEqual(GetValueOfDropDown(FieldNames.CorConnectFields3), "Vehicle ID");
            Assert.AreEqual(GetValueOfDropDown(FieldNames.CorConnectFields4), "Unit Number");
            Assert.AreEqual(GetValueOfDropDown(FieldNames.CorConnectFields5), "Vin Number");
            Assert.AreEqual(GetValueOfDropDown(FieldNames.CorConnectFields6), "Vehicle Mileage");
            Assert.AreEqual(GetValueOfDropDown(FieldNames.CorConnectFields7), "Ref #");

            Assert.AreEqual(VerifyDropDownIsDisabled(FieldNames.CorConnectFields1), "true");
            Assert.AreEqual(VerifyDropDownIsDisabled(FieldNames.CorConnectFields2), "true");
            Assert.AreEqual(VerifyDropDownIsDisabled(FieldNames.CorConnectFields3), "true");
            Assert.AreEqual(VerifyDropDownIsDisabled(FieldNames.CorConnectFields4), "true");
            Assert.AreEqual(VerifyDropDownIsDisabled(FieldNames.CorConnectFields5), "true");
            Assert.AreEqual(VerifyDropDownIsDisabled(FieldNames.CorConnectFields6), "true");
            Assert.AreEqual(VerifyDropDownIsDisabled(FieldNames.CorConnectFields7), "true");
        }

        internal void VerifyDropdownValuesIU()
        {
            string[] corcentricFileFields = { "Invoice Number", "PO Number",
                                              "Vehicle ID", "Unit Number",
                                              "Vin Number", "Vehicle Mileage",
                                              "Ref #" };

            Assert.IsTrue(VerifyValue(FieldNames.FieldOnFile1, corcentricFileFields), ErrorMessages.ListElementsMissing);
            Assert.IsTrue(VerifyValue(FieldNames.FieldOnFile2, corcentricFileFields), ErrorMessages.ListElementsMissing);
            Assert.IsTrue(VerifyValue(FieldNames.FieldOnFile3, corcentricFileFields), ErrorMessages.ListElementsMissing);
            Assert.IsTrue(VerifyValue(FieldNames.FieldOnFile4, corcentricFileFields), ErrorMessages.ListElementsMissing);
            Assert.IsTrue(VerifyValue(FieldNames.FieldOnFile5, corcentricFileFields), ErrorMessages.ListElementsMissing);
            Assert.IsTrue(VerifyValue(FieldNames.FieldOnFile6, corcentricFileFields), ErrorMessages.ListElementsMissing);
            Assert.IsTrue(VerifyValue(FieldNames.FieldOnFile7, corcentricFileFields), ErrorMessages.ListElementsMissing);
        }

        internal void SelectFieldsOnFile()
        {
            SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            SelectValueTableDropDown(FieldNames.FieldOnFile2, "PO Number");
            SelectValueTableDropDown(FieldNames.FieldOnFile3, "Vehicle ID");
            SelectValueTableDropDown(FieldNames.FieldOnFile4, "Unit Number");
            SelectValueTableDropDown(FieldNames.FieldOnFile5, "Vin Number");
            SelectValueTableDropDown(FieldNames.FieldOnFile6, "Vehicle Mileage");
            SelectValueTableDropDown(FieldNames.FieldOnFile7, "Ref #");
        } 
    }
}
