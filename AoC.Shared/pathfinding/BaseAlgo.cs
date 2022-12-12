using static AoC.Shared.Grid2d;

namespace AoC.Shared.pathfinding
{
    public abstract class BaseAlgo
    {
        protected Dictionary<Position, Cell> Cells;
        protected (int x, int y) Dimensions;
        protected Cell Start;
        protected Cell End;

        public BaseAlgo(Dictionary<Position, Cell> cells, (int x, int y) dimensions, Cell start, Cell end)
        {
            Cells = cells;
            Dimensions = dimensions;
            Start = start;
            End = end;

        }
        
        public abstract int Solve();

        protected List<Cell> GetNeighbours(int row, int col)
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
    }
}