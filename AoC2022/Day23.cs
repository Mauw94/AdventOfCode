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
        private List<Elve> _elvePositions = new();

        public Day23(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day23(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            ParseGrid();
            SimulateMovement(10);

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
                var proposedMoves = new Dictionary<int, List<(int x, int y)>>();
                var adjacentPositions = new HashSet<(int x, int y)>();

                // first half
                foreach (var curElve in _elvePositions)
                {    
                    if (!_grid.Contains((curElve.X, curElve.Y))) continue;

                    foreach (var (dx, dy) in _neighbours)
                    {
                        adjacentPositions.Add((curElve.X + dx, curElve.Y + dy));
                    }

                    if (_elvePositions.Where(e =>  adjacentPositions.Any(x => x.x == e.X) && adjacentPositions.Any(x => x.y == e.Y)).Count() > 0)
                    {                                     
                        var moves = new List<(int, int)>();                        
                        moves.Add((curElve.X + 1, curElve.Y));
                        moves.Add((curElve.X - 1, curElve.Y));
                        moves.Add((curElve.X, curElve.Y + 1));
                        moves.Add((curElve.X, curElve.Y - 1));

                        proposedMoves.Add(curElve.Id, moves);
                    }
                }

                // second half
                var elvesToMove = new HashSet<Elve>();
                var movesToExecute = proposedMoves.GroupBy(x => x.Value).Where(x => x.Count() == 1).ToList();

                foreach (var elf in _elvePositions)
                {
                    var elfMoves = proposedMoves[elf.Id];
                    foreach (var m in elfMoves) 
                    {
                        foreach (var pm in proposedMoves)
                        {
                            if (pm.Value.Where(x => x.x == m.x && x.y == m.y).Count() <= 1)
                            {
                                elvesToMove.Add(elf);
                            }
                        }                        
                    }
                }

                foreach (var elf in elvesToMove)
                {

                }
            }
        }

        private void ParseGrid()
        {
            var elveId = 1;

            for (int i = 0; i < FileContent.Count; i++)
            {
                for (int j = 0; j < FileContent[0].Length; j++)
                {
                    var empty = FileContent[i][j] == '#' ? false : true;
                    _grid.Add((i, j));

                    if (!empty) 
                    {
                        _elvePositions.Add(new Elve(i , j, elveId));
                        elveId++;
                    }
                }
            }
        }

        class Elve
        {
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get;set; }
            public Direction CurrDirection { get; set; }
            public (int x, int y) Position { get; set; }

            public Direction GetNextDirection()
            {
                return CurrDirection switch
                {
                    Direction.NORTH => Direction.SOUTH,
                    Direction.SOUTH => Direction.WEST,
                    Direction.WEST => Direction.EAST,
                    Direction.EAST => Direction.NORTH,
                    _ => throw new ArgumentException()
                };
            }

            public Elve(int x, int y, int elfId)
            {
                X = x;
                Y = y;
                Id = elfId;
                CurrDirection = Direction.NORTH;
            }
        }

        enum Direction
        {
            NORTH,
            SOUTH,
            WEST,
            EAST
        }
    }
}


