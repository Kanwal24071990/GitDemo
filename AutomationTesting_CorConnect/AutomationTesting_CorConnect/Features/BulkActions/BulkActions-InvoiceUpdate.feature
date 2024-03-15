Feature: Bulk Actions Invoice Update

As a user i want to verify bulk Invoice Update functionality

Background: Load Application with given user and navigate
	Given User "Admin" is on "Bulk Actions" popup page

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with Only1HeaderRow Uploaded for Bulk Action Invoice Update
	When File with Only1HeaderRow is uploaded for "Invoice Update"
	Then Message "FileIsEmpty" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify NoHeaderNoData file Uploaded for Bulk Action Invoice Update
	When File with NoHeaderNoData is uploaded for "Invoice Update"
	Then Message "FileIsEmpty" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify 1Header1DataRows file Uploaded for Bulk Action Invoice Update
	When File with 1Header1DataRows is uploaded for "Invoice Update"
	When File with 1Header1DataRows submitted for "Invoice Update"
	Then Message "ProgramInvoiceNumberAndAtLeastOneValueError" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify 2HeaderNoDataRows file Uploaded for Bulk Action Invoice Update
	When File with 2HeaderNoDataRows is uploaded for "Invoice Update"
	Then Message "FileIsEmpty" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify AllHeaderNoDataRows file Uploaded for Bulk Action Invoice Update
	When File with AllHeaderNoDataRows is uploaded for "Invoice Update"
	Then Message "FileIsEmpty" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify 2Header2DataRows file Uploaded for Bulk Action Invoice Update
	When File with 2Header2DataRows is uploaded for "Invoice Update"
	Then Message "FileHasBeenProcessedAndUploadedSuccessfully" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify AllHeaderN2DataRows file Uploaded for Bulk Action Invoice Update
	When File with AllHeaderN2DataRows is uploaded for "Invoice Update"
	When File with AllHeaderN2DataRows submitted for "Invoice Update"
	Then Message "InvoiceUpdateStatusWillBeSendByEmail" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario Outline: Verify Invalid File Format is not Uploaded for Bulk Action Invoice Update
	When File with <FileFormate> is uploaded for "Invoice Update"
	Then Message "FileExtensionIsNotAllowed" should appear

	Examples:
	| FileFormate |
	| PDF         |
	| DOC         |

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify Missing Invoice Number file Uploaded & unable to submit for Bulk Action Invoice Update
	Given File with EmptyInvoiceNumber is uploaded for "Invoice Update"
	When File with "EmptyInvoiceNumber" submitted for "Invoice Update"
	Then Message "EmptyInvoiceNumber" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with Duplicate Invoice Number is submitted for Bulk Action Invoice Update
	Given File with DuplicateInvoiceNumber is uploaded for "Invoice Update"
	When File with "DuplicateInvoiceNumber" submitted for "Invoice Update"
	Then Message "DuplicateInvoiceNumber" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with MultipleErrors submitted for Bulk Action Invoice Update
	Given File with MultipleErrors is uploaded for "Invoice Update"
	When File with "MultipleErrors" submitted for "Invoice Update"
	Then Message "MultipleErrors" should appear
	
@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verfiy all fields are mapped properly on gui & Program Invoice Number is disabled
	When File with AllHeaderN2DataRows is uploaded for "Invoice Update"
	Then All fields should be displayed & Program Invoice Number should be disabled

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify unable to submit if file is not uploaded for Bulk Action Invoice Update
	When Submission is performed on "Invoice Update"
	Then Message "Upload File To Proceed" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with Max PO chars is submitted for Bulk Action Invoice Update
	Given File with MaxPOChars is uploaded for "Invoice Update"
	When File with "MaxPOChars" submitted for "Invoice Update"
	Then Message "PONumbersMaxLimit" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with Max Vehicle Id chars is submitted for Bulk Action Invoice Update
	Given File with MaxVehicleIdChars is uploaded for "Invoice Update"
	When File with "MaxVehicleIdChars" submitted for "Invoice Update"
	Then Message "VehicleIdMaxLimit" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with Max Unit Number chars is submitted for Bulk Action Invoice Update
	Given File with MaxUnitNumberChars is uploaded for "Invoice Update"
	When File with "MaxUnitNumberChars" submitted for "Invoice Update"
	Then Message "UnitNumberMaxLimit" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with Max Vin Number chars is submitted for Bulk Action Invoice Update
	Given File with MaxVinNumberChars is uploaded for "Invoice Update"
	When File with "MaxVinNumberChars" submitted for "Invoice Update"
	Then Message "VinNumberMaxLimit" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with Max Vehicle Mileage chars is submitted for Bulk Action Invoice Update
	Given File with MaxVehicleMileageChars is uploaded for "Invoice Update"
	When File with "MaxVehicleMileageChars" submitted for "Invoice Update"
	Then Message "MaxVehicleMaxLimit" should appear

@CON-26779 @Smoke @Functional @20.0 @BulkActions
Scenario: Verify file with Max Ref Number chars is submitted for Bulk Action Invoice Update
	Given File with MaxRefNumberChars is uploaded for "Invoice Update"
	When File with "MaxRefNumberChars" submitted for "Invoice Update"
	Then Message "RefNumberMaxLimit" should appear
