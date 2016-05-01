using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving.Common
{
    public static class Utilities
    {
        public static string GetArrayString(this int[][] input)
        {
            int inputLength = input.Length;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < inputLength; ++i)
            {
                for (int j = 0; j < inputLength; ++j)
                {
                    sb.Append(input[i][j]);
                    sb.Append(", ");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static bool DecrementPermutationSet(int[] permutationSetsLength, int[] permutationSetsMaxLength)
        {
            for (int i = permutationSetsLength.Length - 1; i >= 0; --i)
            {
                int tempDecrementedValue = permutationSetsLength[i] - 1;

                if (tempDecrementedValue < 0)
                {
                    if (i == 0)
                        return false;

                    permutationSetsLength[i] = permutationSetsMaxLength[i] - 1;
                    continue;
                }

                permutationSetsLength[i] = tempDecrementedValue;
                break;
            }

            return true;
        }

        public static List<List<T[]>> GeneratePermutations<T>(List<T[]> input)
        {
            List<List<T[]>> ret = new List<List<T[]>>();
            List<List<T[]>> possibleSubs = new List<List<T[]>>();
            
            foreach (T[] sub in input)
            {
                List<T[]> possibleSub = new List<T[]>();

                foreach (T item in sub)
                {
                    possibleSub.Add(new[] { item });
                }
                
                possibleSubs.Add(possibleSub);
            }

            int[] permutationSetMaxLengths = possibleSubs
                .Select(sub => sub.Count)
                .ToArray();

            int[] permutationSetLengths = possibleSubs
                .Select(sub => sub.Count - 1)
                .ToArray();

            do
            {
                List<T[]> perm = new List<T[]>();

                for (int i = 0; i < possibleSubs.Count; ++i)
                {
                    T[] subPerm = possibleSubs[i][permutationSetLengths[i]];
                    perm.Add(subPerm);
                }

                ret.Add(perm);
            } while (DecrementPermutationSet(permutationSetLengths, permutationSetMaxLengths));

            return ret;
        }
    }
}
