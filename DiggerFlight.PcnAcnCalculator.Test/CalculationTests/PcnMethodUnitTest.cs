namespace DiggerFlight.PcnAcnCalculator.Test.CalculationTests
{
    using DiggerFlight.PcnAcnCalculator.Models;
    using DiggerFlight.PcnAcnCalculator.Calculation;
    using FluentAssertions;
    using static DiggerFlight.PcnAcnCalculator.Models.PcnParts;

    public class PcnMethodUnitTest
    {
        [Theory]
        [InlineData(PcnParts.PcnDeterminedType.T)]
        [InlineData(null)]
        public void PcnMethodIsAuthorisedTestForTrue(PcnParts.PcnDeterminedType? pcnDeterminedType)
        {
            PcnParts pcn = new PcnParts()
            {
                PcnDetermined = pcnDeterminedType,
                PcnNumericalValue = 2.0
            };
            var pcnMethodResult = PcnMethod.PcnMethodIsAuthorised(ref pcn, 3.74, 1.0, 4.0, 1.0, 3.0);
            pcnMethodResult.Should().BeTrue();
        }
        
        [Fact]
        public void PcnMethodIsAuthorisedTestForFalse()
        {
            PcnParts pcn = new PcnParts()
            {
                PcnDetermined = PcnDeterminedType.U,
                PcnNumericalValue = 2.0
            };
            var pcnMethodResult = PcnMethod.PcnMethodIsAuthorised(ref pcn, 3.74, 1.0, 4.0, 1.0, 3.0);
            pcnMethodResult.Should().BeFalse();
        }
    }
}
