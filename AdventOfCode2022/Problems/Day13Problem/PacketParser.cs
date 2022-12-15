using System.Text.Json;

namespace AdventOfCode2022.Problems.Day13
{
    internal static class PacketParser
    {
        public static ListPacketValue Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new ListPacketValue(new List<IPacketValue>());
            }

            var element = (JsonElement)JsonSerializer.Deserialize<object>(input);

            return (ListPacketValue)FromJsonElement(element);
        }

        private static IPacketValue FromJsonElement(JsonElement element) =>
            element.ValueKind switch
            {
                JsonValueKind.Number => new IntegerPacketValue(element.GetInt32()),
                JsonValueKind.Array => new ListPacketValue(element.EnumerateArray().Select(FromJsonElement).ToList()),
                _ => throw new NotImplementedException()
            };

    }
}
