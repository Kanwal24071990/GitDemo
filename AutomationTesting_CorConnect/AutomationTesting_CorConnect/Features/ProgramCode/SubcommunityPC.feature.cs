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
namespace AutomationTesting_CorConnect.Features.ProgramCode
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Sub Community Program Code")]
    public partial class SubCommunityProgramCodeFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
#line 1 "SubcommunityPC.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features/ProgramCode", "Sub Community Program Code", "As a User\r\nI will verify \r\nGrid Column value for Program Code by Subcommunity", ProgrammingLanguage.CSharp, featureTags);
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
        [NUnit.Framework.DescriptionAttribute("Verify Program Code Token Value for Subcommunity Level Assignment for Pages")]
        [NUnit.Framework.CategoryAttribute("ProgramCodeValidationbySubcommunity")]
        [NUnit.Framework.CategoryAttribute("CON-25750")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        [NUnit.Framework.CategoryAttribute("Funtional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        [NUnit.Framework.CategoryAttribute("IndSCF")]
        [NUnit.Framework.TestCaseAttribute("Dealer Locations", null)]
        [NUnit.Framework.TestCaseAttribute("Fleet Locations", null)]
        [NUnit.Framework.TestCaseAttribute("GP Draft Statements", null)]
        [NUnit.Framework.TestCaseAttribute("Draft Statement Report", null)]
        public void VerifyProgramCodeTokenValueForSubcommunityLevelAssignmentForPages(string page, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ProgramCodeValidationbySubcommunity",
                    "CON-25750",
                    "18.0",
                    "Funtional",
                    "Smoke",
                    "IndSCF"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Page", page);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Program Code Token Value for Subcommunity Level Assignment for Pages", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
    testRunner.Given(string.Format("User \"Admin\" is on \"{0}\" page", page), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 15
 testRunner.And("Program CodeToken is \"Active\" for Program Code and \"Active\" for Subcommunity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 16
 testRunner.When(string.Format("User is on \"{0}\" page and populate grid for \"Subcommunity\"", page), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 17
 testRunner.Then(string.Format("Column Program Code value is \"PC Values\" for Page \"{0}\"", page), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Verify Program Code Token Value for Subcommunity Level Assignment for Enrollment " +
            "Page")]
        [NUnit.Framework.CategoryAttribute("ProgramCodeValidationbySubcommunity")]
        [NUnit.Framework.CategoryAttribute("CON-25750")]
        [NUnit.Framework.CategoryAttribute("18.0")]
        [NUnit.Framework.CategoryAttribute("Funtional")]
        [NUnit.Framework.CategoryAttribute("Smoke")]
        public void VerifyProgramCodeTokenValueForSubcommunityLevelAssignmentForEnrollmentPage()
        {
            string[] tagsOfScenario = new string[] {
                    "ProgramCodeValidationbySubcommunity",
                    "CON-25750",
                    "18.0",
                    "Funtional",
                    "Smoke"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Verify Program Code Token Value for Subcommunity Level Assignment for Enrollment " +
                    "Page", null, tagsOfScenario, argumentsOfScenario, featureTags);
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
#line 31
    testRunner.Given("User \"Admin\" is on \"Enrollment\" popup page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 32
 testRunner.And("Program CodeToken is \"Active\" for Program Code and \"Active\" for Subcommunity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 33
 testRunner.When("User is on \"Enrollment\" page and populate grid for \"Subcommunity\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 34
 testRunner.Then("Column Program Code value is \"PC Values\" for Page \"Enrollment\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
