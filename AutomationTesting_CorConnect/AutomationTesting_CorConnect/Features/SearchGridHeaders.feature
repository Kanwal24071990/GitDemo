Feature: Search Grid Headers

Validate Headers for All pages

@tag1
Scenario: To Validate Page Header before and after Search for all pages part 1
	Given User "Admin" logs in
	And User navigates to "Invoice Watch List" page
	And Verify Headers "Before" Search for Page "Invoice Watch List"
	And Loads data on "Invoice Watch List" page
	And Verify Headers "After" Search for Page "Invoice Watch List"
	And User closes "Invoice Watch List" and navigates to "Open Authorizations" page
	And Verify Headers "Before" Search for Page "Open Authorization"
	And Loads data on "Open Authorizations" page
	And Verify Headers "After" Search for Page "Open Authorization"
	And User closes "Open Authorizations" and navigates to "Dealer Lookup" page
	And Verify Headers "Before" Search for Page "Dealer Lookup"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Dealer Lookup"
	And User closes "Dealer Lookup" and navigates to "Disputes" page
	And Verify Headers "Before" Search for Page "Disputes"
	And Loads data on "Disputes" page
	And Verify Headers "After" Search for Page "Disputes"
	And User closes "Disputes" and navigates to "Dealer Invoices" page
	And Verify Headers "Before" Search for Page "Dealer Invoices"
	And User switch to "Advance" search
	And Verify Headers "Before" Search for Page "Dealer Invoices"
	And Loads data on "Dealer Invoices" page
	And Verify Headers "After" Search for Page "Dealer Invoices"
	And User switch to "Quick" search
	And Loads data on "Dealer Invoices" page with invoice number
	And Verify Headers "After" Search for Page "Dealer Invoices"
	And User closes "Dealer Invoices" and navigates to "Dealer Invoice Transaction Lookup" page
	And Verify Headers "Before" Search for Page "Dealer Invoice Transaction Lookup"
	And User switch to "Advance" search
	And Verify Headers "Before" Search for Page "Dealer Invoice Transaction Lookup"
	And Loads data on "Dealer Invoice Transaction Lookup" page
	And Verify Headers "After" Search for Page "Dealer Invoice Transaction Lookup"
	And User switch to "Quick" search
	And Loads data on "Dealer Invoice Transaction Lookup" page with invoice number
	And Verify Headers "After" Search for Page "Dealer Invoice Transaction Lookup"
	And User closes "Dealer Invoice Transaction Lookup" and navigates to "Dealer Statements" page
	And Verify Headers "Before" Search for Page "Dealer Statements"
	And Loads data on "Dealer Statements" page
	And Verify Headers "After" Search for Page "Dealer Statements"
	And User closes "Dealer Statements" and navigates to "Fleet Invoices" page
	And Verify Headers "Before" Search for Page "Fleet Invoices"
	And User switch to "Advance" search
	And Verify Headers "Before" Search for Page "Fleet Invoices"
	And Loads data on "Fleet Invoices" page
	And Verify Headers "After" Search for Page "Fleet Invoices"
	And User switch to "Quick" search
	And Loads data on "Fleet Invoices" page with invoice number
	And Verify Headers "After" Search for Page "Fleet Invoices"
	And User closes "Fleet Invoices" and navigates to "Fleet Invoice Transaction Lookup" page
	And Verify Headers "Before" Search for Page "Fleet Invoice Transaction Lookup"
	And User switch to "Advance" search
	And Verify Headers "Before" Search for Page "Fleet Invoice Transaction Lookup"
	And Loads data on "Fleet Invoice Transaction Lookup" page
	And Verify Headers "After" Search for Page "Fleet Invoice Transaction Lookup"
	And User switch to "Quick" search
	And Loads data on "Fleet Invoice Transaction Lookup" page with invoice number
	And Verify Headers "After" Search for Page "Fleet Invoice Transaction Lookup"
	Then Verify Errors for all pages


