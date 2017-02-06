using HuntingCalculator.Attributes;
using HuntingCalculator.Resources;

namespace HuntingCalculator.Enums
{
    public enum DzivnieksEnum
    {
        [LocalizableDescription(@"Alnis", typeof(DzivnieksResource))]
        Alnis = 1,
        [LocalizableDescription(@"Staltbriedis", typeof(DzivnieksResource))]
        Staltbriedis = 2,
        [LocalizableDescription(@"Stirna", typeof(DzivnieksResource))]
        Stirna = 3,
        [LocalizableDescription(@"Mezacuka", typeof(DzivnieksResource))]
        Mezacuka = 4
    }
}
