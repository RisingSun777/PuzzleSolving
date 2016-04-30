using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolving;

namespace ProblemSolvingTest
{
    [TestClass]
    public class BloodyTextTest
    {
        private BloodyText bloodyText;

        [TestInitialize]
        public void TestInit()
        {
            bloodyText = new BloodyText();
        }

        [TestMethod]
        public void GetEncryptedTextcharacterIndexes()
        {
            string input = "ab fghhi ba";

            var ret = bloodyText.GetEncryptedTextcharacterIndexes(input);

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

            string ret = bloodyText.Solve(input);

            Assert.AreEqual("trust me i am an engineer", ret);
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

            char[] ret = bloodyText.Solve(input.EncryptedText.ToCharArray(), input.Dictionary, input.Substitutions);
            Assert.AreEqual("trust me i am an engineer", new string(ret));

            //string ret = bloodyText.Solve(input);
            //Assert.AreEqual("trust me i am an engineer", ret);
        }
    }
}
