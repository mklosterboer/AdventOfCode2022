namespace AdventOfCode2022.DataStructures
{
    public record Vector2(int X, int Y) : IEquatable<Vector2>
    {
        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        bool IEquatable<Vector2>.Equals(Vector2 other) => X == other.X && Y == other.Y;
    }
}
