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

        public static double? LoadOrActualAcnFormula(double? aircraftMinWeightOrAcnMin,
                                                     double? aircraftMaxWeightOrAcnMax,
                                                     double? pcnOrmValue,
                                                     double? acnMinOrmValueMin,
                                                     double? acnMaxOrmValueMax,
                                                     bool isAcnCalculation = false
                                                     )
        {
            try
            {
                aircraftMinWeightOrAcnMin.Should().NotBeNull();
                aircraftMaxWeightOrAcnMax.Should().NotBeNull();
                pcnOrmValue.Should().NotBeNull();
                acnMinOrmValueMin.Should().NotBeNull();
                acnMaxOrmValueMax.Should().NotBeNull();
            }
            catch
            {
                throw new Exception("Can't Calculate actual aircraft ACN or Load");
            }

            //Defaults won't be triggered just included to help analyse equation but need to ensure positive values.
            aircraftMinWeightOrAcnMin = Math.Abs(aircraftMinWeightOrAcnMin.GetValueOrDefault(1.0));
            aircraftMaxWeightOrAcnMax = Math.Abs(aircraftMaxWeightOrAcnMax.GetValueOrDefault(4.0));
            pcnOrmValue = Math.Abs(pcnOrmValue.GetValueOrDefault(2.0));
            acnMinOrmValueMin = Math.Abs(acnMinOrmValueMin.GetValueOrDefault(1.0));
            acnMaxOrmValueMax = Math.Abs(acnMaxOrmValueMax.GetValueOrDefault(3.0));
            //Load = 2.5
            //Log.Information("aircraftMinWeightOrAcnMin: " + aircraftMinWeightOrAcnMin);
            //Log.Information("aircraftMaxWeightOrAcnMax: " + aircraftMaxWeightOrAcnMax);
            //Log.Information("pcnOrmValue: " + pcnOrmValue);
            //Log.Information("acnMinOrmValueMin: " + acnMinOrmValueMin);
            //Log.Information("acnMaxOrmValueMax: " + acnMaxOrmValueMax);

            var fraction = (pcnOrmValue - acnMinOrmValueMin) / (acnMaxOrmValueMax - acnMinOrmValueMin);
            var loadOrActualAcnFormula = aircraftMinWeightOrAcnMin + ((aircraftMaxWeightOrAcnMax - aircraftMinWeightOrAcnMin) * fraction);
            //Sub optimal to properly fixed ASSP
            if (isAcnCalculation)
            {
                fraction = (pcnOrmValue - aircraftMinWeightOrAcnMin) / (aircraftMaxWeightOrAcnMax - aircraftMinWeightOrAcnMin);
                loadOrActualAcnFormula = acnMinOrmValueMin + ((acnMaxOrmValueMax - acnMinOrmValueMin) * fraction);
            }
            //Log.Information("loadOrActualAcnFormula: " + loadOrActualAcnFormula);
            return loadOrActualAcnFormula;
        }
    }
}
