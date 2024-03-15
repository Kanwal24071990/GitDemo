Feature: CreateDispute

As a User
I want to verify Dispute Creation flow

Background: Load Application with given user and navigate
	Given User "Admin" is on "Fleet Invoice Transaction Lookup" page

@CreateDispute
Scenario Outline: Create Dispute with All Given Reasons
	When Dispute is created with "<Reason>" for invoice ""
	Then Invoice "" should be disputed successfully

Examples:
	| Reason                               |
	| Pricing Error                        |
	| Bad PO                               |
	| Need More Info                       |
	| Duplicate Invoice                    |
	| Not Our Invoice                      |
	| Invalid Charge                       |
	| Other                                |
	| Tax Exempt                           |
	| Does not match Quoted Contract Price |
	| Warranty                             |
	| Discount Amount not correct          |
	| Need Delivery Receipt                |
