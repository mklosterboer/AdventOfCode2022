using AdventOfCode2022.Utilities;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems
{
    internal class Day05Problem : Problem
    {
        protected override string InputName => "Actual";

        private List<Instruction> Instructions { get; init; }

        // TODO: This should actually be parsed out of the file so I don't have to make duplicates
        private readonly Dictionary<int, List<string>> TestStacksPartOne = new()
        {
            {1, new List<string> {"Z", "N"} },
            {2, new List<string> {"M", "C", "D"} },
            {3, new List<string> {"P"} },
        };

        private readonly Dictionary<int, List<string>> TestStacksPartTwo = new()
        {
            {1, new List<string> {"Z", "N"} },
            {2, new List<string> {"M", "C", "D"} },
            {3, new List<string> {"P"} },
        };

        // TODO: This should actually be parsed out of the file so I don't have to make duplicates
        private readonly Dictionary<int, List<string>> ActualStacksPartOne = new()
        {
            {1, new List<string> {"H", "B", "V", "W", "N", "M", "L", "P"} },
            {2, new List<string> {"M", "Q", "H"} },
            {3, new List<string> {"N", "D", "B", "G", "F", "Q", "M", "L"} },
            {4, new List<string> {"Z", "T", "F", "Q", "M", "W", "G"} },
            {5, new List<string> {"M", "T", "H", "P"} },
            {6, new List<string> {"C", "B", "M", "J", "D", "H", "G", "T"} },
            {7, new List<string> {"M", "N", "B", "F", "V", "R"} },
            {8, new List<string> {"P", "L", "H", "M", "R", "G", "S"} },
            {9, new List<string> {"P", "D", "B", "C", "N"} },
        };

        private readonly Dictionary<int, List<string>> ActualStacksPartTwo = new()
        {
            {1, new List<string> {"H", "B", "V", "W", "N", "M", "L", "P"} },
            {2, new List<string> {"M", "Q", "H"} },
            {3, new List<string> {"N", "D", "B", "G", "F", "Q", "M", "L"} },
            {4, new List<string> {"Z", "T", "F", "Q", "M", "W", "G"} },
            {5, new List<string> {"M", "T", "H", "P"} },
            {6, new List<string> {"C", "B", "M", "J", "D", "H", "G", "T"} },
            {7, new List<string> {"M", "N", "B", "F", "V", "R"} },
            {8, new List<string> {"P", "L", "H", "M", "R", "G", "S"} },
            {9, new List<string> {"P", "D", "B", "C", "N"} },
        };

        public Day05Problem()
        {
            Instructions = new List<Instruction>();

            var rows = GetInputStringList();

            var isPastStacks = false;
            foreach (var row in rows)
            {
                if (isPastStacks)
                {
                    Instructions.Add(new Instruction(row));
                }

                if (string.IsNullOrEmpty(row))
                {
                    isPastStacks = true;
                }
            }
        }

        public override object PartOne()
        {
            var Stacks = InputName == "Actual" ? ActualStacksPartOne : TestStacksPartOne;

            foreach (var i in Instructions)
            {
                var sourceStack = Stacks[i.Start];
                var destinationStack = Stacks[i.End];

                // Find set to move and reverse it since the boxes will be picked up one at a time
                var setToMove = sourceStack.TakeLast(i.NumberToMove).Reverse();

                // Move to the new stack
                Stacks[i.End] = destinationStack.Concat(setToMove).ToList();

                // Remove from the old stack
                Stacks[i.Start] = sourceStack.Take(sourceStack.Count - i.NumberToMove).ToList();
            }

            var result = string.Empty;

            foreach (var value in Stacks.Values)
            {
                result += value.Last();
            }

            return result;
        }

        public override object PartTwo()
        {
            var Stacks = InputName == "Actual" ? ActualStacksPartTwo : TestStacksPartTwo;

            foreach (var i in Instructions)
            {
                var sourceStack = Stacks[i.Start];
                var destinationStack = Stacks[i.End];

                // Find set to move. New crane picks up all boxes at once. 
                var setToMove = sourceStack.TakeLast(i.NumberToMove);

                // Move to the new stack
                Stacks[i.End] = destinationStack.Concat(setToMove).ToList();

                // Remove from the old stack
                Stacks[i.Start] = sourceStack.Take(sourceStack.Count - i.NumberToMove).ToList();
            }

            var result = string.Empty;

            foreach (var value in Stacks.Values)
            {
                result += value.Last();
            }

            return result;
        }

        private class Instruction
        {
            public int NumberToMove { get; init; }

            public int Start { get; init; }

            public int End { get; init; }

            private static readonly Regex Parser = new(@"move (?<count>\d*) from (?<start>\d*) to (?<end>\d*)");

            public Instruction(string instruction)
            {
                var groups = Parser.Match(instruction).Groups;

                NumberToMove = int.Parse(groups["count"].Value);
                Start = int.Parse(groups["start"].Value);
                End = int.Parse(groups["end"].Value);
            }
        }
    }
}
