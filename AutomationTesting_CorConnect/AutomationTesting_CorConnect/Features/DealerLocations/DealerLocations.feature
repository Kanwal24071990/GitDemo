Feature: DealerLocations

As a User
I will verify 
Grid Column value for Program Code

Background: Load Application with given user and navigate
    Given User "Admin" is on "Dealer Locations" page

@CON-25750 @ProgramCodeValidation @18.0 @Smoke @IndSCF
Scenario: Verify Program Code Value for Subcommunity Level on Dealer Locations
	  And Program CodeToken is "Active" for Program Code and "InActive" for Subcommunity
	  When User is on "Dealer Locations" page and populate grid for "Subcommunity"
	  Then Column Program Code value is "null" for Page "Dealer Locations"
	
@CON-25750 @ProgramCodeValidation @18.0 @Smoke @Daimler
Scenario: Verify Program Code Value for Entity Level on Dealer Locations
	  And Program CodeToken is "Active" for Program Code and "InActive" for Subcommunity
	  When User is on "Dealer Locations" page and populate grid for "Entity Level"
	  Then Column Program Code value is "Entity level PC values" for Page "Dealer Locations"
	
@CON-25750 @ProgramCodeValidation @18.0 @Funtional @Smoke
Scenario: Verify Program Code Value for Enrollment Created on Dealer Locations
	 And Program CodeToken is "Active" for Program Code and "InActive" for Subcommunity
	 When User is on "Dealer Locations" page and populate grid for "Enrollment Entity"
	 Then Column Program Code value is "Enrollment PC Value" for Page "Dealer Locations"

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify user with matching subcommunity is displayed under user list of the location on Dealer Locations
    When User drills down record for "AssociateDlr" Location
    Then User with Matching subcommunity for "AssociateDlr" should be displayed

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify user with non matching subcommunity is not displayed under user list of the location on Dealer Locations
    When User drills down record for "uatdealertest12" Location
    Then User with Non-Matching subcommunity for "AssociateDlr" should not be displayed

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify Empty Grid for Locations where no users are associated yet on Dealer Locations
    When User drills down record for "Sup13" Location
    Then Empty Grid should be displayed


@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify InActive Users are not displaying under user list of the location on Dealer Locations
    When User drills down record for "Seller" Location
    Then "Inactive" users should not be displayed

@CON-25400 @Regression @Functional @UserAccess @19.0
Scenario: Verify Suspended Users are not displaying under user list of the location on Dealer Locations
    When User drills down record for "19SupH" Location
    Then "Suspended" users should not be displayed


