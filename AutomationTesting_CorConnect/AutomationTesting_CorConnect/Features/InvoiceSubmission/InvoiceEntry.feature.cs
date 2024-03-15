﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AutomationTesting_CorConnect.Features.InvoiceSubmission
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("InvoiceEntry")]
    public partial class InvoiceEntryFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "InvoiceEntry.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/InvoiceSubmission", "InvoiceEntry", "As a User\r\nI will verify \r\nInvoice Entry for Admin User", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 8
#line hidden
#line 9
testRunner.Given("User \"Admin\" is on \"Invoice Entry\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for invoice created from UI and invoice validity on r" +
            "elationship is greater than Community table")]
        [NUnit.Framework.CategoryAttribute("CON-16798")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceEntry")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("InvoiceEntry")]
        public void ExpirationDateCalculationForInvoiceCreatedFromUIAndInvoiceValidityOnRelationshipIsGreaterThanCommunityTable()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16798",
                    "Functional",
                    "Regression",
                    "InvoiceEntry",
                    "InvoiceSubmission",
                    "InvoiceEntry"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for invoice created from UI and invoice validity on r" +
                    "elationship is greater than Community table", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 14
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 15
    testRunner.Given("Invoice validity setup is 4 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 16
 testRunner.When("Invoice of type \"Parts\" is created using Supplier \"19Sup02\" and Buyer \"19Byr02\" w" +
                        "ith current transaction date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 17
 testRunner.Then("Invoice should be settled successfully with expiration date null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for invoice created from DMS and invoice validity on " +
            "relationship is greater than Community table")]
        [NUnit.Framework.CategoryAttribute("CON-16798")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        public void ExpirationDateCalculationForInvoiceCreatedFromDMSAndInvoiceValidityOnRelationshipIsGreaterThanCommunityTable()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16798",
                    "Functional",
                    "Regression",
                    "InvoiceSubmission",
                    "DMS"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for invoice created from DMS and invoice validity on " +
                    "relationship is greater than Community table", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 21
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 22
 testRunner.Given("Invoice validity setup is 4 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 23
 testRunner.When("Invoice is submitted from DMS with Buyer \"19Byr02\" and Supplier \"19Sup02\" with cu" +
                        "rrent transaction date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 24
 testRunner.Then("Invoice should be settled successfully with expiration date null", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for invoice created from DMS with back date greater t" +
            "han community level")]
        [NUnit.Framework.CategoryAttribute("CON-16799")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        public void ExpirationDateCalculationForInvoiceCreatedFromDMSWithBackDateGreaterThanCommunityLevel()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16799",
                    "Functional",
                    "Regression",
                    "InvoiceSubmission",
                    "DMS"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for invoice created from DMS with back date greater t" +
                    "han community level", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 28
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 29
 testRunner.Given("Invoice validity setup is 4 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 30
 testRunner.When("Invoice of transaction type \"Parts\" is submitted from DMS with Supplier \"SupBlgUS" +
                        "D\" and Buyer \"ByrblgUSD\" with transaction date -5 days from current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 31
 testRunner.Then("Invoice should be in discrepancy with error \"Transaction date is invalid\" for buy" +
                        "er \"ByrblgUSD\" and supplier \"SupBlgUSD\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 32
 testRunner.And("Invoice expiration date is +4 days to transaction date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for invoice created from DMS with back date equal to " +
            "invoice validity relationship")]
        [NUnit.Framework.CategoryAttribute("CON-16680")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        public void ExpirationDateCalculationForInvoiceCreatedFromDMSWithBackDateEqualToInvoiceValidityRelationship()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16680",
                    "Functional",
                    "Regression",
                    "InvoiceSubmission",
                    "DMS"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for invoice created from DMS with back date equal to " +
                    "invoice validity relationship", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 37
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 38
 testRunner.Given("Invoice validity setup is 5 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 39
 testRunner.When("Invoice of transaction type \"Parts\" is submitted from DMS with Supplier \"19Sup03\"" +
                        " and Buyer \"19Byr03\" with transaction date -8 days from current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 40
 testRunner.Then("Invoice should be settled successfully with no errors", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for invoice submitted from DMS with back date greater" +
            " than invoice validity relationship")]
        [NUnit.Framework.CategoryAttribute("CON-16680")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        public void ExpirationDateCalculationForInvoiceSubmittedFromDMSWithBackDateGreaterThanInvoiceValidityRelationship()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16680",
                    "Functional",
                    "Regression",
                    "InvoiceSubmission",
                    "DMS"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for invoice submitted from DMS with back date greater" +
                    " than invoice validity relationship", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 44
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 45
 testRunner.Given("Invoice validity setup is 5 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 46
 testRunner.When("Invoice of transaction type \"Parts\" is submitted from DMS with Supplier \"19Sup03\"" +
                        " and Buyer \"19Byr03\" with transaction date -9 days from current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 47
 testRunner.Then("Invoice should be in discrepancy with error \"Transaction date is invalid\" for buy" +
                        "er \"19Byr03\" and supplier \"19Sup03\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 48
 testRunner.And("Invoice expiration date is +8 days to transaction date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for credit invoice submission from DMS with back date" +
            " equal to invoice validity relationship")]
        [NUnit.Framework.CategoryAttribute("CON-16680")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        public void ExpirationDateCalculationForCreditInvoiceSubmissionFromDMSWithBackDateEqualToInvoiceValidityRelationship()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16680",
                    "Functional",
                    "Regression",
                    "InvoiceSubmission",
                    "DMS"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for credit invoice submission from DMS with back date" +
                    " equal to invoice validity relationship", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 53
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 54
 testRunner.Given("Invoice validity setup is 4 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 55
 testRunner.When("Invoice is submitted from DMS with buyer \"19Byr03\" and supplier \"19Sup03\" with tr" +
                        "ansaction amount -100 and transaction date -8 from current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 56
 testRunner.Then("Invoice should be in discrepancy with error \"Transaction date is invalid\" for buy" +
                        "er \"19Byr03\" and supplier \"19Sup03\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 57
 testRunner.And("Invoice expiration date is +4 days to transaction date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for credit invoice submission from DMS with back date" +
            " equal to community table with relationship available as invoice type")]
        [NUnit.Framework.CategoryAttribute("CON-16680")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        public void ExpirationDateCalculationForCreditInvoiceSubmissionFromDMSWithBackDateEqualToCommunityTableWithRelationshipAvailableAsInvoiceType()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16680",
                    "Functional",
                    "Regression",
                    "InvoiceSubmission",
                    "DMS"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for credit invoice submission from DMS with back date" +
                    " equal to community table with relationship available as invoice type", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 61
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 62
 testRunner.Given("Invoice validity setup is 4 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 63
 testRunner.When("Invoice is submitted from DMS with buyer \"19Byr03\" and supplier \"19Sup03\" with tr" +
                        "ansaction amount -100 and transaction date -4 from current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 64
 testRunner.Then("Invoice should be in discrepancy with error \"Transaction date is invalid\" for buy" +
                        "er \"19Byr03\" and supplier \"19Sup03\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 65
 testRunner.And("Invoice expiration date is +4 days to transaction date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for invoice submitted from DMS with transaction type " +
            "other than parts with back date greater than invoice validity relationship")]
        [NUnit.Framework.CategoryAttribute("CON-16680")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        public void ExpirationDateCalculationForInvoiceSubmittedFromDMSWithTransactionTypeOtherThanPartsWithBackDateGreaterThanInvoiceValidityRelationship()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16680",
                    "Functional",
                    "Regression",
                    "InvoiceSubmission",
                    "DMS"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for invoice submitted from DMS with transaction type " +
                    "other than parts with back date greater than invoice validity relationship", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 70
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 71
 testRunner.Given("Invoice validity setup is 4 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 72
 testRunner.When("Invoice of transaction type \"Service\" is submitted from DMS with Supplier \"19Sup0" +
                        "3\" and Buyer \"19Byr03\" with transaction date -8 days from current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 73
 testRunner.Then("Invoice should be in discrepancy with error \"Transaction date is invalid\" for buy" +
                        "er \"19Byr03\" and supplier \"19Sup03\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Expiration date calculation for invoice submitted from DMS with transaction type " +
            "other than parts with back date equal to invoice validity relationship")]
        [NUnit.Framework.CategoryAttribute("CON-16680")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Regression")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        public void ExpirationDateCalculationForInvoiceSubmittedFromDMSWithTransactionTypeOtherThanPartsWithBackDateEqualToInvoiceValidityRelationship()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-16680",
                    "Functional",
                    "Regression",
                    "InvoiceSubmission",
                    "DMS"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Expiration date calculation for invoice submitted from DMS with transaction type " +
                    "other than parts with back date equal to invoice validity relationship", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 78
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 79
 testRunner.Given("Invoice validity setup is 4 days", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 80
 testRunner.When("Invoice of transaction type \"Service\" is submitted from DMS with Supplier \"19Sup0" +
                        "3\" and Buyer \"19Byr03\" with transaction date -4 days from current date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 81
 testRunner.Then("Invoice should be settled successfully with no errors", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify invoice eligible for online payment when fleet billing location has Enable" +
            " Payment Portal checkbox checked")]
        [NUnit.Framework.CategoryAttribute("CON-26500")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        [NUnit.Framework.CategoryAttribute("19.0")]
        public void VerifyInvoiceEligibleForOnlinePaymentWhenFleetBillingLocationHasEnablePaymentPortalCheckboxChecked()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-26500",
                    "Functional",
                    "InvoiceSubmission",
                    "DMS",
                    "19.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify invoice eligible for online payment when fleet billing location has Enable" +
                    " Payment Portal checkbox checked", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 85
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 86
testRunner.When("Invoice is submitted from DMS with Buyer \"19fbm1s1\" and Supplier \"19dbm1s1\" with " +
                        "current transaction date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 87
testRunner.Then("Invoice marked as eligible for Online Payment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify invoice not eligible for online payment when fleet billing location has En" +
            "able Payment Portal checkbox unchecked")]
        [NUnit.Framework.CategoryAttribute("CON-26500")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("InvoiceSubmission")]
        [NUnit.Framework.CategoryAttribute("DMS")]
        [NUnit.Framework.CategoryAttribute("19.0")]
        public void VerifyInvoiceNotEligibleForOnlinePaymentWhenFleetBillingLocationHasEnablePaymentPortalCheckboxUnchecked()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-26500",
                    "Functional",
                    "InvoiceSubmission",
                    "DMS",
                    "19.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify invoice not eligible for online payment when fleet billing location has En" +
                    "able Payment Portal checkbox unchecked", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 91
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
this.FeatureBackground();
#line hidden
#line 92
testRunner.When("Invoice is submitted from DMS with Buyer \"12.12_akfleet\" and Supplier \"12.12_akde" +
                        "aler\" with current transaction date", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 93
testRunner.Then("Invoice not marked as eligible for Online Payment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion