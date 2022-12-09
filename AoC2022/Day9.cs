using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day9 : BaseDay
    {
        private HashSet<(int, int)> _visited;
        private Coordinate2d _positionHead;
        private Coordinate2d _positionTail;
        private Coordinate2d[] _segments;

        public Day9(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day9(int day, int year) : base(day, year) { }

        public Day9(int day, int year, string filename) : base(day, year, filename) { }

        public override object SolvePart1()
        {
            _segments = Enumerable.Range(0, 2).Select(_ => new Coordinate2d(0, 0)).ToArray();
            _positionHead = _segments[0];
            _positionTail = _segments[1];
            _visited = new() { (_positionTail.X, _positionTail.Y) };

            foreach (var line in FileContent)
            {
                Move(line.Split(' ')[0], int.Parse(line.Split(' ')[^1])); // ^1 is the same as line.lenghth -1
            }

            return _visited.Count;
        }

        public override object SolvePart2()
        {
            _segments = Enumerable.Range(0, 10).Select(_ => new Coordinate2d(0, 0)).ToArray();
            _positionHead = _segments[0];
            _positionTail = _segments[9];
            _visited = new() { (_positionTail.X, _positionTail.Y) };

            foreach (var line in FileContent)
            {
                Move(line.Split(' ')[0], int.Parse(line.Split(' ')[^1]));
            }

            return _visited.Count;
        }

        private void Move(string direction, int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _positionHead.Move(GetCompassDirection(direction));

                for (var j = 1; j < _segments.Length; j++)
                    _segments[j].Follow(_segments[j - 1]);

                _visited.Add((_positionTail.X, _positionTail.Y));
            }
        }

        private CompassDirection GetCompassDirection(string direction)
        {
            return direction switch
            {
                "U" => CompassDirection.N,
                "D" => CompassDirection.S,
                "R" => CompassDirection.E,
                "L" => CompassDirection.W,
                _ => throw new ArgumentException("Not a valid direction ", nameof(direction))
            };
        }
    }
}
