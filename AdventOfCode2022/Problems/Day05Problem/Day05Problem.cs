using AdventOfCode2022.Problems.Day05;
using AdventOfCode2022.Utilities;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems
{
    internal partial class Day05Problem : Problem
    {
        protected override string InputName => "Actual";

        private readonly List<string> InstructionRows = new();
        private readonly List<string> StackRows = new();

        private static readonly Regex CrateParser = new(@"(?<crate>[A-Z\[\]\s]{3})(\s|$)");

        public Day05Problem()
        {
            var inputRows = GetInputStringList();

            var isPastStacks = false;
            foreach (var row in inputRows)
            {
                if (isPastStacks)
                {
                    InstructionRows.Add(row);
                }
                else
                {
                    StackRows.Add(row);
                }

                if (string.IsNullOrWhiteSpace(row))
                {
                    isPastStacks = true;
                }
            }
        }

        public override object PartOne()
        {
            return Solve(InstructionRows, StackRows, CrateMover9000MoveStrategy);
        }

        public override object PartTwo()
        {
            return Solve(InstructionRows, StackRows, CrateMover9001MoveStrategy);
        }

        private static string Solve(
            IEnumerable<string> instructionRows,
            IEnumerable<string> stackRows,
            Func<IEnumerable<char>, int, IEnumerable<char>> moveStrategy)
        {
            var stacks = ParseStacks(stackRows);
            var instructions = GetInstructions(instructionRows);

            foreach (var i in instructions)
            {
                var sourceStack = stacks[i.Source];
                var destinationStack = stacks[i.Destination];

                // Find set to move. Move them according to the supplied strategy.
                var setToMove = moveStrategy(sourceStack, i.NumberToMove);

                // Move to the new stack
                stacks[i.Destination] = destinationStack.Concat(setToMove).ToList();

                // Remove from the old stack
                stacks[i.Source] = sourceStack.Take(sourceStack.Count() - i.NumberToMove).ToList();
            }

            var result = string.Empty;

            foreach (var key in stacks.Keys.OrderBy(x => x))
            {
                result += stacks[key].Last();
            }

            return result;
        }

        private static IEnumerable<char> CrateMover9000MoveStrategy(IEnumerable<char> stack, int numberToMove)
        {
            return stack.TakeLast(numberToMove).Reverse();
        }

        private static IEnumerable<char> CrateMover9001MoveStrategy(IEnumerable<char> stack, int numberToMove)
        {
            return stack.TakeLast(numberToMove);
        }

        private static Dictionary<int, IEnumerable<char>> ParseStacks(IEnumerable<string> stackRows)
        {
            var stacks = new Dictionary<int, IEnumerable<char>>();

            foreach (var row in stackRows)
            {
                var matches = CrateParser.Matches(row);

                var stackIndex = 1;
                foreach (Match match in matches)
                {
                    var crateMatch = match.Groups["crate"];
                    if (crateMatch != null && !string.IsNullOrWhiteSpace(crateMatch.Value))
                    {
                        // A valid match will be, for example, "[A]" and we need the 'A'
                        var crateValue = crateMatch.Value.ElementAt(1);

                        stacks.AddOrPrependAtKey(stackIndex, crateValue);
                    }

                    stackIndex++;
                };
            }

            return stacks;
        }

        private static IEnumerable<Instruction> GetInstructions(IEnumerable<string> instructionRows)
        {
            return instructionRows.Select(row => new Instruction(row)).ToList();
        }
    }
}
