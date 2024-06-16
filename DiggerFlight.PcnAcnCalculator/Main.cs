﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiggerFlight.PcnAcnCalculator
{
    using DiggerFlight.PcnAcnCalculator.Calculation;
    using Models;

    public class Main
    {
        public static string GetAircraftInvalidTaxiWaysInKg(string ICAO, string aircraftWeight, string simbriefAircraftId)
        {
            try
            {
                var aircraftAcns = AircraftAcns.GetAircraftAcns();
                var airfieldOps = AirfieldOperations.GetAirfieldOperations();

                var aircraftWeightInKn = UnitsConverter.GetAircraftWeighInKn(aircraftWeight);

                var aircraftAcn = aircraftAcns.AircraftAcn.Where(t => t.SimbriefId.ToUpper() == simbriefAircraftId.ToUpper()).FirstOrDefault();
                var airfield = airfieldOps.Airfields.Where(t => t.Icao.ToUpper() == ICAO.ToUpper()).FirstOrDefault();
                var pcnPart = PcnParts.GetPcnParts(airfield.Infrastructure);

                List<string> invalidTaxiWays = new List<string>();
                foreach (var infrastructureElement in pcnPart)
                {
                    PcnParts pcnParts = infrastructureElement;
                    var isInfrastructureElementValid = TyrePresureCheck.AircraftTyrePresureIsWithinLimits(ref pcnParts, ref aircraftAcn, ref aircraftWeightInKn);
                    if (!isInfrastructureElementValid)
                    {
                        invalidTaxiWays.Add(infrastructureElement.InfrastructureName);
                    }
                }
                return string.Join(",", invalidTaxiWays);
            }
            catch 
            {
                return string.Empty;
            }
        }
    }
}
