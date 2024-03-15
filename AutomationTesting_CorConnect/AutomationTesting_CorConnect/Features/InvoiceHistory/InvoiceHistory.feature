Feature: InvoiceHistory

Validating Invoice History grid for different invoice operations
Background: 
Given User "Admin" is on "Dealer Invoice Transaction Lookup" page

@CON-27653 @InvoiceHistory @Functional @20.0 @NoUAT
Scenario: Verify record is captured in Invoice History grid upon invoice reversal
    Given Invoice exists between buyer "InvoiceHistoryByr" and supplier "InvoiceHistorySup"
	When The invoice is reversed
	Then "Reverse" operation is captured in Invoice History grid

@CON-27653 @InvoiceHistory @Functional @20.0 @NoUAT
Scenario: Verify record is captured in Invoice History grid upon invoice rebill
	Given Invoice exists between buyer "InvoiceHistoryByr" and supplier "InvoiceHistorySup"
	When The invoice is rebilled
	Then "Rebill" operation is captured in Invoice History grid

@CON-27653 @InvoiceHistory @Functional @20.0 @NoUAT
Scenario: Verify record is captured in Invoice History grid upon bulk invoice reversal
	Given Invoice created with Bulk Reversal exists in the system 
	When The invoice is opened
	Then "Bulk Reversal" operation is captured in Invoice History grid


