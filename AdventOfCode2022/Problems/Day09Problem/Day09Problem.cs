using AdventOfCode2022.DataStructures;
using AdventOfCode2022.Problems.Day09;
using AdventOfCode2022.Utilities;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Problems
{
    internal class Day09Problem : Problem
    {
        protected override string InputName => "Actual";

        private IEnumerable<string> Input { get; init; }

        private static readonly Regex MovePattern = new(@"(?<direction>[A-Z]) (?<steps>\d*)");

        public Day09Problem()
        {
            Input = GetInputStringList();
        }

        public override object PartOne()
        {
            var head = new Knot(new Vector2(0, 0));
            var tail = new Knot(new Vector2(0, 0));

            foreach (var row in Input)
            {
                var (direction, steps) = ParseInstruction(row);

                for (var s = 0; s < steps; s++)
                {
                    head.Step(direction);
                    tail.FollowHead(head.Location);
                }
            }

            return tail.Visited.Count;
        }

        public override object PartTwo()
        {
            var head = new Knot(new Vector2(0, 0));
            var knot1 = new Knot(new Vector2(0, 0));
            var knot2 = new Knot(new Vector2(0, 0));
            var knot3 = new Knot(new Vector2(0, 0));
            var knot4 = new Knot(new Vector2(0, 0));
            var knot5 = new Knot(new Vector2(0, 0));
            var knot6 = new Knot(new Vector2(0, 0));
            var knot7 = new Knot(new Vector2(0, 0));
            var knot8 = new Knot(new Vector2(0, 0));
            var tail = new Knot(new Vector2(0, 0));

            foreach (var row in Input)
            {
                var (direction, steps) = ParseInstruction(row);

                for (var s = 0; s < steps; s++)
                {
                    head.Step(direction);
                    knot1.FollowHead(head.Location);
                    knot2.FollowHead(knot1.Location);
                    knot3.FollowHead(knot2.Location);
                    knot4.FollowHead(knot3.Location);
                    knot5.FollowHead(knot4.Location);
                    knot6.FollowHead(knot5.Location);
                    knot7.FollowHead(knot6.Location);
                    knot8.FollowHead(knot7.Location);
                    tail.FollowHead(knot8.Location);
                }
            }

            return tail.Visited.Count;
        }

        private static (string direction, int steps) ParseInstruction(string row)
        {
            var matchGroups = MovePattern.Match(row).Groups;

            return (matchGroups["direction"].Value, int.Parse(matchGroups["steps"].Value));
        }
    }
}