Scenario: To Validate Page Header before and after Search for all pages Part 2
	Given User "Admin" logs in
	And User navigates to "Fleet Master Invoices/Statements" page
	And Verify Headers "Before" Search for Page "Fleet Master Invoices/Statements"
	And Loads data on "Fleet Master Invoices/Statements" page
	And Verify Headers "After" Search for Page "Fleet Master Invoices/Statements"
	And User closes "Fleet Master Invoices/Statements" and navigates to "Fleet Statements" page
	And Verify Headers "Before" Search for Page "Fleet Statements"
	And Loads data on "Fleet Statements" page
	And Verify Headers "After" Search for Page "Fleet Statements"
	And User closes "Fleet Statements" and navigates to "GP Draft Statements" page
	And Verify Headers "Before" Search for Page "GP Draft Statements"
	And Loads data on "GP Draft Statements" page
	And Verify Headers "After" Search for Page "GP Draft Statements"
	And User closes "GP Draft Statements" and navigates to "P-Card Transactions" page
	And Verify Headers "Before" Search for Page "P-Card Transactions"
	And Loads data on "P-Card Transactions" page
	And Verify Headers "After" Search for Page "P-Card Transactions"
	And User closes "P-Card Transactions" and navigates to "Settlement File" page
	And Verify Headers "Before" Search for Page "Settlement File"
	And Loads data on "Settlement File" page
	And Verify Headers "After" Search for Page "Settlement File"
	And User closes "Settlement File" and navigates to "Settlement File Summary" page
	And Verify Headers "Before" Search for Page "Settlement File Summary"
	And Loads data on "Settlement File Summary" page
	And Verify Headers "After" Search for Page "Settlement File Summary"
	And User closes "Settlement File Summary" and navigates to "Aged Invoice Report" page
	And Verify Headers "Before" Search for Page "Aged Invoice Report"
	And Loads data on "Aged Invoice Report" page
	And Verify Headers "After" Search for Page "Aged Invoice Report"
	And User closes "Aged Invoice Report" and navigates to "Community Fee Report" page
	And Verify Headers "Before" Search for Page "Community Fee Report"
	And Loads data on "Community Fee Report" page
	And Verify Headers "After" Search for Page "Community Fee Report"
	And User closes "Community Fee Report" and navigates to "Draft Statement Report" page
	And Verify Headers "Before" Search for Page "Draft Statement Report"
	And Loads data on "Draft Statement Report" page
	And Verify Headers "After" Search for Page "Draft Statement Report"
	And User closes "Draft Statement Report" and navigates to "Fleet Credit Limit" page
	And Verify Headers "Before" Search for Page "Fleet Credit Limit"
	And Loads data on "Fleet Credit Limit" page
	And Verify Headers "After" Search for Page "Fleet Credit Limit"
	And User closes "Fleet Credit Limit" and navigates to "Fleet Credit Limit Watch List Report" page
	And Verify Headers "Before" Search for Page "Fleet Credit Limit Watch List Report"
	And Loads data on "Fleet Credit Limit Watch List Report" page
	And Verify Headers "After" Search for Page "Fleet Credit Limit Watch List Report"
	Then Verify Errors for all pages

