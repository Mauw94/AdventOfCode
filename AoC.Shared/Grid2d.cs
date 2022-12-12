using AoC.Shared.pathfinding;

namespace AoC.Shared
{
    public class Grid2d
    {
        public (int x, int y) Dimensions { get; init; }
        public Dictionary<Position, Cell> Cells { get; } = new();
        public int Count => Cells.Count;

        public Cell Start { get; set; }
        public Cell End { get; set; }

        public Grid2d(List<string> input)
        {
            Dimensions = (input.Count(), input[0].Length);

            for (var x = 0; x < Dimensions.x; x++)
            {
                for (var y = 0; y < Dimensions.y; y++)
                {
                    var gc = new Cell(new Position(x, y), input[x][y]);
                    Cells.Add(gc.Position, gc);
                }
            }
        }

        public Grid2d(List<string> input, char start, char end)
        {
            Dimensions = (input.Count(), input[0].Length);

            for (var x = 0; x < Dimensions.x; x++)
            {
                for (var y = 0; y < Dimensions.y; y++)
                {
                    if (input[x][y] == start) Start = new Cell(new Position(x, y), input[x][y]);
                    if (input[x][y] == end) End = new Cell(new Position(x, y), input[x][y]);

                    var gc = new Cell(new Position(x, y), input[x][y]);
                    Cells.Add(gc.Position, gc);
                }
            }
        }

        public Cell this[int x, int y] => Cells[new Position(x, y)];
        public Cell GetCell(Cell c) => Cells[c.Position];
        public void Add(Cell cell) => Cells.Add(cell.Position, cell);

        public static int ManhattenDistance(Cell start, Cell end)
            => Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);

        /// <summary>
        /// Get all the neighbouring cells of a cell.
        /// </summary>
        public List<Cell> GetNeighbours(int row, int col)
        {
            List<Position> coords = new()
            {
                new Position(row - 1, col),
                new Position(row + 1, col),
                new Position(row, col - 1),
                new Position(row, col + 1)
            };

            var neighbours = coords
                .Where(p => p.x >= 0 && p.x < Dimensions.x
                        && p.y >= 0 && p.y < Dimensions.y)
                .Select(x => x)
                .ToList();

            return Cells
                .Where(x => neighbours.Contains(x.Key))
                .Select(x => x.Value)
                .ToList();
        }

        public IEnumerable<Cell> GetCells()
        {
            foreach (var cell in Cells.Values)
                yield return cell;
        }

        /// <summary>
        /// Count all the edges of the grid.
        ///  </summary
        public int CountEdges()
        {
            var edges = 0;

            for (int r = 0; r < Dimensions.x; r++)
            {
                for (int c = 0; c < Dimensions.y; c++)
                {
                    var pos = new Position(r, c);
                    if (Cells[pos].X == 0
                       || Cells[pos].X == Dimensions.x - 1
                       || Cells[pos].Y == 0
                       || Cells[pos].Y == Dimensions.y - 1)
                        edges++;
                }
            }

            return edges;
        }

        /// <summary>
        /// Breadth-first search.
        /// </summary>        
        public int BFS()
        {
            var BFS = new BFS(Cells, Dimensions, Start, End);
            return BFS.Solve();
        }

        /// </summary>
        /// A* search
        /// </summary>
        public int SolveAStar()
        {
            var AStar = new AStar(Cells, Dimensions, Start, End);
            return AStar.Solve();
        }

        /// <summary>
        /// Move from from position to to position.
        /// This method can only move from left <-> right or up <-> down.
        /// </summary>
        public int MoveUntilPosition(Position from, Position to, Direction direction)
        {
            var steps = 0;

            switch (direction)
            {
                case Direction.Down:
                    for (int r = from.x + 1; r <= to.x; r++)
                    {
                        steps++;
                        if (Cells[from].X == Cells[to].X)
                            return steps;
                    }
                    break;
                case Direction.Up:
                    for (int r = from.x - 1; r >= to.x; r--)
                    {
                        steps++;
                        if (Cells[from].X == Cells[to].X)
                            return steps;
                    }
                    break;
                case Direction.Left:
                    for (int c = from.y - 1; c >= to.x; c--)
                    {
                        steps++;
                        if (Cells[from].X == Cells[to].X)
                            return steps;
                    }
                    break;
                case Direction.Right:
                    for (int c = from.x + 1; c <= to.x; c++)
                    {
                        steps++;
                        if (Cells[from].X == Cells[to].X)
                            return steps;
                    }
                    break;
            }

            return steps;
        }

        public class Cell
        {
            public Position Position { get; init; }
            public char Value { get; set; }

            public int Y => Position.y;
            public int X => Position.x;

            public Cell(Position position, char value)
            {
                Position = position;
                Value = value;
            }

            public int GetValue() => char.IsDigit(Value) ? Value.ToInt() : -1;
            public int AsciiValue() => char.IsUpper(Value) ? Value.CharToAscii() - 64 : Value.CharToAscii() - 96;
        }

        public record Position(int x, int y);

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}