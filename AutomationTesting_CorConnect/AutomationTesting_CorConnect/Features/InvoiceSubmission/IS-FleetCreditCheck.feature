@IS-FleetCreditCheck
Feature: IS-FleetCreditCheck

As a user i want to verify fleet credit limit check with fleet non-corcentric locations 
Background: Load Application with given user and navigate
	Given User "Admin" logs in

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @InvoiceEntry @QAFunctional
Scenario: Invoice bypass credit not available discrepancy when financial handling is community from UI
    Given Fleet "byrShFinCom" Credit Limit is Updated to 0    
    When invoice is sucessfully created using Dealer "SupShFinCom" and Fleet "byrShFinCom" with invoice type "Parts"
    Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "byrShFinCom"


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @InvoiceEntry @QAFunctional
Scenario: Invoice bypass credit not available discrepancy when financial handling is Pcard from UI
    Given Fleet "byrShFinPca" Credit Limit is Updated to 0
    When invoice is sucessfully created using Dealer "SupShFinPca" and Fleet "byrShFinPca" with invoice type "Parts"
    Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "byrShFinPca"


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @InvoiceEntry @QAFunctional
Scenario: Invoice bypass credit not available discrepancy when financial handling is Reporting only from UI
    Given Fleet "byrShFinRep" Credit Limit is Updated to 0    
    When invoice is sucessfully created using Dealer "SupShFinRep" and Fleet "byrShFinRep" with invoice type "Parts"
    Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "byrShFinRep"


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @InvoiceEntry @QAFunctional
Scenario: Invoice bypass credit not available discrepancy when financial handling is community and payment method is pcard from UI
    Given Fleet "byrSh4FinRep" Credit Limit is Updated to 0
    When invoice is sucessfully created using Dealer "SupSh4FinRep" and Fleet "byrSh4FinRep" with invoice type "Parts"
    Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "byrSh4FinRep"


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @InvoiceEntry @QAFunctional
Scenario: Invoice bypass credit not available discrepancy when payment method is pcard from UI
	Given Fleet "ByrblgUSD1" Credit Limit is Updated to 0
    When invoice is sucessfully created using Dealer "SupBlgUSD1" and Fleet "ByrblgUSD1" with invoice type "Parts"
    Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "ByrblgUSD1"

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional @UAT
Scenario Outline: Credit Limit Validation on Account Configuration When Financial Handling Relationship is Community	
	Given User navigates to "Account Maintenance" page
	And Fleet "byrShFinCom" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrShFinCom" on "Account Maintenance"
	Then credit limit should be 0 on "Account Configuration"	
Examples:  	
	| CreditAmount  |	
	|  -100         |	
	|   100         |	

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Account Configuration When Financial Handling Relationship is Pcard	
	Given User navigates to "Account Maintenance" page
	And Fleet "byrShFinPca" Credit Limit is Updated to <CreditAmount> 	
	When Search fleet "byrShFinPca" on "Account Maintenance"
	Then credit limit should be 0 on "Account Configuration"	
Examples:  	
	| CreditAmount  |	
	|   -100        |	
	|    100        |	

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Account Configuration When Financial Handling Relationship is Reporting Only	
	Given User navigates to "Account Maintenance" page
	And Fleet "byrShFinRep" Credit Limit is Updated to <CreditAmount> 	
	When Search fleet "byrShFinRep" on "Account Maintenance"
	Then credit limit should be 0 on "Account Configuration"	
Examples:  	
	| CreditAmount  |	
	|    -100       |	
	|     100       |

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Fleet Credit Limit Page When Financial Handling Relationship is Community 	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrShFinCom" Credit Limit is Updated to <CreditAmount> 
	When Search fleet "byrShFinCom" on "Fleet Credit Limit"
	Then credit limit should be 0 on "Fleet Credit Limit"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |	

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional @UAT
Scenario Outline: Credit Limit Validation on Fleet Credit Limit Page When Financial Handling Relationship is PCard 	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrShFinPca" Credit Limit is Updated to <CreditAmount> 	
	When Search fleet "byrShFinPca" on "Fleet Credit Limit"
	Then credit limit should be 0 on "Fleet Credit Limit"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |	

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Fleet Credit Limit Page When Financial Handling Relationship is Reporting Only 	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrShFinRep" Credit Limit is Updated to <CreditAmount> 	
	When Search fleet "byrShFinRep" on "Fleet Credit Limit"
	Then credit limit should be 0 on "Fleet Credit Limit"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |	


@CON-22288
@Functional @Smoke @18.0
@UpdateCredit @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Update Credit Page when Financial Handling Relationship is Community 	
	Given User navigates to "Update Credit" page
	And Fleet "byrShFinCom" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrShFinCom" on "Update Credit"
	Then credit limit should be 0 on "Update Credit"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |	

@CON-22288
@Functional @Smoke @18.0
@UpdateCredit @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Update Credit Page when Financial Handling Relationship is PCard	
	Given User navigates to "Update Credit" page
	And Fleet "byrShFinPca" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrShFinPca" on "Update Credit"
	Then credit limit should be 0 on "Update Credit"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |	

@CON-22288
@Functional @Smoke @18.0
@UpdateCredit @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Update Credit Page when Financial Handling Relationship is Reporting Only	
	Given User navigates to "Update Credit" page
	And Fleet "byrShFinRep" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrShFinRep" on "Update Credit"
	Then credit limit should be 0 on "Update Credit"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |


@CON-22288
@Functional @Smoke @18.0
@UpdateCredit @CreditValidation @QAFunctional @UAT
Scenario Outline: Credit Limit Validation on Update Credit Page when Payment Method Relationship is Pcard	
	Given User navigates to "Update Credit" page
	And Fleet "ByrblgUSD1" Credit Limit is Updated to <CreditAmount> 	
	When Search fleet "ByrblgUSD1" on "Update Credit"
	Then credit limit should be 0 on "Update Credit"	
Examples:  	
	| CreditAmount  |	
	|   -100        |	
	|    100        |


@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Fleet Credit Limit Page when Payment method Relationship is Pcard	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "ByrblgUSD1" Credit Limit is Updated to <CreditAmount> 
	When Search fleet "ByrblgUSD1" on "Fleet Credit Limit"
	Then credit limit should be 0 on "Fleet Credit Limit"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional
Scenario Outline: Credit Limit Validation on Account Configuration when Payment Method Relationship	is PCard
	Given User navigates to "Account Maintenance" page
	And Fleet "ByrblgUSD1" Credit Limit is Updated to <CreditAmount>
	When Search fleet "ByrblgUSD1" on "Account Maintenance"
	Then credit limit should be 0 on "Account Configuration"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |	


@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional @UAT
Scenario: Credit Limit Validation on Account Configuration when financial handling and payment method relationship not exist	
	Given User navigates to "Account Maintenance" page
	And Fleet "ByrblgUSD" Credit Limit is Updated to 100
	When Search fleet "ByrblgUSD" on "Account Maintenance"
	Then credit limit should be 100 on "Account Configuration"

