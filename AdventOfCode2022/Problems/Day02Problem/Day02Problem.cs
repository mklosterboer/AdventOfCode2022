using AdventOfCode2022.Problems.Day02;
using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    public class Day02Problem : Problem
    {
        protected override string InputName => "Actual";

        private List<string> InputStrings { get; set; }

        public Day02Problem()
        {
            InputStrings = GetInputStringList().ToList();
        }

        public override object PartOne()
        {
            return InputStrings
                .Select(x => new ResponseGameRound(x))
                .Sum(x => x.Score);
        }

        public override object PartTwo()
        {
            return InputStrings
                .Select(x => new ResultGameRound(x))
                .Sum(x => x.Score);
        }
    }
}
