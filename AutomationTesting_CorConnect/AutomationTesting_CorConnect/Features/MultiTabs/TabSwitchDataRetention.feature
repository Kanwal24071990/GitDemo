Feature: TabSwitchDataRetention

As a User 
I will verify
Data while Tab switches 


@tag1
Scenario: Verify Data should Retain After Tab Switch
	Given User "Admin" logs in
	And User navigates to "Part Sales by Shop Report" page
	And Loads data on "Part Sales by Shop Report" page
	And User navigates to "Aged Invoice Report" page
	And Loads data on "Aged Invoice Report" page
	And User navigates to "Intercommunity Invoice Report" page
	And Loads data on "Intercommunity Invoice Report" page
	And User navigates to "Pending Billing Management Report" page
	And Loads data on "Pending Billing Management Report" page
	And User navigates to "Assign User Charts" page
	And Loads data on "Assign User Charts" page
	And User navigates to "Shop Summary Report" page
	And User Populate Grid
	And User switches tab to "Part Sales by Shop Report" page
	And Verify data on right grid
	And User switches tab to "Aged Invoice Report" page
	And Verify data on right grid
	And User switches tab to "Intercommunity Invoice Report" page
	And Verify data on right grid
	And User switches tab to "Pending Billing Management Report" page
	And Verify data on right grid
	And User switches tab to "Assign User Charts" page
	And Verify data on right grid
	And User switches tab to "Shop Summary Report" page
	And Verify data on right grid

Scenario: Verify Data should Retain After Tab Closed
	Given User "Admin" logs in
	And User navigates to "Part Sales by Shop Report" page
	And Loads data on "Part Sales by Shop Report" page
	And User navigates to "Aged Invoice Report" page
	And Loads data on "Aged Invoice Report" page
	And User navigates to "Intercommunity Invoice Report" page
	And Loads data on "Intercommunity Invoice Report" page
	And User navigates to "Pending Billing Management Report" page
	And Loads data on "Pending Billing Management Report" page
	And User navigates to "Assign User Charts" page
	And Loads data on "Assign User Charts" page
	And User navigates to "Shop Summary Report" page
	And User Populate Grid
	And User closes Tab "Shop Summary Report" page
	And Verify data on right grid
