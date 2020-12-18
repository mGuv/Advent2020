using System;
using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day12
{
    /// <summary>
    /// Solution to Day 12 - Part A - Basic rotations and following 'forward'
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] input = this.GetInput(arguments);

            float angle = 90;
            (int x, int y) position = (0, 0);

            Dictionary<string, Action<int>> actions = new Dictionary<string, Action<int>>()
            {
                {"N", i => position.y += i},
                {"E", i => position.x += i},
                {"S", i => position.y -= i},
                {"W", i => position.x -= i},
                {
                    "L", i =>
                    {
                        angle -= i;
                        if (angle < 0)
                        {
                            angle += 360;
                        }
                    }
                },
                {
                    "R", i =>
                    {
                        angle += i;
                        if (angle >= 360)
                        {
                            angle -= 360;
                        }
                    }
                },
                {
                    "F", i =>
                    {
                        if (angle == 0)
                        {
                            position.y += i;
                        }

                        if (angle == 90)
                        {
                            position.x += i;
                        }

                        if (angle == 180)
                        {
                            position.y -= i;
                        }

                        if (angle == 270)
                        {
                            position.x -= i;
                        }
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