@CON-22288
@Functional @Smoke @18.0
@UpdateCredit @CreditValidation @QAFunctional 
Scenario: Credit Limit Validation on Update Credit when financial handling and payment method relationship not exist	
	Given User navigates to "Update Credit" page	
	And Fleet "ByrblgUSD" Credit Limit is Updated to 100
	When Search fleet "ByrblgUSD" on "Update Credit"
	Then credit limit should be 100 on "Update Credit"	

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional
Scenario: Credit Limit Validation on Fleet Credit Limit when financial handling and payment method relationship not exist
	Given User navigates to "Fleet Credit Limit" page	
	And Fleet "ByrblgUSD" Credit Limit is Updated to 100 
	When Search fleet "ByrblgUSD" on "Fleet Credit Limit"
	Then credit limit should be 100 on "Fleet Credit Limit"

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional
Scenario: Credit Limit Validation on Account Configuration when Financial Handling Relationship is Corcentric
	Given User navigates to "Account Maintenance" page		
	And Fleet "byrShFinCor1" Credit Limit is Updated to -100
	When Search fleet "byrShFinCor1" on "Account Maintenance"
	Then credit limit should be -100 on "Account Configuration"	

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional @UAT
Scenario: Credit Limit Validation on Fleet Credit Limit Page when Financial Handling Relationship is Corcentric 	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrShFinCor1" Credit Limit is Updated to -100
	When Search fleet "byrShFinCor1" on "Fleet Credit Limit"
	Then credit limit should be -100 on "Fleet Credit Limit"	

@CON-22288
@Functional @Smoke @18.0
@UpdateCredit @CreditValidation @QAFunctional
Scenario: Credit Limit Validation on Update Credit Page when Financial Handling Relationship is Corcentric 
	Given User navigates to "Update Credit" page
	And Fleet "byrShFinCor1" Credit Limit is Updated to -100
	When Search fleet "byrShFinCor1" on "Update Credit"
	Then credit limit should be -100 on "Update Credit"

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional @UAT
Scenario Outline: Credit Limit Validation on Fleet Credit Limit Page Financial Handling is Corcentric and Payment Method is PCard
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrSh1FinCor" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrSh1FinCor" on "Fleet Credit Limit"
	Then credit limit should be 0 on "Fleet Credit Limit"	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@Update @UpdateCredit @QAFunctional
Scenario Outline: Update Credit Limit for fleet when financial handling relationship is corcentric
	Given User navigates to "Update Credit" page
	And Fleet "byrShFinCor1" Credit Limit is Updated to <CreditAmount>	
	When User "should" be able to update credit to 500  for fleet "byrShFinCor1"
	Then Credit limit 500 should be updated successfully
	
Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@Update @UpdateCredit @QAFunctional
Scenario Outline: Update Credit Limit for fleet when financial handling and payment method relationship does not exist
	Given User navigates to "Update Credit" page
	And Fleet "ByrblgUSD" Credit Limit is Updated to <CreditAmount> 	
	When User "should" be able to update credit to 500  for fleet "ByrblgUSD"	
	Then Credit limit 500 should be updated successfully

Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |	

	
@CON-22288
@Functional @Smoke @18.0
@Update @UpdateCredit @QAFunctional
Scenario Outline: Update credit limit for fleet when payment method relationship is Check	
	Given User navigates to "Update Credit" page
	And Fleet "ByrblgUSD" Credit Limit is Updated to <CreditAmount> 	
	When User "should" be able to update credit to 500  for fleet "ByrblgUSD"	
	Then Credit limit 500 should be updated successfully

Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional
Scenario Outline:Credit Limit validation on fleet credit limit page when Financial Handling is PCard and Payment Method is PCard	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrSh2FinCor" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrSh2FinCor" on "Fleet Credit Limit"
	Then credit limit should be 0 on "Fleet Credit Limit"	
		
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional
Scenario Outline:Credit Limit validation on fleet credit limit page when Financial Handling is Reporting Only and Payment Method is PCard	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrSh3FinPca" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrSh3FinPca" on "Fleet Credit Limit"
	Then credit limit should be 0 on "Fleet Credit Limit"	
		
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |	

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @CreditValidation @QAFunctional
Scenario Outline:Credit Limit validation on fleet credit limit page when Financial Handling is Community and Payment Method is PCard	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrSh4FinRep" Credit Limit is Updated to <CreditAmount> 		
	When Search fleet "byrSh4FinRep" on "Fleet Credit Limit"
	Then credit limit should be 0 on "Fleet Credit Limit"	
		
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |


@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional
Scenario Outline:Credit Limit Validation on Account Configuration when Financial Handling Corcentric and Payment Method PCard   	
	Given User navigates to "Account Maintenance" page
	And Fleet "byrSh1FinCor" Credit Limit is Updated to <CreditAmount>	
	When Search fleet "byrSh1FinCor" on "Account Maintenance"
	Then credit limit should be 0 on "Account Configuration"	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional
Scenario Outline:Credit Limit Validation on Account Configuration when Financial Handling PCard and Payment Method PCard   	
	Given User navigates to "Account Maintenance" page
	And Fleet "byrSh2FinCor" Credit Limit is Updated to <CreditAmount>	
	When Search fleet "byrSh2FinCor" on "Account Maintenance"
	Then credit limit should be 0 on "Account Configuration"	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional
Scenario Outline:Credit Limit Validation on Account Configuration when Financial Handling Reporting Only and Payment Method PCard   	
	Given User navigates to "Account Maintenance" page
	And Fleet "byrSh3FinPca" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrSh3FinPca" on "Account Maintenance"
	Then credit limit should be 0 on "Account Configuration"	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @CreditValidation @QAFunctional
Scenario Outline:Credit Limit Validation on Account Configuration when Financial Handling Community and Payment Method PCard  	
	Given User navigates to "Account Maintenance" page
	And Fleet "byrSh4FinRep" Credit Limit is Updated to <CreditAmount>	
	When Search fleet "byrSh4FinRep" on "Account Maintenance"
	Then credit limit should be 0 on "Account Configuration"	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@CreditValidation @QAFunctional
Scenario Outline:Update Credit page validation when Financial Handling Corcentric and Payment Method PCard   	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh1FinCor" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byrSh1FinCor" on "Update Credit"
	Then credit limit should be 0 on "Update Credit"	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@CreditValidation @QAFunctional
Scenario Outline:Update Credit page validation when Financial Handling PCard and Payment Method PCard   	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh2FinCor" Credit Limit is Updated to <CreditAmount>	
	When Search fleet "byrSh2FinCor" on "Update Credit"
	Then credit limit should be 0 on "Update Credit"	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@CreditValidation @QAFunctional
Scenario Outline:Update Credit page validation when Financial Handling Reporting Only and Payment Method PCard   	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh3FinPca" Credit Limit is Updated to <CreditAmount>	
	When Search fleet "byrSh3FinPca" on "Update Credit" 
	Then credit limit should be 0 on "Update Credit"	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@CreditValidation @QAFunctional
Scenario Outline:Update Credit page validation when Financial Handling Community and Payment Method PCard   	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh4FinRep" Credit Limit is Updated to <CreditAmount>	
	When Search fleet "byrSh4FinRep" on "Update Credit" 
	Then credit limit should be 0 on "Update Credit"	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |


