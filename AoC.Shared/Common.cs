﻿using System.Diagnostics;

namespace AoC.Shared
{
    public static class Common
    {
        private static Stopwatch _watch;

        /// <summary>
        /// Read the input file (test or prod) to solve the problems with.
        /// </summary>
        public static List<string> GetInput(int day, int year, bool isTest, string fn = "")
        {
            var inputPath = string.Empty;
            var fileName = string.Empty;

            if (!fn.Equals(string.Empty))
                fileName = fn;
            else
                fileName = isTest ? $"day{day}_test.txt" : $"day{day}.txt";

            if (OperatingSystem.IsMacOS())
                inputPath = "/Users/mauritsseelen/Documents/Projects/AdventOfCode/AoC.Shared/Input/" + year + "/";
            else if (OperatingSystem.IsWindows())
                inputPath = "C:/Projects/AdventOfCode/AoC.Shared/Input/" + year + "/";
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

