
namespace DiggerFlight.PcnAcnCalculator.Models
{
    using DiggerFlight.PcnAcnCalculator.FileManagement;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class AirfieldOperations
    {
        public static AirfieldOperations? GetAirfieldOperations()
        {
            try
            {
                var airfieldOpsJson = FileControl.GetJsonFile(FileControl.ConfigFileToFetch.AirfieldOperations);
                AirfieldOperations airfieldOps = JsonConvert.DeserializeObject<AirfieldOperations>(airfieldOpsJson);
                return airfieldOps;
            }
            catch { return null; }
        }

        [JsonProperty("airfields")]
        public List<Airfield> Airfields { get; set; }
    }

    public class Airfield
    {
        [JsonProperty("icao")]
        public string Icao { get; set; }
        [JsonProperty("infrastructure")]
        public List<Infrastructure> Infrastructure { get; set; }
    }

    public class Infrastructure
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("pcnValue")]
        public string PcnValue { get; set; }
    }
}
