using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems.Day05
{
    internal class Instruction
    {
        public int NumberToMove { get; init; }

        public int Source { get; init; }

        public int Destination { get; init; }

        private static readonly Regex Parser = new(@"move (?<count>\d*) from (?<start>\d*) to (?<end>\d*)");

        public Instruction(string instruction)
        {
            var groups = Parser.Match(instruction).Groups;

            NumberToMove = int.Parse(groups["count"].Value);
            Source = int.Parse(groups["start"].Value);
            Destination = int.Parse(groups["end"].Value);
        }
    }
}
