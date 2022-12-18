using AoC.Shared;
using System;
namespace AoC2022
{
    public class Grid3D
    {
        public HashSet<(int x, int y, int z)> Cells { get; set; }
        // I tried hashing a Cell object here before which did not work -> obv
        // an object hash doesn't care about its mutable properties
        // the hash will always be the same, even if the properties change
        // so that approach failed
        // also tried List<Cell> objects -> which did work with some added ContainsCell method
        // in the Grid3D class, but was terribly slow compared to a HashSet of tuple(int, int, int)
        // the difference between the two was about 30 seconds

        public int MaxX => Cells.Select(c => c.x).Max();
        public int MinX => Cells.Select(c => c.x).Min();
        public int MaxY => Cells.Select(c => c.y).Max();
        public int MinY => Cells.Select(c => c.y).Min();
        public int MaxZ => Cells.Select(c => c.z).Max();
        public int MinZ => Cells.Select(c => c.z).Min();

        public Grid3D()
        {
            Cells = new();
        }

        private List<(int, int, int)> _neighbors = new List<(int x, int y, int z)> { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };

        public List<(int x, int y, int z)> Neigbours => _neighbors;
    }

    public class Day18 : BaseDay
    {
        public Day18(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day18(int day, int year) : base(day, year) { }

        private Grid3D _grid;
        private long _queueCalls = 0;

        public override object SolvePart1()
        {
            _grid = ParseGrid();

            var surface = 0;

            foreach (var cell in _grid.Cells)
            {
                foreach (var (dx, dy, dz) in _grid.Neigbours)
                {
                    if (!_grid.Cells.Contains((cell.x + dx, cell.y + dy, cell.z + dz)))
                        surface++;
                }
            }

            return surface;
        }

        public override object SolvePart2() // flood fill
        {
            _grid = ParseGrid();
            var surface = 0;
            var rangeX = Enumerable.Range(_grid.MinX, _grid.MaxX + 1).ToList();
            var rangeY = Enumerable.Range(_grid.MinY, _grid.MaxY + 1).ToList();
            var rangeZ = Enumerable.Range(_grid.MinZ, _grid.MaxZ + 1).ToList();

            foreach (var cell in _grid.Cells)
            {
                foreach (var (dx, dy, dz) in _grid.Neigbours)
                    if (NotInDroplet((cell.x + dx, cell.y + dy, cell.z + dz), rangeX, rangeY, rangeZ))
                        surface++;
            }

            Console.WriteLine("Queue calls: " + _queueCalls.ToString());
            return surface;
        }

        private bool NotInDroplet((int x, int y, int z) cell, List<int> rangeX, List<int> rangeY, List<int> rangeZ)
        {
            if (_grid.Cells.Contains((cell.x, cell.y, cell.z))) return false;

            var checkedCells = new HashSet<(int x, int y, int z)>();
            var queue = new Queue<(int x, int y, int z)>();
            queue.Enqueue((cell.x, cell.y, cell.z));

            while (queue.Any())
            {
                _queueCalls++;
                var c = queue.Dequeue();
                if (checkedCells.Contains(c)) continue;
                checkedCells.Add(c);

                if (!rangeX.Contains(c.x) || !rangeY.Contains(c.y) || !rangeZ.Contains(c.z))
                    return true; // check if it's outside the droplets ranges

                if (!_grid.Cells.Contains(c))
                {
                    foreach (var (dx, dy, dz) in _grid.Neigbours)
                    {
                        queue.Enqueue((c.x + dx, c.y + dy, c.z + dz));
                    }
                }
            }
            return false;
        }

        private Grid3D ParseGrid()
        {
            var grid = new Grid3D();
            foreach (var line in FileContent)
            {
                var coords = line.Split(',').Select(x => int.Parse(x)).ToArray();
                grid.Cells.Add((coords[0], coords[1], coords[2]));
            }

            return grid;
        }
    }
}
