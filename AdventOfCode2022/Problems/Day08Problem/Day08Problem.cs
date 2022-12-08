using AdventOfCode2022.Problems.Day08;
using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    internal class Day08Problem : Problem
    {
        protected override string InputName => "Actual";

        private Tree[,] Trees { get; init; }

        public Day08Problem()
        {
            var input = GetInputStringList().ToArray();

            var totalHeight = input.Length;
            var totalWidth = input.ElementAt(0).Length;

            Tree[,] trees = new Tree[totalHeight, totalWidth];

            for (var rowIdx = 0; rowIdx < totalHeight; rowIdx++)
            {
                for (var colIdx = 0; colIdx < totalWidth; colIdx++)
                {
                    var value = int.Parse(input[rowIdx][colIdx].ToString());

                    var tree = new Tree(value);

                    trees[rowIdx, colIdx] = tree;
                }
            }

            for (var rowIdx = 0; rowIdx < totalHeight; rowIdx++)
            {
                for (var colIdx = 0; colIdx < totalWidth; colIdx++)
                {
                    var tree = trees[rowIdx, colIdx];
                    if (rowIdx > 0)
                    {
                        tree.Top = trees[rowIdx - 1, colIdx];
                    }
                    if (colIdx > 0)
                    {
                        tree.Left = trees[rowIdx, colIdx - 1];
                    }
                    if (colIdx < totalWidth - 1)
                    {
                        tree.Right = trees[rowIdx, colIdx + 1];
                    }
                    if (rowIdx < totalHeight - 1)
                    {
                        tree.Bottom = trees[rowIdx + 1, colIdx];
                    }

                    trees[rowIdx, colIdx] = tree;
                }
            }

            Trees = trees;
        }

        public override object PartOne()
        {
            var seenCount = 0;
            foreach (var tree in Trees)
            {
                if (tree.CanBeSeen())
                {
                    seenCount++;
                }
            }
            return seenCount;
        }

        public override object PartTwo()
        {
            var highestScore = 0;
            foreach (var tree in Trees)
            {
                var scenicScore = tree.GetScenicScore();
                if (scenicScore > highestScore)
                {
                    highestScore = scenicScore;
                }
            }
            return highestScore;
        }
    }
}
