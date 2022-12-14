using AoC.Shared;
using System;
using System.Text.Json.Nodes;

namespace AoC2022
{
    public class Day13 : BaseDay
    {
        public Day13(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day13(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            var packetsInOrder = FileContent
                .Where(x => x != string.Empty)
                .Select(GetPacket)
                .Chunk(2)
                .Select((pair, index) => Compare(pair[0], pair[1]) < 0 ? index + 1 : 0)
                .Sum();

            return packetsInOrder;
        }

        public override object SolvePart2()
        {
            var dividers = new List<JsonNode>();
            dividers.Add(GetPacket("[[2]]"));
            dividers.Add(GetPacket("[[6]]"));

            var packets = FileContent
                .Where(x => x != string.Empty)
                .Select(GetPacket)
                .Concat(dividers)
                .ToList();

            packets.Sort(Compare);

            var d1 = packets.IndexOf(dividers[0]) + 1;
            var d2 = packets.IndexOf(dividers[1]) + 1;

            return d1 * d2;
        }

        JsonNode GetPacket(string input) =>
            JsonNode.Parse(input);

        int Compare(JsonNode nodeA, JsonNode nodeB)
        {
            if (nodeA is JsonValue && nodeB is JsonValue)
            {
                return (int)nodeA - (int)nodeB;
            }
            else
            {
                var arr1 = nodeA as JsonArray ?? new JsonArray((int)nodeA);
                var arr2 = nodeB as JsonArray ?? new JsonArray((int)nodeB);

                return Enumerable.Zip(arr1, arr2)
                    .Select(p => Compare(p.First, p.Second))
                    .FirstOrDefault(c => c != 0, arr1.Count - arr2.Count);
            }
        }
    }
}
