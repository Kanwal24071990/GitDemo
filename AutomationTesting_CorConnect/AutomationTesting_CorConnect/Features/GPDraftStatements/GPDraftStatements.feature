Feature: GPDraftStatements

As a User
I will verify 
Grid Column value for Program Code with Admin User

@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke
@IndSCF
Scenario: Verify Program Code Value for Subcommunity Level
     Given User "Admin" is on "GP Draft Statements" page
	 And Program CodeToken is "Active" for Program Code and "InActive" for Subcommunity
	 When User is on "GP Draft Statements" page and populate grid for "Subcommunity"
	 Then Column Program Code value is "null" for Page "GP Draft Statements"

@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke
@Daimler
Scenario: Verify Program Code Value for Entity Level
     Given User "Admin" is on "GP Draft Statements" page
	 And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	 When User is on "GP Draft Statements" page and populate grid for "Entity Level"
	 Then Column Program Code value is "PC Values" for Page "GP Draft Statements"
	
@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke
Scenario: Verify Program Code Value for Enrollment Created
    Given User "Admin" is on "GP Draft Statements" page
	And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	When User is on "GP Draft Statements" page and populate grid for "Entity Level"
	Then Column Program Code value is "Enrollment PC Value" for Page "GP Draft Statements"

