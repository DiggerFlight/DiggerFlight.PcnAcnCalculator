namespace DiggerFlight.PcnAcnCalculator.Test.CalculationTests
{
    using DiggerFlight.PcnAcnCalculator.Calculation;
    using FluentAssertions;
    using static DiggerFlight.PcnAcnCalculator.Models.PcnParts;

    public class CalculationOfActualAircraftAcnUnitTest
    {
        [Theory]
        [InlineData(2.09, null, RigidityType.R, true)]
        [InlineData(null, 2.0, RigidityType.R, true)]
        [InlineData(null, null, RigidityType.R, true)]
        [InlineData(2.09, 0.0, RigidityType.R, false)]
        [InlineData(2.09, 2.0, RigidityType.R, true)]
        [InlineData(2.10, 2.0, RigidityType.R, true)]
        [InlineData(-2.10, 2.0, RigidityType.R, true)]
        [InlineData(2.10, -2.0, RigidityType.R, true)]
        [InlineData(-2.10, -2.0, RigidityType.R, true)]
        [InlineData(2.11, 2.0, RigidityType.R, false)]

        [InlineData(2.19, null, RigidityType.F, true)]
        [InlineData(null, 2.0, RigidityType.F, true)]
        [InlineData(null, null, RigidityType.F, true)]
        [InlineData(2.19, 0.0, RigidityType.F, false)]
        [InlineData(2.19, 2.0, RigidityType.F, true)]
        [InlineData(2.20, 2.0, RigidityType.F, true)]
        [InlineData(-2.20, 2.0, RigidityType.F, true)]
        [InlineData(2.20, -2.0, RigidityType.F, true)]
        [InlineData(-2.20, -2.0, RigidityType.F, true)]
        [InlineData(2.21, 2.0, RigidityType.F, false)]

        [InlineData(2.19, null, null, true)]
        [InlineData(null, 2.0, null, true)]
        [InlineData(null, null, null, true)]
        [InlineData(2.19, 0.0, null, false)]
        [InlineData(2.19, 2.0, null, true)]
        [InlineData(2.20, 2.0, null, true)]
        [InlineData(-2.20, 2.0, null, true)]
        [InlineData(2.20, -2.0, null, true)]
        [InlineData(-2.20, -2.0, null, true)]
        [InlineData(2.21, 2.0, null, false)]
        public void TestIsAircraftAcnValueAuthorized(double? acnActualValue, double? pcnValue, RigidityType? rigidity, bool expectedResult)
        {
            var acnAuthorisedValue = CalculationOfActualAircraftAcn.IsAircraftAcnValueAuthorized(acnActualValue, pcnValue, rigidity);
            acnAuthorisedValue.Should().Be(expectedResult);
        }
    }
}
