using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolving;
using System.Collections.Generic;

namespace ProblemSolvingTest
{
    [TestClass]
    public class EasyKeypadTest
    {
        private EasyKeypad easyKeypad;

        [TestInitialize]
        public void TestInit()
        {
            easyKeypad = new EasyKeypad();
        }

        [TestMethod]
        public void SolveTest()
        {
            EasyKeypad.Input input = new EasyKeypad.Input
            {
                LengthOfPasscode = 3,
                KeypadInputs = new int[][]
                {
                    new [] { 0, 0, 0 },
                    new [] { 1, 0, 0 },
                    new [] { 0, 2, 0 },
                    new [] { 0, 1, 0 },
                }
            };

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

            List<int[]> output = easyKeypad.Solve(input);

            Assert.AreEqual(12, output.Count);

            for (int i = 0; i < expected.Count; ++i)
                for (int j = 0; j < input.LengthOfPasscode; ++j)
                    Assert.AreEqual(expected[i][j], output[i][j]);
        }
    }
}
