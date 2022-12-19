using System.Linq.Expressions;

namespace AoC.Shared
{
    public class Grid3D
    {
        public HashSet<(int x, int y, int z)> Cells { get; set; }
        public List<(int x, int y, int z)> Neigbours => _neighbors;
        public IEnumerable<int> RangeX => Enumerable.Range(_minX, _maxX);
        public IEnumerable<int> RangeY => Enumerable.Range(_minY, _maxY);
        public IEnumerable<int> RangeZ => Enumerable.Range(_minZ, _maxZ);

        // possible neigbours in a 3d grid
        private List<(int, int, int)> _neighbors = new List<(int x, int y, int z)>
            { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };

        private int _minX, _maxX, _minY, _maxY, _minZ, _maxZ;

        public Grid3D()
        {
            Cells = new();
        }

        public void SetMinMaxAxis()
        {
            _minX = Cells.Select(c => c.x).Min();
            _maxX = Cells.Select(c => c.x).Max() + 1;

            _minY = Cells.Select(c => c.y).Min();
            _maxY = Cells.Select(c => c.y).Max() + 1;

            _minZ = Cells.Select(c => c.z).Min();
            _maxZ = Cells.Select(c => c.z).Max() + 1;
        }
    }
}