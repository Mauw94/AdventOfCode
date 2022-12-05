using System;
namespace AoC2022
{
    public class Day1 : BaseDay
    {
        public Day1(int day, bool isTest)
            : base(day, isTest) { }

        public Day1(int day)
            : base(day) { }

        public override object SolvePart1()
        {
            var totalCalories = CalculateTotalCalories();
            return totalCalories.Max();
        }

        public override object SolvePart2()
        {
            var totalCalories = CalculateTotalCalories().OrderByDescending(x => x);
            var largest = totalCalories.Take(3);

            return largest.Sum();
        }

        private List<int> CalculateTotalCalories()
        {
            var totals = new List<int>();
            var temp = 0;
            for (var i = 0; i < FileContent.Count; i++)
            {
                if (FileContent[i] == "")
                {
                    totals.Add(temp);
                    temp = 0;
                }
                else
                    temp += int.Parse(FileContent[i]);

                if (i == FileContent.Count - 1)
                    totals.Add(temp);
            }

            return totals;
        }

    }
}