@CON-22288
@Functional @Smoke @18.0
@Update @QAFunctional
Scenario Outline:Unable to update credit when Financial Handling is Corcentric and Payment Method is PCard   	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh1FinCor" Credit Limit is Updated to <CreditAmount>			
    When User "should not" be able to update credit to 500  for fleet "byrSh1FinCor"
	Then Credit limit 500 should not be updated
		
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@Update @QAFunctional
Scenario Outline:Unable to update credit when Financial Handling is PCard and Payment Method is PCard  	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh2FinCor" Credit Limit is Updated to <CreditAmount>		
    When User "should not" be able to update credit to 500  for fleet "byrSh2FinCor"	
	Then Credit limit 500 should not be updated

	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@Update @QAFunctional
Scenario Outline:Unable to update credit when Financial Handling is Reporting Only and Payment Method is PCard  	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh3FinPca" Credit Limit is Updated to <CreditAmount>		
    When User "should not" be able to update credit to 500  for fleet "byrSh3FinPca"	
	Then Credit limit 500 should not be updated
	
	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@Update @QAFunctional
Scenario Outline:Unable to update credit  when Financial Handling is Community and Payment Method is PCard  	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh4FinRep" Credit Limit is Updated to <CreditAmount>	
    When User "should not" be able to update credit to 500  for fleet "byrSh4FinRep"
	Then Credit limit 500 should not be updated

	Examples:  	
	| CreditAmount  |	
	| -100          |	
	| 100           |

@CON-22288
@Functional @Smoke @18.0
@Update @QAFunctional
Scenario Outline:Unable to update credit when Financial Handling is Pcard and Payment Method is check	
	Given User navigates to "Update Credit" page
	And Fleet "byrSh5FinCom" Credit Limit is Updated to <CreditAmount>	
	When User "should not" be able to update credit to 500  for fleet "byrSh5FinCom"
	Then Credit limit 500 should not be updated

Examples:	
	| CreditAmount |	
	| 100          |	
	| -100         |

@CON-22288
@Functional @Smoke @18.0
@FleetCreditLimit @QAFunctional
Scenario Outline:Fleet Credit Limit Page validation when Financial Handling is Pcard and Payment Method is nonPcard	
	Given User navigates to "Fleet Credit Limit" page
	And Fleet "byrSh5FinCom" Credit Limit is Updated to <CreditAmount>	
	When Search fleet "byrSh5FinCom" on "Fleet Credit Limit" 
	Then credit limit should be 0 on "Fleet Credit Limit"	
Examples:	
	| CreditAmount |	
	| 100          |	
	| -100         |	

@CON-22288
@Functional @Smoke @18.0
@AccountConfiguration @QAFunctional
Scenario Outline:Account Configuration page validation when Financial Handling is Pcard and Payment Method is nonPcard	
	Given User navigates to "Account Maintenance" page
	And Fleet "ByrblgUSD2" Credit Limit is Updated to <CreditAmount>
	When Search fleet "ByrblgUSD2" on "Account Maintenance" 
	Then credit limit should be 0 on "Account Configuration"	
Examples:	
	| CreditAmount |	
	| 100          |	
	| -100         |

@CON-22288
@Functional @Smoke @18.0
@UpdateCredit @QAFunctional
Scenario Outline:Update Credit page validation when Financial Handling is community and Payment Method is nonPcard 	
	Given User navigates to "Update Credit" page
	And Fleet "byr2Sh1FinCom" Credit Limit is Updated to <CreditAmount>
	When Search fleet "byr2Sh1FinCom" on "Update Credit"
	Then credit limit should be 0 on "Update Credit"	
Examples:	
	| CreditAmount |	
	| 100          |	
	| -100         |


@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @CreateAuthorization
Scenario: Authorization Creation Failed Due To Credit Not Available when financial handling and payment method does not exist
	Given User navigates to "Create Authorization" page
	And Fleet "ByrblgUSD" Credit Limit is Updated to 0
	When User Create Authorization with type "Parts" for Supplier "SupBlgUSD" and Buyer "ByrblgUSD" with invoice amount "50.00"
	Then "Error, Credit not available: ByrblgUSD" message should appear on UI for page "Create Authorization"

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @CreateAuthorization
Scenario: Authorization Submission Sucessfully when financial handling and payment method does not exist
	Given User navigates to "Create Authorization" page
   	And Fleet "ByrblgUSD" Credit Limit is Updated to 99999
	When User Create Authorization with type "Parts" for Supplier "SupBlgUSD" and Buyer "ByrblgUSD" with invoice amount "50.00"
	Then "Successful transaction." message should appear on UI for page "Create Authorization"

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @CreateAuthorization
Scenario: Credit Authorization Submission when financial handling and payment method does not exist
   Given User navigates to "Create Authorization" page
   	And Fleet "ByrblgUSD" Credit Limit is Updated to 0
	When User Create Authorization with type "Parts" for Supplier "SupBlgUSD" and Buyer "ByrblgUSD" with invoice amount "-600.00"
	And "Successful transaction." message should appear on UI for page "Create Authorization"
	Then "ByrblgUSD" Buyer credit should be 600

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @CreateAuthorization
Scenario: Credit Authorization Submission when Financial Handling relationship is Corcentric and Payment Method Relationship is PCard
   Given User navigates to "Create Authorization" page
   	And Fleet "18Byr11" Credit Limit is Updated to 0
	When User Create Authorization with type "Parts" for Supplier "18Sup11" and Buyer "18Byr11" with invoice amount "-600.00"
	Then "Successful transaction." message should appear on UI for page "Create Authorization"
	Then "18Byr11" Buyer credit should be 600


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMSSubmission
Scenario: Credit Invoice Submission when Financial Handling is community and Payment Method is PCard
		Given Fleet "18Byr12" Credit Limit is Updated to 0
 	    When Invoice is submitted from DMS with Fleet "18Byr12" and Dealer "18Sup12" with transaction amount -100 and quantity -1
		Then Invoice is submitted successfully for "Non-Corcentric" Location with "Positive" credit for fleet "18Byr12"

@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @QAFunctional 
Scenario: Invoice submission from fixable to reviewable for corcentric location
	Given User navigates to "Invoice Discrepancy" page
	And Fleet "ByrNonCor1" Credit Limit is Updated to 0
	When Invoice is resolved from "Unit Number" discrepancy for "ByrNonCor1" buyer and "SupNonCor1" supplier
	Then invoice moved to discrepancy state with error "Credit not available" for fleet "ByrNonCor1" and supplier "SupNonCor1"

@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @QAFunctional
Scenario: Invoice submission from fixable to settle state for noncorcentric location
	 Given User navigates to "Invoice Discrepancy" page
	 And Fleet "18Byr3" Credit Limit is Updated to 0 
	 When Invoice is resolved from "Unit Number" discrepancy for "18Byr3" buyer and "18Sup3" supplier by alert "Invoice submission completed successfully."
	 Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "18Byr3" and dealer "18Sup3"


@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @QAFunctional
Scenario: Invoice submission from reviewable to settle for corcentric location
	Given User navigates to "Invoice Discrepancy" page
	And Fleet "ByrblgUSD" Credit Limit is Updated to 0 
	When Invoice is resolved from "Credit Not Available" discrepancy for "ByrblgUSD" buyer and "SupBlgUSD" supplier by alert "Invoice submission completed successfully."
	Then Invoice is submitted successfully for "Corcentric" Location with "ByrblgUSD" buyer

