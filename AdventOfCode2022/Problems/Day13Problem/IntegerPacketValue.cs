namespace AdventOfCode2022.Problems.Day13
{
    internal class IntegerPacketValue : IPacketValue
    {
        public int Value { get; private set; }

        public IntegerPacketValue(int value)
        {
            Value = value;
        }
    }
}
