Feature: GPDraftStatements-Fleet

As a User
I will verify 
Grid Column value for Program Code with Admin User
	
@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke

Scenario: Verify Program Code Value for Subcommunity Level Fleet User
     Given User "Fleet" is on "GP Draft Statements" page
	 And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	 When User is on "GP Draft Statements" page and populate grid for "Subcommunity"
	 Then Column Program Code value is "null" for Page "GP Draft Statements"
	
@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke

Scenario: Verify Program Code Value for Entity Level Fleet User
     Given User "Fleet" is on "GP Draft Statements" page
	 And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	 When User is on "GP Draft Statements" page and populate grid for "Subcommunity"
	 Then Column Program Code value is "Entity level PC values" for Page "GP Draft Statements"
	
@ProgramCodeValidation
@CON-25750
@18.0
@Funtional
@Smoke

Scenario: Verify Program Code Value for Enrollment Created Fleet User
      Given User "Fleet" is on "GP Draft Statements" page
	  And Program CodeToken is "Active" for Program Code and "Inactive" for Subcommunity
	  When User is on "GP Draft Statements" page and populate grid for "Subcommunity"
	  Then Column Program Code value is "Enrollment PC Value" for Page "GP Draft Statements"