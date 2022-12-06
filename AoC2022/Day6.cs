using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day6 : BaseDay
    {
        public Day6(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day6(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            return FindMarker(4);
        }

        public override object SolvePart2()
        {
            return FindMarker(14);
        }

        private int FindMarker(int rangeToCheck)
        {
            var index = 0;
            var markerFound = false;

            while (!markerFound)
            {
                markerFound = AllUnique(FileContent[0].Take(new Range(index, index + rangeToCheck)));
                if (markerFound)
                    break;
                index++;
            }

            return index + rangeToCheck;
        }

        private bool AllUnique(IEnumerable<char> characters)
        {
            bool[] array = new bool[256];
            foreach (char c in characters)
                if (array[(int)c])
                    return false;
                else
                    array[(int)c] = true;

            return true;
        }
    }
}
