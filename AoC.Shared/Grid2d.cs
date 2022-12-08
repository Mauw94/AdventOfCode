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

        public List<Cell> GetNeighbours(Cell cell)
        {
            var currentGrid = Cells.Select(x => x.Value).ToList();
            var neighbours = currentGrid
                .Where(c => c.Position.x == cell.Position.x - 1
                    || c.Position.x == cell.Position.x + 1
                    || c.Position.y == cell.Position.y - 1
                    || c.Position.y == cell.Position.y + 1).ToList();

            return neighbours;
        }

        public void Add(Cell cell) => Cells.Add(cell.Position, cell);

        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (var cell in Cells.Values)
                yield return cell;
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