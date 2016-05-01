using ProblemSolving.Common;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSolving
{
    public class EasyKeypad
    {
        public class Input
        {
            public int LengthOfPasscode { get; set; }
            public int[][] KeypadInputs { get; set; }
        }

        public List<int[]> Solve(Input input)
        {
            List<int[]> ret = new List<int[]>();
            LinkedList<int> numberSet = new LinkedList<int>();
            int keypadWidth = input.KeypadInputs[0].Length;
            int keypadHeight = input.KeypadInputs.Length;

            for (int i = 0; i < keypadHeight; ++i)
                for (int j = 0; j < keypadWidth; ++j)
                    for (int count = 0; count < input.KeypadInputs[i][j]; ++count)
                        if (i == 3 && j == 1)
                            numberSet.AddFirst(0);
                        else
                            numberSet.AddLast(keypadWidth * i + j + 1);

            ret = Utilities.GetCombination(numberSet, input.LengthOfPasscode).ToList();
            
            return ret;
        }
    }
}