using AoC.Shared;
using System;
using System.Text.RegularExpressions;

namespace AoC2022
{
    public class BluePrint
    {
        public int Id { get; set; }
        public int OreRobot { get; set; }
        public int ClayRobot { get; set; }
        public (int ore, int clay) ObsidianRobot { get; set; }
        public (int ore, int obsidian) GeodeRobot { get; set; }

        public int GetMaxOre()
        {
            var maxOreOrClay = Math.Max(OreRobot, ClayRobot);
            var maxObsiOrGeodeOre = Math.Max(ObsidianRobot.ore, GeodeRobot.ore);

            return Math.Max(maxOreOrClay, maxObsiOrGeodeOre);
        }
    }

    public class Day19 : BaseDay
    {
        public Day19(int day, int year, bool isTest) : base(day, year, isTest) { }

        public Day19(int day, int year) : base(day, year) { }

        private List<BluePrint> _bluePrints;

        public override object SolvePart1()
        {
            ParseInput();
            var qualityLevels = 0;
            foreach (var bp in _bluePrints)
            {
                var best = Solve(bp, 24);
                qualityLevels += bp.Id * best;
            }

            return qualityLevels;
        }

        public override object SolvePart2()
        {
            ParseInput();
            var best = 1;
            foreach (var bp in _bluePrints.Take(3))
            {
                best *= Solve(bp, 32);
            }

            return best;
        }

        private int Solve(BluePrint b, int timeleft)
        {
            HashSet<(int, int, int, int, int, int, int, int, int)> states = new();
            Queue<(int, int, int, int, int, int, int, int, int)> queue = new();
            var startState = (0, 0, 0, 0, 1, 0, 0, 0, timeleft);
            var best = 0;

            queue.Enqueue(startState);

            while (queue.TryDequeue(out var state))
            {
                var (ore, clay, obsidian, geodes, oreRobs, clayRobs, obRobs, gRobs, time) = state;

                best = Math.Max(best, geodes);

                if (time == 0) continue;

                oreRobs = Math.Min(oreRobs, b.GetMaxOre());
                clayRobs = Math.Min(clayRobs, b.ObsidianRobot.clay);
                obRobs = Math.Min(obRobs, b.GeodeRobot.obsidian);

                ore = Math.Min(ore, (time * b.GetMaxOre()) - (oreRobs * (time - 1)));
                clay = Math.Min(clay, (time * b.ObsidianRobot.clay) - (clayRobs * (time - 1)));
                obsidian = Math.Min(obsidian, (time * b.GeodeRobot.obsidian) - (obRobs * (time - 1)));

                var curState = (ore, clay, obsidian, geodes, oreRobs, clayRobs, obRobs, gRobs, time);

                if (states.Contains(curState)) continue;

                states.Add(curState);

                queue.Enqueue((ore + oreRobs, clay + clayRobs, obsidian + obRobs, geodes + gRobs, oreRobs, clayRobs, obRobs, gRobs, time - 1));

                if (ore >= b.OreRobot) queue.Enqueue((ore + oreRobs - b.OreRobot, clay + clayRobs, obsidian + obRobs, geodes + gRobs, oreRobs + 1, clayRobs, obRobs, gRobs, time - 1));
                if (ore >= b.ClayRobot) queue.Enqueue((ore + oreRobs - b.ClayRobot, clay + clayRobs, obsidian + obRobs, geodes + gRobs, oreRobs, clayRobs + 1, obRobs, gRobs, time - 1));
                if (ore >= b.ObsidianRobot.ore && clay >= b.ObsidianRobot.clay) queue.Enqueue((ore + oreRobs - b.ObsidianRobot.ore, clay + clayRobs - b.ObsidianRobot.clay, obsidian + obRobs, geodes + gRobs, oreRobs, clayRobs, obRobs + 1, gRobs, time - 1));
                if (ore >= b.GeodeRobot.ore && obsidian >= b.GeodeRobot.obsidian) queue.Enqueue((ore + oreRobs - b.GeodeRobot.ore, clay + clayRobs, obsidian + obRobs - b.GeodeRobot.obsidian, geodes + gRobs, oreRobs, clayRobs, obRobs, gRobs + 1, time - 1));
            }

            return best;
        }

        private void ParseInput()
        {
            _bluePrints = new();
            var i = 0;
            while (i < FileContent.Count)
            {
                var blueprint = new BluePrint();
                var info = FileContent.Take(new Range(i, i + 5));
                foreach (var line in info)
                {
                    if (line.StartsWith("  Each ore robot costs"))
                    {
                        var oreRobotCost = Regex.Match(line, @"\d+").Value;
                        blueprint.OreRobot = int.Parse(oreRobotCost);
                    }
                    else if (line.StartsWith("  Each clay robot costs"))
                    {
                        var clayRobotCost = Regex.Match(line, @"\d+").Value;
                        blueprint.ClayRobot = int.Parse(clayRobotCost);
                    }
                    else if (line.StartsWith("  Each obsidian robot costs"))
                    {
                        var obsidianCost = Regex.Matches(line, @"\d+")
                            .Select(x => int.Parse(x.Value))
                            .ToArray();
                        blueprint.ObsidianRobot = (obsidianCost[0], obsidianCost[1]);
                    }
                    else if (line.StartsWith("  Each geode robot costs"))
                    {
                        var geodeCost = Regex.Matches(line, @"\d+")
                            .Select(x => int.Parse(x.Value))
                            .ToArray();
                        blueprint.GeodeRobot = (geodeCost[0], geodeCost[1]);
                    }
                    else
                    {
                        var id = Regex.Match(line, @"\d+").Value;
                        blueprint.Id = int.Parse(id);
                    }
                }
                _bluePrints.Add(blueprint);
                i += 6;
            }
        }
    }
}

