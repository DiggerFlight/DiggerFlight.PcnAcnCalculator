namespace DiggerFlight.PcnAcnCalculator.Calculation
{
    using FluentAssertions;
    using System;

    public class AllowableLoadVersesAircraftWeight
    {
        public static bool AircraftWeightIsAllowedForLoad(ref double? aircraftActualWeight,
                                                          double? aircraftMinWeight,
                                                          double? aircraftMaxWeight,
                                                          double? pcn,
                                                          double? acnMin,
                                                          double? acnMax
                                                          )
        {
            try
            {
                aircraftActualWeight.Should().NotBeNull();
                aircraftMinWeight.Should().NotBeNull();
                aircraftMaxWeight.Should().NotBeNull();
                pcn.Should().NotBeNull();
                acnMin.Should().NotBeNull();
                acnMax.Should().NotBeNull();
            }
            catch
            {
                return true;
            }

            aircraftActualWeight = Math.Abs(aircraftActualWeight.GetValueOrDefault(2.0));
            //Load = 2.5 (>=3.75 or <=2.75)
            var load = LoadOrActualAcnFormula(aircraftMinWeight,
                                              aircraftMaxWeight,
                                              pcn,
                                              acnMin,
                                              acnMax);

            if (aircraftActualWeight >= load * 1.5) { return false; }
            {
                if (aircraftActualWeight <= load * 1.1) { return true; }
                //Calculate Equivalent daily traffic <= 10 per day is a pass
                //These values are unknown in MSFS 2020 therefore
            }
            return true;
        }

        public static double? LoadOrActualAcnFormula(double? equationMinValue,
                                                     double? equationMaxValue,
                                                     double? fractionValue,
                                                     double? fractionMinValue,
                                                     double? fractionMaxValue
                                                     )
        {
            try
            {
                equationMinValue.Should().NotBeNull();
                equationMaxValue.Should().NotBeNull();
                fractionValue.Should().NotBeNull();
                fractionMinValue.Should().NotBeNull();
                fractionMaxValue.Should().NotBeNull();
            }
            catch
            {
                throw new Exception("Can't Calculate actual aircraft ACN or Load");
            }

            //Defaults won't be triggered just included to help analyse equation but need to ensure positive values.
            equationMinValue = Math.Abs(equationMinValue.GetValueOrDefault(1.0));
            equationMaxValue = Math.Abs(equationMaxValue.GetValueOrDefault(4.0));
            fractionValue = Math.Abs(fractionValue.GetValueOrDefault(2.0));
            fractionMinValue = Math.Abs(fractionMinValue.GetValueOrDefault(1.0));
            fractionMaxValue = Math.Abs(fractionMaxValue.GetValueOrDefault(3.0));
            //Load = 2.5
            //Log.Information("aircraftMinWeightOrAcnMin: " + aircraftMinWeightOrAcnMin);
            //Log.Information("aircraftMaxWeightOrAcnMax: " + aircraftMaxWeightOrAcnMax);
            //Log.Information("pcnOrmValue: " + pcnOrmValue);
            //Log.Information("acnMinOrmValueMin: " + acnMinOrmValueMin);
            //Log.Information("acnMaxOrmValueMax: " + acnMaxOrmValueMax);

            var fraction = (fractionValue - fractionMinValue) / (fractionMaxValue - fractionMinValue);
            var loadOrActualAcnFormula = equationMinValue + ((equationMaxValue - equationMinValue) * fraction);

            //Log.Information("loadOrActualAcnFormula: " + loadOrActualAcnFormula);
            return loadOrActualAcnFormula;
        }
    }
}
