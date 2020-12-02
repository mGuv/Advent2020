using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day1
{
    /// <summary>
    /// Solution to Day1 - Part A, utilising nested loops
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override async IAsyncEnumerable<string> RunAsync(Arguments arguments)
        {
            string[] parts = this.GetInput(arguments);
            int iterations = 0;

            for (int x = 0; x < parts.Length - 1; x++)
            {
                for (int y = x + 1; y < parts.Length; y++)
                {
                    iterations++;
                    int a = int.Parse(parts[x].Trim());
                    int b = int.Parse(parts[y].Trim());

                    if (a + b == 2020)
                    {
                        yield return $"Answer: {(a * b).ToString()} - iterations: {iterations}";
                    }
                }
            }
        }
    }
}
