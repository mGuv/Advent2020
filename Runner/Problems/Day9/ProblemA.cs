using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day9
{
    /// <summary>
    /// Solution to Day 9 - Part A - Oh, just more loops and math :x
    /// </summary>
    [Injectable]
    public class ProblemA: FileProblem
    {
        public override string Run(Arguments arguments, Writer writer)
        {
            // Turn them all to ulongs as they looked suspiciously large but no negatives
            string[] input = this.GetInput(arguments);
            ulong[] parsed = input.Select(ulong.Parse).ToArray();

            // Check from 25 onwards
            for (int i = 25; i < parsed.Length; i++)
            {
                ulong next = parsed[i];
                bool found = false;

                // check all 25 digit before it against each other
                for (int x = i - 25; x < i; x++)
                {
                    for (int y = x + 1; y < i; y++)
                    {
                        ulong a = parsed[x];
                        ulong b = parsed[y];

                        // Says the two numbers can't equal. Unsure if they meant the same index or if numbers can repeat
                        if (a == b)
                        {
                            continue;
                        }

                        // Check if the number mathces
                        if (a + b == next)
                        {
                            // It does, bail out
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        // Found in inner loop so bail this index
                        break;
                    }
                }

                // All previous 25 checked against each other now, if it wasn't found, it's the number we're after
                if (!found)
                {
                    return $"{next} is invalid";
                }
            }

            return "well, no answer";
        }
    }
}
