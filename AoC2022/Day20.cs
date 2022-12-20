using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day20 : BaseDay
    {
        public Day20(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day20(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            var res = 0L;
            var index = 0;
            var arr = ParseNumbersList(1);
            MoveAround(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].number == 0)
                {
                    index = i;
                    break;
                }
            }

            res = arr[(index + 1000) % arr.Length].number
                    + arr[(index + 2000) % arr.Length].number
                    + arr[(index + 3000) % arr.Length].number;

            return res;
        }

        public override object SolvePart2()
        {
            var res = 0L;
            var index = 0;
            var arr = ParseNumbersList(811589153);

            for (int i = 0; i < 10; i++)
                MoveAround(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].number == 0)
                {
                    index = i;
                    break;
                }
            }

            res = arr[(index + 1000) % arr.Length].number
                    + arr[(index + 2000) % arr.Length].number
                    + arr[(index + 3000) % arr.Length].number;

            return res;
        }

        private void MoveAround((long value, int index)[] arr)
        {
            var steps = 0;

            while (steps < arr.Length)
            {
                var original = arr.First(a => a.index == steps);
                var originalNumber = original.value;
                var number = originalNumber;
                var i = FindIndex(arr, original.index);

                if (number == 0) { steps++; continue; }

                if (number < 0)
                {
                    number = arr.Length - (Math.Abs(number) % (arr.Length - 1)) - 1;
                }
                if (number > 0)
                {
                    var skip = number % (arr.Length - 1);
                    var newIndex = (i + skip) % arr.Length;

                    if (newIndex > i)
                    {
                        int j;
                        for (j = i + 1; j <= newIndex; j++)
                            arr[j - 1] = arr[j];
                        arr[j - 1] = new(originalNumber, original.index);
                    }
                    else
                    {
                        int j;
                        for (j = i - 1; j > newIndex; j--)
                            arr[j + 1] = arr[j];
                        arr[j + 1] = new(originalNumber, original.index);
                    }
                }

                steps++;
            }
        }

        private void Mix((long value, int index)[] arr)
        {
            int steps = 0;

            while (steps < arr.Length)
            {
                var original = arr.First(a => a.index == steps);
                var number = original.value;
                var orignalNumber = number;
                var i = FindIndex(arr, original.index);

                if (number == 0)
                {
                    steps++;
                    continue;
                }

                if (number > 0)
                {
                    var newIndex = steps + number;
                    for (int j = i + 1; j <= newIndex; j++)
                    {
                        if (j > arr.Length - 1)
                            arr[newIndex % (arr.Length - 1) - 1] = arr[newIndex % (arr.Length - 1)];
                        else
                            arr[j - 1] = arr[j];
                    }
                }

                steps++;
            }
        }

        private int FindIndex((long value, int index)[] arr, int index)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i].index == index) return i;

            return -1;
        }

        private (long number, int index)[] ParseNumbersList(long multiplier)
        {
            var arr = new (long number, int index)[FileContent.Count];

            for (int i = 0; i < FileContent.Count; i++)
                arr[i] = (long.Parse(FileContent[i]) * multiplier, i);

            return arr;
        }
    }
}