@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @QAFunctional
Scenario: Invoice Submission for Corcentric Location with Do Not Put Hold For Dealer Copy checkbox unchecked
	Given User navigates to "Invoice Discrepancy" page
	And Fleet "ByrHold2" Credit Limit is Updated to 0
	When User submit "On hold for physical copy" discrepant invoice for "ByrHold2" buyer and "SupHold2" supplier
	Then "The Invoice could not be submitted :Credit not available: ByrHold2" message should appear on UI for page "Invoice Entry"
	Then "Invoice on hold, awaiting physical copy" message should appear on UI for page "Invoice Entry"


@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @QAFunctional
Scenario: Invoice Submission for Corcentric Location with Do Not Put Hold For Dealer Copy checkbox checked
	Given User navigates to "Invoice Discrepancy" page
	And Fleet "ByrHold2" Credit Limit is Updated to 0
	When Invoice is resolved from "On hold for physical copy" discrepancy for "ByrHold2" buyer and "SupHold2" supplier
    Then "The Invoice could not be submitted :Credit not available: ByrHold2" message should appear on UI for page "Invoice Entry"


@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @QAFunctional
Scenario: Invoice Submission From Reviewable to Awaiting Fleet Release State when Financial Handling is Corcentric
   	Given User navigates to "Invoice Discrepancy" page
	And Fleet "ByrCorc2" Credit Limit is Updated to 0
	When Invoice is resolved from "Credit Not Available" discrepancy for "ByrCorc2" buyer and "SupCorc2" supplier by alert "Invoice submission completed successfully."
	Then invoice moved to discrepancy state with error "Awaiting Fleet Release" for fleet "ByrCorc2" with credit ""


@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @FleetReleaseInvoices @QAFunctional
Scenario: Invoice Submission From Awaiting Fleet Release to Settle State When Financial Handling is Corcentric
   	Given User navigates to "Fleet Release Invoices" page	
	And Fleet "ByrCorc2" Credit Limit is Updated to 10
	When Invoice is resolved from "Awaiting Fleet Release" discrepancy for "ByrCorc2" buyer and "SupCorc2" supplier
    Then Invoice is submitted successfully for "Corcentric" Location with "ByrCorc2" buyer

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @FleetReleaseInvoices
Scenario: Invoice Submission From Awaiting Fleet Release to Settle State When Financial Handling is Corcentric and Payment Method is PCard
	 Given User navigates to "Fleet Release Invoices" page
	 And Fleet "18Byr6" Credit Limit is Updated to 0
	 When Invoice is resolved from "Awaiting Fleet Release" discrepancy for "18Byr6" buyer and "18Sup6" supplier by alert ""
	 Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "18Byr6" and dealer "18Sup6"

@CON-22288
@Functional @Smoke @18.0
@QAFunctional @InvoiceDiscrepancy
Scenario: Invoice Submission From Reviewable to Awaiting Dealer Release State When Financial Handling is Corcentric
   	Given User navigates to "Invoice Discrepancy" page
	And Fleet "ByrCorADR" Credit Limit is Updated to 0
	When Invoice is resolved from "Credit Not Available" discrepancy for "ByrCorADR" buyer and "SupCorADR" supplier by alert "Invoice submission completed successfully."
	Then invoice moved to discrepancy state with error "Awaiting Dealer Release" for fleet "ByrCorADR" with credit ""

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @DealerReleaseInvoices @QAFunctional
Scenario: Invoice Submission From Awaiting Dealer Release to Settle State When Financial Handling is Corcentric
   	Given User navigates to "Dealer Release Invoices" page	
	And Fleet "ByrCorADR" Credit Limit is Updated to 200
	When Invoice is resolved from "Awaiting Dealer Release" discrepancy for "ByrCorADR" buyer and "SupCorADR" supplier by alert "Invoice submission completed successfully."
    Then Invoice is submitted successfully for "Corcentric" Location with "ByrCorADR" buyer

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional
Scenario: Invoice submission for noncorcentric shop location then negative credit added to its billing
	Given Fleet "ByrHrchy1" Credit Limit is Updated to 0
	And Fleet "Byrhrchyshop1" Credit Limit is Updated to 0
	When Invoice is submitted from DMS with Fleet "Byrhrchyshop1" and Dealer "Suphrchshop1" with transaction amount 100 and quantity 1
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "ByrHrchy1"
	Then "Byrhrchyshop1" Buyer credit should be 0

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @DealerInvoices @Clone @QAFunctional
Scenario: Invoice Clone Submission Sucessfully with corcentric location
	Given User navigates to "Dealer Invoices" page
	And Fleet "ByrCloneCor2" Credit Limit is Updated to 99999
	When Rebill the invoice from "SupCloneCor1" dealer and "ByrCloneCor2" fleet for settle state
    Then Invoice is submitted successfully for "Corcentric" Location with "ByrCloneCor2" buyer after Create Rebill	

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @DealerInvoices @QAFunctional @Clone
Scenario: Invoice Clone Submission Failed Due to Credit Not Available
	Given User navigates to "Dealer Invoices" page
	And Fleet "ByrCloneCor2" Credit Limit is Updated to 0
	When "Rebill the invoice" from "SupCloneCor1" dealer and "ByrCloneCor2" fleet
	Then "The Invoice could not be submitted :Credit not available: ByrCloneCor2" message should appear on UI for page "Invoice Entry"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @DealerInvoices @QAFunctional @Clone
Scenario: Invoice Clone Submission Sucessfully with noncorcentric location when credit is lesser than transaction amount
	Given User navigates to "Dealer Invoices" page
	And Fleet "ByrClone3" Credit Limit is Updated to 0
	When Rebill the invoice from "SupClone3" dealer and "ByrClone3 " fleet for settle state
    Then Invoice is submitted successfully for "Non-Corcentric" Location with "ByrClone3 " buyer after Create Rebill	

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @CreateAuthorization @QAFunctional
Scenario: Authorization Submission From Create Authorization Failed Due To Credit Not Available When Location Is Corcentric
 	Given User navigates to "Create Authorization" page
	And Fleet "byrShFinCor1" Credit Limit is Updated to 0
	When User Create Authorization with type "Parts" for Supplier "SupShFinCor1" and Buyer "byrShFinCor1" with invoice amount "50.00"
	Then "Error, Credit not available: byrShFinCor1" message should appear on UI for page "Create Authorization"

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @CreateAuthorization @QAFunctional
Scenario: Authorization Submission Sucessfull From Create Authorization With Credit Available for corcentric location
 	Given User navigates to "Create Authorization" page
   	And Fleet "byrShFinCor1" Credit Limit is Updated to 99999
	When User Create Authorization with type "Parts" for Supplier "SupShFinCor1" and Buyer "byrShFinCor1" with invoice amount "50.00"
	Then "Successful transaction." message should appear on UI for page "Create Authorization"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DealerInvoices @Reversal
