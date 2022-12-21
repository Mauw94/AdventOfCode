using AoC.Shared;
using System;
namespace AoC2022
{
    public class Monkey2
    {
        public string Name { get; set; }
        public int? Value { get; set; }

        public Monkey2(string name, int? value = null)
        {
            Name = name;
            Value = value;
        }
    }

    public class Day21 : BaseDay
    {
        public Day21(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day21(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            return 1;
        }

        public override object SolvePart2()
        {
            return 1;
        }
        
        private void Parse()
        {
            // make monkey obj with value or null
            foreach (var line in FileContent)
            {

            }
        }
    }
}
