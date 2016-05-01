using System.Collections.Generic;
using System.Linq;

namespace ProblemSolving.Common
{
    public static class Utilities
    {
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

        private static int GetMinimalNumberNotExistingInSet(int[] set, int? startingNumber = null)
        {
            int ret = startingNumber ?? 0;

            while (set.Contains(ret))
                ++ret;

            return ret;
        }

        public static bool IncrementSearchIndexes(int[] searchIndexes, int length, bool indexNotOverlapped = false)
        {
            for (int i = searchIndexes.Length - 1; i >= 0; --i)
            {
                int calculatedValue = searchIndexes[i] + 1;
                
                if (calculatedValue == length)
                {
                    searchIndexes[i] = 0;

                    continue;
                }

                searchIndexes[i] = calculatedValue;

                if (indexNotOverlapped && searchIndexes.Length != searchIndexes.Distinct().Count())
                    return IncrementSearchIndexes(searchIndexes, length, true);

                return true;
            }

            return false;
        }

        public static IEnumerable<T[]> GetCombination<T>(IEnumerable<T> inputSet, int outputSetLength)
        {
            List<T[]> ret = new List<T[]>();
            int inputSetCount = inputSet.Count();

            int[] searchIndexes = new int[outputSetLength];
            for (int i = 0; i < outputSetLength; ++i)
                searchIndexes[i] = i;

            do
            {
                T[] row = new T[outputSetLength];

                for (int i = 0; i < outputSetLength; ++i)
                    row[i] = inputSet.ElementAt(searchIndexes[i]);

                if (!Extensions.Contains(ret, row))
                    ret.Add(row);
            }
            while (IncrementSearchIndexes(searchIndexes, inputSetCount, true));

            return ret;
        }
    }
}
