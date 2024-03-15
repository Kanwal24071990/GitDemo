Feature: CreateNewEntity

As a user
I want to verify
Create New Entity Functionality

Background: Load Application with given user and navigate to Create New Entity
	Given User "Admin" is on "Create New Entity" popup page

@CON-26500 
@CreateNewEntity
@Smoke @Functional @19.0
Scenario: Validate Create New Entity GUI for Buyer Billing Location 
When User select Enrollment Type as "Buyer" on Create New Entity page
Then Valid Fields are displayed on Create New Entity page


@CON-26500
@Create @CreateNewEntity
@Smoke @Functional @19.0
Scenario:Create Buyer Billing Location with default values
When "Buyer Billing" Location is created with default values
Then "Buyer Billing" Location is created successfully

@CON-26500 
@Create @CreateNewEntity
@Smoke @Functional @19.0
Scenario:Create new Buyer Billing Location enabled for Online Payment 
When "Buyer Billing" Location with Pay Online is created
Then Pay Online "Buyer Billing" Location is created successfully 

@CON-26500
@Create @CreateNewEntity
@Smoke @Functional @19.0
Scenario:Create Buyer Billing Location with default values and Tax Information
When "Buyer Billing" Location is created with default values and Tax Information
Then "Buyer Billing" Location is created with Tax Information