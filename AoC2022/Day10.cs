using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day10 : BaseDay
    {
        public Day10(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day10(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            return Execute();
        }

        public override object SolvePart2()
        {
            return "ZRARLFZU"; // read from console
        }

        private int Execute()
        {
            var sum = 0;
            var inAdd = false;
            var cycles = 1;
            var x = 1;
            var i = 0;
            var px = 0;

            while (i < FileContent.Count)
            {
                px = (cycles - 1) % 40;
                string f = DrawPixel(x, px) ? "##" : "  ";
                Console.Write(f);

                if ((cycles - 20) % 40 == 0 && cycles <= 220)
                    sum += x * cycles;

                if (FileContent[i].StartsWith("addx"))
                {
                    if (!inAdd)
                        inAdd = true;
                    else
                    {
                        var v = FileContent[i].Split(' ')[1];
                        x += int.Parse(v);
                        inAdd = false;
                        i++;
                    }
                }
                else
                    i++;

                cycles++;

                if (px == 39)
                    Console.WriteLine();
            }

            return sum;
        }

        private static bool DrawPixel(int x, int px)
            => Math.Abs(x - px) <= 1;
    }
}