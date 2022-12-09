namespace AoC.Shared
{
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

        public int ManhattenDistance(Coordinate2d point)
        {
            int x = Math.Abs(this.X - point.X);
            int y = Math.Abs(this.Y - point.Y);

            return x + y;
        }

        public void Move(string direction)
        {
            if (direction == "U") Y += 1;
            else if (direction == "D") Y -= 1;
            else if (direction == "R") X += 1;
            else if (direction == "L") X -= 1;
        }

        public void Follow(Coordinate2d target)
        {
            if (Math.Abs(target.X - X) <= 1
                && Math.Abs(target.Y - Y) <= 1) return;

            var stepX = target.X - X;
            var stepY = target.Y - Y;

            X += Math.Sign(stepX);
            Y += Math.Sign(stepY);
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}