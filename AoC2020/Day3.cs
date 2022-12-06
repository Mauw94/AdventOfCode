using AoC.Shared;

namespace AoC2020
{
    public class Day3 : BaseDay
    {
        public Day3(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day3(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            var map = CreateMap();
            return CountTrees(map, 3, 1);
        }

        public override object SolvePart2()
        {
            var map = CreateMap();
            return CountTrees(map, 1, 1) 
                * CountTrees(map, 3, 1) 
                * CountTrees(map, 5, 1) 
                * CountTrees(map, 7, 1) 
                * CountTrees(map, 1, 2);
        }

        private char[,] CreateMap()
        {
            char[,] map = new char[FileContent.Count, FileContent[0].Length];

            for (int i = 0; i < FileContent.Count; i++)
            {
                for (int j = 0; j < FileContent[i].Length; j++)
                {
                    var t = FileContent[i][j];
                    if (t == '.')
                        map[i, j] = '.';
                    else
                        map[i, j] = '#';
                }
            }

            // PrintMap(map);
            return map;
        }

        private long CountTrees(char[,] map, int right, int down)
        {
            var trees = 0;
            var cur_row = 0;
            var cur_col = 0;
            var rows = map.GetLength(0);
            var cols = map.GetLength(1);

            while (cur_row < rows)
            {
                if (map[cur_row, cur_col] == '#')
                    trees++;

                cur_row += down;
                if ((cur_col + right) >= cols)
                    cur_col = ((cur_col + right) % cols);
                else
                    cur_col += right;
            }

            return trees;
        }

        private void PrintMap(char[,] map) // debugging  purposes
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                    Console.Write(map[i, j]);
                Console.Write("\n");
            }
        }
    }
}