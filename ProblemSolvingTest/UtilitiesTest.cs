using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolving.Common;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSolvingTest
{
    [TestClass]
    public class UtilitiesTest
    {
        [TestMethod]
        public void IncrementSearchIndexes_test_noOverlapped_is_false_should_increment_array_with_overlapped_numbers()
        {
            int[] input = new int[] { 0, 0 };

            Console.Write("Original input: ");
            input.Print();

            for (int i = 0; i < 99; ++i) {
                Assert.IsTrue(Utilities.IncrementSearchIndexes(input, 10));
                input.Print();
            }

            Assert.IsFalse(Utilities.IncrementSearchIndexes(input, 10));
        }

        [TestMethod]
        public void IncrementSearchIndexes_test_noOverlapped_is_true_should_increment_array_with_no_overlapped_numbers()
        {
            int[] input = new int[] { 0, 1 };

            Console.Write("Original input: ");
            input.Print();

            for (int i = 0; i < 89; ++i)
            {
                Assert.IsTrue(Utilities.IncrementSearchIndexes(input, 10, true));
                input.Print();
            }

            Assert.IsFalse(Utilities.IncrementSearchIndexes(input, 10, true));
        }

        [TestMethod]
        public void GetCombination_return_correct_result()
        {
            int[] inputSet = new int[] { 0, 4, 8, 8 };
            const int outputSetLength = 3;

            List<int[]> expected = new List<int[]>
            {
                new [] { 0, 4, 8 },
                new [] { 0, 8, 4 },
                new [] { 0, 8, 8 },
                new [] { 4, 0, 8 },
                new [] { 4, 8, 0 },
                new [] { 4, 8, 8 },
                new [] { 8, 0, 4 },
                new [] { 8, 0, 8 },
                new [] { 8, 4, 0 },
                new [] { 8, 4, 8 },
                new [] { 8, 8, 0 },
                new [] { 8, 8, 4 },
            };

            List<int[]> output = Utilities.GetCombination(inputSet, outputSetLength).ToList();

            Assert.AreEqual(12, output.Count);

            for (int i = 0; i < expected.Count; ++i)
                for (int j = 0; j < outputSetLength; ++j)
                    Assert.AreEqual(expected[i][j], output[i][j]);
        }
    }
}
