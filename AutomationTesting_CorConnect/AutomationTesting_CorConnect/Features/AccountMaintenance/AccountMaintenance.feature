Feature: AccountMaintenance

Background: Load Application with given user and navigate to Account Maintenance
	Given User "Admin" is on "Account Maintenance" page

@CON-26179 @CON-26500
@AccountMaintenance
@Smoke @Functional @19.0
Scenario: Validate Account Configuration GUI fields and values for Buyer Location
	When User is on "Account Configuration" tab for Enrollment Type "Buyer" and  <LocationType>
	Then Valid fields with default values should displayed for <LocationType> on "Account Configuration" tab
Examples:
	| LocationType   |
	| Master Billing |
	| Billing        |
	| Master         |
	| Shop           |
	| Parent Shop    |

@CON-26500
@Update
@Smoke @Functional @19.0
Scenario Outline: Update fleet billing location as non pay online account
	When "Buyer Billing" Location is updated to non pay online account
	Then "Buyer Billing" Location is updated to non pay online successfully

@CON-26500
@AccountMaintenance
@Smoke @Functional @19.0
Scenario: Validate Add New Location GUI fields and values for Buyer Location
	When User is on "Account Configuration" tab for Enrollment Type "Buyer" and <LocationType>
	And User switches to "New Locations" tab
	Then Valid fields with default values should displayed for <LocationType> on "New Locations" tab
Examples:
	| LocationType |
	| Shop         |
	| Parent Shop  |

@CON-26179
@AccountMaintenance
@Smoke @Functional @19.0 @AdditionalTaxFields
Scenario: Verify Data Saved for Tax ID Type and Tax Classification Fields on Account Configuration Page for Buyer Location
	When User is on "Account Configuration" tab for Enrollment Type "Buyer" and  <LocationType>
	And  User Updates data for in Tax ID Type and Tax Classification Fields for "Buyer" and  "<LocationType>"
	Then Data should be saved in Tax ID Type and Tax Classification Fields 
Examples:
	| LocationType   |
	| Master Billing |
	| Billing        |
	| Master         |
	| Shop           |
	| Parent Shop    |
