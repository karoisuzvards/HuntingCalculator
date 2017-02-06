using HuntingCalculator.Enums;

namespace HuntingCalculator.Logic
{
    public static class Bonitates
    {
        public static Bonitate GetByTipsAndVecums(string tips, int vecumaGrupa)
        {
            switch (tips)
            {
                case "I":
                    if (vecumaGrupa == 1) return GetFirstForFirst();
                    if (vecumaGrupa == 2) return GetSecondForFirst();
                    if (vecumaGrupa == 3) return GetThirdForFirst();
                    if (vecumaGrupa == 4) return GetForthForFirst();
                    break;
                case "II":
                    if (vecumaGrupa == 1) return GetFirstForSecond();
                    if (vecumaGrupa == 2) return GetSecondForSecond();
                    if (vecumaGrupa == 3) return GetThirdForSecond();
                    if (vecumaGrupa == 4) return GetForthForSecond();
                    break;
                case "III":
                    if (vecumaGrupa == 1) return GetFirstForThird();
                    if (vecumaGrupa == 2) return GetSecondForThird();
                    if (vecumaGrupa == 3) return GetThirdForThird();
                    if (vecumaGrupa == 4) return GetForthForThird();
                    break;
                case "IV":
                    if (vecumaGrupa == 1) return GetFirstForForth();
                    if (vecumaGrupa == 2) return GetSecondForForth();
                    if (vecumaGrupa == 3) return GetThirdForForth();
                    if (vecumaGrupa == 4) return GetForthForForth();
                    break;
                case "V":
                    if (vecumaGrupa == 1) return GetFirstForFifth();
                    if (vecumaGrupa == 2) return GetSecondForFifth();
                    if (vecumaGrupa == 3) return GetThirdForFifth();
                    if (vecumaGrupa == 4) return GetForthForFifth();
                    break;
                case "VI":
                    if (vecumaGrupa == 1) return GetFirstForSixth();
                    if (vecumaGrupa == 2) return GetSecondForSixth();
                    if (vecumaGrupa == 3) return GetThirdForSixth();
                    if (vecumaGrupa == 4) return GetForthForSixth();
                    break;
                case "VII":
                    if (vecumaGrupa == 1) return GetFirstForSeventh();
                    if (vecumaGrupa == 2) return GetSecondForSeventh();
                    if (vecumaGrupa == 3) return GetThirdForSeventh();
                    if (vecumaGrupa == 4) return GetForthForSeventh();
                    break;
            }
            return null;
        }

        public static Bonitate GetByTips(MaatEnum tips)
        {
            return GetParejoMezaGrupuBonitateNoMaat(tips);
        }

        #region I Meza grupa

        private static Bonitate GetFirstForFirst()
        {
            return new Bonitate
            {
                Alnis = 1,
                Staltbriedis = 1,
                StirnaPirma = 1,
                StirnaOtra = 2,
                StirnaTresa = 2,
                Mezacuka = 3
            };
        }

        private static Bonitate GetSecondForFirst()
        {
            return new Bonitate
            {
                Alnis = 5,
                Staltbriedis = 3,
                StirnaPirma = 2,
                StirnaOtra = 3,
                StirnaTresa = 4,
                Mezacuka = 5
            };
        }

        private static Bonitate GetThirdForFirst()
        {
            return new Bonitate
            {
                Alnis = 5,
                Staltbriedis = 2,
                StirnaPirma = 1,
                StirnaOtra = 2,
                StirnaTresa = 3,
                Mezacuka = 5
            };
        }

        private static Bonitate GetForthForFirst()
        {
            return new Bonitate
            {
                Alnis = 5,
                Staltbriedis = 2,
                StirnaPirma = 1,
                StirnaOtra = 2,
                StirnaTresa = 3,
                Mezacuka = 5
            };
        }

        #endregion

        #region II Meza grupa

        private static Bonitate GetFirstForSecond()
        {
            return new Bonitate
            {
                Alnis = 1,
                Staltbriedis = 1,
                StirnaPirma = 1,
                StirnaOtra = 2,
                StirnaTresa = 2,
                Mezacuka = 3
            };
        }

        private static Bonitate GetSecondForSecond()
        {
            return new Bonitate
            {
                Alnis = 5,
                Staltbriedis = 4,
                StirnaPirma = 3,
                StirnaOtra = 4,
                StirnaTresa = 4,
                Mezacuka = 4
            };
        }

