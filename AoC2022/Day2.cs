using System;
namespace AoC2022
{
    public enum WinDrawLose
    {
        WIN = 0,
        DRAW = 1,
        LOSE = 2
    }

    public enum RockPaperScissors
    {
        ROCK,
        PAPER,
        SCISSORS
    }

    public class Day2 : BaseDay
    {
        public Day2(int day, bool isTest)
            : base(day, isTest) { }

        public Day2(int day)
            : base(day) { }

        public override object SolvePart1()
        {
            return StrategyGuide(true);
            // return SolveForPart1();
        }

        public override object SolvePart2()
        {
            return StrategyGuide(false);
            // return SolveForPart2();
        }

        private int StrategyGuide(bool p1)
        {
            var totalPoints = 0;

            foreach (var line in FileContent)
            {
                if (p1)
                    totalPoints += CalculatePoints(line);
                else
                    totalPoints += CalculatePointsPart2(line);
            }

            return totalPoints;
        }

        private static int CalculatePointsPart2(string line)
        {
            line = line.Replace(" ", "");
            var input = line.ToCharArray();
            var player = input[0];
            var you = input[1];
            var wdl = WinDrawLose.DRAW;
            var points = 0;

            if (you == 'X')
            {
                wdl = WinDrawLose.LOSE;
                points += 0;
            }
            if (you == 'Y')
            {
                wdl = WinDrawLose.DRAW;
                points += 3;
            }
            if (you == 'Z')
            {
                wdl = WinDrawLose.WIN;
                points += 6;
            }

            var whatToPlay = DetermineWhatToPlay(player, wdl);
            points += GetPointsRPS(whatToPlay);

            return points;
        }

        private static RockPaperScissors DetermineWhatToPlay(char computer, WinDrawLose wdl)
        {
            #region Computer plays rock
            if (RPS(computer) == RockPaperScissors.ROCK && wdl == WinDrawLose.WIN)
                return RockPaperScissors.PAPER;
            if (RPS(computer) == RockPaperScissors.ROCK && wdl == WinDrawLose.DRAW)
                return RockPaperScissors.ROCK;
            if (RPS(computer) == RockPaperScissors.ROCK && wdl == WinDrawLose.LOSE)
                return RockPaperScissors.SCISSORS;
            #endregion

            #region Computer plays paper
            if (RPS(computer) == RockPaperScissors.PAPER && wdl == WinDrawLose.WIN)
                return RockPaperScissors.SCISSORS;
            if (RPS(computer) == RockPaperScissors.PAPER && wdl == WinDrawLose.DRAW)
                return RockPaperScissors.PAPER;
            if (RPS(computer) == RockPaperScissors.PAPER && wdl == WinDrawLose.LOSE)
                return RockPaperScissors.ROCK;
            #endregion

            #region Computer plays scissors
            if (RPS(computer) == RockPaperScissors.SCISSORS && wdl == WinDrawLose.WIN)
                return RockPaperScissors.ROCK;
            if (RPS(computer) == RockPaperScissors.SCISSORS && wdl == WinDrawLose.DRAW)
                return RockPaperScissors.SCISSORS;
            if (RPS(computer) == RockPaperScissors.SCISSORS && wdl == WinDrawLose.LOSE)
                return RockPaperScissors.PAPER;
            #endregion

            throw new ArgumentException($"{computer} or {wdl} are not valid inputs.");
        }

        private static int CalculatePoints(string line)
        {
            line = line.Replace(" ", "");
            var input = line.ToCharArray();
            var player = input[0];
            var you = input[1];
            var points = 0;

            var result = IsWinner(player, you);

            points += GetPointsRPS(RPS(you));

            if (result == WinDrawLose.WIN)
                points += 6;
            if (result == WinDrawLose.LOSE)
                points += 0;
            if (result == WinDrawLose.DRAW)
                points += 3;

            return points;
        }

        private static WinDrawLose IsWinner(char computer, char you)
        {
            #region Computer players rock
            if (RPS(computer) == RockPaperScissors.ROCK && RPS(you) == RockPaperScissors.PAPER)
                return WinDrawLose.WIN;

            if (RPS(computer) == RockPaperScissors.ROCK && RPS(you) == RockPaperScissors.ROCK)
                return WinDrawLose.DRAW;

            if (RPS(computer) == RockPaperScissors.ROCK && RPS(you) == RockPaperScissors.SCISSORS)
                return WinDrawLose.LOSE;
            #endregion

            #region Computer players paper
            if (RPS(computer) == RockPaperScissors.PAPER && RPS(you) == RockPaperScissors.PAPER)
                return WinDrawLose.DRAW;

            if (RPS(computer) == RockPaperScissors.PAPER && RPS(you) == RockPaperScissors.ROCK)
                return WinDrawLose.LOSE;

            if (RPS(computer) == RockPaperScissors.PAPER && RPS(you) == RockPaperScissors.SCISSORS)
                return WinDrawLose.WIN;
            #endregion

            #region Computer players scissors
            if (RPS(computer) == RockPaperScissors.SCISSORS && RPS(you) == RockPaperScissors.PAPER)
                return WinDrawLose.LOSE;

            if (RPS(computer) == RockPaperScissors.SCISSORS && RPS(you) == RockPaperScissors.ROCK)
                return WinDrawLose.WIN;

            if (RPS(computer) == RockPaperScissors.SCISSORS && RPS(you) == RockPaperScissors.SCISSORS)
                return WinDrawLose.DRAW;
            #endregion

            throw new ArgumentException($"{computer} or {you} input was in the wrong format");
        }

        private static RockPaperScissors RPS(char c)
        {
            return c switch
            {
                'B' or 'Y' => RockPaperScissors.PAPER,
                'A' or 'X' => RockPaperScissors.ROCK,
                'C' or 'Z' => RockPaperScissors.SCISSORS,
                _ => throw new ArgumentException($"{c} is not a valid character"),
            };
        }

        private static int GetPointsRPS(RockPaperScissors rps)
        {
            return rps switch
            {
                RockPaperScissors.ROCK => 1,
                RockPaperScissors.PAPER => 2,
                RockPaperScissors.SCISSORS => 3,
                _ => throw new ArgumentException($"input is not valid."),
            };
        }

        private int SolveForPart1()
        {
            return FileContent.Select(x => x switch
            {
                "A X" => 4,
                "A Y" => 8,
                "A Z" => 3,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 7,
                "C Y" => 2,
                "C Z" => 6,
                _ => throw new ArgumentException($"Input is not valid: {x}"),
            }).Sum();
        }

        private int SolveForPart2()
        {
            return FileContent.Select(x => x switch
            {
                "A X" => 3,
                "A Y" => 4,
                "A Z" => 8,
                "B X" => 1,
                "B Y" => 5,
                "B Z" => 9,
                "C X" => 2,
                "C Y" => 6,
                "C Z" => 7,
                _ => throw new ArgumentException($"Input is not valid: {x}"),
            }).Sum();
        }
    }
}