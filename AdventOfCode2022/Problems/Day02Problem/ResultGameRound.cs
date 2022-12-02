namespace AdventOfCode2022.Problems.Day02
{
    internal class ResultGameRound : GameRound
    {
        public ResultGameRound(string inputString) : base(inputString)
        {
        }

        protected override Shape GetSelfShape(char input) =>
            input switch
            {
                'X' => LosingShape,
                'Y' => OpponentShape,
                'Z' => WinningShape,
                _ => throw new NotImplementedException()
            };

        private Shape LosingShape =>
            OpponentShape switch
            {
                Shape.Rock => Shape.Scissors,
                Shape.Paper => Shape.Rock,
                Shape.Scissors => Shape.Paper,
                _ => throw new NotImplementedException()
            };
    }
}
