Feature: DealerLocations-Dealer

As a User
I will verify 
Grid Column value for Program Code


@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke
Scenario: Verify Program Code Value for Subcommunity Level Dealer User
     Given User "Dealer" is on "Dealer Locations" page
	 And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	 When User is on "Dealer Locations" page and populate grid for "Subcommunity"
	 Then Column Program Code value is "null" for Page "Dealer Locations"
	
@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke
Scenario: Verify Program Code Value for Entity Level Dealer User
     Given User "Dealer" is on "Dealer Locations" page
	 And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	 When User is on "Dealer Locations" page and populate grid for "Entity Level"
	 Then Column Program Code value is "Entity level PC values" for Page "Dealer Locations"
	
@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke
Scenario: Verify Program Code Value for Enrollment Created Dealer User
     Given User "Dealer" is on "Dealer Locations" page
	 And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	 When User is on "Dealer Locations" page and populate grid for "Enrollment Entity"
	 Then Column Program Code value is "Enrollment PC Value" for Page "Dealer Locations"
