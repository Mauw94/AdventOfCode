using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day25 : BaseDay
    {
        public Day25(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day25(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            var numbers = new List<long>();

            for (int i = 0; i < FileContent.Count; i++)
            {
                var num = FileContent[i].Reverse().ToList();
                var number = 0L;

                for (int j = num.Count - 1; j >= 0; j--)
                {   
                    var multiplier = (long) Math.Pow(5, j);

                    if (num[j] == '=')  number += multiplier * (-2);
                    else if (num[j] == '-') number += multiplier * (-1);
                    else
                    {
                        var value = (long) Char.GetNumericValue(num[j]);
                        number += multiplier * value;
                    }
                }
                numbers.Add(number);
            }

            var result = numbers.Sum();
            var print = result;
            var SNAFUResult = "";

            while (result > 0)
            {
                var n = result % 5;
                result /= 5;

                switch (n)
                {
                     case 0:
                        SNAFUResult = "0" + SNAFUResult;
                        break;
                    case 1:
                        SNAFUResult = "1" + SNAFUResult;
                        break;
                    case 2:
                        SNAFUResult = "2" + SNAFUResult;
                        break;
                    case 3:
                        SNAFUResult = "=" + SNAFUResult;
                        result++;
                        break;
                    case 4:
                        SNAFUResult = "-" + SNAFUResult;
                        result++;
                        break;
                    default:
                        throw new Exception();
                }
            }

            return SNAFUResult;
        }   

        public override object SolvePart2()
        {
            return 1;
        }
    }
}
