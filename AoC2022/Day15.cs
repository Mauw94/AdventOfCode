using AoC.Shared;
using System;
using System.Text.RegularExpressions;

namespace AoC2022
{
    public class Day15 : BaseDay
    {
        private List<Coordinate2d> _sensors;
        private List<Coordinate2d> _beacons;

        public Day15(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day15(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            ParseInput();
            return CalcPositions(2000000);
        }

        public override object SolvePart2()
        {
            ParseInput();
            return TuningFrequency();
        }

        private void ParseInput()
        {
            _sensors = new();
            _beacons = new();

            foreach (var line in FileContent)
            {
                var coords = Regex.Matches(line, @"-?\d+")
                    .Select(c => int.Parse(c.Value))
                    .ToList();

                var sensor = new Coordinate2d(coords[0], coords[1]);
                var beacon = new Coordinate2d(coords[2], coords[3]);
                _sensors.Add(sensor);
                _beacons.Add(beacon);
            }
        }

        private int CalcPositions(int Y)
        {
            var cannot = new HashSet<long>();
            var known = new HashSet<long>();

            for (int i = 0; i < _sensors.Count; i++)
            {
                var sx = _sensors[i].X;
                var sy = _sensors[i].Y;
                var bx = _beacons[i].X;
                var by = _beacons[i].Y;

                var d = Math.Abs(sx - bx) + Math.Abs(sy - by);
                var o = d - (Math.Abs(sy - Y));

                if (o < 0)
                    continue;

                var lx = sx - o;
                var hx = sx + o;

                for (long x = lx; x <= hx; x++)
                    cannot.Add(x);

                if (by == Y)
                    known.Add(bx);
            }

            return cannot.Count - known.Count;
        }

        private long TuningFrequency(long M = 4000000)
        {
            (long x, long y) beaconAt = (0, 0);

            for (int Y = 0; Y <= M; Y++)
            {
                var intervals = new List<(int low, int high)>();
                for (int i = 0; i < _sensors.Count; i++)
                {
                    var sx = _sensors[i].X;
                    var sy = _sensors[i].Y;
                    var bx = _beacons[i].X;
                    var by = _beacons[i].Y;

                    var d = Math.Abs(sx - bx) + Math.Abs(sy - by);
                    var o = d - (Math.Abs(sy - Y));  // some math stuff, ty Google

                    if (o < 0)
                        continue;

                    var lx = sx - o;
                    var hx = sx + o;

                    intervals.Add((lx, hx));
                }

                intervals.Sort();

                var q = new List<List<int>>();  // list containing overlaps
                                                // using a list to track low and high bound
                                                // instead of tuples cos tuples are immutable

                // check for overlaps in intervals
                foreach (var inter in intervals)
                {
                    var lo = inter.low;
                    var hi = inter.high;

                    if (!q.Any())
                    {
                        q.Add(new List<int> { lo, hi });
                        continue;
                    }

                    var last = q.Last();
                    var qlo = last[0]; // low bound
                    var qhi = last[1]; // high bound

                    if (lo > qhi + 1)
                    {
                        q.Add(new List<int> { lo, hi });
                        continue;
                    }

                    var l = q.Last();
                    last[1] = Math.Max(qhi, hi);
                }

                // foreach (var item in q)
                // {
                //     Console.WriteLine($"[{item[0]}] [{item[1]}]");
                // }

                var x = 0;
                foreach (var interval in q)
                {
                    if (x < interval[0])
                    {
                        beaconAt.x = x;
                        beaconAt.y = Y;
                        break;
                    }
                    else
                    {
                        x = interval[1] + 1;
                    }

                    if (x > M)
                        break;
                }
            }

            return (beaconAt.x * 4000000) + beaconAt.y;
        }
    }
}
