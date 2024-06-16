namespace DiggerFlight.PcnAcnCalculator.Calculation
{
    using System;

    internal class UnitsConverter
    {
        public static double? GetAircraftWeighInKn(string mass)
        {
            var weightInKn = 0.0;
            if (string.IsNullOrWhiteSpace(mass)) return weightInKn;
            if (mass.ToUpper().Contains("LBS"))
            {
                mass = mass.ToUpper().Replace("LBS", "").Trim();
                weightInKn = GetKiloNewtons(GetKilogrames(Convert.ToDouble(mass)));
            }
            else if ((mass.ToUpper().Contains("KGS")))
            {
                mass = mass.ToUpper().Replace("KGS", "").Trim();
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
            return RoundingTo(pounds / 2.204623,2);
        }

        public static double GetKiloNewtons(double kilograms)
        {
            return RoundingTo((kilograms * 9.98) / 1000,2);
        }

        public static double RoundingTo(double valueToRound, int numberOfDigit)
        {
            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(valueToRound))) + 1 - numberOfDigit);
            double result = Math.Round(valueToRound / scale) * scale;
            return result;
        }
    }
}
