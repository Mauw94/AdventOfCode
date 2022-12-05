using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day4 : BaseDay
    {
        public Day4(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day4(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            return CalculateAssignmentPairs(true);
        }

        public override object SolvePart2()
        {
            return CalculateAssignmentPairs(false);
        }

        private int CalculateAssignmentPairs(bool isPart1)
        {
            var assignmentPairs = 0;

            foreach (var pair in FileContent)
            {
                var numberPairs = pair.Split(",");
                var (list1, list2) = ParseNumberPairToList(numberPairs[0], numberPairs[1]);

                if (isPart1)
                {
                    if (ContainsOther(list1, list2))
                        assignmentPairs++;
                }
                else
                {
                    if (HasAnyOverlap(list1, list2))
                        assignmentPairs++;
                }
            }

            return assignmentPairs;
        }

        private (List<int>, List<int>) ParseNumberPairToList(string pair1, string pair2)
        {
            var p1 = pair1.Split("-").Select(x => int.Parse(x)).ToList();
            var p2 = pair2.Split("-").Select(x => int.Parse(x)).ToList();

            p1 = p1.FillList(p1[0], p1[1]);
            p2 = p2.FillList(p2[0], p2[1]);

            return (p1, p2);
        }

        private bool ContainsOther(List<int> list1, List<int> list2)
        {
            return list1.Count >= list2.Count
                ? list1.ContainsAllItems(list2)
                : list2.ContainsAllItems(list1);
        }

        private bool HasAnyOverlap(List<int> list1, List<int> list2)
        {
            return list1.Count >= list2.Count
                ? list1.HasAnyOverlap(list2)
                : list2.HasAnyOverlap(list1);
        }
    }
}
