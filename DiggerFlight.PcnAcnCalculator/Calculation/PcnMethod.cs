namespace DiggerFlight.PcnAcnCalculator.Calculation
{
    using DiggerFlight.PcnAcnCalculator.Models;
    using static Models.PcnParts;
    public class PcnMethod
    {
        public static bool PcnMethodIsAuthorised(ref PcnParts? pcnParts, 
                                                 double? aircraftActualWeight,
                                                 double? aircraftMinWeight,
                                                 double? aircraftMaxWeight,
                                                 double? acnMin,
                                                 double? acnMax
                                                 )
        {
            if (pcnParts.PcnDetermined == PcnDeterminedType.U) return false;
            return AllowableLoadVersesAircraftWeight.AircraftWeightIsAllowedForLoad(ref aircraftActualWeight,
                                                                                    aircraftMinWeight,
                                                                                    aircraftMaxWeight,
                                                                                    pcnParts.PcnNumericalValue,
                                                                                    acnMin,
                                                                                    acnMax
                                                                                    );
        }
    }
}
