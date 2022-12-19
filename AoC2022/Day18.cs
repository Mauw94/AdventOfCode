using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day18 : BaseDay
    {
        public Day18(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day18(int day, int year) : base(day, year) { }

        private Grid3D _grid;
        private long _queueCalls = 0;

        public override object SolvePart1()
        {
            _grid = ParseGrid();
            _grid.SetMinMaxAxis();

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
            _grid.SetMinMaxAxis();
            var surface = 0;

            foreach (var cell in _grid.Cells)
            {
                foreach (var (dx, dy, dz) in _grid.Neigbours)
                    if (NotInDroplet((cell.x + dx, cell.y + dy, cell.z + dz)))
                        surface++;
            }

            Console.WriteLine("Queue calls: " + _queueCalls.ToString());
            return surface;
        }

        private bool NotInDroplet((int x, int y, int z) cell)
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

                if (!_grid.RangeX.Contains(c.x) || !_grid.RangeY.Contains(c.y) || !_grid.RangeZ.Contains(c.z))
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
