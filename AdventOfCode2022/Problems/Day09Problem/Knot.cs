using System.Numerics;

namespace AdventOfCode2022.Problems.Day09
{
    internal class Knot
    {
        public Vector2 Location { get; set; }

        public HashSet<Vector2> Visited { get; private set; }

        public Knot(Vector2 initialLocation)
        {
            Location = initialLocation;
            Visited = new HashSet<Vector2>() { initialLocation };
        }

        public void Step(string direction)
        {
            switch (direction)
            {
                case "R":
                    StepRight();
                    break;
                case "L":
                    StepLeft();
                    break;
                case "U":
                    StepUp();
                    break;
                case "D":
                    StepDown();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void StepRight()
        {
            Location += new Vector2(1, 0);
        }

        private void StepLeft()
        {
            Location += new Vector2(-1, 0);
        }

        private void StepUp()
        {
            Location += new Vector2(0, 1);
        }

        private void StepDown()
        {
            Location += new Vector2(0, -1);
        }

        public void FollowHead(Vector2 headLocation)
        {
            if (headLocation == Location)
            {
                // Same location, don't move
                return;
            }

            var distance = headLocation - Location;

            if (headLocation.Y == Location.Y && Math.Abs(distance.X) > 1)
            {
                // Same vertical axis, but not touching, move towards head on the horizontal axis
                if (distance.X > 0)
                {
                    StepRight();
                }
                else
                {
                    StepLeft();
                }
            }
            else if (headLocation.X == Location.X && Math.Abs(distance.Y) > 1)
            {
                // Same horizontal axis, but not touching, move towards head on the verical axis
                if (distance.Y > 0)
                {
                    StepUp();
                }
                else
                {
                    StepDown();
                }
            }
            else if (Math.Abs(distance.X) > 1 || Math.Abs(distance.Y) > 1)
            {
                // Not on the same axis and not touching, move diagonally towards head
                if (headLocation.Y > Location.Y)
                {
                    StepUp();
                }
                else
                {
                    StepDown();
                }

                if (headLocation.X > Location.X)
                {
                    StepRight();
                }
                else
                {
                    StepLeft();
                }
            }

            Visited.Add(Location);
        }
    }
}
