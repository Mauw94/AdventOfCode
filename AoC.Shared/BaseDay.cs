namespace AoC.Shared
{
    public abstract class BaseDay
    {
        public int DayNr { get; set; }
        protected List<string> FileContent { get; set; } = new();

        public BaseDay(int day, int year, bool isTest)
        {
            DayNr = day;
            FileContent = Common.GetInput(day, year, isTest);
        }

        public BaseDay(int day, int year)
        {
            DayNr = day;
            FileContent = Common.GetInput(day, year, true);
        }

        public abstract object SolvePart1();

        public abstract object SolvePart2();

        protected virtual void DebugLogging(int day, int solution, long result)
        {
            Console.WriteLine($"Day: {day} \tSolution: {solution} \tResult: {result}");
        }
    }
}

