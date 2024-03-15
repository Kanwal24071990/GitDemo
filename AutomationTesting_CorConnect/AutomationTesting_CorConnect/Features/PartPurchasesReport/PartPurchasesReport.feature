Feature: PartPurchasesReport

As a User
I want to verify DateRange dropdown options
I want to verify Search by all options of DateRange dropdown

Background: Load Application with given user and navigate
	Given User "Admin" is on pop-up page "Part Purchases Report" page

#TASK CON-20036: Last 185 days Date Range Search Option

@CON-20036 @Functional @Smoke @PartPurchasesReport @18.0
Scenario: Verify DateRange dropdown values on PartPurchasesReport
    Then Dropdown "Date Range" should have valid values on pop-up "Part Purchases Report" page

@CON-20036 @Functional @Smoke @PartPurchasesReport @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 7 Days on PartPurchasesReport
	When Search by DateRange value "Last 7 days" for selected dealer "18AutoDlr" and fleet "18AutoFlt"
	Then Data for "Last 7 days" is shown on the results grid


@CON-20036 @Functional @Smoke @PartPurchasesReport @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Current Quarter on PartPurchasesReport
	When Search by DateRange value "Current Quarter" for selected dealer "18AutoDlr" and fleet "18AutoFlt"
	Then Data for "Current Quarter" is shown on the results grid

@CON-20036 @Functional @Smoke @PartPurchasesReport @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 185 Days on PartPurchasesReport
	When Search by DateRange value "Last 185 days" for selected dealer "18AutoDlr" and fleet "18AutoFlt"
	Then Data for "Last 185 days" is shown on the results grid
	
@CON-20036 @Functional @Smoke @PartPurchasesReport @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Current month on PartPurchasesReport
	When Search by DateRange value "Current month" for selected dealer "18AutoDlr" and fleet "18AutoFlt"
	Then Data for "Current month" is shown on the results grid

@CON-20036 @Functional @SearchByDateRange @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Last 12 months on PartPurchasesReport
	When Search by DateRange value "Last 12 months" for selected dealer "18AutoDlr" and fleet "18AutoFlt"
	Then Data for "Last 12 months" is shown on the results grid

@CON-20036 @Functional @Smoke @PartPurchasesReport @18.0
Scenario: Verify Search By DateRange Dropdown When Value is Customized date on PartPurchasesReport
	When Search by DateRange value "Customized date" for selected dealer "18AutoDlr" and fleet "18AutoFlt"
	Then Data for "Customized date" is shown on the results grid

@CON-20036 @Functional @Smoke @PartPurchasesReport @18.0
Scenario: Verify Search By DateRange Dropdown When Value is YTD on PartPurchasesReport
	When Search by DateRange value "YTD" for selected dealer "18AutoDlr" and fleet "18AutoFlt"
	Then Data for "YTD" is shown on the results grid