namespace AdventOfCode2022.Problems.Day02
{
    internal class ResponseGameRound : GameRound
    {
        public ResponseGameRound(string inputString) : base(inputString)
        { }

        protected override Shape GetSelfShape(char input) =>
            input switch
            {
                'X' => Shape.Rock,
                'Y' => Shape.Paper,
                'Z' => Shape.Scissors,
                _ => throw new NotImplementedException()
            };
    }
}
