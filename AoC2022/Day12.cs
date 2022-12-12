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
            var grid = new Grid2d(FileContent);
            var start = grid.Cells.Where(c => c.Value.Value == 'S').First();
            var end = grid.Cells.Where(c => c.Value.Value == 'E').First();
            grid.Start = start.Value;
            grid.End = end.Value;
            grid.Start.Value = 'a';
            grid.End.Value = 'z';

            return grid.SolveAStar(true);
            //return grid.BFS(true);
        }

        public override object SolvePart2()
        {
            return SolveP2AStart(false);
            //return SolveP2BFS(false);
        }

        private int SolveP2AStart(bool p1)
        {
            var grid = new Grid2d(FileContent);
            grid.End = grid.Cells.Where(c => c.Value.Value == 'E').First().Value;
            grid.End.Value = 'z';
            grid.Start = grid.Cells.Where(c => c.Value.Value == 'a').First().Value;
            grid.Start.Value = 'a';

            var path = grid.SolveAStar(p1);

            return path;
        }

        private int SolveP2BFS(bool p1)
        {
            var paths = new List<int>();
            var grid = new Grid2d(FileContent);
            var start = grid.Cells.Where(c => c.Value.Value == 'S').First();
            var end = grid.Cells.Where(c => c.Value.Value == 'E').First();
            grid.Start = start.Value;
            grid.End = end.Value;
            grid.Start.Value = 'a';
            grid.End.Value = 'z';

            var path = grid.BFS(p1);

            return path;
        }
    }
}