Scenario:Invoice Reversal with credit not available for corcentric location
		Given User navigates to "Dealer Invoices" page
   		And Fleet "ByrReverseCor2" Credit Limit is Updated to 0
		When "Create a reversal" from "SupReverseCor2" dealer and "ByrReverseCor2" fleet
		Then "Reversal Transaction submission completed successfully." message should appear on UI for page "Offset Transaction"	
		Then Invoice is submitted successfully for "Corcentric" Location with "ByrReverseCor2" buyer after Reversal


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DealerInvoices @Reversal
Scenario: Invoice Reversal with credit not available for noncorcentric location 
		Given User navigates to "Dealer Invoices" page
   		And Fleet "ByrReverseCon4" Credit Limit is Updated to 0
		When "Create a reversal" from "SupReverseCon4" dealer and "ByrReverseCon4" fleet
		Then "Reversal Transaction submission completed successfully." message should appear on UI for page "Offset Transaction"	
		Then Invoice is submitted successfully for "Non-Corcentric" Location with "ByrReverseCon4" buyer after Reversal

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @CreateAuthorization @QAFunctional
Scenario: Authorization Submission from Create Authorization for nonCorcentric location with insuffcient credit
	Given User navigates to "Create Authorization" page
   	And Fleet "18Byr9" Credit Limit is Updated to 10
	When User Create Authorization with type "Parts" for Supplier "18Sup9" and Buyer "18Byr9" with invoice amount "50.00"
	Then "Successful transaction." message should appear on UI for page "Create Authorization"


@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @OpenAuthorization @QAFunctional
Scenario:Authorization Submission from Open Authorization for nonCorcentric location with insuffcient credit 
	Given User navigates to "Open Authorizations" page 
	And Fleet "18Byr10" Credit Limit is Updated to 10
	When User Create Authorization with type "Parts" for Supplier "18Sup10" and Buyer "18Byr10" with invoice amount "50.00" from Open Authorization 
	Then "Successful transaction." message should appear on UI for page "Create Authorization Popup"


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario: Credit Invoice Submission For Corcentric Location
		Given Fleet "ByrblgUSD" Credit Limit is Updated to 0
 	    When Invoice is submitted from DMS with Fleet "ByrblgUSD" and Dealer "SupBlgUSD" with transaction amount -100 and quantity -1
		Then Invoice is submitted successfully for "Corcentric" Location with "Positive" credit for fleet "ByrblgUSD"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @InvoiceDiscrepancy @QAFunctional
Scenario: Invoice submission from fatal to reviewable state for corcentric locations 
		Given User navigates to "Invoice Discrepancy" page
		And Fleet "byrShFinCor" Credit Limit is Updated to 0
		When Invoice is resolved from "Dealer Code Invalid" discrepancy for "byrShFinCor" buyer and "SupShFinCor" supplier
		Then invoice moved to discrepancy state with error "Credit not available" for fleet "byrShFinCor" with credit ""

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @InvoiceDiscrepancy @QAFunctional
Scenario: Invoice submission from fatal to settle state for noncorcentric locations 
	 Given User navigates to "Invoice Discrepancy" page
	 And Fleet "18Byr4" Credit Limit is Updated to 0 
	 When Invoice is resolved from "Dealer Code Invalid" discrepancy for "18Byr4" buyer and "18Sup4" supplier by alert "Invoice submission completed successfully."
	 Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "18Byr4" and dealer "18Sup4"

@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @QAFunctional 
Scenario:Invoice Submission for NonCorcentric Location with Do Not Put Hold For Dealer Copy checkbox unchecked
	Given User navigates to "Invoice Discrepancy" page
	And Fleet "ByrHold3" Credit Limit is Updated to 50
	When User submit "On hold for physical copy" discrepant invoice for "ByrHold3" buyer and "SupHold3" supplier
	Then "Invoice submission resulted in discrepancy." message should appear on UI for page "Invoice Entry"
	Then invoice moved to discrepancy state with error "Invoice on hold" for fleet "ByrHold3" with credit ""


@CON-22288
@Functional @Smoke @18.0
@InvoiceDiscrepancy @QAFunctional
Scenario: Invoice Submission for NonCorcentric Location with Do Not Put Hold For Dealer Copy checkbox checked
	Given User navigates to "Invoice Discrepancy" page
	And Fleet "ByrHold3" Credit Limit is Updated to 50
	When Invoice is resolved from "On hold for physical copy" discrepancy for "ByrHold3" buyer and "SupHold3" supplier by alert "Invoice submission completed successfully."
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "ByrHold3" buyer

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @DealerReleaseInvoices @QAFunctional
Scenario: Invoice Submission From Awaiting Dealer Release to Settle State with NonCorcentric Location
	Given User navigates to "Dealer Release Invoices" page
	And Fleet "18Byr5" Credit Limit is Updated to 1
	When Invoice is resolved from "Awaiting Dealer Release" discrepancy for "18Byr5" buyer and "18Sup5" supplier
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "18Byr5" and dealer "18Sup5"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @HierarchyScenarios @QAFunctional
Scenario: Invoice Submitted as Non corcentric for Fleet participating in a Group and Group has Non corcentric relationship
	Given Fleet "Byrhrchyshop2" Credit Limit is Updated to 0
	And Fleet "ByrHrchy2" Credit Limit is Updated to 0
    When invoice is sucessfully created using Dealer "SupHrchy2" and Fleet "Byrhrchyshop2" with invoice type "Parts"
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "ByrHrchy2"
	Then "Byrhrchyshop2" Buyer credit should be 0

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @HierarchyScenarios
Scenario: Invoice Submitted as non corcentric for Fleet participating in a Group where Fleet to All dealer relationship is corcentric and Group has non corcentric relationship 
	Given Fleet "Byrhrchyshop3" Credit Limit is Updated to 0
	And Fleet "ByrHrchy3" Credit Limit is Updated to 0 
	When invoice is sucessfully created using Dealer "SupHrchy2" and Fleet "Byrhrchyshop3" with invoice type "Parts"
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "ByrHrchy3"
	Then "Byrhrchyshop3" Buyer credit should be 0

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @HierarchyScenarios
Scenario: For both payment method and financial handling relationship when fleet to specific dealer and fleet to all dealer relationship exist than specific is picked
	Given Fleet "ByrHrchy5" Credit Limit is Updated to 0
	When invoice is sucessfully created using Dealer "SupHrchy5" and Fleet "ByrHrchy5" with invoice type "Parts"
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "ByrHrchy5"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @HierarchyScenarios
Scenario: Invoice Submitted as corcentric for Fleet participating in a Group where Group has corcentric relationship
	Given Fleet "ByrHrchy4" Credit Limit is Updated to 0
	When Invoice is submitted from DMS with Fleet "ByrHrchy4" and Dealer "SupHrchy4" with transaction amount 100 and quantity 1
	Then invoice moved to discrepancy state with error "Credit not available" for fleet "ByrHrchy4" and dealer "SupHrchy4" with credit ""

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @HierarchyScenarios
Scenario: Invoice Submitted as Non corcentric for Fleet parent shop when Fleet Billing participating in a Group which has corcentric relationship but Fleet parent shop has Non corcentric relationship
	Given Fleet "ByrHrchyPS4" Credit Limit is Updated to 0
	And Fleet "ByrHrchy4" Credit Limit is Updated to 0
	When invoice is sucessfully created using Dealer "SupHrchy4" and Fleet "ByrHrchyPS4" with invoice type "Parts"
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "ByrHrchy4"
	Then "ByrHrchyPS4" Buyer credit should be 0

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @HierarchyScenarios
Scenario: Invoice Submitted as corcentric for Fleet participationg in a Group and Group has corcentric relationship
	Given Fleet "ByrHrchy6" Credit Limit is Updated to 0
	When Invoice is submitted from DMS with Fleet "ByrHrchy6" and Dealer "SupHrchy6" with transaction amount 100 and quantity 1
	Then invoice moved to discrepancy state with error "Credit not available" for fleet "ByrHrchy6" and dealer "SupHrchy6" with credit ""

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @HierarchyScenarios
Scenario: Invoice Submitted as non corcentric for Fleet shop when Fleet Billing participating in a Group which has corcentric relationship but Fleet shop has non corcentric relationship
	Given Fleet "ByrHrchy6" Credit Limit is Updated to 0
	And Fleet "ByrHrchyshop6" Credit Limit is Updated to 0
	When Invoice is submitted from DMS with Fleet "ByrHrchyshop6" and Dealer "SupHrchy6" with transaction amount 100 and quantity 1
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "Negative" credit for fleet "ByrHrchy6"
	Then "ByrHrchyshop6" Buyer credit should be 0

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional
#CPS-10147P1
Scenario: Invoice Submission when payment method relationship is PCard and financial handling is Corcentric
	Given User navigates to "Account Maintenance" page
	And Relationship "Financial Handling" with "Corcentric" is created between Dealer "All Dealers" and Fleet "Byr13"
	And Relationship "Payment Method" with "PCard" is created between Dealer "All Dealers" and Fleet "Byr13"
	When Invoice is submitted from DMS with Fleet "Byr13" and Dealer "Sup13" with transaction amount 100 and quantity 1
	Then Invoice is submitted successfully for "Non-Corcentric" Location with "Positive" credit for fleet "Byr13"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional
