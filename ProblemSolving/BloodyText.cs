using System.Collections.Generic;
using System.Linq;

namespace ProblemSolving
{
    public class BloodyTextHelper
    {
        public bool DecrementPermutationSet(int[] permutationSetsLength, int[] permutationSetsMaxLength)
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

        public List<List<char[]>> DecomposeSubstitutions(List<char[]> substitutionsWithMoreThan2Substitutions)
        {
            List<List<char[]>> ret = new List<List<char[]>>();
            List<List<char[]>> possibleSubs = new List<List<char[]>>();

            foreach (char[] sub in substitutionsWithMoreThan2Substitutions)
            {
                List<char[]> possibleSub = new List<char[]>();

                for (int idx = 1; idx < sub.Length; ++idx)
                    possibleSub.Add(new[] { sub[0], sub[idx] });

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
                List<char[]> perm = new List<char[]>();

                for (int i = 0; i < possibleSubs.Count; ++i)
                {
                    char[] subPerm = possibleSubs[i][permutationSetLengths[i]];
                    perm.Add(subPerm);
                }

                ret.Add(perm);
            } while (DecrementPermutationSet(permutationSetLengths, permutationSetMaxLengths));

            return ret;
        }
    }

    public class BloodyText
    {
        private BloodyTextHelper helper;

        public BloodyText()
        {
            helper = new BloodyTextHelper();
        }

        public class Input
        {
            public string EncryptedText { get; set; }
            public string Dictionary { get; set; }
            public List<char[]> Substitutions { get; set; }
        }
        
        public Dictionary<char, List<int>> GetEncryptedTextcharacterIndexes(char[] encryptedText)
        {
            Dictionary<char, List<int>> ret = new Dictionary<char, List<int>>();

            for (var i = 0; i < encryptedText.Length; ++i)
                if (!ret.ContainsKey(encryptedText[i]))
                    ret.Add(encryptedText[i], new List<int> { i });
                else
                    ret[encryptedText[i]].Add(i);

            return ret;
        }

        public List<int> ReplaceSubstitutionsWithOneToOneRelationship(char[] encryptedMsgToReplace, List<char[]> substitutions)
        {
            var lookupCharsToReplace = GetEncryptedTextcharacterIndexes(encryptedMsgToReplace);
            List<int> decryptedIndexes = new List<int>();

            foreach (char[] sub in substitutions)
            {
                foreach (int index in lookupCharsToReplace[sub[0]])
                { 
                    encryptedMsgToReplace[index] = sub[1];
                    decryptedIndexes.Add(index);
                }
            }

            return decryptedIndexes;
        }

        public char[] Solve(Input input)
        {
            List<char[]> substitutionsWithMoreThan2Substitutions = input.Substitutions.Where(a => a.Length > 2).ToList();
            List<char[]> substitutionsWith2Substitutions = input.Substitutions.Where(a => a.Length == 2).ToList();
            char[] decryptedResult = null;

            char[] encryptedMsg = input.EncryptedText.ToCharArray();

            List<int> decryptedIndexes = ReplaceSubstitutionsWithOneToOneRelationship(encryptedMsg, substitutionsWith2Substitutions);

            List<List<char[]>> possibleSubstitutionSets = helper.DecomposeSubstitutions(substitutionsWithMoreThan2Substitutions);

            foreach (List<char[]> possibleSubstitutionSet in possibleSubstitutionSets)
            {
                if (SolveForSpecificCases(encryptedMsg, input.Dictionary, possibleSubstitutionSet, out decryptedResult, decryptedIndexes))
                    break;
            }

            return decryptedResult;
        }

        public bool SolveForSpecificCases(char[] encryptedTextUsingSubstitutions, string dictionary, List<char[]> substitutions, out char[] output, List<int> ignoredIndexes = null)
        {
            output = new char[encryptedTextUsingSubstitutions.Length];
            encryptedTextUsingSubstitutions.CopyTo(output, 0);

            var explodedDict = dictionary == null ? null : dictionary.Split(' ').GroupBy(a => a.Length);

            List<int> processedIndexes = new List<int>();
            int bookmarkIndex = 0;

            for (int i = 0; i <= output.Length; ++i)
            {
                if (ignoredIndexes != null && ignoredIndexes.Contains(i))
                    continue;

                if (i < output.Length && ReplaceWithSubstitutions(output, i, substitutions))
                {
                    processedIndexes.Add(i);
                    continue;
                }

                if (explodedDict == null || i < output.Length && output[i] != ' ')
                    continue;

                int textLength = i - bookmarkIndex;
                bool decryptedTextExistedInDictionary = false;
                var dictionaryWithAppropriateLength = explodedDict.Single(a => a.Key == textLength);

                foreach (string dictText in dictionaryWithAppropriateLength)
                {
                    int counter = 0;

                    for (int j = 0; j < textLength; ++j)
                    {
                        if (processedIndexes.Contains(bookmarkIndex + j) && dictText[j] != output[bookmarkIndex + j])
                            break;

                        ++counter;
                    }

                    if (counter == textLength)
                    { 
                        for (int j = 0; j < textLength; ++j)
                        {
                            int processingIndex = bookmarkIndex + j;
                            if (processedIndexes.Contains(processingIndex) || (ignoredIndexes != null && ignoredIndexes.Contains(processingIndex)))
                                continue;

                            output[processingIndex] = dictText[j];
                        }

                        decryptedTextExistedInDictionary = true;
                        break;
                    }
                }

                if (!decryptedTextExistedInDictionary)
                    return false;

                bookmarkIndex = i + 1;
            }

            return true;
        }

        private bool ReplaceWithSubstitutions(char[] input, int replacingIndex, List<char[]> subs)
        {
            foreach (char[] sub in subs)
            {
                if (sub[0] != input[replacingIndex])
                    continue;

                input[replacingIndex] = sub[1];
                return true;
            }

            return false;
        }
    }
}
