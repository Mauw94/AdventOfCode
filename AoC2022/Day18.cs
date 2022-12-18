using AoC.Shared;
using System;
namespace AoC2022
{
    public class Grid3D
    {
        public List<Cell> Cells { get; set; } = new();

        public int MaxX => Cells.Select(c => c.X).Max();
        public int MinX => Cells.Select(c => c.X).Min();
        public int MaxY => Cells.Select(c => c.Y).Max();
        public int MinY => Cells.Select(c => c.Y).Min();
        public int MaxZ => Cells.Select(c => c.Z).Max();
        public int MinZ => Cells.Select(c => c.Z).Min();

        public bool ContainsCell(int x, int y, int z)
        {
            var cell = Cells.Where(c => c.X == x && c.Y == y && c.Z == z).FirstOrDefault();
            return cell != null ? true : false;
        }

        public List<(int x, int y, int z)> Neigbours = new()
        {
            new (1, 0, 0),
            new (- 1, 0, 0),
            new (0, 1, 0),
            new (0, - 1, 0),
            new (0, 0, 1),
            new (0, 0, - 1)
        };
    }

    public class Cell
    {
        public Cell(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
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
                    if (!_grid.ContainsCell(cell.X + dx, cell.Y + dy, cell.Z + dz))
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
                    if (NotInDroplet((cell.X + dx, cell.Y + dy, cell.Z + dz), rangeX, rangeY, rangeZ))
                        surface++;
            }

            Console.WriteLine("Queue calls: " + _queueCalls.ToString());
            return surface;
        }

        private bool NotInDroplet((int x, int y, int z) cell, List<int> rangeX, List<int> rangeY, List<int> rangeZ)
        {
            if (_grid.ContainsCell(cell.x, cell.y, cell.z)) return false;

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

                if (!_grid.ContainsCell(c.x, c.y, c.z))
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
            var cells = new List<Cell>();
            foreach (var line in FileContent)
            {
                var coords = line.Split(',').Select(x => int.Parse(x)).ToArray();
                var cell = new Cell(coords[0], coords[1], coords[2]);
                grid.Cells.Add(cell);
            }

            return grid;
        }
    }
}
