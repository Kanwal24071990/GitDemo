Feature: UserGroupSetup

Background: Load Application with given user and navigate to User Group Setup
	
@CON-26867
@UserGroupSetup
@NoUAT @Functional @20.0
Scenario: Bulk Action function is accessible to user
	Given Username "TestFullAccessUser" is on "User Group Setup" page
	And User group - <Usergroup> have <FunctionName> function full access
	When user "TestFullAccessUser" navigates to "Bulk Actions" page under Support Menu
	Then <FunctionName> pop up page should be loaded for the "TestFullAccessUser" user and submission is allowed
Examples:
	| FunctionName | Usergroup          |
	| Bulk Actions | TestFullBulkAccess |
 

 
@CON-26867
@UserGroupSetup
@NoUAT @Functional @20.0
Scenario: Ensure that Bulk Action page link is not visible in support menu
	Given Username "TestNoAccessUser" is on "User Group Setup" page
	And User group - <Usergroup> do not have <FunctionName> function access
	When user "TestNoAccessUser" navigates to "Bulk Actions" page under Support Menu
	Then <FunctionName> menu item should not visible to "TestNoAccessUser" user under "Support" menu
Examples:
	| FunctionName | Usergroup                 |
	| Bulk Actions | TestGroupWithNoBulkAccess |


@CON-26867
@UserGroupSetup
@NoUAT @Functional @20.0
Scenario: Bulk Action function with readonly access to user
	Given Username "TestReadOnlyAccessUser" is on "User Group Setup" page
	And User group - <Usergroup> have <FunctionName> function readonly access
	When user "TestReadOnlyAccessUser" navigates to "Bulk Actions" page under Support Menu
	Then <FunctionName> pop up page should be loaded for "TestReadOnlyAccessUser" user with submission disabled
Examples:
	| FunctionName | Usergroup          |
	| Bulk Actions | TestFullBulkAccess |

@CON-26867
@UserGroupSetup
@NoUAT @Functional @20.0
Scenario: Bulk Action - sub function is accessible to user
	Given Username "TestFullAccessUser" is on "User Group Setup" page
	And User group - <Usergroup> have <BulkActionChildFunctions> function full access
	When user "TestFullAccessUser" navigates to "Bulk Actions" page under Support Menu
	Then "Bulk Actions" pop up page should be loaded for the "TestFullAccessUser" user and submission is allowed for <BulkActionChildFunctions>
Examples:
	| BulkActionChildFunctions        | Usergroup          |
	| Bulk Actions - Dispute Creation | TestFullBulkAccess |
	| Bulk Actions - Invoice Update   | TestFullBulkAccess |
	| Bulk Actions - Invoice Reversal | TestFullBulkAccess |
	| Bulk Actions - Resend Invoices  | TestFullBulkAccess |
	| Bulk Actions - Update Credit    | TestFullBulkAccess |

@CON-26867
@UserGroupSetup
@NoUAT @Functional @20.0
Scenario: Bulk Action - sub function with readonly access to user
	Given Username "TestReadOnlyChildUser" is on "User Group Setup" page
	And User group - <Usergroup> have <BulkActionChildFunctions> function readonly access
	When user "TestReadOnlyChildUser" navigates to "Bulk Actions" page under Support Menu
	Then "Bulk Actions" pop up page should be loaded for "TestReadOnlyAccessUser" user with submission disabled for <BulkActionChildFunctions>
Examples:
	| BulkActionChildFunctions        | Usergroup                        |
	| Bulk Actions - Dispute Creation | ChildFunctionsWIthReadOnlyAccess |
	| Bulk Actions - Invoice Update   | ChildFunctionsWIthReadOnlyAccess |
	| Bulk Actions - Invoice Reversal | ChildFunctionsWIthReadOnlyAccess |
	| Bulk Actions - Resend Invoices  | ChildFunctionsWIthReadOnlyAccess |
	| Bulk Actions - Update Credit    | ChildFunctionsWIthReadOnlyAccess |
	
@CON-26867
@UserGroupSetup
@NoUAT @Functional @20.0
Scenario: Bulk Action - sub function is not accessible to user
	Given Username "TestRestrictChildAccessUser" is on "User Group Setup" page
	And User group - <Usergroup> do not have <BulkActionChildFunctions> function access
	When user "TestRestrictChildAccessUser" navigates to "Bulk Actions" page under Support Menu
	Then "Bulk Action" pop up page should be loaded for "TestRestrictChildAccessUser" user without <BulkActionChildFunctions> in the dropdown
Examples:
	| BulkActionChildFunctions        | Usergroup                   |
	| Bulk Actions - Dispute Creation | RestrictChildFunctionAccess |
	| Bulk Actions - Invoice Update   | RestrictChildFunctionAccess |
	| Bulk Actions - Invoice Reversal | RestrictChildFunctionAccess |
	| Bulk Actions - Resend Invoices  | RestrictChildFunctionAccess |
	| Bulk Actions - Update Credit    | RestrictChildFunctionAccess |
 