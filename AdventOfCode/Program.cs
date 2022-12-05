using AoC.Shared;
using AoC2022;


List<BaseDay> days2020 = new()
{
    new Day1(1, 2020, false)
};

List<BaseDay> days2022 = new()
{
    new Day1(1, 2022, false),
    new Day2(2, 2022, false),
    new Day3(3, 2022, false),
    new Day4(4, 2022, false),
    new Day5(5, 2022, false),
};

Run(days2020, 2020);
Run(days2022, 2022);

static void Run(List<BaseDay> days, int year)
{
    Console.WriteLine($"\nWelcome to Advent of Code {year}!\n\n");
    foreach (var day in days)
    {
        Console.WriteLine("Day{0}", day.DayNr);

        Common.StartTimeExecution();
        Console.WriteLine("\tSolution part1: {0}", day.SolvePart1());
        Common.StopTimeExecution();
        Console.Write($"\tExecuted in {Common.TimeExecutionElapsed()} ms");

        Console.WriteLine("\n");

        Common.StartTimeExecution();
        Console.WriteLine("\tSolution part2: {0}", day.SolvePart2());
        Common.StopTimeExecution();
        Console.Write($"\tExecuted in {Common.TimeExecutionElapsed()} ms");

        Console.WriteLine("\n");
    }
}