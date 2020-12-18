using System;
using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day12
{
    /// <summary>
    /// Solution to Day 12 - Part B - Basic 90 degree rotations around a moving point
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] input = this.GetInput(arguments);

            (int x, int y) position = (0, 0);
            (int x, int y) waypoint = (10, 1);

            Dictionary<string, Action<int>> actions = new Dictionary<string, Action<int>>()
            {
                {"N", i => waypoint.y += i},
                {"E", i => waypoint.x += i},
                {"S", i => waypoint.y -= i},
                {"W", i => waypoint.x -= i},
                {
                    "L", i =>
                    {
                        int x = waypoint.x;
                        int y = waypoint.y;
                        if (i == 90)
                        {
                            waypoint.x = y * -1;
                            waypoint.y = x;
                        }

                        if (i == 180)
                        {
                            waypoint.x = x * -1;
                            waypoint.y = y * -1;
                        }

                        if (i == 270)
                        {
                            waypoint.x = y;
                            waypoint.y = x * -1;
                        }
                    }
                },
                {
                    "R", i =>
                    {
                        int x = waypoint.x;
                        int y = waypoint.y;
                        if (i == 270)
                        {
                            waypoint.x = y * -1;
                            waypoint.y = x;
                        }

                        if (i == 180)
                        {
                            waypoint.x = x * -1;
                            waypoint.y = y * -1;
                        }

                        if (i == 90)
                        {
                            waypoint.x = y;
                            waypoint.y = x * -1;
                        }
                    }
                },
                {
                    "F", i =>
                    {
                        position.x += waypoint.x * i;
                        position.y += waypoint.y * i;
                    }
                }
            };

            for (int i = 0; i < input.Length; i++)
            {
                string action = input[i].Substring(0, 1);
                int amount = int.Parse(input[i].Substring(1));

                actions[action](amount);
            }

            return $"{Math.Abs(position.x) + Math.Abs(position.y)}";
        }
    }
}
