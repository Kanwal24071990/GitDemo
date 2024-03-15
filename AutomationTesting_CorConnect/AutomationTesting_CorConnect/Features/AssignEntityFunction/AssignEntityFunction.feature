Feature: AssignEntityFunction

Background: Load Application with given user and navigate to Assign Entity Function
	Given User "Admin" is on "Assign Entity Function" page

	
@CON-26867
@AssignEntityFunction
@Smoke @Functional @20.0 @NoUAT 
Scenario: Validate Assigned functions permission in the relevant access group
	#Picking all the functions from a file passed in step defination 
	When user "Admin" Search for an entity functions in access group
	Then Verify that entity functions is visible in the relevant accesss group


@CON-26867
@AssignEntityFunction
@Smoke @Functional @20.0 @NoUAT 
Scenario: Assigned Entity function is accessible to User group
	# This is to exercise both assign and unassign function 
	Given Usergroup <AccessGroup> have <FunctionName> function assgined
	When user "Admin" Search <AccessGroup> on "User Group Setup" page
	Then <FunctionName> function should be available for user group <AccessGroup> on "User Group Setup" page
Examples:
	| AccessGroup | FunctionName |
	| Community   | Contact Us   |