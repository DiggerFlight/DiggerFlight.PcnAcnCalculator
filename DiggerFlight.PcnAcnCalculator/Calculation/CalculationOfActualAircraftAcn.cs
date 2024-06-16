namespace DiggerFlight.PcnAcnCalculator.Calculation
{
    using DiggerFlight.PcnAcnCalculator.Models;
    using System;
    using System.Text.RegularExpressions;
    using static Models.PcnParts;
    public class CalculationOfActualAircraftAcn
    {
        public static bool IsActualAircraftAcnAllowed(ref PcnParts? pcnParts, ref Aircraftacn? aircraftacn, ref double actualAircraftWeighInKg)
        {
            if (pcnParts.NonDecodedPcnValue.Contains("/"))
            {
                var actualAircraftAcn = GetActualAircraftAcn(ref pcnParts, ref aircraftacn, ref actualAircraftWeighInKg);
                var pcnToCheckAgainst = pcnParts.PcnNumericalValue;
                var acnMaxValue = GetAcnMaxMinValues(ref pcnParts, ref aircraftacn, true);
                var acnMinValue = GetAcnMaxMinValues(ref pcnParts, ref aircraftacn, false);

                if (actualAircraftAcn <= pcnToCheckAgainst) { return true; }
                else
                {
                    if (pcnParts.Rigidity == RigidityType.R)
                    {
                        if (actualAircraftAcn <= pcnToCheckAgainst * 1.05) { return true; }
                    }
                    else
                    {
                        if (actualAircraftAcn <= pcnToCheckAgainst * 1.1) { return true; }
                    }
                }
                return PcnMethod.PcnMethodIsAuthorised(ref pcnParts,
                                                       actualAircraftWeighInKg,
                                                       aircraftacn.MinWeightValues.WeightKn,
                                                       aircraftacn.MaxWeightValues.WeightKn,
                                                       acnMinValue, acnMaxValue
                                                       );
            }
            else
            {
                var pattern = @"\d"; // This pattern will match the decimal
                Match match = Regex.Match(pcnParts.NonDecodedPcnValue, pattern);
                if (match.Success)
                {
                    var maxWeigh = Math.Abs(Convert.ToDouble(match.Value));
                    if (actualAircraftWeighInKg <= maxWeigh) { return true; }
                    return false;
                }
                //Not valid PCN data revert to default
                return true;
            }
        }

        public static double? GetActualAircraftAcn(ref PcnParts? pcnParts, ref Aircraftacn? aircraftacn, ref double actualAircraftWeighInKg)
        {
            try
            {
                var acnMaxValue = GetAcnMaxMinValues(ref pcnParts, ref aircraftacn, true);
                var acnMinValue = GetAcnMaxMinValues(ref pcnParts, ref aircraftacn, false);
                double? aircraftMaxWeight = aircraftacn.MaxWeightValues.WeightKn;
                double? aircraftMinWeight = aircraftacn.MinWeightValues.WeightKn;

                var actualAircraftAcn = AllowableLoadVersesAircraftWeight.LoadOrActualAcnFormula(aircraftMinWeight,
                                                                                                 aircraftMaxWeight,
                                                                                                 actualAircraftWeighInKg,
                                                                                                 acnMinValue,
                                                                                                 acnMaxValue
                                                                                                 );
                return actualAircraftAcn;
            }
            catch
            {
                throw new Exception("Can't Calculate actual aircraft ACN");
            }
        }

        public static double? GetAcnMaxMinValues(ref PcnParts? pcnParts, ref Aircraftacn? aircraftacn, bool isMaxValue = false)
        {
            try 
            {
                double? acnToUse = null;             
                if (pcnParts.Rigidity == RigidityType.R)
                {
                    switch (pcnParts.Strengh)
                    {
                        case StrenghType.A:
                            if (isMaxValue) acnToUse = aircraftacn.MaxWeightValues.RigidSubgradeHigh;
                            else acnToUse = aircraftacn.MinWeightValues.RigidSubgradeHigh;
                            break;
                        case StrenghType.B:
                            if (isMaxValue) acnToUse = aircraftacn.MaxWeightValues.RigidSubgradeMedium;
                            else acnToUse = aircraftacn.MinWeightValues.RigidSubgradeMedium;
                            break;
                        case StrenghType.C:
                            if (isMaxValue) acnToUse = aircraftacn.MaxWeightValues.RigidSubgradeLow;
                            else acnToUse = aircraftacn.MinWeightValues.RigidSubgradeLow;
                            break;
                        case StrenghType.D:
                            if (isMaxValue) acnToUse = aircraftacn.MaxWeightValues.RigidSubgradeVeryLow;
                            else acnToUse = aircraftacn.MinWeightValues.RigidSubgradeVeryLow;
                            break;
                    }
                }
                else
                {
                    switch (pcnParts.Strengh)
                    {
                        case StrenghType.A:
                            if (isMaxValue) acnToUse = aircraftacn.MaxWeightValues.FlexSubgradeHigh;
                            else acnToUse = aircraftacn.MinWeightValues.FlexSubgradeHigh;
                            break;
                        case StrenghType.B:
                            if (isMaxValue) acnToUse = aircraftacn.MaxWeightValues.FlexSubgradeMedium;
                            else acnToUse = aircraftacn.MinWeightValues.FlexSubgradeMedium;
                            break;
                        case StrenghType.C:
                            if (isMaxValue) acnToUse = aircraftacn.MaxWeightValues.FlexSubgradeLow;
                            else acnToUse = aircraftacn.MinWeightValues.FlexSubgradeLow;
                            break;
                        case StrenghType.D:
                            if (isMaxValue) acnToUse = aircraftacn.MaxWeightValues.FlexSubgradeVeryLow;
                            else acnToUse = aircraftacn.MinWeightValues.FlexSubgradeVeryLow;
                            break;
                    }
                }
                return acnToUse;
            }
            catch 
            {
                throw new Exception("ACN config missing your aircraft data");
            }
        }

        public static bool IsAircraftAcnValueAuthorized(double? acnActualValue, double? pcnValue, RigidityType? rigidity)
        {
            acnActualValue = Math.Abs(acnActualValue.GetValueOrDefault(0.0));
            pcnValue = Math.Abs(pcnValue.GetValueOrDefault((double)acnActualValue + 1.0));

            var pcnValueToleranceCodeF = pcnValue * 1.1;
            var pcnValueToleranceCodeR = pcnValue * 1.05;
            if (acnActualValue <= pcnValue) { return true; }
            else
            {
                switch (rigidity) 
                {
                    case RigidityType.R:
                        if (acnActualValue <= pcnValueToleranceCodeR) { return true; }
                        break;
                    case RigidityType.F:
                    default:
                        if (acnActualValue <= pcnValueToleranceCodeF) { return true; }
                        break;
                }
            }
            return false;
        }
    }
}
