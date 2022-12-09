namespace AoC.Shared
{
    public enum CompassDirection
    {
        N,
        E,
        S,
        W,
        NE,
        NW,
        SE,
        SW
    }

    public class Coordinate2d
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate2d(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate2d((int x, int y) coord)
        {
            X = coord.x;
            Y = coord.y;
        }

        public (int, int) CurrentPos() => (X, Y);

        /// <summary>
        /// Calculate manhatten distance between 2 points.
        /// </summary>
        public int ManhattenDistance(Coordinate2d point)
        {
            int x = Math.Abs(this.X - point.X);
            int y = Math.Abs(this.Y - point.Y);

            return x + y;
        }

        /// <summary>
        /// Move a point n steps left, right, up or down.
        /// </summary>
        public void Move(CompassDirection dir, int steps = 1)
        {
            if (dir == CompassDirection.N) Y += steps;
            else if (dir == CompassDirection.S) Y -= steps;
            else if (dir == CompassDirection.E) X += steps;
            else if (dir == CompassDirection.W) X -= steps;
            // TODO: implement diagonal
        }

        /// <summary>
        /// Follow a point towards x, y for n(1) step.
        /// </summary>
        public void Follow(Coordinate2d target)
        {
            if (Math.Abs(target.X - X) <= 1
                && Math.Abs(target.Y - Y) <= 1) return; // touching target => don't move

            var stepX = target.X - X;
            var stepY = target.Y - Y;

            X += Math.Sign(stepX);
            Y += Math.Sign(stepY);
        }

        public override string ToString() => $"({X}, {Y})";
    }
}