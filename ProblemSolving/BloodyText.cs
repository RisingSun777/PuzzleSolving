using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving
{
    public class BloodyText
    {
        public class Input
        {
            public string EncryptedText { get; set; }
            public string Dictionary { get; set; }
            public List<char[]> Substitutions { get; set; }
        }
        
        public Dictionary<char, List<int>> GetEncryptedTextcharacterIndexes(string encryptedText)
        {
            Dictionary<char, List<int>> ret = new Dictionary<char, List<int>>();

            for (var i = 0; i < encryptedText.Length; ++i)
                if (!ret.ContainsKey(encryptedText[i]))
                    ret.Add(encryptedText[i], new List<int> { i });
                else
                    ret[encryptedText[i]].Add(i);

            return ret;
        }

        public char[] Solve(Input input)
        {
            char[] encryptedTextUsingSubstitutions = input.EncryptedText.ToCharArray();
            string dictionary = input.Dictionary; 
            List<char[]> substitutions = input.Substitutions;

            var explodedDict = dictionary.Split(' ').GroupBy(a => a.Length);

            int bookmarkIndex = 0;

            for (int i = 0; i <= encryptedTextUsingSubstitutions.Length; ++i)
            {
                if (i != encryptedTextUsingSubstitutions.Length && encryptedTextUsingSubstitutions[i] != ' ')
                    continue;

                int textLength = i - bookmarkIndex;
                List<int> processedIndexes = new List<int>();

                var dictionaryWithAppropriateLength = explodedDict.Single(a => a.Key == textLength);

                foreach (string dictText in dictionaryWithAppropriateLength)
                {
                    int counter = 0;

                    for (int j = 0; j < textLength; ++j)
                    {
                        if (!processedIndexes.Contains(j) && ReplaceWithSubstitutions(encryptedTextUsingSubstitutions, bookmarkIndex + j, substitutions))
                        {
                            processedIndexes.Add(j);

                            if (dictText[j] == encryptedTextUsingSubstitutions[bookmarkIndex + j])
                            {
                                ++counter; 
                                continue;
                            }
                            else
                                break;
                        }

                        if (processedIndexes.Contains(j) && dictText[j] != encryptedTextUsingSubstitutions[bookmarkIndex + j])
                            break;

                        ++counter;
                    }

                    if (counter == textLength)
                    { 
                        for (int j = 0; j < textLength; ++j)
                        {
                            if (processedIndexes.Contains(j))
                                continue;

                            encryptedTextUsingSubstitutions[bookmarkIndex + j] = dictText[j];
                        }

                        break;
                    }
                }



                bookmarkIndex = i + 1;
            }

            return encryptedTextUsingSubstitutions;
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
