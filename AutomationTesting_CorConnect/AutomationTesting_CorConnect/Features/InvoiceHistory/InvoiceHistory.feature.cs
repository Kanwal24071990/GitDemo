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
namespace AutomationTesting_CorConnect.Features.InvoiceHistory
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("InvoiceHistory")]
    public partial class InvoiceHistoryFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "InvoiceHistory.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/InvoiceHistory", "InvoiceHistory", "Validating Invoice History grid for different invoice operations", ProgrammingLanguage.CSharp, featureTags);
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
#line 4
#line hidden
#line 5
testRunner.Given("User \"Admin\" is on \"Dealer Invoice Transaction Lookup\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify record is captured in Invoice History grid upon invoice reversal")]
        [NUnit.Framework.CategoryAttribute("CON-27653")]
        [NUnit.Framework.CategoryAttribute("InvoiceHistory")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("20.0")]
        [NUnit.Framework.CategoryAttribute("NoUAT")]
        public void VerifyRecordIsCapturedInInvoiceHistoryGridUponInvoiceReversal()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-27653",
                    "InvoiceHistory",
                    "Functional",
                    "20.0",
                    "NoUAT"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify record is captured in Invoice History grid upon invoice reversal", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 8
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 9
    testRunner.Given("Invoice exists between buyer \"InvoiceHistoryByr\" and supplier \"InvoiceHistorySup\"" +
                        "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 10
 testRunner.When("The invoice is reversed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 11
 testRunner.Then("\"Reverse\" operation is captured in Invoice History grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify record is captured in Invoice History grid upon invoice rebill")]
        [NUnit.Framework.CategoryAttribute("CON-27653")]
        [NUnit.Framework.CategoryAttribute("InvoiceHistory")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("20.0")]
        [NUnit.Framework.CategoryAttribute("NoUAT")]
        public void VerifyRecordIsCapturedInInvoiceHistoryGridUponInvoiceRebill()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-27653",
                    "InvoiceHistory",
                    "Functional",
                    "20.0",
                    "NoUAT"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify record is captured in Invoice History grid upon invoice rebill", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 4
this.FeatureBackground();
#line hidden
#line 15
 testRunner.Given("Invoice exists between buyer \"InvoiceHistoryByr\" and supplier \"InvoiceHistorySup\"" +
                        "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 16
 testRunner.When("The invoice is rebilled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 17
 testRunner.Then("\"Rebill\" operation is captured in Invoice History grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify record is captured in Invoice History grid upon bulk invoice reversal")]
        [NUnit.Framework.CategoryAttribute("CON-27653")]
        [NUnit.Framework.CategoryAttribute("InvoiceHistory")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("20.0")]
        [NUnit.Framework.CategoryAttribute("NoUAT")]
        public void VerifyRecordIsCapturedInInvoiceHistoryGridUponBulkInvoiceReversal()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-27653",
                    "InvoiceHistory",
                    "Functional",
                    "20.0",
                    "NoUAT"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify record is captured in Invoice History grid upon bulk invoice reversal", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 20
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 4
this.FeatureBackground();
#line hidden
#line 21
 testRunner.Given("Invoice created with Bulk Reversal exists in the system", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 22
 testRunner.When("The invoice is opened", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 23
 testRunner.Then("\"Bulk Reversal\" operation is captured in Invoice History grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
