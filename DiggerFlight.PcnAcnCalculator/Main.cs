using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiggerFlight.PcnAcnCalculator
{
    using DiggerFlight.PcnAcnCalculator.Calculation;
    using FluentAssertions;
    using Models;
    using FileManagement;

    public class Main
    {
        public static string GetAircraftInvalidTaxiWaysInKg(string icao, string aircraftWeight, string simbriefAircraftId)
        {
            try
            {
                icao.Should().NotBeNullOrEmpty();
                aircraftWeight.Should().NotBeNullOrEmpty();
                simbriefAircraftId.Should().NotBeNullOrEmpty();

                var aircraftAcns = AircraftAcns.GetAircraftAcns();
                var airfieldOps = AirfieldOperations.GetAirfieldOperations();

                var aircraftWeightInKn = UnitsConverter.GetAircraftWeighInKn(aircraftWeight);

                var aircraftAcn = aircraftAcns.AircraftAcn.Where(t => t.SimbriefId.ToUpper() == simbriefAircraftId.ToUpper()).FirstOrDefault();
                var airfield = airfieldOps.Airfields.Where(t => t.Icao.ToUpper() == icao.ToUpper()).FirstOrDefault();
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
