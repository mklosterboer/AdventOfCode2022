namespace AdventOfCode2022.Problems.Day13
{
    internal class PacketComparer : IComparer<IPacketValue>
    {
        public int Compare(IPacketValue? left, IPacketValue? right)
        {
            if (left == null || right == null)
            {
                throw new ArgumentNullException();
            }

            return ComparePackets(left, right);
        }

        private static int ComparePackets(IPacketValue left, IPacketValue right)
        {
            if (left is IntegerPacketValue leftInteger && right is IntegerPacketValue rightInteger)
            {
                return leftInteger.Value.CompareTo(rightInteger.Value);
            }

            var leftList = EnsureValueIsList(left);
            var rightList = EnsureValueIsList(right);

            if (leftList.PacketValues.Length == 0 && rightList.PacketValues.Length == 0)
            {
                // If both are empty, there is no comparison necessary
                return 0;
            }

            var shortestList = Math.Min(leftList.PacketValues.Length, rightList.PacketValues.Length);

            // Compare the two lists
            for (var i = 0; i < shortestList; i++)
            {
                var leftValue = leftList.PacketValues.ElementAtOrDefault(i);
                var rightValue = rightList.PacketValues.ElementAtOrDefault(i);

                var comparisonResult = ComparePackets(leftValue, rightValue);
                if (comparisonResult != 0)
                {
                    return comparisonResult;
                }
            }

            // They are equal, so check the list length instead.
            return Math.Sign(leftList.PacketValues.Length - rightList.PacketValues.Length);
        }

        private static ListPacketValue EnsureValueIsList(IPacketValue value) =>
            value switch
            {
                ListPacketValue listPacket => listPacket,
                IntegerPacketValue integerPacket => new ListPacketValue(new IPacketValue[] { integerPacket }),
                _ => throw new NotImplementedException()
            };
    }
}
