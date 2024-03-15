Feature: SixTab

As a User
I want to verify
Six tab functionality 

Background: 
	Given User "Admin" logs in

@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate existing menu item should open when same menu item opened again
	Given User is on "Fleet Statements" page
	When User opens "Fleet Statements" page again
	Then Single menu item should open for "Fleet Statements" page

@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate existing menu item should open when same menu item opened again with other menu items open
	Given User is on "Dealer Invoice Transaction Lookup" page
	And User is on new "Invoice Discrepancy" page
	When User opens "Dealer Invoice Transaction Lookup" page again
	Then Single menu item should open for "Dealer Invoice Transaction Lookup" page

@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate existing menu item should open when same menu item opened again with popup page opened
	Given User is on "Shop Summary Report" page 
	And User is on new "Price Exception Report" page and populates grid
	And User is on new popup page "Part Purchases Report" 
	And User Closes Popup page
	When User opens "Shop Summary Report" page again
	Then Single menu item should open for "Shop Summary Report" page

@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate existing menu item should open when same menu item opened again with other search grid menu items opened
	Given User is on "Invoice Discrepancy" page and populates grid
	And User is on new "Fleet Statements" page and populates grid with Search Criteria
	And User Clears Search Criteria
	When User opens "Invoice Discrepancy" page again
	Then Single menu item should open for "Invoice Discrepancy" page

@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate new menu item should open when same menu item opened again after closing old one
	Given User is on "Dealer Lookup" page and populates grid
	And User is on new "Dealer Statements" page and populates grid
	And User switches to "Dealer Lookup" menu item 
	And User closes "Dealer Lookup" page
	When User opens "Dealer Lookup" page again
	Then Single menu item should open for "Dealer Lookup" page
	 
@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate existing single grid menu item should open when same menu item opened again
	Given User is on single grid "Assign Entity Chart" page
	And User is on new "Assign Entity Function" page
	And User is on new "User Group Setup" page
	When User opens "Assign Entity Chart" page again
	Then Single menu item should open for "Assign Entity Chart" page 

@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate existing menu item should open when any menu item opened again from already opened six menu items
	Given User is on "Dealer Lookup" page 
	And User is on new "Dealer Statements" page
	And User is on new "Parts" page 
	And User is on new "Disputes" page
	And User is on new "Fleet Invoices" page
	And User is on new "Account Maintenance" page
	When User opens "Dealer Lookup" page again
	Then Max tabs error message is not displayed
	And Single menu item should open for "Dealer Lookup" page 
	 
@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate correct empty grid message is displayed when existing menu item is opened
	Given User is on "Account Maintenance" page and populates grid 
	And User is on new "Assign Entity Chart" page
	And User switches to "Account Maintenance" menu item
	And User Clears Search Criteria
	And User is on new "Assign Entity Function" page
	When User opens "Account Maintenance" page again
	Then Empty grid message is displayed 

@CON-25674 @Smoke @Functional @SixTab @18.0 @UAT
Scenario: Validate grid data is displayed when existing menu item is opened
	Given User is on single grid "Assign Entity Chart" page
	And User is on new "Assign Entity Function" page
	And User is on new popup page "Create New Entity" 
	And User Closes Popup page
	When User opens "Assign Entity Chart" page again
	Then Grid data is displayed