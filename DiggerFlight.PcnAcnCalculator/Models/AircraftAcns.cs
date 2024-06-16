namespace DiggerFlight.PcnAcnCalculator.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using FileManagement;

    public class AircraftAcns
    {
        public static AircraftAcns? GetAircraftAcns()
        {
            try 
            {
                var aircraftAcnsJson = FileControl.GetJsonFile(FileControl.ConfigFileToFetch.AirCraftAcns);
                AircraftAcns aircraftAcns = JsonConvert.DeserializeObject<AircraftAcns>(aircraftAcnsJson);
                return aircraftAcns;
            }
            catch { return null; }
        }

        [JsonProperty("aircraftAcns")]
        public List<Aircraftacn> AircraftAcn { get; set; }
    }

    public class Aircraftacn
    {
        [JsonProperty("aircraftName")] 
        public string AircraftName { get; set; }
        [JsonProperty("simbriefId")] 
        public string SimbriefId { get; set; }
        [JsonProperty("tirePressureMpa")] 
        public double? TirePressureMpa { get; set; }
        [JsonProperty("maxWeightValues")]
        public Weightvalues? MaxWeightValues { get; set; }
        [JsonProperty("minWeightValues")] 
        public Weightvalues? MinWeightValues { get; set; }
        [JsonProperty("perceentageLoadOnOneMainGear")] 
        public double? PerceentageLoadOnOneMainGear { get; set; }
    }

    public class Weightvalues
    {
        [JsonProperty("weightKn")]
        public double? WeightKn { get; set; }
        [JsonProperty("flexSubgradeHigh")]
        public double? FlexSubgradeHigh { get; set; }
        [JsonProperty("flexSubgradeMedium")]
        public double? FlexSubgradeMedium { get; set; }
        [JsonProperty("flexSubgradeLow")]
        public double? FlexSubgradeLow { get; set; }
        [JsonProperty("flexSubgradeVeryLow")]
        public double? FlexSubgradeVeryLow { get; set; }
        [JsonProperty("rigidSubgradeHigh")]
        public double? RigidSubgradeHigh { get; set; }
        [JsonProperty("rigidSubgradeMedium")]
        public double? RigidSubgradeMedium { get; set; }
        [JsonProperty("rigidSubgradeLow")]
        public double? RigidSubgradeLow { get; set; }
        [JsonProperty("rigidSubgradeVeryLow")]
        public double? RigidSubgradeVeryLow { get; set; }
        [JsonProperty("stOverSb")]
        public double? StOverSb { get; set; }
    }
}
