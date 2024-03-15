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
namespace AutomationTesting_CorConnect.Features.FleetPOPOQTransactionLookup
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("FleetPOPOQTransactionLookup")]
    public partial class FleetPOPOQTransactionLookupFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "FleetPOPOQTransactionLookup.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/FleetPOPOQTransactionLookup", "FleetPOPOQTransactionLookup", "As a User\r\nI want to verify DateRange dropdown options\r\nI want to verify Search b" +
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
#line 8
#line hidden
#line 9
 testRunner.Given("User \"Admin\" is on \"Fleet PO/POQ Transaction Lookup\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify DateRange dropdown values on FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyDateRangeDropdownValuesOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify DateRange dropdown values on FleetPOPOQTransactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
    testRunner.Then("Dropdown \"Date Range\" should have valid values on \"Fleet PO/POQ Transaction Looku" +
                        "p\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Last 7 Days on FleetPOPOQTransa" +
            "ctionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsLast7DaysOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Last 7 Days on FleetPOPOQTransa" +
                    "ctionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 19
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
#line 20
 testRunner.When("Search by DateRange value \"Last 7 days\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 21
 testRunner.Then("Data for \"Last 7 days\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Last 14 Days on FleetPOPOQTrans" +
            "actionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsLast14DaysOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Last 14 Days on FleetPOPOQTrans" +
                    "actionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 25
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
#line 26
 testRunner.When("Search by DateRange value \"Last 14 days\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 27
 testRunner.Then("Data for \"Last 14 days\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Last 185 Days on FleetPOPOQTran" +
            "sactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsLast185DaysOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Last 185 Days on FleetPOPOQTran" +
                    "sactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 30
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
#line 31
 testRunner.When("Search by DateRange value \"Last 185 days\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 32
 testRunner.Then("Data for \"Last 185 days\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Current month on FleetPOPOQTran" +
            "sactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsCurrentMonthOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Current month on FleetPOPOQTran" +
                    "sactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 35
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
#line 36
 testRunner.When("Search by DateRange value \"Current month\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 37
 testRunner.Then("Data for \"Current month\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Last month on FleetPOPOQTransac" +
            "tionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsLastMonthOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Last month on FleetPOPOQTransac" +
                    "tionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 40
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
#line 41
 testRunner.When("Search by DateRange value \"Last month\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 42
 testRunner.Then("Data for \"Last month\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Search By DateRange Dropdown When Value is Customized date on FleetPOPOQTr" +
            "ansactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifySearchByDateRangeDropdownWhenValueIsCustomizedDateOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Search By DateRange Dropdown When Value is Customized date on FleetPOPOQTr" +
                    "ansactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 45
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
#line 46
 testRunner.When("Search by DateRange value \"Customized date\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 47
 testRunner.Then("Data for \"Customized date\" is shown on the results grid", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Last 7 days date range option sets the correct From and To " +
            "dates on FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingLast7DaysDateRangeOptionSetsTheCorrectFromAndToDatesOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Last 7 days date range option sets the correct From and To " +
                    "dates on FleetPOPOQTransactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 50
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
#line 51
 testRunner.When("User selects \"Last 7 days\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 52
 testRunner.Then("The From Date and To Date are set correctly for the \"Last 7 days\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Last 14 days date range option sets the correct From and To" +
            " dates on FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingLast14DaysDateRangeOptionSetsTheCorrectFromAndToDatesOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Last 14 days date range option sets the correct From and To" +
                    " dates on FleetPOPOQTransactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 55
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
#line 56
 testRunner.When("User selects \"Last 14 days\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 57
 testRunner.Then("The From Date and To Date are set correctly for the \"Last 14 days\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Last 185 days date range option sets the correct From and T" +
            "o dates on FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingLast185DaysDateRangeOptionSetsTheCorrectFromAndToDatesOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Last 185 days date range option sets the correct From and T" +
                    "o dates on FleetPOPOQTransactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 60
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
#line 61
 testRunner.When("User selects \"Last 185 days\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 62
 testRunner.Then("The From Date and To Date are set correctly for the \"Last 185 days\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Current month date range option sets the correct From and T" +
            "o dates on FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingCurrentMonthDateRangeOptionSetsTheCorrectFromAndToDatesOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Current month date range option sets the correct From and T" +
                    "o dates on FleetPOPOQTransactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 65
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
#line 66
 testRunner.When("User selects \"Current month\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 67
 testRunner.Then("The From Date and To Date are set correctly for the \"Current month\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Last month date range option sets the correct From and To d" +
            "ates on FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingLastMonthDateRangeOptionSetsTheCorrectFromAndToDatesOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Last month date range option sets the correct From and To d" +
                    "ates on FleetPOPOQTransactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
 testRunner.When("User selects \"Last month\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 72
 testRunner.Then("The From Date and To Date are set correctly for the \"Last month\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify that selecting Customized date date range option sets the correct From and" +
            " To dates on FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("CON-20036")]
        [NUnit.Framework.CategoryAttribute("Functional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("FleetPOPOQTransactionLookup")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        public void VerifyThatSelectingCustomizedDateDateRangeOptionSetsTheCorrectFromAndToDatesOnFleetPOPOQTransactionLookup()
        {
            string[] tagsOfScenario = new string[] {
                    "CON-20036",
                    "Functional",
                    "Smoke",
                    "FleetPOPOQTransactionLookup",
                    "18.0"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify that selecting Customized date date range option sets the correct From and" +
                    " To dates on FleetPOPOQTransactionLookup", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 75
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
#line 76
 testRunner.When("User selects \"Customized date\" from DateRange dropdown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 77
 testRunner.Then("The From Date and To Date are set correctly for the \"Customized date\" option", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
