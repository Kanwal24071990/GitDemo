Feature: FleetInvoices

As a User
I want to verify DateRange dropdown options
I want to verify Search by all options of DateRange dropdown

Background: Load Application with given user and navigate
	Given User "Admin" is on "Fleet Invoices" page

#TASK CON-20036: Last 185 days Date Range Search Option

@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify DateRange dropdown values on FleetInvoices
    Then Dropdown "Date Range" should have valid values on "Fleet Invoices" page


@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 7 Days on FleetInvoices
	When Advanced Search by DateRange value "Last 7 days"
	Then Data for "Last 7 days" is shown on the results grid

	 
@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 14 Days on FleetInvoices
	When Advanced Search by DateRange value "Last 14 days"
	Then Data for "Last 14 days" is shown on the results grid

@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 185 Days on FleetInvoices
	When Advanced Search by DateRange value "Last 185 days"
	Then Data for "Last 185 days" is shown on the results grid
	
@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Current month on FleetInvoices
	When Advanced Search by DateRange value "Current month"
	Then Data for "Current month" is shown on the results grid

@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last month on FleetInvoices
	When Advanced Search by DateRange value "Last month"
	Then Data for "Last month" is shown on the results grid

@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Customized date on FleetInvoices
	When Advanced Search by DateRange value "Customized date"
	Then Data for "Customized date" is shown on the results grid

@CON-20036 @Functional @Smoke @FleetInvoices @18.0 @UAT
Scenario: Verify the Last 185 days date range message on Advanced Search on FleetInvoices
	When User navigates to Advanced Search
	Then The message "Advanced Search will only allow 185 days date range" is shown
	 
@CON-20036 @Functional @Smoke @FleetInvoices @18.0 @UAT
Scenario: Verify the Date Range message when user selects FROM date greater than 185 days on FleetInvoices
	When User selects From date greater than 185 days on Advanced Search
	Then The message Date Range cannot exceed 185 days is shown as a tooltip

@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify that selecting Last 7 days date range option sets the correct From and To dates on FleetInvoices
	When User selects "Last 7 days" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Last 7 days" option

@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify that selecting Last 14 days date range option sets the correct From and To dates on FleetInvoices
	When User selects "Last 14 days" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Last 14 days" option

@CON-20036 @Functional @Smoke @FleetInvoices @18.0 @UAT
Scenario: Verify that selecting Last 185 days date range option sets the correct From and To dates on FleetInvoices
	When User selects "Last 185 days" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Last 185 days" option

@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify that selecting Current month date range option sets the correct From and To dates on FleetInvoices
	When User selects "Current month" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Current month" option

@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify that selecting Last month date range option sets the correct From and To dates on FleetInvoices
	When User selects "Last month" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Last month" option
	 
@CON-20036 @Functional @Smoke @FleetInvoices @18.0
Scenario: Verify that selecting Customized date date range option sets the correct From and To dates on FleetInvoices
	When User selects "Customized date" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Customized date" option

	
