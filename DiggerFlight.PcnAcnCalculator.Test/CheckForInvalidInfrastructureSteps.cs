using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiggerFlight.PcnAcnCalculator.Test
{
    [Binding]
    public class CheckForInvalidInfrastructureSteps
    {
        private string _aircraft;
        private string _icao;
        private string _weight;

	    [Given(@"^that I am an '(.*)' at '(.*)' and '(.*)'$")]
        public void GivenThatIamAnAtAnd(string aircraft, string icao, string weight)
        {
            _aircraft = aircraft;
            _icao = icao;
            _weight = weight;
        }

        [When(@"^I ask for taxi")]
        public void WhenIaskForTaxi()
        {
            //No codeNeeded
        }

        [Then(@"^If data is valid I want to know what '(.*)'$")]
        public void WhenIfDataIsValidIwantToKnowWhat(string taxiwaysAreInvalid)
        {
            double? weight = null;
            if (!string.IsNullOrEmpty(_weight)) weight = Convert.ToDouble(_weight.Replace("kgs",""));
            var result = Main.GetAircraftInvalidTaxiWaysInKg(_icao, weight, _aircraft);
            result.Should().Be(taxiwaysAreInvalid);
        }
    }
}
