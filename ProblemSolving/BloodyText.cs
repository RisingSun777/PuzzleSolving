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

        public char[] Solve(char[] encryptedTextUsingSubstitutions, string dictionary, List<char[]> substitutions)
        {
            var explodedDict = dictionary.Split(' ').GroupBy(a => a.Length);

            int bookmarkIndex = 0;

            for (int i = 0; i <= encryptedTextUsingSubstitutions.Length; ++i)
            {
                if (i != encryptedTextUsingSubstitutions.Length && encryptedTextUsingSubstitutions[i] != ' ')
                    continue;

                int textLength = i - bookmarkIndex;

                var dictionaryWithAppropriateLength = explodedDict.Single(a => a.Key == textLength);
                
                
                List<int> processedIndexes = new List<int>();
                //for (int j = 0; j < textLength; ++j)
                //{
                //    if (!processedIndexes.Contains(j) && ReplaceWithSubstitutions(encryptedTextUsingSubstitutions, bookmarkIndex + j, substitutions))
                //    {
                //        processedIndexes.Add(j);
                //        continue;
                //    }

                //    foreach (string dictText in dictionaryWithAppropriateLength)
                //    {
                //        tempArr[j] = dictText[j];
                //    }
                //}
                
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
       
        [Obsolete]
        public string Solve(Input input)
        {
            string ret = null;
            string[] explodedText = input.EncryptedText.Split(' ');

            Dictionary<char, List<int>> encryptedTextcharacterIndexes = GetEncryptedTextcharacterIndexes(input.EncryptedText);
            char[] encryptedTextInCharArray = input.EncryptedText.ToCharArray();

            foreach (char[] substitution in input.Substitutions)
            {
                if (substitution.Length == 2)
                    if (encryptedTextcharacterIndexes.ContainsKey(substitution[0]))
                        foreach (int index in encryptedTextcharacterIndexes[substitution[0]])
                            encryptedTextInCharArray[index] = substitution[1];


            }

            char[] alreadyProcessedChars = input.Substitutions.Select(a => a[1]).ToArray();

            //ret = new string(VerifyWithDictionary(
            //    encryptedTextInCharArray,
            //    input.Dictionary,
            //    alreadyProcessedChars));
            
            return ret;
        }
    }
}