@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify Error upon Pay Invoices with Multiple Currencies and different Subcommunity
    And Invoice is submitted from DMS with Buyer "SMBuyer" Seller "SMSupplier" and Buyer "MultiBuyerCAD" Seller "MutliSupplierCAD"
	When User Populates Grid and Initiates Payment for invoice belong to Buyer "SMBuyer" and "MultiBuyerCAD"
	Then Error message "Please select invoices with single currency type for payment. Cannot process payment with multiple currencies." should be displayed

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify Error upon Pay Invoices with Multiple Currencies and same Subcommunity
    And Invoice is submitted from DMS with Buyer "SMBuyer" Seller "SMSupplier" and Buyer "MultiBuyer" Seller "MultiSupplier"
	When User Populates Grid and Initiates Payment for invoice belong to Buyer "SMBuyer" and "MultiBuyer"
	Then Error message "Please select invoices with single currency type for payment. Cannot process payment with multiple currencies." should be displayed
	

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify successful payment initiation by selecting invoices with Same Currecies and Same Subcommunity
    And Invoice is submitted from DMS with Buyer "191Smkf1" Seller "191Smkd1" and Buyer "SMBuyer" Seller "SMSupplier"
	When User Populates Grid and Initiates Payment for invoice belong to Buyer "191Smkf1" and "SMBuyer"
    Then On Success the "Invoices" Status is Initiated and Paymeny Portal is launched

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify Error upon Pay Invoices with Same Currencies and different Subcommunity
   And Invoice is submitted from DMS with Buyer "SMBuyer" Seller "SMSupplier" and Buyer "SMCurrencyB" Seller "SMCurrencyS"
	When User Populates Grid and Initiates Payment for invoice belong to Buyer "SMBuyer" and "SMCurrencyB"
	Then Error message "Buyer Billing Locations must be in the same sub-community" should be displayed 

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify successful payment initiation by selecting invoice with Single Currency  
    And  User Populates PayOnline Eligible Invoices from Past "180" days with single Currency "<Currency>"
	When User initiates Payment for invoices belong to Single Currency "<Currency>"
	Then On Success the "Invoice" Status is Initiated and Paymeny Portal is launched

	Examples: 
	| Currency |
	| USD      |
	| CAD      |
	| Euro     |

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify successful payment initiation by selecting invoice with Single Currecny and Transaction Types
    And Fresh Invoices are submitted from DMS having Transaction Type as "<Type>"
	When User Populates Grid and Initiates Payment for invoice belong to Buyer 
	Then On Success the "Invoice" Status is Initiated and Paymeny Portal is launched

	Examples: 
	| Type          |
	| Service       |
	| Miscellaneous |
    | Fixed         |

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify successful payment initiation by selecting invoice with Credit Invoice Checked and Single Currency 
    And Fresh Invoices are submitted from DMS with Buyer "SMBuyer" and Supplier "SMSupplier"
	When User Populates Grid and Initiates Payment for invoice belong to Buyer "SMBuyer"
	Then On Success the "Invoice" Status is Initiated and Paymeny Portal is launched

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify Error upon Pay Invoices with Transaction Status other than Current
    And User Populates PayOnline Eligible Invoices from Past "165" days with single Currency "USD"
	When User initiates Payment for Invoices with Transaction Status "Past Due"
	Then Error message "Please select Invoices with single currency type for payment; cannot process payment with multiple currencies" should be displayed

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify successful payment initiation by selecting invoice having Location Type as Shop 
    And Fresh Invoices are submitted from DMS with Buyer having Location Type as "Shop"
	When User Populates Grid and Initiates Payment for invoice belong to Buyer 
	Then On Success the "Invoice" Status is Initiated and Paymeny Portal is launched

@CON-26463 @19.0 @Functional @Regression @PayInvoiceValidation @19.1 @CON-27565 @QAOnly
Scenario: Verify successful payment initiation by selecting invoice having Statement Type as Master Billing
    And Fresh Invoices are submitted from DMS with Buyer "Masterbuyer" and Supplier "MasterSupplier"
	When User Populates Grid and Initiates Payment for invoice belong to Buyer "SMBuyer"
	Then On Success the "Invoice" Status is Initiated and Paymeny Portal is launched


@CON-26473 @19.0 @Functional @Regression @InvoiceStatusUpdate 
Scenario: Verify Statuses are availble under Transaction Status Dorpdown
    When User navigates to Advanced Search
    Then Statues "Current" "Current-Closed" "Current-Disputed" "Current-Hold" "Current-Hold Released" "In Progress" "Initiated" "Paid" "Past due" should be visible under Transaction Status Dropdown

@CON-26473 @19.0 @Functional @Regression @InvoiceStatusUpdate  
Scenario: Verfiy Invoice Transaction Status is Initated
   When User Searches by Dealer Invoice Number "DTI000099216"
   Then Inovice Transaction status should be "Initiated"

@CON-26473 @19.0 @Functional @Regression @InvoiceStatusUpdate  
Scenario: Verfiy Invoice Transaction Status is In Progress
   When User Searches by Dealer Invoice Number "DTI000099133"
   Then Inovice Transaction status should be "In Progress"

@CON-26473 @19.0 @Functional @Regression @InvoiceStatusUpdate 
Scenario: Verfiy Invoice Transaction Status is Changing to Past Due upon Failed 
   When User Searches by Dealer Invoice Number "VNI000098595"
   Then Inovice Transaction status should be "Past due"

@CON-26473 @19.0 @Functional @Regression @InvoiceStatusUpdate  
Scenario: Verfiy Invoice Transaction Status is Changing to Past Due upon Cancelling 
   When User Searches by Dealer Invoice Number "DTI000099115"
   Then Inovice Transaction status should be "Past due"
	







