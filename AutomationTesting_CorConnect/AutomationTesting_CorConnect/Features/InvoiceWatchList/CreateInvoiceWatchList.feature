Feature: Create Invoice Watch List

As a user
I want to verify
Create Invoice Watch List Functionality

Background: 
	Given User "Admin" is on "Invoice Watch List" page
	And User is on Create Invoice Watch List page

@CON-25184 @CreateInvoiceWatchList @Functional @Create @18.0 @UAT
Scenario: Validate invoice watch list creation with active locations
	When Invoice watch list is created with "active" locations
	Then Invoice watch list record is created successfully

@CON-25184 @CreateInvoiceWatchList @Functional @Create @18.0
Scenario: Validate invoice watch list creation with inactive locations
	When Invoice watch list is created with "inactive" locations
	Then Invoice watch list record is created successfully

@CON-25184 @CreateInvoiceWatchList @Functional @Create @18.0
Scenario: Validate invoice watch list creation with terminated locations
	When Invoice watch list is created with "terminated" locations
	Then Invoice watch list record is created successfully

@CON-25184 @CreateInvoiceWatchList @Functional @Create @18.0
Scenario: Validate invoice watch list creation with duplicate locations
	When Invoice watch list is created with "duplicate" locations
	Then Duplicate entries message is verified

@CON-25184 @CreateInvoiceWatchList @Functional @Create @18.0 @UAT
Scenario: Validate invoice watch list creation with 200 locations
	When Invoice watch list is created using 200 locations
	Then Duplicate entries message is verified

@CON-25184 @CreateInvoiceWatchList @Functional @Create @18.0
Scenario: Validate invoice watch list update
	When User updates invoice watch list
	Then Invoice watch list record is updated successfully



