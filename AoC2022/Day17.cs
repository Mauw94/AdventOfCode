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
        private int[,,] _pieces = new int[,,]
            {{
                { 1, 0, 0, 0 },
                { 1, 0 ,0, 0 },
                { 1, 0 ,0, 0 },
                { 1, 0, 0, 0 }
            },
            {
                { 0, 1, 0, 0 },
                { 1, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 0, 0 }
            },
            {
                { 0, 0, 1, 0 },
                { 0, 0, 1, 0 },
                { 1, 1, 1, 0 },
                { 0, 0, 0, 0 }
            },
            {
                { 1, 1, 1, 1 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },
                        {
                { 1, 1, 0, 0 },
                { 1, 1, 0, 0 },
                { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }
            },};

        private int[] _plow = new int[] { 3, 1, 1, 0, 2 };
        private long _maxY = 0;
        private int _pid = 0;
        private long _px = 0;
        private long _py = 0;
        private Dictionary<(long x, long y), int> _grid = new();
        private Dictionary<(Tuple<long[]>, long, int), (long index, long maxY)> _seen = new();

        public override object SolvePart1()
        {

            return SimulateTetrisWithRocks(2022);
        }

        public override object SolvePart2()
        {
            return SimulateTetrisWithRocks(1000000000000);
        }

        private Tuple<long[]> GenerateTopView()
        {
            var maxys = new long[] { -17, -17, -17, -17, -17, -17, -17 };

            foreach (var k in _grid.Keys)
            {
                var value = _grid[k];
                if (value == 1)
                    maxys[k.x] = Math.Max(maxys[k.x], k.y);
            }
            var v = maxys.Max();
            var r = new long[7];

            for (int i = 0; i < maxys.Length; i++)
            {
                r[i] = maxys[i] - v;
            }

            return new Tuple<long[]>(r);
        }

        private long SimulateTetrisWithRocks(long rocksToFall)
        {
            var moves = FileContent[0]
                .ToCharArray()
                .Select(c => c == '<' ? -1 : 1)
                .ToList();

            var i = 0L;
            var oldIndex = 0L;
            var oldMaxY = 0L;
            var repeat = 0L;
            var additional = 0L;
            var topv = new Tuple<long[]>(new long[7]);

            _maxY = -1;
            _pid = 0;
            _px = 2;
            _py = _maxY + 3 + 4 - _plow[_pid];

            while (i < rocksToFall)
            {
                for (int m = 0; m < moves.Count; m++) // moves are only horizontal -> x
                {
                    var move = moves[m];
                    CanMove(move, 0);
                    if (!MoveDown()) // piece is locked
                    {
                        foreach (var k in _grid.Keys)
                        {
                            var value = _grid[k];
                            if (value == 1)
                                _maxY = Math.Max(_maxY, k.y);
                        }
                        _pid = (_pid + 1) % 5;
                        _px = 2;
                        _py = _maxY + 3 + 4 - _plow[_pid];
                        i++;
                        if (i >= rocksToFall) break;

                        // var topv = GenerateTopView();

                        if (_seen.ContainsKey((topv, _pid, m)))
                        {
                            var seen = _seen[(topv, _pid, m)];
                            oldIndex = seen.index;
                            oldMaxY = seen.maxY;
                            repeat = (rocksToFall - i) / (i - oldIndex);
                            i += (i - oldIndex) * repeat;
                            additional += repeat * (_maxY - oldMaxY);
                            // seen = new();
                        }
                        _seen[(topv, _pid, m)] = (i, _maxY);
                    }
                }
            }

            return _maxY + 1 + additional;
        }

        private bool CanMove(int dx, int dy)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (_pieces[_pid, i, j] == 0) continue;
                    var npx = _px + dx;
                    var npy = _py + dy;
                    var rpx = npx + i;
                    var rpy = npy - j;
                    if (rpx > 6) return false;
                    if (rpx < 0) return false;
                    if (rpy < 0) return false;
                    if (_grid.ContainsKey((rpx, rpy)) &&
                        _grid[(rpx, rpy)] != 0) return false;
                }
            }
            _px += dx;
            _py += dy;

            return true;
        }

        private bool MoveDown()
        {
            if (!CanMove(0, -1))
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_pieces[_pid, i, j] == 0) continue;
                        var rpx = _px + i;
                        var rpy = _py - j;
                        _grid[(rpx, rpy)] = 1;
                    }
                }
                return false;
            }
            return true;
        }
    }
}
