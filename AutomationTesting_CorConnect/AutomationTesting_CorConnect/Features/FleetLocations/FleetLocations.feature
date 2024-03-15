Feature: FleetLocations

As a User
I will verify 
Grid Column value for Program Code

Background: Load Application with given user and navigate
    Given User "Admin" is on "Fleet Locations" page 

@CON-25750 @ProgramCodeValidation @18.0 @Smoke @IndSCF
Scenario: Verify Program Code Value for Subcommunity Level on Fleet Locations
  	 And Program CodeToken is "Active" for Program Code and "InActive" for Subcommunity
	 When User is on "Fleet Locations" page and populate grid for "Subcommunity"
	 Then Column Program Code value is "null" for Page "Fleet Locations"
	
@CON-25750 @ProgramCodeValidation @18.0 @Smoke @Daimler
Scenario: Verify Program Code Value for Entity Level on Fleet Locations
	 And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	 When User is on "Fleet Locations" page and populate grid for "Entity Level"
	 Then Column Program Code value is "Entity level PC values" for Page "Fleet Locations"
	
@CON-25750 @ProgramCodeValidation @18.0 @Funtional @Smoke
Scenario: Verify Program Code Value for Enrollment Created on Fleet Locations
	 And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	 When User is on "Fleet Locations" page and populate grid for "Enrollment Entity"
	 Then Column Program Code value is "Enrollment PC Value" for Page "Fleet Locations"

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario:Verify user with matching subcommunity is displayed under user list of the location on Fleet Locations
    When User drills down record for Buyer "17FleetB2" Location
    Then Users with Matching subcommunity for Buyer "17FleetB2" should be displayed

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify user with non matching subcommunity is not displayed under user list of the location on Fleet Locations
    When User drills down record for Buyer "R25V1" Location
    Then User with Non-Matching subcommunity for Buyer "A17FleetB2" should not be displayed

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify Empty Grid for Locations where no users are associated yet on Fleet Locations
    When User drills down record for Buyer "18Byr1" Location
    Then Empty Grid should be displayed for Buyer Location

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify InActive Users are not displaying under user list of the location on Fleet Locations
    When User drills down record for Buyer "Buyer" Location
    Then "Inactive" users should not be displayed for Buyer Location

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify Suspended Users are not displaying under user list of the location on Fleet Locations
    When User drills down record for Buyer "USFleet" Location
    Then "Suspended" users should not be displayed for Buyer Location

