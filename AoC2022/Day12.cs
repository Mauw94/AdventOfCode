using AoC.Shared;
using System;
using static AoC.Shared.Grid2d;

namespace AoC2022
{
    public class Day12 : BaseDay
    {
        public Day12(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day12(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            // https://en.wikipedia.org/wiki/A*_search_algorithm#Pseudocode

            var grid = new Grid2d(FileContent);
            var start = grid.Cells.Where(c => c.Value.Value == 'S').First();
            var end = grid.Cells.Where(c => c.Value.Value == 'E').First();
            grid.Start = start.Value;
            grid.End = end.Value;
            grid.Start.Value = 'a';
            grid.End.Value = 'z';
            // var path = grid.AStar();
            // return path.Count - 1;
            return grid.BFS();
        }

        public override object SolvePart2()
        {
            // SolveP2AStart();
            return SolveP2BFS();
        }

        private int SolveP2AStart()
        {
            var paths = new List<List<Cell>>();
            var grid = new Grid2d(FileContent);
            var possibleStarts = grid.Cells
                .Where(c => c.Value.Value == 'a' || c.Value.Value == 'S')
                .Where(c => grid.GetNeighbours(c.Value.X, c.Value.Y).Any(n => n.Value == 'b'))
                .ToList();

            grid.End = grid.Cells.Where(c => c.Value.Value == 'E').First().Value;
            grid.End.Value = 'z';

            foreach (var start in possibleStarts)
            {
                grid.Start = start.Value;
                grid.Start.Value = 'a';
                var path = grid.AStar();

                if (path.Count > 0)
                    paths.Add(path);
            }

            var shortestPath = paths.MinBy(p => p.Count);
            return shortestPath.Count - 1;
        }

        private int SolveP2BFS()
        {
            var paths = new List<int>();
            var grid = new Grid2d(FileContent);
            var possibleStarts = grid.Cells
                .Where(c => c.Value.Value == 'a' || c.Value.Value == 'S')
                .Where(c => grid.GetNeighbours(c.Value.X, c.Value.Y).Any(n => n.Value == 'b'))
                .ToList();

            grid.End = grid.Cells.Where(c => c.Value.Value == 'E').First().Value;
            grid.End.Value = 'z';

            foreach (var start in possibleStarts)
            {
                grid.Start = start.Value;
                grid.Start.Value = 'a';
                var path = grid.BFS();

                paths.Add(path);
            }

            return paths.Min();
        }
    }
}
