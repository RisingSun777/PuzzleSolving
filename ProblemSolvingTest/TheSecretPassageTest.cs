using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSolving;
using ProblemSolving.Common;

namespace ProblemSolvingTest
{
    [TestClass]
    public class TheSecretPassageHelperTest
    {
        private TheSecretPassageHelper helper;

        [TestInitialize]
        public void TestInit()
        {
            helper = new TheSecretPassageHelper();
        }

        [TestMethod]
        public void RotateClockwise()
        {
            int[][] expected = new int[][]
            {
                new[] { 0, 0, 0, 0 },
                new[] { 0, 0, 1, 1 },
                new[] { 0, 0, 1, 1 },
                new[] { 0, 0, 0, 0 },
            };

            int[][] ret = helper.RotateClockwise(new int[][] 
            {
                new[] { 0, 1, 1, 0 },
                new[] { 0, 1, 1, 0 },
                new[] { 0, 0, 0, 0 },
                new[] { 0, 0, 0, 0 },
            });

            string expectedStr = Extensions.ToString(expected);
            string retStr = Extensions.ToString(ret);

            Assert.AreEqual(expectedStr, retStr);

            expected = new int[][]
            {
                new[] { 1, 1, 1, 1 },
                new[] { 1, 0, 1, 0 },
                new[] { 1, 1, 0, 0 },
                new[] { 1, 0, 0, 0 },
            };

            ret = helper.RotateClockwise(new int[][] 
            {
                new[] { 1, 0, 0, 0 },
                new[] { 1, 1, 0, 0 },
                new[] { 1, 0, 1, 0 },
                new[] { 1, 1, 1, 1 },
            });

            retStr = Extensions.ToString(ret);
            expectedStr = Extensions.ToString(expected);

            Assert.AreEqual(expectedStr, retStr);
        }
    }

    [TestClass]
    public class TheSecretPassageTest
    {
        private TheSecretPassage theSecretPassage;

        [TestInitialize]
        public void TestInit()
        {
            theSecretPassage = new TheSecretPassage();
        }

        [TestMethod]
        public void GetBlueprintPermutationsTest()
        {
            var input = new List<int[][]>
            {
                new int[][]
                {
                    new[] { 0, 1, 1, 0 },
                    new[] { 0, 1, 1, 0 },
                    new[] { 0, 0, 0, 0 },
                    new[] { 0, 0, 0, 0 },
                },
                new int[][]
                {
                    new[] { 1, 1, 1, 1 },
                    new[] { 0, 0, 0, 0 },
                    new[] { 0, 0, 0, 0 },
                    new[] { 0, 0, 0, 0 },
                },
            };

            List<List<int[][][]>> output = theSecretPassage.GetBlueprintPermutations(input);

            Assert.AreEqual(16, output.Count);

            foreach (List<int[][][]> perm in output)
            {
                Assert.AreEqual(2, perm.Count);

                var firstBlueprint = Extensions.ToString(perm[0][0]);
                var secondBlueprint = Extensions.ToString(perm[1][0]);
            }
        }

        [TestMethod]
        public void TestSolve()
        {
            TheSecretPassage.Input input = new TheSecretPassage.Input
            {
                SideLengthOfBlueprints = 4,
                Blueprints = new List<int[][]>
                {
                    new int[][]
                    {
                        new[] { 0, 1, 1, 0 },
                        new[] { 0, 1, 1, 0 },
                        new[] { 0, 0, 0, 0 },
                        new[] { 0, 0, 0, 0 },
                    },
                    new int[][]
                    {
                        new[] { 0, 0, 0, 0 },
                        new[] { 0, 0, 1, 1 },
                        new[] { 0, 0, 1, 1 },
                        new[] { 0, 0, 0, 0 },
                    },
                }
            };

            TheSecretPassage.Square output = theSecretPassage.Solve(input);

            Assert.AreEqual(0, output.X);
            Assert.AreEqual(1, output.Y);
            Assert.AreEqual(2, output.SideLength);
        }

        [TestMethod]
        public void TestSolve_AnotherInput()
        {
            TheSecretPassage.Input input = new TheSecretPassage.Input
            {
                SideLengthOfBlueprints = 4,
                Blueprints = new List<int[][]>
                {
                    new int[][]
                    {
                        new[] { 0, 1, 1, 1 },
                        new[] { 0, 1, 1, 1 },
                        new[] { 0, 1, 1, 1 },
                        new[] { 0, 0, 0, 0 },
                    },
                    new int[][]
                    {
                        new[] { 0, 0, 0, 0 },
                        new[] { 1, 1, 1, 0 },
                        new[] { 1, 1, 1, 0 },
                        new[] { 1, 1, 1, 0 },
                    },

                    new int[][]
                    {
                        new[] { 0, 0, 0, 0 },
                        new[] { 0, 0, 0, 0 },
                        new[] { 0, 0, 1, 1 },
                        new[] { 0, 0, 1, 1 },
                    },
                }
            };

            TheSecretPassage.Square output = theSecretPassage.Solve(input);

            Assert.AreEqual(2, output.SideLength);
            Assert.AreEqual(0, output.X);
            Assert.AreEqual(2, output.Y);
        }
    }
}
