namespace AoC.Shared
{
    public class Grid2d // TODO: unit test this class and implement in challenges.
    {
        public (int x, int y) Dimensions { get; init; }
        public Dictionary<Position, Cell> Cells { get; } = new();
        public int Count => Cells.Count;

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

        public Cell this[int x, int y] => Cells[new Position(x, y)];
        public Cell GetCell(Cell c) => Cells[c.Position];

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
                .Where(p => p.x >= 0 && p.x <= Dimensions.x
                        && p.y >= 0 && p.y <= Dimensions.y)
                .Select(x => x)
                .ToList();

            return Cells
                .Where(x => neighbours.Contains(x.Key))
                .Select(x => x.Value)
                .ToList();
        }

        public void Add(Cell cell) => Cells.Add(cell.Position, cell);

        public IEnumerable<Cell> GetCells()
        {
            foreach (var cell in Cells.Values)
                yield return cell;
        }

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
    }

    public record Position(int x, int y);
}