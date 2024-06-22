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
namespace DiggerFlight.PcnAcnCalculator.Test
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class CheckForInvalidInfrastructreFeature : object, Xunit.IClassFixture<CheckForInvalidInfrastructreFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "CheckForInvalidInfrastructre.feature"
#line hidden
        
        public CheckForInvalidInfrastructreFeature(CheckForInvalidInfrastructreFeature.FixtureData fixtureData, DiggerFlight_PcnAcnCalculator_Test_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "CheckForInvalidInfrastructre", "\tAs an MSFS user and AI Air Trafic Control User\r\n\tI Want Air Trafic Control to kn" +
                    "ow what Infrastructure is invalid for my aircraft type\r\n\tSo that I don\'t receive" +
                    " taxi requests via invalid areas similarly to the real world, unless data is mis" +
                    "sing", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableTheoryAttribute(DisplayName="Check For Invalid Taxiways only when valid data is available")]
        [Xunit.TraitAttribute("FeatureTitle", "CheckForInvalidInfrastructre")]
        [Xunit.TraitAttribute("Description", "Check For Invalid Taxiways only when valid data is available")]
        [Xunit.InlineDataAttribute("Valid data", "A320", "EGNJ", "44000kgs", "TAXI_B,TAXI_E,TAXI_G,TAXI_H", new string[0])]
        [Xunit.InlineDataAttribute("Valid data", "A320", "EGNJ", "44000kg", "TAXI_B,TAXI_E,TAXI_G,TAXI_H", new string[0])]
        [Xunit.InlineDataAttribute("Valid data", "A320", "EGNJ", "97000lbs", "TAXI_B,TAXI_E,TAXI_G,TAXI_H", new string[0])]
        [Xunit.InlineDataAttribute("Valid data", "A320", "EGNJ", "88000lb", "TAXI_E,TAXI_G,TAXI_H", new string[0])]
        [Xunit.InlineDataAttribute("Valid data no invalid Taxiways", "C152", "EGNJ", "2499kgs", "", new string[0])]
        [Xunit.InlineDataAttribute("Valid data invalid Taxiways", "C152", "EGNJ", "2501kgs", "TAXI_G,TAXI_H", new string[0])]
        [Xunit.InlineDataAttribute("Invalid data - aircraft", "B747", "EGNJ", "44000kgs", "", new string[0])]
        [Xunit.InlineDataAttribute("Invalid data - airfield", "A320", "EGKK", "44000kgs", "", new string[0])]
        [Xunit.InlineDataAttribute("Invalid data - weight", "A320", "EGNJ", "", "", new string[0])]
        [Xunit.InlineDataAttribute("Invalid data - all", "B747", "EGKK", "", "", new string[0])]
        public void CheckForInvalidTaxiwaysOnlyWhenValidDataIsAvailable(string scenario, string aircraft, string icao, string mass, string taxiwaysAreInvalid, string[] exampleTags)
        {
            string[] tagsOfScenario = exampleTags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("scenario", scenario);
            argumentsOfScenario.Add("aircraft", aircraft);
            argumentsOfScenario.Add("icao", icao);
            argumentsOfScenario.Add("mass", mass);
            argumentsOfScenario.Add("taxiways are invalid", taxiwaysAreInvalid);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Check For Invalid Taxiways only when valid data is available", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 6
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
 testRunner.Given(string.Format("that I am an \'{0}\' at \'{1}\' and \'{2}\'", aircraft, icao, mass), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 8
 testRunner.When("I ask for taxi", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 9
 testRunner.Then(string.Format("If data is valid I want to know what \'{0}\'", taxiwaysAreInvalid), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                CheckForInvalidInfrastructreFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                CheckForInvalidInfrastructreFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
