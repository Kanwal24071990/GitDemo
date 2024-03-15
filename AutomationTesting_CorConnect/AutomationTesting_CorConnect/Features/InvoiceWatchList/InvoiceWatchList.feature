Feature: Invoice Watch List

As a user
I want to verify
Create Invoice Watch List page Multiselect dropdowns

Background: 
	Given User "Admin" is on "Invoice Watch List" page

@CON-25184 @CreateInvoiceWatchList @Sanity @18.0 @UAT
Scenario: Validate multiselect Dealer dropdown UI
	When User is on Create Invoice Watch List page
	Then "Dealer" dropdown is verified

@CON-25184 @CreateInvoiceWatchList @Sanity @18.0 @UAT
Scenario: Validate multiselect Fleet dropdown UI
	When User is on Create Invoice Watch List page
	Then "Fleet" dropdown is verified

@CON-25184 @CreateInvoiceWatchList @Sanity @18.0
Scenario: Validate multiple rows selection on Dealer dropdown
	When User is on Create Invoice Watch List page
	Then User can select 2 records from "Dealer" dropdown

@CON-25184 @CreateInvoiceWatchList @Sanity @18.0
Scenario: Validate multiple rows selection on Fleet dropdown
	When User is on Create Invoice Watch List page
	Then User can select 2 records from "Fleet" dropdown








