using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day3 : BaseDay
    {
        private const int _lowerCaseA = 97;
        private const int _upperCaseA = 65;

        public Day3(int day, int year, bool isTest) : base(day, year,isTest) { }

        public Day3(int day, int year) : base(day, year) { }

        public override object SolvePart1() => ReorganizeRucksack();

        public override object SolvePart2() => ReorganizeRucksackPart2();

        private int ReorganizeRucksack()
        {
            var priorities = 0;

            foreach (var compartment in FileContent)
                priorities += CheckItemPriority(compartment);

            return priorities;
        }

        private int ReorganizeRucksackPart2()
        {
            var priorities = 0;
            var index = 0;

            while (index < FileContent.Count)
            {
                if (index + 3 <= FileContent.Count)
                {
                    var lines = FileContent.Take(new Range(index, index + 3)).ToList();
                    priorities += CheckItemPriorityPart2(lines[0], lines[1], lines[2]);
                }
                index += 3;
            }

            return priorities;
        }

        private int CheckItemPriorityPart2(string line1, string line2, string line3)
        {
            // checking with .Any() performans much better than 3 for loops.
            foreach (var c in line1)
            {
                if (line2.Any(i => i == c) &&
                    line3.Any(i => i == c))
                    return GetPriorityValue(c);
            }

            throw new ArgumentException("Input is not valid.");
        }

        private int CheckItemPriority(string line)
        {
            var first = line.Substring(0, line.Length / 2);
            var last = line.Substring(line.Length / 2, line.Length / 2);

            // for (int i = 0; i < first.Length; i++)
            //     for (int j = 0; j < last.Length; j++)
            //         if (first[i] == last[j])
            //             return GetPriorityValue(first[i]);

            // Alternative.
            foreach (var c in first)
                if (last.Any(x => x == c))
                    return GetPriorityValue(c);

            throw new ArgumentException("Input is not valid");
        }

        private int GetPriorityValue(char c)
        {
            return (Char.IsUpper(c))
               ? c - _upperCaseA + 27
               : (c - _lowerCaseA) + 1;
        }
    }
}
