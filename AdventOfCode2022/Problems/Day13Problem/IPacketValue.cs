namespace AdventOfCode2022.Problems.Day13
{
    internal interface IPacketValue { }

    internal record ListPacketValue(IPacketValue[] PacketValues) : IPacketValue { }

    internal record IntegerPacketValue(int Value) : IPacketValue { }
}
