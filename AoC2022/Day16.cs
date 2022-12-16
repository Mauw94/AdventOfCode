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
    }

    public class Day16 : BaseDay
    {
        private List<Valve> _valves = new();

        public Day16(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day16(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            Parse();

            var root = _valves.First(v => v.Name == "AA");
            var pressure = OpenValves(root);

            return pressure;
        }

        public override object SolvePart2()
        {
            return 1;
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
                _valves.Add(valve);
            }
        }

        private int OpenValves(Valve root)
        {
            var queue = new Queue<(Valve v, int p)>();
            var opened = new HashSet<Valve>();
            var minutes = 30;
            var pressure = 0;

            queue.Enqueue((root, 0));

            while (queue.Any())
            {
                var cur = queue.Dequeue();
                --minutes;
                var options = new List<Valve>();

                foreach (var option in cur.v.LeadsTo)
                {
                    var valve = _valves.First(v => v.Name.Equals(option))!;
                    if (valve == null) continue;
                    if (opened.Contains(valve))
                        continue;
                    else
                        options.Add(valve);
                }

                if (options.Count == 0)
                {
                    // it ends here?
                    break;
                }


// keep track of minutes for each option
// explore each option and suboption within
// keep track of max pressure

                var valveToOpen = options.Where(o => o.FlowRate == options.Max(x => x.FlowRate)).First();
                opened.Add(valveToOpen);
                --minutes;
                var pressureReleased = minutes * valveToOpen.FlowRate;
                pressure += pressureReleased;
                queue.Enqueue((valveToOpen, pressureReleased));
            }

            return pressure;
        }
    }
}
