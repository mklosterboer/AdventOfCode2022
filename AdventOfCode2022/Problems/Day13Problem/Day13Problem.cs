using AdventOfCode2022.Problems.Day13;
using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Problems
{
    internal class Day13Problem : Problem
    {
        protected override string InputName => "Actual";

        private static PacketComparer Comparer = new PacketComparer();

        private IEnumerable<string> Rows { get; init; }

        public Day13Problem()
        {
            Rows = GetInputStringList();
        }

        public override object PartOne()
        {
            var rowLength = Rows.Count();

            var result = 0;

            var packetIndex = 1;
            for (var rowIndex = 0; rowIndex < rowLength; rowIndex += 3)
            {

                var left = PacketParser.Parse(Rows.ElementAt(rowIndex));
                var right = PacketParser.Parse(Rows.ElementAt(rowIndex + 1));

                if (Comparer.Compare(left, right) < 0)
                {
                    result += packetIndex;
                }

                packetIndex++;
            }

            return result;
        }

        public override object PartTwo()
        {

            var packets = new List<ListPacketValue>();

            foreach (var row in Rows)
            {
                if (!string.IsNullOrWhiteSpace(row))
                {
                    packets.Add(PacketParser.Parse(row));
                }
            }

            var firstDividerPacket = new ListPacketValue(
                new IPacketValue[] {
                    new ListPacketValue(
                        new IPacketValue[] {
                            new IntegerPacketValue(2)
                        })
                });
            packets.Add(firstDividerPacket);

            var secondDividerPacket = new ListPacketValue(
                new IPacketValue[] {
                    new ListPacketValue(
                        new IPacketValue[] {
                            new IntegerPacketValue(6)
                        })
                });
            packets.Add(secondDividerPacket);

            packets.Sort(Comparer);

            var firstDividerIndex = packets.FindIndex(x => x == firstDividerPacket) + 1;
            var secondDividerIndex = packets.FindIndex(x => x == secondDividerPacket) + 1;

            return firstDividerIndex * secondDividerIndex;
        }
    }
}
