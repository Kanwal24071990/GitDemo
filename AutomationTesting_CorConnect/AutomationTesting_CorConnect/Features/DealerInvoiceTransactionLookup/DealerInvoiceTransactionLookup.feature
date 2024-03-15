Feature: DealerInvoiceTransactionLookup

As a User
I want to verify DateRange dropdown options
I want to verify Search by all options of DateRange dropdown


Background: Load Application with given user and navigate
	Given User "Admin" is on "Dealer Invoice Transaction Lookup" page

#TASK CON-20036: Last 185 days Date Range Search Option

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify DateRange dropdown values on DealerInvoiceTransactionLookup
    Then Dropdown "Date Range" should have valid values on "Dealer Invoice Transaction Lookup" page

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 7 Days on DealerInvoiceTransactionLookup
	When Advanced Search by DateRange value "Last 7 days"
	Then Data for "Last 7 days" is shown on the results grid


@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 14 Days on DealerInvoiceTransactionLookup
	When Advanced Search by DateRange value "Last 14 days"
	Then Data for "Last 14 days" is shown on the results grid

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 185 Days on DealerInvoiceTransactionLookup
	When Advanced Search by DateRange value "Last 185 days"
	Then Data for "Last 185 days" is shown on the results grid
	
@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Current month on DealerInvoiceTransactionLookup
	When Advanced Search by DateRange value "Current month"
	Then Data for "Current month" is shown on the results grid

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last month on DealerInvoiceTransactionLookup
	When Advanced Search by DateRange value "Last month"
	Then Data for "Last month" is shown on the results grid

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Customized date on DealerInvoiceTransactionLookup
	When Advanced Search by DateRange value "Customized date"
	Then Data for "Customized date" is shown on the results grid

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0 @UAT
Scenario: Verify the Last 185 days date range message on Advanced Search on DealerInvoiceTransactionLookup
	When User navigates to Advanced Search
	Then The message "Advanced Search will only allow 185 days date range" is shown

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0 @UAT
Scenario: Verify the Date Range message when user selects FROM date greater than 185 days on DealerInvoiceTransactionLookup
	When User selects From date greater than 185 days on Advanced Search
	Then The message Date Range cannot exceed 185 days is shown as a tooltip

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify that selecting Last 7 days date range option sets the correct From and To dates on DealerInvoiceTransactionLookup
	When User selects "Last 7 days" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Last 7 days" option

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify that selecting Last 14 days date range option sets the correct From and To dates on DealerInvoiceTransactionLookup
	When User selects "Last 14 days" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Last 14 days" option

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0 @UAT
Scenario: Verify that selecting Last 185 days date range option sets the correct From and To dates on DealerInvoiceTransactionLookup
	When User selects "Last 185 days" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Last 185 days" option

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify that selecting Current month date range option sets the correct From and To dates on DealerInvoiceTransactionLookup
	When User selects "Current month" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Current month" option

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify that selecting Last month date range option sets the correct From and To dates on DealerInvoiceTransactionLookup
	When User selects "Last month" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Last month" option

@CON-20036 @Functional @Smoke @DealerInvoiceTransactionLookup @18.0
Scenario: Verify that selecting Customized date date range option sets the correct From and To dates on DealerInvoiceTransactionLookup
	When User selects "Customized date" from DateRange dropdown on Advanced Search
	Then The From Date and To Date are set correctly for the "Customized date" option