Feature: BulkAction-Reversal

As a user i want to verify bulk reversal functionality

Background: Navigate to Bulk Action Page
	Given User "Admin" is on "Bulk Actions" popup page


@BulkActions @Functional @Smoke @CON-25452 @CON-27956 @CON-27957 @CON-27958 @CON-27959 @CON-27960 @CON-27961 @20.0
Scenario Outline: Submitting template without mandatory fields 
	Given File with <columnname> is uploaded for "Invoice Reversal"
	When File with <columnname> submitted for "Invoice Reversal"
	Then Message Mandatory field <columnname> For Bulk Reversal should appear
Examples: 
| columnname          |
| Invoice Number      |
| Dealer Code         |
| Fleet Code          |
| Approver            |
| Reason              |
| Dont Reverse Fees	  |
| Send Notification   |


@BulkActions @Functional @Smoke @CON-25452 @CON-27945 @20.0
Scenario: Duplicate invoice number cannot be submitted
	Given File with Duplicate Invoice Number is uploaded for "Invoice Reversal"
	When File with "Duplicate Invoice Number" submitted for "Invoice Reversal"
	Then Message "Duplicate Invoices For Bulk Reversal" should appear


@BulkActions @Functional @Smoke @CON-25452 @CON-27946 @20.0
Scenario: Verify bulk action reversal when file uploaded with no records
	Given File with No Records is uploaded for "Invoice Reversal"
	Then Message "FileIsEmpty" should appear


@BulkActions @Functional @Smoke @CON-25452 @CON-27947 @20.0
Scenario: Verify bulk action reversal with multiple errors(Duplicate Invoice Number , Mandatory Field Empty)
	Given File with Multiple Errors Bulk Reversal is uploaded for "Invoice Reversal"
	When File with "Multiple Errors Bulk Reversal" submitted for "Invoice Reversal"
	Then Message "Multiple Error For Bulk Reversal" should appear


@BulkActions @Functional @Smoke @CON-25452 @CON-27948 @20.0
Scenario: Verify empty excel file uploaded for bulk action reversal
	Given File with Blank File is uploaded for "Invoice Reversal"
	Then Message "FileIsEmpty" should appear


@BulkActions @Functional @Smoke @CON-25452 @CON-27962 @CON-27963 @CON-27964 @20.0
Scenario Outline: Verify Invalid Format file is not Uploaded for Bulk Action Invoice Reversal
	When File with <invalidextensionfile> is uploaded for "Invoice Reversal"
	Then Message "FileExtensionIsNotAllowed" should appear
Examples: 
| invalidextensionfile |
| Doc                  |
| Docx                 |
| PDF                  |


@BulkActions @Functional @Smoke @CON-25452 @CON-27949 @20.0
Scenario: Verify bulk action reversal when headers on 5th row 
	When File with Header On 5th Row is uploaded for "Invoice Reversal"
	Then Message "Upload Success" should appear

@BulkActions @Functional @Smoke @CON-25452 @CON-27951 @20.0
Scenario: Verify download template should be sucessful
	When "Invoice Reversal" template is downloaded 
	Then Invoice Reversal template is downloaded successfully


@BulkActions @Functional @Smoke @CON-25452 @CON-27951 @20.0
Scenario: Verify file should be sucessfully uploaded
	Given File with Sample Bulk Reversal is uploaded for "Invoice Reversal"
	Then Message "Upload Success" should appear

@BulkActions @Functional @Smoke @CON-25452 @CON-27953 @20.0
Scenario: Verify valid file should be processed after submitting
	Given File with Sample Bulk Reversal is uploaded for "Invoice Reversal"
	When File with "Sample Bulk Reversal" submitted for "Invoice Reversal"
	Then Message "Invoice submitted successfully" should appear
	

@BulkActions @Functional @Smoke @CON-25452 @CON-27954 @20.0
Scenario: All fields on file should be empty when hitting submit
	Given File with Sample Bulk Reversal is uploaded for "Invoice Reversal"
	When File with "Sample Bulk Reversal" submitted for "Invoice Reversal" without mapping
	Then Field on File required message should appear for "Invoice Reversal"


@BulkActions @Functional @Smoke @CON-25452 @CON-27955 @20.0
Scenario:Verify Unable to submit if file is not uploaded
	When Submission is performed on "Invoice Reversal"
	Then Message "Upload File To Proceed" should appear