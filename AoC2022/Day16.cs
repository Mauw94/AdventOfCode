using AoC.Shared;
using System;
using System.Text.RegularExpressions;

namespace AoC2022
{
    class Valve
    {
        public Valve(string n, int f)
        {
            this.Name = n;
            this.FlowRate = f;
        }
        public Valve() { }
        public string Name { get; set; }
        public int FlowRate { get; set; }
        public List<string> LeadsTo { get; set; } = new();
        public int Minutes { get; set; } = 30;
        public Dictionary<string, int> Paths { get; set; } = new();
    }

    public class Day16 : BaseDay
    {
        private Dictionary<string, Valve> _valves = new();

        public Day16(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day16(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            Setup();
            var valvesToUse = _valves.Values.Where(v => v.FlowRate > 0).ToList();
            var max = CalculateReleasedPressure(30, valvesToUse, "AA");

            return max;
        }

        public override object SolvePart2()
        {
            Setup();
            var valvesTouse = _valves.Values.Where(v => v.FlowRate > 0).ToList();
            var max = CalculateReleasePressureWithElephant(new int[] { 26, 26 }, valvesTouse, new string[] { "AA", "AA" });

            return max;
        }

        private void Setup()
        {
            _valves = new();
            Parse();

            foreach (var v in _valves.Values)
            {
                var target = v.Name;
                var cur = _valves[target];
                cur.Paths[target] = 0;
                CalculatePathsPerValve(cur, target);
            }
        }

        private void Parse()
        {
            foreach (var line in FileContent)
            {
                var p1 = line.Split(';')[0];
                var p2 = line.Split(';')[1];
                var n = p1.Split(' ')[1];
                var f = Regex.Match(p1, @"\d+").Value;
                var valve = new Valve(n, int.Parse(f));
                var l = p2
                    .Substring(23, p2.Length - 23)
                    .Trim()
                    .Split(',')
                    .Select(x => x.Trim());

                valve.LeadsTo.AddRange(l);
                _valves[valve.Name] = valve;
            }
        }

        private void CalculatePathsPerValve(Valve current, string target)
        {
            var visited = new HashSet<string>();

            while (current != null && visited.Count < _valves.Count)
            {
                visited.Add(current.Name);
                var distance = current.Paths[target] + 1;
                foreach (var option in current.LeadsTo)
                {
                    if (!visited.Contains(option))
                    {
                        var cur = _valves[option];
                        if (cur.Paths.ContainsKey(target))
                        {
                            if (distance < cur.Paths[target])
                                cur.Paths[target] = distance;
                        }
                        else
                            cur.Paths[target] = distance;
                    }
                }

                current = _valves.Values
                    .Where(c => !visited.Contains(c.Name)
                            && c.Paths.ContainsKey(target))
                    .OrderBy(c => c.Paths[target])
                    .FirstOrDefault()!;
            }
        }

        private long CalculateReleasedPressure(int time, List<Valve> valvesToUse, string current)
        {
            var max = 0L;
            var cur = _valves[current];

            foreach (var valve in valvesToUse)
            {
                var timeLeft = time - cur.Paths[valve.Name] - 1;
                if (timeLeft > 0)
                {
                    var pressureReleased = timeLeft * valve.FlowRate
                        + CalculateReleasedPressure(timeLeft, valvesToUse.Where(v => v.Name != valve.Name).ToList(), valve.Name);
                    if (pressureReleased > max) max = pressureReleased;
                }
            }

            return max;
        }

        private long CalculateReleasePressureWithElephant(int[] time, List<Valve> valvesToUse, string[] current)
        {
            var max = 0L;
            var act = time[0] > time[1] ? 0 : 1;
            var cur = _valves[current[act]];

            foreach (var valve in valvesToUse)
            {
                var timeLeft = time[act] - cur.Paths[valve.Name] - 1;
                if (timeLeft > 0)
                {
                    var times = new int[] { timeLeft, time[1 - act] };
                    var currentValves = new string[] { valve.Name, current[1 - act] };
                    var pressureReleased = timeLeft * valve.FlowRate
                        + CalculateReleasePressureWithElephant(times, valvesToUse.Where(v => v.Name != valve.Name).ToList(), currentValves);

                    if (pressureReleased > max) max = pressureReleased;
                }
            }

            return max;
        }
    }
}
