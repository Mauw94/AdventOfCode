using System.Diagnostics;

namespace AoC2022
{
    public static class Common
    {
        private static Stopwatch _watch;

        /// <summary>
        /// Read the input file (test or prod) to solve the problems with.
        /// </summary>
        public static List<string> GetInput(int day, bool isTest)
        {
            var inputPath = string.Empty;
            var fileName = isTest ? $"day{day}_test.txt" : $"day{day}.txt";

            if (OperatingSystem.IsMacOS())
                inputPath = "/Users/mauritsseelen/Projects/AoC2022/AoC2022/Input/";
            else if (OperatingSystem.IsWindows())
                inputPath = "C:/Projects/AoC2022/AoC2022/Input/";
            else
                throw new Exception("OS linux or others not implemented for file reading yet.");

            var file = inputPath + fileName;

            if (File.Exists(file))
                return File.ReadAllLines(file).ToList();

            throw new FileNotFoundException($"Could't find file name: {fileName} in path: {inputPath}");
        }

        public static void StartTimeExecution() => _watch = Stopwatch.StartNew();
        public static void StopTimeExecution() => _watch.Stop();
        public static long TimeExecutionElapsed() => _watch.ElapsedMilliseconds;
    }
}

