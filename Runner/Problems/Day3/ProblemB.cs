using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day3
{
    /// <summary>
    /// Solution to Day3 - Part B, Nested loops but now with modulo
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] parts = this.GetInput(arguments);
            HashSet<(int, int)> trees = new HashSet<(int, int)>();

            for (int y = 0; y < parts.Length; y++)
            {
                for (int x = 0; x < parts[y].Length; x++)
                {
                    if (parts[y][x] == '#')
                    {
                        trees.Add((x, y));
                    }
                }
            }

            (int, int)[] slopes =
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };


            ulong totalHit = slopes.Select(s =>
            {
                writer.WriteLine($"Checking slope ({s.Item1}, {s.Item2})");
                ulong treesHit = 0;
                for (int y = 1; y < parts.Length; y++)
                {
                    if (trees.Contains(((y * s.Item1) % parts[y].Length, y * s.Item2)))
                    {
                        treesHit++;
                    }
                }

                return treesHit;
            }).Aggregate((a, b) => a * b);


            return $"Hit {totalHit} on the way";
        }
    }
}
