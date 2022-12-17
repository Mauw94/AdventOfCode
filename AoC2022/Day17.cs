using AoC.Shared;
using System;
namespace AoC2022
{
    public class Day17 : BaseDay
    {
        public Day17(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day17(int day, int year) : base(day, year) { }

        // ####     

        // .#.
        // ###
        // .#.

        // ..#
        // ..#
        // ###

        // #
        // #
        // #
        // #

        // ##
        // ##

        public override object SolvePart1()
        {

            return 1;
        }

        public override object SolvePart2()
        {
            return 1;
        }

        private int SimulateTetrisWithRocks()
        {
            var moves = FileContent[0].ToCharArray();
            var movesCounter = 0; 
            var rocksToFall = 2022;
            var width = 7;
            var points = new HashSet<(int x, int y)>();

            for (int i = 0; i < rocksToFall; i++)
            {   
                if (movesCounter == moves.Length) 
                {
                    moves = FileContent[0].ToCharArray();
                    movesCounter = 0;
                }

                var move = moves[movesCounter];

                switch (move)
                {
                    case '<':
                        MoveLeft(points);
                        break;
                    case '>':
                        break;
                }

                movesCounter++;
            }

            var maxHeight = points.Select(x => x.y).Max();
            return maxHeight;
        }

        private void MoveLeft(HashSet<(int x, int y)> grid)
        {

        }
    }
}
