using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    internal class Day06Problem : Problem
    {
        protected override string InputName => "Actual";

        private List<char> Signals { get; init; }

        public Day06Problem()
        {
            Signals = GetFirstRow().ToList(); ;
        }

        public override object PartOne()
        {
            return FindStartOfFirstSignal(4);
        }

        public override object PartTwo()
        {
            return FindStartOfFirstSignal(14);
        }

        private int FindStartOfFirstSignal(int signalSize)
        {
            var result = 0;

            for (int i = 0; i < Signals.Count; i++)
            {
                var buffer = Signals.GetRange(i, signalSize);
                if (buffer.Distinct().Count() == signalSize)
                {
                    result = i + signalSize;
                    break;
                }
            }

            return result;
        }
    }
}
