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
namespace AutomationTesting_CorConnect.Features.GPDraftStatements
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("GPDraftStatements-Fleet")]
    public partial class GPDraftStatements_FleetFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "GPDraftStatements-Fleet.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/GPDraftStatements", "GPDraftStatements-Fleet", "As a User\r\nI will verify \r\nGrid Column value for Program Code with Admin User", ProgrammingLanguage.CSharp, featureTags);
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
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Program Code Value for Subcommunity Level Fleet User")]
        [NUnit.Framework.CategoryAttribute("ProgramCodeValidation")]
        [NUnit.Framework.CategoryAttribute("CON-25750")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        [NUnit.Framework.CategoryAttribute("Funtional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        public void VerifyProgramCodeValueForSubcommunityLevelFleetUser()
        {
            string[] tagsOfScenario = new string[] {
                    "ProgramCodeValidation",
                    "CON-25750",
                    "18.0",
                    "Funtional",
                    "Smoke"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Program Code Value for Subcommunity Level Fleet User", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 14
     testRunner.Given("User \"Fleet\" is on \"GP Draft Statements\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 15
  testRunner.And("Program CodeToken is \"Active\" for Program Code and \"Inactive\" for Subcommunity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 16
  testRunner.When("User is on \"GP Draft Statements\" page and populate grid for \"Subcommunity\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 17
  testRunner.Then("Column Program Code value is \"null\" for Page \"GP Draft Statements\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Program Code Value for Entity Level Fleet User")]
        [NUnit.Framework.CategoryAttribute("ProgramCodeValidation")]
        [NUnit.Framework.CategoryAttribute("CON-25750")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        [NUnit.Framework.CategoryAttribute("Funtional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        public void VerifyProgramCodeValueForEntityLevelFleetUser()
        {
            string[] tagsOfScenario = new string[] {
                    "ProgramCodeValidation",
                    "CON-25750",
                    "18.0",
                    "Funtional",
                    "Smoke"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Program Code Value for Entity Level Fleet User", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 26
     testRunner.Given("User \"Fleet\" is on \"GP Draft Statements\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 27
  testRunner.And("Program CodeToken is \"Active\" for Program Code and \"Inactive\" for Subcommunity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 28
  testRunner.When("User is on \"GP Draft Statements\" page and populate grid for \"Subcommunity\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 29
  testRunner.Then("Column Program Code value is \"Entity level PC values\" for Page \"GP Draft Statemen" +
                        "ts\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Program Code Value for Enrollment Created Fleet User")]
        [NUnit.Framework.CategoryAttribute("ProgramCodeValidation")]
        [NUnit.Framework.CategoryAttribute("CON-25750")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        [NUnit.Framework.CategoryAttribute("Funtional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        public void VerifyProgramCodeValueForEnrollmentCreatedFleetUser()
        {
            string[] tagsOfScenario = new string[] {
                    "ProgramCodeValidation",
                    "CON-25750",
                    "18.0",
                    "Funtional",
                    "Smoke"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Program Code Value for Enrollment Created Fleet User", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 38
      testRunner.Given("User \"Fleet\" is on \"GP Draft Statements\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 39
   testRunner.And("Program CodeToken is \"Active\" for Program Code and \"Inactive\" for Subcommunity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 40
   testRunner.When("User is on \"GP Draft Statements\" page and populate grid for \"Subcommunity\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 41
   testRunner.Then("Column Program Code value is \"Enrollment PC Value\" for Page \"GP Draft Statements\"" +
                        "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
