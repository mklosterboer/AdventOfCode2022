namespace AdventOfCode2022.Problems.Day13
{
    internal class ListPacketValue : IPacketValue
    {
        public List<IPacketValue> PacketValues { get; private set; }

        public ListPacketValue(List<IPacketValue> packetValues)
        {
            PacketValues = packetValues;
        }
    }
}
