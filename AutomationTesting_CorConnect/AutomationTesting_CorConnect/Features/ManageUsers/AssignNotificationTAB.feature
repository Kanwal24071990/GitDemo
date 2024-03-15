Feature: AssignNotificationTAB

As a User
I will Verify
Location Saved and Unlink Selection on Assign Notificatio TAB
	
@CON-18601
@18.0
@Functional
@Smoke
Scenario: Verify Locations are saved through select all option for Notification with Configuration
     Given User "Admin" is on "Manage Users" page
	 And User Populate Grid for "<User>"
     When <Locations> locations are saved using select all option for "Fee Report"
     Then Verify for <User> locations <Locations>  are saved

	Examples: 
	| User   | Locations | 
	| Admin  | 10        | 
	| Admin  | 200       | 
	
@CON-18601
@18.0
@Functional
@Smoke
Scenario: Verify Locations are saved through select all option for Discrepancy Notification without Configuration
     Given User "Admin" is on "Manage Users" page
	 And User Populate Grid for "<User>"
     When <Locations> locations gets saved using select all option for "Discrepancy Report"
     Then Verify for <User> locations <Locations>  gets saved

	Examples: 
	| User   | Locations | 
	| Admin  | 10        | 
	| Admin  | 200       | 
	
@CON-18601
@18.0
@Functional
@Smoke
Scenario: Verify Locations are Linked when navigated to UnLinked Assign Locations with Configuration
     Given User "Admin" is on "Manage Users" page
	 And User Populate Grid for "01818-01"
     When User Navigates to Link Assigned Locations for "Assign Notifications"
     Then Checkboxes are Enabled and Link Assign Button are disbaled 

@CON-18601
@18.0
@Functional
@Smoke
Scenario: Verify Locations are Unlinked when navigated to Linked Assign Locations without Configuration
     Given User "Admin" is on "Manage Users" page
	 And User Populate Grid for "01818-01"
     When User Navigates to Linked Assign Locations for "Assign Notifications"
     Then Checkboxes are Enabled and Link Assign Button are disbaled 
	
@CON-18601
@18.0
@Functional
@Smoke
@Remove
Scenario: Verify Unlinked Locations are saved when navigated to Linked Assign Locations
     Given User "Admin" is on "Manage Users" page
	 And User Populate Grid for "01818-01"
     When User Navigates to Link Assigned Locations for "Assign Notifications" and save Unlink Locations
    Then Checkboxes and Link Assign Button are disbaled
	
@CON-18601
@18.0
@Functional
@Smoke
Scenario: Verify Save Locations when Clicks on Unlinked Assign Locations without Configuration
     Given User "Admin" is on "Manage Users" page
	 And User Populate Grid for "01818-01"
     When User Clicks on Save Location for Unlink Assign Locations in "Assign Notifications"
     Then Checkboxes are Unchecked and Link Assign Button is Enabled 

@CON-18601
@18.0
@Functional
@Smoke
Scenario: Verify Save Locations when Clicks on to Linked Assign Locations with Configuration
     Given User "Admin" is on "Manage Users" page
	 And User Populate Grid for "01818-01"
     When User Navigates to Link Assigned Locations for "Assign Notifications"
     Then Checkboxes are Enabled and Link Assign Button are disbaled 

@CON-18601
@18.0
@Functional
@Smoke
Scenario: Verify Reset when User is on UnLinked Assign Locations without Configuration
     Given User "Admin" is on "Manage Users" page
	 And User Populate Grid for "01818-01"
     When User Clicks on Reset to UnLinked Assign Locations for "Assign Notifications"
     Then Checkboxes are Unchecked and Link Assign Button is Enabled
