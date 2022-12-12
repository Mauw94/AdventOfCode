using static AoC.Shared.Grid2d;

namespace AoC.Shared.pathfinding
{
    public class BFS : BaseAlgo
    {
        public BFS(Dictionary<Position, Cell> cells, (int x, int y) dimensions, Cell start, Cell end)
            : base(cells, dimensions, start, end)
        {
        }

        public override int Solve()
        {
            var q = new Queue<(Cell, int)>();
            var seen = new HashSet<Cell>();
            q.Enqueue((Start, 0));

            while (q.Any())
            {
                var (cell, cost) = q.Dequeue();
                if (cell.X == End.X && cell.Y == End.Y) return cost;
                if (!seen.Add(cell)) continue;

                foreach (var n in GetNeighbours(cell.X, cell.Y))
                {
                    if (n.Value - cell.Value <= 1)
                    {
                        q.Enqueue((n, cost + 1));
                    }
                }
            }

            return 0;
        }

        public int SolveReverse() // start at the end and go until the first 'a' is found
        {
            var q = new Queue<(Cell, int)>();
            var seen = new HashSet<Cell>();
            q.Enqueue((End, 0));

            while (q.Any())
            {
                var (cell, cost) = q.Dequeue();
                if (cell.Value == 'a') return cost;
                if (!seen.Add(cell)) continue;

                foreach (var n in GetNeighbours(cell.X, cell.Y))
                {
                    if (n.Value - cell.Value >= -1)
                    {
                        q.Enqueue((n, cost + 1));
                    }
                }
            }

            return 0;
        }
    }
}