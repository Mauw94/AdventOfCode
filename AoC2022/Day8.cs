using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day8 : BaseDay
    {
        public Day8(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day8(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            var grid = CreateTreeGrid();
            var visibleTrees = CountVisibleTrees(grid);

            return visibleTrees;
        }

        public override object SolvePart2()
        {
            var grid = CreateTreeGrid();
            var scenicScores = CountScenicScores(grid);

            return scenicScores.Max();
        }

        private List<int> CountScenicScores(int[,] grid)
        {
            var scenicScores = new List<int>();

            for (int r = 0; r < grid.GetLength(0); r++)
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    scenicScores.Add(CalculateScenicScore(grid, r, c));
                }

            return scenicScores;
        }


        private int CalculateScenicScore(int[,] grid, int row, int col)
        {
            var scenicScoresForTree = new List<int>();

            scenicScoresForTree.Add(LookUp(grid, row, col, 0));
            scenicScoresForTree.Add(LookDown(grid, row, col, grid.GetLength(0) - 1));
            scenicScoresForTree.Add(LookLeft(grid, row, col, 0));
            scenicScoresForTree.Add(LookRight(grid, row, col, grid.GetLength(1) - 1));

            return scenicScoresForTree.Product();
        }

        private int LookUp(int[,] grid, int row, int col, int edge)
        {
            var treesEncountered = 0;
            for (int r = row - 1; r >= edge; r--)
            {
                treesEncountered++;
                if (grid[row, col] <= grid[r, col])
                    return treesEncountered;
            }

            return treesEncountered;
        }

        private int LookDown(int[,] grid, int row, int col, int edge)
        {
            var treesEncountered = 0;
            for (int r = row + 1; r <= edge; r++)
            {
                treesEncountered++;
                if (grid[row, col] <= grid[r, col])
                    return treesEncountered;
            }

            return treesEncountered;
        }

        private int LookLeft(int[,] grid, int row, int col, int edge)
        {
            var treesEncountered = 0;
            for (int c = col - 1; c >= edge; c--)
            {
                treesEncountered++;
                if (grid[row, col] <= grid[row, c])
                    return treesEncountered;
            }

            return treesEncountered;
        }

        private int LookRight(int[,] grid, int row, int col, int edge)
        {
            var treesEncountered = 0;
            for (int c = col + 1; c <= edge; c++)
            {
                treesEncountered++;
                if (grid[row, col] <= grid[row, c])
                    return treesEncountered;
            }

            return treesEncountered;
        }

        private int CountVisibleTrees(int[,] grid)
        {
            var visibleTrees = 0;
            visibleTrees += CountEdges(grid);

            for (int r = 1; r < grid.GetLength(0) - 1; r++)
            {
                for (int c = 1; c < grid.GetLength(1) - 1; c++)
                {
                    if (IsVisibleTop(grid, r, c, 0)
                    || IsVisibleBottom(grid, r, c, grid.GetLength(0) - 1)
                    || IsVisibleLeft(grid, r, c, 0)
                    || IsVisibleRight(grid, r, c, grid.GetLength(1) - 1))
                        visibleTrees++;
                }
            }

            return visibleTrees;
        }

        private bool IsVisibleTop(int[,] grid, int row, int col, int edge)
        {
            for (int r = row - 1; r >= edge; r--)
            {
                if (!(grid[row, col] > grid[r, col]))
                    return false;
            }

            return true;
        }

        private bool IsVisibleBottom(int[,] grid, int row, int col, int edge)
        {
            for (int r = row + 1; r <= edge; r++)
            {
                if (!(grid[row, col] > grid[r, col]))
                    return false;
            }

            return true;
        }

        private bool IsVisibleLeft(int[,] grid, int row, int col, int edge)
        {
            for (int c = col - 1; c >= edge; c--)
            {
                if (!(grid[row, col] > grid[row, c]))
                    return false;
            }

            return true;
        }

        private bool IsVisibleRight(int[,] grid, int row, int col, int edge)
        {
            for (int c = col + 1; c <= edge; c++)
            {
                if (!(grid[row, col] > grid[row, c]))
                    return false;
            }

            return true;
        }

        private int CountEdges(int[,] grid)
        {
            var edge = 0;
            for (int r = 0; r < grid.GetLength(0); r++)
                for (int c = 0; c < grid.GetLength(1); c++)
                    if (r == 0
                    || c == 0
                    || r == grid.GetLength(0) - 1
                    || c == grid.GetLength(1) - 1)
                        edge++;
            return edge;
        }

        private int[,] CreateTreeGrid()
        {
            var R = FileContent.Count;
            var C = FileContent[0].Count();
            var grid = new int[R, C];

            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    grid[i, j] = FileContent[i][j].ToInt();
                }
            }

            // PrintGrid(grid);
            return grid;
        }

        private void PrintGrid(int[,] grid) // debugging 
        {
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    Console.Write(grid[r, c]);
                }
                Console.Write("\n");
            }
        }
    }
}
