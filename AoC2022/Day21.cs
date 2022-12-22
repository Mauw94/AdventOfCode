using AoC.Shared;
using System;
namespace AoC2022
{
    public class Monkey2
    {
        public string Name { get; set; }
        public long? Value { get; set; }
        public List<string> EvalMonkeys { get; set; }
        public string Expression { get; set; }

        public Monkey2(string name, long? value = null)
        {
            Name = name;
            Value = value;
        }

        public bool HasValue => Value.HasValue;

        public long Evaluate(long val1, long val2)
        {
            if (EvalMonkeys == null) throw new ArgumentException("Monkeys to evaluate is null");

            switch (Expression)
            {
                case "+":
                    return val1 + val2;
                case "/":
                    return val1 / val2;
                case "*":
                    return val1 * val2;
                case "-":
                    return val1 - val2;
                default:
                    throw new ArgumentException($"Invalid expression term {Expression}");
            }
        }

        public bool EvaluateRoot(long val1, long val2) => val1 == val2;
    }

    public class Day21 : BaseDay
    {
        private List<Monkey2> _monkeys;
        private long _rootValue = 0;

        public Day21(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day21(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            Parse();
            return Solve(true);
        }

        public override object SolvePart2()
        {
            Parse();
            return Solve(false);
        }

        private long Solve(bool p1)
        {
            foreach (var monkey in _monkeys)
            {
                if (!monkey.HasValue)
                {
                    if (p1)
                        GetValue(monkey);
                    else
                        GetValueP2(monkey);
                }
            }

            return _rootValue;
        }

        private long GetValue(Monkey2 monkey)
        {
            var res = 0L;
            var value1 = 0L;
            var value2 = 0L;

            var m1 = _monkeys.First(m => m.Name == monkey.EvalMonkeys[0]);
            var m2 = _monkeys.First(m => m.Name == monkey.EvalMonkeys[1]);

            value1 = !m1.HasValue ? GetValue(m1) : m1.Value!.Value;
            value2 = !m2.HasValue ? GetValue(m2) : m2.Value!.Value;

            if (value1 > 0 && value2 > 0)
            {
                res = monkey.Evaluate(value1, value2);

                if (monkey.Name == "root")
                {
                    Console.WriteLine(monkey.Name + " " + res);
                    _rootValue = res;
                }
            }

            return res;
        }

        private long GetValueP2(Monkey2 monkey)
        {
            return 0L;
        }

        private void Parse()
        {
            _monkeys = new();

            foreach (var line in FileContent)
            {
                var monkeyName = line.Split(':')[0];
                var eval = line.Split(": ")[1];
                var monkey = new Monkey2(monkeyName);

                if (eval.Length == 1 || eval.Length == 2 || eval.Length == 3)
                    monkey.Value = int.Parse(eval);
                else
                {
                    var monkeys = eval.Split();
                    monkey.EvalMonkeys = new();
                    monkey.EvalMonkeys.Add(monkeys[0]);
                    monkey.EvalMonkeys.Add(monkeys[2]);
                    monkey.Expression = monkeys[1];
                }

                _monkeys.Add(monkey);
            }
        }
    }
}

