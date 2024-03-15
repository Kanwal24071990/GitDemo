Feature: Sub Community Program Code

As a User
I will verify 
Grid Column value for Program Code by Subcommunity

@ProgramCodeValidationbySubcommunity
@CON-25750
@18.0
@Funtional
@Smoke
@IndSCF
Scenario: Verify Program Code Token Value for Subcommunity Level Assignment for Pages
    Given User "Admin" is on "<Page>" page
	And Program CodeToken is "Active" for Program Code and "Active" for Subcommunity
	When User is on "<Page>" page and populate grid for "Subcommunity"
	Then Column Program Code value is "PC Values" for Page "<Page>"
	Examples: 
	| Page                               |
	| Dealer Locations                   |
	| Fleet Locations                    | 
	| GP Draft Statements                |
	| Draft Statement Report             |

@ProgramCodeValidationbySubcommunity
@CON-25750
@18.0
@Funtional
@Smoke
Scenario: Verify Program Code Token Value for Subcommunity Level Assignment for Enrollment Page
    Given User "Admin" is on "Enrollment" popup page
	And Program CodeToken is "Active" for Program Code and "Active" for Subcommunity
	When User is on "Enrollment" page and populate grid for "Subcommunity"
	Then Column Program Code value is "PC Values" for Page "Enrollment"
	
	

	

	
	
	
	

