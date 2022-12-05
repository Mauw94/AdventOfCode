using AoC.Shared;

namespace AoC2020
{
    public class Day1 : BaseDay
    {
        public Day1(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day1(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            var numbers = FileContent.Select(x => int.Parse(x)).ToList();

            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = 0; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == 2020)
                        return numbers[i] * numbers[j];
                }
            }

            throw new ArgumentException();
        }

        public override object SolvePart2()
        {
            var numbers = FileContent.Select(x => int.Parse(x)).ToList();

            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = 0; j < numbers.Count; j++)
                {
                    for (int k = 0; k < numbers.Count; k++)
                    {
                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                            return numbers[i] * numbers[j] * numbers[k];
                    }
                }
            }

            throw new ArgumentException();
        }
    }
}