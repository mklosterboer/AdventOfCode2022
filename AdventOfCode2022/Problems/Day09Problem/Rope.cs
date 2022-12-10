using System.Numerics;

namespace AdventOfCode2022.Problems.Day09
{
    internal class Rope
    {
        public List<Knot> Knots { get; init; }

        public Knot Tail => Knots.Last();

        private Knot Head => Knots.First();

        public Rope(int numKnots)
        {
            Knots = new List<Knot>();
            for (var k = 0; k < numKnots; k++)
            {
                Knots.Add(new Knot(Vector2.Zero));
            }
        }

        public void Move(Instruction move)
        {
            for (var step = 0; step < move.Steps; step++)
            {
                // Move the head
                Head.Step(move.Direction);

                // Follow the head with the rest of the knots
                for (var k = 1; k < Knots.Count; k++)
                {
                    var knot = Knots.ElementAt(k);
                    var previousKnot = Knots.ElementAt(k - 1);

                    knot.Follow(previousKnot.Location);
                }
            }
        }
    }
}
