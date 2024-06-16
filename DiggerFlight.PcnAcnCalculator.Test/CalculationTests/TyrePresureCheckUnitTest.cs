namespace DiggerFlight.PcnAcnCalculator.Test.CalculationTests
{
    using DiggerFlight.PcnAcnCalculator.Models;
    using DiggerFlight.PcnAcnCalculator.Calculation;
    using FluentAssertions;

    public class TyrePresureCheckUnitTest
    {
        [Theory]
        [InlineData(null, PcnParts.MaximumTyrePresureType.Z, true)]
        [InlineData(0.49, PcnParts.MaximumTyrePresureType.Z, true)]
        [InlineData(0.50, PcnParts.MaximumTyrePresureType.Z, true)]
        [InlineData(0.51, PcnParts.MaximumTyrePresureType.Z, false)]
        [InlineData(1.249, PcnParts.MaximumTyrePresureType.Z, false)]
        [InlineData(1.249, PcnParts.MaximumTyrePresureType.Y, true)]
        [InlineData(1.250, PcnParts.MaximumTyrePresureType.Y, true)]
        [InlineData(1.251, PcnParts.MaximumTyrePresureType.Y, false)]
        [InlineData(1.749, PcnParts.MaximumTyrePresureType.Y, false)]
        [InlineData(1.749, PcnParts.MaximumTyrePresureType.X, true)]
        [InlineData(1.750, PcnParts.MaximumTyrePresureType.X, true)]
        [InlineData(1.751, PcnParts.MaximumTyrePresureType.X, false)]
        [InlineData(2.0, PcnParts.MaximumTyrePresureType.X, false)]
        [InlineData(2.0, PcnParts.MaximumTyrePresureType.W, true)]
        [InlineData(0.2, PcnParts.MaximumTyrePresureType.W, true)]
        [InlineData(-0.2, PcnParts.MaximumTyrePresureType.W, true)]
        [InlineData(2.0, null, true)]
        [InlineData(0.2, null, true)]
        [InlineData(null, null, true)]
        public void TestAircraftTyrePresureIsWithinLimits(double? aircraftTyrePresure, PcnParts.MaximumTyrePresureType? pcnMaximumTyrePresure, bool expectedResult)
        {
            PcnParts pcnParts = new PcnParts() { MaximumTyrePresure = pcnMaximumTyrePresure};
            Aircraftacn aircraftacn = new Aircraftacn() { TirePressureMpa = aircraftTyrePresure };
            double? actualAircraftWeighInKg = 1.0;
            var tyrePresureResult = TyrePresureCheck.AircraftTyrePresureIsWithinLimits(ref pcnParts, ref aircraftacn, ref actualAircraftWeighInKg);
            tyrePresureResult.Should().Be(expectedResult);
        }
    }
}
