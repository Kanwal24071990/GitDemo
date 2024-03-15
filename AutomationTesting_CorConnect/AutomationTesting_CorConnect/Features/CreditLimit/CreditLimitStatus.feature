Feature: CreditLimitStatus

A short summary of the feature
Background: Load Application with given user and navigate
	Given User "Admin" logs in
	And Token "EnableCreditLimitToCorSymphony" is "Active"

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27485
Scenario: Validate Updated Credit Limit data is staged on Auth Submission
	Given Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType           | TransactionAmount | BYR      | SUP       |
		| 500          | Create Authorization | 400               | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27486
Scenario: Validate Updated Credit Limit data is staged on Invoice Submission
	Given Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType     | TransactionAmount | BYR      | SUP       |
		| 600          | Create Invoice | 250               | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27487
Scenario: Validate Updated Credit Limit data is staged on Invoice Reversal
	Given User navigates to "Dealer Invoices" page
	And Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType       | BYR      | SUP       |
		| 700          | Invoice Reversal | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27490
Scenario: Validate Credit Limit data is staged when Credit Limit is Increased
	Given User navigates to "Update Credit" page
	And Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType                | TransactionAmount | BYR      | SUP       |
		| 700          | Credit Limit is Increased | 1000              | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27491
Scenario: Validate Credit Limit data is staged when Credit Limit is Descreased
	Given User navigates to "Update Credit" page
	And Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType                 | TransactionAmount | BYR      | SUP       |
		| 700          | Credit Limit is Descreased | 400               | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27492
Scenario: Validate Credit Limit data is staged when New Buyer Account is Created
	Given User navigates to "Create New Entity" popup page
	And Credit Limit and Available credit limit is updated
		| ActionType      |
		| New BYR Created |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table


@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27493
Scenario: Validate Updated Credit Limit records with negative AvailCredit is staged on Auth Submission
	Given CreditLimitVarianceThreshHoldPct is set to "50"
	And Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType                          | TransactionAmount | BYR      | SUP       |
		| 500          | Negative AvailCredit on Create Auth | 700               | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table


@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27494
Scenario: Validate Updated Credit Limit records with negative AvailCredit is staged on Invoice Submission
	Given CreditLimitVarianceThreshHoldPct is set to "50"
	And Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType                         | TransactionAmount | BYR      | SUP       |
		| 600          | Negative AvailCredit on Create Inv | 250               | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27495
Scenario: Verify record with only latest credit update is placed in staging history table
	Given Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType           | TransactionAmount | BYR      | SUP       |
		| 500          | Create Authorization | 400               | CLFleet1 | CLDealer1 |
	And Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType     | TransactionAmount | BYR      | SUP       |
		| 600          | Create Invoice | 250               | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Only Latest Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27496
Scenario: Validate Active but terminated records having updated credit limit are placed in staging history table
	Given Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType           | TransactionAmount | BYR      | SUP       |
		| 500          | Create Authorization | 400               | CLFleet2 | CLDealer2 |
	And Entity "CLFleet2" is set to "Terminated"
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27497
Scenario: Validate inactive records having updated credit limit are placed in staging history table
	Given Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType                       | TransactionAmount | BYR      | SUP       |
		| 500          | Create Auth & Inactivate Account | 300               | CLFleet2 | CLDealer2 |
	And Entity "CLFleet2" is set to "InActive"
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27498
Scenario: Validate Active but Suspended records having updated credit limit are placed in staging history table
	Given Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType                    | TransactionAmount | BYR      | SUP       |
		| 500          | Create Auth & Suspend Account | 200               | CLFleet3 | CLDealer4 |
	And Entity "CLFleet3" is set to "Suspended"
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table

@CON-26496 @Functional @19.0 @CreditLimitStatus @CON-27499
Scenario: Validate Credit Limit data is staged when Auth is Used in Invoice Submission
	Given Credit Limit and Available credit limit is updated
		| CreditAmount | ActionType      | TransactionAmount | BYR      | SUP       |
		| 800          | Use Auth in Inv | 100               | CLFleet1 | CLDealer1 |
	When Job "CorSymphony_Producer_CreditStatus" is executed
	Then Updated CreditAmount, AvailableCredit and TotalAR values should be placed in staging history table



	