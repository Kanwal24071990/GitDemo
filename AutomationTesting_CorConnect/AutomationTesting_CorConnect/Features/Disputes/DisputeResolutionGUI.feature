Feature: DisputeResolutionGUI

A short summary of the feature

@CON-25856
@InvoiceOptions
@Smoke  @18.0 @UAT
Scenario Outline: Validate Navigation to Invoice Options for Disputed Inv
	Given User "<User>" is on "Disputes" page
	When User clicks on "Disputed Invoice" hyperlink from search grid
	Then User should land on Invoice Options window "Dispute Resolution" tab

Examples:
	| User   |
	| Admin  |
	| Fleet  |
	| Dealer |

@CON-25856
@InvoiceOptions
@Smoke @18.0 @UAT
Scenario: Validate Navigation to Invoice Options for Dispute Resolved Inv
	Given User "<User>" is on "Disputes" page
	When User clicks on "Dispute Resolved Inv" hyperlink from search grid
	Then User should land on Invoice Options window "Dispute Info" tab

Examples:
	| User  |
	| Admin |
	| Fleet |

@CON-25856
@InvoiceOptions
@Smoke @18.0 @UAT
Scenario: Validate Dlr Usr Navigation to Invoice Options for Dispute Resolved Inv
	Given User "Dealer" is on "Disputes" page
	When User clicks on "Dispute Resolved Inv" hyperlink from search grid
	Then User should land on Invoice Options window "Dispute Resolution" tab

@CON-25979
@DisputeGUI
@Smoke @18.0 @UAT
Scenario: Validate Create Dispute GUI
	Given User "<User>" is on "Fleet Invoices" page
	When User goes to Invoice Options window for "Dispute Creation" on "Fleet Invoices" page
	Then Valid Fields should be displayed for "Current Invoice"


Examples:
	| User  |
	| Admin |
	| Fleet |

@CON-25979
@DisputeGUI
@Smoke @18.0 @UAT
Scenario: Validate Update Dispute GUI
	Given User "<User>" is on "<PageName>" page
	When User goes to Invoice Options window for "Dispute Updation" on "<PageName>" page
	Then Valid Fields should be displayed for "Disputed Invoice"

Examples:
	| User  | PageName       |
	| Admin | Disputes       |
	| Admin | Fleet Invoices |
	| Fleet | Disputes       |
	| Fleet | Fleet Invoices |

@CON-25979
@DisputeGUI
@Smoke @18.0 @UAT
Scenario: Validate Dispute Resolution GUI
	Given User "<User>" is on "<PageName>" page
	When User goes to Invoice Options window for "Resolving Dispute" on "<PageName>" page
	Then Valid Fields should be displayed for "Dispute Resolution"

Examples:
	| User   | PageName       |
	| Admin  | Disputes       |
	| Admin  | Fleet Invoices |
	| Fleet  | Disputes       |
	| Fleet  | Fleet Invoices |
	| Dealer | Disputes       |



@CON-25979
@DisputeGUI
@Smoke @18.0 @UAT
Scenario: Validate Re-Dispute GUI
	Given User "<User>" is on "<PageName>" page
	When User goes to Invoice Options window for "ReDispute" on "<PageName>" page
	Then Valid Fields should be displayed for "ReDispute"

Examples:
	| User  | PageName       |
	| Admin | Disputes       |
	| Admin | Fleet Invoices |
	| Fleet | Disputes       |
	| Fleet | Fleet Invoices |
