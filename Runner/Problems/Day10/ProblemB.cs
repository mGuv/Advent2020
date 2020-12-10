using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day10
{
    /// <summary>
    /// Solution to Day 10 - Part B - Finally some optimisation. Possible paths is a huge number so time for shortcuts
    /// </summary>
    [Injectable]
    public class ProblemB : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            // Convert to ulong this time as I have a feeling the number will go large, this saves any int conversion
            HashSet<ulong> adapters = new HashSet<ulong>(this.GetInput(arguments).Select(ulong.Parse));

            // Sort them from smallest to largest so we can process in order
            IEnumerable<ulong> sorted = adapters.OrderBy(a => a);
            // Running count for many times each possible adapter can be used
            Dictionary<ulong, ulong> count = new Dictionary<ulong, ulong>();

            // If we add zero, we can use it to detect a match for adapters 1/2/3 and be sneaky
            adapters.Add(0);
            count.Add(0, 1);
            // Add our final device in order to count it at the end
            adapters.Add(adapters.Max() + 3);

            // Check each adapter in turn
            foreach (ulong adapter in sorted)
            {
                // We don't actually want to process zero as it'll end up saying there are 0 ways of making 0, ruining the chain
                if (adapter == 0)
                {
                    continue;
                }

                ulong ways = 0;

                // Check all possible compatible adapters and add their combinations together to give a running total of all possibilities
                // This saves us actually branching down every path, as we can imply the paths instead, knowing how many branches were at each node
                if (adapters.Contains(adapter - 1))
                {
                    ways += count[adapter - 1];
                }
                if (adapters.Contains(adapter - 2))
                {
                    ways += count[adapter - 2];
                }
                if (adapters.Contains(adapter - 3))
                {
                    ways += count[adapter - 3];
                }

                // Record the total for the next adapter to use
                count.Add(adapter, ways);
            }

            // All adapters processed, our device should be in the count dictionary with all paths summed.
            return $"{count[adapters.Max()]} possible adapter chains.";
        }
    }
}
