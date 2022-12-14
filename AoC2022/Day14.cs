using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day14 : BaseDay
    {
        private HashSet<(int x, int y)> _set;

        public Day14(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day14(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            _set = new();
            CreateRockFormation();
            // PrintGrid();
            var res = SimulateSandFalling();

            return res;
        }

        public override object SolvePart2()
        {
            _set = new();
            CreateRockFormation();
            // PrintGrid();
            var res = SimulateSandFallingP2();

            return res;
        }

        private void CreateRockFormation()
        {
            var singlePoints = new List<(int x, int y)>();

            foreach (var line in FileContent)
            {
                var coords = line.Split(" -> ");
                singlePoints = new();
                foreach (var coord in coords)
                {
                    var x = int.Parse(coord.Split(',')[0]);
                    var y = int.Parse(coord.Split(',')[1]);
                    singlePoints.Add((x, y));
                }

                for (int i = 0; i < singlePoints.Count; i++)
                {
                    if (i + 1 >= singlePoints.Count) break;

                    var a = singlePoints[i];
                    var b = singlePoints[i + 1];

                    var dx = b.x - a.x;
                    var dy = b.y - a.y;

                    _set.Add(a);

                    while (a != b)
                    {
                        a = dx == 0 ? a with { y = a.y + Math.Sign(dy) } : a with { x = a.x + Math.Sign(dx) };
                        _set.Add(a);
                    }
                }
            }
        }

        private void PrintGrid()
        {
            for (int r = 0; r < 10; r++)
            {
                for (int c = 494; c <= 503; c++)
                {
                    if (_set.Contains((c, r)))
                        Console.Write('#');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
        }

        private int SimulateSandFalling()
        {
            var total = 0;
            var maxY = _set.Select(s => s.y).Max() + 1;
            var complete = false;

            while (!complete)
            {
                var s = (x: 500, y: 0);
                while (true)
                {
                    if (s.y >= maxY)
                    {
                        complete = true;
                        break;
                    }
                    if (!_set.Contains((s.x, s.y + 1)))
                    {
                        s.y++;
                        continue;
                    }
                    if (!_set.Contains((s.x - 1, s.y + 1)))
                    {
                        s.x--;
                        s.y++;
                        continue;
                    }
                    if (!_set.Contains((s.x + 1, s.y + 1)))
                    {
                        s.x++;
                        s.y++;
                        continue;
                    }
                    total++;
                    _set.Add(s);
                    break;
                }
            }

            return total;
        }

        private int SimulateSandFallingP2()
        {
            var total = 0;
            var maxY = _set.Select(s => s.y).Max() + 1;

            while (!_set.Contains((500, 0)))
            {
                var s = (x: 500, y: 0);
                while (true)
                {
                    if (s.y >= maxY)
                    {
                        break;
                    }
                    if (!_set.Contains((s.x, s.y + 1)))
                    {
                        s.y++;
                        continue;
                    }
                    if (!_set.Contains((s.x - 1, s.y + 1)))
                    {
                        s.x--;
                        s.y++;
                        continue;
                    }
                    if (!_set.Contains((s.x + 1, s.y + 1)))
                    {
                        s.x++;
                        s.y++;
                        continue;
                    }
                    break;
                }
                total++;
                _set.Add(s);
            }

            return total;
        }
    }
}
