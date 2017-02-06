using System.Collections.Generic;
using HuntingCalculator.Enums;

namespace HuntingCalculator.Logic
{
    public static class CenasLogic
    {
        public static List<PriceType> PopulatePrices()
        {
            const double conversionRate = 1.422872;

            var prices = new List<PriceType>
            {
                new PriceType
                {
                    Dzivnieks = DzivnieksEnum.Alnis,
                    CenaParPirmoBonitati = (double)(0.16*conversionRate),
                    CenaParOtroBonitati = (double)(0.13*conversionRate),
                    CenaParTresoBonitati = (double)(0.10*conversionRate),
                    CenaParCeturtoBonitati = (double)(0.07*conversionRate),
                    CenaParPiektoBonitati = (double)(0.06*conversionRate)
                },
                new PriceType
                {
                    Dzivnieks = DzivnieksEnum.Staltbriedis,
                    CenaParPirmoBonitati = (double)(0.20*conversionRate),
                    CenaParOtroBonitati = (double)(0.17*conversionRate),
                    CenaParTresoBonitati = (double)(0.14*conversionRate),
                    CenaParCeturtoBonitati = (double)(0.11*conversionRate),
                    CenaParPiektoBonitati = (double)(0.09*conversionRate)
                },
                new PriceType
                {
                    Dzivnieks = DzivnieksEnum.Stirna,
                    CenaParPirmoBonitati = (double)(0.13*conversionRate),
                    CenaParOtroBonitati = (double)(0.10*conversionRate),
                    CenaParTresoBonitati = (double)(0.07*conversionRate),
                    CenaParCeturtoBonitati = (double)(0.06*conversionRate),
                    CenaParPiektoBonitati = (double)(0.03*conversionRate)
                },
                new PriceType
                {
                    Dzivnieks = DzivnieksEnum.Mezacuka,
                    CenaParPirmoBonitati = (double)(0.14*conversionRate),
                    CenaParOtroBonitati = (double)(0.11*conversionRate),
                    CenaParTresoBonitati = (double)(0.09*conversionRate),
                    CenaParCeturtoBonitati = (double)(0.07*conversionRate),
                    CenaParPiektoBonitati = (double)(0.04*conversionRate)
                }
            };

            return prices;
        }
    }
}
