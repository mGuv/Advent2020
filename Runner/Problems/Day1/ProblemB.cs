using System.Collections.Generic;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day1
{
    /// <summary>
    /// Solution to Day1 - Part B, utilising more nested loops
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        /// <inheritdoc/>
        public override async IAsyncEnumerable<string> RunAsync(Arguments arguments)
        {
            string input = this.GetRawInput(arguments);

            string[] parts = input.Split("\n");


            for (int x = 0; x < parts.Length - 3; x++)
            {
                for (int y = x + 1; y < parts.Length - 2; y++)
                {
                    for (int z = y + 1; z < parts.Length - 1; z++)
                    {
                        int a = int.Parse(parts[x].Trim());
                        int b = int.Parse(parts[y].Trim());
                        int c = int.Parse(parts[z].Trim());

                        if (a + b + c == 2020)
                        {
                            yield return (a * b * c).ToString();
                        }
                    }
                }
            }
        }
    }
}
