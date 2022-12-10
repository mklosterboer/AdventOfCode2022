using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems.Day09
{
    internal record Instruction
    {
        public string Direction;

        public int Steps;

        private static readonly Regex InstructionPattern = new(@"(?<direction>[A-Z]) (?<steps>\d*)");

        public Instruction(string rowInput)
        {
            var matchGroups = InstructionPattern.Match(rowInput).Groups;

            Direction = matchGroups["direction"].Value;
            Steps = int.Parse(matchGroups["steps"].Value);
        }
    }
}
