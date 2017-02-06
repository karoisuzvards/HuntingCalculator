using HuntingCalculator.Attributes;
using HuntingCalculator.Resources;

namespace HuntingCalculator.Enums
{
    public enum MezaTipsEnum
    {
        [LocalizableDescription(@"NesaslegusasPKulturas", typeof(MezaTipsResource))]
        NesaslegusasPKulturas = 1,
        [LocalizableDescription(@"NesaslegusasEKulturas", typeof(MezaTipsResource))]
        NesaslegusasEKulturas = 2,
        [LocalizableDescription(@"ArMezuNeapklataPlatiba", typeof(MezaTipsResource))]
        ArMezuNeapklataPlatiba = 3,
        [LocalizableDescription(@"Krumaji", typeof(MezaTipsResource))]
        Krumaji = 4,
        [LocalizableDescription(@"KlajieSunuPurvi", typeof(MezaTipsResource))]
        KlajieSunuPurvi = 5,
        [LocalizableDescription(@"ZaluParejasUnApaugusiSunuPurvi", typeof(MezaTipsResource))]
        ZaluParejasUnApaugusiSunuPurvi = 6,
        [LocalizableDescription(@"NeiezogotasLauksaimniecibasPlatibas", typeof(MezaTipsResource))]
        NeiezogotasLauksaimniecibasPlatibas = 7,
        [LocalizableDescription(@"Trases", typeof(MezaTipsResource))]
        Trases = 8,
        [LocalizableDescription(@"Virsaji", typeof(MezaTipsResource))]
        Virsaji = 9,
        [LocalizableDescription(@"DzivniekuBarosanasLauces", typeof(MezaTipsResource))]
        DzivniekuBarosanasLauces = 10
    }
}
