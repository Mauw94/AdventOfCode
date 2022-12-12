using AoC.Shared;
using static AoC.Shared.Grid2d;

namespace AoC2022.Tests
{
    [TestClass]
    public class Grid2Tests
    {
        private List<string> _input = new();

        public Grid2Tests()
        {
            // input 
            // 30373
            // 25512
            // 65332
            // 33549
            // 35390
            _input = Common.GetInput(8, 2022, true);
        }

        [TestMethod]
        public void Test_Dimensions()
        {
            // arrange
            var grid = new Grid2d(_input);

            // act

            // assert
            Assert.AreEqual(5, grid.Dimensions.x);
            Assert.AreEqual(5, grid.Dimensions.y);
        }

        [TestMethod]
        public void Test_CountEdges()
        {
            // arrange
            var grid = new Grid2d(_input);

            // act
            var edges = grid.CountEdges();

            // assert
            Assert.AreEqual(16, edges);
        }

        [TestMethod]
        public void Test_GetNeighbours()
        {
            // arrange
            var grid = new Grid2d(_input);
            var cell = grid.Cells[new Position(1, 1)]; // is 5 on 2nd row and 2nd col in _input

            // act
            var neighbours = grid.GetNeighbours(cell.Position.x, cell.Position.y);
            // returns 0, 2, 5, 5 as neighbours in the grid

            // assert
            Assert.IsNotNull(cell);
            Assert.AreEqual(4, neighbours.Count);
            Assert.IsTrue(neighbours.Any(x => x.Value.ToInt() == 2));
            Assert.IsTrue(neighbours.Any(x => x.Value.ToInt() == 5));
            Assert.IsTrue(neighbours.Any(x => x.Value.ToInt() == 0));
        }

        [TestMethod]
        public void Test_GetCells()
        {
            // arrange
            var grid = new Grid2d(_input);

            // act
            var cells = grid.GetCells();

            // assert
            Assert.AreEqual(grid.Cells.Count, cells.Count());
        }

        [TestMethod]
        public void Test_MoveUntil()
        {
            // arrange
            var grid = new Grid2d(_input);
            var from = new Position(1, 1);
            var to = new Position(2, 1);
            var direction = Direction.Right; // todo: define this from the from and to coords

            // act
            var moves = grid.MoveUntilPosition(from, to, direction);

            // assert
            Assert.AreEqual(1, moves);
        }
    }
}