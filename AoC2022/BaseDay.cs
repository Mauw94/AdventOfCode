using System;
namespace AoC2022
{
    public abstract class BaseDay
    {
        public int DayNr { get; set; }
        protected List<string> FileContent { get; set; } = new();

        public BaseDay(int day, bool isTest)
        {
            DayNr = day;
            FileContent = Common.GetInput(day, isTest);
        }

        public BaseDay(int day)
        {
            DayNr = day;
            FileContent = Common.GetInput(day, true);
        }

        public abstract object SolvePart1();

        public abstract object SolvePart2();

        protected virtual void DebugLogging(int day, int solution, long result)
        {
            Console.WriteLine($"Day: {day} \tSolution: {solution} \tResult: {result}");
        }
    }
}