#CPS-10147P2
Scenario: Invoice Submission when payment method relationship updated from PCard to NonPCard and Financial Handling is Corcentric
	Given User navigates to "Account Maintenance" page
	And Relationship "Payment Method" with "EFT PUSH" is updated between Dealer "All Dealers" and Fleet "Byr13" 
	When Invoice is submitted from DMS with Fleet "Byr13" and Dealer "Sup13" with transaction amount 100 and quantity 1
	Then Invoice is submitted successfully for "Corcentric" Location with "Positive" credit for fleet "Byr13"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice Submission with no credit available when fianancial handling and payment method does not exist
	 Given Fleet "ByrblgUSD" Credit Limit is Updated to <CreditAmount>	
	 When Invoice is submitted from DMS with Fleet "ByrblgUSD" and Dealer "SupBlgUSD" with transaction amount 2 and quantity 1
	 Then invoice moved to discrepancy state with error "Credit Not Available" for fleet "ByrblgUSD" and dealer "SupBlgUSD" with credit "<CreditAmount>"	
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS @UAT
Scenario Outline: Invoice Submission with no credit available when financial handling relationship is corcentric
	 Given Fleet "byrShFinCor1" Credit Limit is Updated to <CreditAmount>	
  	 When Invoice is submitted from DMS with Fleet "byrShFinCor1" and Dealer "SupShFinCor1" with transaction amount 2 and quantity 1
	 Then invoice moved to discrepancy state with error "Credit Not Available" for fleet "byrShFinCor1" and dealer "SupShFinCor1" with credit "<CreditAmount>"
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice Submission with no credit available when Financial Handling is corcentric and Payment Method is check	
	 Given Fleet "byrShFinCor" Credit Limit is Updated to <CreditAmount>	
  	 When Invoice is submitted from DMS with Fleet "byrShFinCor" and Dealer "SupShFinCor" with transaction amount 2 and quantity 1
	 Then invoice moved to discrepancy state with error "Credit Not Available" for fleet "byrShFinCor" and dealer "SupShFinCor" with credit "<CreditAmount>"
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @DMS @UAT
Scenario Outline: Authorization Submission with no credit available when Financial Handling is corcentric and Payment Method is check	
     Given Fleet "byrShFinCor" Credit Limit is Updated to <CreditAmount>
	 When Authorization is submitted from DMS with Fleet "byrShFinCor" and Dealer "SupShFinCor"
     Then Authorization should move to discrepancy state with error "Credit Not Available" for fleet "byrShFinCor" with credit "<CreditAmount>"
Examples:
    | CreditAmount |
    | 0            |
    | 1            |

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @DMS
Scenario Outline: Authorization Submission with no credit available when fianancial handling and payment method does not exist
     Given Fleet "ByrblgUSD" Credit Limit is Updated to <CreditAmount>
     When Authorization is submitted from DMS with Fleet "ByrblgUSD" and Dealer "SupBlgUSD"
     Then Authorization should move to discrepancy state with error "Credit Not Available" for fleet "ByrblgUSD" with credit "<CreditAmount>"
Examples:
    | CreditAmount |
    | 0            |
    | 1            |



@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @DMS
Scenario Outline: Authorization Submission with no credit available when Financial Handling Relationship is corcentric
     Given Fleet "byrShFinCor1" Credit Limit is Updated to <CreditAmount>
     When Authorization is submitted from DMS with Fleet "byrShFinCor1" and Dealer "SupShFinCor1"
     Then Authorization should move to discrepancy state with error "Credit Not Available" for fleet "byrShFinCor1" with credit "<CreditAmount>"
Examples:
    | CreditAmount |
    | 0            |
    | 1            |

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS @UAT
Scenario Outline: Invoice Submission with no credit available when Financial Handling Relationship is PCard
	 Given Fleet "byrShFinPca" Credit Limit is Updated to <CreditAmount>	
 	 When Invoice is submitted from DMS with Fleet "byrShFinPca" and Dealer "SupShFinPca" with transaction amount 2 and quantity 1
	 Then invoice should move to settle state without error "Credit Not Available" for fleet "byrShFinPca"	
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice bypass credit not available discrepancy when financial handling is Community from DMS	
     Given Fleet "byrShFinCom" Credit Limit is Updated to <CreditAmount>	
 	 When Invoice is submitted from DMS with Fleet "byrShFinCom" and Dealer "SupShFinCom" with transaction amount 2 and quantity 1
     Then invoice should move to settle state without error "Credit Not Available" for fleet "byrShFinCom"	
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice bypass credit not available discrepancy when financial handling is Reporting Only	from DMS
     Given Fleet "byrShFinRep" Credit Limit is Updated to <CreditAmount>	
 	 When Invoice is submitted from DMS with Fleet "byrShFinRep" and Dealer "SupShFinRep" with transaction amount 2 and quantity 1
     Then invoice should move to settle state without error "Credit Not Available" for fleet "byrShFinRep"	
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario: Invoice bypass credit not available discrepancy when Payment Method Relationship is PCard	from DMS
     Given Fleet "ByrblgUSD1" Credit Limit is Updated to <CreditAmount>	
     When Invoice is submitted from DMS with Fleet "ByrblgUSD1" and Dealer "SupBlgUSD1" with transaction amount 2 and quantity 1	
     Then invoice should move to settle state without error "Credit Not Available" for fleet "ByrblgUSD1"	
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice bypass credit not available discrepancy when financial handling is Corcentric and Payment Method is PCard from DMS
     Given Fleet "byrSh1FinCor" Credit Limit is Updated to <CreditAmount>	
     When Invoice is submitted from DMS with Fleet "byrSh1FinCor" and Dealer "SupSh1FinCor" with transaction amount 2 and quantity 1	
     Then invoice should move to settle state without error "Credit Not Available" for fleet "byrSh1FinCor"	
