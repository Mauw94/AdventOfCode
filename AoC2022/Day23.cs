using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day23 : BaseDay
    {
        private List<(int x, int y)> _neighbours = new List<(int x, int y)>
        {
            new (0, -1),
            new (0, +1),
            new (-1, 0),
            new (+1, 0),
            new (-1, -1),
            new (-1, +1),
            new (+1, -1),
            new (+1, +1)
        };

        private HashSet<(int x, int y)> _grid = new();
        private HashSet<(int x, int y)> _elvePositions = new();
        public Day23(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day23(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            ParseGrid();

            return 1;
        }

        public override object SolvePart2()
        {
            return 1;
        }

        private void SimulateMovement(int rounds)
        {
            for (int i = 0; i < rounds; i++)
            {
                var proposedMoves = new List<(int x, int y, int elveX, int elveY)>();
                var adjacentPositions = new HashSet<(int x, int y)>();

                foreach (var curEleve in _elvePositions)
                {
                    if (!_grid.Contains(curEleve)) continue;

                    foreach (var (dx, dy) in _neighbours)
                    {
                        adjacentPositions.Add((curEleve.x + dx, curEleve.y + dy));
                    }

                    if (adjacentPositions.Any(x => _elvePositions.Contains(x)))
                    {
                        // keep previous movement in a dict?
                        proposedMoves.Add((curEleve.x + 1, curEleve.y, curEleve.x, curEleve.y));
                        proposedMoves.Add((curEleve.x - 1, curEleve.y, curEleve.x, curEleve.y));
                        proposedMoves.Add((curEleve.x, curEleve.y + 1, curEleve.x, curEleve.y));
                        proposedMoves.Add((curEleve.x, curEleve.y - 1, curEleve.x, curEleve.y));
                    }
                }
            }
        }

        private void ParseGrid()
        {
            for (int i = 0; i < FileContent.Count; i++)
            {
                for (int j = 0; j < FileContent[0].Length; j++)
                {
                    var empty = FileContent[i][j] == '#' ? false : true;
                    _grid.Add((i, j));

                    if (!empty)
                        _elvePositions.Add((i, j));
                }
            }
        }
    }
}


