namespace HuntingCalculator.Logic
{
    public static class MezaTipaGrupaLogic
    {
        public static string GetMezaTips(string kokaSuga, string maat)
        {
            if (kokaSuga == "A")
                #region A

                switch (maat)
                {
                    case "Am":
                    case "Av":
                    case "Km":
                    case "Kv":
                        return "V";
                    case "Ap":
                    case "As":
                    case "Db":
                    case "Dms":
                    case "Grs":
                    case "Kp":
                    case "Ks":
                    case "Lk":
                    case "Vrs":
                        return "VI";
                    case "Dm":
                    case "Gr":
                    case "Gs":
                    case "Ln":
                    case "Mrs":
                    case "Nd":
                    case "Vr":
                        return "VII";
                }

            #endregion

            if (kokaSuga == "B")
                #region B

                switch (maat)
                {
                    case "Am":
                    case "Ap":
                    case "As":
                    case "Av":
                    case "Dm":
                    case "Dms":
                    case "Gr":
                    case "Grs":
                    case "Gs":
                    case "Mr":
                    case "Mrs":
                    case "Nd":
                    case "Sl":
                    case "Vr":
                    case "Vrs":
                        return "V";
                    case "Db":
                    case "Km":
                    case "Kp":
                    case "Ks":
                    case "Kv":
                    case "Lk":
                    case "Ln":
                    case "Pv":
                        return "VI";
                }

            #endregion

            if (kokaSuga == "Ba")
                #region Ba

                switch (maat)
                {
                    case "Ap":
                    case "As":
                    case "Dm":
                    case "Dms":
                    case "Gr":
                    case "Grs":
                    case "Kp":
                    case "Vr":
                    case "Vrs":
                        return "V";
                    case "Av":
                    case "Db":
                    case "Km":
                    case "Ks":
                    case "Ln":
                    case "Mrs":
                    case "Nd":
                    case "Pv":
                        return "V";
                }

            #endregion

            if (kokaSuga == "Be")
                #region Be

                switch (maat)
                {
                    case "Ap":
                    case "As":
                    case "Dm":
                    case "Dms":
                    case "Gr":
                    case "Grs":
                    case "Km":
                    case "Kp":
                    case "Ks":
                    case "Kv":
                    case "Lk":
                    case "Ln":
                    case "Vr":
                    case "Vrs":
                        return "IV";
                    case "Db":
                    case "Nd":
                        return "VI";
                }

            #endregion

            if (kokaSuga == "Bl" || kokaSuga == "Ds" || kokaSuga == "G" || kokaSuga == "K" || kokaSuga == "Ki" || kokaSuga == "L" || kokaSuga == "Le"
                || kokaSuga == "Os" || kokaSuga == "Oz" || kokaSuga == "Pa" || kokaSuga == "Sk" || kokaSuga == "V" || kokaSuga == "Vi")
                #region Bl Ds G ...

                switch (maat)
                {
                    case "Am":
                    case "Av":
                    case "Km":
                    case "Kv":
                        return "V";
                    case "Ap":
                    case "As":
                    case "Db":
                    case "Dms":
                    case "Grs":
                    case "Kp":
                    case "Ks":
                    case "Lk":
                    case "Vrs":
                        return "VI";
                    case "Dm":
                    case "Gr":
                    case "Gs":
                    case "Ln":
                    case "Mrs":
                    case "Nd":
                    case "Vr":
                        return "VII";
                }
                #endregion

            if (kokaSuga == "Cp" || kokaSuga == "P" || kokaSuga == "PC")
                #region Cp P PC

                switch (maat)
                {
                    case "Av":
                    case "Gs":
                    case "Kv":
                    case "Mr":
                    case "Mrs":
                    case "Pv":
                    case "Sl":
                        return "I";
                    case "Am":
                    case "As":
                    case "Dm":
                    case "Dms":
                    case "Ln":
                        return "II";
                    case "Db":
                    case "Km":
                    case "Ks":
                    case "Nd":
                        return "III";
                }

            #endregion


            if (kokaSuga == "E" || kokaSuga == "EC")
                #region E EC

                switch (maat)
                {
                    case "Ap":
                    case "As":
                    case "Dm":
                    case "Dms":
                    case "Gr":
                    case "Grs":
                    case "Km":
                    case "Kp":
                    case "Ks":
                    case "Kv":
                    case "Lk":
                    case "Ln":
                    case "Vr":
                    case "Vrs":
                        return "IV";
                    case "Db":
                    case "Nd":
                        return "VI";
                }

            #endregion

            if (kokaSuga == "M")
                #region M

                switch (maat)
                {
                    case "Grs":
                    case "Km":
                    case "Kp":
                    case "Ks":
                    case "Kv":
                    case "Lk":
                    case "Nd":
                    case "Pv":
                    case "Vrs":
                        return "VI";
                    case "Am":
                    case "Ap":
                    case "As":
                    case "Av":
                    case "Db":
                    case "Dms":
                    case "Mrs":
                        return "VII";
                }

            #endregion

            return "X";
        }
    }
}
