using AoC.Shared;
using System;
using System.Text.RegularExpressions;

namespace AoC2022
{
    public class Monkey
    {
        public int MonekyIndex { get; set; }
        public long ItemsInspected { get; set; } = 0;
        public Queue<long> Items { get; set; } = new();
        public long WorryLevelNew { get; set; }
        public char Operator { get; set; }
        public int ThrowToMonkeyOnTrue { get; set; }
        public int ThrowToMonkeyOnFalse { get; set; }
        public long DivisibleBy { get; set; }
        public bool MultipliedByOld { get; set; } = false;
        public long MultiplyBy { get; set; }
        public long DivisibleByForP2 { get; set; }

        public void CalculateNewWorryLevel(long old, bool isPart1)
        {
            if (MultipliedByOld)
            {
                if (this.Operator == '+')
                    WorryLevelNew = old + old;
                else
                    WorryLevelNew = old * old;
            }
            else
            {
                if (this.Operator == '+')
                    WorryLevelNew = old + MultiplyBy;
                else
                    WorryLevelNew = old * MultiplyBy;
            }

            if (isPart1)
                WorryLevelNew /= 3;
            else
                WorryLevelNew %= DivisibleByForP2;
        }

        public bool IsDivisible() => WorryLevelNew % DivisibleBy == 0;

        public long GetNext()
        {
            ItemsInspected++;
            return Items.Dequeue();
        }
    }

    public class Day11 : BaseDay
    {
        private List<Monkey> _monkeys;

        public Day11(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day11(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            ParseMonkeys();
            PlayMonkeyrounds(20, true);

            return _monkeys
                .Select(x => x.ItemsInspected)
                .OrderByDescending(x => x)
                .Take(2)
                .LongProduct();
        }

        public override object SolvePart2()
        {
            ParseMonkeys();

            var denominators = _monkeys.Select(x => x.DivisibleBy);
            var mod = 1L;
            foreach (var d in denominators)
                mod *= d;
            foreach (var m in _monkeys)
                m.DivisibleByForP2 = mod;

            PlayMonkeyrounds(10000, false);

            return _monkeys
                .Select(x => x.ItemsInspected)
                .OrderByDescending(x => x)
                .Take(2)
                .LongProduct();
        }

        public void PlayMonkeyrounds(int rounds, bool p1)
        {
            for (int i = 0; i < rounds; i++)
            {
                foreach (var monkey in _monkeys)
                {
                    while (monkey.Items.Count > 0)
                    {
                        var item = monkey.GetNext();
                        monkey.CalculateNewWorryLevel(item, p1);
                        ThrowItemToMonkey(monkey, monkey.WorryLevelNew);
                    }
                }
            }
        }

        private void ThrowItemToMonkey(Monkey cur, long item)
        {
            if (cur.IsDivisible())
                _monkeys[cur.ThrowToMonkeyOnTrue].Items.Enqueue(item);
            else
                _monkeys[cur.ThrowToMonkeyOnFalse].Items.Enqueue(item);
        }

        private void ParseMonkeys()
        {
            _monkeys = new List<Monkey>();

            var index = 0;
            var monkeyIndex = 0;

            while (index < FileContent.Count)
            {
                var monkeyInfo = FileContent.Take(new Range(index, index + 6));
                var monkey = new Monkey();
                monkey.MonekyIndex = monkeyIndex;
                foreach (var info in monkeyInfo)
                {
                    if (info.Contains(" Starting items:"))
                    {
                        var items = info.Split(':')[1];
                        var itemsParsed = items.Split(',').Select(x => int.Parse(x));
                        foreach (var i in itemsParsed)
                            monkey.Items.Enqueue(i);
                    }
                    if (info.Contains(" Operation:"))
                    {
                        var o = info.Split('=')[1].Split(' ');
                        var op = o[2];
                        var multiply = o[3];
                        if (int.TryParse(multiply, out int n))
                        {
                            monkey.MultiplyBy = n;
                        }
                        else
                        {
                            monkey.MultipliedByOld = true;
                        }
                        monkey.Operator = op.ToCharArray()[0];
                    }
                    if (info.Contains(" Test:"))
                    {
                        var d = Regex.Match(info, @"\d+").Value;
                        monkey.DivisibleBy = int.Parse(d);
                    }
                    if (info.Contains("true"))
                    {
                        var n = Regex.Match(info, @"\d+").Value;
                        var mi = int.Parse(n);
                        monkey.ThrowToMonkeyOnTrue = mi;
                    }
                    if (info.Contains("false"))
                    {
                        var n = Regex.Match(info, @"\d+").Value;
                        var mi = int.Parse(n);
                        monkey.ThrowToMonkeyOnFalse = mi;
                    }
                }

                _monkeys.Add(monkey);
                monkeyIndex++;
                index += 7;
            }

        }
    }
}
