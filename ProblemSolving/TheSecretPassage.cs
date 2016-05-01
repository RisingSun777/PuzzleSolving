using ProblemSolving.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSolving
{
    public class TheSecretPassageHelper
    {
        public int[][] RotateClockwise(int[][] blueprint)
        {
            int sideLength = blueprint.Length;

            int[][] ret = new int[sideLength][];
            for (int i = 0; i < sideLength; ++i)
                ret[i] = new int[sideLength];

            for (int i = 0; i < sideLength; ++i)
            {
                for (int j = 0; j < sideLength; ++j)
                {
                    ret[j][sideLength - i - 1] = blueprint[i][j];
                }
            }

            return ret;
        }
    }

    public class TheSecretPassage
    {
        private TheSecretPassageHelper helper;

        public TheSecretPassage()
        {
            helper = new TheSecretPassageHelper();
        }

        public class Input
        {
            public int SideLengthOfBlueprints { get; set; }
            public List<int[][]> Blueprints { get; set; }
        }

        public class Square
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int SideLength { get; set; }
        }

        private bool IsSquareExist(int[][] blueprint, int x, int y, int expectedSideLength)
        {
            for (int i = x; i < x + expectedSideLength; ++i)
                for (int j = y; j < y + expectedSideLength; ++j)
                    if (blueprint[i][j] == 0)
                        return false;

            return true;
        }

        //TODO: Limit the number of redundant permutations
        public List<List<int[][][]>> GetBlueprintPermutations(List<int[][]> blueprints)
        {
            List<int[][][]> fourShadesOfBlueprints = new List<int[][][]>();

            for (int i = 0; i < blueprints.Count; ++i)
            {
                int[][] rotatedObject = blueprints[i];
                int[][][] fourShadesOfABlueprint = new int[4][][];

                for (int j = 0; j < 4; ++j)
                {
                    rotatedObject = helper.RotateClockwise(rotatedObject);
                    fourShadesOfABlueprint[j] = rotatedObject;
                }

                fourShadesOfBlueprints.Add(fourShadesOfABlueprint);
            }

            List<List<int[][][]>> permutatedResult = Utilities.GeneratePermutations(fourShadesOfBlueprints);

            return permutatedResult;
        }

        public Square Solve(Input input)
        {
            Square ret = new Square();

            List<List<int[][][]>> permutations = GetBlueprintPermutations(input.Blueprints.Skip(1).ToList());

            for (int outputSideLength = input.SideLengthOfBlueprints; outputSideLength > 0; --outputSideLength)
            {
                for (int i = 0; i <= input.SideLengthOfBlueprints - outputSideLength; ++i)
                {
                    for (int j = 0; j <= input.SideLengthOfBlueprints - outputSideLength; ++j)
                    {
                        if (IsSquareExist(input.Blueprints[0], i, j, outputSideLength))
                        {
                            foreach (List<int[][][]> permSet in permutations)
                            {
                                int counter = 0;

                                foreach (int[][][] rotatedBlueprint in permSet)
                                {
                                    if (!IsSquareExist(rotatedBlueprint[0], i, j, outputSideLength))
                                        break;

                                    ++counter;
                                }

                                if (counter == permSet.Count)
                                    return new Square
                                    {
                                        X = i,
                                        Y = j,
                                        SideLength = outputSideLength
                                    };
                            }
                        }
                    }
                }
            }
            
            return ret;
        }
    }
}
