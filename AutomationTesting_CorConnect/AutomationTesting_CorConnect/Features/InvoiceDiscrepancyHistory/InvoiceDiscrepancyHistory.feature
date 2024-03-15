Feature: InvoiceDiscrepancyHistory-Dealer

As a User
I will verify 
Invoice Discrepancy History for Admin User

Background: Load Application with given user and navigate
Given User "Admin" is on "Invoice Discrepancy History" page


@CON-16603 @Functional @Regression
@InvoiceDiscrepancyHistory
Scenario: Validate support user authority to move invoices out of history
Given Invoice exist in system with "19ByrH" Buyer and "19SupH" Supplier with "Not in balance" Discrepancy 
And Invoice is in history	
When User select "Non-Expired" invoice to move out of history
Then "Non-Expired" Invoice should be successfully move out of history


@CON-16606 @Functional @Regression
@InvoiceDiscrepancyHistory 
Scenario: Validate support user authority to move expired invoices out of history
Given Invoice exist in system with "19ByrH" Buyer and "19SupH" Supplier with "Not in balance" Discrepancy 
And Invoice is in history
When User select "Expired" invoice to move out of history
Then "Expired" Invoice should be successfully move out of history