Examples:  	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice bypass credit not available discrepancy when financial handling is PCard and Payment Method is PCard from DMS	
     Given Fleet "byrSh2FinCor" Credit Limit is Updated to <CreditAmount>			
     When Invoice is submitted from DMS with Fleet "byrSh2FinCor" and Dealer "SupSh2FinCor" with transaction amount 2 and quantity 1			
     Then invoice should move to settle state without error "Credit Not Available" for fleet "byrSh2FinCor"		
Examples:  		
	| CreditAmount |		
	| 0            |		
	| 1            |		

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice bypass credit not available discrepancy when financial handling is Reporting Only and Payment Method is Pcard	from DMS	
     Given Fleet "byrSh3FinPca" Credit Limit is Updated to <CreditAmount>		
     When Invoice is submitted from DMS with Fleet "byrSh3FinPca" and Dealer "SupSh3FinPca" with transaction amount 2 and quantity 1			
     Then invoice should move to settle state without error "Credit Not Available" for fleet "byrSh3FinPca"		
	Examples:  		
	| CreditAmount |		
	| 0            |		
	| 1            |		

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice bypass credit not available discrepancy when financial handling is Community and Payment Method is Pcard from DMS	
     Given Fleet "byrSh4FinRep" Credit Limit is Updated to <CreditAmount>		
     When Invoice is submitted from DMS with Fleet "byrSh4FinRep" and Dealer "SupSh4FinRep" with transaction amount 2 and quantity 1			
     Then invoice should move to settle state without error "Credit Not Available" for fleet "byrSh4FinRep"		
	 Examples:  		
	| CreditAmount |		
	| 0            |		
	| 1            |		

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @QAFunctional @DMS
Scenario Outline: Invoice bypass credit not available discrepancy when Financial Handling is community and Payment Method is check from DMS	
	Given Fleet "byr2Sh1FinCom" Credit Limit is Updated to <CreditAmount>		
	When Invoice is submitted from DMS with Fleet "byr2Sh1FinCom" and Dealer "Sup2Sh1FinCom" with transaction amount 2 and quantity 1 		
    Then invoice should move to settle state without error "Credit Not Available" for fleet "byr2Sh1FinCom"		
 Examples:  		
	| CreditAmount |		
	| 0            |		
	| 1            |		

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @DMS
Scenario Outline: Authorization bypass credit not available discrepancy when financial handling is Community and Payment Method is check from DMS		
     Given Fleet "byrShFinCom" Credit Limit is Updated to <CreditAmount>		
     When Authorization is submitted from DMS with Fleet "byrShFinCom" and Dealer "SupShFinCom" with transaction amount 4	
     Then Authorization should move to settle state without error "Credit Not Available" for fleet "byrShFinCom" with credit "Negative"			
Examples:		
	| CreditAmount |		
	| 0            |		
	| 1            |		

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @DMS
Scenario Outline: Authorization bypass credit not available discrepancy when financial handling PCard from DMS		
     Given Fleet "byrShFinPca" Credit Limit is Updated to <CreditAmount>		
 	 When Authorization is submitted from DMS with Fleet "byrShFinPca" and Dealer "SupShFinPca" with transaction amount 4	
     Then Authorization should move to settle state without error "Credit Not Available" for fleet "byrShFinPca" with credit "Negative"			
Examples:		
	| CreditAmount |		
	| 0            |		
	| 1            |	

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @DMS
Scenario Outline: Authorization bypass credit not available discrepancy when financial handling Reporting Only from DMS
     Given Fleet "byrShFinRep" Credit Limit is Updated to <CreditAmount>	
 	 When Authorization is submitted from DMS with Fleet "byrShFinRep" and Dealer "SupShFinRep" with transaction amount 4	
     Then Authorization should move to settle state without error "Credit Not Available" for fleet "byrShFinRep" with credit "Negative"		
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @DMS
Scenario Outline: Authorization bypass credit not available discrepancy when Payment Method is PCard from DMS	
     Given Fleet "ByrblgUSD1" Credit Limit is Updated to <CreditAmount>	
     When Authorization is submitted from DMS with Fleet "ByrblgUSD1" and Dealer "SupBlgUSD1" with transaction amount 4	
     Then Authorization should move to settle state without error "Credit Not Available" for fleet "ByrblgUSD1" with credit "Negative"	
Examples:	
	| CreditAmount |	
	| 0            |	
	| 1            |	

@CON-22288
@Functional @Smoke @18.0
@AuthorizationSubmission @QAFunctional @DMS @UAT
Scenario Outline: Authorization bypass credit not available discrepancy when financial handling Corcentric and Payment Method is PCard from DMS
     Given Fleet "byrSh1FinCor" Credit Limit is Updated to <CreditAmount>	
     When Authorization is submitted from DMS with Fleet "byrSh1FinCor" and Dealer "SupSh1FinCor" with transaction amount 4	
     Then Authorization should move to settle state without error "Credit Not Available" for fleet "byrSh1FinCor" with credit "Negative"	
Examples:  	
	| CreditAmount |	
	| 0            |	
	| 1            |


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @ExportedToAccounting @QAFunctional @DMS
Scenario: Corcentric Invoice of financial handling Corcentric type sent to D365
	Given Fleet "byrShFinCor1" Credit Limit is Updated to 999999
	When Invoice is submitted from DMS with Fleet "byrShFinCor1" and Dealer "SupShFinCor1" with transaction amount 100 and quantity 1
	Then Exported To Accounting should be "True"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @ExportedToAccounting @QAFunctional @DMS
Scenario: Non Corcentric Invoice of financial handling Pcard type not sent to D365
	Given Fleet "byrShFinPca" Credit Limit is Updated to 999999
	When Invoice is submitted from DMS with Fleet "byrShFinPca" and Dealer "SupShFinPca" with transaction amount 100 and quantity 1
	Then Exported To Accounting should be "False"

@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @ExportedToAccounting @QAFunctional @DMS
Scenario: Non Corcentric Invoice of financial handling Reporting Only type not sent to D365
	Given Fleet "byrShFinRep" Credit Limit is Updated to 999999
	When Invoice is submitted from DMS with Fleet "byrShFinRep" and Dealer "SupShFinRep" with transaction amount 100 and quantity 1
	Then Exported To Accounting should be "False"


@CON-22288
@Functional @Smoke @18.0
@InvoiceSubmission @ExportedToAccounting @QAFunctional @DMS
Scenario: Non Corcentric Invoice of financial handling Community type not sent to D365
	Given Fleet "byrShFinCom" Credit Limit is Updated to 999999
	When Invoice is submitted from DMS with Fleet "byrShFinCom" and Dealer "SupShFinCom" with transaction amount 100 and quantity 1
	Then Exported To Accounting should be "False"


