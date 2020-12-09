using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day9
{
    /// <summary>
    /// Solution to Day 9 - Part B - Sliding window loop, but probably has a more elegant solution
    /// </summary>
    [Injectable]
    public class ProblemB: FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] input = this.GetInput(arguments);
            ulong[] parsed = input.Select(ulong.Parse).ToArray();
            // Bad as this is hardcoded but it relied on answer from PArt A
            ulong toFind = 393911906;

            // Check from start to end
            for (int i = 0; i < parsed.Length; i++)
            {
                // Count and keep each number in turn
                ulong totalSoFar = 0;
                List<ulong> series = new List<ulong>();
                for (int j = i + 1; j < parsed.Length; j++)
                {
                    // Add it to running total/set
                    totalSoFar += parsed[j];
                    series.Add(parsed[j]);

                    // No point continuing if we're over our target
                    if (totalSoFar > toFind)
                    {
                        break;
                    }

                    // If it matches the target, it's what we want
                    if (totalSoFar == toFind)
                    {
                        return $"{series.Min() + series.Max()}";
                    }
                }
            }

            return "well, no answer";
        }
    }
}
