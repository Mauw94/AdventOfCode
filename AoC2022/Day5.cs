using AoC.Shared;
using System;
using System.Text.RegularExpressions;

namespace AoC2022
{
    class Move
    {
        public Move(int amnt, int from, int to)
        {
            Amount = amnt;
            From = from - 1;
            To = to - 1;
        }

        public int Amount { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }

    public class Day5 : BaseDay
    {
        public Day5(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day5(int day, int year) : base(day, year) { }

        public override object SolvePart1()
        {
            var (moves, stacks) = SetupMovesAndStacks();
            return CrateMover9000(stacks, moves);
        }

        public override object SolvePart2()
        {
            var (moves, stacks) = SetupMovesAndStacks();
            return CrateMover9001(stacks, moves);
        }

        private (List<Move>, List<Stack<char>>) SetupMovesAndStacks()
        {
            var cratesArrangement = FileContent.TakeWhile(x => x != "").ToList();
            var commands = FileContent.Skip(cratesArrangement.Count + 1).ToList();

            var moves = GetMoves(commands);
            var stacks = GetStacks(cratesArrangement);

            return (moves, stacks);
        }

        private List<Stack<char>> GetStacks(List<string> rows)
        {
            var stacks = new List<Stack<char>>();

            for (int i = 0; i < rows.Count; i++)
                stacks.Add(GetStack(rows.SkipLast(1), i * 4));

            return stacks;
        }

        private Stack<char> GetStack(IEnumerable<string> rows, int index)
        {
            var stack = new Stack<char>();
            foreach (var row in rows.Reverse())
            {
                if (index > row.Length) break;
                var crate = row[index + 1];
                if (crate != ' ')
                    stack.Push(crate);
            }

            return stack;
        }

        private List<Move> GetMoves(List<string> commands)
        {
            var moves = new List<Move>();
            foreach (var command in commands)
            {
                var numbers = command.GetNumbersFromString();

                // amount, from, to
                moves.Add(new Move(numbers[0], numbers[1], numbers[2]));
            }

            return moves;
        }

        private string CrateMover9000(List<Stack<char>> stacks, List<Move> moves)
        {
            var result = string.Empty;

            foreach (var move in moves)
            {
                for (int i = 0; i < move.Amount; i++)
                {
                    var s = stacks[move.From].Pop();
                    stacks[move.To].Push(s);
                }
            }

            foreach (var s in stacks)
                if (s.Any())
                    result += s.Peek();

            return result;
        }

        private string CrateMover9001(List<Stack<char>> stacks, List<Move> moves)
        {
            var result = string.Empty;

            foreach (var move in moves)
            {
                var chars = new List<char>();

                for (int i = 0; i < move.Amount; i++)
                    chars.Add(stacks[move.From].Pop());

                foreach (var c in chars.AsEnumerable().Reverse())
                    stacks[move.To].Push(c);
            }

            foreach (var s in stacks)
                if (s.Any())
                    result += s.Peek();

            return result;
        }
    }
}
