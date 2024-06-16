using System;
using System.Collections.Generic;

namespace DiggerFlight.PcnAcnCalculator.Models
{
    public class PcnParts
    {
        public enum RigidityType
        {
            R,
            F,
        }

        public enum StrenghType
        {
            A,
            B,
            C,
            D
        }

        public enum MaximumTyrePresureType
        {
            W,
            X,
            Y,
            Z
        }

        public enum PcnDeterminedType
        {
            T,
            U
        }

        public string InfrastructureName { get; set; }
        public string NonDecodedPcnValue { get; set; }
        public double? PcnNumericalValue { get; set; }
        public RigidityType? Rigidity { get; set; }
        public StrenghType? Strengh { get; set; }
        public MaximumTyrePresureType? MaximumTyrePresure { get; set; }
        public PcnDeterminedType? PcnDetermined { get; set; }

        public static List<PcnParts>? GetPcnParts(List<Infrastructure> infrastructure)
        {
            try
            {
                List<PcnParts> parts = new List<PcnParts>();
                foreach(var infrastructureElement in infrastructure)
                {
                    PcnParts pcnParts = new PcnParts();
                    pcnParts.InfrastructureName = infrastructureElement.Name;
                    pcnParts.NonDecodedPcnValue = infrastructureElement.PcnValue;
                    if (infrastructureElement.PcnValue.Contains("/"))
                    {
                        var pcnValues = infrastructureElement.PcnValue.ToUpper().Split("/");
                        pcnParts.PcnNumericalValue = Convert.ToInt32(pcnValues[0]);
                        Enum.TryParse(pcnValues[1], out RigidityType rigidity);
                        pcnParts.Rigidity = rigidity;
                        Enum.TryParse(pcnValues[2], out StrenghType strengh);
                        pcnParts.Strengh = strengh;
                        Enum.TryParse(pcnValues[3], out MaximumTyrePresureType maximumTyrePresure);
                        pcnParts.MaximumTyrePresure = maximumTyrePresure;
                        Enum.TryParse(pcnValues[4], out PcnDeterminedType pcnDetermined);
                        pcnParts.PcnDetermined = pcnDetermined;
                    }
                    parts.Add(pcnParts);
                }
                return parts;
            }
            catch
            {
                //throw new Exception(infrastructureElement.pcnName + " with value " + infrastructureElement.pcnValue + " could not be extracted.");
                return null;
            }
        }
    }
}
