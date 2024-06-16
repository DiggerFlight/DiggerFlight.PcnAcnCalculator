namespace DiggerFlight.PcnAcnCalculator.Test.CalculationTests
{
    using DiggerFlight.PcnAcnCalculator.Calculation;
    using FluentAssertions;

    public class AllowableLoadVersesAircraftWeightUnitTest
    {
        [Theory]
        [InlineData(3.76, 1.0, 4.0, 2.0, 1.0, 3.0, false)]
        [InlineData(3.75, 1.0, 4.0, 2.0, 1.0, 3.0, false)]
        [InlineData(3.74, 1.0, 4.0, 2.0, 1.0, 3.0, true)]
        [InlineData(2.76, 1.0, 4.0, 2.0, 1.0, 3.0, true)]
        [InlineData(2.75, 1.0, 4.0, 2.0, 1.0, 3.0, true)]
        [InlineData(2.74, 1.0, 4.0, 2.0, 1.0, 3.0, true)]
        [InlineData(null, 1.0, 4.0, 2.0, 1.0, 3.0, true)]
        [InlineData(2.74, null, 4.0, 2.0, 1.0, 3.0, true)]
        [InlineData(2.74, 1.0, null, 2.0, 1.0, 3.0, true)]
        [InlineData(2.74, 1.0, 4.0, null, 1.0, 3.0, true)]
        [InlineData(2.74, 1.0, 4.0, 2.0, null, 3.0, true)]
        [InlineData(2.74, 1.0, 4.0, 2.0, 1.0, null, true)]
        public void TestAircraftWeightIsAllowedForLoad(double? aircraftActualWeight,
                                                       double? aircraftMinWeight,
                                                       double? aircraftMaxWeight,
                                                       double? pcn,
                                                       double? acnMin,
                                                       double? acnMax,
                                                       bool expectedResult
                                                       )
        {
            var aircraftWeightAlowance = AllowableLoadVersesAircraftWeight.AircraftWeightIsAllowedForLoad(ref aircraftActualWeight,
                                                                                                          aircraftMinWeight,
                                                                                                          aircraftMaxWeight,
                                                                                                          pcn,
                                                                                                          acnMin,
                                                                                                          acnMax);
            aircraftWeightAlowance.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1.0, 4.0, 2.0, 1.0, 3.0, 2.5)]
        [InlineData(null, 4.0, 2.0, 1.0, 3.0, 0.0)]
        [InlineData(1.0, null, 2.0, 1.0, 3.0, 0.0)]
        [InlineData(1.0, 4.0, null, 1.0, 3.0, 0.0)]
        [InlineData(1.0, 4.0, 2.0, null, 3.0, 0.0)]
        [InlineData(1.0, 4.0, 2.0, 1.0, null, 0.0)]
        public void TestLoadOrActualAcnFormula(double? aircraftMinWeightOrAcnMin,
                                               double? aircraftMaxWeightOrAcnMax,
                                               double? pcnOrmValue,
                                               double? acnMinOrmValueMin,
                                               double? acnMaxOrmValueMax,
                                               double expectedResult
                                               )
        {
            double? loadOrActualAcnFormula = 0.0;
            try
            {
                loadOrActualAcnFormula = AllowableLoadVersesAircraftWeight.LoadOrActualAcnFormula(aircraftMinWeightOrAcnMin,
                                                                                                  aircraftMaxWeightOrAcnMax,
                                                                                                  pcnOrmValue,
                                                                                                  acnMinOrmValueMin,
                                                                                                  acnMaxOrmValueMax);
                loadOrActualAcnFormula.Should().Be(expectedResult);
            }
            catch (Exception e)
            {
                loadOrActualAcnFormula.Should().Be(expectedResult);
                e.Message.Should().Be("Can't Calculate actual aircraft ACN or Load");
            }
        }
    }
}