Scenario: To Validate Page Header before and after Search for all pages Part 3
	Given User "Admin" logs in
	And User navigates to "Intercommunity Invoice Report" page
	And Verify Headers "Before" Search for Page "Intercommunity Invoice Report"
	And Loads data on "Intercommunity Invoice Report" page
	And Verify Headers "After" Search for Page "Intercommunity Invoice Report"
	And User closes "Intercommunity Invoice Report" and navigates to "Pending Billing Management Report" page
	And Verify Headers "Before" Search for Page "Pending Billing Management Report"
	And Loads data on "Pending Billing Management Report" page
	And Verify Headers "After" Search for Page "Pending Billing Management Report"
	And User closes "Pending Billing Management Report" and navigates to "Part Sales by Shop Report" page
	And Verify Headers "Before" Search for Page "Part Sales by Shop Report"
	And Loads data on "Part Sales by Shop Report" page
	And Verify Headers "After" Search for Page "Part Sales by Shop Report"
	And User closes "Part Sales by Shop Report" and navigates to "Summary Reconcile Report" page
	And Verify Headers "Before" Search for Page "Summary Reconcile Report"
	And Loads data on "Summary Reconcile Report" page
	And Verify Headers "After" Search for Page "Summary Reconcile Report"
	And User closes "Summary Reconcile Report" and navigates to "Invoice Discrepancy History" page
	And Verify Headers "Before" Search for Page "Invoice Discrepancy History"
	And Loads data on "Invoice Discrepancy History" page
	And Verify Headers "After" Search for Page "Invoice Discrepancy History"
	And User closes "Invoice Discrepancy History" and navigates to "Dealer Due Date Report" page
	And Verify Headers "Before" Search for Page "Dealer Due Date Report"
	And Loads data on "Dealer Due Date Report" page
	And Verify Headers "After" Search for Page "Dealer Due Date Report"
	And User closes "Dealer Due Date Report" and navigates to "Dealer Discount Date Report" page
	And Verify Headers "Before" Search for Page "Dealer Discount Date Report"
	And Loads data on "Dealer Discount Date Report" page
	And Verify Headers "After" Search for Page "Dealer Discount Date Report"
	And User closes "Dealer Discount Date Report" and navigates to "Dealer Invoice Pre-Approval Report" page
	And Verify Headers "Before" Search for Page "Dealer Invoice Pre-Approval Report"
	And Loads data on "Dealer Invoice Pre-Approval Report" page
	And Verify Headers "After" Search for Page "Dealer Invoice Pre-Approval Report"
	And User closes "Dealer Invoice Pre-Approval Report" and navigates to "Dealer Locations" page
	And Verify Headers "Before" Search for Page "Dealer Locations"
	And Loads data on "Dealer Locations" page
	And Verify Headers "After" Search for Page "Dealer Locations"
	And User closes "Dealer Locations" and navigates to "Dealer Part Summary - Fleet Bill To" page
	And Verify Headers "Before" Search for Page "Dealer Part Summary - Fleet Bill To"
	And Loads data on "Dealer Part Summary - Fleet Bill To" page
	And Verify Headers "After" Search for Page "Dealer Part Summary - Fleet Bill To"
	And User closes "Dealer Part Summary - Fleet Bill To" and navigates to "Dealer Part Summary - Fleet Location" page
	And Verify Headers "Before" Search for Page "Dealer Part Summary - Fleet Location"
	And Loads data on "Dealer Part Summary - Fleet Location" page
	And Verify Headers "After" Search for Page "Dealer Part Summary - Fleet Location"
	And User closes "Dealer Part Summary - Fleet Location" and navigates to "Dealer Sales Summary - Bill To" page
	And Verify Headers "Before" Search for Page "Dealer Sales Summary - Bill To"
	And Loads data on "Dealer Sales Summary - Bill To" page
	And Verify Headers "After" Search for Page "Dealer Sales Summary - Bill To"
	And User closes "Dealer Sales Summary - Bill To" and navigates to "Dealer Sales Summary - Location" page
	And Verify Headers "Before" Search for Page "Dealer Sales Summary - Location"
	And Loads data on "Dealer Sales Summary - Location" page
	And Verify Headers "After" Search for Page "Dealer Sales Summary - Location"
	And User closes "Dealer Sales Summary - Location" and navigates to "Gross Margin Credit Report" page
	And Verify Headers "Before" Search for Page "Gross Margin Credit Report"
	And Loads data on "Gross Margin Credit Report" page
	And Verify Headers "After" Search for Page "Gross Margin Credit Report"
	And User closes "Gross Margin Credit Report" and navigates to "Price Exception Report" page
	And Verify Headers "Before" Search for Page "Price Exception Report"
	And Loads data on "Price Exception Report" page
	And Verify Headers "After" Search for Page "Price Exception Report"
	Then Verify Errors for all pages

