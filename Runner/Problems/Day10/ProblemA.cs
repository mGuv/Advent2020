using System.Collections.Generic;
using System.Linq;
using Advent2020.DI;
using Runner.Console;

namespace Runner.Problems.Day10
{
    /// <summary>
    /// Solution to Day 10 - Part A - Again just loops, checks and math
    /// </summary>
    [Injectable]
    public class ProblemA : FileProblem
    {
        /// <inheritdoc/>
        public override string Run(Arguments arguments, Writer writer)
        {
            string[] input = this.GetInput(arguments);
            HashSet<int> adapters = new HashSet<int>(input.Select(int.Parse));

            // Track voltage as it steps up, counting everytime we use +1 and +3
            int currentVoltage = 0;
            int jolt1 = 0;
            int jolt3 = 1;
            while (true)
            {
                // Always check smallest first in order to create longest chain
                if (adapters.Contains(currentVoltage + 1))
                {
                    currentVoltage += 1;
                    jolt1++;
                    continue;
                }

                if (adapters.Contains(currentVoltage + 2))
                {
                    currentVoltage += 2;
                    continue;
                }
                if (adapters.Contains(currentVoltage + 3))
                {
                    currentVoltage += 3;
                    jolt3++;
                    continue;
                }

                break;
            }

            // Multiply both parts together
            return $"{jolt1 * jolt3} jolt1 to jolt3 difference";
        }
    }
}
