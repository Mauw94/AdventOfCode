using static AoC.Shared.Grid2d;

namespace AoC.Shared.pathfinding
{
    // https://en.wikipedia.org/wiki/A*_search_algorithm#Pseudocode
    public class AStar : BaseAlgo
    {
        public AStar(Dictionary<Position, Cell> cells, (int x, int y) dimensions, Cell start, Cell end)
            : base(cells, dimensions, start, end)
        {
        }

        public override int Solve()
        {
            var openSet = new PriorityQueue<Cell, int>();
            openSet.Enqueue(Start, 0);

            var cameFrom = new Dictionary<Cell, Cell>();
            var gScore = new Dictionary<Cell, int>()
            {
                {Start, 0}
            };
            var fScore = new Dictionary<Cell, int>()
            {
                {Start, Heuristic(Start, End)}
            };

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();
                if (current.X == End.X && current.Y == End.Y)
                    return ReconstructPath(cameFrom, current);

                foreach (var n in GetNeighbours(current.X, current.Y))
                {
                    var weight = EdgeWeight(current, n);
                    if (weight == null) continue;

                    var tScore = gScore[current] + weight.Value;
                    if (!gScore.ContainsKey(n) || tScore < gScore[n])
                    {
                        cameFrom[n] = current;
                        gScore[n] = tScore;
                        fScore[n] = tScore + Heuristic(current, End);
                        openSet.Enqueue(n, fScore[n]);
                    }
                }
            }

            return 0;
        }

        private int ReconstructPath(Dictionary<Cell, Cell> cameFrom, Cell current)
        {
            var path = new List<Cell>() { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            path.Reverse();
            return path.Count - 1;
        }

        private int? EdgeWeight(Cell curr, Cell neighbour)
        {
            var nEff = EffectiveValue(neighbour.Value);
            var cEff = EffectiveValue(curr.Value);

            return nEff > cEff + 1
                ? null
                : (int?)cEff - nEff + 1;
        }

        private int EffectiveValue(char c) => c switch
        {
            'S' => 'a',
            'E' => 'z',
            _ => c
        };

        private int Heuristic(Cell curr, Cell end) =>
            Math.Abs(curr.X - end.X) + Math.Abs(curr.Y - end.Y);
    }
}