Feature: PODiscrepancy

As a User
I want to verify DateRange dropdown options
I want to verify Search by all options of DateRange dropdown

Background: Load Application with given user and navigate
	Given User "Admin" is on "PO Discrepancy" page

#TASK CON-20036: Last 185 days Date Range Search Option

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify DateRange dropdown values on PODiscrepancy
    Then Dropdown "Date Range" should have valid values on "PO Discrepancy" page


@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 7 Days on PODiscrepancy
	When Search by DateRange value "Last 7 days"
	Then Data for "Last 7 days" is shown on the results grid

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 14 Days on PODiscrepancy
	When Search by DateRange value "Last 14 days"
	Then Data for "Last 14 days" is shown on the results grid

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 185 Days on PODiscrepancy
	When Search by DateRange value "Last 185 days"
	Then Data for "Last 185 days" is shown on the results grid
	
@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Current month on PODiscrepancy
	When Search by DateRange value "Current month"
	Then Data for "Current month" is shown on the results grid

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last month on PODiscrepancy
	When Search by DateRange value "Last month"
	Then Data for "Last month" is shown on the results grid

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Customized date on PODiscrepancy
	When Search by DateRange value "Customized date"
	Then Data for "Customized date" is shown on the results grid

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify that selecting Last 7 days date range option sets the correct From and To dates on PODiscrepancy
	When User selects "Last 7 days" from DateRange dropdown
	Then The From Date and To Date are set correctly for the "Last 7 days" option

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify that selecting Last 14 days date range option sets the correct From and To dates on PODiscrepancy
	When User selects "Last 14 days" from DateRange dropdown
	Then The From Date and To Date are set correctly for the "Last 14 days" option

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify that selecting Last 185 days date range option sets the correct From and To dates on PODiscrepancy
	When User selects "Last 185 days" from DateRange dropdown
	Then The From Date and To Date are set correctly for the "Last 185 days" option

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify that selecting Current month date range option sets the correct From and To dates on PODiscrepancy
	When User selects "Current month" from DateRange dropdown
	Then The From Date and To Date are set correctly for the "Current month" option

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify that selecting Last month date range option sets the correct From and To dates on PODiscrepancy
	When User selects "Last month" from DateRange dropdown
	Then The From Date and To Date are set correctly for the "Last month" option

@CON-20036 @Functional @Smoke @PODiscrepancy @18.0
Scenario: Verify that selecting Customized date date range option sets the correct From and To dates on PODiscrepancy
	When User selects "Customized date" from DateRange dropdown
	Then The From Date and To Date are set correctly for the "Customized date" option
