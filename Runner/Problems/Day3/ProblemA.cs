using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day3
{
    /// <summary>
    /// Solution to Day3 - Part A, Nested loops but now with modulo
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override async IAsyncEnumerable<string> RunAsync(Arguments arguments)
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

            int treesHit = 0;

            for (int y = 1; y < parts.Length; y++)
            {
                if (trees.Contains(((y *3) % parts[y].Length, y)))
                {
                    treesHit++;
                }
            }

            yield return $"Hit {treesHit} on the way, there are {trees.Count()} trees counted";
        }
    }
}