        private static Bonitate GetThirdForSecond()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 1,
                StirnaPirma = 1,
                StirnaOtra = 1,
                StirnaTresa = 2,
                Mezacuka = 3
            };
        }

        private static Bonitate GetForthForSecond()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 1,
                StirnaPirma = 1,
                StirnaOtra = 1,
                StirnaTresa = 2,
                Mezacuka = 3
            };
        }

        #endregion

        #region III Meza grupa

        private static Bonitate GetFirstForThird()
        {
            return new Bonitate
            {
                Alnis = 1,
                Staltbriedis = 1,
                StirnaPirma = 1,
                StirnaOtra = 2,
                StirnaTresa = 2,
                Mezacuka = 3
            };
        }

        private static Bonitate GetSecondForThird()
        {
            return new Bonitate
            {
                Alnis = 2,
                Staltbriedis = 3,
                StirnaPirma = 2,
                StirnaOtra = 3,
                StirnaTresa = 3,
                Mezacuka = 3
            };
        }

        private static Bonitate GetThirdForThird()
        {
            return new Bonitate
            {
                Alnis = 2,
                Staltbriedis = 1,
                StirnaPirma = 1,
                StirnaOtra = 1,
                StirnaTresa = 1,
                Mezacuka = 3
            };
        }

        private static Bonitate GetForthForThird()
        {
            return new Bonitate
            {
                Alnis = 2,
                Staltbriedis = 1,
                StirnaPirma = 1,
                StirnaOtra = 1,
                StirnaTresa = 1,
                Mezacuka = 3
            };
        }

        #endregion

        #region IV Meza grupa

        private static Bonitate GetFirstForForth()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 3,
                StirnaPirma = 4,
                StirnaOtra = 4,
                StirnaTresa = 5,
                Mezacuka = 1
            };
        }

        private static Bonitate GetSecondForForth()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 5,
                StirnaPirma = 3,
                StirnaOtra = 4,
                StirnaTresa = 5,
                Mezacuka = 3
            };
        }

        private static Bonitate GetThirdForForth()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 3,
                StirnaPirma = 2,
                StirnaOtra = 3,
                StirnaTresa = 3,
                Mezacuka = 2
            };
        }

        private static Bonitate GetForthForForth()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 2,
                StirnaPirma = 2,
                StirnaOtra = 2,
                StirnaTresa = 3,
                Mezacuka = 1
            };
        }

        #endregion

        #region V Meza grupa

        private static Bonitate GetFirstForFifth()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 2,
                StirnaPirma = 1,
                StirnaOtra = 1,
                StirnaTresa = 1,
                Mezacuka = 2
            };
        }

        private static Bonitate GetSecondForFifth()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 4,
                StirnaPirma = 4,
                StirnaOtra = 5,
                StirnaTresa = 5,
                Mezacuka = 3
            };
        }

        private static Bonitate GetThirdForFifth()
        {
            return new Bonitate
            {
                Alnis = 3,
                Staltbriedis = 2,
                StirnaPirma = 3,
                StirnaOtra = 3,
                StirnaTresa = 4,
                Mezacuka = 2
            };
        }

        private static Bonitate GetForthForFifth()
        {
            return new Bonitate
            {
                Alnis = 3,
                Staltbriedis = 2,
                StirnaPirma = 3,
                StirnaOtra = 3,
                StirnaTresa = 4,
                Mezacuka = 2
            };
        }

        #endregion

        #region VI Meza grupa

        private static Bonitate GetFirstForSixth()
        {
            return new Bonitate
            {
                Alnis = 4,
                Staltbriedis = 2,
                StirnaPirma = 5,
                StirnaOtra = 5,
                StirnaTresa = 5,
                Mezacuka = 2
            };
        }

        private static Bonitate GetSecondForSixth()
        {
            return new Bonitate
            {
                Alnis = 2,
                Staltbriedis = 3,
                StirnaPirma = 4,
                StirnaOtra = 4,
                StirnaTresa = 4,
                Mezacuka = 2
            };
        }

        private static Bonitate GetThirdForSixth()
        {
            return new Bonitate
            {
                Alnis = 2,
                Staltbriedis = 2,
                StirnaPirma = 4,
                StirnaOtra = 4,
                StirnaTresa = 4,
                Mezacuka = 2
            };
        }

        private static Bonitate GetForthForSixth()
        {
            return new Bonitate
            {
                Alnis = 2,
                Staltbriedis = 2,
                StirnaPirma = 4,
                StirnaOtra = 4,
                StirnaTresa = 4,
                Mezacuka = 2
            };
        }

        #endregion

        #region VII Meza grupa

        private static Bonitate GetFirstForSeventh()
        {
            return new Bonitate
            {
                Alnis = 1,
                Staltbriedis = 2,
                StirnaPirma = 1,
                StirnaOtra = 1,
                StirnaTresa = 1,
                Mezacuka = 1
            };
        }

        private static Bonitate GetSecondForSeventh()
        {
            return new Bonitate
            {
                Alnis = 3,
                Staltbriedis = 3,
                StirnaPirma = 4,
                StirnaOtra = 4,
                StirnaTresa = 4,
                Mezacuka = 2
            };
        }

        private static Bonitate GetThirdForSeventh()
        {
            return new Bonitate
            {
                Alnis = 2,
                Staltbriedis = 2,
                StirnaPirma = 3,
                StirnaOtra = 3,
                StirnaTresa = 3,
                Mezacuka = 2
            };
        }

        private static Bonitate GetForthForSeventh()
        {
            return new Bonitate
            {
                Alnis = 2,
                Staltbriedis = 2,
                StirnaPirma = 3,
                StirnaOtra = 3,
                StirnaTresa = 3,
                Mezacuka = 2
            };
        }

        #endregion

        #region VIII Meza grupa

        private static Bonitate GetFirstForEighth()
        {
            return new Bonitate
            {
                Alnis = 1,
                Staltbriedis = 1,
                StirnaPirma = 1,
                StirnaOtra = 2,
                StirnaTresa = 2,
                Mezacuka = 3
            };
        }

        private static Bonitate GetSecondForEighth()
        {
            return new Bonitate
            {
                Alnis = 5,
                Staltbriedis = 3,
                StirnaPirma = 2,
                StirnaOtra = 3,
                StirnaTresa = 4,
                Mezacuka = 5
            };
        }

        private static Bonitate GetThirdForEighth()
        {
            return new Bonitate
            {
                Alnis = 5,
                Staltbriedis = 2,
                StirnaPirma = 1,
                StirnaOtra = 2,
                StirnaTresa = 3,
                Mezacuka = 5
            };
        }

        private static Bonitate GetForthForEighth()
        {
            return new Bonitate
            {
                Alnis = 5,
                Staltbriedis = 2,
                StirnaPirma = 1,
                StirnaOtra = 2,
                StirnaTresa = 3,
                Mezacuka = 5
            };
        }

        #endregion

        #region Parejas Meza grupa

        private static Bonitate GetParejoMezaGrupuBonitate(int enumId)
        {
            switch (enumId)
            {
                case 1:
                    return new Bonitate
                    {
                        Alnis = 0,
                        Staltbriedis = 0,
                        StirnaPirma = 1,
                        StirnaOtra = 2,
                        StirnaTresa = 2,
                        Mezacuka = 4
                    };
                case 2:
                    return new Bonitate
                    {
                        Alnis = 4,
                        Staltbriedis = 3,
                        StirnaPirma = 4,
                        StirnaOtra = 4,
                        StirnaTresa = 5,
                        Mezacuka = 4
                    };
                case 3:
                    return new Bonitate
                    {
                        Alnis = 5,
                        Staltbriedis = 4,
                        StirnaPirma = 4,
                        StirnaOtra = 4,
                        StirnaTresa = 4,
                        Mezacuka = 5
                    };
                case 4:
                    return new Bonitate
                    {
                        Alnis = 5,
                        Staltbriedis = 5,
                        StirnaPirma = 5,
                        StirnaOtra = 5,
                        StirnaTresa = 5,
                        Mezacuka = 5
                    };
                case 5:
                    return new Bonitate
                    {
                        Alnis = 5,
                        Staltbriedis = 5,
                        StirnaPirma = 5,
                        StirnaOtra = 5,
                        StirnaTresa = 5,
                        Mezacuka = 5
                    };
                case 6:
                    return new Bonitate
                    {
                        Alnis = 2,
                        Staltbriedis = 2,
                        StirnaPirma = 2,
                        StirnaOtra = 2,
                        StirnaTresa = 2,
                        Mezacuka = 3
                    };
                case 7:
                    return new Bonitate
                    {
                        Alnis = 0,
                        Staltbriedis = 0,
                        StirnaPirma = 2,
                        StirnaOtra = 2,
                        StirnaTresa = 2,
                        Mezacuka = 0
                    };
                case 8:
                    return new Bonitate
                    {
                        Alnis = 2,
                        Staltbriedis = 2,
                        StirnaPirma = 2,
                        StirnaOtra = 2,
                        StirnaTresa = 2,
                        Mezacuka = 2
                    };
                case 9:
                    return new Bonitate
                    {
                        Alnis = 5,
                        Staltbriedis = 3,
                        StirnaPirma = 4,
                        StirnaOtra = 4,
                        StirnaTresa = 4,
                        Mezacuka = 5
                    };
                case 10:
                    return new Bonitate
                    {
                        Alnis = 2,
                        Staltbriedis = 2,
                        StirnaPirma = 3,
                        StirnaOtra = 3,
                        StirnaTresa = 3,
                        Mezacuka = 3
                    };
            }
            return null;
        }

        #endregion


        #region Parejas Meza grupa no MAAT

        private static Bonitate GetParejoMezaGrupuBonitateNoMaat(MaatEnum maat)
        {
            switch (maat)
            {
                case MaatEnum.NesaslPKult:
                    return new Bonitate
                    {
                        Alnis = 0,
                        Staltbriedis = 0,
                        StirnaPirma = 1,
                        StirnaOtra = 2,
                        StirnaTresa = 2,
                        Mezacuka = 4
                    };
                case MaatEnum.NesaslEKult:
                    return new Bonitate
                    {
                        Alnis = 4,
                        Staltbriedis = 3,
                        StirnaPirma = 4,
                        StirnaOtra = 4,
                        StirnaTresa = 5,
                        Mezacuka = 4
                    };
                case MaatEnum.MezaNeapkl:
                    return new Bonitate
                    {
                        Alnis = 5,
                        Staltbriedis = 4,
                        StirnaPirma = 4,
                        StirnaOtra = 4,
                        StirnaTresa = 4,
                        Mezacuka = 5
                    };
                case MaatEnum.Krumaji:
                    return new Bonitate
                    {
                        Alnis = 5,
                        Staltbriedis = 5,
                        StirnaPirma = 5,
                        StirnaOtra = 5,
                        StirnaTresa = 5,
                        Mezacuka = 5
                    };
                case MaatEnum.KlajieSunuPurvi:
                    return new Bonitate
                    {
                        Alnis = 5,
                        Staltbriedis = 5,
                        StirnaPirma = 5,
                        StirnaOtra = 5,
                        StirnaTresa = 5,
                        Mezacuka = 5
                    };
                case MaatEnum.ZaluParejas:
                    return new Bonitate
                    {
                        Alnis = 2,
                        Staltbriedis = 2,
                        StirnaPirma = 2,
                        StirnaOtra = 2,
                        StirnaTresa = 2,
                        Mezacuka = 3
                    };
                case MaatEnum.NeiezLauksaimn:
                    return new Bonitate
                    {
                        Alnis = 0,
                        Staltbriedis = 3,
                        StirnaPirma = 2,
                        StirnaOtra = 2,
                        StirnaTresa = 2,
                        Mezacuka = 3
                    };
                case MaatEnum.Trases:
                    return new Bonitate
                    {
                        Alnis = 2,
                        Staltbriedis = 2,
                        StirnaPirma = 2,
                        StirnaOtra = 2,
                        StirnaTresa = 2,
                        Mezacuka = 2
                    };
                case MaatEnum.Virsaji:
                    return new Bonitate
                    {
                        Alnis = 5,
                        Staltbriedis = 3,
                        StirnaPirma = 4,
                        StirnaOtra = 4,
                        StirnaTresa = 4,
                        Mezacuka = 5
                    };
                case MaatEnum.DzivnBarLauces:
                    return new Bonitate
                    {
                        Alnis = 2,
                        Staltbriedis = 2,
                        StirnaPirma = 3,
                        StirnaOtra = 3,
                        StirnaTresa = 3,
                        Mezacuka = 3
                    };
            }
            return null;
        }

        #endregion
    }
}