#@CON-22288
#@Functional @Smoke @18.0
#@Update @QAFunctional
##CON-26508
#Scenario:Update credit limit when payment method is check for direct relationship and pcard for all dealers
#	Given User navigates to "Update Credit" page
#	And Fleet "18Byr22" Credit Limit is Updated to 100	
#	When User "should" be able to update credit to 500  for fleet "18Byr22"
#	Then Credit limit 500 should be updated successfully
#
#@CON-22288
#@Functional @Smoke @18.0
#@Update @QAFunctional
##CON-26508
#Scenario:Unable to Update credit limit when payment method is pcard for direct relationship and check for all dealers
#	Given User navigates to "Update Credit" page
#	And Fleet "18Byr23" Credit Limit is Updated to 100	
#	When User "should not" be able to update credit to 500  for fleet "18Byr23"
#	Then Credit limit 500 should not be updated
#	Then credit limit should be 100 on "Update Credit"
#
#@CON-22288
#@Functional @Smoke @18.0
#@Update @QAFunctional
##CON-26508
#Scenario:Unable to Update credit limit when financial handling is pcard for direct relationship and corcentric for all dealers
#	Given User navigates to "Update Credit" page
#	And Fleet "18Byr24" Credit Limit is Updated to 100	
#	When User "should not" be able to update credit to 500  for fleet "18Byr24"
#	Then Credit limit 500 should not be updated
#	Then credit limit should be 100 on "Update Credit"
#
#@CON-22288
#@Functional @Smoke @18.0
#@Update @QAFunctional
##CON-26508
#Scenario:Update credit limit when financial handling is corcentric for direct relationship and pcard for all dealers
#	Given User navigates to "Update Credit" page
#	And Fleet "18Byr25" Credit Limit is Updated to 100	
#	When User "should" be able to update credit to 500  for fleet "18Byr25"
#	Then Credit limit 500 should be updated successfully
#
#@CON-22288
#@Functional @Smoke @18.0
#@UpdateCredit @CreditValidation @QAFunctional
##CON-26508
#Scenario: Credit Limit Validation on Update Credit Page when payment method is check for direct relationship and pcard for all dealers
#	Given User navigates to "Update Credit" page
#	And Fleet "18Byr22" Credit Limit is Updated to 100
#	When Search fleet "18Byr22" on "Update Credit"
#	Then credit limit should be 100 on "Update Credit"
#	
#@CON-22288
#@Functional @Smoke @18.0
#@UpdateCredit @CreditValidation @QAFunctional
##CON-26508
#Scenario: Credit Limit Validation on Update Credit Page when payment method is pcard for direct relationship and check for all dealers
#	Given User navigates to "Update Credit" page
#	And Fleet "18Byr23" Credit Limit is Updated to 100
#	When Search fleet "18Byr23" on "Update Credit"
#	Then credit limit should be 0 on "Update Credit"
#
#@CON-22288
#@Functional @Smoke @18.0
#@UpdateCredit @CreditValidation @QAFunctional
##CON-26508
#Scenario: Credit Limit Validation on Update Credit Page when financial handling is pcard for direct relationship and corcentric for all dealers
#	Given User navigates to "Update Credit" page
#	And Fleet "18Byr24" Credit Limit is Updated to 100
#	When Search fleet "18Byr24" on "Update Credit"
#	Then credit limit should be 0 on "Update Credit"
#
#@CON-22288
#@Functional @Smoke @18.0
#@UpdateCredit @CreditValidation @QAFunctional
##CON-26508
#Scenario: Credit Limit Validation on Update Credit Page when financial handling is corcentric for direct relationship and pcard for all dealers
#	Given User navigates to "Update Credit" page
#	And Fleet "18Byr25" Credit Limit is Updated to 100
#	When Search fleet "18Byr25" on "Update Credit"
#	Then credit limit should be 100 on "Update Credit"
#
#@CON-22288
#@Functional @Smoke @18.0
#@AccountConfiguration @CreditValidation @QAFunctional
##CON-26508
#Scenario: Credit Limit Validation on Account Configuration when payment method is check for direct relationship and pcard for all dealers
#	Given User navigates to "Account Maintenance" page		
#	And Fleet "18Byr22" Credit Limit is Updated to 100
#	When Search fleet "18Byr22" on "Account Maintenance"
#	Then credit limit should be 100 on "Account Configuration"
#	
#@CON-22288
#@Functional @Smoke @18.0
#@AccountConfiguration @CreditValidation @QAFunctional
##CON-26508
#Scenario: Credit Limit Validation on Account Configuration when payment method is pcard for direct relationship and check for all dealers
#	Given User navigates to "Account Maintenance" page		
#	And Fleet "18Byr23" Credit Limit is Updated to 100
#	When Search fleet "18Byr23" on "Account Maintenance"
#	Then credit limit should be 0 on "Account Configuration"
#
#@CON-22288
#@Functional @Smoke @18.0
#@AccountConfiguration @CreditValidation @QAFunctional
##CON-26508
#Scenario: Credit Limit Validation on Account Configuration when financial handling is pcard for direct relationship and corcentric for all dealers
#	Given User navigates to "Account Maintenance" page		
#	And Fleet "18Byr24" Credit Limit is Updated to 100
#	When Search fleet "18Byr24" on "Account Maintenance"
#	Then credit limit should be 0 on "Account Configuration"
#
#@CON-22288
#@Functional @Smoke @18.0
#@AccountConfiguration @CreditValidation @QAFunctional
##CON-26508
#Scenario: Credit Limit Validation on Account Configuration when financial handling is corcentric for direct relationship and pcard for all dealers
#	Given User navigates to "Account Maintenance" page		
#	And Fleet "18Byr25" Credit Limit is Updated to 100
#	When Search fleet "18Byr25" on "Account Maintenance"
#	Then credit limit should be 100 on "Account Configuration"
#
#@CON-22288
#@Functional @Smoke @18.0
#@FleetCreditLimit @CreditValidation @QAFunctional
##CON-26508
#Scenario:Credit Limit validation on fleet credit limit page when payment method is check for direct relationship and pcard for all dealers	
#	Given User navigates to "Fleet Credit Limit" page
#	And Fleet "18Byr22" Credit Limit is Updated to 100 		
#	When Search fleet "18Byr22" on "Fleet Credit Limit"
#	Then credit limit should be 100 on "Fleet Credit Limit"	
#
#@CON-22288
#@Functional @Smoke @18.0
#@FleetCreditLimit @CreditValidation @QAFunctional
##CON-26508
#Scenario:Credit Limit validation on fleet credit limit page when payment method is pcard for direct relationship and check for all dealers	
#	Given User navigates to "Fleet Credit Limit" page
#	And Fleet "18Byr23" Credit Limit is Updated to 100 		
#	When Search fleet "18Byr23" on "Fleet Credit Limit"
#	Then credit limit should be 0 on "Fleet Credit Limit"
#	
#@CON-22288
#@Functional @Smoke @18.0
#@FleetCreditLimit @CreditValidation @QAFunctional
##CON-26508
#Scenario:Credit Limit validation on fleet credit limit page when financial handling is pcard for direct relationship and corcentric for all dealers	
#	Given User navigates to "Fleet Credit Limit" page
#	And Fleet "18Byr24" Credit Limit is Updated to 100 		
#	When Search fleet "18Byr24" on "Fleet Credit Limit"
#	Then credit limit should be 0 on "Fleet Credit Limit"	
#
#@CON-22288
#@Functional @Smoke @18.0
#@FleetCreditLimit @CreditValidation @QAFunctional
##CON-26508
#Scenario:Credit Limit validation on fleet credit limit page when financial handling is corcentric for direct relationship and pcard for all dealers	
#	Given User navigates to "Fleet Credit Limit" page
#	And Fleet "18Byr25" Credit Limit is Updated to 100 		
#	When Search fleet "18Byr25" on "Fleet Credit Limit"
#	Then credit limit should be 100 on "Fleet Credit Limit"
