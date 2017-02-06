using HuntingCalculator.Enums;

namespace HuntingCalculator.Logic
{
    public class PriceType
    {
        public DzivnieksEnum Dzivnieks { get; set; }
        public double CenaParPirmoBonitati { get; set; }
        public double CenaParOtroBonitati { get; set; }
        public double CenaParTresoBonitati { get; set; }
        public double CenaParCeturtoBonitati { get; set; }
        public double CenaParPiektoBonitati { get; set; }
    }
}
