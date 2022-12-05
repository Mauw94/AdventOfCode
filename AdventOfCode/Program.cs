using AoC2022;

Console.WriteLine("Welcome to Advent of Code 2022!");
List<BaseDay> days = new()
{
    new Day1(1, false),
    new Day2(2, false),
    new Day3(3, false),
    new Day4(4, false),
    new Day5(5, false),
};

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