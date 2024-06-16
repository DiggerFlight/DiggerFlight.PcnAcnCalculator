namespace DiggerFlight.PcnAcnCalculator.Calculation
{
    using DiggerFlight.PcnAcnCalculator.Models;
    using FluentAssertions;
    using System;
    using static Models.PcnParts;
    public class TyrePresureCheck
    {
        public static bool AircraftTyrePresureIsWithinLimits(ref PcnParts? pcnParts, ref Aircraftacn? aircraftacn, ref double? actualAircraftWeighInKn)
        {
            try
            {
                var aircraftTyrePresure = Math.Abs(aircraftacn.TirePressureMpa.GetValueOrDefault(0.0));
                actualAircraftWeighInKn.Should().NotBeNull();
                var aircraftWeight = actualAircraftWeighInKn.GetValueOrDefault(0.0);
                var pcnTyrePresure = 0.5;
                switch (pcnParts.MaximumTyrePresure)
                {
                    case MaximumTyrePresureType.X:
                        pcnTyrePresure = 1.75;
                        break;
                    case MaximumTyrePresureType.Y:
                        pcnTyrePresure = 1.25;
                        break;
                    case MaximumTyrePresureType.Z:
                        pcnTyrePresure = 0.5;
                        break;
                    case MaximumTyrePresureType.W:
                    //No Tyre Presure Limit
                    default:
                        //Equivalent to N/A
                        return CalculationOfActualAircraftAcn.IsActualAircraftAcnAllowed(ref pcnParts, ref aircraftacn, ref aircraftWeight);
                }
                if (aircraftTyrePresure <= pcnTyrePresure)
                {
                    return CalculationOfActualAircraftAcn.IsActualAircraftAcnAllowed(ref pcnParts, ref aircraftacn, ref aircraftWeight);
                }
                else return false;
            }
            catch
            {
                //Invalid Data therefore revert to default i.e. alowed
                return true;
            }
        }
    }
}