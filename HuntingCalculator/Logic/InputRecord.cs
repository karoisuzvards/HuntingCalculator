using HuntingCalculator.Enums;

namespace HuntingCalculator.Logic
{
    public class InputRecord
    {
        public double KvartalaNr { get; set; }
        public double NogabalaNr { get; set; }
        public double Platiba { get; set; }
        public MaatEnum MezaApstakluAugsanasTips { get; set; }
        public KokaVeidsEnum KokaSuga { get; set; }
        public int Vecums { get; set; }
        public Bonitate Bonitates { get; set; }
    }
}
