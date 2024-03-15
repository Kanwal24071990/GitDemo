Feature: Create Users

validate fields visible on  user creation form and create different types of  users 

Background: Load Application and navigate to Manage Users page
	Given User "Admin" is on "Manage Users" page

@CON-26334 @CON-26182 @19.0 @CON-26733 @Smoke 
Scenario Outline: GUI validation of Add New User Page for all types of users
	When Open Add New User page and select the User Type "<usertype>" and Entity Type "<entitytype>"
	Then Valid fields should be displayed on Add New User page for "<usertype>" and "<entitytype>"
	

Examples:
	| usertype        | entitytype |
	| Regular Users   | Dealer     |
	| Regular Users   | Fleet      |
	| Entity Admin    | Dealer     |
	| Entity Admin    | Fleet      |
	| Community Admin |            |
	| Super Admin     |            |


@CON-26334 @CON-26182 @19.0 @CON-26735 @Functional
Scenario: Create users of different types and languages
	When Create new user:
		| UserType        | EntityType | LocType      | Language |
		| Regular Users   | Dealer     | Shop         | English  |
		| Regular Users   | Fleet      | Billing      | French   |
		| Entity Admin    | Dealer     | Shop         | Spanish  |
		| Entity Admin    | Fleet      | Billing      | English  |
		| Entity Admin    | Dealer     | Subcommunity | Spanish  |
		| Entity Admin    | Fleet      | Subcommunity | French   |
		| Super Admin     |            |              | French   |
		| Community Admin |            |              | Spanish  |
	Then user is created 




@CON-26334 @CON-26182 @19.0 @CON-26737 @Functional
Scenario:Create notification users of different types and languages
	When Create new Notification user:
		| UserType        | EntityType | LocType      | Language |
		| Regular Users   | Dealer     | Billing      | English  |
		| Regular Users   | Fleet      | Shop         | Spanish  |
		| Entity Admin    | Dealer     | Shop         | French   |
		| Entity Admin    | Fleet      | Billing      | English  |
		| Entity Admin    | Dealer     | Subcommunity | Spanish  |
		| Entity Admin    | Fleet      | Subcommunity | French   |
		| Super Admin     |            |              | French   |
		| Community Admin |            |              | Spanish  |
	Then Notification user is created 














 