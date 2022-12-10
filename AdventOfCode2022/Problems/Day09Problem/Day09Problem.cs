using AdventOfCode2022.Problems.Day09;
using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    internal class Day09Problem : Problem
    {
        protected override string InputName => "Actual";

        private IEnumerable<string> Input { get; init; }

        public Day09Problem()
        {
            Input = GetInputStringList();
        }

        public override object PartOne()
        {
            return Solve(2);
        }

        public override object PartTwo()
        {
            return Solve(10);
        }

        private int Solve(int numKnots)
        {
            var rope = new Rope(numKnots);

            foreach (var row in Input)
            {
                var instruction = new Instruction(row);
                rope.Move(instruction);
            }

            return rope.Tail.Visited.Count;
        }
    }
}
