using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemSolving.Common
{
    public static class Extensions
    {
        public static void Print<T>(this T[] input)
        {
            for (int counter = 0; counter < input.Length; ++counter)
                Console.Write(input[counter] + ", ");

            Console.WriteLine();
        }

        public static bool Contains<T>(this IEnumerable<T[]> list, T[] itemToCheck)
        {
            int itemToCheckCount = itemToCheck.Count();

            foreach (T[] listItem in list)
            {
                int count = 0;

                for (int i = 0; i < itemToCheckCount; ++i)
                {
                    if (!EqualityComparer<T>.Default.Equals(itemToCheck[i], listItem[i]))
                        break;

                    ++count;
                }

                if (count == itemToCheckCount)
                    return true;
            }

            return false;
        }

        public static string ToString(this int[][] input)
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
    }
}
