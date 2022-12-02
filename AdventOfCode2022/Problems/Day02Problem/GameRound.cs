namespace AdventOfCode2022.Problems.Day02
{
    internal abstract class GameRound
    {
        public int Score { get; init; }

        protected Shape SelfShape { get; set; }

        protected Shape OpponentShape { get; init; }

        public GameRound(string inputString)
        {
            OpponentShape = GetOpponentShape(inputString.ElementAt(0));
            SelfShape = GetSelfShape(inputString.ElementAt(2));
            Score = GetScore();
        }

        protected abstract Shape GetSelfShape(char input);

        protected Shape WinningShape =>
            OpponentShape switch
            {
                Shape.Rock => Shape.Paper,
                Shape.Paper => Shape.Scissors,
                Shape.Scissors => Shape.Rock,
                _ => throw new NotImplementedException()
            };

        private static Shape GetOpponentShape(char input) =>
            input switch
            {
                'A' => Shape.Rock,
                'B' => Shape.Paper,
                'C' => Shape.Scissors,
                _ => throw new NotImplementedException()
            };

        private int ShapeScore =>
            SelfShape switch
            {
                Shape.Rock => 1,
                Shape.Paper => 2,
                Shape.Scissors => 3,
                _ => 0
            };

        private int GetScore()
        {
            if (SelfShape == OpponentShape)
            {
                return ShapeScore + 3;
            }

            if (SelfShape == WinningShape)
            {
                return ShapeScore + 6;
            }

            return ShapeScore;
        }
    }

    internal enum Shape
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2
    }
}
