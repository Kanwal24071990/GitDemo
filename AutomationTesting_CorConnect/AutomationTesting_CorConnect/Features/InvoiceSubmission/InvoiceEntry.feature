Feature: InvoiceEntry

As a User
I will verify 
Invoice Entry for Admin User


Background: Load Application with given user and navigate
Given User "Admin" is on "Invoice Entry" page


@CON-16798 @Functional @Regression
@InvoiceEntry @InvoiceSubmission @InvoiceEntry
Scenario: Expiration date calculation for invoice created from UI and invoice validity on relationship is greater than Community table
    Given Invoice validity setup is 4 days 
	When Invoice of type "Parts" is created using Supplier "19Sup02" and Buyer "19Byr02" with current transaction date
	Then Invoice should be settled successfully with expiration date null

@CON-16798 @Functional @Regression
@InvoiceSubmission @DMS
Scenario: Expiration date calculation for invoice created from DMS and invoice validity on relationship is greater than Community table
	Given Invoice validity setup is 4 days 
	When Invoice is submitted from DMS with Buyer "19Byr02" and Supplier "19Sup02" with current transaction date
	Then Invoice should be settled successfully with expiration date null

@CON-16799 @Functional @Regression
@InvoiceSubmission @DMS
Scenario: Expiration date calculation for invoice created from DMS with back date greater than community level
	Given Invoice validity setup is 4 days 
	When Invoice of transaction type "Parts" is submitted from DMS with Supplier "SupBlgUSD" and Buyer "ByrblgUSD" with transaction date -5 days from current date
	Then Invoice should be in discrepancy with error "Transaction date is invalid" for buyer "ByrblgUSD" and supplier "SupBlgUSD"
	And Invoice expiration date is +4 days to transaction date


@CON-16680 @Functional @Regression
@InvoiceSubmission @DMS
Scenario: Expiration date calculation for invoice created from DMS with back date equal to invoice validity relationship
	Given Invoice validity setup is 5 days	
	When Invoice of transaction type "Parts" is submitted from DMS with Supplier "19Sup03" and Buyer "19Byr03" with transaction date -8 days from current date
	Then Invoice should be settled successfully with no errors

@CON-16680 @Functional @Regression
@InvoiceSubmission @DMS
Scenario: Expiration date calculation for invoice submitted from DMS with back date greater than invoice validity relationship
	Given Invoice validity setup is 5 days
	When Invoice of transaction type "Parts" is submitted from DMS with Supplier "19Sup03" and Buyer "19Byr03" with transaction date -9 days from current date
	Then Invoice should be in discrepancy with error "Transaction date is invalid" for buyer "19Byr03" and supplier "19Sup03"
	And Invoice expiration date is +8 days to transaction date


@CON-16680 @Functional @Regression
@InvoiceSubmission @DMS
Scenario: Expiration date calculation for credit invoice submission from DMS with back date equal to invoice validity relationship
	Given Invoice validity setup is 4 days 
	When Invoice is submitted from DMS with buyer "19Byr03" and supplier "19Sup03" with transaction amount -100 and transaction date -8 from current date
	Then Invoice should be in discrepancy with error "Transaction date is invalid" for buyer "19Byr03" and supplier "19Sup03"
	And Invoice expiration date is +4 days to transaction date

@CON-16680 @Functional @Regression
@InvoiceSubmission @DMS
Scenario: Expiration date calculation for credit invoice submission from DMS with back date equal to community table with relationship available as invoice type
	Given Invoice validity setup is 4 days 
	When Invoice is submitted from DMS with buyer "19Byr03" and supplier "19Sup03" with transaction amount -100 and transaction date -4 from current date
	Then Invoice should be in discrepancy with error "Transaction date is invalid" for buyer "19Byr03" and supplier "19Sup03"
	And Invoice expiration date is +4 days to transaction date


@CON-16680 @Functional @Regression
@InvoiceSubmission @DMS
Scenario: Expiration date calculation for invoice submitted from DMS with transaction type other than parts with back date greater than invoice validity relationship
	Given Invoice validity setup is 4 days 
	When Invoice of transaction type "Service" is submitted from DMS with Supplier "19Sup03" and Buyer "19Byr03" with transaction date -8 days from current date
	Then Invoice should be in discrepancy with error "Transaction date is invalid" for buyer "19Byr03" and supplier "19Sup03"


@CON-16680 @Functional @Regression
@InvoiceSubmission @DMS
Scenario: Expiration date calculation for invoice submitted from DMS with transaction type other than parts with back date equal to invoice validity relationship
	Given Invoice validity setup is 4 days 
	When Invoice of transaction type "Service" is submitted from DMS with Supplier "19Sup03" and Buyer "19Byr03" with transaction date -4 days from current date
	Then Invoice should be settled successfully with no errors

@CON-26500 @Functional
@InvoiceSubmission @DMS @19.0
Scenario: Verify invoice eligible for online payment when fleet billing location has Enable Payment Portal checkbox checked
When Invoice is submitted from DMS with Buyer "19fbm1s1" and Supplier "19dbm1s1" with current transaction date
Then Invoice marked as eligible for Online Payment

@CON-26500 @Functional
@InvoiceSubmission @DMS @19.0
Scenario: Verify invoice not eligible for online payment when fleet billing location has Enable Payment Portal checkbox unchecked
When Invoice is submitted from DMS with Buyer "12.12_akfleet" and Supplier "12.12_akdealer" with current transaction date
Then Invoice not marked as eligible for Online Payment