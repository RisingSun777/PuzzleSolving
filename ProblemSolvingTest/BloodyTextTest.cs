using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolving;

namespace ProblemSolvingTest
{
    [TestClass]
    public class BloodyTextHelperTest
    {
        private BloodyTextHelper bloodyTextHelper;

        [TestInitialize]
        public void TestInit()
        {
            bloodyTextHelper = new BloodyTextHelper();
        }

        [TestMethod]
        public void DecrementPermutationSet()
        {
            int[][] inputs = new int[][]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5 },
                new[] { 6, 7, 8 }
            };

            int[] permutationSetLengths = inputs.Select(a => a.Length).ToArray();
            int[] permutationSetMaxLengths = new int[permutationSetLengths.Length];
            Array.Copy(permutationSetLengths, permutationSetMaxLengths, 3);

            int counter = 0;

            while (bloodyTextHelper.DecrementPermutationSet(permutationSetLengths, permutationSetMaxLengths))
            {
                Console.WriteLine(string.Format("Decreasing permutation length: {0} {1} {2}", permutationSetLengths[0], permutationSetLengths[1], permutationSetLengths[2]));
                ++counter;
            }

            Assert.AreEqual(3, permutationSetLengths.Length);
            Assert.AreEqual(27, counter);
        }

        [TestMethod]
        public void DecomposeSubstitutions()
        {
            List<char[]> input = new List<char[]>()
            {
                new[] { 'b', 'a', 'f' },
                new[] { 'c', 'i' },
            };

            List<List<char[]>> output = bloodyTextHelper.DecomposeSubstitutions(input);

            Assert.AreEqual(2, output.Count);

            Assert.AreEqual(2, output[0].Count);
            Assert.AreEqual('b', output[0][0][0]); Assert.AreEqual('f', output[0][0][1]);
            Assert.AreEqual('c', output[0][1][0]); Assert.AreEqual('i', output[0][1][1]);

            Assert.AreEqual(2, output[1].Count);
            Assert.AreEqual('b', output[1][0][0]); Assert.AreEqual('a', output[1][0][1]);
            Assert.AreEqual('c', output[0][1][0]); Assert.AreEqual('i', output[0][1][1]);
        }
    }

    [TestClass]
    public class BloodyTextTest
    {
        private BloodyText bloodyText;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Console.WriteLine("Here we assume that the input is always valid, so the algorithm can run without validation.");
        }

        [TestInitialize]
        public void TestInit()
        {
            bloodyText = new BloodyText();
        }

        [TestMethod]
        public void ReplaceSubstitutionsWithOneToOneRelationship()
        {
            char[] input = "abc ddaem".ToCharArray();
            List<char[]> substitutions = new List<char[]>()
            {
                new[] { 'a', 'm' },
                new[] { 'd', 'n' }
            };

            List<int> decryptedIndexes = bloodyText.ReplaceSubstitutionsWithOneToOneRelationship(input, substitutions);

            Assert.AreEqual(4, decryptedIndexes.Count);
            Assert.AreEqual("mbc nnmem", new string(input));
        }

        [TestMethod]
        public void GetEncryptedTextcharacterIndexes_run_successfully()
        {
            string input = "ab fghhi ba";

            var ret = bloodyText.GetEncryptedTextcharacterIndexes(input.ToCharArray());

            Assert.AreEqual(input.Distinct().Count(), ret.Count);

            Assert.AreEqual(0, ret['a'][0]);
            Assert.AreEqual(10, ret['a'][1]);

            Assert.AreEqual(1, ret['b'][0]);
            Assert.AreEqual(9, ret['b'][1]);

            Assert.AreEqual(3, ret['f'][0]);

            Assert.AreEqual(4, ret['g'][0]);

            Assert.AreEqual(5, ret['h'][0]);
            Assert.AreEqual(6, ret['h'][1]);

            Assert.AreEqual(7, ret['i'][0]);
        }

        [TestMethod]
        public void Solve_for_2_params()
        {
            BloodyText.Input input = new BloodyText.Input()
            {
                EncryptedText = "pgfqp td c bt bh dhschddg",
                Substitutions = new List<char[]>
                {
                    new[] { 'b', 'a' },
                    new[] { 'c', 'i' },
                    new[] { 'd', 'e' },
                    new[] { 'f', 'u' },
                    new[] { 'g', 'r' },
                    new[] { 'h', 'n' },
                    new[] { 'p', 't' },
                    new[] { 'q', 's' },
                    new[] { 't', 'm' },
                    new[] { 's', 'g' },
                }
            };

            char[] decryptedMsg;
            bool ret = bloodyText.SolveForSpecificCases(input.EncryptedText.ToCharArray(), input.Dictionary, input.Substitutions, out decryptedMsg);

            Assert.IsTrue(ret);
            Assert.AreEqual("trust me i am an engineer", new string(decryptedMsg));
        }

        [TestMethod]
        public void Solve_for_2_params_with_missing_substitution_and_have_to_look_up_dictionary()
        {
            BloodyText.Input input = new BloodyText.Input()
            {
                EncryptedText = "pgfqp td c bt bh dhschddg",
                Substitutions = new List<char[]>
                {
                    new[] { 'b', 'a' },
                    new[] { 'c', 'i' },
                    new[] { 'd', 'e' },
                    new[] { 'f', 'u' },
                    new[] { 'g', 'r' },
                    new[] { 'h', 'n' },
                    new[] { 'p', 't' },
                    new[] { 'q', 's' },
                    new[] { 't', 'm' },
                },
                Dictionary = "rage am holy an fairy engine tale engineer i le me my trust yes thunder truth oh april goddess beach please godzilla neo matrix mogu al capone you know tired of this sheet"
            };

            char[] decryptedMsg;
            bool ret = bloodyText.SolveForSpecificCases(input.EncryptedText.ToCharArray(), input.Dictionary, input.Substitutions, out decryptedMsg);

            Assert.IsTrue(ret);
            Assert.AreEqual("trust me i am an engineer", new string(decryptedMsg));
        }

        [TestMethod]
        public void SolveForSpecificCases_for_2_params_with_missing_substitution_and_have_to_look_up_dictionary()
        {
            BloodyText.Input input = new BloodyText.Input()
            {
                EncryptedText = "pgfqp td c bt bh dhschddg",
                Substitutions = new List<char[]>
                {
                    new[] { 'b', 'a' },
                    new[] { 'c', 'i' },
                    new[] { 'd', 'e' },
                    new[] { 'f', 'u' },
                    new[] { 'g', 'r' },
                    new[] { 'h', 'n' },
                    new[] { 'p', 't' },
                    new[] { 'q', 's' },
                    new[] { 't', 'm' },
                },
                Dictionary = "rage am holy an fairy engine tale engineer i le me my trust yes thunder truth oh april goddess beach please godzilla neo matrix mogu al capone you know tired of this sheet"
            };

            char[] decryptedMsg;
            bool ret = bloodyText.SolveForSpecificCases(input.EncryptedText.ToCharArray(), input.Dictionary, input.Substitutions, out decryptedMsg);

            Assert.IsTrue(ret);
            Assert.AreEqual("trust me i am an engineer", new string(decryptedMsg));
        }

        [TestMethod]
        public void Solve_for_2_params_with_multiple_substitution_and_have_to_look_up_dictionary()
        {
            BloodyText.Input input = new BloodyText.Input()
            {
                EncryptedText = "pgfqp td c bt bh dhschddg",
                Substitutions = new List<char[]>
                {
                    new[] { 'b', 'f', 'a' },
                    new[] { 'c', 'i', 'z' },
                    new[] { 'd', 'e' },
                    new[] { 'f', 'u' },
                    new[] { 'g', 'r' },
                    new[] { 'h', 'n' },
                    new[] { 'p', 't' },
                    new[] { 'q', 's' },
                    new[] { 't', 'm' },
                },
                Dictionary = "rage am holy an fairy engine tale engineer i le me my trust yes thunder truth oh april goddess beach please godzilla neo matrix mogu al capone you know tired of this sheet"
            };

            char[] decryptedMsg = bloodyText.Solve(input);
            
            Assert.AreEqual("trust me i am an engineer", new string(decryptedMsg));
        }
    }
}