Scenario: To Validate Page Header before and after Search for all pages Part 4
	Given User "Admin" logs in
	And User navigates to "Purchasing Fleet Summary" page
	And Verify Headers "Before" Search for Page "Purchasing Fleet Summary"
	And Loads data on "Purchasing Fleet Summary" page
	And Verify Headers "After" Search for Page "Purchasing Fleet Summary"
	And User closes "Purchasing Fleet Summary" and navigates to "Part Price Lookup" page
	And Verify Headers "Before" Search for Page "Part Price Lookup"
	And Loads data on "Part Price Lookup" page
	And Verify Headers "After" Search for Page "Part Price Lookup"
	And User closes "Part Price Lookup" and navigates to "Remittance Report" page
	And Verify Headers "Before" Search for Page "Remittance Report"
	And Loads data on "Remittance Report" page
	And Verify Headers "After" Search for Page "Remittance Report"
	And User closes "Remittance Report" and navigates to "Summary Remittance Report" page
	And Verify Headers "Before" Search for Page "Summary Remittance Report"
	And Loads data on "Summary Remittance Report" page
	And Verify Headers "After" Search for Page "Summary Remittance Report"
	And User closes "Summary Remittance Report" and navigates to "Tax Review Report" page
	And Verify Headers "Before" Search for Page "Tax Review Report"
	And Loads data on "Tax Review Report" page
	And Verify Headers "After" Search for Page "Tax Review Report"
	And User closes "Tax Review Report" and navigates to "Fleet Bill To Sales Summary" page
	And Verify Headers "Before" Search for Page "Fleet Bill To Sales Summary"
	And Loads data on "Fleet Bill To Sales Summary" page
	And Verify Headers "After" Search for Page "Fleet Bill To Sales Summary"
	And User closes "Fleet Bill To Sales Summary" and navigates to "Fleet Due Date Report" page
	And Verify Headers "Before" Search for Page "Fleet Due Date Report"
	And Loads data on "Fleet Due Date Report" page
	And Verify Headers "After" Search for Page "Fleet Due Date Report"
	And User closes "Fleet Due Date Report" and navigates to "Fleet Discount Date Report" page
	And Verify Headers "Before" Search for Page "Fleet Discount Date Report"
	And Loads data on "Fleet Discount Date Report" page
	And Verify Headers "After" Search for Page "Fleet Discount Date Report"
	And User closes "Fleet Discount Date Report" and navigates to "Fleet Invoice Pre-Approval Report" page
	And Verify Headers "Before" Search for Page "Fleet Invoice Pre-Approval Report"
	And Loads data on "Fleet Invoice Pre-Approval Report" page
	And Verify Headers "After" Search for Page "Fleet Invoice Pre-Approval Report"
	And User closes "Fleet Invoice Pre-Approval Report" and navigates to "Fleet Locations" page
	And Verify Headers "Before" Search for Page "Fleet Locations"
	And Loads data on "Fleet Locations" page
	And Verify Headers "After" Search for Page "Fleet Locations"
	And User closes "Fleet Locations" and navigates to "Fleet Location Sales Summary" page
	And Verify Headers "Before" Search for Page "Fleet Location Sales Summary"
	And Loads data on "Fleet Location Sales Summary" page
	And Verify Headers "After" Search for Page "Fleet Location Sales Summary"
	And User closes "Fleet Location Sales Summary" and navigates to "Fleet Part Category Sales Summary - Bill To" page
	And Verify Headers "Before" Search for Page "Fleet Part Category Sales Summary - Bill To"
	And Loads data on "Fleet Part Category Sales Summary - Bill To" page
	And Verify Headers "After" Search for Page "Fleet Part Category Sales Summary - Bill To"
	Then Verify Errors for all pages

	Scenario: To Validate Page Header before and after Search for all pages Part 5
	Given User "Admin" logs in
	And User navigates to "Fleet Part Category Sales Summary - Location" page
	And Verify Headers "Before" Search for Page "Fleet Part Category Sales Summary - Location"
	And Loads data on "Fleet Part Category Sales Summary - Location" page
	And Verify Headers "After" Search for Page "Fleet Part Category Sales Summary - Location"
	And User closes "Fleet Part Category Sales Summary - Location" and navigates to "Fleet Part Summary - Bill To" page
	And Verify Headers "Before" Search for Page "Fleet Part Summary - Bill To"
	And Loads data on "Fleet Part Summary - Bill To" page
	And Verify Headers "After" Search for Page "Fleet Part Summary - Bill To"
	And User closes "Fleet Part Summary - Bill To" and navigates to "Fleet Part Summary - Location" page
	And Verify Headers "Before" Search for Page "Fleet Part Summary - Location"
	And Loads data on "Fleet Part Summary - Location" page
	And Verify Headers "After" Search for Page "Fleet Part Summary - Location"
	And User closes "Fleet Part Summary - Location" and navigates to "Fleet Sales Summary - Bill To" page
	And Verify Headers "Before" Search for Page "Fleet Sales Summary - Bill To"
	And Loads data on "Fleet Sales Summary - Bill To" page
	And Verify Headers "After" Search for Page "Fleet Sales Summary - Bill To"
	And User closes "Fleet Sales Summary - Bill To" and navigates to "Fleet Sales Summary - Location" page
	And Verify Headers "Before" Search for Page "Fleet Sales Summary - Location"
	And Loads data on "Fleet Sales Summary - Location" page
	And Verify Headers "After" Search for Page "Fleet Sales Summary - Location"
	And User closes "Fleet Sales Summary - Location" and navigates to "Line Item Report" page
	And Verify Headers "Before" Search for Page "Line Item Report"
	And Loads data on "Line Item Report" page
	And Verify Headers "After" Search for Page "Line Item Report"
	And User closes "Line Item Report" and navigates to "Dealer Release Invoices" page
	And Verify Headers "Before" Search for Page "Dealer Release Invoices"
	And Loads data on "Dealer Release Invoices" page
	And Verify Headers "After" Search for Page "Dealer Release Invoices"
	And User closes "Dealer Release Invoices" and navigates to "Invoice Entry" page
	And Verify Headers "Before" Search for Page "Invoice Entry"
	And Loads data on "Invoice Entry" page
	And Verify Headers "After" Search for Page "Invoice Entry"
	And User closes "Invoice Entry" and navigates to "PO Discrepancy" page
	And Verify Headers "Before" Search for Page "PO Discrepancy"
	And Loads data on "PO Discrepancy" page
	And Verify Headers "After" Search for Page "PO Discrepancy"
	And User closes "PO Discrepancy" and navigates to "PO Discrepancy History" page
	And Verify Headers "Before" Search for Page "PO Discrepancy History"
	And Loads data on "PO Discrepancy History" page
	And Verify Headers "After" Search for Page "PO Discrepancy History"
	Then Verify Errors for all pages

	Scenario: To Validate Page Header before and after Search for all pages Part 6
	Given User "Admin" logs in
	And User navigates to "PO Orders" page
	And Verify Headers "Before" Search for Page "PO Orders"
	And Loads data on "PO Orders" page
	And Verify Headers "After" Search for Page "PO Orders"
	And User closes "PO Orders" and navigates to "PO Transaction Lookup" page
	And Verify Headers "Before" Search for Page "PO Transaction Lookup"
	And Loads data on "PO Transaction Lookup" page
	And Verify Headers "After" Search for Page "PO Transaction Lookup"
	And User closes "PO Transaction Lookup" and navigates to "Price Lookup" page
	And Verify Headers "Before" Search for Page "Price Lookup"
	And Loads data on "Price Lookup" page
	And Verify Headers "After" Search for Page "Price Lookup"
	And User closes "Price Lookup" and navigates to "Parts Lookup" page
	And Verify Headers "Before" Search for Page "Parts Lookup"
	And Loads data on "Parts Lookup" page
	And Verify Headers "After" Search for Page "Parts Lookup"
	And User closes "Parts Lookup" and navigates to "Dealer PO/POQ Transaction Lookup" page
	And Verify Headers "Before" Search for Page "Dealer PO/POQ Transaction Lookup"
	And Loads data on "Dealer PO/POQ Transaction Lookup" page
	And Verify Headers "After" Search for Page "Dealer PO/POQ Transaction Lookup"
	And User closes "Dealer PO/POQ Transaction Lookup" and navigates to "PO Entry" page
	And Verify Headers "Before" Search for Page "PO Entry"
	And Loads data on "PO Entry" page
	And Verify Headers "After" Search for Page "PO Entry"
	And User closes "PO Entry" and navigates to "POQ Entry" page
	And Verify Headers "Before" Search for Page "POQ Entry"
	And Loads data on "POQ Entry" page
	And Verify Headers "After" Search for Page "POQ Entry"
	And User closes "POQ Entry" and navigates to "Fleet PO/POQ Transaction Lookup" page
	And Verify Headers "Before" Search for Page "Fleet PO/POQ Transaction Lookup"
	And Loads data on "Fleet PO/POQ Transaction Lookup" page
	And Verify Headers "After" Search for Page "Fleet PO/POQ Transaction Lookup"
	And User closes "Fleet PO/POQ Transaction Lookup" and navigates to "Billing Schedule Management" page
	And Verify Headers "Before" Search for Page "Billing Schedule Management"
	And Loads data on "Billing Schedule Management" page
	And Verify Headers "After" Search for Page "Billing Schedule Management"
	And User closes "Billing Schedule Management" and navigates to "Fleet Parts Cross Reference" page
	And Verify Headers "Before" Search for Page "Fleet Parts Cross Reference"
	And Loads data on "Fleet Parts Cross Reference" page
	And Verify Headers "After" Search for Page "Fleet Parts Cross Reference"
	Then Verify Errors for all pages

	Scenario: To Validate Page Header before and after Search for all pages Part 7
	Given User "Admin" logs in
	And User navigates to "Parts" page
	And Verify Headers "Before" Search for Page "Parts"
	And Loads data on "Parts" page
	And Verify Headers "After" Search for Page "Parts"
	And User closes "Parts" and navigates to "Parts Cross Reference" page
	And Verify Headers "Before" Search for Page "Parts Cross Reference"
	And Loads data on "Parts Cross Reference" page
	And Verify Headers "After" Search for Page "Parts Cross Reference"
	And User closes "Parts Cross Reference" and navigates to "Part/Price File Upload Report" page
	And Verify Headers "Before" Search for Page "Part/Price File Upload Report"
	And Loads data on "Part/Price File Upload Report" page
	And Verify Headers "After" Search for Page "Part/Price File Upload Report"
	And User closes "Part/Price File Upload Report" and navigates to "Price" page
	And Verify Headers "Before" Search for Page "Price"
	And Loads data on "Price" page
	And Verify Headers "After" Search for Page "Price"
	And User closes "Price" and navigates to "Account Status Change Report" page
	And Verify Headers "Before" Search for Page "Account Status Change Report"
	And Loads data on "Account Status Change Report" page
	And Verify Headers "After" Search for Page "Account Status Change Report"
	And User closes "Account Status Change Report" and navigates to "Batch Request Status" page
	And Verify Headers "Before" Search for Page "Batch Request Status"
	And Loads data on "Batch Request Status" page
	And Verify Headers "After" Search for Page "Batch Request Status"
	And User closes "Batch Request Status" and navigates to "Invoice Detail Report" page
	And Verify Headers "Before" Search for Page "Invoice Detail Report"
	And Loads data on "Invoice Detail Report" page
	And Verify Headers "After" Search for Page "Invoice Detail Report"
	And User closes "Invoice Detail Report" and navigates to "Part Sales by Fleet Report" page
	And Verify Headers "Before" Search for Page "Part Sales by Fleet Report"
	And Loads data on "Part Sales by Fleet Report" page
	And Verify Headers "After" Search for Page "Part Sales by Fleet Report"
	And User closes "Part Sales by Fleet Report" and navigates to "Entity Cross Reference Maintenance" page
	And Verify Headers "Before" Search for Page "Entity Cross Reference Maintenance"
	And Loads data on "Entity Cross Reference Maintenance" page
	And Verify Headers "After" Search for Page "Entity Cross Reference Maintenance"
	And User closes "Entity Cross Reference Maintenance" and navigates to "Tax Code Configuration" page
	And Verify Headers "Before" Search for Page "Tax Code Configuration"
	And Loads data on "Tax Code Configuration" page
	And Verify Headers "After" Search for Page "Tax Code Configuration"
	Then Verify Errors for all pages

	Scenario: To Validate Page Header before and after Search for all pages Part 8
	Given User "Admin" logs in
	And User navigates to "Bookmarks Maintenance" page
	And Verify Headers "Before" Search for Page "Bookmarks Maintenance"
	And Loads data on "Bookmarks Maintenance" page
	And Verify Headers "After" Search for Page "Bookmarks Maintenance"
	And User closes "Bookmarks Maintenance" and navigates to "Entity Group Maintenance" page
	And Verify Headers "Before" Search for Page "Entity Group Maintenance"
	And Loads data on "Entity Group Maintenance" page
	And Verify Headers "After" Search for Page "Entity Group Maintenance"
	And User closes "Entity Group Maintenance" and navigates to "Assign User Charts" page
	And Verify Headers "Before" Search for Page "Assign User Charts"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Assign User Charts"
	And User closes "Assign User Charts" and navigates to "Manage Approvals" page
	And Verify Headers "Before" Search for Page "Manage Approvals"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Manage Approvals"
	And User closes "Manage Approvals" and navigates to "Manage Rebates" page
	And Verify Headers "Before" Search for Page "Manage Rebates"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Manage Rebates"
	And User closes "Manage Rebates" and navigates to "Manage Users" page
	And Verify Headers "Before" Search for Page "Manage Users"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Manage Users"
	And User closes "Manage Users" and navigates to "PCard Error Invoices" page
	And Verify Headers "Before" Search for Page "PCard Error Invoices"
	And User Populate Grid
	And Verify Headers "After" Search for Page "PCard Error Invoices"
	And User closes "PCard Error Invoices" and navigates to "Invoice Discrepancy" page
	And Verify Headers "Before" Search for Page "Invoice Discrepancy"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Invoice Discrepancy"
	And User closes "Invoice Discrepancy" and navigates to "Cash Flow Report" page
	And Verify Headers "Before" Search for Page "Cash Flow Report"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Cash Flow Report"
	And User closes "Cash Flow Report" and navigates to "Fleet Sales Summary - Bill To" page
	And Verify Headers "Before" Search for Page "Fleet Sales Summary - Bill To"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Fleet Sales Summary - Bill To"
	Then Verify Errors for all pages

	Scenario: To Validate Page Header before and after Search for all pages Part 9
	Given User "Admin" logs in
	And User navigates to "Open Balance Report" page
	And Verify Headers "Before" Search for Page "Open Balance Report"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Open Balance Report"
	And User closes "Open Balance Report" and navigates to "Account Maintenance" page
	And Verify Headers "Before" Search for Page "Account Maintenance"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Account Maintenance"
	And User closes "Account Maintenance" and navigates to "Fee Management" page
	And Verify Headers "Before" Search for Page "Fee Management"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Fee Management"
	And User closes "Fee Management" and navigates to "Master/Billing Statement Configuration" page
	And Verify Headers "Before" Search for Page "Master/Billing Statement Configuration"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Master/Billing Statement Configuration"
	And User closes "Master/Billing Statement Configuration" and navigates to "Manage Custom Fields" page
	And Verify Headers "Before" Search for Page "Manage Custom Fields"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Manage Custom Fields"
	And User closes "Manage Custom Fields" and navigates to "Manage Disputes" page
	And Verify Headers "Before" Search for Page "Manage Disputes"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Manage Disputes"
	And User closes "Manage Disputes" and navigates to "Manage Delinquent Fee" page
	And Verify Headers "Before" Search for Page "Manage Delinquent Fee"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Manage Delinquent Fee"
	And User closes "Manage Delinquent Fee" and navigates to "Manage Payment Acceleration" page
	And Verify Headers "Before" Search for Page "Manage Payment Acceleration"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Manage Payment Acceleration"
	And User closes "Manage Payment Acceleration" and navigates to "Manage Payment Terms" page
	And Verify Headers "Before" Search for Page "Manage Payment Terms"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Manage Payment Terms"
	And User closes "Manage Payment Terms" and navigates to "Product Group Rule Maintenance" page
	And Verify Headers "Before" Search for Page "Product Group Rule Maintenance"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Product Group Rule Maintenance"
	And User closes "Product Group Rule Maintenance" and navigates to "Product Group Rules Override" page
	And Verify Headers "Before" Search for Page "Product Group Rules Override"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Product Group Rules Override"
	And User closes "Product Group Rules Override" and navigates to "Sub-community Management" page
	And Verify Headers "Before" Search for Page "Sub-community Management"
	And User Populate Grid
	And Verify Headers "After" Search for Page "Sub-community Management"
	Then Verify Errors for all pages

	Scenario: To Validate Page Header before and after Search for all pages Part 10
	Given User "Admin" logs in
	And User navigates to "Manage User Notifications" page
	And Verify Headers "After" Search for Page "Manage User Notifications"
	And User closes "Manage User Notifications" and navigates to "User Group Setup" page
	And Verify Headers "After" Search for Page "User Group Setup"
	And User closes "User Group Setup" and navigates to "Part Categories" page
	And Verify Headers "After" Search for Page "Part Categories"
	And User closes "Part Categories" and navigates to "Assign Entity Chart" page
	And Verify Headers "After" Search for Page "Assign Entity Chart"
	And User closes "Assign Entity Chart" and navigates to "Assign Entity Function" page
	And Verify Headers "After" Search for Page "Assign Entity Function"
	Then Verify Errors for all pages
