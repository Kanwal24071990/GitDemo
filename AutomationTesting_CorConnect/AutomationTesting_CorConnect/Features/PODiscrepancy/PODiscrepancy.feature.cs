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
namespace AutomationTesting_CorConnect.Features.PODiscrepancy
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("PODiscrepancy")]
    public partial class PODiscrepancyFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "PODiscrepancy.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/PODiscrepancy", "PODiscrepancy", "As a User\r\nI want to verify DateRange dropdown options\r\nI want to verify Search b" +
                    "y all options of DateRange dropdown", ProgrammingLanguage.CSharp, featureTags);
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
#line 7
#line hidden
#line 8
 testRunner.Given("User \"Admin\" is on \"PO Discrepancy\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify DateRange dropdown values on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyDateRangeDropdownValuesOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify DateRange dropdown values on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 13
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 14
    testRunner.Then("Dropdown \"Date Range\" should have valid values on \"PO Discrepancy\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Last 7 Days on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsLast7DaysOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Last 7 Days on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 18
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 19
 testRunner.When("Search by DateRange value \"Last 7 days\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 20
 testRunner.Then("Data for \"Last 7 days\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Last 14 Days on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsLast14DaysOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Last 14 Days on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 23
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 24
 testRunner.When("Search by DateRange value \"Last 14 days\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 25
 testRunner.Then("Data for \"Last 14 days\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Last 185 Days on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsLast185DaysOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Last 185 Days on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 7
this.FeatureBackground();
#line hidden
#line 29
 testRunner.When("Search by DateRange value \"Last 185 days\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 30
 testRunner.Then("Data for \"Last 185 days\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Current month on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsCurrentMonthOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Current month on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 33
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 34
 testRunner.When("Search by DateRange value \"Current month\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 35
 testRunner.Then("Data for \"Current month\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Last month on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsLastMonthOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Last month on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 38
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 39
 testRunner.When("Search by DateRange value \"Last month\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 40
 testRunner.Then("Data for \"Last month\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Customized date on PODiscrepanc" +
            "y")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsCustomizedDateOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Customized date on PODiscrepanc" +
                    "y", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 43
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 44
 testRunner.When("Search by DateRange value \"Customized date\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 45
 testRunner.Then("Data for \"Customized date\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Last 7 days date range option sets the correct From and To " +
            "dates on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingLast7DaysDateRangeOptionSetsTheCorrectFromAndToDatesOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Last 7 days date range option sets the correct From and To " +
                    "dates on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 48
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 49
 testRunner.When("User selects \"Last 7 days\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 50
 testRunner.Then("The From Date and To Date are set correctly for the \"Last 7 days\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Last 14 days date range option sets the correct From and To" +
            " dates on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingLast14DaysDateRangeOptionSetsTheCorrectFromAndToDatesOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Last 14 days date range option sets the correct From and To" +
                    " dates on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 7
this.FeatureBackground();
#line hidden
#line 54
 testRunner.When("User selects \"Last 14 days\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 55
 testRunner.Then("The From Date and To Date are set correctly for the \"Last 14 days\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Last 185 days date range option sets the correct From and T" +
            "o dates on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingLast185DaysDateRangeOptionSetsTheCorrectFromAndToDatesOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Last 185 days date range option sets the correct From and T" +
                    "o dates on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 58
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 59
 testRunner.When("User selects \"Last 185 days\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 60
 testRunner.Then("The From Date and To Date are set correctly for the \"Last 185 days\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Current month date range option sets the correct From and T" +
            "o dates on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingCurrentMonthDateRangeOptionSetsTheCorrectFromAndToDatesOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Current month date range option sets the correct From and T" +
                    "o dates on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 63
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 64
 testRunner.When("User selects \"Current month\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 65
 testRunner.Then("The From Date and To Date are set correctly for the \"Current month\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Last month date range option sets the correct From and To d" +
            "ates on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingLastMonthDateRangeOptionSetsTheCorrectFromAndToDatesOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Last month date range option sets the correct From and To d" +
                    "ates on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 68
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 69
 testRunner.When("User selects \"Last month\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 70
 testRunner.Then("The From Date and To Date are set correctly for the \"Last month\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Customized date date range option sets the correct From and" +
            " To dates on PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("PODiscrepancy")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingCustomizedDateDateRangeOptionSetsTheCorrectFromAndToDatesOnPODiscrepancy()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "PODiscrepancy",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Customized date date range option sets the correct From and" +
                    " To dates on PODiscrepancy", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 73
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
this.FeatureBackground();
#line hidden
#line 74
 testRunner.When("User selects \"Customized date\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 75
 testRunner.Then("The From Date and To Date are set correctly for the \"Customized date\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
