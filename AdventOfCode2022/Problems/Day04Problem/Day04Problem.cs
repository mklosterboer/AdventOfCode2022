using AdventOfCode2022.Utilities;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems
{
    internal class Day04Problem : Problem
    {
        protected override string InputName => "Actual";

        private static readonly Regex AssignmentRegex = new(@"(?<a1>\d*-\d*),(?<a2>\d*-\d*)");

        private IEnumerable<(Assignment assignmentOne, Assignment assignmentTwo)> Assignments { get; init; }

        public Day04Problem()
        {
            Assignments = GetInputStringList()
                .Select(row =>
                {
                    var matchGroups = AssignmentRegex.Match(row).Groups;

                    var assignmentOne = new Assignment(matchGroups["a1"].Value);
                    var assignmentTwo = new Assignment(matchGroups["a2"].Value);

                    return (assignmentOne, assignmentTwo);
                });

        }

        public override object PartOne()
        {
            var fullyContainedSets = 0;

            foreach (var (assignmentOne, assignmentTwo) in Assignments)
            {
                if (
                    assignmentOne.FullyContains(assignmentTwo)
                    || assignmentTwo.FullyContains(assignmentOne))
                {
                    fullyContainedSets++;
                }
            }

            return fullyContainedSets;
        }

        public override object PartTwo()
        {
            var overlappingSets = 0;

            foreach (var (assignmentOne, assignmentTwo) in Assignments)
            {
                if (assignmentOne.Overlap(assignmentTwo))
                {
                    overlappingSets++;
                }
            }

            return overlappingSets;
        }

        private class Assignment
        {
            public IEnumerable<int> Range { get; init; }

            private static readonly Regex RangeRegex = new(@"(?<start>\d*)-(?<end>\d*)");

            public Assignment(string assignment)
            {
                var match = RangeRegex.Match(assignment);

                var start = int.Parse(match.Groups["start"].Value);
                var count = int.Parse(match.Groups["end"].Value) - start + 1;

                Range = Enumerable.Range(start, count);
            }

            public bool FullyContains(Assignment otherAssignment)
            {
                var exclusiveRange = Range.Except(otherAssignment.Range);

                return !exclusiveRange.Any();
            }

            public bool Overlap(Assignment otherAssignment)
            {
                var intersection = Range.Intersect(otherAssignment.Range);

                return intersection.Any();
            }
        }
    }
}
