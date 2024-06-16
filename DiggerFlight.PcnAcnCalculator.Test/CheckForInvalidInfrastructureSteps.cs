namespace DiggerFlight.PcnAcnCalculator.Test
{
    using FluentAssertions;
    using TechTalk.SpecFlow;

    [Binding]
    public class CheckForInvalidInfrastructureSteps
    {
        private string? _aircraft;
        private string? _icao;
        private string? _mass;

	    [Given(@"^that I am an '(.*)' at '(.*)' and '(.*)'$")]
        public void GivenThatIamAnAtAnd(string aircraft, string icao, string mass)
        {
            _aircraft = aircraft;
            _icao = icao;
            _mass = mass;
        }

        [When(@"^I ask for taxi")]
        public void WhenIaskForTaxi()
        {
            //No codeNeeded
        }

        [Then(@"^If data is valid I want to know what '(.*)'$")]
        public void WhenIfDataIsValidIwantToKnowWhat(string taxiwaysAreInvalid)
        {
            var result = Main.GetAircraftInvalidTaxiWaysInKg(_icao, _mass, _aircraft);
            result.Should().Be(taxiwaysAreInvalid);
        }
    }
}
