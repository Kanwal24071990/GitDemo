using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.BulkActions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.BulkActions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AutomationTesting_CorConnect.StepDefinitions.BulkActions
{
    [Binding]
    internal class BulkActionsStepDefinitions : DriverBuilderClass
    {
        BulkActionsPage Page;
        dynamic imagesrc;
        int rowCountFromTableBulkActionsMaster;

        [StepDefinition(@"File with (.*) is uploaded for ""([^""]*)""")]
        public void FileIsUploadedFor(string fileType, string actionType)
        {
            Page = new BulkActionsPage(driver);
            Page.SelectAction(actionType);

            switch (actionType)
            {
                case "Invoice Update":
                    Assert.IsTrue(Page.IsAnchorVisible(ButtonsAndMessages.BrowseWithElipsis));
                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Upload));
                    Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Submit));
                    Assert.IsTrue(Page.IsElementVisible(FieldNames.FileName));
                    if (fileType == "PDF")
                    {
                        Page.UploadFileInFileName("UploadFiles//SamplePDF.pdf");
                    }
                    else if (fileType == "DOC")
                    {
                        Page.UploadFileInFileName("UploadFiles//SampleWordDoc.docx");
                    }
                    else { 
                        Page.UploadFileInFileName("UploadFiles//BulkInvoiceUpdate" + fileType + ".xlsx"); 
                    }
                    break;

                case "Invoice Reversal":
                    switch (fileType)
                    {
                        case "Invoice Number":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalMandatoryInv.xlsx");
                            break;
                        case "Approver":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalMandatoryApprover.xlsx");
                            break;
                        case "Dealer Code":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalMandatorySuppCode.xlsx");
                            break;
                        case "Fleet Code":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalMandatoryByrCode.xlsx");
                            break;
                        case "Reason":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalMandatoryReason.xlsx");
                            break;                       
                        case "Dont Reverse Fees":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalMandatoryReverseFee.xlsx");
                            break;
                        case "Send Notification":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalMandatorySendNoti.xlsx");
                            break;
                        case "Header On 5th Row":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalHeaderOnFifthRow.xlsx");
                            break;
                        case "Blank File":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalBlankFile.xlsx");
                            break;
                        case "Multiple Errors Bulk Reversal":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalMultipleError.xlsx");
                            break;
                        case "Sample Bulk Reversal":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalValidFile.xlsx");
                            break;
                        case "No Records":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalWithNoRecords.xlsx");
                            break;
                        case "Doc":
                            Page.UploadFileInFileName("UploadFiles//SampleDocFileUpdated.doc");
                            break;
                        case "Docx":
                            Page.UploadFileInFileName("UploadFiles//SampleWordDoc.docx");
                            break;
                        case "PDF":
                            Page.UploadFileInFileName("UploadFiles//SamplePDF.pdf");
                            break;
                        case "Duplicate Invoice Number":
                            Page.UploadFileInFileName("UploadFiles//BulkReversalDuplicateInvoice.xlsx");
                            break;
                    }
                    break;
            }
            
            Page.ButtonClick(ButtonsAndMessages.Upload);
        }

        [When(@"File with (.*) submitted for ""([^""]*)""")]
        public void WhenFileWithSubmittedFor(string fileName, string bulkAction)
        {


            switch (bulkAction)
            {
                case "Invoice Update":
                    if (fileName == "1Header1DataRows")
                    {
                        Page.AcceptWindowAlert(out string fileUploadedMsg);
                        Assert.AreEqual(ButtonsAndMessages.FileHasBeenProcessedAndUploadedSuccessfully, fileUploadedMsg, ErrorMessages.ErrorMessageMismatch);
                        Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
                    }
                    else
                    {
                        rowCountFromTableBulkActionsMaster = BulkActionsUtil.GetRowCountFromTable("bulkactionsmaster_tb");
                        Page.VerifyFileUploaded();
                        Page.SelectFieldsOnFile();
                    }
                    break;
                case "Invoice Reversal":
                    Page.AcceptWindowAlert(out string msg);
                    Assert.AreEqual(msg, ButtonsAndMessages.FileUploadSuccessfully);
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile1), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile2), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile3), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile4), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile5), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile6), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile7), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile8), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.AreEqual("Invoice Number", Page.GetValueOfDropDown(FieldNames.CorConnectFields1));
                    Assert.AreEqual(Page.RenameMenuField("Dealer Code"),Page.GetValueOfDropDown(FieldNames.CorConnectFields2));
                    Assert.AreEqual(Page.RenameMenuField("Fleet Code"), Page.GetValueOfDropDown(FieldNames.CorConnectFields3));
                    Assert.AreEqual("Reason", Page.GetValueOfDropDown(FieldNames.CorConnectFields4));
                    Assert.AreEqual("Approver",Page.GetValueOfDropDown(FieldNames.CorConnectFields5));
                    Assert.AreEqual("Comments", Page.GetValueOfDropDown(FieldNames.CorConnectFields6));
                    Assert.AreEqual("Don't Reverse Fees",Page.GetValueOfDropDown(FieldNames.CorConnectFields7));
                    Assert.AreEqual("Send Notification",Page.GetValueOfDropDown(FieldNames.CorConnectFields8));
                    Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "invoiceNumber");
                    Page.SelectValueTableDropDown(FieldNames.FieldOnFile2, "Supplier Code");
                    Page.SelectValueTableDropDown(FieldNames.FieldOnFile3, "Buyer Code");
                    Page.SelectValueTableDropDown(FieldNames.FieldOnFile4, "Reason");
                    Page.SelectValueTableDropDown(FieldNames.FieldOnFile5, "Approver");
                    Page.SelectValueTableDropDown(FieldNames.FieldOnFile6, "Comments");
                    Page.SelectValueTableDropDown(FieldNames.FieldOnFile7, "Don't reverse fees");
                    Page.SelectValueTableDropDown(FieldNames.FieldOnFile8, "Send notifaction");                    
                    break;
            }

            Page.ButtonClick(ButtonsAndMessages.Submit);
        }

        [When(@"File with ""([^""]*)"" submitted for ""([^""]*)"" without mapping")]
        public void WhenFileWithSubmittedForWithoutMapping(string fileName, string bulkAction)
        {
            Page.AcceptWindowAlert(out string msg);
            Assert.AreEqual(msg, ButtonsAndMessages.FileUploadSuccessfully);

            switch (bulkAction)
            {
                case "Invoice Update":
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile1), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile2), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields1), "Program Invoice Number");
                    Assert.AreEqual(Page.GetValueOfDropDown(FieldNames.CorConnectFields2), "PO Number");
                    break;
                case "Invoice Reversal":
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile1), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile2), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile3), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile4), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile5), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile6), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile7), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.IsEmpty(Page.GetValueOfDropDown(FieldNames.FieldOnFile8), GetErrorMessage(ErrorMessages.FieldNotEmpty));
                    Assert.AreEqual("Invoice Number", Page.GetValueOfDropDown(FieldNames.CorConnectFields1));
                    Assert.AreEqual(Page.RenameMenuField("Dealer Code"), Page.GetValueOfDropDown(FieldNames.CorConnectFields2));
                    Assert.AreEqual(Page.RenameMenuField("Fleet Code"), Page.GetValueOfDropDown(FieldNames.CorConnectFields3));
                    Assert.AreEqual("Reason", Page.GetValueOfDropDown(FieldNames.CorConnectFields4));
                    Assert.AreEqual("Approver", Page.GetValueOfDropDown(FieldNames.CorConnectFields5));
                    Assert.AreEqual("Comments", Page.GetValueOfDropDown(FieldNames.CorConnectFields6));
                    Assert.AreEqual("Don't Reverse Fees", Page.GetValueOfDropDown(FieldNames.CorConnectFields7));
                    Assert.AreEqual("Send Notification", Page.GetValueOfDropDown(FieldNames.CorConnectFields8));
                    break;
            }
            Page.ButtonClick(ButtonsAndMessages.Submit);
        }

        [Then(@"Message ""([^""]*)"" should appear")]
        public void ThenMessageShouldAppear(string error)
        {
            Page = new BulkActionsPage(driver);
            switch (error)
            {
                case "FileIsEmpty":
                    Page.AcceptWindowAlert(out string emptyFileMsg);
                    Assert.AreEqual(ButtonsAndMessages.UploadedExcelFileIsEmpty, emptyFileMsg, ErrorMessages.ErrorMessageMismatch);
                    break;

                case "FileExtensionIsNotAllowed":
                    string errorText = Page.GetFileUploadErrorMsg(ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
                    Assert.AreEqual(ButtonsAndMessages.ThisFileExtensionIsNotAllowed, errorText);
                    break;

                case "ProgramInvoiceNumberAndAtLeastOneValueError":
                    Page.AcceptWindowAlert(out string msgPInvoiceNumber);
                    Assert.AreEqual(ButtonsAndMessages.ProgramInvoiceNumberAndAtLeastOneValueError, msgPInvoiceNumber.Trim());
                    break;

                case "FileHasBeenProcessedAndUploadedSuccessfully":
                    Page.VerifyFileUploaded();
                    break;

                case "InvoiceUpdateStatusWillBeSendByEmail":
                    Page.AcceptWindowAlert(out string msg);
                    Assert.AreEqual("Invoice update status for 2 Invoices will be sent by email once processed", msg.Trim(), ErrorMessages.ErrorMessageMismatch);
                    break;

                case "DuplicateInvoiceNumber":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.DuplicateInvoiceNumbers);
                    break;

                case "PONumbersMaxLimit":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.PONumbersMaxLimit);
                    break;

                case "VehicleIdMaxLimit":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.VehicleIdMaxLimit);
                    break;

                case "UnitNumberMaxLimit":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.UnitNumberMaxLimit);
                    break;

                case "VinNumberMaxLimit":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.VinNumberMaxLimit);
                    break;

                case "MaxVehicleMaxLimit":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.VehicleMileageMaxLimit);
                    break;

                case "RefNumberMaxLimit":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.RefNumberMaxLimit);
                    break;

                case "EmptyInvoiceNumber":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.InvoiceNumberEmpty);
                    break;

                case "MultipleErrors":
                    Page.AcceptWindowAlert(out string MultiErrorMsg);
                    MultiErrorMsg = MultiErrorMsg.Replace("\r\n", "");
                    string result = Regex.Replace(MultiErrorMsg, @"\s+Duplicate", " Duplicate");
                    Assert.AreEqual(ButtonsAndMessages.BulkActionsMultipleErrors, result.Trim(), ErrorMessages.ErrorMessageMismatch);
                    break;

                case "InvalidInvoiceNumber":
                    Page.verifyErrorMsgAndDB(rowCountFromTableBulkActionsMaster, ButtonsAndMessages.InvalidInvoiceNumberPO);
                    break;

                case "Upload File To Proceed":
                    Page.AcceptWindowAlert(out string uploadFileToProceed);
                    Assert.AreEqual("Please upload a file to proceed", uploadFileToProceed, ErrorMessages.ErrorMessageMismatch);
                    break;

		        case "Duplicate Invoices For Bulk Reversal":
                    Page.AcceptWindowAlert(out string duplicateMsg);
                    Assert.AreEqual("Duplicate invoice numbers [CFI000110314]; please remove duplicate invoice numbers.", duplicateMsg.Trim(), ErrorMessages.ErrorMessageMismatch);
                    break;
                case "Multiple Error For Bulk Reversal":
                    Page.AcceptWindowAlert(out string MultiErrorMsg1);
                    MultiErrorMsg1 = MultiErrorMsg1.Replace("\r\n", "");
                    string result1 = Regex.Replace(MultiErrorMsg1, @"\s+Duplicate", " Duplicate");
                    Assert.AreEqual("Mandatory field(s) [Invoice Number] required for processing; please enter values. Duplicate invoice numbers [CFI000110314]; please remove duplicate invoice numbers.", result1.Trim(), ErrorMessages.ErrorMessageMismatch);
                    break;

                case "No Records":
                    Page.AcceptWindowAlert(out string emptyHeaderFileMsg);
                    Assert.AreEqual("The file contains no data for Invoice Number,Dealer Code,Fleet Code,Reason,Approver,Do not Reverse Fees,Send Notification field.", emptyHeaderFileMsg, ErrorMessages.ErrorMessageMismatch);
                    break;

                case "Invoice submitted successfully":
                    Page.AcceptWindowAlert(out string invoiceSubmittedSuccessfully);
                    Assert.AreEqual("Invoice reversal for 1 Invoices will be sent by email once processed", invoiceSubmittedSuccessfully, ErrorMessages.ErrorMessageMismatch);
                    break;
            }
            
        }

        [Then(@"Message Mandatory field (.*) For Bulk Reversal should appear")]
        public void ThenMessageMandatoryFieldInvoiceNumberForBulkReversalShouldAppear(string headerField)
        {
            Page.AcceptWindowAlert(out string mandatoryMsg);
            Assert.AreEqual("Mandatory field(s)"+" "+"["+headerField+"]"+" "+"required for processing; please enter values.", mandatoryMsg.Trim(), ErrorMessages.ErrorMessageMismatch);
            
        }



        [When(@"""([^""]*)"" template is downloaded")]
        public void WhenTemplateIsDownloaded(string actionType)
        {
            Page = new BulkActionsPage(driver);
            Page.SelectAction(actionType);

            switch (actionType) 
            {
                case "Invoice Reversal":
                    imagesrc = Page.GetSrc(FieldNames.DownloadReversalTemplate, true);                                  
                    break;
            }
           
        }

        [Then(@"Invoice Reversal template is downloaded successfully")]
        public void ThenTemplateIsDownloadedSuccessfully()
        {
            Assert.IsTrue(imagesrc.EndsWith("/download.png"), GetErrorMessage(ErrorMessages.SrcMisMatch));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FileName), GetErrorMessage(ErrorMessages.ElementNotPresent));
        }

        [Then(@"Field on File required message should appear for ""([^""]*)""")]
        public void ThenFieldOnFileRequiredErrorShouldAppearOnDropdownFor(string actionType)
        {
            switch (actionType) 
            {
                case "Invoice Reversal":
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FieldOnFile1MandatoryMessage),GetErrorMessage(ErrorMessages.ElementNotPresent));
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FieldOnFile2MandatoryMessage), GetErrorMessage(ErrorMessages.ElementNotPresent));
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FieldOnFile3MandatoryMessage), GetErrorMessage(ErrorMessages.ElementNotPresent));
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FieldOnFile4MandatoryMessage), GetErrorMessage(ErrorMessages.ElementNotPresent));
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FieldOnFile5MandatoryMessage), GetErrorMessage(ErrorMessages.ElementNotPresent));
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FieldOnFile6MandatoryMessage), GetErrorMessage(ErrorMessages.ElementNotPresent));
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FieldOnFile7MandatoryMessage), GetErrorMessage(ErrorMessages.ElementNotPresent));
                    Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FieldOnFile8MandatoryMessage), GetErrorMessage(ErrorMessages.ElementNotPresent));
                    break;
            }
        }

        [Then(@"All fields should be displayed & Program Invoice Number should be disabled")]
        public void ThenAllFieldsShouldBeDisplayedProgramInvoiceNumberShouldBeDisabled()
        {
            Page = new BulkActionsPage(driver);
            Page.VerifyFileUploaded();
            Page.VerifyDropdownValuesIU();

            Page.ButtonClick(ButtonsAndMessages.Submit);
            Assert.AreEqual(Page.GetText(FieldNames.FieldOnFile1MandatoryMessage), ButtonsAndMessages.PleaseSelectAValue);

            Page.SelectValueTableDropDown(FieldNames.FieldOnFile1, "Invoice Number");
            Page.ButtonClick(ButtonsAndMessages.Submit);
            Page.AcceptWindowAlert(out string msg);
            Assert.AreEqual(ButtonsAndMessages.ProgramInvoiceNumberAndAtLeastOneValueError, msg.Trim());
        }

        [When(@"Submission is performed on ""([^""]*)""")]
        public void WhenSubmissionIsPerformedOn(string actionType)
        {
            Page = new BulkActionsPage(driver);
            Page.SelectAction(actionType);
            Page.ButtonClick(ButtonsAndMessages.Submit);
        }



    }
}
