namespace DiggerFlight.PcnAcnCalculator.Calculation
{
    using System;

    public class UnitsConverter
    {
        public static double? GetAircraftWeighInKn(string mass)
        {
            var weightInKn = 0.0;
            if (string.IsNullOrWhiteSpace(mass)) return weightInKn;
            if (mass.ToUpper().Contains("LB"))
            {
                mass = mass.ToUpper().Replace("LBS", "").Replace("LB", "").Trim();
                weightInKn = GetKiloNewtons(GetKilogrames(Convert.ToDouble(mass)));
            }
            else if ((mass.ToUpper().Contains("KG")))
            {
                mass = mass.ToUpper().Replace("KGS", "").Replace("KG", "").Trim();
                weightInKn = GetKiloNewtons(Convert.ToDouble(mass));
            }
            else
            {
                weightInKn = GetKiloNewtons(Convert.ToDouble(mass));
            }
            return weightInKn;
        }

        public static double GetKilogrames(double pounds)
        {
            return Math.Round(pounds / 2.204623,2);
        }

        public static double GetKiloNewtons(double kilograms)
        {
            return Math.Round((kilograms * 9.98) / 1000,2);
        }
    }
}
