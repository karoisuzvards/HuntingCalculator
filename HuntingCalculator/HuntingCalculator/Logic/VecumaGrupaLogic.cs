namespace HuntingCalculator.Logic
{
    public static class VecumaGrupaLogic
    {
        public static int GetVecumaGrupa(string kokaSuga, int vecums)
        {
            if ((kokaSuga == "P" || 
                 kokaSuga == "E" || 
                 kokaSuga == "B" || 
                 kokaSuga == "A" || 
                 kokaSuga == "Ba" ||
                 kokaSuga == "Ma" || 
                 kokaSuga == "O" || 
                 kokaSuga == "Os") && vecums <= 20)
                return 1;

            if ((kokaSuga == "P" ||
                 kokaSuga == "E" ||
                 kokaSuga == "B" ||
                 kokaSuga == "A" ||
                 kokaSuga == "Ba" ||
                 kokaSuga == "Ma" ||
                 kokaSuga == "O" ||
                 kokaSuga == "Os") && vecums > 20 && vecums <= 40)
                return 2;

            if (((kokaSuga == "E" ||
                  kokaSuga == "B" ||
                  kokaSuga == "A" ||
                  kokaSuga == "Ba" ||
                  kokaSuga == "Ma" ||
                  kokaSuga == "O" ||
                  kokaSuga == "Os") && vecums > 40 && vecums <= 60) ||
                (kokaSuga == "P" && vecums > 40 && vecums <= 80))
                return 3;

            if (((kokaSuga == "E" ||
                  kokaSuga == "B" ||
                  kokaSuga == "A" ||
                  kokaSuga == "Ba" ||
                  kokaSuga == "Ma" ||
                  kokaSuga == "O" ||
                  kokaSuga == "Os") && vecums >  60) ||
                (kokaSuga == "P" && vecums > 80))
                return 4;

            return 0;
        }
    }
}
